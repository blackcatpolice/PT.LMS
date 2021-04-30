using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Pagetechs.Framework.Dtos.ModolHelper
{
    public class ModelMapper
    {
        DbContext dbContext;
        //AppService appService;
        public ModelMapper(DbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<List<ModelInfo>> ModelToFormControl<T>()
        {
            var modelType = typeof(T);

            return await GetModelInfo(modelType);
        }

        private async Task<List<ModelInfo>> GetModelInfo(Type modelType)
        {
            var props = modelType.GetProperties();

            var modelInfoList = new List<ModelInfo>();

            foreach (var prop in props)
            {
                if (prop.CustomAttributes.Any(x => x.AttributeType == typeof(ModelType)))
                {
                    object[] attrs = prop.GetCustomAttributes(true);
                    foreach (object attr in attrs)
                    {
                        ModelType modelTypeAttr = attr as ModelType;
                        if (modelTypeAttr != null)
                        {
                            string propName = prop.Name;
                            string mtName = modelTypeAttr.Name;
                            var dataSource = new Dictionary<string, string>();
                            if (!string.IsNullOrEmpty(modelTypeAttr.DataSource))
                            {
                                var dsArr = modelTypeAttr.DataSource.Split(',');
                                foreach (var item in dsArr)
                                {
                                    dataSource.Add(item.Trim(), item.Trim());
                                }
                            }
                            else if (!string.IsNullOrEmpty(modelTypeAttr.DataSourceTable))
                            {
                                dataSource = await GetDataSourceData(modelTypeAttr.DataSourceTable, modelTypeAttr.DataSourceQuery, modelTypeAttr.DataSourceTableValue, modelTypeAttr.DataSourceTableParentId);

                                if (dataSource == null)
                                {
                                    dataSource = GetDataSrouceFromMeta(attrs);
                                }
                            }



                            List<ModelInfo> subModelInfoList = null;
                            string typeName = "";
                            //if (modelTypeAttr.ControlType == ControlType.DynamicGroup)
                            //{
                            //    //subModelInfoList = await GetModelInfo(prop.PropertyType.GenericTypeArguments[0]);
                            //    typeName = prop.PropertyType.GenericTypeArguments[0].Name;
                            //}
                            typeName = Enum.GetName(typeof(ControlType), modelTypeAttr.ControlType).ToLower();

                            modelInfoList.Add(new ModelInfo
                            {
                                Name = modelTypeAttr.Name,
                                PropName = propName,
                                ControlType = modelTypeAttr.ControlType,
                                DataSource = dataSource,
                                Order = modelTypeAttr.Order,
                                ShowOnList = modelTypeAttr.ShowOnList,
                                Required = modelTypeAttr.Required,
                                TypeName = typeName,
                                SubModelInfoList = subModelInfoList
                            });
                            continue;
                        }
                    }
                }
                else
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "ListGeneric":
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                PropName = prop.Name,
                                ControlType = ControlType.List,
                                TypeName = "list",
                                Order = modelInfoList.Count()
                            });
                            break;
                        case "Boolean":
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                PropName = prop.Name,
                                ControlType = ControlType.CheckBox,
                                TypeName = "bool",
                                Order = modelInfoList.Count()
                            });
                            break;
                        case "Decimber":
                        case "Int64":
                        case "Int32":
                        case "Double":
                        case "Float":
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                ControlType = ControlType.Number,
                                PropName = prop.Name,
                                TypeName = "number",
                                Order = modelInfoList.Count()
                            });
                            break;
                        case "String":
                        default:
                            modelInfoList.Add(new ModelInfo
                            {
                                Name = prop.Name,
                                PropName = prop.Name,
                                TypeName = "string",
                                ControlType = ControlType.Input,
                                Order = modelInfoList.Count()
                            });
                            break;
                    }
                }
            }

            return modelInfoList;
        }



        public async Task<ModelData> ModelToData<T>(T model)
        {
            var modelType = typeof(T);

            var props = modelType.GetProperties();

            var modelData = new ModelData();

            foreach (var prop in props)
            {
                //if (prop.CustomAttributes.Any(x => x.AttributeType == typeof(ModelType)))
                //{
                //    object[] attrs = prop.GetCustomAttributes(true);
                //    List<ModelData> ModelDataList = null;
                //    foreach (object attr in attrs)
                //    {
                //        ModelType modelTypeAttr = attr as ModelType;
                //        if (modelTypeAttr != null)
                //        {
                //            string propName = prop.Name;
                //            string mtName = modelTypeAttr.Name;

                //            if (modelTypeAttr.ControlName == "dynamicgroup")
                //            {
                //                ModelDataList = await GetSubDataList(modelTypeAttr.DataSourceTable, modelTypeAttr.DataSourceQuery, prop.PropertyType.GenericTypeArguments[0]);
                //            }
                //        }
                //    }
                //    modelData.Add(prop.Name, ModelDataList);
                //}
                //else
                //{
                //    modelData.Add(prop.Name, prop.GetValue(model));
                //}
                modelData.Add(prop.Name, prop.GetValue(model));
            }

            return modelData;
        }


        public async Task<ModelTable> ModelToTable<T>()
        {
            var modelType = typeof(T);

            var props = modelType.GetProperties();

            var modelInfoList = await ModelToFormControl<T>();

            var modelTable = new ModelTable();
            modelTable.TableInfoList = new List<TableInfo>();

            if (modelInfoList.Any(x => x.ShowOnList))
            {
                foreach (var modelInfo in modelInfoList.Where(x => x.ShowOnList))
                {
                    modelTable.TableInfoList.Add(new TableInfo
                    {
                        Name = modelInfo.Name,
                        PropName = modelInfo.PropName
                    });
                }
            }
            else
            {
                foreach (var modelInfo in modelInfoList.Take(4))
                {
                    modelTable.TableInfoList.Add(new TableInfo
                    {
                        Name = modelInfo.Name,
                        PropName = modelInfo.PropName
                    });
                }
            }

            return modelTable;
        }

        private Dictionary<string, string> GetDataSrouceFromMeta(object[] attrs)
        {
            var resultData = new Dictionary<string, string>();
            foreach (var attr in attrs)
            {
                ModelBindData bindDataAttr = attr as ModelBindData;
                if (bindDataAttr != null)
                {
                    resultData.Add(bindDataAttr.Key, bindDataAttr.Value);
                }
            }

            return resultData;
        }

        private async Task<Dictionary<string, string>> GetDataSourceData(string table, string query, string value, string parentId)
        {
            List<dynamic> dataResult;
            if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (!string.IsNullOrEmpty(query))
            {
                dataResult = await dbContext.Query(table).Where(query).ToDynamicListAsync<dynamic>();
            }
            else
            {
                dataResult = dbContext.Query(table).ToDynamicList();
            }
            var result = new Dictionary<string, string>();
            foreach (dynamic item in dataResult)
            {
                Type type = item.GetType();
                var props = type.GetProperties();

                var idObj = props.FirstOrDefault(x => x.Name == "Id").GetValue(item);// item["Id"];
                var valueObj = props.FirstOrDefault(x => x.Name == value).GetValue(item);
                if (!string.IsNullOrEmpty(parentId))
                {
                    var pidObj = props.FirstOrDefault(x => x.Name == parentId).GetValue(item);
                    result.Add(idObj?.ToString() ?? "", valueObj?.ToString() ?? "" + "|" + pidObj?.ToString() ?? "top");
                }
                else
                {
                    result.Add(idObj?.ToString() ?? "", valueObj?.ToString() ?? "");
                }
            }
            return result;
        }


    }

}

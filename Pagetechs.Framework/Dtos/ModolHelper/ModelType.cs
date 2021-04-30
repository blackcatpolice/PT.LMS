using System;
using System.Collections.Generic;
using System.Text;

namespace Pagetechs.Framework.Dtos.ModolHelper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelType : Attribute
    {
        public ModelType()
        {

        }
        public string Name { get; set; }

        public ControlType ControlType { get; set; }

        /// <summary>
        /// 与DataSourceTable  方式互斥
        /// 格式 value1,value2,value3
        /// </summary>
        public string DataSource { get; set; }

        public bool Required { get; set; }

        /// <summary>
        /// 数据源:对应数据库查询的表名
        /// 用法:  DataSourceTable = "Category"
        ///  , DataSourceTableValue = "Name"
        /// </summary>
        public string DataSourceTable { get; set; }

        /// <summary>
        /// 前端显示的文本对应表的字段
        ///  用法:  DataSourceTable = "Category"
        ///  , DataSourceTableValue = "Name"
        /// </summary>
        public string DataSourceTableValue { get; set; }

        /// <summary>
        /// 查询表的where 表达式
        ///  用法:  DataSourceTable = "Category"
        ///  , DataSourceTableValue = "Name" 的基础上使用
        /// </summary>
        public string DataSourceQuery { get; set; }

        public bool ShowOnList { get; set; } = true;

        public int Order { get; set; } = 0; 
        public string DataSourceTableParentId { get; set; }

        /// <summary>
        /// Service in AppService, the expression like [serviceName],[method],[param]
        /// </summary>
        public string DataSourceExpression { get; set; }
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ModelBindData : Attribute
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}

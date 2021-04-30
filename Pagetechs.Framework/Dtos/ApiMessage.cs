using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagetechs.Framework.Dtos
{
    public class ApiMessage
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = "success!";

        public string Code { get; set; } = "0";

        public dynamic Data { get; set; }

        public int Count { get; set; }

        public static async Task<ApiMessage> Wrap(Func<Task> action)
        {
            var apiMsg = new ApiMessage();
            try
            {
                await action();
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }

            return apiMsg;
        }

        public static async Task<ApiMessage> Wrap<T>(Func<Task<T>> func)
        {
            var apiMsg = new ApiMessage();
            try
            {
                apiMsg.Data = await func();
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc.StackTrace);
            }
            return apiMsg;
        }

        public static async Task<ApiMessage> WrapAndTuple<T>(Func<Task<Tuple<List<T>, int>>> func)
        {
            var apiMsg = new ApiMessage();
            try
            {
                var tupleData = await func();
                apiMsg.Data = tupleData.Item1;
                apiMsg.Count = tupleData.Item2;
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc.StackTrace);
            }
            return apiMsg;
        }

        public static async Task<T> WrapData<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception exc)
            {
                return default(T);
            }
        }

        public static object UnWrap(string dataJson)
        {
            var setting = new JsonSerializerSettings();
            setting.NullValueHandling = NullValueHandling.Ignore;
            setting.Error = HandleDeserializationError;
            //dataJson = dataJson.Replace("_id", "bid")
            //    .Replace("ObjectId", "")
            //    .Replace("(", "")
            //    .Replace(")", "");

            var apiMsg = JsonConvert.DeserializeObject<ApiMessage>(dataJson, setting);
            return apiMsg.Data;
        }
        public static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }
        public void SetFault(Exception exc)
        {
            SetFault(exc.Message);
        }

        public void SetFault(string msg)
        {

            Success = false;
            Message = msg;
        }
    }
}

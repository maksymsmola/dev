using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MoneyKeeper.Web.ActionResults
{
    public class CustomJsonResult : ActionResult
    {
        private readonly object data;

        public CustomJsonResult(object data)
        {
            this.data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = "application/json";
            response.ContentEncoding = Encoding.UTF8;

            var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            response.Write(JsonConvert.SerializeObject(this.data, Formatting.None, serializerSettings));
        }
    }
}
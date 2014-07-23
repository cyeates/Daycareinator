using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using WebApiContrib.Formatting.Jsonp;

namespace Daycareinator
{
    public class FormatterConfig
    {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            //formatters.Remove(formatters.JsonFormatter);
            //formatters.Insert(0, new JsonpMediaTypeFormatter(
            //{
            //    SerializerSettings = new JsonSerializerSettings
            //    {
            //        ContractResolver = new CamelCasePropertyNamesContractResolver()
            //    }
            //});
        }
    }
}
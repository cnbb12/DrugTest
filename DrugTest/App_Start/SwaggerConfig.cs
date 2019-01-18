using System.Web.Http;
using WebActivatorEx;
using DrugTest;
using Swashbuckle.Application;
using DrugTest.App_Start;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace DrugTest
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Drugs");
                c.IncludeXmlComments(GetXmlCommentsPath());
                c.OperationFilter<HttpHeaderFilter>();
            }).EnableSwaggerUi("help/{*assetPath}", c => { }); //将访问地址改为 help/index
        }
        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}/bin/DrugTest.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}

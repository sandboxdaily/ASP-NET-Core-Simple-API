using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace SimpleCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly IConfiguration _config;


        public ValuesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            bool enabledLog;           
            Boolean.TryParse(_config.GetValue<string>("LogJson:Enabled"), out enabledLog);                      

            var logFolder = _config.GetValue<string>("LogJson:Path");

            var result = new
            {
                Machine = Environment.MachineName,
                Environment = _config.GetValue<string>("ASPNETCORE_ENVIRONMENT"),
                LogJsonPath = logFolder
            };

            if (!enabledLog)
                return Ok(result);

            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }

            var jsonBody = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);

            if (jsonBody.Length > 0)
            {
                System.IO.File.WriteAllText($"{logFolder}\\{DateTime.Now.ToString("yyyyMMddHHmmss")}.json", jsonBody);
            }

            // return Ok($"Environment: {_config.GetValue<string>("ASPNETCORE_ENVIRONMENT")}, LogJson Path:  {_config.GetValue<string>("LogJson:Path")}");
            return Ok(result);
        }


    }
}

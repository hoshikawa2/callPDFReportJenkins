using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace callPDFReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallPDFReportController : ControllerBase
    {

        private readonly ILogger<CallPDFReportController> _logger;
        private static readonly HttpClient HttpClient = new HttpClient();

        public CallPDFReportController(ILogger<CallPDFReportController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get(String sku, String nome, String lista, String preco)
        {
            //Chamada local - teste
            //HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("http://150.136.184.232:8080?url=http://150.136.184.232:32333/showhtml?sku=" + sku + "&nome=" + nome + "&lista=" + lista + "&preco=" + preco);
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("http://tecnospeed-service:8080?url=http://runhtml-service:8080/showhtml?sku=" + sku + "&nome=" + nome + "&lista=" + lista + "&preco=" + preco);

            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/octet-stream; charset=UTF-8";
            httpRequest.AllowAutoRedirect = false;

            Console.WriteLine("ok");

            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream stream = response.GetResponseStream();
                FileStreamResult fsr = new FileStreamResult(stream, "application/pdf");
                return fsr;
            }
            return null;
        }
    }
}
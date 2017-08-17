using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PoCIntegracaoSEI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void WSSei()
        {
            WebClient client = new WebClient();

            CredentialCache cc = new CredentialCache();
            cc.Add(
                new Uri("http://homologasei.dnpm.gov.br/sei/controlador_ws.php?servico=sei"),
                "NTLM",
                new NetworkCredential("breno.machado", "12345678", "dnpmnet"));

            client.Credentials = cc;

            string reply = client.DownloadString("http://homologasei.dnpm.gov.br/sei/controlador_ws.php?servico=sei");


            //Response.Write(System.IO.File.ReadAllText(@"c:\wsdlSEI.xml"));
            Response.ContentType = "application/soap+xml";
            Response.Write(reply);
            Response.End();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoCIntegracaoSEI.SeiServices;

namespace PoCIntegracaoSEI.Controllers
{
    public class SeiIntegrationController : Controller
    {
        // GET: SeiIntegration
        public ActionResult Index()
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            SeiServices.SeiPortTypeClient seiClient = new SeiServices.SeiPortTypeClient();
            seiClient.Endpoint.Binding.SendTimeout = new TimeSpan(0, 5, 0);
            seiClient.Endpoint.Binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            seiClient.Endpoint.Binding.OpenTimeout = new TimeSpan(0, 5, 0);
            seiClient.Endpoint.Binding.CloseTimeout = new TimeSpan(0, 5, 0);

            seiClient.ClientCredentials.UserName.UserName = @"dnpmnet\breno.machado";
            seiClient.ClientCredentials.UserName.Password = @"12345678";

            var documentoSeiList = new List<SeiServices.Documento>();

            documentoSeiList.Add(new Documento
            {
                Conteudo = Convert.ToBase64String(System.IO.File.ReadAllBytes(@"c:\testeSEI.pdf")),
                //ConteudoMTOM = documento.BIDocumento,
                Data = DateTime.Now.ToString("dd/MM/yyyy"),
                Descricao = "Upload Teste SEI - REFIS",
                IdSerie = "337",
                NivelAcesso = "0",
                NomeArquivo = "testeSeiRefis.pdf",
                Tipo = "R",
                Destinatarios = new Destinatario[] { },
                Interessados = new Interessado[] { },
                Remetente = new Remetente { Nome = "REFIS", Sigla = "REFIS" },
                SinBloqueado = "N",
                Numero = "686",
                Observacao = "Documento enviado via REFIS",
            });
            
            var proc = new Procedimento
            {
                // Arrecadação: Cobrança
                IdTipoProcedimento = "100000721",
                Especificacao = "Processo de Cobrança nº 992.323/2017",
                NivelAcesso = "0",
                Assuntos = new Assunto[] { },
                Interessados = new Interessado[] { },
            };

            var retorno = seiClient.gerarProcedimento("REFIS", "Cadastro de processo eletrônico", "110000834", proc, documentoSeiList.ToArray(), null, new String[] { }, "S", "N", "", "", "N");
            
            ViewBag.IdProcesso = retorno.IdProcedimento;
            ViewBag.LinkAcesso = retorno.LinkAcesso;
            ViewBag.ProcedimentoFormatado = retorno.ProcedimentoFormatado;
            ViewBag.RetornoInclusaoDocumentos = retorno.RetornoInclusaoDocumentos.ToDictionary(rid => rid.IdDocumento, rid => new Tuple<string, string>(rid.DocumentoFormatado, rid.LinkAcesso));

            return View();
        }
    }
}
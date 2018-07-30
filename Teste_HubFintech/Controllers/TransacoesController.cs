using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teste_HubFintech.Business;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Controllers
{
    public class TransacoesController : ApiController
    {
        TransacoesBusiness tBusiness = new TransacoesBusiness();

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int transacaoId)
        {
            return tBusiness.Consultar(transacaoId);
        }

        [HttpPost]
        public HttpResponseMessage Post(Transacoes t)
        {
            try
            {
                if (ModelState.IsValid && t != null)
                {
                    HttpStatusCode httpStatus = HttpStatusCode.OK;
                    string msg = tBusiness.MovimentacaoContas(t);
                    if (msg.Length <= 0)
                        msg = "Registro cadastrado com sucesso!";
                    else
                        httpStatus = HttpStatusCode.BadRequest;

                    var response = new HttpResponseMessage(httpStatus)
                    {
                        Content = new StringContent(msg)
                    };
                    return response;
                }
                else
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro. Por favor verifique os dados enviados.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teste_HubFintech.Business;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Controllers
{
    public class PessoasController : ApiController
    {
        PessoasBusiness pBusiness = new PessoasBusiness();

        [HttpGet]
        public string Get(int id)
        {
            return pBusiness.Consultar(id);
        }

        [HttpPost]
        public HttpResponseMessage Post(Pessoas p)
        {
            try
            {
                if (ModelState.IsValid && p != null)
                {
                    string msg = pBusiness.Incluir(p);

                    if (msg.Length <= 0)
                        msg = "Registro cadastrado com sucesso!";

                    var response = new HttpResponseMessage(HttpStatusCode.Created)
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

        [HttpPut]
        public HttpResponseMessage Put(Pessoas p)
        {
            try
            {
                if (ModelState.IsValid && p != null)
                {
                    string msg = pBusiness.Alterar(p);

                    if (msg.Length <= 0)
                        msg = "Registro cadastrado com sucesso!";
                    
                    var response = new HttpResponseMessage(HttpStatusCode.Accepted)
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

        [HttpDelete]
        public HttpResponseMessage Delete(int? id)
        {
            try
            {
                if (ModelState.IsValid && id != null)
                {
                    string msg = pBusiness.Excluir(id.Value);

                    var response = new HttpResponseMessage(HttpStatusCode.Accepted)
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

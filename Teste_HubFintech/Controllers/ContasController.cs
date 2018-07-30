using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teste_HubFintech.Business;
using Teste_HubFintech.Model;

namespace Teste_HubFintech.Controllers
{
    public class ContasController : ApiController
    {
        ContasBusiness CBusiness = new ContasBusiness();

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public string Get(int id)
        {
            return CBusiness.Consultar(id);
        }

        [HttpPost]
        public HttpResponseMessage Post(Contas c)
        {
            try
            {
                if (ModelState.IsValid && c != null)
                {
                    string msg = CBusiness.Incluir(c);

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
        public HttpResponseMessage Put(Contas c)
        {
            try
            {
                if (ModelState.IsValid && c != null)
                {
                    string msg = CBusiness.Alterar(c);

                    if (msg.Length <= 0)
                        msg = "Registro atualizado com sucesso!";

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
                    string msg = CBusiness.Excluir(id.Value);

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
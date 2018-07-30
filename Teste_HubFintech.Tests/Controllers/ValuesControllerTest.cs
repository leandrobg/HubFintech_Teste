using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teste_HubFintech;
using Teste_HubFintech.Controllers;

namespace Teste_HubFintech.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Organizar
            PessoasController controller = new PessoasController();

            // Agir
            IEnumerable<string> result = controller.Get();

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Organizar
            PessoasController controller = new PessoasController();

            // Agir
            string result = controller.Get(5);

            // Declarar
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Organizar
            PessoasController controller = new PessoasController();

            // Agir
            //controller.Post("value");

            // Declarar
        }

        [TestMethod]
        public void Put()
        {
            // Organizar
            PessoasController controller = new PessoasController();

            // Agir
            //controller.Put(5, "value");

            // Declarar
        }

        [TestMethod]
        public void Delete()
        {
            // Organizar
            PessoasController controller = new PessoasController();

            // Agir
            //controller.Delete(5);

            // Declarar
        }
    }
}

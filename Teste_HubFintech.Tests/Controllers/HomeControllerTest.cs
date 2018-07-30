using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Teste_HubFintech;
using Teste_HubFintech.Controllers;

namespace Teste_HubFintech.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Organizar
            HomeController controller = new HomeController();

            // Agir
            ViewResult result = controller.Index() as ViewResult;

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}

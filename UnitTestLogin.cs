using Microsoft.AspNetCore.Mvc;
using ProjetoQSW.Controllers;
using ProjetoQSW.Data;
using ProjetoQSW.ViewModels;

namespace ProjetoQSWTeste
{
    [TestClass]
    public class UnitTestLogin
    {
        [TestMethod]
        public void TestLoginBanco()
        {
            // AQUI ESTOU POPULANDO O BANCO SIMULADO COM DADOS PRÉ EXISTENTES QUE PODEM SER VISUALIZADOS NA CLASSE EscolinhaBancoSimulado
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // AQUI CRIAMOS UMA VIEW MODEL SIMULANDO UMA TENTATIVA DE LOGIN COM ALGUM LOGIN E SENHA DO BANCO SIMULADO
            var viewModel = new LoginViewModel();
            viewModel.Login = db.Alunos.FirstOrDefault().Login;
            viewModel.Senha = db.Alunos.FirstOrDefault(x => x.Login == viewModel.Login).Senha;

            var homeController = new HomeController();
            var resultado = homeController.Login(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Inscrever", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.Login, resultado.RouteValues["login"]);
        }

        [TestMethod]
        public void TestLogin_ErrosNoModelStateQuandoCredenciaisNulas()
        {
            // Arrange
            var viewModel = new LoginViewModel(); // Credenciais nulas
            var homeController = new HomeController();

            // Act
            var resultado = homeController.Login(viewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.IsFalse(resultado.ViewData.ModelState.IsValid);
            Assert.AreEqual(2, resultado.ViewData.ModelState.ErrorCount);
            Assert.AreEqual("O login é obrigatório", resultado.ViewData.ModelState["login"].Errors[0].ErrorMessage);
            Assert.AreEqual("A senha é obrigatória", resultado.ViewData.ModelState["senha"].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void TestLoginErrado()
        {
            // AQUI ESTOU POPULANDO O BANCO SIMULADO COM DADOS PRÉ EXISTENTES QUE PODEM SER VISUALIZADOS NA CLASSE EscolinhaBancoSimulado
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // AQUI CRIAMOS UMA VIEW MODEL SIMULANDO UMA TENTATIVA DE LOGIN COM ALGUM LOGIN E SENHA DO BANCO SIMULADO
            var viewModel = new LoginViewModel();
            viewModel.Login = "TESTE LOGIN ERRADO";
            viewModel.Senha = "TESTE SENHA ERRADA";

            var homeController = new HomeController();
            var resultado = homeController.Login(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Inscrever", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.Login, resultado.RouteValues["login"]);
        }
    }
}
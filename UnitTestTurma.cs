using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using ProjetoQSW.Controllers;
using ProjetoQSW.Data;
using ProjetoQSW.ViewModels;

namespace ProjetoQSWTeste
{
    [TestClass]
    public class UnitTestTurma
    {
        [TestMethod]
        public void TestTurmaInscricao()
        {
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();
            
            // SIMULANDO UM ALUNO VÁLIDO
            var viewModel = new TurmaViewModel();
            viewModel.AlunoId = db.Alunos.FirstOrDefault().Id;
            viewModel.Aluno = db.Alunos.FirstOrDefault(x => x.Id == viewModel.AlunoId);

            // CHAMANDO OS MÉTODOS DE VERIFICAÇÃO DAS REGRAS DE NEGÓCIO

            var turmaController = new TurmaController();
            var httpContextMock = new Mock<HttpContext>();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                // ADICIONANDO AS MATÉRIAS
                { "selecionarMateria", new StringValues(new [] {"10", "14"}) }
            });
            httpContextMock.Setup(c => c.Request.Form).Returns(formCollection);
            turmaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var resultado = turmaController.Inscrever(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Index", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.AlunoId, resultado.RouteValues["idAluno"]);
        }


        [TestMethod]
        public void TestTurmaCheia()
        {
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // SIMULANDO UM ALUNO VÁLIDO
            var viewModel = new TurmaViewModel();
            viewModel.AlunoId = db.Alunos.FirstOrDefault().Id;
            viewModel.Aluno = db.Alunos.FirstOrDefault(x => x.Id == viewModel.AlunoId);

            // CHAMANDO OS MÉTODOS DE VERIFICAÇÃO DAS REGRAS DE NEGÓCIO

            var turmaController = new TurmaController();
            var httpContextMock = new Mock<HttpContext>();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                // ADICIONANDO AS MATÉRIAS
                { "selecionarMateria", new StringValues(new [] {"10", "14"}) }
            });
            httpContextMock.Setup(c => c.Request.Form).Returns(formCollection);
            turmaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };
            // SIMULANDO TURMA LOTADA
            viewModel.Turmas.FirstOrDefault(x => x.MateriaId == 10).Vagas = 0;

            // Act
            var resultado = turmaController.Inscrever(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Index", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.AlunoId, resultado.RouteValues["idAluno"]);
        }

        [TestMethod]
        public void TestChoqueHorarios()
        {
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // SIMULANDO UM ALUNO VÁLIDO
            var viewModel = new TurmaViewModel();
            viewModel.AlunoId = db.Alunos.FirstOrDefault().Id;
            viewModel.Aluno = db.Alunos.FirstOrDefault(x => x.Id == viewModel.AlunoId);

            // CHAMANDO OS MÉTODOS DE VERIFICAÇÃO DAS REGRAS DE NEGÓCIO

            var turmaController = new TurmaController();
            var httpContextMock = new Mock<HttpContext>();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                // ADICIONANDO AS MATÉRIAS
                { "selecionarMateria", new StringValues(new [] {"11", "14"}) }
            });
            httpContextMock.Setup(c => c.Request.Form).Returns(formCollection);
            turmaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var resultado = turmaController.Inscrever(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Index", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.AlunoId, resultado.RouteValues["idAluno"]);
        }

        [TestMethod]
        public void TestLimiteDeCreditos()
        {
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // SIMULANDO UM ALUNO VÁLIDO
            var viewModel = new TurmaViewModel();
            viewModel.AlunoId = db.Alunos.FirstOrDefault().Id;
            viewModel.Aluno = db.Alunos.FirstOrDefault(x => x.Id == viewModel.AlunoId);

            // CHAMANDO OS MÉTODOS DE VERIFICAÇÃO DAS REGRAS DE NEGÓCIO

            var turmaController = new TurmaController();
            var httpContextMock = new Mock<HttpContext>();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                // ADICIONANDO AS MATÉRIAS
                { "selecionarMateria", new StringValues(new [] {"100"}) }
            });
            httpContextMock.Setup(c => c.Request.Form).Returns(formCollection);
            turmaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var resultado = turmaController.Inscrever(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Index", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.AlunoId, resultado.RouteValues["idAluno"]);
        }

        [TestMethod]
        public void TestPreRequisito()
        {
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // SIMULANDO UM ALUNO VÁLIDO
            var viewModel = new TurmaViewModel();
            viewModel.AlunoId = db.Alunos.FirstOrDefault().Id;
            viewModel.Aluno = db.Alunos.FirstOrDefault(x => x.Id == viewModel.AlunoId);

            // CHAMANDO OS MÉTODOS DE VERIFICAÇÃO DAS REGRAS DE NEGÓCIO

            var turmaController = new TurmaController();
            var httpContextMock = new Mock<HttpContext>();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                // ADICIONANDO AS MATÉRIAS
                { "selecionarMateria", new StringValues(new [] {"12"}) }
            });
            httpContextMock.Setup(c => c.Request.Form).Returns(formCollection);
            turmaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var resultado = turmaController.Inscrever(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Index", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.AlunoId, resultado.RouteValues["idAluno"]);
        }

        [TestMethod]
        public void TestJaAprovadoNaMateria()
        {
            var db = new EscolinhaBancoSimulado();
            db.PopularBancoSimulado();

            // SIMULANDO UM ALUNO VÁLIDO
            var viewModel = new TurmaViewModel();
            viewModel.AlunoId = db.Alunos.FirstOrDefault().Id;
            viewModel.Aluno = db.Alunos.FirstOrDefault(x => x.Id == viewModel.AlunoId);

            // CHAMANDO OS MÉTODOS DE VERIFICAÇÃO DAS REGRAS DE NEGÓCIO

            var turmaController = new TurmaController();
            var httpContextMock = new Mock<HttpContext>();
            var formCollection = new FormCollection(new Dictionary<string, StringValues>
            {
                // ADICIONANDO AS MATÉRIAS
                { "selecionarMateria", new StringValues(new [] {"1", "2"}) }
            });
            httpContextMock.Setup(c => c.Request.Form).Returns(formCollection);
            turmaController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };

            // Act
            var resultado = turmaController.Inscrever(viewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("Index", resultado.ActionName);
            Assert.AreEqual("Turma", resultado.ControllerName);
            Assert.AreEqual(viewModel.AlunoId, resultado.RouteValues["idAluno"]);
        }

    }
}

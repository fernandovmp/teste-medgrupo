using AppContatos.Aplicacao.CasosDeUso;
using AppContatos.Aplicacao.CasosDeUso.CriarContato;
using AppContatos.Aplicacao.Enums;
using AppContatos.Aplicacao.Excecoes;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Aplicacao.Resultado;
using AppContatos.Aplicacao.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AppContatos.Aplicacao.Testes.CasosDeUso.CriarContato
{
    public class CriarContatoTestes
    {
        [Theory]
        [MemberData(nameof(InputsValidos))]
        public async Task ExecutarAsync_ComInputValido_DeveSalvarOperacao(CriarContatoInput input)
        {
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            mockUnitOfWork
                .Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Theory]
        [MemberData(nameof(InputsValidos))]
        public async Task ExecutarAsync_ComInputValido_DeveRetornarSucesso(CriarContatoInput input)
        {
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Estado.Should().Be(ResultadoEstadoEnum.Sucesso);
        }

        [Theory]
        [MemberData(nameof(InputsValidos))]
        public async Task ExecutarAsync_ComInputValido_DeveRetornarOutputCoerente(CriarContatoInput input)
        {
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Valor.Should().NotBeNull();
            retorno.Valor!.Nome.Should().Be(input.Nome);
            retorno.Valor!.Sexo.Should().Be(input.Sexo);
        }

        [Theory]
        [MemberData(nameof(InputsInvalidos))]
        public async Task ExecutarAsync_ComInputInvalido_DeveRetornarFalha(CriarContatoInput input)
        {
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Estado.Should().Be(ResultadoEstadoEnum.Falha);
        }

        [Theory]
        [MemberData(nameof(InputsInvalidos))]
        public async Task ExecutarAsync_ComInputInvalido_DeveRetornarErroValidacao(CriarContatoInput input)
        {
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Excecao.Should().NotBeNull();
            retorno.Excecao.Should().BeOfType<ErroValidacaoExcecao>();
        }

        [Theory]
        [MemberData(nameof(InputsInvalidos))]
        public async Task ExecutarAsync_ComInputInvalido_NaoDeveSalvarOperacao(CriarContatoInput input)
        {
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            mockUnitOfWork
               .Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never());
        }

        [Fact]
        public async Task ExecutarAsync_QuandoLancaExcecao_DeveRetornarFalha()
        {
            var input = new CriarContatoInput
            {
                Nome = "Teste",
                DataNascimento = DateTime.Parse("10/10/2000"),
                Sexo = SexoEnum.Feminino
            };
            var excecao = new Exception();
            var mockLogger = new Mock<ILogger<CriarContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
                .Throws(excecao);
            var mockDateTime = ObterMockDateTime();
            var criarContato = new CriarContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object, mockDateTime.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Estado.Should().Be(ResultadoEstadoEnum.Falha);
        }

        private Mock<IDateTimeProvider> ObterMockDateTime()
        {
            var mock = new Mock<IDateTimeProvider>();
            mock.Setup(x => x.Now()).Returns(DateTime.Parse("17/06/2022"));
            return mock;
        }

        private static IEnumerable<object[]> InputsValidos()
        {
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("10/10/2000"),
                    Sexo = SexoEnum.Feminino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("10/10/2000"),
                    Sexo = SexoEnum.Masculino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "iManSDrRmAvIOJvHhBnLYwdwWQehwdQy",
                    DataNascimento = DateTime.Parse("10/10/2000"),
                    Sexo = SexoEnum.Feminino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("10/10/2000"),
                    Sexo = SexoEnum.Feminino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("17/06/2004"),
                    Sexo = SexoEnum.Feminino
                }
            };
        }

        private static IEnumerable<object[]> InputsInvalidos()
        {
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "",
                    DataNascimento = DateTime.Parse("10/10/2000"),
                    Sexo = SexoEnum.Feminino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "iManSDrRmAvIOJvHhBnLYwdwWQehwdQyA",
                    DataNascimento = DateTime.Parse("10/10/2000"),
                    Sexo = SexoEnum.Masculino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("10/10/2030"),
                    Sexo = SexoEnum.Feminino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("10/10/2010"),
                    Sexo = SexoEnum.Feminino
                }
            };
            yield return new[]
            {
                new CriarContatoInput
                {
                    Nome = "Teste",
                    DataNascimento = DateTime.Parse("18/06/2004"),
                    Sexo = SexoEnum.Feminino
                }
            };
        }
    }
}

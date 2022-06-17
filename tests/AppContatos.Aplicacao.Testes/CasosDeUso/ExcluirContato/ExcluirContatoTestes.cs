using AppContatos.Aplicacao.CasosDeUso;
using AppContatos.Aplicacao.CasosDeUso.ExcluirContato;
using AppContatos.Aplicacao.Consultas;
using AppContatos.Aplicacao.Entidades;
using AppContatos.Aplicacao.Repositorios;
using AppContatos.Aplicacao.Resultado;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace AppContatos.Aplicacao.Testes.CasosDeUso.ExcluirContato
{
    public class ExcluirContatoTestes
    {

        [Fact]
        public async Task ExecutarAsync_ComContato_DeveSalvarOperacao()
        {
            var input = new ExcluirContatoInput
            {
                Id = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ExcluirContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            mockContatoRepositorio
                .Setup(x => x.ObterPrimeiroAsync(It.IsAny<IConsultaEspecificacao<Contato>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Contato()));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));
            var criarContato = new ExcluirContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            mockUnitOfWork
                .Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async Task ExecutarAsync_ComContato_DeveChamarRemover()
        {
            var input = new ExcluirContatoInput
            {
                Id = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ExcluirContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            mockContatoRepositorio
                .Setup(x => x.ObterPrimeiroAsync(It.IsAny<IConsultaEspecificacao<Contato>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Contato()));
            mockContatoRepositorio
                .Setup(x => x.Remover(It.IsAny<Contato>()));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));
            var criarContato = new ExcluirContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            mockContatoRepositorio
                .Verify(x => x.Remover(It.IsAny<Contato>()), Times.Once());
        }

        [Fact]
        public async Task ExecutarAsync_ComContato_DeveRetornarSucesso()
        {
            var input = new ExcluirContatoInput
            {
                Id = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ExcluirContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            mockContatoRepositorio
                .Setup(x => x.ObterPrimeiroAsync(It.IsAny<IConsultaEspecificacao<Contato>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Contato()));
            mockContatoRepositorio
                .Setup(x => x.Remover(It.IsAny<Contato>()));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));
            var criarContato = new ExcluirContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Estado.Should().Be(ResultadoEstadoEnum.Sucesso);
        }

        [Fact]
        public async Task ExecutarAsync_SemContato_DeveRetornarFalha()
        {
            var input = new ExcluirContatoInput
            {
                Id = Guid.NewGuid()
            };
            var mockLogger = new Mock<ILogger<ExcluirContatoCasoDeUso>>();
            var mockContatoRepositorio = new Mock<IContatoRepositorio>();
            mockContatoRepositorio
                .Setup(x => x.ObterPrimeiroAsync(It.IsAny<IConsultaEspecificacao<Contato>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((Contato?) null));
            mockContatoRepositorio
                .Setup(x => x.Remover(It.IsAny<Contato>()));
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.ContatoRepositorio)
                .Returns(mockContatoRepositorio.Object);
            mockUnitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));
            var criarContato = new ExcluirContatoCasoDeUso(mockUnitOfWork.Object, mockLogger.Object);
            var retorno = await criarContato.ExecutarAsync(input, CancellationToken.None);
            retorno.Estado.Should().Be(ResultadoEstadoEnum.Falha);
        }
    }
}

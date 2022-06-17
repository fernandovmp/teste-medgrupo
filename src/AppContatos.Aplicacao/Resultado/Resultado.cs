using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppContatos.Aplicacao.Resultado
{
    public struct Resultado<T>
    {
        public Resultado(T valor)
        {
            Estado = ResultadoEstadoEnum.Sucesso;
            Valor = valor;
            Excecao = null;
        }

        public Resultado(Exception excecao)
        {
            Estado = ResultadoEstadoEnum.Falha;
            Valor = default;
            Excecao = excecao;
        }

        public ResultadoEstadoEnum Estado { get; }
        public T? Valor { get; }
        public Exception? Excecao { get; }
    }
}

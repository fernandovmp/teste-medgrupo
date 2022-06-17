namespace AppContatos.Aplicacao.Consultas
{
    public class ListaPaginada<T>
    {
        private ListaPaginada()
        {
        }

        public ListaPaginada(int pagina, int tamanho, int total, IEnumerable<T> lista)
        {
            Pagina = pagina;
            Tamanho = tamanho;
            QtdePaginas = (int)Math.Ceiling(total / (double)tamanho);
            Lista = lista;
        }

        public int Pagina { get; private set; }
        public int Tamanho { get; private set; }
        public int QtdePaginas { get; private set; }
        public IEnumerable<T> Lista { get; private set; }

        public ListaPaginada<TOutput> Projetar<TOutput>(Func<T, TOutput> projecao)
        {
            IEnumerable<TOutput> lista = Lista.Select(projecao);
            return new ListaPaginada<TOutput>
            {
                Pagina = this.Pagina,
                Tamanho = this.Tamanho,
                QtdePaginas = this.QtdePaginas,
                Lista = lista
            };
        }
    }
}

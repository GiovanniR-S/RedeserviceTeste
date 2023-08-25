namespace GiovanniTeste.Models.Entidades
{
    public class Arquivo
    {
        public Arquivo(string pasta, string nomeArquivo, string extensao)
        {
            Pasta = pasta;
            NomeArquivo = nomeArquivo;
            Extensao = extensao;
        }

        public string Pasta { get; set; }

        public string NomeArquivo { get; set; }

        public string Extensao { get; set; }
    }
}

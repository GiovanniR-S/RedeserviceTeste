namespace GiovanniTeste.Models.Entidades
{
    public class Logradouro
    {
        public Logradouro(string bairro, string cep, string cidade, string complemento2, string end, string uf)
        {
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Complemento2 = complemento2;
            End = end;
            Uf = uf;
        }

        public string Bairro { get ; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }

        public string Complemento2 { get; set; }

        public string End { get; set; }

        public string Uf { get; set; }
    }
}

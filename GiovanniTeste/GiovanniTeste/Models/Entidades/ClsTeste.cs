namespace GiovanniTeste.Models.Entidades
{
    public class ClsTeste
    {
        public ClsTeste(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public int Codigo { get; set; }

        public string Descricao { get; set; }
    }
}

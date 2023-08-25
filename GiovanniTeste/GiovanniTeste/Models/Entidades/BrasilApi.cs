namespace GiovanniTeste.Models.Entidades
{
    public class BrasilApi
    {
        public BrasilApi(string ispb, string name, string code, string fullName)
        {
            Ispb = ispb;
            Name = name;
            Code = code;
            FullName = fullName;
        }

        public string Ispb { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string FullName { get; set; }

    }
}

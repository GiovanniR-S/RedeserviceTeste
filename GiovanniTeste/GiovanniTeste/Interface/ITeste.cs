using GiovanniTeste.Models.Entidades;

namespace GiovanniTeste.Interface
{
    public interface ITeste
    {
        Task GravarNumeros(List<int> numero, Arquivo arquivo);

        Task<string> GravarJson(List<ClsTeste> clsTestes, Arquivo arquivo);

        Task<IEnumerable<BrasilApi>> GetApis(string caminho);

        Task DownloadImage(Arquivo arquivo);

        Task<string> GetImage(Arquivo arquivo);
    }
}

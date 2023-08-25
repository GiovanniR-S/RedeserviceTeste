using GiovanniTeste.Interface;
using GiovanniTeste.Models.Entidades;
using System.IO;

namespace GiovanniTeste.Service
{
    public class ServiceTeste 
    {
        private readonly ITeste teste;

        public ServiceTeste(ITeste teste)
        {
            this.teste = teste;
        }
        public async Task GravarNumeros(List<int> numeros, Arquivo arquivo)
        {
            await teste.GravarNumeros(numeros, arquivo);
        }

        public async Task<string> GravarJson(List<ClsTeste> clsTestes, Arquivo arquivo)
        {
            return await teste.GravarJson(clsTestes, arquivo);
        }

        public async Task<IEnumerable<BrasilApi>> GetApis(string caminho)
        {
            return await teste.GetApis(caminho);
        }

        public async Task DownloadImage(Arquivo arquivo)
        {
             await teste.DownloadImage(arquivo);
        }

        public async Task<string> GetImage(Arquivo arquivo)
        {
            return await teste.GetImage(arquivo);
        }
    }
}

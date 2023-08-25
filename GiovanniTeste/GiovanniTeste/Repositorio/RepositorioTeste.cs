using GiovanniTeste.Interface;
using GiovanniTeste.Models.Entidades;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace GiovanniTeste.Repositorio
{
    public class RepositorioTeste : ITeste
    {
        string caminho = Environment.CurrentDirectory;

        public async Task DownloadImage(Arquivo arquivo)
        {
            try
            {
                caminho = caminho + @"\" + arquivo.Pasta + @"\" + arquivo.NomeArquivo + @"." + arquivo.Extensao;
                string url = @"https://redeservice.com.br/wpcontent/uploads/2020/07/redeservice-logo.png";

                WebClient client = new WebClient();
                Stream stream = client.OpenRead(url);
                Bitmap bitmap;
                bitmap = new Bitmap(stream);

                if (bitmap != null)
                {
                    bitmap.Save(caminho, ImageFormat.Png);
                }

                stream.Flush();
                stream.Close();
                client.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<BrasilApi>> GetApis(string caminho)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(caminho);
            var result = client.GetAsync(client.BaseAddress).Result;
            IEnumerable<BrasilApi> obj = new List<BrasilApi>();

            if (result.IsSuccessStatusCode)
            {
                var jsonResponse = Task.Run(async () => await result.Content.ReadAsStringAsync()).Result;
                obj = JsonConvert.DeserializeObject<IEnumerable<BrasilApi>>(jsonResponse);
            }

            return obj;
        }

        public async Task<string> GetImage(Arquivo arquivo)
        {
            string base64 = string.Empty;

            caminho = caminho + @"\" + arquivo.Pasta + @"\" + arquivo.NomeArquivo + @"." + arquivo.Extensao;
            var file = new FileInfo(caminho);

            if (file.Exists)
            {
                Byte[] bytes = File.ReadAllBytes(caminho);

                base64 = Convert.ToBase64String(bytes);
            }

            return base64;
        }

        public async Task<string> GravarJson(List<ClsTeste> clsTestes, Arquivo arquivo)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(clsTestes);

            caminho = caminho + @"\" + arquivo.Pasta + @"\" + arquivo.NomeArquivo + @"." + arquivo.Extensao;            

            var file = new FileInfo(caminho);

            if (file.Exists)
            {
                file.Delete();        
            }
            File.WriteAllText(caminho, jsonString);
            return jsonString;
        }

        /// <summary>
        /// Gravar numeros em arquivo txt.
        /// </summary>
        /// <param name="numeros">parametro.</param>
        /// <returns>Sem retorno</returns>        
        public async Task GravarNumeros(List<int> numeros, Arquivo arquivo)
        {
            try
            {
                caminho = caminho + @"\" + arquivo.Pasta + @"\" + arquivo.NomeArquivo + @"." + arquivo.Extensao;

                var file = new FileInfo(caminho);

                if (file.Exists)
                {
                    file.Delete();                    
                }
                StreamWriter numero = new StreamWriter(caminho, true);

                foreach (var item in numeros)
                {
                    numero.WriteLine(item);
                }
                numero.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {}
        }
    }
}

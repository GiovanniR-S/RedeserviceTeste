using GiovanniTeste.Interface;
using GiovanniTeste.Models;
using GiovanniTeste.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace GiovanniTeste.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<int> ints = new List<int>();
        private static List<ClsTeste> ListaClsTeste = new List<ClsTeste>();

        private readonly ITeste teste;
        private IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, ITeste teste, IWebHostEnvironment _hostEnvironment)
        {
            _logger = logger;     
            this.teste = teste;
            this._hostEnvironment = _hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ListaItens()
        {
            List<ClsTeste>? lista = null;

            try
            {
                Arquivo arquivo = new("Arquivos", "data", "json");

                for (int i = 1; i < 101; i++)
                {
                    ClsTeste clsTeste = new(i, DateTime.Now.ToString());
                    ListaClsTeste.Add(clsTeste);
                }

                var retorno = await teste.GravarJson(ListaClsTeste, arquivo);

                lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ClsTeste>>(retorno);

            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }            

            return View("ListaItens", lista);
        }

        public async Task<IActionResult> BancoBrasileiros()
        {
            IEnumerable<BrasilApi>? lista = null;
            try
            {
                lista = await teste.GetApis("https://brasilapi.com.br/api/banks/v1");
                
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View(lista);

        }
        
        public IActionResult Imagens()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BaixarImagemUrl()
        {
            try
            {
                Arquivo arquivo = new( "Arquivos" , "redeservice-logo" , "png");
                await teste.DownloadImage(arquivo);                
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View("Imagens");

        }

        [HttpGet]
        public async Task<IActionResult> GetImagem()
        {
            try
            {
                Arquivo arquivo = new("Arquivos", "redeservice-logo", "png");
                var res = await teste.GetImage(arquivo);
                string imagemDadosURL = string.Format("data:image/png;base64,{0}", res);
                ViewBag.Image = imagemDadosURL;
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }
            return View("Imagens");

        }

        public IActionResult Correio()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConsutaCep(IFormCollection collection)
        {
            try
            {
                string cep = collection["cep"].ToString();
                var ws = new ServiceReference1.AtendeClienteClient();
                var resposta = await ws.consultaCEPAsync(cep);               
                return View("Correio", resposta);
            }
            catch (Exception ex)
            {
                ViewBag.ErroCorreio = ex.Message;
                return View("Correio");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> GravarNumerosTxt()
        {
            try
            {
                if (ints != null)
                {
                    Arquivo arquivo = new("Arquivos", "numeros_ordenar", "txt");
                    await teste.GravarNumeros(ints, arquivo);
                    ViewBag.Informacao = "Arquivo gravado com sucesso.";
                }
            }
            catch (Exception ex)
            {

                ViewBag.Erro = ex.Message;
            }            
            
            return View("Index");
        }

        [HttpPost]
        public IActionResult FormularioNumeros(IFormCollection collection)
        {
            try
            {
                string num = collection["numero"].ToString();

                if (!string.IsNullOrEmpty(num))
                {
                    ints.Add(Convert.ToInt32(num));
                    ints = ints.OrderBy(x => x).ToList();
                    ViewBag.Numeros = ints;

                }
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }
            
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
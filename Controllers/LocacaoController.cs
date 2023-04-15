using CarRentalCompany.Models;
using CarRentalCompany.Models.Enums;
using CarRentalCompany.Models.ViewModels;
using CarRentalCompany.Services;
using CarRentalCompany.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentalCompany.Controllers
{
    public class LocacaoController : Controller
    {
        private readonly LocacaoService _locacaoService;

        public LocacaoController(LocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var locacaos = _locacaoService.FindAll();

                return View(locacaos);
            }
            catch (UserException e)
            {
                throw new UserException(e.Message);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" });
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var locacao = _locacaoService.FindById(id);

                return View(locacao);
            }
            catch (UserException e)
            {
                throw new UserException(e.Message);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" });
            }
        }

        [HttpGet]
        public IActionResult Reserve(int id)
        {
            try
            {
                DateTime minDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 00);

                ViewData["veiculoId"] = id;
                ViewData["minDate"] = minDate.ToString("s");

                return View();
            }
            catch (UserException e)
            {
                throw new UserException(e.Message);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" });
            }
        }

        [HttpPost]
        public IActionResult Simulate(Locacao locacao, int veiculoId)
        {
            try
            {
                var veiculo = _locacaoService.FindVeiculoById(veiculoId);
                var cliente = _locacaoService.FindClienteByEmail(locacao.Cliente.Email);

                locacao.Veiculo = veiculo;
                locacao.Cliente = cliente;

                var tempoAluguelDias = _locacaoService.CalculateRentalTime(locacao);
                var taxa = new TaxaFactory(locacao, _locacaoService).Make().Taxa();

                locacao.Valor = taxa;
                locacao.TempoAluguelDias = tempoAluguelDias;

                return View(locacao);
            }
            catch (Exception e)
            {
                if (e is UserException)
                {
                    TempData["ErrorMessage"] = e.Message;
                } 
                else
                {
                    TempData["ErrorMessage"] = "Erro de servidor - " + e.Message;
                }

                return RedirectToAction(nameof(Reserve), new { id = veiculoId });
            }
        }

        [HttpPost]
        public IActionResult Create(Locacao locacao)
        {
            try
            {
                _locacaoService.Insert(locacao);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                if (e is UserException)
                {
                    TempData["ErrorMessage"] = e.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Erro de servidor - " + e.Message;
                }

                return RedirectToAction("Dash", "Veiculo");
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}

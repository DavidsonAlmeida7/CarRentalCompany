using CarRentalCompany.Data;
using CarRentalCompany.Models;
using CarRentalCompany.Models.Enums;
using CarRentalCompany.Models.ViewModels;
using CarRentalCompany.Services;
using CarRentalCompany.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentalCompany.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly VeiculoService _veiculoService;

        public VeiculoController(VeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var veiculos = _veiculoService.FindAll();

                return View(veiculos);
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
        public IActionResult Dash()
        {
            try
            {
                var veiculos = _veiculoService.FindAll();

                return View(veiculos);
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
        public IActionResult Create()
        {
            try
            {
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(Veiculo veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _veiculoService.Insert(veiculo);

                    return RedirectToAction(nameof(Index));
                }

                return View(veiculo);
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
                var veiculo = _veiculoService.FindById(id);

                return View(veiculo);
            }
            catch (UserException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" });
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var veiculo = _veiculoService.FindById(id);

                return View(veiculo);
            }
            catch (UserException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Veiculo veiculo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _veiculoService.Update(veiculo);

                    return RedirectToAction(nameof(Index));
                }

                return View(veiculo);
            }
            catch (UserException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" + e.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            try
            {
                _veiculoService.Remove(id);

                return RedirectToAction(nameof(Index));
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

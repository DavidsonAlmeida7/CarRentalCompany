using CarRentalCompany.Models;
using CarRentalCompany.Models.ViewModels;
using CarRentalCompany.Services;
using CarRentalCompany.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentalCompany.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService cliente)
        {
            _clienteService = cliente;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var clientes = _clienteService.FindAll();

                return View(clientes);
            } catch (UserException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de servidor" });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteService.Insert(cliente);

                    return RedirectToAction(nameof(Index));
                }

                return View(cliente);
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
                var cliente = _clienteService.FindById(id);

                return View(cliente);
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
        public IActionResult Edit(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteService.Update(cliente);

                    TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";

                    return RedirectToAction(nameof(Index));
                }

                return View(cliente);
            }
            catch (UserException e)
            {
                TempData["ErrorMessage"] = e.Message;

                return RedirectToAction();
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Erro de servidor!";

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var cliente = _clienteService.FindById(id);

                return View(cliente);
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
        public IActionResult Delete(int id)
        {
            try
            {
                _clienteService.Remove(11);

                return RedirectToAction(nameof(Index));
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

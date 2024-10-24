using AppLayoutAspCore.Libraries.Filtro;
using AppLayoutAspCore.Models;
using AppLayoutAspCore.Repositories.Contract;
using AppLoginAspCore.Libraries.Filtro;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace AppLayoutAspCore.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ClienteController : Controller
    {
        private IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View(_clienteRepository.ObterTodosClientes());
        }
        [HttpPost]
        public IActionResult Index(int draw, int start, int length, string search)
        {
            var clientes = _clienteRepository.ObterTodosClientes();
            return Json(new { data = clientes });
        }
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar(Cliente cliente)
        {
            _clienteRepository.Cadastrar(cliente);
            return View();
        }
      
        //[ValidateHttpReferer]
        public IActionResult Ativar(int id)
        {
            _clienteRepository.Ativar(id);
            return RedirectToAction(nameof(Index));
        }       
      //  [ValidateHttpReferer]
        public IActionResult Desativar(int id)
        {
            _clienteRepository.Desativar(id);
            return RedirectToAction(nameof(Index));
        } 
        public IActionResult Detalhes(int id)
        {   
            return View(_clienteRepository.ObterCliente(id));
        }
        [HttpPost]
        public IActionResult Detalhes(Cliente cliente)
        {
            return View();
        }
    }
}


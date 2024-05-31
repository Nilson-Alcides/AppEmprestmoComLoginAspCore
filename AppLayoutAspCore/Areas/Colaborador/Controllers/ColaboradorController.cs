
using AppLayoutAspCore.Libraries.Filtro;
using AppLayoutAspCore.Models.Constants;
using AppLayoutAspCore.Repositories.Contract;
using AppLayoutAspCore.Repositories.Contracts;
using AppLayoutAspCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AppLayoutAspCore.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
   [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IColaboradorRepository _colaboradorRepository;

        public ColaboradorController(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }
      //  [ValidateHttpReferer]
        public IActionResult Index()
        {
            return View(_colaboradorRepository.ObterTodosColaboradores());
        }
        [HttpGet]
      //  [ValidateHttpReferer]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Models.Colaborador colaborador)
         {
            colaborador.Tipo = ColaboradorTipoConstant.Comum;
            if (ModelState.IsValid) {
                _colaboradorRepository.Cadastrar(colaborador);
                TempData["MSG_S"] = "Registro salvo com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
     //   [ValidateHttpReferer]
        public IActionResult Atualizar(int id)
        {
            Models.Colaborador colaborador = _colaboradorRepository.ObterColaborador(id);
            return View(colaborador);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Colaborador colaborador)
        {           
            if (ModelState.IsValid)
            {
                _colaboradorRepository.Atualizar(colaborador);

                TempData["MSG_S"] = "Registro salvo com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    //    [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _colaboradorRepository.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

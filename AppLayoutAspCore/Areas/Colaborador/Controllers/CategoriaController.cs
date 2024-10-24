using Microsoft.AspNetCore.Mvc;
using AppLayoutAspCore.Repositories.Contracts;
using AppLayoutAspCore.Models;

namespace AppLayoutAspCore.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public IActionResult Index(int? pagina, string pesquisa)
        {
            return View(_categoriaRepository.ObterTodasCategorias(pagina, pesquisa));
        }
        public IActionResult CadCategoria()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadCategoria(Categoria categoria)
        {
            _categoriaRepository.Cadastrar(categoria);
            return RedirectToAction(nameof(Index));
        }
    }
}

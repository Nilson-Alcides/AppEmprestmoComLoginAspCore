using AppLayoutAspCore.Libraries.CarrinhoCompra;
using AppLayoutAspCore.Libraries.Login;
using AppLayoutAspCore.Models;
using AppLayoutAspCore.Repositories.Contract;
using AppLayoutAspCore.Repository;
using AppLayoutAspCore.Repository.Contrato;
using AppLoginAspCore.Libraries.Filtro;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppLayoutAspCore.Controllers
{
    public class HomeController : Controller
    {
        // Injeção de dependencia
        private IClienteRepository _clienteRepository;
        private LoginCliente _loginCliente;

        private IItemRepository _itemRepository;
        private IEmprestimoRepository _emprestimoRepository;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        private ILivroRepository _livroRepository;

        public HomeController(IClienteRepository clienteRepository, LoginCliente loginCliente, ILivroRepository livroRepository, CookieCarrinhoCompra cookieCarrinhoCompra,
                              IEmprestimoRepository emprestimoRepository, IItemRepository itemRepository)
        {
            _clienteRepository = clienteRepository;
            _loginCliente = loginCliente;
            _livroRepository = livroRepository;
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _emprestimoRepository = emprestimoRepository;
            _itemRepository = itemRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] Cliente cliente)
        {
            Cliente clienteDB = _clienteRepository.Login(cliente.Email, cliente.Senha);

            if (clienteDB.Email != null && clienteDB.Senha != null)
            {
                _loginCliente.Login(clienteDB);
                return new RedirectResult(Url.Action(nameof(PainelCliente)));
            }
            else
            {
                //Erro na sessão
                ViewData["MSG_E"] = "Usuário não localizado, por favor verifique e-mail e senha digitado";
                return View();
            }
        }

       [ClienteAutorizacao]
        public IActionResult PainelCliente()
        {
            ViewBag.Nome = _loginCliente.GetCliente().Nome;
            ViewBag.CPF = _loginCliente.GetCliente().CPF;
            ViewBag.Email = _loginCliente.GetCliente().Email;
            //return new ContentResult() { Content = "Este é o Painel do Cliente!" };
            return View();
        }
       [ClienteAutorizacao]
        public IActionResult LogoutCliente()
        {
            _loginCliente.Logout();
            return RedirectToAction(nameof(Index));
        }
        //Exibe os livros na Index 
        public IActionResult Index()
        {
            return View(_livroRepository.ObterTodosLivros());
        }
        //Item ID = ID Produto
        public IActionResult AdicionarItem(int id)
        {
            Livro produto = _livroRepository.ObterLivros(id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Livro()
                {
                    codLivro = id,
                    quantidade = 1,
                    imagemLivro = produto.imagemLivro,
                    nomeLivro = produto.nomeLivro
                };
                _cookieCarrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Carrinho));
            }

        }

        //Carrnho de compra
        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }
        //Remover itens do carrinho
        public IActionResult RemoverItem(int id)
        {
            _cookieCarrinhoCompra.Remover(new Livro() { codLivro = id });
            return RedirectToAction(nameof(Carrinho));
        }

        DateTime data;
        [ClienteAutorizacao]
        public IActionResult SalvarCarrinho(Emprestimo emprestimo)
        {
            List<Livro> carrinho = _cookieCarrinhoCompra.Consultar();

            Emprestimo mdE = new Emprestimo();
            Item mdI = new Item();

            data = DateTime.Now.ToLocalTime();

            mdE.dtEmpre = data.ToString("dd/MM/yyyy");
            mdE.dtDev = data.AddDays(7).ToString();
            mdE.codUsu = "1";
            _emprestimoRepository.Cadastrar(mdE);

            _emprestimoRepository.buscaIdEmp(emprestimo);

            for (int i = 0; i < carrinho.Count; i++)
            {

                mdI.codEmp = Convert.ToInt32(emprestimo.codEmp);
                mdI.codLivro = Convert.ToString(carrinho[i].codLivro);

                _itemRepository.Cadastrar(mdI);
            }

            _cookieCarrinhoCompra.RemoverTodos();
            return RedirectToAction("confEmp");
        }
        public IActionResult confEmp()
        {
            return View();
        }
        public IActionResult Cadastrar()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Cliente cliente)
        {
            var CPFExit = _clienteRepository.BuscaCpfCliente(cliente.CPF).CPF;
            var EmailExit = _clienteRepository.BuscaEmailCliente(cliente.Email).Email;

            if (!string.IsNullOrWhiteSpace(CPFExit))
            {
                //CPF Cadastrado
                ViewData["MSG_CPF"] = "CPF já cadastrado, por favor verifique os dados digitado";
                return View();

            }
            else if (!string.IsNullOrWhiteSpace(EmailExit))
            {
                //Email Cadastrado
                ViewData["MSG_Email"] = "E-mail já cadastrado, por favor verifique os dados digitado";
                return View();
            }
            else if (ModelState.IsValid)
            {

                _clienteRepository.Cadastrar(cliente);
                return RedirectToAction(nameof(Login));
            }
            return View();
        }
        [ClienteAutorizacao]
        public IActionResult Detalhes(int id)
        {
            return View(_clienteRepository.ObterCliente(id));
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}

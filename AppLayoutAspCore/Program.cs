using AppLayoutAspCore.Libraries.GerenciaArquivos;
using AppLayoutAspCore.Libraries.Login;
using AppLayoutAspCore.Libraries.Middleware;
using AppLayoutAspCore.Repositories.Contract;
using AppLayoutAspCore.Repositories.Contracts;
using AppLayoutAspCore.Repository;
using AppLayoutAspCore.Repository.Contrato;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Adicionado para manipular a Sessão
builder.Services.AddHttpContextAccessor();

//Adicionar a Interface como um serviço 
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

builder.Services.AddScoped<AppLayoutAspCore.Libraries.Sessao.Sessao>();
builder.Services.AddScoped<LoginCliente>();
builder.Services.AddScoped<LoginColaborador>();

//Add Gerenciador Arquivo como serviços
builder.Services.AddScoped<GerenciadorArquivo>();
builder.Services.AddScoped<AppLayoutAspCore.Libraries.Cookie.Cookie>();
builder.Services.AddScoped<AppLayoutAspCore.Libraries.CarrinhoCompra.CookieCarrinhoCompra>();



// Corrigir problema com TEMPDATA
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Definir um tempo para duração. 
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    // Mostrar para o navegador que o cookie e essencial   
    options.Cookie.IsEssential = true;
});
builder.Services.AddMvc().AddSessionStateTempDataProvider();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseMiddleware<ValidateAntiForgeryTokenMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

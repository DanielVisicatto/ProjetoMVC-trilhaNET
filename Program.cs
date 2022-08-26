using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#region PassoAPasso
/*
* o comando para gerar um novo projeto seria      >> dotnet new mvc (gera o projeto todo)
* o comando para executar o projeto seria         >> dotnet watch run (sem uma IDE)
* configurando o EntityFramework, comando         >> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
* instalando o Design                             >> dotnet add package Microsoft.EntityFrameworkCore.Design

* Ferraneta do EntityFramework já instalada a nível de máquina...(não precisa instalar de novo)
 
1 - Criação da nossa Model Contato.

2 - Criação da pasta Context

3 - Criação da AgendaContext : DbContext que tem aquele construtor verboso...junto com nossa propriedade DbSet

4 - Colocamos a nossa Connection String lá no appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "ConexaoPadrao": "Server=localhost\\sqlexpress; Initial Catalog=AgendaMvc; Integrated Security=True" 
  }
}

5 - Colocamos nossa variável ConexaoPadrao aqui no Program.cs para efetivar a conexão com o banco de dados
ali em:      //add services to the container

Ficou bem diferente a forma de conexão:
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

6 - Criamos nossas migrations (porque nosso banco de dados ainda não tem nada)
comando                 >>dotnet ef migrations add NomeDaMigration (AdicionaTabelaContato)
comando                 >>dotnet ef database update
aqui já conseguimos ver o reflexo no nosso SSMS, nosso banco foi criado e a tabela também.

7 - Agora vamos escrever um código HTML para listarmos os clientes. Dentro de Views, criamos uma pasta Contato
com nossa view index.cshtml onde vai ficar o código HTML

8 - Configuramos o nosso método na controller, dentro de Controllers, criamos o ContatoController.cs que vai 
trzer as informações do banco de dados do contato no seu Index()... como? atraves do EntityFramework

fazendo nossa classe privada:
private readonly AgendaContext _context;

e nosso construtor:
public ContatoController(AgendaContext context) fazendo nossa injeção de dependencias
{
    _context = context;
}
depois colocamos o uso dela no Index();
var contatos = _context.Contatos.ToList(); recebendo contatos como lista
            return View(contatos);         passando contatos como parametros para a View()

9 - Agora vamos criar a página de criação de contato. O procedimento é o mesmo, temos de criar um novo método na
controller e uma nóva página Index.

10 - Feito isso precisamos implementar a lógica de criar um contato, para isso vamo criar mais um método, só que ele vai receber um
novo contato por parâmetro.
[HttpPost]
        public IActionResult Criar(Contato contato)
        {
            if (ModelState.IsValid) //verificando se o modelo é válido
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
11 - Agora vamos criar nossa primeira página. // Esta aula estava fora de ordem... Esse passo vem entre 6 e 7.

12 - Feito isso vamos contruir nossa página de edição. Que é muito semelhante a nossa página de criação. 
a principal mudanca ocorre na linha  <form asp-action="Editar"> que agora vai chamar outro método na controller
onde vamos criar dois métodos, um de post eoutro que recebe o contato que estamos editando.
assim as mudanças já serão refletidas no nosso banco de dados.

13 - Enfim, vamos criar nossa Página Detalhes, esta não vai editar os detalhes mas tera um link redirecionando
para isso.
Dedepois de montar a pagina com as exibições desejadas, temos que voltar no nosso controller e colocar o código
dessa página de detalhes.

14 - Finalmente pagina de deletar. O Deletar é uma operação irreversível, então vamos tomar o cuidado de 
perguntar ao usuário se ele deseja realmente deletar o contato da nossa aplicação. Esse aviso poderia ser um 
poup-up, mas vamos fazer através de uma página de deleção.
Depois de fazer o HTML, temos que ir lá no nosso controller e fazer as ações dela. GET e POST.
temos assim o nosso CRUD completo.

15 - Agora vamos colocar nosso menu lá na página home para poder acessar nossa pag de contatos. A página inicial
do nosso ASPNet, fica dentro de Views/Shared no arquivo _Layout.cshmtl


*/
#endregion
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

* Ferraneta do EntityFramework j� instalada a n�vel de m�quina...(n�o precisa instalar de novo)
 
1 - Cria��o da nossa Model Contato.

2 - Cria��o da pasta Context

3 - Cria��o da AgendaContext : DbContext que tem aquele construtor verboso...junto com nossa propriedade DbSet

4 - Colocamos a nossa Connection String l� no appsettings.Development.json
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

5 - Colocamos nossa vari�vel ConexaoPadrao aqui no Program.cs para efetivar a conex�o com o banco de dados
ali em:      //add services to the container

Ficou bem diferente a forma de conex�o:
builder.Services.AddDbContext<AgendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

6 - Criamos nossas migrations (porque nosso banco de dados ainda n�o tem nada)
comando                 >>dotnet ef migrations add NomeDaMigration (AdicionaTabelaContato)
comando                 >>dotnet ef database update
aqui j� conseguimos ver o reflexo no nosso SSMS, nosso banco foi criado e a tabela tamb�m.

7 - Agora vamos escrever um c�digo HTML para listarmos os clientes. Dentro de Views, criamos uma pasta Contato
com nossa view index.cshtml onde vai ficar o c�digo HTML

8 - Configuramos o nosso m�todo na controller, dentro de Controllers, criamos o ContatoController.cs que vai 
trzer as informa��es do banco de dados do contato no seu Index()... como? atraves do EntityFramework

fazendo nossa classe privada:
private readonly AgendaContext _context;

e nosso construtor:
public ContatoController(AgendaContext context) fazendo nossa inje��o de dependencias
{
    _context = context;
}
depois colocamos o uso dela no Index();
var contatos = _context.Contatos.ToList(); recebendo contatos como lista
            return View(contatos);         passando contatos como parametros para a View()

9 - Agora vamos criar a p�gina de cria��o de contato. O procedimento � o mesmo, temos de criar um novo m�todo na
controller e uma n�va p�gina Index.

10 - Feito isso precisamos implementar a l�gica de criar um contato, para isso vamo criar mais um m�todo, s� que ele vai receber um
novo contato por par�metro.
[HttpPost]
        public IActionResult Criar(Contato contato)
        {
            if (ModelState.IsValid) //verificando se o modelo � v�lido
            {
                _context.Contatos.Add(contato);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
11 - Agora vamos criar nossa primeira p�gina. // Esta aula estava fora de ordem... Esse passo vem entre 6 e 7.

12 - Feito isso vamos contruir nossa p�gina de edi��o. Que � muito semelhante a nossa p�gina de cria��o. 
a principal mudanca ocorre na linha  <form asp-action="Editar"> que agora vai chamar outro m�todo na controller
onde vamos criar dois m�todos, um de post eoutro que recebe o contato que estamos editando.
assim as mudan�as j� ser�o refletidas no nosso banco de dados.

13 - Enfim, vamos criar nossa P�gina Detalhes, esta n�o vai editar os detalhes mas tera um link redirecionando
para isso.
Dedepois de montar a pagina com as exibi��es desejadas, temos que voltar no nosso controller e colocar o c�digo
dessa p�gina de detalhes.

14 - Finalmente pagina de deletar. O Deletar � uma opera��o irrevers�vel, ent�o vamos tomar o cuidado de 
perguntar ao usu�rio se ele deseja realmente deletar o contato da nossa aplica��o. Esse aviso poderia ser um 
poup-up, mas vamos fazer atrav�s de uma p�gina de dele��o.
Depois de fazer o HTML, temos que ir l� no nosso controller e fazer as a��es dela. GET e POST.
temos assim o nosso CRUD completo.

15 - Agora vamos colocar nosso menu l� na p�gina home para poder acessar nossa pag de contatos. A p�gina inicial
do nosso ASPNet, fica dentro de Views/Shared no arquivo _Layout.cshmtl


*/
#endregion
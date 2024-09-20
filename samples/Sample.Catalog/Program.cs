using GraphQL;
using GraphQL.Server.Sample.Catalog.Services;
using GraphQL.Server.Sample.Catalog.Schema;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICatalogService, CatalogService>();
builder.Services.AddGraphQL(b => b
    .AddAutoSchema<Query>(s => s
        .WithMutation<Mutation>()
        .WithSubscription<Subscription>())
    .AddSystemTextJson());

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseWebSockets();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();

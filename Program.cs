

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".Session";
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession();

app.MapGet("/", () => "Hello World!");
app.MapGet("/set", (HttpContext ctx) => {
    ctx.Session.SetString("key", "value");
    return "set";
});
app.MapGet("/html", () => Results.Text("<h1>Header!</h1>", "text/html"));
app.MapGet("/get", (HttpContext ctx) => {
    var val = ctx.Session.GetString("key");
    return $"get {val}";
});
app.Run();

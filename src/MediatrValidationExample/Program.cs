using MediatrExample.ApplicationCore;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using MediatrExample.WebApi;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddWebApi();
builder.Services.AddApplicationCore();
builder.Services.AddPersistence(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddSecurity(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await SeedData();

app.Run();


async Task SeedData()
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MyAppDbContext>();

    context.Database.EnsureCreated();

    if (!context.Products.Any())
    {
        context.Products.AddRange(new List<Product>
        {
            new Product
            {
                Description = "Product 01",
                Price = 16000
            },
            new Product
            {
                Description = "Product 02",
                Price = 52200
            }
        });

        await context.SaveChangesAsync();
    }
}
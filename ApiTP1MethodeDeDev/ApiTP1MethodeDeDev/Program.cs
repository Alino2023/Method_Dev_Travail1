using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain.Borrower;
using Domain.Loan;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("AppDb"));

builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<IBorrowerRepository, InMemoryBorrowerRepository>();

builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanRepository, InMemoryLoanRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = "";
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbcontext.Database.EnsureCreated();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

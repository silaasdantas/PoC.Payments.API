using PoC.Payments.API.Features.Payment;
using System.Reflection;

namespace PoC.Payments.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adicione todos os serviços necessários aqui
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddScoped<PaymentService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseStaticFiles();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        //app.UseAuthorization();

        app.Run();

    }
}
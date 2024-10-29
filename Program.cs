using CalculationTechTest.Services.MathService;
using CalculationTechTest.Services.MathService.Interface;
using CalculationTechTest.Services.Parser;
using CalculationTechTest.Services.Parser.Json;
using CalculationTechTest.Services.Parser.Xml;

namespace CalculationTechTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTransient<XmlParser>();
            builder.Services.AddTransient<JsonParser>();
            builder.Services.AddTransient<ParserHandler, XmlParser>();
            builder.Services.AddTransient<ParserHandler, JsonParser>();

            builder.Services.AddScoped<ICalculateServcie,CalculatorService>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
           
        }
    }
}
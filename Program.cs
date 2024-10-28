using CalculationTechTest.Services.MathService;
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
            string xmlData =
                @"<?ml version=""1.0"" encoding=""UTF-8""?>
                    <Maths>
                      <Operation ID=""Plus"">
                        <Operation ID=""Plus"">
                          <Value>4</Value>
                          <Value>5</Value>
                          <Operation ID=""SquareRoot"">
                            <Operation ID=""SquareRoot"">
                            <Operation ID=""SquareRoot"">
                            <Operation ID=""Divide"">
                            <Value>16</Value>
<Value>160</Value>
                          </Operation>
                          </Operation>
                          </Operation>
                          </Operation>
                        </Operation>
                        <Value>2</Value>
                        <Value>3</Value>
                      </Operation>
                    </Maths>";
            string jsonData = @"{
              ""Maths"": [
                
                    {
                      ""@ID"": ""Plus"",
                      ""Value"": [
                        ""2"",
                        ""3""
                      ]
                    },
                    {
                      ""@ID"": ""Multiplication"",
                      ""Value"": [
                        ""4"",
                        ""5""
                      ]
                    }
                  ]
                }";
            //            Queue<Object> expressionQ = null;
            //            XmlParser parser = new XmlParser();
            //            JsonParser jsonParser = new JsonParser();
            //            parser.SetNext(jsonParser);

            //            expressionQ = parser.ExtractExpressionQueueFromSerialized(xmlData);

            //            if (expressionQ != null)
            //            {
            //                var result = Calculator.Calculate(expressionQ);
            //                Console.WriteLine(string.Join(", ", result));
            //            }
            //            else
            //            {
            //                Console.WriteLine("Not Supported");
            //            }
        }
    }
}
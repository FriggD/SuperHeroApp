using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SuperheroApp.Core.DTOs;
using System.Reflection;

namespace SuperheroApp.API.Swagger
{
    /// <summary>
    /// Filtro personalizado para adicionar exemplos à documentação do Swagger
    /// </summary>
    public class SwaggerExampleFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.Name == "GetHeroes" && context.MethodInfo.DeclaringType?.Name == "HeroesController")
            {
                var okResponse = operation.Responses["200"];
                okResponse.Description = "Returns a list of all heroes";
                
                var example = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["id"] = new OpenApiInteger(1),
                        ["name"] = new OpenApiString("Bruce Wayne"),
                        ["heroName"] = new OpenApiString("Batman"),
                        ["description"] = new OpenApiString("The Dark Knight of Gotham City"),
                        ["dateOfBirth"] = new OpenApiDateTime(new DateTime(1980, 3, 30)),
                        ["height"] = new OpenApiDouble(188.5),
                        ["weight"] = new OpenApiDouble(95.0),
                        ["superpowers"] = new OpenApiArray
                        {
                            new OpenApiObject
                            {
                                ["id"] = new OpenApiInteger(1),
                                ["name"] = new OpenApiString("Genius Intellect"),
                                ["description"] = new OpenApiString("Exceptional detective skills and strategic thinking")
                            },
                            new OpenApiObject
                            {
                                ["id"] = new OpenApiInteger(2),
                                ["name"] = new OpenApiString("Martial Arts"),
                                ["description"] = new OpenApiString("Master of multiple fighting styles")
                            }
                        }
                    },
                    new OpenApiObject
                    {
                        ["id"] = new OpenApiInteger(2),
                        ["name"] = new OpenApiString("Clark Kent"),
                        ["heroName"] = new OpenApiString("Superman"),
                        ["description"] = new OpenApiString("The Man of Steel"),
                        ["dateOfBirth"] = new OpenApiDateTime(new DateTime(1985, 6, 18)),
                        ["height"] = new OpenApiDouble(190.0),
                        ["weight"] = new OpenApiDouble(107.0),
                        ["superpowers"] = new OpenApiArray
                        {
                            new OpenApiObject
                            {
                                ["id"] = new OpenApiInteger(3),
                                ["name"] = new OpenApiString("Flight"),
                                ["description"] = new OpenApiString("Ability to fly at supersonic speeds")
                            },
                            new OpenApiObject
                            {
                                ["id"] = new OpenApiInteger(4),
                                ["name"] = new OpenApiString("Super Strength"),
                                ["description"] = new OpenApiString("Incredible physical strength")
                            }
                        }
                    }
                };
                
                okResponse.Content["application/json"].Example = example;
            }
            
            if (context.MethodInfo.Name == "GetSuperpowers" && context.MethodInfo.DeclaringType?.Name == "SuperpowersController")
            {
                var okResponse = operation.Responses["200"];
                okResponse.Description = "Returns a list of all superpowers";
                
                var example = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["id"] = new OpenApiInteger(1),
                        ["name"] = new OpenApiString("Genius Intellect"),
                        ["description"] = new OpenApiString("Exceptional detective skills and strategic thinking")
                    },
                    new OpenApiObject
                    {
                        ["id"] = new OpenApiInteger(2),
                        ["name"] = new OpenApiString("Martial Arts"),
                        ["description"] = new OpenApiString("Master of multiple fighting styles")
                    },
                    new OpenApiObject
                    {
                        ["id"] = new OpenApiInteger(3),
                        ["name"] = new OpenApiString("Flight"),
                        ["description"] = new OpenApiString("Ability to fly at supersonic speeds")
                    }
                };
                
                okResponse.Content["application/json"].Example = example;
            }
        }
    }
}
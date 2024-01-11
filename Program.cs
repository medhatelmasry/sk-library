
using System.Configuration;
using Microsoft.SemanticKernel;

string _endpoint = ConfigurationManager.AppSettings["endpoint"]!;
string _apiKey = ConfigurationManager.AppSettings["api-key"]!;
string _deploymentName = ConfigurationManager.AppSettings["deployment-name"]!;

var builder = Kernel.CreateBuilder();

builder.Services
    .AddAzureOpenAITextGeneration(
        _deploymentName
        , _endpoint
        , _apiKey);

var kernel = builder.Build();

var functionDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Skills", "Baking");
var semanticFunctions = kernel.ImportPluginFromPromptDirectory(functionDirectory);

/* request user for input */
Console.WriteLine("Enter a cake type you want to bake:");
var cakeType = Console.ReadLine();
var functionResult = await kernel.InvokeAsync(semanticFunctions["CakeRecipe"],
    new KernelArguments {
                { "input", cakeType }
    });
Console.WriteLine(functionResult);
Console.WriteLine();

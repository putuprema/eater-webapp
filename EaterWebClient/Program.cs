using EaterWebClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddRefitClient<IEaterApiClient>(
    settings: new RefitSettings
    {
        ContentSerializer = new SystemTextJsonContentSerializer(
            new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new JsonStringEnumConverter() },

            }),

        ExceptionFactory = async httpResponseMessage =>
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var errorMessage = string.Empty;
                try
                {
                    var errorResponse = await httpResponseMessage.Content.ReadFromJsonAsync<BaseResponse>();
                    errorMessage = errorResponse?.Message ?? "Unknown error occured. Please try again in a few minutes";
                }
                catch (Exception)
                {
                    errorMessage = "Unknown error occured. Please try again in a few minutes";
                }
                return new EaterApiException(httpResponseMessage.StatusCode, errorMessage);
            }
            return null;
        }
    })
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://eaterapigwap01.azure-api.net");
    });

builder.Services.AddMudServices();

builder.Services.AddSingleton<AppViewModel>();
builder.Services.AddSingleton<ITableService, TableService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IOrderService, OrderService>();

await builder.Build().RunAsync();

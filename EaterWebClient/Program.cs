using Blazored.LocalStorage;
using Blazored.SessionStorage;
using EaterWebClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomCenter;
    config.SnackbarConfiguration.MaximumOpacity = 100;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.ClearAfterNavigation = true;
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddTransient<AuthHeaderHandler>();

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
                    errorMessage = errorResponse?.Message;
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        errorMessage = httpResponseMessage.StatusCode switch
                        {
                            HttpStatusCode.Unauthorized => "Please check your credentials",
                            HttpStatusCode.NotFound => "Item not found",
                            HttpStatusCode.BadRequest => "Bad request",
                            _ => "Unknown error occured. Please try again in a few minutes"
                        };
                    }
                }
                return new EaterApiException(httpResponseMessage.StatusCode, errorMessage);
            }
            return null;
        }
    })
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://eaterapigwap01.azure-api.net");
    })
    .AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddScoped<AppViewModel>();
builder.Services.AddTransient<MenuViewModel>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

await builder.Build().RunAsync();

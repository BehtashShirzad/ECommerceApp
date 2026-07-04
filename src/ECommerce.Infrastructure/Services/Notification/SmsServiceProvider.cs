using System.Net.Http.Json;
using System.Text;
using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
 

namespace ECommerce.Infrastructure.Services.Notification;

public class SmsServiceProvider(
    HttpClient httpClient,
    IOptions<SmsProviderOptions> options,
    ILogger<SmsServiceProvider> logger)
    : ISmsServiceProvider
{
    private readonly SmsProviderOptions _options = options.Value;

    public record ResultDto(string value,int RetStatus,string StrRetStatus);
    public async Task<bool> SendSmsAsync(string toNumber, string message,CancellationToken cancellationToken=default)
    {
        try
        {

       
        var username = _options.Username;
        var password = _options.ApiKey;
        var bodyId = 208577;
        var content = new StringContent(JsonConvert.SerializeObject(new
        {
            to = toNumber,
            username ,
            password,
            bodyId,
            text = message
            
        }),Encoding.UTF8, "application/json");
        var uri = $"{_options.HostAddress}{_options.Endpoint}";
        var result =await httpClient
            .PostAsync(uri, content,cancellationToken);
        result.EnsureSuccessStatusCode();
        var contentResult = await result.Content.ReadFromJsonAsync<ResultDto>(cancellationToken);
         if (contentResult!.RetStatus==1)
            return true;
 
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return false;
        }
       
        return false;
    }
}
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WebApi.Infrastructure.Payments;

public class AdamspayService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public AdamspayService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Adamspay:ApiKey"]!;
    }

public async Task<string?> CreatePaymentLink(
    string reference,
    decimal amountValue,
    string currency,
    string? description,
    string? callbackUrl,
    DateTime? validStart = null,
    DateTime? validEnd = null)
{
    var debt = new Dictionary<string, object?>
    {
        ["docId"] = reference,
        ["amount"] = new { currency = currency, value = amountValue },
        ["label"] = description
    };
    if (validStart.HasValue && validEnd.HasValue)
    {
        debt["validPeriod"] = new { start = validStart.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"), end = validEnd.Value.ToString("yyyy-MM-ddTHH:mm:sszzz") };
    }
    var payload = new Dictionary<string, object?>
    {
        ["debt"] = debt
    };
    if (!string.IsNullOrEmpty(callbackUrl))
        payload["callbackUrl"] = callbackUrl;

    var request = new HttpRequestMessage(HttpMethod.Post, "https://staging.adamspay.com/api/v1/debts");
    request.Headers.Add("apikey", _apiKey);
    request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

    var response = await _httpClient.SendAsync(request);
    var json = await response.Content.ReadAsStringAsync();
    // Log de la respuesta para depuración
    Console.WriteLine("Respuesta Adamspay: " + json);
    if (!response.IsSuccessStatusCode)
        return $"Error Adamspay: {response.StatusCode} - {json}";
    try
    {
        using var doc = JsonDocument.Parse(json);
        if (doc.RootElement.TryGetProperty("debt", out var debtElem) && debtElem.TryGetProperty("payUrl", out var urlElem))
        {
            return urlElem.GetString();
        }
        return $"Respuesta inesperada: {json}";
    }
    catch (Exception ex)
    {
        return $"Error procesando la respuesta Adamspay: {ex.Message} - {json}";
    }
}
}

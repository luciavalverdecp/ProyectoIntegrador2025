using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class OpenAIClient
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public OpenAIClient(HttpClient http, IConfiguration config)
    {
        _http = http;


        _apiKey = config["OpenAI:ApiKey"];


        if (string.IsNullOrEmpty(_apiKey))
        {
            _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        }


        if (string.IsNullOrEmpty(_apiKey))
        {
            throw new Exception(
            "API Key de OpenAI no configurada. Definí OpenAI:ApiKey o la variable OPENAI_API_KEY."
            );
        }
    }

    public async Task<string> EnviarPromptAsync(string prompt)
    {
        var request = new
        {
            model = "gpt-4.1-mini",
            input = prompt
        };

        var httpRequest = new HttpRequestMessage(
                        HttpMethod.Post,
                        "https://api.openai.com/v1/responses"
                        );

        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        httpRequest.Content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.SendAsync(httpRequest);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return json;
    }
}

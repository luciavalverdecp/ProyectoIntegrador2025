using System.Text.Json;
using NextLevel.LogicaAplicacion.InterfacesServicios;
using NextLevel.LogicaNegocio.Entidades;

public class OpenAIService : IOpenAIService
{
    private readonly OpenAIClient _client;

    public OpenAIService(OpenAIClient client)
    {
        _client = client;
    }

    public async Task<CriteriosBusqueda> InterpretarPregunta(string pregunta)
    {
        var prompt = $$"""
                    A partir de la pregunta del usuario, devolvé SOLO un JSON con esta forma:
                    {
                      "Categoria": string | null,
                      "Dificultad": string | null,
                      "DuracionMax": number | null
                    }

                    Pregunta:
                    "{{pregunta}}"
                    """;

        var respuestaCruda = await _client.EnviarPromptAsync(prompt);

        using var doc = JsonDocument.Parse(respuestaCruda);


        var jsonIA = doc.RootElement
        .GetProperty("output")[0]
        .GetProperty("content")[0]
        .GetProperty("text")
        .GetString();
        if (string.IsNullOrWhiteSpace(jsonIA))
            throw new Exception("La IA no devolvió criterios");

        jsonIA = jsonIA.Trim();

        var inicio = jsonIA.IndexOf('{');
        var fin = jsonIA.LastIndexOf('}');

        if (inicio == -1 || fin == -1)
            throw new Exception("La IA no devolvió un JSON válido");

        jsonIA = jsonIA.Substring(inicio, fin - inicio + 1);

        
        return JsonSerializer.Deserialize<CriteriosBusqueda>(
        jsonIA,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )!;
    }

    public async Task<string> GenerarRespuesta(string pregunta, List<Curso> cursos)
    {
        var listado = string.Join("\n",
            cursos.Select(c =>
                $"- {c.Nombre} (Dificultad: {c.Dificultad}, Duración: {c.Duracion} meses)")
        );

        var prompt = $"""
                    Sos un asistente que recomienda cursos.

                    REGLAS:
                    - SOLO usá los cursos del listado
                    - NO inventes cursos
                    - Si no hay coincidencias, decilo

                    Pregunta del usuario:
                    "{pregunta}"

                    Cursos disponibles:
                    {listado}

                    Respondé de forma clara y amigable.
                    """;

        var respuestaCruda = await _client.EnviarPromptAsync(prompt);

        using var doc = JsonDocument.Parse(respuestaCruda);

        var textoFinal = doc.RootElement
            .GetProperty("output")[0]
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetString();

        return textoFinal ?? "No pude generar una respuesta.";
    }
}
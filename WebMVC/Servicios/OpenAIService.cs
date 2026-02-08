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
                          "DuracionMax": number | null,
                          "NombreDocente": string | null,
                          "FechaInicio": DateTime | null,
                          "Calificacion": number | null,
                          "Precio": number | null
                        }

                        Pregunta:
                        "{{pregunta}}"

                        REGLAS:
                        - Si el usuario pide "mejores", "más recomendados" o "mejor calificados",
                          usá Calificacion como un valor mínimo razonable (ej. 4).

                        - Si el usuario pide una fecha próxima, inmediata, desde ahora o similar,
                          devolvé FechaInicio = null.
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

        var criterios = JsonSerializer.Deserialize<CriteriosBusqueda>(
                        jsonIA,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    )!;

        if (criterios.FechaInicio == null &&
            pregunta.ToLower().Contains("próxim") ||
            pregunta.ToLower().Contains("ahora") ||
            pregunta.ToLower().Contains("lo antes posible"))
        {
            criterios.FechaInicio = DateTime.Today;
        }

        return JsonSerializer.Deserialize<CriteriosBusqueda>(
        jsonIA,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        )!;
    }

    public async Task<string> GenerarRespuesta(string pregunta, List<Curso> cursos)
    {
        var listado = string.Join("\n",
                    cursos.Select(c =>
                        $"- {c.Nombre} " +
                        $"(Docente: {c.Docente.NombreCompleto}, " +
                        $"Dificultad: {c.Dificultad}, " +
                        $"Duración: {c.Duracion} meses, " +
                        $"Precio: ${c.Precio}, " +
                        $"Calificación: {c.Calificacion}/5, " +
                        $"Inicio: {c.FechaInicio:dd/MM/yyyy})"
                    )
                );


        var prompt = $"""
                    Sos un asistente que recomienda cursos.

                    CONTEXTO IMPORTANTE:
                    - Dificultad 1 = Principiante
                    - Dificultad 2 = Intermedio
                    - Dificultad 3 = Avanzado

                    REGLAS:
                    - Usá SOLO la información de los cursos disponibles
                    - NO inventes cursos
                    - NO menciones listados, datos internos ni cómo obtenés la información
                    - Si no hay cursos que coincidan, respondé de forma natural (ej: "por el momento no hay cursos disponibles de ese nivel")

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
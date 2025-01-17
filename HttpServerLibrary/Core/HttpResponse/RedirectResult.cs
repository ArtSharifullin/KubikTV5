namespace HttpServerLibrary.Core.HttpResponse;

/// <summary>
/// Представляет результат перенаправления HTTP-запроса.
/// </summary>
public class RedirectResult : IHttpResponseResult
{
    private readonly string _location;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="RedirectResult"/> с указанным местоположением.
    /// </summary>
    /// <param name="location">Путь, на который будет перенаправлен запрос.</param>
    public RedirectResult(string location)
    {
        _location = location;
    }

    /// <summary>
    /// Выполняет перенаправление HTTP-запроса.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    public void Execute(HttpRequestContext context)
    {
        var response = context.Response;
        response.StatusCode = 302;
        response.Headers.Add("Location", _location); // Заголовок для указания пути
        response.Close();
    }
}

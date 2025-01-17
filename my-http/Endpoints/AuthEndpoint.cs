using System.Net;
using HttpServerLibrary;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.Core.HttpResponse;
using MyHttpServer.Helpers;
using TemlateEngine;
using MyHttpServer.Repositories;

namespace MyHttpServer.Endpoints;

/// <summary>
/// Представляет конечную точку для аутентификации пользователей.
/// </summary>
public class AuthEndpoint : EndpointBase
{
    /// <summary>
    /// Получает или задает помощника ответа.
    /// </summary>
    public virtual IResponseHelper ResponseHelper { get; set; } = new ResponseHelper();

    /// <summary>
    /// Обрабатывает GET-запрос для страницы входа.
    /// </summary>
    /// <returns>Результат HTTP-ответа.</returns>
    [Get("login")]
    public IHttpResponseResult Get()
    {
        var localPath = "Auth/login.html";
        var responseText = ResponseHelper.GetResponseText(localPath);
        return Html(responseText);
    }

    /// <summary>
    /// Обрабатывает POST-запрос для входа пользователя.
    /// </summary>
    /// <param name="login">Логин пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <returns>Результат HTTP-ответа.</returns>
    [Post("login")]
    public IHttpResponseResult Login(string login, string password)
    {
        var userRepository = new UserRepository();
        var templateEngine = new HtmlTemplateEngine();
        var localPath = "Auth/login.html";
        var responseText = ResponseHelper.GetResponseText(localPath);

        // Логирование входных данных
        Console.WriteLine("----- User on login-page -----");
        Console.WriteLine($"Login attempt with login: {login} and password: {password}");

        var user = userRepository.GetUser(login, password);

        // Логирование результата запроса
        if (user == null)
        {
            Console.WriteLine("user not found in the database.");
            var result = templateEngine.Render(responseText, "<!--ERROR: USER DOESN'T EXIST-->", "<p class=\"error_message\">Такого пользователя не существует</p>");
            return Html(result);
        }

        Console.WriteLine($"User found: {user.Login}, {user.Password}");

        var token = Guid.NewGuid().ToString();
        Cookie nameCookie = new Cookie("session-token", token);
        nameCookie.Path = "/";
        Context.Response.Cookies.Add(nameCookie);
        SessionStorage.SaveSession(token, user.Id.ToString());

        Console.WriteLine("User found and redirecting to index");
        return Redirect("/index");
    }
}

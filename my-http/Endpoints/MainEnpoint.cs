using HttpServerLibrary;
using HttpServerLibrary.Core;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.Core.HttpResponse;
using HttpServerLibrary.Models;
using MyHttpServer.Helpers;
using TemlateEngine;
using MyHttpServer.Repositories;

namespace MyHttpServer.Endpoints;

/// <summary>
/// Представляет основную конечную точку для отображения страниц.
/// </summary>
public class MainEndpoint : EndpointBase
{
    /// <summary>
    /// Получает или задает помощника ответа.
    /// </summary>
    public virtual IResponseHelper ResponseHelper { get; set; } = new ResponseHelper();

    /// <summary>
    /// Обрабатывает GET-запрос для главной страницы.
    /// </summary>
    /// <returns>Результат HTTP-ответа.</returns>
    [Get("index")]
    public IHttpResponseResult GetPage()
    {
        var localPath = "index.html";
        var responseText = ResponseHelper.GetResponseText(localPath);

        Console.WriteLine("----- User on index-page -----");
        Console.WriteLine(AppConfig.GetInstance().ConnectionStrings["DefaultConnection"]);
        var movieRepository = new MovieRepository();
        var movies = movieRepository.GetMovies();   
        var templateEngine = new HtmlTemplateEngine();
        var model = new
        {
            movies = movies,
        };

        if (!IsAuthorized(Context))
        {
            var text = templateEngine.Render(responseText, model);
            return Html(text);
        }
        var userId = Int32.Parse(SessionStorage.GetUserId(Context.Request.Cookies["session-token"].Value));
        var userRepository = new UserRepository();
        var user = userRepository.GetById(userId);
        var text_with_name = templateEngine.Render(responseText, "<a href=\"/login\"><button class=\"header__btn-login\">Войти</button></a>", $"<a href=\"#\" id=\"adminLink\"h3>{user.Login}</h3></a>");
        var result = templateEngine.Render(text_with_name, model);
        return Html(result);
    }

    /// <summary>
    /// Обрабатывает GET-запрос для страницы фильма "Веном".
    /// </summary>
    /// <returns>Результат HTTP-ответа.</returns>
    [Get("movie")]
    public IHttpResponseResult GetVenomPage(string movieName)
    {
        var localPath = "Venom/venom.html";
        var responseText = ResponseHelper.GetResponseText(localPath);
        Console.WriteLine(movieName);

        Console.WriteLine("----- User on venom-page -----");

        var movieRepository = new MovieRepository();
        var movie = movieRepository.GetByName(movieName);
        var templateEngine = new HtmlTemplateEngine();
        var model = new
        {
            movie = movie,
        };

        if (!IsAuthorized(Context))
        {
            var text = templateEngine.Render(responseText, model);
            return Html(text);
        }

        var userId = Int32.Parse(SessionStorage.GetUserId(Context.Request.Cookies["session-token"].Value));
        var userRepository = new UserRepository();
        var user = userRepository.GetById(userId);
        var text_with_name = templateEngine.Render(responseText, "<a href=\"/login\"><button class=\"header__btn-login\">Войти</button></a>", $"<a href=\"admin\" id=\"user-login\"><h3>{user.Login}</h3></a>");
        var result = templateEngine.Render(text_with_name, model);
        return Html(result);
    }

    /// <summary>
    /// Проверяет, авторизован ли пользователь.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <returns>True, если пользователь авторизован; иначе false.</returns>
    public bool IsAuthorized(HttpRequestContext context)
    {
        // Проверка наличия Cookie с session-token
        if (context.Request.Cookies.Any(c => c.Name == "session-token"))
        {
            var cookie = context.Request.Cookies["session-token"];
            return SessionStorage.ValidateToken(cookie.Value);
        }

        return false;
    }
}

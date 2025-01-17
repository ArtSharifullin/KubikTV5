using System.Net;
using HttpServerLibrary;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.Core.HttpResponse;
using MyHttpServer.Helpers;
using TemlateEngine;
using MyHttpServer.Repositories;

namespace MyHttpServer.Endpoints;

/// <summary>
/// ������������ �������� ����� ��� �������������� �������������.
/// </summary>
public class AuthEndpoint : EndpointBase
{
    /// <summary>
    /// �������� ��� ������ ��������� ������.
    /// </summary>
    public virtual IResponseHelper ResponseHelper { get; set; } = new ResponseHelper();

    /// <summary>
    /// ������������ GET-������ ��� �������� �����.
    /// </summary>
    /// <returns>��������� HTTP-������.</returns>
    [Get("login")]
    public IHttpResponseResult Get()
    {
        var localPath = "Auth/login.html";
        var responseText = ResponseHelper.GetResponseText(localPath);
        return Html(responseText);
    }

    /// <summary>
    /// ������������ POST-������ ��� ����� ������������.
    /// </summary>
    /// <param name="login">����� ������������.</param>
    /// <param name="password">������ ������������.</param>
    /// <returns>��������� HTTP-������.</returns>
    [Post("login")]
    public IHttpResponseResult Login(string login, string password)
    {
        var userRepository = new UserRepository();
        var templateEngine = new HtmlTemplateEngine();
        var localPath = "Auth/login.html";
        var responseText = ResponseHelper.GetResponseText(localPath);

        // ����������� ������� ������
        Console.WriteLine("----- User on login-page -----");
        Console.WriteLine($"Login attempt with login: {login} and password: {password}");

        var user = userRepository.GetUser(login, password);

        // ����������� ���������� �������
        if (user == null)
        {
            Console.WriteLine("user not found in the database.");
            var result = templateEngine.Render(responseText, "<!--ERROR: USER DOESN'T EXIST-->", "<p class=\"error_message\">������ ������������ �� ����������</p>");
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

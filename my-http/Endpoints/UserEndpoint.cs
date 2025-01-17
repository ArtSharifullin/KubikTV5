using System.Data.Common;
using System.Data.SqlClient;
using HttpServerLibrary;
using HttpServerLibrary.Core;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.Core.HttpResponse;
using MyHttpServer.Repositories;

namespace MyServer.Endpoints;

/// <summary>
/// ѕредставл€ет конечную точку дл€ операций с пользовател€ми.
/// </summary>
public class UserEndpoints : EndpointBase
{
    /// <summary>
    /// ќбрабатывает GET-запрос дл€ получени€ всех пользователей.
    /// </summary>
    /// <returns>–езультат HTTP-ответа в формате JSON, содержащий всех пользователей.</returns>
    [Get("users")]
    public IHttpResponseResult GetAllUsers()
    {
        var userRepository = new UserRepository();
        return Json(userRepository.GetUsers());
    }
}

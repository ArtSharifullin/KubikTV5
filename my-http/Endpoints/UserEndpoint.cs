using System.Data.Common;
using System.Data.SqlClient;
using HttpServerLibrary;
using HttpServerLibrary.Core;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.Core.HttpResponse;
using MyHttpServer.Repositories;

namespace MyServer.Endpoints;

/// <summary>
/// ������������ �������� ����� ��� �������� � ��������������.
/// </summary>
public class UserEndpoints : EndpointBase
{
    /// <summary>
    /// ������������ GET-������ ��� ��������� ���� �������������.
    /// </summary>
    /// <returns>��������� HTTP-������ � ������� JSON, ���������� ���� �������������.</returns>
    [Get("users")]
    public IHttpResponseResult GetAllUsers()
    {
        var userRepository = new UserRepository();
        return Json(userRepository.GetUsers());
    }
}

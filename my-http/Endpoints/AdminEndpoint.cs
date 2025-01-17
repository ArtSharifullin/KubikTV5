using System.Net;
using HttpServerLibrary;
using HttpServerLibrary.Core;
using HttpServerLibrary.Core.Attributes;
using HttpServerLibrary.Core.HttpResponse;
using MyHttpServer.Helpers;
using MyHttpServer.Models;
using TemlateEngine;
using MyHttpServer.Repositories;

namespace MyHttpServer.Endpoints;

/// <summary>
/// Представляет конечную точку для административных операций.
/// </summary>
public class AdminEndpoint : EndpointBase
{
    /// <summary>
    /// Получает или задает помощника ответа.
    /// </summary>
    public virtual IResponseHelper ResponseHelper { get; set; } = new ResponseHelper();

    /// <summary>
    /// Обрабатывает GET-запрос для страницы входа администратора.
    /// </summary>
    /// <returns>Результат HTTP-ответа.</returns>
    [Get("admlogin")]
    public IHttpResponseResult Get()
    {
        var localPath = "Auth/admin_login.html";
        var responseText = ResponseHelper.GetResponseText(localPath);
        return Html(responseText);
    }

    /// <summary>
    /// Обрабатывает POST-запрос для входа администратора.
    /// </summary>
    /// <param name="login">Логин администратора.</param>
    /// <param name="password">Пароль администратора.</param>
    /// <returns>Результат HTTP-ответа.</returns>
    [Post("admlogin")]
    public IHttpResponseResult Login(string login, string password)
    {
        var localPath = "Auth/admin_login.html";
        var responseText = ResponseHelper.GetResponseText(localPath);
        var templateEngine = new HtmlTemplateEngine();
        var adminRepository = new AdminRepository();

        // Логирование входных данных
        Console.WriteLine("----- User on admin-login-page -----");
        Console.WriteLine($"Login attempt with login: {login} and password: {password}");

        var admin = adminRepository.GetAdmin(login,password);

        // Логирование результата запроса
        if (admin == null)
        {
            Console.WriteLine("admin not found in the database.");
            var result = templateEngine.Render(responseText, "<!--ERROR: ADMIN DOESN'T EXIST-->", "<p class=\"error_message\">Такого администратора не существует</p>");
            return Html(result);
        }

        Console.WriteLine($"admin found: {admin.Login}, {admin.Password}");

        var token = Guid.NewGuid().ToString();
        Cookie nameCookie = new Cookie("admin-session-token", token);
        nameCookie.Path = "/";
        Context.Response.Cookies.Add(nameCookie);
        SessionStorage.SaveSession(token, admin.Id.ToString());

        Console.WriteLine("admin found and redirecting to index");
        return Redirect("/admin");
    }

    /// <summary>
    /// Обрабатывает GET-запрос для административной страницы.
    /// </summary>
    /// <returns>Результат HTTP-ответа.</returns>
    [Get("admin")]
    public IHttpResponseResult GetPage()
    {
        var localPath = "Admin/admin.html";
        var responseText = ResponseHelper.GetResponseText(localPath);

        Console.WriteLine("----- admin on admin-page -----");
        var movieRepository = new MovieRepository();
        var userRepository = new UserRepository();
        var adminRepository = new AdminRepository();

        var admins = adminRepository.GetAdmins();
        var users = userRepository.GetUsers();
        var movies = movieRepository.GetMovies();

        var templateEngine = new HtmlTemplateEngine();
        var model = new
        {
            movies = movies,
            admins = admins,
            users = users,
        };

        if (!IsAuthorized(Context))
        {
            return Redirect("/admlogin");
        }

        var text = templateEngine.Render(responseText, model);
        return Html(text);
    }

    /// <summary>
    /// Проверяет, авторизован ли пользователь.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <returns>True, если пользователь авторизован; иначе false.</returns>
    public bool IsAuthorized(HttpRequestContext context)
    {
        // Проверка наличия Cookie с session-token
        if (context.Request.Cookies.Any(c => c.Name == "admin-session-token"))
        {
            var cookie = context.Request.Cookies["admin-session-token"];
            return SessionStorage.ValidateToken(cookie.Value);
        }

        return false;
    }

    /// <summary>
    /// Обрабатывает POST-запрос для добавления пользователя.
    /// </summary>
    /// <param name="addUserLogin">Логин нового пользователя.</param>
    /// <param name="addUserPassword">Пароль нового пользователя.</param>
    /// <returns>Результат HTTP-ответа.</returns>
    [Post("admin/user/add")]
    public IHttpResponseResult AddUser(string addUserLogin, string addUserPassword)
    {
        try
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetByLogin(addUserLogin);
            if (user == null)
            {
                User newUser = new User
                {
                    Login = addUserLogin,
                    Password = addUserPassword
                };
                userRepository.Create(newUser);
                user = userRepository.GetByLogin(addUserLogin);
                return Json(user);
            }

            return Json(false);
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine("Error adding movie: " + ex.Message);
            return Json(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Обрабатывает POST-запрос для удаления пользователя по идентификатору.
    /// </summary>
    /// <param name="deleteUserId">Идентификатор пользователя для удаления.</param>
    /// <returns>Результат HTTP-ответа.</returns>
    [Post("admin/user/delete")]
    public IHttpResponseResult DeleteUserById(string deleteUserId)
    {
        try
        {
            var userRepository = new UserRepository();
            var user = userRepository.GetById(int.Parse(deleteUserId));
            if (deleteUserId != null && user.Login != null)
            {
                userRepository.Delete(deleteUserId);
                return Json(userRepository.GetUsers());
            }
            return Json(false);
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine("Error deleting user: " + ex.Message);
            return Json(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Обрабатывает POST-запрос для добавления фильма.
    /// </summary>
    /// <param name="addMovieName">Название фильма.</param>
    /// <param name="addMovieEnglishName">Английское название фильма.</param>
    /// <param name="addMovieYear">Год выпуска фильма.</param>
    /// <param name="addMovieCountry">Страна производства фильма.</param>
    /// <param name="addMovieGenre">Жанр фильма.</param>
    /// <param name="addMovieDirector">Режиссер фильма.</param>
    /// <param name="addMovieActors">Актеры фильма.</param>
    /// <param name="addMovieDescription">Описание фильма.</param>
    /// <param name="addMovieLink">Ссылка на фильм.</param>
    /// <param name="addMovieImageLink">Ссылка на изображение фильма.</param>
    /// <param name="addMovieLLink">Ссылка на фильм.</param>
    /// <returns>Результат HTTP-ответа.</returns>
    [Post("admin/movie/add")]
    public IHttpResponseResult AddMovie(string addMovieName, string addMovieEnglishName, string addMovieYear, string addMovieCountry, string addMovieGenre, string addMovieDirector, string addMovieActors, string addMovieDescription, string addMovieLink, string addMovieImageLink)
    {
        try
        {
            Console.WriteLine(addMovieCountry);
            var movieRepository = new MovieRepository();
            var movie = movieRepository.GetByName(addMovieName);

            if (movie != null)
            {
                return Json(false);
            }
            Movie newMovie = new Movie
            {
                Name = addMovieName,
                EnglishName = addMovieEnglishName,
                Year = addMovieYear,
                Country = addMovieCountry,
                Genre = addMovieGenre,
                Director = addMovieDirector,
                Actors = addMovieActors,
                Description = addMovieDescription,
                MovieLink = addMovieLink,
                ImageLink = addMovieImageLink,
                Link = "/movie"
            };
            movieRepository.Create(newMovie);
            movie = movieRepository.GetByName(addMovieName);
            return Json(movie);
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine("Error adding movie: " + ex.Message);
            return Json(new { error = ex.Message });
        }
    }

    /// <summary>
    /// Обрабатывает POST-запрос для удаления фильма по идентификатору.
    /// </summary>
    /// <param name="deleteMovieId">Идентификатор фильма для удаления.</param>
    /// <returns>Результат HTTP-ответа.</returns>
    [Post("admin/movie/delete")]
    public IHttpResponseResult DeleteMovieById(string deleteMovieId)
    {
        try
        {
            Console.WriteLine(deleteMovieId);
            var movieRepository = new MovieRepository();
            var movie = movieRepository.GetById(int.Parse(deleteMovieId));
            if (deleteMovieId != null && movie.Name != null)
            {
                movieRepository.Delete(deleteMovieId);
                return Json(movieRepository.GetMovies());
            }
            return Json(false);
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine("Error deleting user: " + ex.Message);
            return Json(new { error = ex.Message });
        }
    }


    [Post("admin/user/update")]
    public IHttpResponseResult UpdateUser(int updateUserId, string updateUserLogin, string updateUserPassword)
    {
        try
        {
            var userRepository = new UserRepository();
            var existingUser = userRepository.GetById(updateUserId);
            if (existingUser == null)
            {
                return Json(false);
            }

            existingUser.Login = updateUserLogin;
            existingUser.Password = updateUserPassword;
            userRepository.UpdateUser(existingUser);
            return Json(userRepository.GetUsers());
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine("Error updating user: " + ex.Message);
            return Json(new { error = ex.Message });
        }
    }

    [Post("admin/movie/update")]
    public IHttpResponseResult UpdateMovie(int updateMovieId, string updateMovieName, string updateMovieEnglishName, string updateMovieYear, string updateMovieCountry, string updateMovieGenre, string updateMovieDirector, string updateMovieActors, string updateMovieDescription, string updateMovieLink, string updateMovieImageLink)
    {
        try
        {
            var movieRepository = new MovieRepository();
            var existingMovie = movieRepository.GetById(updateMovieId);

            if (existingMovie == null)
            {
                return Json(false);
            }

            existingMovie.Name = updateMovieName;
            existingMovie.EnglishName = updateMovieEnglishName;
            existingMovie.Year = updateMovieYear;
            existingMovie.Country = updateMovieCountry;
            existingMovie.Genre = updateMovieGenre;
            existingMovie.Director = updateMovieDirector;
            existingMovie.Actors = updateMovieActors;
            existingMovie.Description = updateMovieDescription;
            existingMovie.MovieLink = updateMovieLink;
            existingMovie.ImageLink = updateMovieImageLink;

            movieRepository.UpdateMovie(existingMovie);

            return Json(movieRepository.GetMovies());
        }
        catch (Exception ex)
        {
            // Логирование ошибки
            Console.WriteLine("Error adding movie: " + ex.Message);
            return Json(new { error = ex.Message });
        }
    }
}

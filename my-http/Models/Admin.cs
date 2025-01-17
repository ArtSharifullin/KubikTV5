namespace MyHttpServer.Models;

/// <summary>
/// Представляет модель администратора.
/// </summary>
public class Admin
{
    /// <summary>
    /// Получает или задает уникальный идентификатор администратора.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Получает или задает логин администратора.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Получает или задает пароль администратора.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Получает или задает имя администратора.
    /// </summary>
    public string Name { get; set; }
}

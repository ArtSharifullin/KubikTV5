using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Models;

/// <summary>
/// Представляет модель фильма.
/// </summary>
public class Movie
{
    /// <summary>
    /// Получает или задает уникальный идентификатор фильма.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Получает или задает название фильма.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Получает или задает английское название фильма.
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// Получает или задает год выпуска фильма.
    /// </summary>
    public string Year { get; set; }

    /// <summary>
    /// Получает или задает страну производства фильма.
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// Получает или задает жанр фильма.
    /// </summary>
    public string Genre { get; set; }

    /// <summary>
    /// Получает или задает режиссера фильма.
    /// </summary>
    public string Director { get; set; }

    /// <summary>
    /// Получает или задает актеров фильма.
    /// </summary>
    public string Actors { get; set; }

    /// <summary>
    /// Получает или задает описание фильма.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Получает или задает ссылку на фильм.
    /// </summary>
    public string MovieLink { get; set; }

    /// <summary>
    /// Получает или задает ссылку на изображение фильма.
    /// </summary>
    public string ImageLink { get; set; }

    /// <summary>
    /// Получает или задает ссылку на фильм.
    /// </summary>
    public string Link { get; set; }
}

namespace AllProject.UnitTests;
using NUnit.Framework;
using TemlateEngine;
using Models;

/// <summary>
/// Класс для тестирования HTML шаблонного движка.
/// </summary>
[TestFixture]
public class HtmlTemplateEngineTests
{
    private HtmlTemplateEngine _engine;

    /// <summary>
    /// Настройка перед каждым тестом.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _engine = new HtmlTemplateEngine();
    }

    /// <summary>
    /// Тест на замену переменных в шаблоне.
    /// </summary>
    [Test]
    public void Render_ShouldReplaceVariables()
    {
        var template = "Hello, {{Name}}! How are you?";
        var model = new { Name = "Bob" };
        var expected = "Hello, Bob! How are you?";

        var result = _engine.Render(template, model);

        Assert.AreEqual(expected, result);
    }

    /// <summary>
    /// Тест на обработку циклов в шаблоне.
    /// </summary>
    [Test]
    public void Render_ShouldHandleLoops()
    {
        var template = "{%for% item %in% Items}{{item.Name}}{%/for%}";
        var model = new { Items = new List<Item> { new Item { Name = "Item1" }, new Item { Name = "Item2" } } };
        var expected = "Item1Item2";

        var result = _engine.Render(template, model);

        Assert.AreEqual(expected, result);
    }

    /// <summary>
    /// Тест на обработку вложенных свойств в шаблоне.
    /// </summary>
    [Test]
    public void Render_ShouldHandleNestedProperties()
    {
        var template = "Genre: {{Genre.Name}}";
        var model = new { Genre = new Genre { Name = "Sci-Fi" } };
        var expected = "Genre: Sci-Fi";

        var result = _engine.Render(template, model);

        Assert.AreEqual(expected, result);
    }

    /// <summary>
    /// Тест на выброс исключения при неверном файле.
    /// </summary>
    [Test]
    public void Render_ShouldThrowExceptionForInvalidFile()
    {
        var fileInfo = new FileInfo("invalid_path.txt");
        var model = new { Name = "Bob" };

        Assert.Throws<FileNotFoundException>(() => _engine.Render(fileInfo, model));
    }

    /// <summary>
    /// Тест на выброс исключения при нечитаемом потоке.
    /// </summary>
    [Test]
    public void Render_ShouldThrowExceptionForUnreadableStream()
    {
        var stream = new MemoryStream();
        stream.Close();
        var model = new { Name = "Bob" };

        Assert.Throws<InvalidOperationException>(() => _engine.Render(stream, model));
    }

    /// <summary>
    /// Тест на обработку сложных циклов в шаблоне.
    /// </summary>
    [Test]
    public void Render_ShouldHandleComplexLoops()
    {
        var template = "{%for% item %in% Items}{{item.Name}} - {{item.Value}}{%/for%}";
        var model = new { Items = new List<Item> { new Item { Name = "Item1", Value = 10 }, new Item { Name = "Item2", Value = 20 } } };
        var expected = "Item1 - 10Item2 - 20";

        var result = _engine.Render(template, model);

        Assert.AreEqual(expected, result);
    }
}

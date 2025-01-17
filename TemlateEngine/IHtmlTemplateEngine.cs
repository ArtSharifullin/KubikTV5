namespace TemlateEngine;

public interface IHtmlTemplateEngine
{
    public string Render(string template, string str, string data);
    public string Render(string template, object obj);
}
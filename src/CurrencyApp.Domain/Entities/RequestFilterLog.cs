using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
namespace CurrencyApp.Domain.Entities;
public class RequestFilterLog
{
    [Key]
    [NotNull]
    public Guid Id { get; set; }

    [NotNull]
    public string ControllerName { get; set; }

    [NotNull]
    public string ActionName { get; set; }


    [NotNull]
    public string Method { get; set; }

    [NotNull]
    public string Protocol { get; set; }

    [NotNull]
    public string Host { get; set; }

    [NotNull]
    public string Path { get; set; }

    [NotNull]
    public DateTime CreateAt { get; set; }

    [NotNull]
    public string ContentType { get; set; }

    [NotNull]
    public string Data { get; set; }

    public RequestFilterLog() { }

    private RequestFilterLog(string controllerName, string actionName, string method, string protocol, string host, string path, string contentType, string data)
    {
        Id = Guid.NewGuid();
        ControllerName = controllerName;
        ActionName = actionName;
        Method = method;
        Protocol = protocol;
        Host = host;
        Path = path;
        CreateAt = DateTime.Now;
        ContentType = contentType;
        Data = data;
    }

    public static RequestFilterLog Create(string controllerName, string actionName, string method, string protocol, string host, string path, string contentType, string data)
    {
        return new RequestFilterLog(controllerName, actionName, method, protocol, host, path, contentType, data);
    }

}


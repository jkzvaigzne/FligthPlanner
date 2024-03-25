using System.Net;

namespace FligthPlanner.UseCases.Models;

public class ServiceResult
{
    public HttpStatusCode Status { get; set; }
    public object? ResultObject { get; set; }
}
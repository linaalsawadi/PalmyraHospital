using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Enums;
using Microsoft.AspNetCore.Http;

namespace PalmyraHospital.Infrastructure.Logging.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ILogService logService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logService.WriteLog(
                ex.Message,
                LogSeverity.Critical);

            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(
                "An unexpected error occurred.");
        }
    }
}
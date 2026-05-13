using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using Microsoft.AspNetCore.Http;

namespace PalmyraHospital.Infrastructure.Logging.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IRequestTraceService traceService)
    {
        var stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        await traceService.TraceAsync(
            context,
            stopwatch.ElapsedMilliseconds);
    }
}
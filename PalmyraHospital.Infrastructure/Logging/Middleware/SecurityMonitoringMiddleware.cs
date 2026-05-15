using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Infrastructure.Logging.Middleware;

public class SecurityMonitoringMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityMonitoringMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ISecurityLogger securityLogger)
    {
        var ip =
            context.Connection.RemoteIpAddress?.ToString();

        var userAgent =
            context.Request.Headers["User-Agent"].ToString();

        var forwarded =
            context.Request.Headers["X-Forwarded-For"].ToString();

        var via =
            context.Request.Headers["Via"].ToString();

        // Detect HTTP usage
        if (!context.Request.IsHttps)
        {
            await securityLogger.LogSecurityEventAsync(
                "HTTP request detected. Possible MITM risk.",
                SecurityEventType.SuspiciousRequest,
                LogSeverity.Warning,
                ipAddress: ip);
        }

        // Detect Proxy Headers
        if (!string.IsNullOrEmpty(forwarded)
            || !string.IsNullOrEmpty(via))
        {
            await securityLogger.LogSecurityEventAsync(
                "Possible proxy or MITM detected.",
                SecurityEventType.SuspiciousRequest,
                LogSeverity.Critical,
                ipAddress: ip);
        }

        // Detect suspicious tools
        if (userAgent.Contains("curl")
            || userAgent.Contains("python")
            || userAgent.Contains("mitmproxy"))
        {
            await securityLogger.LogSecurityEventAsync(
                $"Suspicious client detected: {userAgent}",
                SecurityEventType.SuspiciousRequest,
                LogSeverity.Warning,
                ipAddress: ip);
        }

        await _next(context);
    }
}
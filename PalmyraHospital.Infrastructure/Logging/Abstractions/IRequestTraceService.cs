using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PalmyraHospital.Infrastructure.Logging.Abstractions;

public interface IRequestTraceService
{
    Task TraceAsync(
        HttpContext context,
        long durationMs);
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Logging.Abstractions;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Infrastructure.Logging.Implementations;

public class SerilogService : ILogService
{
    public void WriteLog(
        string message,
        LogSeverity severity)
    {
        switch (severity)
        {
            case LogSeverity.Information:
                Serilog.Log.Information(message);
                break;

            case LogSeverity.Warning:
                Serilog.Log.Warning(message);
                break;

            case LogSeverity.Error:
                Serilog.Log.Error(message);
                break;

            case LogSeverity.Critical:
                Serilog.Log.Fatal(message);
                break;
        }
    }
}
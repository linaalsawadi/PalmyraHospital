using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Logging.Enums;

namespace PalmyraHospital.Infrastructure.Logging.Abstractions;

public interface ILogService
{
    void WriteLog(
        string message,
        LogSeverity severity);
}
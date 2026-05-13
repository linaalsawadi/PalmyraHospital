using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PalmyraHospital.Infrastructure.Logging.Configurations;

public class LoggingOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string TableName { get; set; } = "Logs";
}
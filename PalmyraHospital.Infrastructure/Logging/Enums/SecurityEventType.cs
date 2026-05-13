using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PalmyraHospital.Infrastructure.Logging.Enums;

public enum SecurityEventType
{
    LoginSuccess = 1,
    LoginFailed = 2,
    UnauthorizedAccess = 3,
    PermissionDenied = 4,
    SuspiciousRequest = 5,
    SqlInjectionAttempt = 6,
    JwtManipulation = 7
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PalmyraHospital.Infrastructure.Logging.Enums;

public enum AuditActionType
{
    Create = 1,
    Update = 2,
    Delete = 3,
    AssignRole = 4,
    RemoveRole = 5
}
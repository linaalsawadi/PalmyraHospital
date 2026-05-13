using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalmyraHospital.Infrastructure.Logging.Models;

namespace PalmyraHospital.Infrastructure.Logging.Repositories;

public interface IRequestTraceRepository
{
    Task AddAsync(RequestTrace trace);
}
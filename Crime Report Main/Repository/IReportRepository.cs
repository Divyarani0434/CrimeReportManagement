using CrimeReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Repository
{
    public interface IReportRepository
    {
       Reports GenerateIncidentReport(Incidents incident);
    }
}

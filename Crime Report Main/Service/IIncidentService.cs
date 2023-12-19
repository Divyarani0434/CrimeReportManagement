using CrimeReport.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Service
{
    internal interface IIncidentService
    {
        void ViewIncidents(IIncidentRepository NewIncidentAnalysis);
        void ViewIncidentsTime(IIncidentRepository NewIncidentAnalysis);
        void SearchIncident(IIncidentRepository NewIncidentAnalysis);
        void AddIncident(IIncidentRepository NewIncidentAnalysis, ICaseRepository NewCaseAnalysis);
        void UpdateIncident(IIncidentRepository NewIncidentAnalysis);
        void GenerateReports(IReportRepository newReportAnalysis, IIncidentRepository NewIncidentAnalysis);
    }
}

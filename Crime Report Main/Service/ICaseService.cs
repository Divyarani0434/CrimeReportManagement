using CrimeReport.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Service
{
    internal interface ICaseService
    {
        void ViewCases(ICaseRepository NewCaseAnalysis);
        void GetCase(ICaseRepository NewCaseAnalysis);
        void CreateCase(IIncidentRepository NewIncidentAnalysis, ICaseRepository NewCaseAnalysis);
        void UpdateCase(ICaseRepository NewCaseAnalysis);

    }
}

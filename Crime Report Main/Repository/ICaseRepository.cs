using CrimeReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Repository
{
    public interface ICaseRepository
    {
        Cases CreateCase(string caseDescription, List<Incidents> relatedIncidents);
        Cases GetCaseDetails(int caseId);
        void UpdateCaseDetails(Cases caseToUpdate);
        List<Cases> GetAllCases();
        List<Incidents> GetRelatedIncidents(int caseId);
    }
}

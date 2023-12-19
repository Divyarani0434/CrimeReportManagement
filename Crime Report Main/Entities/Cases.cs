using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Entities
{
    public class Cases
    {
        public int CaseID { get; set; }
        public string CaseDescription { get; set; }
        
        public List<Incidents> RelatedIncidents { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Exceptions
{
    internal class IncidentNumberNotFoundException : Exception
    {

        public int IncidentNumber { get; }

        public IncidentNumberNotFoundException(int incidentNumber)
            : base($"Incident with ID {incidentNumber} not found.")
        {
            IncidentNumber = incidentNumber;
        }

    }
    

    
}

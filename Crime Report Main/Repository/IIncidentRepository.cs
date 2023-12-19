using CrimeReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Repository
{
    public interface IIncidentRepository
    {
         List<Incidents> GetAllIncidents();
        int CreateIncident(Incidents incident);
        //void UpdateIncidentStatus(int incidentId, string newStatus);
         List<Incidents> GetIncidentsInDateRange(DateTime startDate, DateTime endDate);
        List<Incidents> SearchIncidents(string criteria);
        // Update the status of an incident
        int  UpdateIncidentStatus(int incidentId, string newStatus);
        public Incidents GetIncidentByID(int id);
        public void UpdateIncidentOfficer(int incidentId);
    }

    
}

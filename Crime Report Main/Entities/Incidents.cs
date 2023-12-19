using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Entities
{
    public class Incidents
    {
        int incidentID;
        string incidentType;
        DateTime incidentDate;
        string location;
        string description;
        string status;
        int victimID;
        int suspectID;
        int caseID;
        public int IncidentID {  get { return incidentID; } set {  incidentID = value; } }
//        public int CaseID { get { return CaseID; } set { CaseID = value; } }
        public string IncidentType { get {  return incidentType; } set {  incidentType = value; } }
        public DateTime IncidentDate { get { return incidentDate; } set { incidentDate = value; } }
        public string Location { get { return location; } set { location = value; } }
        public string Description { get { return description; } set { description = value; } }
        public string Status { get { return status; } set { status = value; } }
        public int VictimID { get { return victimID; } set {  victimID = value; } }
        public int SuspectID { get { return suspectID; } set { suspectID = value; } }
        public int CaseID { get { return caseID; } set { caseID = value; } }

        public Incidents(int incidentID,int caseID,string incidentType,DateTime incidentDate,string location,string status,int victimID,int suspectId)
        {
            IncidentID = incidentID;
            IncidentType = incidentType;
            IncidentDate = incidentDate;
            Location = location;
            CaseID = caseID;


            Status = status;
            VictimID = victimID;
            SuspectID = suspectId;
        }
        public Incidents( int caseID, string incidentType, DateTime incidentDate, string location, string status, int victimID, int suspectId)
        {
            
            IncidentType = incidentType;
            IncidentDate = incidentDate;
            Location = location;
            CaseID = caseID;


            Status = status;
            VictimID = victimID;
            SuspectID = suspectId;
        }

        public Incidents()
        {
        }

        public override string ToString()
        {
            return $"incidentID::{IncidentID}\t caseID::{CaseID}\t incidentType::{IncidentType}\t incidentDate::{IncidentDate}\t location::{Location}\t description::{Description}\t status::{Status}\t victimID::{VictimID}\t suspectID::{SuspectID}";

        }


    }
}

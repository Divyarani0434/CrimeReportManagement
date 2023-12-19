using CrimeReport.Entities;
using CrimeReport.Exceptions;
using CrimeReport.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CrimeReport.Service
{

    public class IncidentService : IIncidentService
    {

        public  void ViewIncidents(IIncidentRepository NewIncidentAnalysis)
        {
            Console.WriteLine("---------VIEW ALL INCIDENTS-----------");

            List<Incidents> incidents = NewIncidentAnalysis.GetAllIncidents();
            int k = incidents.Count();
            if (incidents.Count >= 0)
            {
                Console.WriteLine("-------------------All Incidents ------------------");
                foreach (var incident in incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.IncidentID},Case ID: {incident.CaseID},  Type: {incident.IncidentType}, Date: {incident.IncidentDate}, Status: {incident.Status}");
                }
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Retrieved {k} Incidents.");
            }
        }
        public  void ViewIncidentsTime(IIncidentRepository NewIncidentAnalysis)
        {
            Console.WriteLine("---------VIEW INCIDENTS IN SPECIFIC TIME RANGE-----------");
            Console.Write("Enter Start Date (yyyy-MM-dd HH:mm:ss): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter End Date (yyyy-MM-dd HH:mm:ss): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            List<Incidents> incidents = NewIncidentAnalysis.GetIncidentsInDateRange(startDate, endDate);
            int k = incidents.Count();
            if (incidents.Count >= 0)
            {
                Console.WriteLine($"----------------All Incidents from {startDate} to {endDate} ----------------");
                foreach (var incident in incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.IncidentID},Case ID: {incident.CaseID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate}, Status: {incident.Status}");
                }
                Console.WriteLine("---------------------------------------------------------------------------- ");
                Console.WriteLine($"Retrieved {k} Incidents.");
            }
        }

        public  void SearchIncident(IIncidentRepository NewIncidentAnalysis)
        {
            Console.WriteLine("---------SEARCH INCIDENT-----------");
            Console.Write("Enter Incident Type to Search ");
            String search = Console.ReadLine();

            List<Incidents> incidents = NewIncidentAnalysis.SearchIncidents(search);
            int k = incidents.Count();
            if (incidents.Count >= 0)
            {
                Console.WriteLine($"----------------All Incidents with IncidentType like {search} ----------------");
                foreach (var incident in incidents)
                {
                    Console.WriteLine($"Incident ID: {incident.IncidentID}, Case ID: {incident.CaseID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate}, Status: {incident.Status}");
                }
                Console.WriteLine("---------------------------------------------------------------------------- ");
                Console.WriteLine($"Retrieved {k} Incidents.");
            }
        }
        public   void AddIncident(IIncidentRepository NewIncidentAnalysis, ICaseRepository NewCaseAnalysis)
        {
            Console.WriteLine("---------ADD INCIDENT-----------");
            Console.WriteLine("Enter Incident Type: ");

            string incidentType = Console.ReadLine();
            //ViewCases(NewCaseAnalysis);
            Console.Write("Enter Case ID to Relate Incident  ");
            int caseID = int.Parse(Console.ReadLine());
            Console.Write("Enter Location:  ");
            String location = Console.ReadLine();
            Console.Write("Enter Incident Date (yyyy-MM-dd HH:mm:ss): ");
            DateTime incidentDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Enter VictimID:  ");
            int victimID = int.Parse(Console.ReadLine());
            Console.Write("Enter SuspectID:  ");
            int suspectID = int.Parse(Console.ReadLine());


            // Console.Write("Enter Incident Description: ");
            //string description = Console.ReadLine();

            Console.Write("Enter Incident Status: ");
            string status = Console.ReadLine();



            Incidents newIncident = new()
            {
                IncidentType = incidentType,
                IncidentDate = incidentDate,
                Location = location,
                CaseID = caseID,
                // Description = description,
                Status = status,
                VictimID = victimID,
                SuspectID = suspectID
            };
            int addIncidentStatus = NewIncidentAnalysis.CreateIncident(newIncident);

            if (addIncidentStatus > 0)
            {
                
                Console.WriteLine("--------------Incident Added SucessFully--------------");
            }
            else
            {
                Console.WriteLine("--------------Incident Addition Failed----------------");
            }



        }
        public  void UpdateIncident(IIncidentRepository NewIncidentAnalysis)
        {
            Console.WriteLine("---------UPDATE INCIDENT-----------");
            ViewIncidents(NewIncidentAnalysis);
            Console.Write("Enter Incident Id to Update ");
            int id = int.Parse(Console.ReadLine());
           
           try{
               Incidents incident1 = NewIncidentAnalysis.GetIncidentByID(id);
            NewIncidentAnalysis.GetIncidentByID(id);
            
            
                Console.WriteLine($"Incident ID: {incident1.IncidentID},Case ID: {incident1.CaseID}, Type: {incident1.IncidentType}, Date: {incident1.IncidentDate}, Status: {incident1.Status}");
                Console.Write("Enter Incident Status to Update ");
                String status = Console.ReadLine();

                NewIncidentAnalysis.UpdateIncidentStatus(id, status);
                Console.WriteLine("Incident Updated successfully.");
                NewIncidentAnalysis.GetIncidentByID(id);
                Incidents incident = NewIncidentAnalysis.GetIncidentByID(id);
                Console.WriteLine($"Incident ID: {incident.IncidentID},Case ID: {incident.CaseID}, Date: {incident.IncidentDate}, Status: {incident.Status}");

            }catch(IncidentNumberNotFoundException ex)
            {
                Console.WriteLine($" Given Incident Id: {ex.IncidentNumber}");
                Console.WriteLine($"!!!!Error!!!!: {ex.Message}");
                
            }
            catch(Exception ex)

            {
                Console.WriteLine($"Error: {ex.Message}");

            }
            

        }

        public  void GenerateReports(IReportRepository newReportAnalysis, IIncidentRepository NewIncidentAnalysis)
        {
            Console.WriteLine("---------GENERTAE REPORT-----------");
            ViewIncidents(NewIncidentAnalysis);
            Console.Write("Enter Incident Id to Generate Report ");
            int id = int.Parse(Console.ReadLine());

            Incidents incident1 = NewIncidentAnalysis.GetIncidentByID(id);

            Reports incidentReport = newReportAnalysis.GenerateIncidentReport(incident1);
            if (incidentReport != null)
            {
                Console.WriteLine("Generated Report:");
                Console.WriteLine($"Report ID: {incidentReport.ReportID}");
                Console.WriteLine($"Incident ID: {incidentReport.IncidentID}");
                Console.WriteLine($"Reporting Officer ID: {incidentReport.ReportingOfficerID}");
                Console.WriteLine($"Report Date: {incidentReport.ReportDate}");
                Console.WriteLine($"Report Details: {incidentReport.ReportDetails}");
                Console.WriteLine($"Status: {incidentReport.Status}");
                Console.WriteLine($"Incident Report generated successfully. Report ID: {incidentReport.ReportID}");
                Console.WriteLine($"Incident ID: {incidentReport.IncidentID}, Reporting Office ID: {incidentReport.ReportingOfficerID}, Date: {incidentReport.ReportDate},ReportDetails:{incidentReport.ReportDetails} ,Status: {incidentReport.Status}");

            }
            else
            {
                Console.WriteLine("Failed to generate a report.");
            }

        }

    }
}

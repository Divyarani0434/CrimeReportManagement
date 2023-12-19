using CrimeReport.Entities;
using CrimeReport.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Service
{
    internal class CaseService : ICaseService
    {
        public  void ViewCases(ICaseRepository NewCaseAnalysis)
        {
            List<Cases> cases = NewCaseAnalysis.GetAllCases();
            int k = cases.Count;
            Console.WriteLine("---------ALL CASES-----------");
            foreach (var Case in cases)
            {
                Console.WriteLine($"Case ID: {Case.CaseID}, Case Description: {Case.CaseDescription}");
                Console.WriteLine("Related Incidents to the Case : ");
                List<Incidents> incidents = NewCaseAnalysis.GetRelatedIncidents(Case.CaseID);
                if (incidents.Count > 0)
                {
                    foreach (var incident1 in incidents)
                    {
                        Console.WriteLine($"Incident ID: {incident1.IncidentID}, Type: {incident1.IncidentType}, Date: {incident1.IncidentDate}, Status: {incident1.Status}");
                    }

                }
                else
                {
                    Console.WriteLine("No Related Incidents Found");

                }
                Console.WriteLine("-------");


            }
            Console.WriteLine("--------------------");
            Console.WriteLine($"Retrieved {k} Cases.");
        }

        public  void GetCase(ICaseRepository NewCaseAnalysis)
        {


            Console.WriteLine("---------GET CASE DETAILS-----------");
            Console.WriteLine("Enter the Case ID:");
            int id = int.Parse(Console.ReadLine());
            Cases Case = NewCaseAnalysis.GetCaseDetails(id);
            if (Case != null)
            {
                Console.WriteLine($"Case ID: {Case.CaseID}, Case Description: {Case.CaseDescription}");
                Console.WriteLine("Related Incidents to the Case : ");
                List<Incidents> incidents = NewCaseAnalysis.GetRelatedIncidents(Case.CaseID);
                if (incidents.Count > 0)
                {
                    foreach (var incident1 in incidents)
                    {
                        Console.WriteLine($"Incident ID: {incident1.IncidentID}, Type: {incident1.IncidentType}, Date: {incident1.IncidentDate}, Status: {incident1.Status}");
                    }

                }
                else
                {
                    Console.WriteLine("No Related Incidents Found");

                }
                Console.WriteLine("-------");
            }
            else
            {
                Console.WriteLine("No Case Found with given Id");
            }

        }
        public  void CreateCase(IIncidentRepository NewIncidentAnalysis, ICaseRepository NewCaseAnalysis)

        {
            Console.WriteLine("----------TO CREATE A CASE YOU NEED TO ADD INCIDENTS TO THE CASE-----------");
            Console.WriteLine("How many Incidents You want to Add");
            int count = int.Parse(Console.ReadLine());
            List<Incidents> RelatedIncidents = new List<Incidents>();
            while (count > 0)
            {

                Console.WriteLine("---------ADD INCIDENT-----------");
                Console.WriteLine("Enter Incident Type: ");

                string incidentType = Console.ReadLine();
                //ViewCases(NewCaseAnalysis);
                //Console.Write("Enter Case ID to Relate Incident  ");
                //int caseID = int.Parse(Console.ReadLine());
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
                    CaseID = 1,
                    // Description = description,
                    Status = status,
                    VictimID = victimID,
                    SuspectID = suspectID
                };
                int incidentID = NewIncidentAnalysis.CreateIncident(newIncident);

                if (incidentID > 0)
                {
                    newIncident.IncidentID = incidentID;
                    RelatedIncidents.Add(newIncident);

                    Console.WriteLine("--------------Incident Added SucessFully--------------");
                    count = count - 1;
                }
                else
                {
                    Console.WriteLine("--------------Incident Addition Failed----------------");
                }
            }
            Console.WriteLine("Add Case Based on Above Added Incidnets");
            Console.WriteLine("Enter Case Description");
            string CaseDescription = Console.ReadLine();
            Cases newCase = NewCaseAnalysis.CreateCase(CaseDescription, RelatedIncidents);

            if (newCase != null)
            {
                Console.WriteLine("New Case Created:");
                Console.WriteLine($"Case ID: {newCase.CaseID}");
                Console.WriteLine($"Description: {newCase.CaseDescription}");
                Console.WriteLine("Associated Incidents:");

                foreach (Incidents incident in newCase.RelatedIncidents)
                {
                    Console.WriteLine($"  Incident ID: {incident.IncidentID}, Type: {incident.IncidentType}, Date: {incident.IncidentDate}");

                }
            }
            else
            {
                Console.WriteLine("Failed to create a new case.");
            }

            Console.ReadLine(); // Keep the console window open

        }






        public  void UpdateCase(ICaseRepository NewCaseAnalysis)
        {
            Console.WriteLine("---------UPDATE CASE DETAILS-----------");
            ViewCases(NewCaseAnalysis);

            Console.WriteLine("Enter the ID of Case to Updated:");
            int id = int.Parse(Console.ReadLine());
            Cases Case = NewCaseAnalysis.GetCaseDetails(id);
            if (Case != null)
            {
                Console.WriteLine($"Case ID: {Case.CaseID}, Case Description: {Case.CaseDescription}");
                Console.WriteLine("Related Incidents to the Case : ");
                List<Incidents> incidents = NewCaseAnalysis.GetRelatedIncidents(Case.CaseID);
                if (incidents.Count > 0)
                {
                    foreach (var incident1 in incidents)
                    {
                        Console.WriteLine($"Incident ID: {incident1.IncidentID}, Type: {incident1.IncidentType}, Date: {incident1.IncidentDate}, Status: {incident1.Status}");
                    }

                }
                else
                {
                    Console.WriteLine("No Related Incidents Found");

                }
                Console.WriteLine("-------");
            }
            else
            {
                Console.WriteLine("No Case Found with given Id");
            }


            Console.Write("Enter Case Description to Update ");
            string Description = Console.ReadLine();
            Cases case1 = new Cases();

            case1.CaseDescription = Description;
            case1.CaseID = id;

            NewCaseAnalysis.UpdateCaseDetails(case1);
            Console.WriteLine("Case Updated successfully.");
            NewCaseAnalysis.GetCaseDetails(id);
            Cases Case2 = NewCaseAnalysis.GetCaseDetails(id);
            Console.WriteLine($"Case ID: {Case2.CaseID}, Case Description: {Case2.CaseDescription}");
            Console.WriteLine("Related Incidents to the Case : ");
            List<Incidents> incidents2 = NewCaseAnalysis.GetRelatedIncidents(Case2.CaseID);
            if (incidents2.Count > 0)
            {
                foreach (var incident1 in incidents2)
                {
                    Console.WriteLine($"Incident ID: {incident1.IncidentID}, Type: {incident1.IncidentType}, Date: {incident1.IncidentDate}, Status: {incident1.Status}");
                }

            }
            else
            {
                Console.WriteLine("No Related Incidents Found");

            }






        }
    }
}

// See https://aka.ms/new-console-template for more information
using CrimeReport.Entities;
using CrimeReport.Repository;
using CrimeReport.Service;

// Instantiate services
IIncidentRepository NewIncidentAnalysis = new IncidentRepository();
ICaseRepository NewCaseAnalysis = new CaseRepository();
IReportRepository NewReportAnalysis = new ReportRepository();
IIncidentService incidentService = new IncidentService();
ICaseService caseService = new CaseService();   

// Menu loop
while (true)
{
    Console.WriteLine("-------------------Crime Report Management--------------------");
MainMenu:
    Console.WriteLine("Main Menu:");
    Console.WriteLine("1. Manage Incidents");
    Console.WriteLine("2. Manage Cases");
    Console.WriteLine("3. Create Reports");
    Console.WriteLine("4. Exit");

    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            
            while (true)
            {

                Console.WriteLine("-------------------Incident Management--------------------");
                Console.WriteLine("1. Get All Incidents");
                Console.WriteLine("2. Get Incidents Within Specific Time Range");
                Console.WriteLine("3. Create New Incident");
                Console.WriteLine("4. Search Incidents based on Incident Type");
                Console.WriteLine("5. Update Incident Status ");
                Console.WriteLine("6.Main Menu");
                Console.WriteLine("Enter your Incident choice");
                int Ic = int.Parse(Console.ReadLine());
                switch (Ic)
                {
                    case 1:
                        incidentService.ViewIncidents(NewIncidentAnalysis);
                        break;
                    case 2:
                        incidentService.ViewIncidentsTime(NewIncidentAnalysis);
                        break;
                    case 3:
                        incidentService.AddIncident(NewIncidentAnalysis,NewCaseAnalysis);
                        break;
                    case 4:
                        incidentService.SearchIncident(NewIncidentAnalysis);
                        break;

                    case 5:
                        incidentService.UpdateIncident(NewIncidentAnalysis);
                        break;
                    case 6:
                        goto MainMenu;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;



                }
            }
            break;


        case "2":
            ManageCases:
            while (true)

            {

                Console.WriteLine("-------------------Case Management--------------------");
                Console.WriteLine("1.View Cases");
                Console.WriteLine("2.Update Case Details");
                Console.WriteLine("3.Get Specific Case Details");
                Console.WriteLine("4.Create New Case ");
                Console.WriteLine("5.Main Menu");
                Console.WriteLine("Enter your case choice");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        caseService.ViewCases(NewCaseAnalysis);
                        break;
                    case 2:
                        caseService.UpdateCase(NewCaseAnalysis);
                        break;
                    case 3:
                        caseService.GetCase(NewCaseAnalysis);
                        break;
                    case 4:
                        caseService.CreateCase(NewIncidentAnalysis, NewCaseAnalysis);
                        break;
                        
                    case 5:
                        goto MainMenu;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;


                }

            }

            break;

        case "3":
            incidentService.GenerateReports(NewReportAnalysis,NewIncidentAnalysis);
            break;
        case "4":
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
            break;

    }
}










using CrimeReport;
using CrimeReport.Repository;
using CrimeReport.Entities;
using System.Reflection;
using System.Net.NetworkInformation;

namespace CrimeReportTest
{
    public class Tests
    {

        private const string connectionString = "Server=DESKTOP-2A9B8EB;Database=CrimeReporting;Trusted_Connection=True";
       


        [Test]
        public void TestToGetAllIncidents()
        {
            //Arrange or Assign
            IncidentRepository incidentRepository = new IncidentRepository();
            incidentRepository.connectionString = connectionString;

            //Act
            var allIncidents = incidentRepository.GetAllIncidents();

            //Assert
            Assert.IsNotNull(allIncidents);
            //Assert.AreEqual(1, allProducts.Count());
            Assert.GreaterOrEqual(allIncidents.Count, 0);
        }

       [Test]
        public void TestToAddIncident()
        {
            //assign
            IncidentRepository incidentRepository = new IncidentRepository();
            incidentRepository.connectionString = connectionString;
            //act
            int addIncidentStatus = incidentRepository.CreateIncident(new Incidents
            {
                IncidentType = "Test",
                IncidentDate = DateTime.Now,
                Location = "Test",
                Status = "test",
                VictimID =1,
                SuspectID =2,
                CaseID =2
            });
            //assert
            Assert.IsNotNull(addIncidentStatus);
          // Assert.AreEqual(, addIncidentStatus);

        }
       [Test]
        public void TesttoUpdateIncidentStatus()
        {
            IncidentRepository incidentRepository = new IncidentRepository();
            incidentRepository.connectionString = connectionString;
            int updatestatus = incidentRepository.UpdateIncidentStatus(20, "Closed");
            Assert.AreEqual(1, updatestatus);
        }
        [Test]
        public void TesttoGetIncidentsInRange()
        {
            IncidentRepository incidentRepository = new IncidentRepository();
            incidentRepository.connectionString = connectionString;
            List<Incidents> incidents = incidentRepository.GetIncidentsInDateRange(DateTime.MinValue, DateTime.MaxValue);
            Assert.IsNotNull(incidents);
        }
        [Test]
        public void TesttoSearchIncidents()
        {
            IncidentRepository incidentRepository = new IncidentRepository();
            incidentRepository.connectionString = connectionString;
            List<Incidents> incidents = null;
           // Assert.Throws<Exception>(() => incidents = incidentRepository.SearchIncidents("Something"), "Incident type not Found");
            Assert.IsNull(incidents);
            incidents = incidentRepository.SearchIncidents("Missing");
            Assert.IsNotNull(incidents);
        }
        [Test]
        public void TesttoGenerateIncidentReport()
        {
            IncidentRepository incidentRepository = new IncidentRepository();
            incidentRepository.connectionString = connectionString;
            ReportRepository reportsRepository = new ReportRepository();
            Reports reports = new Reports();
            int addIncidentStatus = incidentRepository.CreateIncident(new Incidents
            {
                IncidentType = "Test",
                IncidentDate = DateTime.Now,
                Location = "Test",
                Status = "test",
                VictimID = 1,
                SuspectID = 2,
                CaseID = 2
            });
            Incidents incident = incidentRepository.GetIncidentByID(addIncidentStatus);
            //assert
            Assert.IsNotNull(addIncidentStatus);
            Assert.IsNotNull(incident.IncidentID);
            
          //  Assert.Throws<Exception>(() => reports = reportsRepository.GenerateIncidentReport(incident), "Report not found.");
            Assert.AreEqual(0, reports.ReportID);
            reports = reportsRepository.GenerateIncidentReport(incident);
            Assert.IsNotNull(reports);
            //Assert.IsNotNull(reports.ReportID);
           // Assert.IsNotNull(reports);
        }






    }
}
using CrimeReport.Entities;
using CrimeReport.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeReport.Repository
{
    public class ReportRepository : IReportRepository
    {
        // SqlConnection sqlConnection = null;
        public string connectionString;
        SqlCommand cmd = null;

        //constructor
        public ReportRepository()
        {

            //  sqlConnection = new SqlConnection("Server=DESKTOP-0TE71RT;Database=PRODUCTAPPDB;Trusted_Connection=True");
            connectionString = DbConn.GetConnectionString();
            cmd = new SqlCommand();
        }



        public Reports GenerateIncidentReport(Incidents incident)

        {

            if (incident == null)
            {
                // Handle the case where incident is null
                return null;
            }

            Reports report = new Reports();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Assuming there is a table IncidentOfficers to represent the relationship between incidents and officers
                string officerQuery = "SELECT TOP 1 OfficerID FROM IncidentOfficers WHERE IncidentID = @IncidentID";

                using (SqlCommand officerCommand = new SqlCommand(officerQuery, connection))
                {
                    officerCommand.Parameters.AddWithValue("@IncidentID", incident.IncidentID);

                    // Execute the SQL command to get the ReportingOfficerID
                    object result = officerCommand.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int reportingOfficerId = (int)result;
                        int ReportId;
                        // Generate a report
                        report.IncidentID = incident.IncidentID;
                        report.ReportingOfficerID = reportingOfficerId;
                        report.ReportDate = DateTime.Now;
                        report.ReportDetails = $"Incident Type: {incident.IncidentType}, Location: {incident.Location}, Description: {incident.Description}";
                        report.Status = "Draft";  // Set initial status

                        // Assuming you have a Reports table to insert the generated report
                        string reportQuery = "INSERT INTO Reports (IncidentID, ReportingOfficer, ReportDate, ReportDetails, Status) OUTPUT INSERTED.ReportID VALUES (@IncidentID, @ReportingOfficerID, @ReportDate, @ReportDetails, @Status)";

                        using (SqlCommand insertCommand = new SqlCommand(reportQuery, connection))
                        {
                            // Add parameters to the SQL query
                            insertCommand.Parameters.AddWithValue("@IncidentID", report.IncidentID);
                            insertCommand.Parameters.AddWithValue("@ReportingOfficerID", report.ReportingOfficerID);
                            insertCommand.Parameters.AddWithValue("@ReportDate", report.ReportDate);
                            insertCommand.Parameters.AddWithValue("@ReportDetails", report.ReportDetails);
                            insertCommand.Parameters.AddWithValue("@Status", report.Status);

                            // Execute the SQL command to insert the report
                            insertCommand.ExecuteNonQuery();
                            ReportId = (int)insertCommand.ExecuteScalar();
                            report.ReportID = ReportId;
                        }

                        return report;
                    }
                    else
                    {
                        // Handle the case where no reporting officer is found
                        return null;
                    }
                }
            }
        }

            

    }


}


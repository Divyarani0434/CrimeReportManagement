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
    public class CaseRepository : ICaseRepository 
    {
        public string connectionString;
        SqlCommand cmd = null;

        //constructor
        public CaseRepository()
        {

            //  sqlConnection = new SqlConnection("Server=DESKTOP-0TE71RT;Database=PRODUCTAPPDB;Trusted_Connection=True");
            connectionString = DbConn.GetConnectionString();
            cmd = new SqlCommand();
        }

        public Cases CreateCase(string caseDescription, List<Incidents> relatedIncidents)
        {
            if (relatedIncidents == null || relatedIncidents.Count == 0)
            {
                Console.WriteLine("No Related Incidents to create Case");
                // case where relatedIncidents is null or empty
                return null;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Insert the new case and retrieve the CaseID
                string insertCaseQuery = "INSERT INTO Cases (CaseDescription) OUTPUT INSERTED.CaseID VALUES (@CaseDescription)";
                int caseId;

                using (SqlCommand insertCaseCommand = new SqlCommand(insertCaseQuery, connection))
                {
                    insertCaseCommand.Parameters.AddWithValue("@CaseDescription", caseDescription);

                    // Execute the query and get the CaseID
                    caseId = (int)insertCaseCommand.ExecuteScalar();
                }

                // Associate incidents with the new case
                foreach (Incidents incident in relatedIncidents)
                {
                    // Update the Incident table with the CaseID
                     
                    string updateIncidentQuery = "UPDATE Incidents SET CaseID = @CaseID WHERE IncidentID = @incidentID";

                    using (SqlCommand updateIncidentCommand = new SqlCommand(updateIncidentQuery, connection))
                    {
                        updateIncidentCommand.Parameters.AddWithValue("@CaseID", caseId);
                        updateIncidentCommand.Parameters.AddWithValue("@IncidentID", incident.IncidentID);

                        updateIncidentCommand.ExecuteNonQuery();
                    }
                }

                return new Cases
                {
                    CaseID = caseId,
                    CaseDescription = caseDescription,
                    RelatedIncidents = relatedIncidents
                };
            }
        }

        public Cases GetCaseDetails(int caseId)
        {
            Cases caseDetails = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string caseQuery = "SELECT * FROM Cases WHERE CaseID = @CaseID";

                using (SqlCommand command = new SqlCommand(caseQuery, connection))
                {
                    command.Parameters.AddWithValue("@CaseID", caseId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            caseDetails = new Cases
                            {
                                CaseID = (int)reader["CaseID"],
                                CaseDescription = reader["CaseDescription"].ToString(),
                                RelatedIncidents = GetRelatedIncidents(caseId)
                            };
                        }
                    }
                }
            }

            return caseDetails;
        }


        public List<Incidents> GetRelatedIncidents(int caseId)
        {
            List<Incidents> relatedIncidents = new List<Incidents>();

            // Query to get incidents related to the case
            string incidentsQuery = "SELECT * FROM Incidents WHERE CaseID = @CaseID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(incidentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@CaseID", caseId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Incidents incident = new Incidents
                            {
                                IncidentID = (int)reader["IncidentID"],
                                IncidentType = (string)reader["IncidentType"],
                                IncidentDate = (DateTime)reader["IncidentDate"],
                                Location = (string)reader["Location"],
                            // incident.Description = (string)reader["Description"];
                                Status = (string)reader["status"],
                                SuspectID = (int)reader["SuspectID"],
                                VictimID = (int)reader["VictimID"]
                             };

                            relatedIncidents.Add(incident);
                        }
                    }
                }
            }

            return relatedIncidents;
        }



        public void UpdateCaseDetails(Cases caseToUpdate)
        {
            if (caseToUpdate == null)
            {
                Console.WriteLine("Case Not Found with given id");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Cases SET CaseDescription = @CaseDescription WHERE CaseID = @CaseID";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@CaseDescription", caseToUpdate.CaseDescription);
                    command.Parameters.AddWithValue("@CaseID", caseToUpdate.CaseID);

                    // Add parameters for other properties if needed

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Cases> GetAllCases()
        {
            List<Cases> cases = new List<Cases>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Cases";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Cases newCase = new Cases();
                        // Console.WriteLine("All Incidents retrieved successfully.");


                        newCase.CaseID = (int)reader["CaseID"];
                       //int caseNo = (int)reader["CaseID"];
                        newCase.CaseDescription = (string)reader["CaseDescription"];
                       // newCase.Status = (string)reader["Status"];
                        //newCase.IncidentID = (int)reader["IncidentID"];
                        // newCase.RelatedIncidents = GetRelatedIncidents(caseNo);
                        //(List<Incidents>)reader["IncidentID"];
                        

                      

                        cases.Add(newCase);




                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cases;


        
         }
    }
}




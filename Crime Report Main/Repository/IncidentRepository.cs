using CrimeReport.Entities;
using CrimeReport.Exceptions;
using CrimeReport.Utility;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CrimeReport.Repository
{
    public class IncidentRepository: IIncidentRepository
    {

        // SqlConnection sqlConnection = null;
        public string connectionString;
        SqlCommand cmd = null;

        //constructor
        public IncidentRepository()
        {

          //  sqlConnection = new SqlConnection("Server=DESKTOP-0TE71RT;Database=PRODUCTAPPDB;Trusted_Connection=True");
            connectionString = DbConn.GetConnectionString();
            cmd = new SqlCommand();
        }
       

       
        public int CreateIncident(Incidents incident)



        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))

            {
                cmd.CommandText = "insert into Incidents OUTPUT INSERTED.IncidentID values(@IncidentType,@IncidentDate,@Location,@Status,@VictimID,@SuspectID,@CaseID)";
                cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
                cmd.Parameters.AddWithValue("@IncidentDate", incident.IncidentDate);
                cmd.Parameters.AddWithValue("@Location", incident.Location);
                //cmd.Parameters.AddWithValue("@Description", incident.Description);
                cmd.Parameters.AddWithValue("@Status", incident.Status);
                 cmd.Parameters.AddWithValue("@VictimID", incident.VictimID);
               cmd.Parameters.AddWithValue("@SuspectID", incident.SuspectID);
                cmd.Parameters.AddWithValue("@CaseID", incident.CaseID);
                
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                int incidentID =( int)(cmd.ExecuteScalar());
                int addIncidentStatus= cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                if(incidentID >0)
                {
                    UpdateIncidentOfficer(incidentID);
                }
                return incidentID;
            }

        }
        public void UpdateIncidentOfficer(int incidentId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string query = "insert into IncidentOfficers  values(@IncidentID,3)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                       
                        command.Parameters.AddWithValue("@IncidentID", incidentId);


                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       



        public  List<Incidents> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
            
        {
            List<Incidents> incidents = new List<Incidents>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                 
                    cmd.CommandText = "select * from Incidents where IncidentDate >= @startDate AND IncidentDate <= @endDate";
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    cmd.Connection = sqlConnection;

                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Console.WriteLine("All Incidents retrieved successfully.");
                        Incidents incident = new Incidents();

                        incident.IncidentID = (int)reader["IncidentID"];
                        incident.IncidentType = (string)reader["IncidentType"];
                         incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Location = (string)reader["Location"];
                       // incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["status"];
                        incident.SuspectID = (int)reader["SuspectID"];
                        incident.VictimID = (int)reader["VictimID"];
                        incident.CaseID = (int)reader["CaseID"];
                      
                       
                    
                        incidents.Add(incident);



                        
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return incidents;

            
        }
        public List<Incidents> GetAllIncidents()
        {
            List<Incidents> incidents = new List<Incidents>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "select * from Incidents";
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                   
                    while (reader.Read())
                    {
                       // Console.WriteLine("All Incidents retrieved successfully.");
                        Incidents incident = new Incidents();

                        incident.IncidentID = (int)reader["IncidentID"];
                        incident.IncidentType = (string)reader["IncidentType"];
                        incident.IncidentDate = (DateTime)reader["IncidentDate"];
                        incident.Location = (string)reader["Location"];
                       // incident.Description = (string)reader["Description"];
                        incident.Status = (string)reader["status"];
                        incident.SuspectID = (int)reader["SuspectID"];
                        incident.VictimID = (int)reader["VictimID"];
                        incident.CaseID = (int)reader["CaseID"];

                        incidents.Add(incident);

                        

                       
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return incidents;



            
            
        }


        public List<Incidents> SearchIncidents(string criteria)
        {
            List<Incidents> result = new List<Incidents>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Incidents WHERE IncidentType like @IncidentType";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IncidentType", "%" + criteria + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               Incidents incident = new Incidents();
                                incident.IncidentID = (int)reader["IncidentID"];
                                incident.IncidentType = (string)reader["IncidentType"];
                                incident.IncidentDate = (DateTime)reader["IncidentDate"];
                                incident.Location = (string)reader["Location"];
                                // incident.Description = (string)reader["Description"];
                                incident.Status = (string)reader["status"];
                                incident.SuspectID = (int)reader["SuspectID"];
                                incident.VictimID = (int)reader["VictimID"];
                                incident.CaseID = (int)reader["CaseID"];

                                result.Add(incident);
                            };

                            
                        }
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public int UpdateIncidentStatus(int incidentId, string newStatus)
        {
            int result = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string query = "UPDATE Incidents SET Status = @NewStatus WHERE IncidentID = @IncidentID";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                       
                        command.Parameters.AddWithValue("@NewStatus", newStatus);
                        command.Parameters.AddWithValue("@IncidentID", incidentId);

                       
                        command.ExecuteNonQuery();
                        result = 1;

                        
                    }
                }

            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
                }
            return result;
        }

        public Incidents GetIncidentByID(int id)
        {
            Incidents result = null;
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Incidents WHERE IncidentID = @incidentId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IncidentID",id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Incidents incident = new Incidents();
                                incident.IncidentID = (int)reader["IncidentID"];
                                incident.IncidentType = (string)reader["IncidentType"];
                                incident.IncidentDate = (DateTime)reader["IncidentDate"];
                                incident.Location = (string)reader["Location"];
                                // incident.Description = (string)reader["Description"];
                                incident.Status = (string)reader["status"];
                                incident.SuspectID = (int)reader["SuspectID"];
                                incident.VictimID = (int)reader["VictimID"];
                                incident.CaseID = (int)reader["CaseID"];

                                result =incident;
                            };


                        }
                    }
                
                 }
                if(result != null) {
                return result;
            }
            else
            {
                throw new  IncidentNumberNotFoundException(id);
            }
            

            
        }





    }
}

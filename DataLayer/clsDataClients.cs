using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class ClientsDTO
    {
        public ClientsDTO(int ClientID, int PersonID, int VehicalLicenseNumber, DateTime LicenseExpirationDate, DateTime AccountCreationDate, int CreatedByUserID)
        {
            this.ClientID = ClientID;
            this.PersonID = PersonID;
            this.VehicalLicenseNumber = VehicalLicenseNumber;
            this.LicenseExpirationDate = LicenseExpirationDate;
            this.AccountCreationDate = AccountCreationDate;
            this.CreatedByUserID = CreatedByUserID;


        }
        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public int VehicalLicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public int CreatedByUserID { get; set; }
        
    }
        public class clsDataClients
    {
        public static int AddNewClient(ClientsDTO clientDTO)
        {
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewClient", Connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@PersonID", clientDTO.PersonID);
                        command.Parameters.AddWithValue("@VehicalLicenseNumber", clientDTO.VehicalLicenseNumber);
                        command.Parameters.AddWithValue("@LicenseExpirationDate", clientDTO.LicenseExpirationDate);
                        command.Parameters.AddWithValue("@AccountCreationDate", clientDTO.AccountCreationDate);
                        command.Parameters.AddWithValue("@CreatedByUserID", clientDTO.CreatedByUserID);
                       

                        var outputIdParam = new SqlParameter("@NewClientID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);
                        Connection.Open();
                        command.ExecuteNonQuery();

                        return (int)outputIdParam.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return 0;
                    }
                }
            }
        }

        public static bool UpdateClient(ClientsDTO clientDTO)
        {

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_UpdateClient", Connection))
            {
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", clientDTO.ClientID);
                    command.Parameters.AddWithValue("@PersonID", clientDTO.PersonID);
                    command.Parameters.AddWithValue("@VehicalLicenseNumber", clientDTO.VehicalLicenseNumber);
                    command.Parameters.AddWithValue("@LicenseExpirationDate", clientDTO.LicenseExpirationDate);
                    command.Parameters.AddWithValue("@AccountCreationDate", clientDTO.AccountCreationDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", clientDTO.CreatedByUserID);

                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }

        public static bool DeleteClientByID(int ClientID)
        {
            using (var connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_DeleteClient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientID);
                connection.Open();

                int rowsAffected = (int)command.ExecuteNonQuery();
                return (rowsAffected == 1);


            }
        }


        public static ClientsDTO GetClientInfoByClientID(int ClientID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetClientByClientID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@ClientID", ClientID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new ClientsDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetInt32(reader.GetOrdinal("VehicalLicenseNumber")),
                                  reader.GetDateTime(reader.GetOrdinal("LicenseExpirationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("AccountCreationDate")),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))








                               );
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }


        }

        public static ClientsDTO GetClientInfoByVehicalLicenseNumber(int VehicalLicenseNumber)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetClientByVehicalLicenseNumber", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@VehicalLicenseNumber", VehicalLicenseNumber);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new ClientsDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetInt32(reader.GetOrdinal("VehicalLicenseNumber")),
                                  reader.GetDateTime(reader.GetOrdinal("LicenseExpirationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("AccountCreationDate")),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))








                               );
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }


        }
        public static ClientsDTO GetClientInfoByPersonID(int PersonID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetClientByPersonID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@PersonID", PersonID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new ClientsDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetInt32(reader.GetOrdinal("VehicalLicenseNumber")),
                                  reader.GetDateTime(reader.GetOrdinal("LicenseExpirationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("AccountCreationDate")),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))








                               );
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }


        }


        public static List<ClientsDTO> GetAllClients()
        {
            //Create Object 
            var clientsDTO = new List<ClientsDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("GetAllClients", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Commande.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Load List StudentDTO class 
                            clientsDTO.Add(new ClientsDTO
                            (
                            reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetInt32(reader.GetOrdinal("VehicalLicenseNumber")),
                                  reader.GetDateTime(reader.GetOrdinal("LicenseExpirationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("AccountCreationDate")),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                           ));

                        }
                    }

                }
                //Return List Loaded 
                return clientsDTO;
            }
        }

        public static int IsClientExistByClientID(int ClientID)
        {
            int IsExist = 0;

            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsClientExistByClientID", Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientID", ClientID);

                        Connection.Open();

                        object result = command.ExecuteScalar();


                        if (result != DBNull.Value)
                        {
                            IsExist = Convert.ToInt32(result);
                        }
                        else
                        {
                            IsExist = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Optionally log the exception or handle accordingly
            }

            return IsExist;
        }

        public static int IsClientExistByPersonID(int PersonID)
        {
            int IsExist = 0;

            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsClientExistByPersonID", Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);

                        Connection.Open();

                        object result = command.ExecuteScalar();


                        if (result != DBNull.Value)
                        {
                            IsExist = Convert.ToInt32(result);
                        }
                        else
                        {
                            IsExist = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Optionally log the exception or handle accordingly
            }

            return IsExist;
        }


        public static int IsClientExistByVihecalLicenseNumber(int VehicalLicenseNumber)
        {
            int IsExist = 0;

            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsClientExistByVehicalLicenseNumber", Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@VehicalLicenseNumber", VehicalLicenseNumber);

                        Connection.Open();

                        object result = command.ExecuteScalar();


                        if (result != DBNull.Value)
                        {
                            IsExist = Convert.ToInt32(result);
                        }
                        else
                        {
                            IsExist = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                // Optionally log the exception or handle accordingly
            }

            return IsExist;
        }









    }
}

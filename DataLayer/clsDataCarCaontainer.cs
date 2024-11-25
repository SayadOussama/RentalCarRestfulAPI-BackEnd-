using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CarContainerDTO
    {
        public CarContainerDTO(int CarID, string CarName, string CarModel, int CarType, string EngineModel, string CarPlateNumber, decimal RentalCarPrice, string Color, int DoorsNumber, string ImagePath, int CurrentKLMT, bool IsAvailable, int ClientTakenID, int CreatedByUserID)
        {
            this.CarID = CarID;
            this.CarName = CarName;
            this.CarModel = CarModel;
            this.CarType = CarType;
            this.EngineModel = EngineModel;
            this.CarPlateNumber = CarPlateNumber;
            this.RentalCarPrice = RentalCarPrice;
            this.Color = Color;
            this.DoorsNumber = DoorsNumber;
            this.ImagePath = ImagePath;
            this.CurrentKLMT = CurrentKLMT;
            this.IsAvailable = IsAvailable;
            this.ClientTakenID = ClientTakenID;
            this.CreatedByUserID = CreatedByUserID;



        }

        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode = enMode.AddNew;

        public int CarID { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public int CarType { get; set; }

        public string EngineModel { get; set; }
        public string CarPlateNumber { get; set; }
        public decimal RentalCarPrice { get; set; }

        public string Color { get; set; }
        public int DoorsNumber { get; set; }
        public string ImagePath { get; set; }

        public int CurrentKLMT { get; set; }
        public bool IsAvailable { get; set; }
        public int ClientTakenID { get; set; }
        public int CreatedByUserID { get; set; }


    }
    public class clsDataCarCaontainer
    {


        public static int AddNewCar(CarContainerDTO carContainerDTO)
        {
            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_AddNewCar", Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@CarName", carContainerDTO.CarName);
                        command.Parameters.AddWithValue("@CarModel", carContainerDTO.CarModel);
                        command.Parameters.AddWithValue("@EngineModel", carContainerDTO.EngineModel);
                        command.Parameters.AddWithValue("@CarType", carContainerDTO.CarType);
                        command.Parameters.AddWithValue("@CarPlateNumber", carContainerDTO.CarPlateNumber);
                        command.Parameters.AddWithValue("@RentalCarPrice", carContainerDTO.RentalCarPrice);
                        command.Parameters.AddWithValue("@Color", carContainerDTO.Color);
                        command.Parameters.AddWithValue("@DoorsNumber", carContainerDTO.DoorsNumber);
                        command.Parameters.AddWithValue("@ImagePath", carContainerDTO.ImagePath);
                        command.Parameters.AddWithValue("@CurrentKLMT", carContainerDTO.CurrentKLMT);
                        command.Parameters.AddWithValue("@IsAvailable", carContainerDTO.IsAvailable);

                        // Handle nullable ClientTakenID
                        if (carContainerDTO.ClientTakenID != -1)
                            command.Parameters.AddWithValue("@ClientTakenID", carContainerDTO.ClientTakenID);
                        else
                            command.Parameters.AddWithValue("@ClientTakenID", DBNull.Value);

                        command.Parameters.AddWithValue("@CreatedByUserID", carContainerDTO.CreatedByUserID);

                        // Output parameter to return the new Car ID
                        var outputIdParam = new SqlParameter("@NewCarID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        // Open the connection and execute the command
                        Connection.Open();
                        command.ExecuteNonQuery();

                        // Return the new Car ID from the output parameter
                        return (int)outputIdParam.Value;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., to a file, a database, or other logging mechanisms)
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                return -1;  // You can adjust the return value to indicate failure, e.g., return -1 or any appropriate error code
            }
            catch (Exception ex)
            {
                // Log the general exception (e.g., to a file, a database, or other logging mechanisms)
                Console.WriteLine($"Exception: {ex.Message}");
                return -1;  // You can adjust the return value to indicate failure, e.g., return -1 or any appropriate error code
            }
        }


        public static bool UpdateCar(CarContainerDTO carContainerDTO)
        {
            try {  
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_UpdateCar", Connection))
                    {


                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CarID", carContainerDTO.CarID);
                        command.Parameters.AddWithValue("@CarName", carContainerDTO.CarName);
                        command.Parameters.AddWithValue("@CarModel", carContainerDTO.CarModel);
                        command.Parameters.AddWithValue("@EngineModel", carContainerDTO.EngineModel);
                        command.Parameters.AddWithValue("@CarType", carContainerDTO.CarType);
                        command.Parameters.AddWithValue("@CarPlateNumber", carContainerDTO.CarPlateNumber);
                        command.Parameters.AddWithValue("@RentalCarPrice", carContainerDTO.RentalCarPrice);
                        command.Parameters.AddWithValue("@Color", carContainerDTO.Color);
                        command.Parameters.AddWithValue("@DoorsNumber", carContainerDTO.DoorsNumber);
                        command.Parameters.AddWithValue("@ImagePath", carContainerDTO.ImagePath);
                        command.Parameters.AddWithValue("@CurrentKLMT", carContainerDTO.CurrentKLMT);
                        command.Parameters.AddWithValue("@IsAvailable", carContainerDTO.IsAvailable);
                        if (carContainerDTO.ClientTakenID != -1)
                            command.Parameters.AddWithValue("@ClientTakenID", carContainerDTO.ClientTakenID);
                        else
                            command.Parameters.AddWithValue("@ClientTakenID", System.DBNull.Value);

                        command.Parameters.AddWithValue("@CreatedByUserID", carContainerDTO.CreatedByUserID);


                        Connection.Open();
                        command.ExecuteNonQuery();

                        return true;
                    }
            }
            }


            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., to a file, a database, or other logging mechanisms)
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                return false;  // You can adjust the return value to indicate failure, e.g., return -1 or any appropriate error code
            }
            catch (Exception ex)
            {
                // Log the general exception (e.g., to a file, a database, or other logging mechanisms)
                Console.WriteLine($"Exception: {ex.Message}");
                return false;  // You can adjust the return value to indicate failure, e.g., return -1 or any appropriate error code
            }
        }
    
        public static bool DeleteCar(int CarID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                using (var command = new SqlCommand("SP_DeleteCar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarID", CarID);
                    connection.Open();

                    int rowsAffected = (int)command.ExecuteNonQuery();
                    return (rowsAffected == 1);


                }
            }

            catch (SqlException sqlEx)
            {
                // Log the SQL exception (e.g., to a file, a database, or other logging mechanisms)
                Console.WriteLine($"SQL Exception: {sqlEx.Message}");
                return false;  // You can adjust the return value to indicate failure, e.g., return -1 or any appropriate error code
            }
            catch (Exception ex)
            {
                // Log the general exception (e.g., to a file, a database, or other logging mechanisms)
                Console.WriteLine($"Exception: {ex.Message}");
                return false;  // You can adjust the return value to indicate failure, e.g., return -1 or any appropriate error code
            }
            return false;
        }

        public static CarContainerDTO GetCarByID(int CarID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetCarByID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@CarID", CarID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new CarContainerDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("CarID")),
                                  reader.GetString(reader.GetOrdinal("CarName")),
                                  reader.GetString(reader.GetOrdinal("CarModel")),
                                  reader.GetInt32(reader.GetOrdinal("CarType")),
                                  reader.GetString(reader.GetOrdinal("EngineModel")),
                                  reader.GetString(reader.GetOrdinal("CarPlateNumber")),
                                  reader.GetDecimal(reader.GetOrdinal("RentalCarPrice")),
                                  reader.GetString(reader.GetOrdinal("Color")),
                                  reader.GetInt32(reader.GetOrdinal("DoorsNumber")),
                                  reader.GetString(reader.GetOrdinal("ImagePath")),
                                  reader.GetInt32(reader.GetOrdinal("CurrentKLMT")),
                                  reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                                  (reader.IsDBNull(reader.GetOrdinal("ClientTakenID")) ? -1 :
                                  reader.GetInt32(reader.GetOrdinal("ClientTakenID"))),
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



        public static CarContainerDTO GetCarByCarPlateNumber(string CarPlateNumber)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetCarByCarPlateNumber", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@CarPlateNumber", CarPlateNumber);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new CarContainerDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("CarID")),
                                  reader.GetString(reader.GetOrdinal("CarName")),
                                  reader.GetString(reader.GetOrdinal("CarModel")),
                                  reader.GetInt32(reader.GetOrdinal("CarType")),
                                  reader.GetString(reader.GetOrdinal("EngineModel")),
                                  reader.GetString(reader.GetOrdinal("CarPlateNumber")),
                                  reader.GetDecimal(reader.GetOrdinal("RentalCarPrice")),
                                  reader.GetString(reader.GetOrdinal("Color")),
                                  reader.GetInt32(reader.GetOrdinal("DoorsNumber")),
                                  reader.GetString(reader.GetOrdinal("ImagePath")),
                                  reader.GetInt32(reader.GetOrdinal("CurrentKLMT")),
                                  reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                                  (reader.IsDBNull(reader.GetOrdinal("ClientTakenID")) ? -1 :
                                  reader.GetInt32(reader.GetOrdinal("ClientTakenID"))),
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

        public static List<CarContainerDTO> GetAllCars()
        {
            //Create Object 
            var CarContainerDTO = new List<CarContainerDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("SP_GetAllCars", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Commande.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Load List StudentDTO class 
                            CarContainerDTO.Add(new CarContainerDTO
                            (
                                 reader.GetInt32(reader.GetOrdinal("CarID")),
                                  reader.GetString(reader.GetOrdinal("CarName")),
                                  reader.GetString(reader.GetOrdinal("CarModel")),
                                  reader.GetInt32(reader.GetOrdinal("CarType")),
                                  reader.GetString(reader.GetOrdinal("EngineModel")),
                                  reader.GetString(reader.GetOrdinal("CarPlateNumber")),
                                  reader.GetDecimal(reader.GetOrdinal("RentalCarPrice")),
                                  reader.GetString(reader.GetOrdinal("Color")),
                                  reader.GetInt32(reader.GetOrdinal("DoorsNumber")),
                                  reader.GetString(reader.GetOrdinal("ImagePath")),
                                  reader.GetInt32(reader.GetOrdinal("CurrentKLMT")),
                                  reader.GetBoolean(reader.GetOrdinal("IsAvailable")),
                                  (reader.IsDBNull(reader.GetOrdinal("ClientTakenID")) ? -1 :
                                  reader.GetInt32(reader.GetOrdinal("ClientTakenID"))),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                           ));

                        }
                    }

                }
                //Return List Loaded 
                return CarContainerDTO;
            }
        }


        public static int IsCarExistByID(int CarID)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsCarExistByID", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarID", CarID);

                    Connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        IsExist = Convert.ToInt32(result);
                    }
                    else
                        IsExist = 0;

                }
            }

            return IsExist;
        }
        public static int IsCarExistByCarPlateNumber(string CarPlateNumber)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsCarExistByCarPlateNumber", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarPlateNumber", CarPlateNumber);

                    Connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        IsExist = Convert.ToInt32(result);
                    }
                    else
                        IsExist = 0;

                }
            }

            return IsExist;
        }


    }
}
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReservationCarDTO
    {


        public ReservationCarDTO()
        {
            this.ReservationID = -1;
            this.CarSelectedID = -1;
            this.ClientID = -1;
            this.ReservationDate = DateTime.MinValue;
            this.DateToCheckOut = DateTime.MinValue;
            this.DateToCheckIn = DateTime.MinValue;
            this.KLMTSpend = -1;
            this.TotalRentalFee = -1;
            this.DamageCostfee = -1;
            this.Note = "";
            this.CarIsReturn = false;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;



        }
        public ReservationCarDTO(int ReservationID, int CarSelectedID, int ClientID,
            DateTime ReservationDate, DateTime DateToCkeckOut, DateTime DateToCkeckIn, int KLMTSpend,
            decimal TotalRentalFee, decimal DamageCostFee, string Note, bool CarIsReturn, int CreatedByUserID)
        {
            this.ReservationID = ReservationID;
            this.CarSelectedID = CarSelectedID;
            this.ClientID = ClientID;
            this.ReservationDate = ReservationDate;
            this.DateToCheckOut = DateToCkeckOut;
            this.DateToCheckIn = DateToCkeckIn;
            this.KLMTSpend = KLMTSpend;
            this.TotalRentalFee = TotalRentalFee;
            this.DamageCostfee = DamageCostFee;
            this.Note = Note;
            this.CarIsReturn = CarIsReturn;
            this.CreatedByUserID = CreatedByUserID;



        }

        public enum enMode { AddNew = 1, Update = 2 }

        public enMode Mode = enMode.AddNew;
        public int ReservationID { get; set; }
        public int CarSelectedID { get; set; }
        public int ClientID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DateToCheckOut { get; set; }
        public DateTime DateToCheckIn { get; set; }
        public int KLMTSpend { get; set; }
        public decimal TotalRentalFee { get; set; }
        public decimal DamageCostfee { get; set; }

        public string Note { get; set; }
        public bool CarIsReturn { get; set; }
        public int CreatedByUserID { get; set; }

    }
    public  class clsDataReservationCar
    {

        public static int AddNewReservationCar(ReservationCarDTO reservationCarDTO)
        {
            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_AddNewReservationCar", Connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters for the stored procedure
                        command.Parameters.AddWithValue("@CarSelectedID", reservationCarDTO.CarSelectedID);
                        command.Parameters.AddWithValue("@ClientID", reservationCarDTO.ClientID);
                        command.Parameters.AddWithValue("@ReservationDate", reservationCarDTO.ReservationDate);
                        command.Parameters.AddWithValue("@DateToCheckOut", reservationCarDTO.DateToCheckOut);
                        command.Parameters.AddWithValue("@DateToCheckIn", reservationCarDTO.DateToCheckIn);
                        command.Parameters.AddWithValue("@KLMTSpend", reservationCarDTO.KLMTSpend);
                        command.Parameters.AddWithValue("@TotalRentalFee", reservationCarDTO.TotalRentalFee);
                        if (reservationCarDTO.DamageCostfee != -1)
                            command.Parameters.AddWithValue("@DamageCostfee", reservationCarDTO.DamageCostfee);
                        else
                            command.Parameters.AddWithValue("@DamageCostfee", System.DBNull.Value);
                        if (reservationCarDTO.Note != "" && reservationCarDTO.Note != null)
                            command.Parameters.AddWithValue("@Note", reservationCarDTO.Note);
                        else
                            command.Parameters.AddWithValue("@Note", System.DBNull.Value);

                        command.Parameters.AddWithValue("@CarIsReturn", reservationCarDTO.CarIsReturn);
                        command.Parameters.AddWithValue("@CreatedByUserID", reservationCarDTO.CreatedByUserID);
                        var outputIdParam = new SqlParameter("@NewReservationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        }; command.Parameters.Add(outputIdParam);
                        Connection.Open();
                        command.ExecuteNonQuery();

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

        public static bool UpdateReservationCar(ReservationCarDTO reservationCarDTO)
        {
            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (var command = new SqlCommand("SP_UpdateReservationCar", Connection))
                    {


                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ReservationID", reservationCarDTO.ReservationID);
                        command.Parameters.AddWithValue("@CarSelectedID", reservationCarDTO.CarSelectedID);
                        command.Parameters.AddWithValue("@ClientID", reservationCarDTO.ClientID);
                        command.Parameters.AddWithValue("@ReservationDate", reservationCarDTO.ReservationDate);
                        command.Parameters.AddWithValue("@DateToCheckOut", reservationCarDTO.DateToCheckOut);
                        command.Parameters.AddWithValue("@DateToCheckIn", reservationCarDTO.DateToCheckIn);
                        command.Parameters.AddWithValue("@KLMTSpend", reservationCarDTO.KLMTSpend);
                        command.Parameters.AddWithValue("@TotalRentalFee", reservationCarDTO.TotalRentalFee);
                        if (reservationCarDTO.DamageCostfee != -1)
                            command.Parameters.AddWithValue("@DamageCostfee", reservationCarDTO.DamageCostfee);
                        else
                            command.Parameters.AddWithValue("@DamageCostfee", System.DBNull.Value);
                        if (reservationCarDTO.Note != "" && reservationCarDTO.Note != null)
                            command.Parameters.AddWithValue("@Note", reservationCarDTO.Note);
                        else
                            command.Parameters.AddWithValue("@Note", System.DBNull.Value);
                        command.Parameters.AddWithValue("@CarIsReturn", reservationCarDTO.CarIsReturn);
                        command.Parameters.AddWithValue("@CreatedByUserID", reservationCarDTO.CreatedByUserID);


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

        public static bool DeleteReservationCar(int ReservationID)
        {
            try
            {
                using (var connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                using (var command = new SqlCommand("SP_DeleteReservationCars", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReservationID", ReservationID);
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
        public static ReservationCarDTO GetReservationCarByID(int ReservationID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetReservationByID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@ReservationID", ReservationID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new ReservationCarDTO
                                 (

                                 reader.GetInt32(reader.GetOrdinal("ReservationID")),
                                  reader.GetInt32(reader.GetOrdinal("CarSelectedID")),
                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetDateTime(reader.GetOrdinal("ReservationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckOut")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckIn")),
                                  reader.GetInt32(reader.GetOrdinal("KLMTSpend")),
                                  reader.GetDecimal(reader.GetOrdinal("TotalRentalFee")),
                                   (reader.IsDBNull(reader.GetOrdinal("DamageCostfee")) ? 0 :
                                    reader.GetDecimal(reader.GetOrdinal("DamageCostfee"))),
                                  (reader.IsDBNull(reader.GetOrdinal("Note")) ? string.Empty :
                                  reader.GetString(reader.GetOrdinal("Note"))),
                                  reader.GetBoolean(reader.GetOrdinal("CarIsReturn")),
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


        public static ReservationCarDTO GetReservationCarByClientID(int ClientID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetReservationByClientID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@ClientID", ClientID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new ReservationCarDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("ReservationID")),
                                  reader.GetInt32(reader.GetOrdinal("CarSelectedID")),
                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetDateTime(reader.GetOrdinal("ReservationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckOut")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckIn")),
                                  reader.GetInt32(reader.GetOrdinal("KLMTSpend")),
                                  reader.GetDecimal(reader.GetOrdinal("TotalRentalFee")),
                                   (reader.IsDBNull(reader.GetOrdinal("DamageCostfee")) ? 0 :
                                    reader.GetDecimal(reader.GetOrdinal("DamageCostfee"))),
                                  (reader.IsDBNull(reader.GetOrdinal("Note")) ? string.Empty :
                                  reader.GetString(reader.GetOrdinal("Note"))),
                                  reader.GetBoolean(reader.GetOrdinal("CarIsReturn")),
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


        public static ReservationCarDTO GetReservationCarByCarSelectedID(int CarSelectedID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetReservationByCarSelectedID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@CarSelectedID", CarSelectedID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new ReservationCarDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("ReservationID")),
                                  reader.GetInt32(reader.GetOrdinal("CarSelectedID")),
                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetDateTime(reader.GetOrdinal("ReservationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckOut")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckIn")),
                                  reader.GetInt32(reader.GetOrdinal("KLMTSpend")),
                                  reader.GetDecimal(reader.GetOrdinal("TotalRentalFee")),
                                   (reader.IsDBNull(reader.GetOrdinal("DamageCostfee")) ? 0 :
                                    reader.GetDecimal(reader.GetOrdinal("DamageCostfee"))),
                                  (reader.IsDBNull(reader.GetOrdinal("Note")) ? string.Empty :
                                  reader.GetString(reader.GetOrdinal("Note"))),
                                  reader.GetBoolean(reader.GetOrdinal("CarIsReturn")),
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



        public static List<ReservationCarDTO> GetAllReservation()
        {
            //Create Object 
            var reservationCarDTO = new List<ReservationCarDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("GetAllReservationCars", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Commande.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Load List StudentDTO class 
                            reservationCarDTO.Add(new ReservationCarDTO
                            (
                                    reader.GetInt32(reader.GetOrdinal("ReservationID")),
                                  reader.GetInt32(reader.GetOrdinal("CarSelectedID")),
                                  reader.GetInt32(reader.GetOrdinal("ClientID")),
                                  reader.GetDateTime(reader.GetOrdinal("ReservationDate")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckOut")),
                                  reader.GetDateTime(reader.GetOrdinal("DateToCheckIn")),
                                  reader.GetInt32(reader.GetOrdinal("KLMTSpend")),
                                  reader.GetDecimal(reader.GetOrdinal("TotalRentalFee")),
                                   (reader.IsDBNull(reader.GetOrdinal("DamageCostfee")) ? 0 :
                                    reader.GetDecimal(reader.GetOrdinal("DamageCostfee"))),
                                  (reader.IsDBNull(reader.GetOrdinal("Note")) ? string.Empty :
                                  reader.GetString(reader.GetOrdinal("Note"))),
                                  reader.GetBoolean(reader.GetOrdinal("CarIsReturn")),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                        ) )  ;

                        }
                    }

                }
                //Return List Loaded 
                return reservationCarDTO;
            }
        }


        public static int IsReservationExistByID(int ReservationID)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsReservationExistByID", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReservationID", ReservationID);

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
        public static int IsCarReturnByID(int ReservationID)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsReservationCar_CarIsReturnById", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ReservationID", ReservationID);

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

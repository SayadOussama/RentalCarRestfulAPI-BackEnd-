using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CarTypeDTO
    {
        public CarTypeDTO(int CarTypeID , string TypeName ,int CreatedByUserID)
        {
           this.CarTypeID = CarTypeID;
            this.TypeName = TypeName;
            this.CreatedByUserID = CreatedByUserID;



        }

        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode = enMode.AddNew;

        public int CarTypeID { get; set; }
        public string TypeName { get; set; }

        public int CreatedByUserID { get; set; }

    }
    public  class clsDataCarTypes
    {
        public static int AddNewCarType(CarTypeDTO carTypeDTO)
        {
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewCarType", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@TypeName", carTypeDTO.TypeName);
                    command.Parameters.AddWithValue("@CreatedByUserID", carTypeDTO.CreatedByUserID);
                    


                    var outputIdParam = new SqlParameter("@NewCarTypeID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    Connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputIdParam.Value;
                }
            }
        }


        public static bool UpdateCarType(CarTypeDTO carTypeDTO)
        {

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_UpdateCarType", Connection))
            {
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarTypeID", carTypeDTO.CarTypeID);
                    command.Parameters.AddWithValue("@TypeName", carTypeDTO.TypeName);
                    command.Parameters.AddWithValue("@CreatedByUserID", carTypeDTO.CreatedByUserID);


                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }

        public static bool DeleteCarTypeByID(int CarTypeID)
        {
            using (var connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_DeleteCarTypes", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CarTypeID", CarTypeID);
                connection.Open();

                int rowsAffected = (int)command.ExecuteNonQuery();
                return (rowsAffected == 1);


            }
        }


        public static CarTypeDTO GetUserInfoByCarTypeID(int CarTypeID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetCarTypeByID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@CarTypeID", CarTypeID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new CarTypeDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("CarTypeID")),
                                  reader.GetString(reader.GetOrdinal("TypeName")),
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

        public static CarTypeDTO GetUserInfoByTypeName(string TypeName)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetCarTypeByCarTypeName", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@TypeName", TypeName);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new CarTypeDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("CarTypeID")),
                                  reader.GetString(reader.GetOrdinal("TypeName")),
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


        public static List<CarTypeDTO> GetAllCarTypes()
        {
            //Create Object 
            var carTypeDTO = new List<CarTypeDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("SP_GetAllCarTypes", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Commande.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Load List StudentDTO class 
                            carTypeDTO.Add(new CarTypeDTO
                            (
                                reader.GetInt32(reader.GetOrdinal("CarTypeID")),
                                  reader.GetString(reader.GetOrdinal("TypeName")),
                                  reader.GetInt32(reader.GetOrdinal("CreatedByUserID"))
                         

                           ));

                        }
                    }

                }
                //Return List Loaded 
                return carTypeDTO;
            }
        }


        public static int IsCarTypeExistByID(int CarTypeID)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsCarTypeExistByID", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarTypeID", CarTypeID);

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
        public static int IsCarTypeExistByNameType(string NameType)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsCarTypeExistByNameType", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NameType", NameType);

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

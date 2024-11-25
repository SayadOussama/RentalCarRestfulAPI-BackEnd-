using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UsersDTO
    {
        public UsersDTO(int UserID, int PersonID, string UserName, string PassWord, bool IsActive)
        {
           this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.PassWord = PassWord;
            this.IsActive = IsActive;


        }

        public enum enMode { AddNew = 1, Update = 2 }

        public enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsActive { get; set; }

    }




    public  class clsDataUsers

    {

        public static int AddNewUser(UsersDTO userDTO)
        {
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNewUser", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                    command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                    command.Parameters.AddWithValue("@passWord", userDTO.PassWord);
                    command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);
                    

                    var outputIdParam = new SqlParameter("@NewUser", SqlDbType.Int)
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

        public static bool UpdateUser(UsersDTO userDTO)
        {

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_UpdateUser", Connection))
            {
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userDTO.UserID);
                    command.Parameters.AddWithValue("@PersonID", userDTO.PersonID);
                    command.Parameters.AddWithValue("@UserName", userDTO.UserName);
                    command.Parameters.AddWithValue("@PassWord", userDTO.PassWord);
                    command.Parameters.AddWithValue("@IsActive", userDTO.IsActive);
                
                        
                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }

        public static bool DeleteUserByID(int UserID)
        {
            using (var connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_DeleteUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                connection.Open();

                int rowsAffected = (int)command.ExecuteNonQuery();
                return (rowsAffected == 1);


            }
        }
        public static UsersDTO GetUserInfoByPersonID(int PersonID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetUserByPersonId", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@PersonID", PersonID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new UsersDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("UserID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("UserName")),
                                  reader.GetString(reader.GetOrdinal("PassWord")),
                                  reader.GetBoolean(reader.GetOrdinal("IsActive"))
                                 







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

        public static UsersDTO GetUserByUserNameAndPasword(string UserName , string PassWord)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetUserByUsernamePassword", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@UserName", UserName);
                    Commande.Parameters.AddWithValue("@PassWord", PassWord);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new UsersDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("UserID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("UserName")),
                                  reader.GetString(reader.GetOrdinal("PassWord")),
                                  reader.GetBoolean(reader.GetOrdinal("IsActive"))








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

        public static UsersDTO GetUserInfoByUserID(int UserID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetUserByUserId", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@UserID", UserID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new UsersDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("UserID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("UserName")),
                                  reader.GetString(reader.GetOrdinal("PassWord")),
                                  reader.GetBoolean(reader.GetOrdinal("IsActive"))








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
        public static int IsUserExistByUserID(int UserID)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsUserExist", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);

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
        public static int IsUserExistByUserName(string  UserName)
        {
            int IsExist = 0;

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_IsUserExistByUserName", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);

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
        public static List<UsersDTO> GetAllUsers()
        {
            //Create Object 
            var userDTO = new List<UsersDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("SP_GetAllUsers", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Commande.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Load List StudentDTO class 
                            userDTO.Add(new UsersDTO
                            (
                                 reader.GetInt32(reader.GetOrdinal("UserID")),
                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("UserName")),
                                  reader.GetString(reader.GetOrdinal("PassWord")),
                                  reader.GetBoolean(reader.GetOrdinal("IsActive"))

                           ));

                        }
                    }

                }
                //Return List Loaded 
                return userDTO;
            }
        }

    }
}

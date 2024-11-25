using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataLayer
{
    public class PersonDTO
    {
        public PersonDTO(int PersonID, string NationalNO, string FirstName, string LastName, DateTime BirthDay, byte Gender, string PhoneNumber, string Address, string Email, int NationalCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNO = NationalNO;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BirthDay = BirthDay;
            this.Gender = Gender;
            this.PhoneNumber = PhoneNumber;
            this.address = Address;
            this.PhoneNumber = PhoneNumber;
            this.address = Address;
            this.Email = Email;
            this.NationalCountryID = NationalCountryID;
            this.ImagePath = ImagePath;


        }

        public int PersonID { set; get; }
        public string NationalNO { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FullName { get { return LastName + " " + FirstName; } }
        public DateTime BirthDay { set; get; }
        public byte Gender { set; get; }
        public string PhoneNumber { set; get; }

        public string address { get; set; }

        public string Email { get; set; }

        public int NationalCountryID { get; set; }

        public string ImagePath { get; set; }

    }
   public  class clsDataPerson
    {

        public static int AddNewPerson(PersonDTO personDTO)
        {
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                using (var command = new SqlCommand("SP_AddNEwPerson", Connection))
                {
                    try { 
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@NationalNO", personDTO.NationalNO);
                    command.Parameters.AddWithValue("@FirstName", personDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", personDTO.LastName);
                    command.Parameters.AddWithValue("@BirthDay", personDTO.BirthDay);
                    command.Parameters.AddWithValue("@Gender", personDTO.Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", personDTO.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", personDTO.address);
                    if (personDTO.Email != "" && personDTO.Email != null)
                        command.Parameters.AddWithValue("@Email", personDTO.Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);
                    command.Parameters.AddWithValue("@NationalCountryID", personDTO.NationalCountryID);
                    if (personDTO.ImagePath != "" && personDTO.ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", personDTO.ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);

                    var outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    }; 
                    command.Parameters.Add(outputIdParam);
                    Connection.Open();
                    command.ExecuteNonQuery();

                    return (int)outputIdParam.Value;
                        }
                    catch(Exception ex) {
                        Console.WriteLine(ex.Message);
                        return 0;
                        }
                }
            }
        }
        public static bool UpdatePerson(PersonDTO PersonDTO)
        {

            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_UpdatePerson", Connection))
            {
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonDTO.PersonID);
                    command.Parameters.AddWithValue("@NationalNO", PersonDTO.NationalNO);
                    command.Parameters.AddWithValue("@FirstName", PersonDTO.FirstName);
                    command.Parameters.AddWithValue("@LastName", PersonDTO.LastName);
                    command.Parameters.AddWithValue("@BirthDay", PersonDTO.BirthDay);
                    command.Parameters.AddWithValue("@Gender", PersonDTO.Gender);
                    command.Parameters.AddWithValue("@PhoneNumber", PersonDTO.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", PersonDTO.address);
                    if (PersonDTO.Email != "" && PersonDTO.Email != null)
                        command.Parameters.AddWithValue("@Email", PersonDTO.Email);
                    else
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);
                    command.Parameters.AddWithValue("@NationalCountryID", PersonDTO.NationalCountryID);
                    if (PersonDTO.ImagePath != "" && PersonDTO.ImagePath != null)
                        command.Parameters.AddWithValue("@ImagePath", PersonDTO.ImagePath);
                    else
                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                    Connection.Open();
                    command.ExecuteNonQuery();
                    return true;

                }
            }
        }
        public static bool DeletePersonByID(int PersonID)
        {
            using (var connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            using (var command = new SqlCommand("SP_DeletePerson", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", PersonID);
                connection.Open();

                int rowsAffected = (int)command.ExecuteNonQuery();
                return (rowsAffected == 1);


            }
        }
       
        public static PersonDTO GetPersonInfoByPersonID(int PersonID)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetPersonByID", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@PersonID", PersonID);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new PersonDTO
                                 (

                                      reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("NationalNo")),
                                  reader.GetString(reader.GetOrdinal("FirstName")),
                                  reader.GetString(reader.GetOrdinal("LastName")),
                                  reader.GetDateTime(reader.GetOrdinal("BirthDay")),
                                  reader.GetByte(reader.GetOrdinal("Gender")),
                                  reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                  reader.GetString(reader.GetOrdinal("address")),
                                   reader.GetString(reader.GetOrdinal("Email")),
                                  reader.GetInt32(reader.GetOrdinal("NationalCountryID")),

                               (reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty :
                               reader.GetString(reader.GetOrdinal("ImagePath"))







                               ));
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }


        }
        public static PersonDTO GetPersonInfoByFirstName(string FirstName)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetPersonByFirstName", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@FirstName", FirstName);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new PersonDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("NationalNo")),
                                  reader.GetString(reader.GetOrdinal("FirstName")),
                                  reader.GetString(reader.GetOrdinal("LastName")),
                                  reader.GetDateTime(reader.GetOrdinal("BirthDay")),
                                  reader.GetByte(reader.GetOrdinal("Gender")),
                                  reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                  reader.GetString(reader.GetOrdinal("address")),
                                   reader.GetString(reader.GetOrdinal("Email")),
                                  reader.GetInt32(reader.GetOrdinal("NationalCountryID")),
                               (reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty :
                               reader.GetString(reader.GetOrdinal("ImagePath"))







                               ));
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }

        }

        public static PersonDTO GetPersonInfoByNationalNo(string NationalNo)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetPersonByNationalNo", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@NationalNo", NationalNo);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new PersonDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("NationalNo")),
                                  reader.GetString(reader.GetOrdinal("FirstName")),
                                  reader.GetString(reader.GetOrdinal("LastName")),
                                  reader.GetDateTime(reader.GetOrdinal("BirthDay")),
                                  reader.GetByte(reader.GetOrdinal("Gender")),
                                  reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                  reader.GetString(reader.GetOrdinal("address")),
                                   reader.GetString(reader.GetOrdinal("Email")),
                                  reader.GetInt32(reader.GetOrdinal("NationalCountryID")),
                                 (reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty :
                                  reader.GetString(reader.GetOrdinal("ImagePath"))







                               ));
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }

        }


      
        public static PersonDTO GetPersonInfoByLastName(string LastName)
        {


            //Get Connection in DataBase 
            //we Use Var Here Becuase will Return Only One Object 
            using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (var Commande = new SqlCommand("SP_GetPersonInfoByLastName", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Commande.Parameters.AddWithValue("@LastName", LastName);
                    Connection.Open();

                    using (var reader = Commande.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //will return Only DTO Data 
                            return new PersonDTO
                                 (

                                  reader.GetInt32(reader.GetOrdinal("PersonID")),
                                  reader.GetString(reader.GetOrdinal("NationalNo")),
                                  reader.GetString(reader.GetOrdinal("FirstName")),
                                  reader.GetString(reader.GetOrdinal("LastName")),
                                  reader.GetDateTime(reader.GetOrdinal("BirthDay")),
                                  reader.GetByte(reader.GetOrdinal("Gender")),
                                  reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                  reader.GetString(reader.GetOrdinal("address")),
                                   reader.GetString(reader.GetOrdinal("Email")),
                                  reader.GetInt32(reader.GetOrdinal("NationalCountryID")),
                                 (reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty :
                                  reader.GetString(reader.GetOrdinal("ImagePath"))







                               ));
                        }
                        else
                        {
                            return null;
                        }
                    }

                }

            }

        }

        public static int IsPersonExistByID(int PersonID)
        {
            int IsExist = 0;

            try
            {
                using (var Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_IsPersonIsExist", Connection))
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
  
        


        public static List<PersonDTO> GetAllPeople()
        {
            //Create Object 
            var personDTO = new List<PersonDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("SP_GetAllPeople", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Commande.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Load List StudentDTO class 
                            personDTO.Add(new PersonDTO
                            (
                            reader.GetInt32(reader.GetOrdinal("PersonID")),
                            reader.GetString(reader.GetOrdinal("NationalNo")),
                            reader.GetString(reader.GetOrdinal("FirstName")),
                            reader.GetString(reader.GetOrdinal("LastName")),
                            reader.GetDateTime(reader.GetOrdinal("BirthDay")),
                            reader.GetByte(reader.GetOrdinal("Gender")),
                            reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            reader.GetString(reader.GetOrdinal("address")),
                            reader.GetString(reader.GetOrdinal("Email")),
                            reader.GetInt32(reader.GetOrdinal("NationalCountryID")),
                           (reader.IsDBNull(reader.GetOrdinal("ImagePath")) ? string.Empty : reader.GetString(reader.GetOrdinal("ImagePath"))
                           )));
                               
                        }
                    }

                }
                //Return List Loaded 
                return personDTO;
            }
        }
    }
}

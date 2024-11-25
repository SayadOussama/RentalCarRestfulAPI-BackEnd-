using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;

namespace DataLayer
{


     public class CountriesDTO
    {
        public CountriesDTO(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

    public class clsDataCountries
    {

        public static CountriesDTO GetFindCountryByCountryID(int CountryID)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString); string query = "SELECT * FROM Countries WHERE CountryID= @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID ", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //will return Only DTO Data 
                    return new CountriesDTO
                    (
                    reader.GetInt32(reader.GetOrdinal("CountryID")),
                    reader.GetString(reader.GetOrdinal("CountryName"))

                         );
                }
                else
                {


                    return null;
                }




            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        public static CountriesDTO GetFindCountryByCountryName(string CountryName)
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSetting.ConnectionString); string query = "SELECT * FROM Countries WHERE CountryName= @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName ", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new CountriesDTO
                        (
                    reader.GetInt32(reader.GetOrdinal("CountryID")),
                    reader.GetString(reader.GetOrdinal("CountryName"))
                    );
                }
                else
                {
                    // The record was not found
                    return null;
                }




            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }

        }
        public static List<CountriesDTO> GetAllCountries()
        {
            //Create Object 
            var CountriesList = new List<CountriesDTO>();

            //Get Connection in DataBase 
            using (SqlConnection Connection = new SqlConnection(clsDataAccessSetting.ConnectionString))
            {
                //Get Connect in SP                          SP Name             //Sql Connection 
                using (SqlCommand Commande = new SqlCommand("SP_GetAllCountries", Connection))
                {
                    Commande.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader Reader = Commande.ExecuteReader())
                    {
                        while (Reader.Read())
                        {
                            //Load List CountriesDTO class 
                            CountriesList.Add(new CountriesDTO
                                (
                                Reader.GetInt32(Reader.GetOrdinal("CountryID")),
                                Reader.GetString(Reader.GetOrdinal("CountryName"))

                                ));
                        }
                    }

                }
                //Return List Loaded 
                return CountriesList;
            }
        }
    }
}




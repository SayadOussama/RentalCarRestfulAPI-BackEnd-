using DataLayer;

namespace BusinessLayer
{

        public class clsCountries
        {


            public enum enMode { AddNew = 1, Update = 2 }
            enMode Mode = enMode.AddNew;
            public int CountryID { get; set; }
            public string CountryName { get; set; }

            public CountriesDTO CDTO
            {
                get { return (new CountriesDTO(this.CountryID, this.CountryName)); }
            }

            public clsCountries(CountriesDTO CDTO, enMode cMode = enMode.AddNew)
            {
                this.CountryID = CDTO.CountryID;
                this.CountryName = CDTO.CountryName;
                Mode = cMode;
            }




            public static clsCountries FindCountryInfoByCountryID(int CountryID)
            {
                //return Student DTO Data 
                CountriesDTO CDTO = clsDataCountries.GetFindCountryByCountryID(CountryID);
                if (CDTO != null)
                {

                    return new clsCountries(CDTO, enMode.Update);
                }
                else
                    return null;
            }
            public static clsCountries FindCountryInfoByCountryName(string CountryName)
            {
                //return Student DTO Data 
                CountriesDTO CDTO = clsDataCountries.GetFindCountryByCountryName(CountryName);
                if (CDTO != null)
                {

                    return new clsCountries(CDTO, enMode.Update);
                }
                else
                    return null;
            }
            public static List<CountriesDTO> GetAllCountriesList()
            {

                return clsDataCountries.GetAllCountries();
            }
        }



    }

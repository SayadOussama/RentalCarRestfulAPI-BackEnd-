using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class clsPerson
    {
        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode = enMode.AddNew;
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


        public string  ImagePath { get; set; }

        public clsCountries _Countries;  
        public PersonDTO PDTO
        {
            get { return (new PersonDTO(this.PersonID, this.NationalNO, this.FirstName, this.LastName ,  this.BirthDay, this.Gender,
                this.PhoneNumber,this.address , this.Email,this.NationalCountryID,this.ImagePath)); }
        }
        public clsPerson(PersonDTO PDTO, enMode cMode = enMode.AddNew)
        {
            this.PersonID = PDTO.PersonID;
            this.NationalNO = PDTO.NationalNO;
            this.FirstName = PDTO.FirstName;
            this.LastName = PDTO.LastName;
            this.BirthDay = PDTO.BirthDay;
            this.Gender = PDTO.Gender;
            this.PhoneNumber = PDTO.PhoneNumber;
            this.address = PDTO.address;
            this.PhoneNumber = PDTO.PhoneNumber;
            this.Email = PDTO.Email;
            this.NationalCountryID = PDTO.NationalCountryID;
            this.ImagePath = PDTO.ImagePath;
            //add Composition of Countries Class 
            _Countries = clsCountries.FindCountryInfoByCountryID(NationalCountryID);
            Mode = cMode;
        }
        private bool _AddNewPerson()
        {
            //return the Current SDTO
            this.PersonID = clsDataPerson.AddNewPerson(PDTO);
            return (this.PersonID != -1);


        }
        private bool _UpdatePerson()
        {                                       //return the Current SDTO
            return clsDataPerson.UpdatePerson(PDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }

            return false;
        }

        public static clsPerson GetPersonInfoByID(int PersonID)
        {
            //return Student DTO Data 
            PersonDTO PDTO = clsDataPerson.GetPersonInfoByPersonID(PersonID);
            if (PDTO != null)
            {

                return new clsPerson(PDTO, enMode.Update);
            }
            else
                return null;
        }

        public static clsPerson GetPersonIDByNationalNo(string NationalNo)
        {
            //return Student DTO Data 
            PersonDTO PDTO = clsDataPerson.GetPersonInfoByNationalNo(NationalNo);
            if (PDTO != null)
            {

                return new clsPerson(PDTO, enMode.Update);
            }
            else
                return null;
        }

        public static clsPerson GetPersonByFirstName(string FirstName)
        {
            //return Student DTO Data 
            PersonDTO PDTO = clsDataPerson.GetPersonInfoByFirstName(FirstName);
            if (PDTO != null)
            {

                return new clsPerson(PDTO, enMode.Update);
            }
            else
                return null;
        }
        public static clsPerson GetPersonByLastName(string LastFirst)
        {
            //return Student DTO Data 
            PersonDTO PDTO = clsDataPerson.GetPersonInfoByLastName(LastFirst);
            if (PDTO != null)
            {

                return new clsPerson(PDTO, enMode.Update);
            }
            else
                return null;
        }
        public static List<PersonDTO> GetAllPeople()
        {

            return clsDataPerson.GetAllPeople();
        }
        public static int IsPersonExist(int PersonID)
        {
            return clsDataPerson.IsPersonExistByID(PersonID);
        }
        public static bool DeletePersonByID(int PersonID)
        {
            return clsDataPerson.DeletePersonByID(PersonID);
        }
    }
}


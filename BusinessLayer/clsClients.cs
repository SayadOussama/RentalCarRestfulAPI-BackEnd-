using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class clsClients
    {

        public enum enMode { AddNew = 1, Update = 2 }
        public enMode Mode = enMode.AddNew;



        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public int VehicalLicenseNumber { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public int CreatedByUserID { get; set; }
        public clsPerson _PersonInfo;

        public ClientsDTO CDTO
        {
            get
            {
                return (new ClientsDTO(this.ClientID, this.PersonID, this.VehicalLicenseNumber, this.LicenseExpirationDate, this.AccountCreationDate,this.CreatedByUserID));
            }
        }
        public clsClients(ClientsDTO CDTO, enMode cMode = enMode.AddNew)
        {
            this.ClientID = CDTO.ClientID;
            this.PersonID = CDTO.PersonID;
            this.VehicalLicenseNumber = CDTO.VehicalLicenseNumber;
            this.LicenseExpirationDate = CDTO.LicenseExpirationDate;
            this.AccountCreationDate = CDTO.AccountCreationDate;
            this.CreatedByUserID = CDTO.CreatedByUserID;
            //add Composition of Person Class 
            _PersonInfo = clsPerson.GetPersonInfoByID(this.PersonID);
            Mode = cMode;
        }

        private bool _AddNewClient()
        {
            //return the Current SDTO
            this.ClientID = clsDataClients.AddNewClient(CDTO);
            return (this.ClientID != -1);


        }
        private bool _UpdateClient()
        {                                       //return the Current SDTO
            return clsDataClients.UpdateClient(CDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewClient())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateClient();

            }

            return false;
        }

        public static clsClients GetClientsByPersonID(int PersonID)
        {
            //return Student DTO Data 
            ClientsDTO CDTO = clsDataClients.GetClientInfoByPersonID(PersonID);
            if (CDTO != null)
            {

                return new clsClients(CDTO, enMode.Update);
            }
            else
                return null;
        }

        public static clsClients GetClientsByClientID(int ClientID)
        {
            //return Student DTO Data 
            ClientsDTO CDTO = clsDataClients.GetClientInfoByClientID(ClientID);
            if (CDTO != null)
            {

                return new clsClients(CDTO, enMode.Update);
            }
            else
                return null;
        }


        public static clsClients GetClientsByVehicalNumber(int VehicalLicenseNumber)
        {
            //return Student DTO Data 
            ClientsDTO CDTO = clsDataClients.GetClientInfoByVehicalLicenseNumber(VehicalLicenseNumber);
            if (CDTO != null)
            {

                return new clsClients(CDTO, enMode.Update);
            }
            else
                return null;
        }

        public static List<ClientsDTO> GetAllClients()
        {

            return clsDataClients.GetAllClients();
        }

        public static bool DeleteClientByID(int ClientId)
        {
            return clsDataClients.DeleteClientByID(ClientId);
        }
        public static int IsClientExistByPersonID(int PersonID)
        {
            return clsDataClients.IsClientExistByPersonID(PersonID);
        }
        public static int IsClientExistByClientID(int ClientID)
        {
            return clsDataClients.IsClientExistByClientID(ClientID);
        }
        public static int IsClientExistByVehicalLicenseNumber(int VehicalLicenseNumber)
        {
            return clsDataClients.IsClientExistByVihecalLicenseNumber(VehicalLicenseNumber);
        }
    }
}

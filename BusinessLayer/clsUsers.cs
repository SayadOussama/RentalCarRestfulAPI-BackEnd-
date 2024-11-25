using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public  class clsUsers
    {

        public enum enMode { AddNew = 1, Update = 2 }

        public enMode Mode = enMode.AddNew;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsActive { get; set; }
        public clsPerson _PersonInfo;
        public UsersDTO UDTO
        {
            get
            {
                return (new UsersDTO(this.UserID, this.PersonID, this.UserName, this.PassWord, this.IsActive));
            }
        }

        public clsUsers(UsersDTO UDTO, enMode cMode = enMode.AddNew)
        {
           this.UserID = UDTO.UserID;
            this.PersonID = UDTO.PersonID;
            this.UserName = UDTO.UserName;
            this.PassWord = UDTO.PassWord;
            this.IsActive = UDTO.IsActive;
            //add Composition of Countries Class 
            _PersonInfo = clsPerson.GetPersonInfoByID(this.PersonID);
            Mode = cMode;
        }


        private bool _AddNewUser()
        {
            //return the Current SDTO
            this.UserID = clsDataUsers.AddNewUser(UDTO);
            return (this.UserID != -1);


        }
        private bool _UpdateUser()
        {                                       //return the Current SDTO
            return clsDataUsers.UpdateUser(UDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        public static clsUsers GetUserByPersonID(int PersonID)
        {
            //return Student DTO Data 
            UsersDTO UDTO = clsDataUsers.GetUserInfoByPersonID(PersonID);
            if (UDTO != null)
            {

                return new clsUsers(UDTO, enMode.Update);
            }
            else
                return null;
        }
        public static clsUsers GetUserByUserNameAndPassWord(string  UserName , string PassWord)
        {
            //return Student DTO Data 
            UsersDTO UDTO = clsDataUsers.GetUserByUserNameAndPasword(UserName, PassWord);
            if (UDTO != null)
            {

                return new clsUsers(UDTO, enMode.Update);
            }
            else
                return null;
        }

        public static clsUsers GetUserByUserID(int UserID)
        {
            //return Student DTO Data 
            UsersDTO UDTO = clsDataUsers.GetUserInfoByUserID(UserID);
            if (UDTO != null)
            {

                return new clsUsers(UDTO, enMode.Update);
            }
            else
                return null;
        }




        public static int IsExistByUserID(int UserID)
        {
            return clsDataUsers.IsUserExistByUserID(UserID);
        }
        public static int IsUserExistByUserName(string UserName)
        {
            return clsDataUsers.IsUserExistByUserName(UserName);
        }


        public static List<UsersDTO> GetAllUsers()
        {

            return clsDataUsers.GetAllUsers();
        }
        public static bool DeleteUserByID(int UserId)
        {
            return clsDataUsers.DeleteUserByID(UserId);
        }

    }




}

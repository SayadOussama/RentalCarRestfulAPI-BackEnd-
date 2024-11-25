using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCarTypes
    {
        public enum enMode { AddNew = 1, Update = 2 }

        public enMode Mode = enMode.AddNew;
      

        public int CarTypeID { get; set; }
        public string TypeName { get; set; }

        public int CreatedByUserID { get; set; }
        public CarTypeDTO CarTypeDTO
        {
            get
            {
                return (new CarTypeDTO(this.CarTypeID, this.TypeName, this.CreatedByUserID));
            }
        }

        public clsCarTypes(CarTypeDTO CarTypeDTO, enMode cMode = enMode.AddNew)
        {
           this.CarTypeID = CarTypeDTO.CarTypeID;
            this.TypeName = CarTypeDTO.TypeName;
            this.CreatedByUserID = CarTypeDTO.CreatedByUserID;
            Mode = cMode;
        }
        private bool _AddNewCarType()
        {
            //return the Current SDTO
            this.CarTypeID = clsDataCarTypes.AddNewCarType(CarTypeDTO);
            return (this.CarTypeID != -1);


        }
        private bool _UpdateCarType()
        {                                       //return the Current SDTO
            return clsDataCarTypes.UpdateCarType(CarTypeDTO);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCarType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCarType();

            }

            return false;
        }
        public static clsCarTypes GetCarTypeByID(int CarTypeID)
        {
            //return Student DTO Data 
            CarTypeDTO carTypeDTO = clsDataCarTypes.GetUserInfoByCarTypeID(CarTypeID);
            if (carTypeDTO != null)
            {

                return new clsCarTypes(carTypeDTO, enMode.Update);
            }
            else
                return null;
        }
        public static clsCarTypes GetCarTypeByTypeName(string TypeName)
        {
            //return Student DTO Data 
            CarTypeDTO carTypeDTO = clsDataCarTypes.GetUserInfoByTypeName(TypeName);
            if (carTypeDTO != null)
            {

                return new clsCarTypes(carTypeDTO, enMode.Update);
            }
            else
                return null;
        }


        public static List<CarTypeDTO> GetAllCarType()
        {

            return clsDataCarTypes.GetAllCarTypes();
        }
        public static bool DeleteCarTypeByID(int CarTypeID)
        {
            return clsDataCarTypes.DeleteCarTypeByID(CarTypeID);
        }

        public static int IsCarTypeExistByID(int CarTypeID)
        {
            return clsDataCarTypes.IsCarTypeExistByID(CarTypeID);
        }
        public static int IsCarTypeExistByTypeName(string TypeName)
        {
            return clsDataCarTypes.IsCarTypeExistByNameType(TypeName);
        }





    }
}

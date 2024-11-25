using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class clsCarContainer
    {

        public enum enMode { AddNew = 1, Update = 2 }

        public enMode Mode = enMode.AddNew;


        public int CarID { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public int CarType { get; set; }

        public string EngineModel { get; set; }
        public string CarPlateNumber { get; set; }
        public decimal RentalCarPrice { get; set; }

        public string Color { get; set; }
        public int DoorsNumber { get; set; }
        public string ImagePath { get; set; }

        public int CurrentKLMT { get; set; }
        public bool IsAvailable { get; set; }
        public int ClientTakenID { get; set; }
       
       
        public int CreatedByUserID { get; set; }
        public clsClients _TakenClientInfo { get; set; }
        //Car Type Info Class 
        public clsCarTypes _CarTypeInfo;
        public CarContainerDTO carContainerDTO
        {
            get
            {
                return (new CarContainerDTO(this.CarID, this.CarName, this.CarModel, this.CarType, this.EngineModel, this.CarPlateNumber, this.RentalCarPrice, this.Color, this.DoorsNumber, this.ImagePath, this.CurrentKLMT, this.IsAvailable, this.ClientTakenID, this.CreatedByUserID));
            }
        }
        public clsCarContainer(CarContainerDTO carContainerDTO, enMode cMode = enMode.AddNew)
        {
            this.CarID = carContainerDTO.CarID;
            this.CarName = carContainerDTO.CarName;
            this.CarModel = carContainerDTO.CarModel;
            this.CarType = carContainerDTO.CarType;
            this.EngineModel = carContainerDTO.EngineModel;
            this.CarPlateNumber = carContainerDTO.CarPlateNumber;
            this.RentalCarPrice = carContainerDTO.RentalCarPrice;
            this.Color = carContainerDTO.Color;
            this.DoorsNumber = carContainerDTO.DoorsNumber;
            this.ImagePath = carContainerDTO.ImagePath;
            this.CurrentKLMT = carContainerDTO.CurrentKLMT;
            this.IsAvailable = carContainerDTO.IsAvailable;
            this.ClientTakenID = carContainerDTO.ClientTakenID;
            this.CreatedByUserID = carContainerDTO.CreatedByUserID;
            Mode = cMode;
            if (this.ClientTakenID != -1)
                this._TakenClientInfo = clsClients.GetClientsByClientID(this.ClientTakenID);


            this._CarTypeInfo = clsCarTypes.GetCarTypeByID(this.CarType);
        }
        private bool _AddNewCar()
        {
            //return the Current SDTO
            this.CarID = clsDataCarCaontainer.AddNewCar(carContainerDTO);
            return (this.CarID != -1);


        }
        private bool _UpdateCar()
        {                                       //return the Current SDTO
            return clsDataCarCaontainer.UpdateCar(carContainerDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCar())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateCar();

            }

            return false;
        }

        public static clsCarContainer GetCarByCarID(int CarID)
        {
            //return Student DTO Data 
            CarContainerDTO carContainerDTO = clsDataCarCaontainer.GetCarByID(CarID);
            if (carContainerDTO != null)
            {

                return new clsCarContainer(carContainerDTO, enMode.Update);
            }
            else
                return null;
        }



        public static clsCarContainer GetCarByCarPlateNumber(string CarPlateNumber)
        {
            //return Student DTO Data 
            CarContainerDTO carContainerDTO = clsDataCarCaontainer.GetCarByCarPlateNumber(CarPlateNumber);
            if (carContainerDTO != null)
            {

                return new clsCarContainer(carContainerDTO, enMode.Update);
            }
            else
                return null;
        }


        public static List<CarContainerDTO> GetAllCars()
        {

            return clsDataCarCaontainer.GetAllCars();
        }



        public static bool DeleteCarByID(int CarID)
        {
            return clsDataCarCaontainer.DeleteCar(CarID);
        }
        public static int IsCarExistByID(int CarID)
        {
            return clsDataCarTypes.IsCarTypeExistByID(CarID);
        }
        public static int IsCarExistByCarPlateNumber(string CarPlateNumber)
        {
            return clsDataCarCaontainer.IsCarExistByCarPlateNumber(CarPlateNumber);
        }


    }
}

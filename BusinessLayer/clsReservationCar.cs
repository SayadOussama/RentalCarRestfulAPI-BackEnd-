using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
      public class clsReservationCar
    {
        public enum enMode { AddNew = 1, Update = 2 }

        public enMode Mode = enMode.AddNew;
        public int ReservationID { get; set; }
        public int CarSelectedID { get; set; }
        public int ClientID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime DateToCheckOut { get; set; }
        public DateTime DateToCheckIn { get; set; }
        public int KLMTSpend { get; set; }
        public decimal TotalRentalFee { get; set; }
        public decimal DamageCostfee { get; set; }

        public string Note { get; set; }
        public bool CarIsReturn { get; set; }
        public int CreatedByUserID { get; set; }
        public clsPerson _PersonInfo;
        public clsClients _ClientInfo;
        public clsCarContainer _CarContainer;

        public ReservationCarDTO reservationCarDTO
        {
            get
            {
                return (new ReservationCarDTO(this.ReservationID, this.CarSelectedID, this.ClientID, this.ReservationDate, this.DateToCheckOut, this.DateToCheckIn, this.KLMTSpend, this.TotalRentalFee, this.DamageCostfee, this.Note, this.CarIsReturn, this.CreatedByUserID));
            }
        }
        public clsReservationCar(ReservationCarDTO reservationCarDTO, enMode cMode = enMode.AddNew)
        {
            this.ReservationID = reservationCarDTO.ReservationID;
            this.CarSelectedID = reservationCarDTO.CarSelectedID;
            this.ClientID = reservationCarDTO.ClientID;
            this.ReservationDate = reservationCarDTO.ReservationDate;
            this.DateToCheckOut = reservationCarDTO.DateToCheckOut;
            this.DateToCheckIn = reservationCarDTO.DateToCheckIn;
            this.KLMTSpend = reservationCarDTO.KLMTSpend;
            this.TotalRentalFee = reservationCarDTO.TotalRentalFee;
            this.DamageCostfee = reservationCarDTO.DamageCostfee;
            this.Note = reservationCarDTO.Note;
            this.CarIsReturn = reservationCarDTO.CarIsReturn;
            this.CreatedByUserID = reservationCarDTO.CreatedByUserID;
            Mode = cMode;
            this._ClientInfo = clsClients.GetClientsByClientID(this.ClientID);
            
            this._CarContainer = clsCarContainer.GetCarByCarID(this.CarSelectedID);
        }

        private bool _AddNewReservationCar()
        {
            //return the Current SDTO
            this.ReservationID = clsDataReservationCar.AddNewReservationCar(reservationCarDTO);
            return (this.ReservationID != -1);


        }
        private bool _UpdateReservation()
        {                                       //return the Current SDTO
            return clsDataReservationCar.UpdateReservationCar(reservationCarDTO);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewReservationCar())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateReservation();

            }

            return false;
        }

        public static clsReservationCar GetReservationCarByID(int ReservationID)
        {
            //return Student DTO Data 
            ReservationCarDTO reservationCarDTO = clsDataReservationCar.GetReservationCarByID(ReservationID);
            if (reservationCarDTO != null)
            {

                return new clsReservationCar(reservationCarDTO, enMode.Update);
            }
            else
                return null;
        }



        public static clsReservationCar GetReservationCarBySelectedCarID(int SelectedCarID)
        {
            //return Student DTO Data 
            ReservationCarDTO reservationCarDTO = clsDataReservationCar.GetReservationCarByCarSelectedID(SelectedCarID);
            if (reservationCarDTO != null)
            {

                return new clsReservationCar(reservationCarDTO, enMode.Update);
            }
            else
                return null;
        }
        public static clsReservationCar GetReservationCarByClientID(int ClientID)
        {
            //return Student DTO Data 
            ReservationCarDTO reservationCarDTO = clsDataReservationCar.GetReservationCarByClientID(ClientID);
            if (reservationCarDTO != null)
            {

                return new clsReservationCar(reservationCarDTO, enMode.Update);
            }
            else
                return null;
        }
        

        public static List<ReservationCarDTO> GetAllReservation()
        {

            return clsDataReservationCar.GetAllReservation();
        }



        public static bool DeleteReservationByID(int ReservationID)
        {
            return clsDataReservationCar.DeleteReservationCar(ReservationID);
        }
        public static int IsReservationExistByID(int ReservationID)
        {
            return clsDataReservationCar.IsReservationExistByID(ReservationID);
        }
        public static int IsCarReturnByID(int ReservationID)
        {
            return clsDataReservationCar.IsCarReturnByID(ReservationID);
        }

    }
}

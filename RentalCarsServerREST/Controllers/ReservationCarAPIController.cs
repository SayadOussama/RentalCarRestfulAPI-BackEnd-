using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace RentalCarsServerREST.Controllers
{
    [ApiController] 
    [Route("api/ReservationCar")]
    public class ReservationCarAPIController : Controller
    {
        [HttpGet("GetAllReservations", Name = "GetAllReservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CarContainerDTO>> GetAllReservations()
        {

            List<ReservationCarDTO> reservationCarDTOs = BusinessLayer.clsReservationCar.GetAllReservation();
            if (reservationCarDTOs.Count == 0)
            {
                return NotFound("No Car Types Found");
            }

            return Ok(reservationCarDTOs);

        }



        [HttpGet("GetReservationByID{ReservationID}", Name = "GetReservationByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ReservationCarDTO> GetReservationByID(int ReservationID)
        {

            if (ReservationID < 1)
            {
                return BadRequest($"Not accepted ID {ReservationID}");
            }


            BusinessLayer.clsReservationCar ReservationInfo = BusinessLayer.clsReservationCar.GetReservationCarByID(ReservationID);

            if (ReservationInfo == null)
            {
                return NotFound($"Reservation  with  ID {ReservationID} not found.");
            }

            ReservationCarDTO reservationCarDTO = ReservationInfo.reservationCarDTO;

            
            return Ok(reservationCarDTO);

        }
        [HttpGet("getReservationByClientID{ClientID}", Name = "GetReservationByClientID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ReservationCarDTO> GetReservationByClientID(int ClientID)
        {

            if (ClientID < 1)
            {
                return BadRequest($"Not accepted ID {ClientID}");
            }


            BusinessLayer.clsReservationCar ReservationInfo = BusinessLayer.clsReservationCar.GetReservationCarByClientID(ClientID);

            if (ReservationInfo == null)
            {
                return NotFound($"Reservation  with  CLient ID {ClientID} not found.");
            }

            
            ReservationCarDTO reservationCarDTO = ReservationInfo.reservationCarDTO;

            
            return Ok(reservationCarDTO);

        }
        [HttpGet("GetReservationByCarSelectedID{CarSelectedID}", Name = "GetReservationByCarSelectedID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ReservationCarDTO> GetReservationByCarSelectedID(int CarSelectedID)
        {

            if (CarSelectedID < 1)
            {
                return BadRequest($"Not accepted ID {CarSelectedID}");
            }


            BusinessLayer.clsReservationCar ReservationInfo = BusinessLayer.clsReservationCar.GetReservationCarBySelectedCarID(CarSelectedID);

            if (ReservationInfo == null)
            {
                return NotFound($"Reservation  with  ID {CarSelectedID} not found.");
            }

            //here we get only the DTO object to send it back.
            ReservationCarDTO reservationCarDTO = ReservationInfo.reservationCarDTO;

            //we return the DTO not the student object.
            return Ok(reservationCarDTO);

        }

        [HttpPost("AddReservation", Name = "PostReservation")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsReservationCar> AddNewReservation(ReservationCarDTO newReservationCarDTO)
        {
            //we validate the data here
            if (newReservationCarDTO == null || newReservationCarDTO.ReservationID == -1 || newReservationCarDTO.CarSelectedID ==-1 || newReservationCarDTO.ClientID == -1||
                newReservationCarDTO.ReservationDate == DateTime.MinValue || newReservationCarDTO.DateToCheckOut == DateTime.MinValue || newReservationCarDTO.DateToCheckIn == DateTime.MinValue ||
                newReservationCarDTO.KLMTSpend == -1 || newReservationCarDTO.TotalRentalFee == 0|| newReservationCarDTO.CarIsReturn ==null ||
                newReservationCarDTO.CreatedByUserID == -1)

            {
                return BadRequest("Invalid Car data.");
            }
            BusinessLayer.clsReservationCar NewReservation = new BusinessLayer.clsReservationCar(new ReservationCarDTO(newReservationCarDTO.ReservationID
                , newReservationCarDTO.CarSelectedID, newReservationCarDTO.ClientID, newReservationCarDTO.ReservationDate,

                newReservationCarDTO.DateToCheckOut, newReservationCarDTO.DateToCheckIn, newReservationCarDTO.KLMTSpend, newReservationCarDTO.TotalRentalFee,
                    newReservationCarDTO.DamageCostfee, newReservationCarDTO.Note, newReservationCarDTO.CarIsReturn, newReservationCarDTO.CreatedByUserID));
            NewReservation.Save();

            newReservationCarDTO.ReservationID = NewReservation.ReservationID;

            
            return CreatedAtRoute("GetReservationByID", new { ReservationID = newReservationCarDTO.ReservationID }, newReservationCarDTO);

        }

        [HttpPut("UpdateResercationCar/{ReservationID}", Name = "PutResercationCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarContainerDTO> UpdateReservationCar(int ReservationID, ReservationCarDTO newReservationCarDTO)
        {
            if (newReservationCarDTO == null || newReservationCarDTO.ReservationID == -1 || newReservationCarDTO.CarSelectedID == -1 || newReservationCarDTO.ClientID == -1 ||
                   newReservationCarDTO.ReservationDate == DateTime.MinValue || newReservationCarDTO.DateToCheckOut == DateTime.MinValue || newReservationCarDTO.DateToCheckIn == DateTime.MinValue ||
                   newReservationCarDTO.KLMTSpend == -1 || newReservationCarDTO.TotalRentalFee == 0 || newReservationCarDTO.CarIsReturn == null ||
                   newReservationCarDTO.CreatedByUserID == -1)

            {
                return BadRequest("Invalid Car data.");
            }





           
            BusinessLayer.clsReservationCar UpdateReservation = BusinessLayer.clsReservationCar.GetReservationCarByID(ReservationID);


            if (UpdateReservation == null)
            {
                return NotFound($"Reservation Car with ID {ReservationID} not found.");
            }



            UpdateReservation.CarSelectedID = newReservationCarDTO.CarSelectedID;
            UpdateReservation.ClientID = newReservationCarDTO.ClientID;
            UpdateReservation.ReservationDate = newReservationCarDTO.ReservationDate;
            UpdateReservation.DateToCheckOut = newReservationCarDTO.DateToCheckOut;
            UpdateReservation.DateToCheckIn = newReservationCarDTO.DateToCheckIn;
            UpdateReservation.KLMTSpend = newReservationCarDTO.KLMTSpend;
            UpdateReservation.TotalRentalFee = newReservationCarDTO.TotalRentalFee;
            UpdateReservation.DamageCostfee = newReservationCarDTO.DamageCostfee;
            UpdateReservation.Note = newReservationCarDTO.Note;
            UpdateReservation.CarIsReturn = newReservationCarDTO.CarIsReturn;
            UpdateReservation.CreatedByUserID = newReservationCarDTO.CreatedByUserID;




            UpdateReservation.Save();

           
            return Ok(UpdateReservation.reservationCarDTO);

        }

        [HttpDelete("{ReservationID}", Name = "DeleteReserbationByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteReservationByID(int ReservationID)
        {
            if (ReservationID < 1)
            {
                return BadRequest($"Not accepted ID {ReservationID}");
            }

            

            if (BusinessLayer.clsReservationCar.DeleteReservationByID(ReservationID))

                return Ok($"Reservation with ID {ReservationID} has been deleted.");
            else
                return NotFound($"Reservation with ID {ReservationID} not found. no rows deleted!");
        }


        [HttpGet("IsReservationExistByID{ReservationID}", Name = "GetIsReservationExistByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> GetIsReservationExistByID(int ReservationID)
        {
            int IsExist = BusinessLayer.clsReservationCar.IsReservationExistByID(ReservationID);
            return Ok(IsExist);
        }

        [HttpGet("IsCarReturnReservationByID{ReservationID}", Name = "GetIsCarReturnReservationByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> GetIsCarReturnReservationByID(int ReservationID)
        {
            int IsExist = BusinessLayer.clsReservationCar.IsCarReturnByID(ReservationID);
            return Ok(IsExist);
        }














     
    }
}

using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace RentalCarsServerREST.Controllers
{
    [ApiController] 
    [Route("api/CarContainer")]
    public class CarContainerAPIController : Controller
    {
        [HttpGet("GetAllCarContainer", Name = "GetAllCarContainer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CarContainerDTO>> GetAllCars()
        {

            List<CarContainerDTO> CarContainerDTOs = BusinessLayer.clsCarContainer.GetAllCars();
            if (CarContainerDTOs.Count == 0)
            {
                return NotFound("No Car Types Found");
            }

            return Ok(CarContainerDTOs);

        }



        [HttpGet("GetCarByCarID/{CarID}", Name = "getCarByCarID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<CarContainerDTO> GetCarByID(int CarID)
        {

            if (CarID < 1)
            {
                return BadRequest($"Not accepted ID {CarID}");
            }


            BusinessLayer.clsCarContainer CarInfo = BusinessLayer.clsCarContainer.GetCarByCarID(CarID);

            if (CarInfo == null)
            {
                return NotFound($"car with  ID {CarID} not found.");
            }

       
            CarContainerDTO CarTypesDTO = CarInfo.carContainerDTO;

            
            return Ok(CarTypesDTO);

        }

        [HttpGet("GetCarByPlateNumber/{CarPlateNumber}", Name = "CarByPlateNumber")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<CarContainerDTO> GetCarByPlateNumber(string  CarPlateNumber)
        {

            if (CarPlateNumber == string.Empty)
            {
                return BadRequest($"Not accepted Plat Number {CarPlateNumber}");
            }


            BusinessLayer.clsCarContainer CarInfo = BusinessLayer.clsCarContainer.GetCarByCarPlateNumber(CarPlateNumber);

            if (CarInfo == null)
            {
                return NotFound($"car with  Plat Number {CarPlateNumber} not found.");
            }

            CarContainerDTO CarTypesDTO = CarInfo.carContainerDTO;

          
            return Ok(CarTypesDTO);

        }


        [HttpGet("IsCarExistByID/{Id}", Name = "IsCarExistByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> IsCarExistByID(int Id)
        {
            //var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);
            int IsExist = BusinessLayer.clsCarContainer.IsCarExistByID(Id);
            return Ok(IsExist);
        }



        [HttpGet("IsCarExistByPlatNumber/{CarPlateNumber}", Name = "IsCarExistByPlatNumber")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<int> IsCarExistByPlatNumber(string CarPlateNumber)
        {
            //var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);
            int IsExist = BusinessLayer.clsCarContainer.IsCarExistByCarPlateNumber(CarPlateNumber);
            return Ok(IsExist);
        }


        [HttpPost("AddNewCar", Name = "PostAddNewCar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<clsCarContainer> AddNewCar(CarContainerDTO newCarContainerDTO)
        {
            //we validate the data here
            if (newCarContainerDTO == null || newCarContainerDTO.CarID == -1 || newCarContainerDTO.CarPlateNumber == string.Empty ||
                newCarContainerDTO.DoorsNumber == 0 || newCarContainerDTO.Color == string.Empty || newCarContainerDTO.IsAvailable == null ||
                newCarContainerDTO.CarModel == string.Empty || newCarContainerDTO.CarName == string.Empty || newCarContainerDTO.CurrentKLMT <0 ||
                newCarContainerDTO.CarType==-1|| newCarContainerDTO.EngineModel==string.Empty||newCarContainerDTO.ImagePath==string.Empty||newCarContainerDTO.RentalCarPrice==0)

            {
                return BadRequest("Invalid Car data.");
            }
            


            BusinessLayer.clsCarContainer NewCar = new BusinessLayer.clsCarContainer(new CarContainerDTO(newCarContainerDTO.CarID, newCarContainerDTO.CarName, newCarContainerDTO.CarModel, newCarContainerDTO.CarType,

                newCarContainerDTO.EngineModel, newCarContainerDTO.CarPlateNumber, newCarContainerDTO.RentalCarPrice, newCarContainerDTO.Color,
                    newCarContainerDTO.DoorsNumber , newCarContainerDTO.ImagePath,         newCarContainerDTO.CurrentKLMT, newCarContainerDTO.IsAvailable, newCarContainerDTO.ClientTakenID, newCarContainerDTO.CreatedByUserID));
            NewCar.Save();

            newCarContainerDTO.CarID = NewCar.CarID;

            //we return the DTO only not the full student object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("getCarByCarID", new { CarID = newCarContainerDTO.CarID }, newCarContainerDTO);

        }

        [HttpPut("UpdateCar/{CarID}", Name = "PutUpdateCar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarContainerDTO> UpdateClient(int CarID, CarContainerDTO newCarContainerDTO)
        {
            if (newCarContainerDTO == null || newCarContainerDTO.CarID == -1 || newCarContainerDTO.CarPlateNumber == string.Empty ||
              newCarContainerDTO.DoorsNumber == 0 || newCarContainerDTO.Color == string.Empty || newCarContainerDTO.IsAvailable == null ||
              newCarContainerDTO.CarModel == string.Empty || newCarContainerDTO.CarName == string.Empty || newCarContainerDTO.CurrentKLMT < 0 ||
              newCarContainerDTO.CarType == -1 || newCarContainerDTO.EngineModel == string.Empty || newCarContainerDTO.ImagePath.IsNullOrEmpty() || newCarContainerDTO.RentalCarPrice == 0)

            {
                return BadRequest("Invalid Car data.");
            }





            BusinessLayer.clsCarContainer CarInfo = BusinessLayer.clsCarContainer.GetCarByCarID(CarID);


            if (CarInfo == null)
            {
                return NotFound($"Car with ID {CarID} not found.");
            }


            
            CarInfo.CarName = newCarContainerDTO.CarName;
            CarInfo.CarModel = newCarContainerDTO.CarModel;
            CarInfo.CarType = newCarContainerDTO.CarType;
            CarInfo.EngineModel = newCarContainerDTO.EngineModel;
            CarInfo.CarPlateNumber = newCarContainerDTO.CarPlateNumber;
            CarInfo.RentalCarPrice = newCarContainerDTO.RentalCarPrice;
            CarInfo.Color = newCarContainerDTO.Color;
            CarInfo.DoorsNumber = newCarContainerDTO.DoorsNumber;
            CarInfo.ImagePath = newCarContainerDTO.ImagePath;
            CarInfo.CurrentKLMT = newCarContainerDTO.CurrentKLMT;
            CarInfo.IsAvailable = newCarContainerDTO.IsAvailable;
            CarInfo.ClientTakenID = newCarContainerDTO.ClientTakenID;
            CarInfo.CreatedByUserID = newCarContainerDTO.CreatedByUserID;


            CarInfo.Save();

            //we return the DTO not the full student object.
            return Ok(CarInfo.carContainerDTO);

        }

        [HttpDelete("{CarID}", Name = "DeleteCarByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCar(int CarID)
        {
            if (CarID < 1)
            {
                return BadRequest($"Not accepted ID {CarID}");
            }

           

            if (BusinessLayer.clsCarContainer.DeleteCarByID(CarID))

                return Ok($"Car with ID {CarID} has been deleted.");
            else
                return NotFound($"Client with ID {CarID} not found. no rows deleted!");
        }

        [HttpGet("IsCarExistByCarID{CarID}", Name = "GetIsCarExistByCarID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> GetIsCarExistByCarID(int CarID)
        {
            
            int IsExist = BusinessLayer.clsCarContainer.IsCarExistByID(CarID); 
            return Ok(IsExist);
        }
        [HttpGet("IsCarExistByCarPlateNumber/{CarPlateNumber}", Name = "GetIsCarExistByCarPlateNumber")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> GetIsCarExistByCarID(string CarPlateNumber)
        {
        
            int IsExist = BusinessLayer.clsCarContainer.IsCarExistByCarPlateNumber(CarPlateNumber);
            return Ok(IsExist);
        }

    }
}

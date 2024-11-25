using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace RentalCarsServerREST.Controllers
{
    [ApiController] 
    [Route("api/CarTypes")]
    public class CarTypesAPIController : Controller
    {
        [HttpGet("GetAllCarTypes", Name = "GetAllCarTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CarTypeDTO>> GetAllCarTypes()
        {

            List<CarTypeDTO> CarTypesDTOs = BusinessLayer.clsCarTypes.GetAllCarType();
            if (CarTypesDTOs.Count == 0)
            {
                return NotFound("No Car Types Found");
            }

            return Ok(CarTypesDTOs);

        }
        [HttpGet("getCarTypesByCarTypeID/{CarTypeID}", Name = "getCarTypesByCarTypeID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<CarTypeDTO> GetCarTypesByCarTypeID(int CarTypeID)
        {

            if (CarTypeID < 1)
            {
                return BadRequest($"Not accepted ID {CarTypeID}");
            }


            BusinessLayer.clsCarTypes CarTypes = BusinessLayer.clsCarTypes.GetCarTypeByID(CarTypeID);

            if (CarTypes == null)
            {
                return NotFound($"CarType with  ID {CarTypeID} not found.");
            }

          
            CarTypeDTO CarTypesDTO = CarTypes.CarTypeDTO;

         
            return Ok(CarTypesDTO);

        }
        [HttpGet("getCarTypesByTypeName/{TypeName}", Name = "getCarTypesByTypeName")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<CarTypeDTO> GetCarTypesByTypeName(string TypeName)
        {

            if (TypeName.IsNullOrEmpty())
            {
                return BadRequest($"Not accepted TypeName {TypeName}");
            }


            BusinessLayer.clsCarTypes CarTypes = BusinessLayer.clsCarTypes.GetCarTypeByTypeName(TypeName);

            if (CarTypes == null)
            {
                return NotFound($"CarType with  TypeName {CarTypes} not found.");
            }

            //here we get only the DTO object to send it back.
            CarTypeDTO CarTypesDTO = CarTypes.CarTypeDTO;

            //we return the DTO not the student object.
            return Ok(CarTypesDTO);

        }

        [HttpPost("AddNewCarType", Name = "PostAddNewCarType")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ClientsDTO> AddNewClinet(CarTypeDTO newCarTypeDTO)
        {
            //we validate the data here
            if (newCarTypeDTO == null || newCarTypeDTO.CarTypeID == -1 || newCarTypeDTO.TypeName == string.Empty || newCarTypeDTO.CreatedByUserID == -1 )

            {
                return BadRequest("Invalid CarTypes data.");
            }
            if (clsCarTypes.GetCarTypeByID(newCarTypeDTO.CarTypeID) != null)
            {
                return BadRequest($"CarType With ID {newCarTypeDTO.CarTypeID} Is AllReady Exist .");
            }


            BusinessLayer.clsCarTypes CarType = new BusinessLayer.clsCarTypes(new CarTypeDTO(newCarTypeDTO.CarTypeID, newCarTypeDTO.TypeName, newCarTypeDTO.CreatedByUserID));
            CarType.Save();

            newCarTypeDTO.CarTypeID = CarType.CarTypeID;

           
            return CreatedAtRoute("getCarTypesByCarTypeID", new { CarTypeID = newCarTypeDTO.CarTypeID }, newCarTypeDTO);

        }

       
        [HttpPut("UpdateCarType/{CarTypeID}", Name = "PutUpdateCarType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarTypeDTO> UpdateCarType(int CarTypeID, CarTypeDTO newCarTypeDTO)
        {
            if (newCarTypeDTO == null || newCarTypeDTO.CarTypeID == -1 || newCarTypeDTO.TypeName == string.Empty || newCarTypeDTO.CreatedByUserID == -1)

            {
                return BadRequest("Invalid Car Type data.");
            }





            BusinessLayer.clsCarTypes carType = BusinessLayer.clsCarTypes.GetCarTypeByID(CarTypeID);


            if (carType == null)
            {
                return NotFound($"Client with ID {CarTypeID} not found.");
            }


            carType.CarTypeID = newCarTypeDTO.CarTypeID;
            carType.TypeName = newCarTypeDTO.TypeName;
            carType.CreatedByUserID = newCarTypeDTO.CreatedByUserID;


            carType.Save();

            
            return Ok(carType.CarTypeDTO);

        }

        [HttpDelete("{CarTypeID}", Name = "DeleteCarType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCarType(int CarTypeID)
        {
            if (CarTypeID < 1)
            {
                return BadRequest($"Not accepted ID {CarTypeID}");
            }

           

            if (BusinessLayer.clsCarTypes.DeleteCarTypeByID(CarTypeID))

                return Ok($"CarType with ID {CarTypeID} has been deleted.");
            else
                return NotFound($"CarType with ID {CarTypeID} not found. no rows deleted!");
        }















    }
}

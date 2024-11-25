using DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RentalCarsServerREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesAPIController : ControllerBase
    {
        
        [HttpGet("AllCountries", Name = "GetAllPerson")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CountriesDTO>> GetAllCountries()
        {

            List<CountriesDTO> CountriesList = BusinessLayer.clsCountries.GetAllCountriesList();
            if (CountriesList.Count == 0)
            {
                return NotFound("No Students Found");
            }

            return Ok(CountriesList);

        }

    }
}

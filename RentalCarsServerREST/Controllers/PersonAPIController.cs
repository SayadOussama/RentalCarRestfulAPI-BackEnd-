using DataLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace RentalCarsServerREST.Controllers
{
    [ApiController] 
    [Route("api/Person")]

    public class PersonAPIController : Controller
    {

        






        [HttpGet("AllPeople", Name = "GetAll")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonDTO>> GetAllPeople()
        {

            List<PersonDTO> personDTOs = BusinessLayer.clsPerson.GetAllPeople();
            if (personDTOs.Count == 0)
            {
                return NotFound("No Students Found");
            }

            return Ok(personDTOs);

        }

        [HttpGet("{PersonID}", Name = "GetPersonInfoByID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<PersonDTO> GetPersonInfoByID(int PersonID)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }

            
            BusinessLayer.clsPerson Person = BusinessLayer.clsPerson.GetPersonInfoByID(PersonID);

            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }

            //here we get only the DTO object to send it back.
            PersonDTO PDTO = Person.PDTO;

            //we return the DTO not the student object.
            return Ok(PDTO);

        }
        [HttpGet(" GetPersonInfoByNationalNo/{NationalNo}", Name = "GetPersonByNationalNo")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<PersonDTO> GetPersonInfoByNationalNo(string NationalNo)
        {

            if (NationalNo == string.Empty)
            {
                return BadRequest($"Not accepted National No {NationalNo}");
            }

            
            BusinessLayer.clsPerson Person = BusinessLayer.clsPerson.GetPersonIDByNationalNo(NationalNo);

            if (Person == null)
            {
                return NotFound($"NatonalNo with Num {NationalNo} not found.");
            }

        
            PersonDTO PDTO = Person.PDTO;

            return Ok(PDTO);

        }
        [HttpGet(" GetPersonInfoByFirstName/{FirstName}", Name = "GetPersonByFirstName")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<PersonDTO> GetPersonInfoByFirstName(string FirstName)
        {

            if (FirstName == string.Empty)
            {
                return BadRequest($"Not accepted First Name {FirstName}");
            }


            BusinessLayer.clsPerson Person = BusinessLayer.clsPerson.GetPersonByFirstName(FirstName);

            if (Person == null)
            {
                return NotFound($"NatonalNo with Name {FirstName} not found.");
            }

           
            PersonDTO PDTO = Person.PDTO;

            return Ok(PDTO);

        }

        [HttpGet(" GetPersonInfoByLastName/{LastName}", Name = "GetPersonByLastName")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<PersonDTO> GetPersonInfoByLastName(string LastName)
        {

            if (LastName == string.Empty)
            {
                return BadRequest($"Not accepted LastName {LastName}");
            }


            BusinessLayer.clsPerson Person = BusinessLayer.clsPerson.GetPersonByLastName(LastName);

            if (Person == null)
            {
                return NotFound($"NatonalNo with Name {LastName} not found.");
            }

           
            PersonDTO PDTO = Person.PDTO;

    
            return Ok(PDTO);

        }
      
        [HttpGet("IsPersonExistByID{PersonID}", Name = "IsPersonExist")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> IsPersonExist(int PersonID)
        {
           
            int IsExist = BusinessLayer.clsPerson.IsPersonExist(PersonID);
            return Ok(IsExist);
        }












        [HttpPost(Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PersonDTO> AddNewPerson(PersonDTO newPersonDTO)
        {
            //we validate the data here
            if (newPersonDTO == null || string.IsNullOrEmpty(newPersonDTO.FirstName) || string.IsNullOrEmpty(newPersonDTO.LastName) || newPersonDTO.BirthDay==DateTime.MinValue || newPersonDTO.Gender == 99 || string.IsNullOrEmpty(newPersonDTO.PhoneNumber)||
                string.IsNullOrEmpty(newPersonDTO.address)|| newPersonDTO.NationalCountryID ==-1 )
            {
                return BadRequest("Invalid student data.");
            }

           

            BusinessLayer.clsPerson Person = new BusinessLayer.clsPerson(new PersonDTO(newPersonDTO.PersonID, newPersonDTO.NationalNO, newPersonDTO.FirstName, newPersonDTO.LastName ,newPersonDTO.BirthDay, newPersonDTO.Gender, newPersonDTO.PhoneNumber,  newPersonDTO.address, newPersonDTO.Email, newPersonDTO.NationalCountryID, newPersonDTO.ImagePath));
            Person.Save();

            newPersonDTO.PersonID = Person.PersonID;

           
            return CreatedAtRoute("GetPersonInfoByID", new { PersonID = newPersonDTO.PersonID }, newPersonDTO);

        }
      






        //here we use http put method for update
        [HttpPut("{PersonID}", Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PersonDTO> UpdateStudent(int PersonID, PersonDTO updatedPerson)
        {
            if (updatedPerson == null || string.IsNullOrEmpty(updatedPerson.FirstName) || string.IsNullOrEmpty(updatedPerson.LastName) || updatedPerson.BirthDay == DateTime.MinValue || updatedPerson.Gender == 99 || string.IsNullOrEmpty(updatedPerson.PhoneNumber) ||
                string.IsNullOrEmpty(updatedPerson.address) || updatedPerson.NationalCountryID == -1)
            {
                return BadRequest("Invalid student data.");
            }





            //We Must Find the Person First 
            BusinessLayer.clsPerson Person = BusinessLayer.clsPerson.GetPersonInfoByID( PersonID);


            if (Person == null)
            {
                return NotFound($"Person with ID {PersonID} not found.");
            }



            Person.NationalNO = updatedPerson.NationalNO;
            Person.FirstName = updatedPerson.FirstName;
            Person.LastName = updatedPerson.LastName;
            Person.BirthDay = updatedPerson.BirthDay;
            Person.Gender = updatedPerson.Gender;
            Person.PhoneNumber = updatedPerson.PhoneNumber;
            Person.address = updatedPerson.address;
            Person.Email = updatedPerson.Email;
            Person.NationalCountryID = updatedPerson.NationalCountryID;
            Person.ImagePath = updatedPerson.ImagePath;

            Person.Save();

            //we return the DTO not the full student object.
            return Ok(Person.PDTO);

        }

        [HttpDelete("{id}", Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeletePerson(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

           
            if (BusinessLayer.clsPerson.DeletePersonByID(id))

                return Ok($"Person with ID {id} has been deleted.");
            else
                return NotFound($"Person with ID {id} not found. no rows deleted!");
        }






    }
}

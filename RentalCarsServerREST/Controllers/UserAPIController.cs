using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace RentalCarsServerREST.Controllers
{

    [ApiController] 
    [Route("api/Users")]
    public class UserAPIController : Controller
    {


        [HttpGet("AllUsers", Name = "GetAllUsers")]

       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PersonDTO>> GetAllPeople()
        {

            List<UsersDTO> usersDTOs = BusinessLayer.clsUsers.GetAllUsers();
            if (usersDTOs.Count == 0)
            {
                return NotFound("No Students Found");
            }

            return Ok(usersDTOs);

        }

        [HttpGet("getUser{PersonID}", Name = "getUserInfoByPersonID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<UsersDTO> GetUserInfoByPersonID(int PersonID)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }


            BusinessLayer.clsUsers User = BusinessLayer.clsUsers.GetUserByPersonID(PersonID);

            if (User == null)
            {
                return NotFound($"User with Person ID {PersonID} not found.");
            }

            UsersDTO UDTO = User.UDTO;

            return Ok(UDTO);

        }








        [HttpGet("get/{UserID}", Name = "getUserByUserID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<UsersDTO> GetUserByUserID(int UserID)
        {

            if (UserID < 1)
            {
                return BadRequest($"Not accepted ID {UserID}");
            }


            BusinessLayer.clsUsers User = BusinessLayer.clsUsers.GetUserByUserID(UserID);

            if (User == null)
            {
                return NotFound($"User with Person ID {UserID} not found.");
            }

            //here we get only the DTO object to send it back.
            UsersDTO UDTO = User.UDTO;

            //we return the DTO not the student object.
            return Ok(UDTO);

        }

        [HttpGet("byUsernameAndPassword{UserName}/{PassWord}", Name = "GetUserInfoByUserNameAndPassWord")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<UsersDTO> GetUserByUserNameAndPassWord(string UserName, string PassWord)
        {

            if (UserName == "" && PassWord == "")
            {
                return BadRequest($" UserName and PassWord Not accepted");
            }

            //var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            //if (student == null)
            //{
            //    return NotFound($"Student with ID {id} not found.");
            //}
            BusinessLayer.clsUsers User = BusinessLayer.clsUsers.GetUserByUserNameAndPassWord(UserName, PassWord);

            if (User == null)
            {
                return NotFound($"User with  ID {UserName} and {PassWord} not found.");
            }

            UsersDTO UDTO = User.UDTO;

          
            return Ok(UDTO);

        }








     
        [HttpPost("addNewUser",Name = "PostAddNewUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UsersDTO> AddNewUser(UsersDTO newUserDTO)
        {
            //we validate the data here
            if (newUserDTO == null || string.IsNullOrEmpty(newUserDTO.UserName) || newUserDTO.PersonID == -1 || string.IsNullOrEmpty(newUserDTO.PassWord))

            {
                return BadRequest("Invalid User data.");
            }
            if(clsUsers.GetUserByPersonID(newUserDTO.PersonID) != null)
            {
                return BadRequest($"user With PersonID {newUserDTO.PersonID} Is AllReady Exist .");
            }
           

            BusinessLayer.clsUsers User = new BusinessLayer.clsUsers(new UsersDTO(newUserDTO.UserID, newUserDTO.PersonID, newUserDTO.UserName, newUserDTO.PassWord, newUserDTO.IsActive));
            User.Save();

            newUserDTO.UserID = User.UserID;

         
            return CreatedAtRoute("getUserByUserID", new { UserID = newUserDTO.UserID }, newUserDTO);

        }







    
        [HttpPut("PutUser/{UserID}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UsersDTO> UpdateUser(int UserID, UsersDTO updatedUser)
        {
            if (updatedUser == null || updatedUser.PersonID == -1 || string.IsNullOrEmpty(updatedUser.UserName) || string.IsNullOrEmpty(updatedUser.PassWord))

            {
                return BadRequest("Invalid User data.");
            }





          
            BusinessLayer.clsUsers User = BusinessLayer.clsUsers.GetUserByUserID(UserID);


            if (User == null)
            {
                return NotFound($"User with ID {UserID} not found.");
            }


            User.PersonID = updatedUser.PersonID;
            User.UserName = updatedUser.UserName;
            User.PassWord = updatedUser.PassWord;
            User.IsActive = updatedUser.IsActive;


            User.Save();

            return Ok(User.UDTO);

        }

     

    }
}

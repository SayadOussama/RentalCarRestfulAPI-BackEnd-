using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace RentalCarsServerREST.Controllers
{
    [ApiController] 
    [Route("api/Clients")]
    public class ClientsAPIController : Controller
    {
        [HttpGet("AllClients", Name = "GetAllClients")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ClientsDTO>> GetAllClients()
        {

            List<ClientsDTO> ClientsDTOs = BusinessLayer.clsClients.GetAllClients();
            if (ClientsDTOs.Count == 0)
            {
                return NotFound("No Students Found");
            }

            return Ok(ClientsDTOs);

        }

        [HttpGet("getClientByPersonID/{PersonID}", Name = "getClientInfoByPersonID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ClientsDTO> GetClientInfoByPersonID(int PersonID)
        {

            if (PersonID < 1)
            {
                return BadRequest($"Not accepted ID {PersonID}");
            }


            BusinessLayer.clsClients Clients = BusinessLayer.clsClients.GetClientsByPersonID(PersonID);

            if (Clients == null)
            {
                return NotFound($"Client with Person ID {PersonID} not found.");
            }

            ClientsDTO CDTO = Clients.CDTO;

            return Ok(CDTO);

        }

        [HttpGet("getClientByClientID/{ClientID}", Name = "getClientInfoByClientID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ClientsDTO> GetClientInfoByClient(int ClientID)
        {

            if (ClientID < 1)
            {
                return BadRequest($"Not accepted ID {ClientID}");
            }


            BusinessLayer.clsClients Clients = BusinessLayer.clsClients.GetClientsByClientID(ClientID);

            if (Clients == null)
            {
                return NotFound($"Client with Client ID {ClientID} not found.");
            }

            //here we get only the DTO object to send it back.
            ClientsDTO CDTO = Clients.CDTO;

            //we return the DTO not the student object.
            return Ok(CDTO);

        }
        [HttpGet("getClientByVehicalLicenseNumber/{VehicalLicenseNumber}", Name = "getClientInfoByVehicalLicenseNumber")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<ClientsDTO> GetClientInfoByVehicalLicenseNumber(int VehicalLicenseNumber)
        {

            if (VehicalLicenseNumber < 1)
            {
                return BadRequest($"Not accepted ID {VehicalLicenseNumber}");
            }


            BusinessLayer.clsClients Clients = BusinessLayer.clsClients.GetClientsByVehicalNumber(VehicalLicenseNumber);

            if (Clients == null)
            {
                return NotFound($"Client with VeicalLicenseNumber {VehicalLicenseNumber} not found.");
            }

            //here we get only the DTO object to send it back.
            ClientsDTO CDTO = Clients.CDTO;

            //we return the DTO not the student object.
            return Ok(CDTO);

        }
        [HttpPost("addNewClient", Name = "PostAddNewClient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ClientsDTO> AddNewClinet(ClientsDTO newClientDTO)
        {
            //we validate the data here
            if (newClientDTO == null || newClientDTO.PersonID == -1 || newClientDTO.VehicalLicenseNumber == -1 || newClientDTO.CreatedByUserID == -1 || newClientDTO.AccountCreationDate== DateTime.MinValue|| newClientDTO.LicenseExpirationDate <= DateTime.Now)

            {
                return BadRequest("Invalid Client data.");
            }
            if (clsClients.GetClientsByPersonID(newClientDTO.PersonID) != null)
            {
                return BadRequest($"Client With PersonID {newClientDTO.PersonID} Is AllReady Exist .");
            }


            BusinessLayer.clsClients Client = new BusinessLayer.clsClients(new ClientsDTO(newClientDTO.ClientID, newClientDTO.PersonID, newClientDTO.VehicalLicenseNumber, newClientDTO.AccountCreationDate, newClientDTO.LicenseExpirationDate, newClientDTO.CreatedByUserID));
            Client.Save();

            newClientDTO.ClientID = Client.ClientID;

            //we return the DTO only not the full student object
            //we dont return Ok here,we return createdAtRoute: this will be status code 201 created.
            return CreatedAtRoute("getClientInfoByClientID", new { ClientID = newClientDTO.ClientID }, newClientDTO);

        }

        //here we use http put method for update
        [HttpPut("PutClient/{ClientID}", Name = "UpdateClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClientsDTO> UpdateClient(int ClientID, ClientsDTO newClientDTO)
        {
            if (newClientDTO == null || newClientDTO.PersonID == -1 || newClientDTO.VehicalLicenseNumber == -1 || newClientDTO.CreatedByUserID == -1 || newClientDTO.AccountCreationDate == DateTime.MinValue || newClientDTO.LicenseExpirationDate <= DateTime.Now)

            {
                return BadRequest("Invalid User data.");
            }





            //We Must Find the User First 
            BusinessLayer.clsClients Client = BusinessLayer.clsClients.GetClientsByClientID(ClientID);


            if (Client == null)
            {
                return NotFound($"Client with ID {ClientID} not found.");
            }


            Client.PersonID = newClientDTO.PersonID;
            Client.VehicalLicenseNumber = newClientDTO.VehicalLicenseNumber;
            Client.AccountCreationDate = newClientDTO.AccountCreationDate;
            Client.LicenseExpirationDate = newClientDTO.LicenseExpirationDate;
            Client.CreatedByUserID = newClientDTO.CreatedByUserID;


            Client.Save();

            //we return the DTO not the full student object.
            return Ok(Client.CDTO);

        }

        [HttpGet("IsClientExistByClientID/{Id}", Name = "IsClientExistByClientID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> IsClientExistByClientID(int Id)
        {
            int IsExist = BusinessLayer.clsClients.IsClientExistByClientID(Id);
            return Ok(IsExist);
        }

        [HttpGet("IsClientExistByPersonID{PersonID}", Name = "IsClientExistByPersonID")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> IsClientExistByPersonID(int PersonID)
        {
            
            int IsExist = BusinessLayer.clsClients.IsClientExistByPersonID(PersonID); ;
            return Ok(IsExist);
        }

        [HttpGet("IsClientExistByVehicalLicenseNumber/{VehicalLicenseNumber}", Name = "IsClientExistByVehicalLicenseNumber")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<int> IsClientExistByVehicalLicenseNumber(int VehicalLicenseNumber)
        {
            int IsExist = BusinessLayer.clsClients.IsClientExistByVehicalLicenseNumber(VehicalLicenseNumber); ;
            return Ok(IsExist);
        }



        [HttpDelete("{ClientID}", Name = "DeleteClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteClient(int ClientID)
        {
            if (ClientID < 1)
            {
                return BadRequest($"Not accepted ID {ClientID}");
            }

            if (BusinessLayer.clsClients.DeleteClientByID(ClientID))

                return Ok($"Client with ID {ClientID} has been deleted.");
            else
                return NotFound($"Client with ID {ClientID} not found. no rows deleted!");
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechChallenge.Application.Models;
using TechChallenge.Application.Interfaces;
using TechChallenge.API.Logging;

namespace TechChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            CustomLogger.IsFile = true;
            _contactService = contactService;
            _logger = logger;
        }

        // GET: api/contacts
        [HttpGet]
        [SwaggerOperation(Summary = "Get all contacts", Description = "Returns a list of all contacts.")]
        [SwaggerResponse(200, "List of contacts retrieved successfully.", typeof(IEnumerable<ContactModel>))]
        public async Task<ActionResult<IEnumerable<ContactModel>>> GetAllContacts()
        {
            _logger.LogInformation("Get all contacts");
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        // GET: api/contacts/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get contact by ID", Description = "Retrieves a contact by its unique ID.")]
        [SwaggerResponse(200, "Contact retrieved successfully.", typeof(ContactModel))]
        [SwaggerResponse(404, "Contact not found.")]
        public async Task<ActionResult<ContactModel>> GetContactById(Guid id)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(id);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/contacts/name/{name}
        [HttpGet("name/{name}")]
        [SwaggerOperation(Summary = "Get contact by name", Description = "Retrieves a contact by its name.")]
        [SwaggerResponse(200, "Contact retrieved successfully.", typeof(ContactModel))]
        [SwaggerResponse(404, "Contact not found.")]
        public async Task<ActionResult<ContactModel>> GetContactByName(string name)
        {
            try
            {
                var contact = await _contactService.GetContactByNameAsync(name);
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/contacts/ddd/{ddd}
        [HttpGet("ddd/{ddd}")]
        [SwaggerOperation(Summary = "Get contacts by DDD", Description = "Retrieves a list of contacts filtered by DDD.")]
        [SwaggerResponse(200, "Contacts retrieved successfully.", typeof(IEnumerable<ContactModel>))]
        [SwaggerResponse(404, "No contacts found for the specified DDD.")]
        public async Task<ActionResult<IEnumerable<ContactModel>>> GetContactsByDdd(string ddd)
        {
            try
            {
                var contacts = await _contactService.GetContactByDddAsync(ddd);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/contacts
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new contact", Description = "Creates a new contact with the provided details.")]
        [SwaggerResponse(201, "Contact created successfully.", typeof(ContactModel))]
        [SwaggerResponse(400, "Invalid input data.")]
        public async Task<ActionResult<ContactModel>> CreateContact([FromBody] ContactModel contactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var result = await _contactService.CreateContactAsync(contactDto);
            return CreatedAtAction(nameof(GetContactById), new { id = result.Id }, result);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update contact", Description = "Updates an existing contact's details by its ID.")]
        [SwaggerResponse(200, "Contact updated successfully.", typeof(ContactModel))]
        [SwaggerResponse(400, "Invalid input data.")]
        [SwaggerResponse(404, "Contact not found.")]
        public async Task<ActionResult<ContactModel>> UpdateContact(Guid id, [FromBody] ContactModel ContactModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _contactService.UpdateContactAsync(id, ContactModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete contact", Description = "Deletes an existing contact by its ID.")]
        [SwaggerResponse(204, "Contact deleted successfully.")]
        [SwaggerResponse(404, "Contact not found.")]
        public async Task<ActionResult> DeleteContact(Guid id)
        {
            try
            {
                await _contactService.DeleteContactAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}

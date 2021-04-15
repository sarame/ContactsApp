using Contacts.Domain.Models;
using Contacts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Contacts.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactsServices _contactsServices;

        public ContactsController(ILogger<ContactsController> logger, IContactsServices contactsServices)
        {
            _logger = logger;
            _contactsServices = contactsServices;
        }

        [Route("insertContact")]
        [HttpPost]
        public async Task<IActionResult> InsertContactAsync([FromBody] Contact record)
        {
            try
            {
                await _contactsServices.AddAsync(record);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to insert new contact";
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        [Route("loadContacts")]
        [HttpGet]
        public async Task<IActionResult> LoadContacts()
        {
            try
            {
                var result = await _contactsServices.AllAsync();
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to load contacts";
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [Route("loadRecordById/{id}")]
        [HttpGet]
        public async Task<IActionResult> LoadRecordById(Guid id)
        {
            try
            {
                var result = await _contactsServices.GetAsync(id);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to load contact";
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }

        [Route("upsertContact/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpsertContact(Guid id, [FromBody] Contact record)
        {
            try
            {
                await _contactsServices.UpdateAsync(record, id);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to update contact";
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
        [Route("deleteContacts/{id}")]
        [HttpGet]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            try
            {
                await _contactsServices.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Failed to delete contact";
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
    }
}

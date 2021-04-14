using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
using Contacts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly ContactsServices _contactsServices;

        public ContactsController(ILogger<ContactsController> logger, ContactsServices contactsServices)
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
                await _contactsServices.Add(record);
                return new OkObjectResult(Ok());
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
        public IActionResult LoadContacts()
        {
            try
            {
                var result = _contactsServices.All();
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
        public IActionResult LoadRecordById(Guid id)
        {
            try
            {
                var result = _contactsServices.Get(id);
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
        public IActionResult UpsertContact(Guid id, [FromBody] Contact record)
        {
            try
            {
                _contactsServices.Update(record, id);
                return new OkObjectResult(Ok());
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
        public IActionResult DeleteContact(Guid id)
        {
            try
            {
                _contactsServices.Delete(id);
                return new OkObjectResult(Ok());
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

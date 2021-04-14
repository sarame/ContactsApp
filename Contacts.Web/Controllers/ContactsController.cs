using Contacts.Domain.Models;
using Contacts.Infrastructure.Repositories;
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
        private static readonly string TableName = "Contacts";
        private readonly ILogger<ContactsController> _logger;
        private readonly IRepository<Contact> _repository;

        public ContactsController(ILogger<ContactsController> logger, IRepository<Contact> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Route("insertContact")]
        [HttpPost]
        public IActionResult InsertContact([FromBody] Contact record)
        {
            try
            {
                _repository.Add(TableName, record);
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
                var result = _repository.All(TableName);
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
                var result = _repository.Get(TableName, id);
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
                _repository.Update(TableName, record, id);
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
                _repository.Delete(TableName, id);
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

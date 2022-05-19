using BlazorFormsAPI.Models;
using BlazorFormsAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlazorFormsAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonRepository personRepository, ILogger<PersonController> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        [HttpGet("people")]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var people = await _personRepository.GetPeople();
                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error ocurred getting people");
            }
        }

        [HttpGet("people/{id:int}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(nameof(id));
            }

            try
            {
                var person = await _personRepository.GetPersonById(id);
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error ocurred getting a person");
            }
        }

        [HttpPost("people")]
        public async Task<IActionResult> AddPerson(Person person)
        {
            try
            {
                bool result = await _personRepository.AddPerson(person);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error ocurred adding a person");
            }
        }

        [HttpPut("people")]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            try
            {
                bool result = await _personRepository.UpdatePerson(person);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error ocurred updating a person");
            }
        }

        [HttpDelete("people/{personId:int}")]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            try
            {
                bool result = await _personRepository.DeletePerson(personId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "An error ocurred deleting a person");
            }
        }
    }
}

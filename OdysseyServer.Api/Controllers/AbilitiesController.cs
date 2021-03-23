using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdysseyServer.ApiClient;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OdysseyServer.Api.Controllers
{
    [Route("api/ability")]
    [ApiController]
    public class AbilitiesController : ControllerBase
    {
        private IAbilityService _abilityService;

        public AbilitiesController(IAbilityService abilityService)
        {
            _abilityService = abilityService;
        }

        // GET: api/<CharacterController>
        [HttpGet("/api/abilities")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityAllResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAbilities()
        {
            var abilities = await _abilityService.GetAllAbilities();
            var data = abilities.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // GET api/ability
        [HttpGet("{id}")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityGetResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAbilityById(long id)
        {
            var ability = await _abilityService.GetAbilityById(id);
            var data = ability.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // POST api/ability
        /// <summary>
        /// Accepts AbilityAddRequest
        /// </summary>
        [HttpPost]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityAddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAbility()
        {           
            var stream = Request.BodyReader.AsStream();
            var requestObject = AbilityAddRequest.Parser.ParseFrom(stream);
            var result = await _abilityService.CreateAbility(requestObject);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // PUT api/ability
        /// <summary>
        /// Accepts AbilityUpdateRequest
        /// </summary>
        [HttpPut]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityAddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AbilityUpdate()
        {           
            var stream = Request.BodyReader.AsStream();
            var requestObject = AbilityUpdateRequest.Parser.ParseFrom(stream);
            
            var result = await _abilityService.UpdateAbility(requestObject);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // DELETE api/ability
        [HttpDelete("{id}")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AbilityDelete(long id)
        {           
            await _abilityService.DeleteAbility(id);
            return Ok();
        }
    }
}

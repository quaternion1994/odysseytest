using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdysseyServer.ApiClient;
using OdysseyServer.Services.Contracts;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OdysseyServer.Api.Controllers
{
    [Route("api/ability")]
    [ApiController]
    public class AbilitiesController : OdysseyControllerBase
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
        public async Task<IActionResult> GetAllAbilitiesAsync()
        {
            return Protobuf(await _abilityService.GetAllAbilitiesAsync());
        }

        // GET api/ability
        [HttpGet("{id}")]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityGetResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAbilityByIdAsync(long id)
        {
            return Protobuf(await _abilityService.GetAbilityByIdAsync(id));
        }

        // POST api/ability
        /// <summary>
        /// Accepts AbilityAddRequest
        /// </summary>
        [HttpPost]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityAddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAbilityAsync(AbilityAddRequest requestObject)
        {
            return Protobuf(await _abilityService.CreateAbilityAsync(requestObject));
        }

        // PUT api/ability
        /// <summary>
        /// Accepts AbilityUpdateRequest
        /// </summary>
        [HttpPut]
        [Produces("application/x-protobuf")]
        [ProducesResponseType(typeof(AbilityAddResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AbilityUpdateAsync(AbilityUpdateRequest requestObject)
        {
            return Protobuf(await _abilityService.UpdateAbility(requestObject));
        }

        // DELETE api/ability
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AbilityDeleteAsync(long id)
        {           
            await _abilityService.DeleteAbilityAsync(id);
            return Ok();
        }
    }
}

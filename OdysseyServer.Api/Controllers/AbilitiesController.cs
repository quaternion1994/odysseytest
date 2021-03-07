using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using OdysseyServer.ApiClient;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet("allabilities")]
        public async Task<IActionResult> GetAllAbilities()
        {
            var abilities = await _abilityService.GetAllAbilities();
            var data = abilities.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // GET api/ability
        [HttpGet("")]
        public async Task<IActionResult> GetAbilityById(long id)
        {
            var ability = await _abilityService.GetAbilityById(id);
            var data = ability.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // POST api/ability
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var ability = new Ability
            {
                Level = 1,
                RequiredLevel = 4,
                Name = "Fireball",
                Stats = new AbilityStats
                {
                    Attack = 10,
                    Defence = 20
                }
            };
            /*var stream = Request.BodyReader.AsStream();
            var person = Character.Parser.ParseFrom(stream);*/
            //seria
            await _abilityService.CreateAbility(ability);

            return Ok();
        }

        // PUT api/ability
        [HttpPut("")]
        public async Task<IActionResult> AbilityUpdate()
        {
            var ability = new Ability
            {
                Level = 1,
                RequiredLevel = 4,
                Name = "Frostball",
                Stats = new AbilityStats
                {
                    Attack = 10,
                    Defence = 20,
                    Id = 1
                },
                Id = 1
            };
            /*var stream = Request.BodyReader.AsStream();
            var person = Character.Parser.ParseFrom(stream);*/
            var result = await _abilityService.UpdateAbility(ability);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // DELETE api/ability
        [HttpDelete("")]
        public async Task<IActionResult> AbilityDelete(long id)
        {
            await _abilityService.DeleteAbility(id);
            return Ok();
        }
    }
}

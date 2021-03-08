using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using OdysseyServer.ApiClient;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdysseyServer.Api.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IDistributedCache _distributedCache;
        private const string cacheKey = "allGroups";

        public GroupController(IGroupService groupService, IDistributedCache distributedCache)
        {
            _groupService = groupService;
            _distributedCache = distributedCache;
        }

        [HttpGet("/api/groups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var cachedGroups = await _distributedCache.GetAsync(cacheKey);
            if (cachedGroups != null)
            {
                return File(cachedGroups, "application/octet-stream");
            }
            else
            {
                var groups = await _groupService.GetAllGroups();
                var data = groups.ToByteArray();
                await _distributedCache.SetAsync(cacheKey, data);
                return File(data, "application/octet-stream");
            }         
        }


        [HttpGet]
        public async Task<IActionResult> GetGroupById(long id)
        {
            var ability = await _groupService.GetGroupById(id);
            var data = ability.ToByteArray();
            return File(data, "application/octet-stream");
        }


        [HttpPost]
        public async Task<IActionResult> CreateGroup()
        {           
            var stream = Request.BodyReader.AsStream();
            var group = Group.Parser.ParseFrom(stream);
            await _groupService.CreateGroup(group);
            //_distributedCache.RemoveAsync(cacheKey, data);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> GroupUpdate()
        {
            var stream = Request.BodyReader.AsStream();
            var group = Group.Parser.ParseFrom(stream);
            var result = await _groupService.UpdateGroup(group);
            var data = result.ToByteArray();
            return File(data, "application/octet-stream");
        }

        // DELETE api/ability
        [HttpDelete]
        public async Task<IActionResult> GroupDelete(long id)
        {
            await _groupService.DeleteGroup(id);
            return Ok();
        }
    }
}

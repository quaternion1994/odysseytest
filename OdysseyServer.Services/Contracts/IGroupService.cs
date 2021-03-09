using OdysseyServer.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface IGroupService
    {
        Task<GroupAddResponse> CreateGroup(GroupAddRequest requestObject);
        Task<GroupByIdResponse> GetGroupById(GroupByIdRequest requestObject);
        Task DeleteGroup(GroupDeleteRequest requestObject);
        Task<GroupAllResponse> GetAllGroups();
    }
}

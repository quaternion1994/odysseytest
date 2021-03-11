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
        Task<GroupByIdResponse> GetGroupById(long groupId);
        Task DeleteGroup(long groupId);
        Task<GroupAllResponse> GetAllGroups();
    }
}

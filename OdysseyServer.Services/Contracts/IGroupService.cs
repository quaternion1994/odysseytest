using OdysseyServer.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services.Contracts
{
    public interface IGroupService
    {
        Task CreateGroup(Group group);
        Task<Group> GetGroupById(long Id);
        Task<AllGroup> GetAllGroups();
        Task<Group> UpdateGroup(Group group);
        Task DeleteGroup(long id);
    }
}

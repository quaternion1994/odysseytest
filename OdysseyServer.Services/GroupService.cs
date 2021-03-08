using AutoMapper;
using OdysseyServer.ApiClient;
using OdysseyServer.Persistence.Contracts;
using OdysseyServer.Persistence.Entities;
using OdysseyServer.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OdysseyServer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GroupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Group> GetGroupById(long id)
        {
            var groupDbo = await _unitOfWork.Group.GetByID(id);
            var group = _mapper.Map<Group>(groupDbo);
            return group;
        }

        public async Task CreateGroup(Group group)
        {
            var groupDbo = _mapper.Map<GroupDbo>(group);
            await _unitOfWork.Group.Insert(groupDbo);
        }

        public async Task<AllGroup> GetAllGroups()
        {
            var groupsDbo = await _unitOfWork.Group.Get();
            var allGroup = new AllGroupDbo
            {
                Groups = groupsDbo
            };
            var groups = _mapper.Map<AllGroup>(allGroup);
            return groups;
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            var groupDbo = _mapper.Map<GroupDbo>(group);
            await _unitOfWork.Group.Update(groupDbo);
            var groupDboUpdated = await _unitOfWork.Group.GetByID(group.Id);
            var result = _mapper.Map<Group>(groupDboUpdated);
            return result;
        }

        public async Task DeleteGroup(long id)
        {
            await _unitOfWork.Group.Delete(id);
        }
    }
}

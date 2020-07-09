using Common.Dto;
using Common.Enum;
using System.Collections.Generic;

namespace Accessors.Concrete
{
    public interface IUserAccessor
    {
        UserDto CreateUser(UserDto userToCreate);
        List<UserDto> GetAllUser();
        List<UserDto> GetAllUserByRole(Role role);
        UserDto GetUserFromUNameAndPassword(string userName, string password);
    }
}
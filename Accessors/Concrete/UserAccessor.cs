using AutoMapper;
using Common.Dto;
using Common.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accessors.Concrete
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IMapper _mapper;
        private DbContextOptions<UserDbContext> _option;
        public UserAccessor(IMapper mapper, DbContextOptions<UserDbContext> option)
        {
            _mapper = mapper;
            _option = option;
        }
        public List<UserDto> GetAllUser()
        {
            using (var context = new UserDbContext(_option))
            {
                var userList = context.Users;
                var userReturn = _mapper.Map<List<UserDto>>(userList);
                return userReturn;
            }
        }

        public List<UserDto> GetAllUserByRole(Role role)
        {
            using (var context = new UserDbContext(_option))
            {
                var userList = context.Users.Where(u=>u.RoleId == (int)role);
                var userReturn = _mapper.Map<List<UserDto>>(userList);
                return userReturn;
            }
        }
        
        public UserDto CreateUser(UserDto userToCreate)
        {
            using (var context = new UserDbContext(_option))
            {
                var userDb = _mapper.Map<Users>(userToCreate);
                var usertoRet = context.Users.Add(userDb);
                context.SaveChanges();
                return userToCreate;
            }
        }

        public UserDto GetUserFromUNameAndPassword(string userName,string password)
        {
            using (var context = new UserDbContext(_option))
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == userName && x.Password == password);
                var userReturn = _mapper.Map<UserDto>(user);
                return userReturn;
            }
        }
    }
}

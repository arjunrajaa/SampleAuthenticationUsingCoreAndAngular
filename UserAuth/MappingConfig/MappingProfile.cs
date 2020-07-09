using Accessors;
using AutoMapper;
using Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserAuth.MappingConfig
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<Users, UserDto>()
                .ForMember(dest =>
                        dest.Role,
                        opt => opt.MapFrom(src => (Common.Enum.Role)src.RoleId));
            CreateMap<UserDto, Users>()
                .ForMember(dest =>
                        dest.RoleId,
                        opt => opt.MapFrom(src => (int)src.Role));
        }
    }
}

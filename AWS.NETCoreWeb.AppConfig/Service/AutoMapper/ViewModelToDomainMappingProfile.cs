using AutoMapper;
using AWS.NETCoreWeb.AppConfig.Model;
using AWS.NETCoreWeb.AppConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            //手动进行配置
            CreateMap<UserViewModel, UserModel>();
        }
    }
}

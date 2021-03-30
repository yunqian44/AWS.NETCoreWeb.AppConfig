using AutoMapper;
using AWS.NETCoreWeb.AppConfig.Model;
using AWS.NETCoreWeb.AppConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
            CreateMap<UserModel, UserViewModel>();
        }
    }
}

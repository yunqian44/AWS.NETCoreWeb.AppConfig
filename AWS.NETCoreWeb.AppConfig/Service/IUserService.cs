using AWS.NETCoreWeb.AppConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.NETCoreWeb.AppConfig.Service
{
    public interface IUserService : IApplicationService<UserViewModel>
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userViewModel"></param>
        Task<int> Register(UserViewModel userViewModel);
    }
}

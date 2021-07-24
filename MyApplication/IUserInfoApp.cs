using Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication
{
    public interface IUserInfoApp
    {
        public Task<List<UserInfo>> GetUserInfo();
        int GetUserInfoConts();
    }
}

using Demo.WorkerService.Helper;
using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApplication
{
    public class UserInfoApp: IUserInfoApp
    {
        private readonly MyContext _mycontext;
        public UserInfoApp(MyContext myContext)
        {
            _mycontext = myContext;
        }
        /// <summary>
        /// 使用ef获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserInfo>> GetUserInfo()
        {
            return  _mycontext.userInfos.Where(x => x.Name != "").ToList();
        }

        public int GetUserInfoConts()
        {
            string sql = " select *  from  userInfos   ";
            var dr = SqlserverHelper.ExecuteTable(sql);
            var count = dr.Rows.Count;
            return count;

        }
    }
}

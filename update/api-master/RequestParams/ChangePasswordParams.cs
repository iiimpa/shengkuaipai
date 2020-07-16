using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.RequestParams
{
    public class ChangePasswordParams
    {
        //原密码
        public string password { get; set; }
        //新密码
        public string new_password { get; set; }
        //验证码
        public string verify_code { get; set; }
        //验证码标识
        public string verifyCode_key { get; set; }
    }
}

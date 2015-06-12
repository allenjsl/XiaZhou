using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.ComStructure;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 游轮网站会员登录interface
    /// </summary>
    public interface IYlHuiYuanLogin
    {
        /// <summary>
        /// 会员登录，根据系统公司编号、用户名、用户密码获取用户信息
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">登录账号</param>
        /// <param name="pwd">登录密码</param>
        /// <returns></returns>
        MYlHuiYuanInfo Login(string companyId, string username, MPasswordInfo pwd);
        /// <summary>
        /// 会员登录，根据用户编号获取用户信息
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        /// <returns></returns>
        MYlHuiYuanInfo Login(string huiYuanId);
        /// <summary>
        /// 写会员登录日志
        /// </summary>
        /// <param name="info">登录会员信息</param>
        /// <param name="leiXing">0:会员登录 1:自动登录</param>
        void LoginLogwr(MYlHuiYuanInfo info, byte leiXing);
        /// <summary>
        /// 获取域名信息
        /// </summary>
        /// <param name="yuMing">域名</param>
        /// <returns></returns>
        EyouSoft.Model.YlStructure.MWzYuMingInfo GetYuMingInfo(string yuMing);
    }
}

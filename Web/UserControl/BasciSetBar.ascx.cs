using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.EnumType.PrivsStructure;

namespace Web.UserControl
{
    /// <summary>
    /// 系统设置，基础设置导航条
    /// 修改记录:
    /// 1 2011-10-13　曹胡生 创建
    /// </summary>
    public partial class BasciSetBar : System.Web.UI.UserControl
    {
        #region 
        private bool _IsLogin = false;
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _IsLogin;
            }
        }
        private MUserInfo _userInfo = null;
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public MUserInfo SiteUserInfo
        {
            get
            {
                return _userInfo;
            }
        }
        #endregion

        protected string[] PrivsPage = new string[10] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        protected void Page_Load(object sender, EventArgs e)
        {
            PowerControl();
        }

        /// <summary>
        /// 权限控制
        /// </summary>
        private void PowerControl()
        {
            PrivsPage[0] = CheckGrant(Privs.系统设置_基础设置_城市管理栏目) ? "1" : "0";
            PrivsPage[1] = CheckGrant(Privs.系统设置_基础设置_线路区域栏目) ? "1" : "0";
            PrivsPage[2] = CheckGrant(Privs.系统设置_基础设置_模板管理栏目) ? "1" : "0";
            PrivsPage[3] = CheckGrant(Privs.系统设置_基础设置_报价标准栏目) ? "1" : "0";
            PrivsPage[4] = CheckGrant(Privs.系统设置_基础设置_客户等级栏目) ? "1" : "0";
            PrivsPage[5] = CheckGrant(Privs.系统设置_基础设置_支付方式栏目) ? "1" : "0";
            PrivsPage[6] = CheckGrant(Privs.系统设置_基础设置_会员管理栏目) ? "1" : "0";
            PrivsPage[7] = CheckGrant(Privs.系统设置_基础设置_保险类型栏目) ? "1" : "0";
            PrivsPage[8] = CheckGrant(Privs.系统设置_基础设置_上车地点管理栏目) ? "1" : "0";
            PrivsPage[9] = CheckGrant(Privs.系统设置_基础设置_车型管理栏目) ? "1" : "0";
        }

        /// <summary>
        /// 判断当前用户是否有权限
        /// </summary>
        /// <param name="permissionId">权限ID</param>
        /// <returns></returns>
        private bool CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs permission)
        {
            //初始化用户信息
            MUserInfo userInfo = null;
            _IsLogin = EyouSoft.Security.Membership.UserProvider.IsLogin(out userInfo);
            _userInfo = userInfo;
            if (_userInfo == null) return false;
            return _userInfo.Privs.Contains((int)permission);
        }
    }
}
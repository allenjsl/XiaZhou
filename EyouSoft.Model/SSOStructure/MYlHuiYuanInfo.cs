using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.Model.SSOStructure
{
    #region 游轮网站会员信息业务实体
    /// <summary>
    /// 游轮网站会员信息业务实体
    /// </summary>
    public class MYlHuiYuanInfo
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public string HuiYuanId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 会员账号
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 会员姓名
        /// </summary>
        public string XingMing { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public EyouSoft.Model.EnumType.GovStructure.Gender XingBie { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string DianHua { get; set; }
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ShouJi { get; set; }
        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string YouXiang { get; set; }
        /// <summary>
        /// 会员状态
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus Status { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime ZhuCeShiJian { get; set; }
        /// <summary>
        /// 会员类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanLeiXing LeiXing { get; set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        public EyouSoft.Model.EnumType.YlStructure.HuiYuanPingTaiLeiXing PingTaiLeiXing { get; set; }
        /// <summary>
        /// 平台OpenID
        /// </summary>
        public string OpenID { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public DateTime? LatestLoginTime { get; set; }
    }
    #endregion
}

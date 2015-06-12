using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;

namespace Web.UserControl
{
    /// <summary>
    /// 成本核算
    /// 创建人：田想兵  创建日期：2011.9.15
    /// </summary>
    public partial class CostAccounting : System.Web.UI.UserControl
    {
        #region 控件属性
        /// <summary>
        /// 成本核算
        /// </summary>
        public string CostCalculation;

        /// <summary>
        /// 不含项目
        /// </summary>
        public string NoNeedItem;

        /// <summary>
        /// 服务标准
        /// </summary>
        public string ServiceStandard;

        /// <summary>
        /// 儿童安排
        /// </summary>
        public string ChildServiceItem;

        /// <summary>
        /// 自费项目
        /// </summary>
        public string OwnExpense;

        /// <summary>
        /// 注意事项
        /// </summary>
        public string NeedAttention;

        /// <summary>
        /// 温馨提醒
        /// </summary>
        public string WarmRemind;

        /// <summary>
        /// 内部信息
        /// </summary>
        public string InsiderInfor;

        /// <summary>
        /// 购物安排
        /// </summary>
        public string ShoppingItem;

        public bool IsRoute = false;


        public bool IsSanPin = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

        }


    }
}
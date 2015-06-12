using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EyouSoft.Web.CommonPage
{
    using EyouSoft.Common.Page;
    using EyouSoft.BLL.FinStructure;
    using EyouSoft.Model.FinStructure;
    using EyouSoft.Common;
    /// <summary>
    /// 金蝶科目类型选用
    /// </summary>
    /// 参数备注
    /// isType  = 1表示核算项目
    /// isType != 1表示科目
    public partial class KingDeeSubject : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            IList<KingDeePageData> list = null;
            if (Utils.GetInt(Utils.GetQueryStringValue("isType")) == 0)
            {
                var ls = new BFinance().GetKingDeeSubjectLst(CurrentUserCompanyID, 0);
                if (ls != null && ls.Count > 0)
                {
                    list = new List<KingDeePageData>();
                    foreach (MKingDeeSubject item in ls)
                    {
                        list.Add(new KingDeePageData
                        {
                            Id = item.SubjectId,
                            Cd = item.SubjectCd,
                            Name = item.SubjectNm
                        });
                    }
                }
            }
            else
            {
                var ls = new BFinance().GetKingDeeChkLst(CurrentUserCompanyID, "0");
                if (ls != null && ls.Count > 0)
                {
                    list = new List<KingDeePageData>();
                    foreach (MKingDeeChk item in ls)
                    {
                        list.Add(new KingDeePageData
                        {
                            Id = item.ChkId,
                            Cd = item.ChkCd,
                            Name = item.ChkNm
                        });
                    }
                }
            }
            if (list != null && list.Count > 0)
            {
                rpt_list.DataSource = list;
                rpt_list.DataBind();
            }

        }

    }
    /// <summary>
    /// 金蝶科目,金蝶核算项目通用实体(仅用于该页面)
    /// </summary>
    public class KingDeePageData
    {
        public KingDeePageData() { }
        /// <summary>
        /// 核算项目编号,科目编号
        /// </summary>
        public object Id { get; set; }
        /// <summary>
        /// 核算项目代码,科目代码
        /// </summary>
        public object Cd { get; set; }
        /// <summary>
        /// 核算项目名称,科目名称
        /// </summary>
        public object Name { get; set; }
    }
}

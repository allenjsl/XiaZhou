using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Model.FinStructure;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Common;
using EyouSoft.Web.CommonPage;

namespace EyouSoft.Web.FinanceManage.KingdeeSubject
{
    /// <summary>
    /// 金蝶科目类型选用 Ajax页面
    /// </summary>
    public partial class AjaxKingDeeSubject : BackPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<KingDeePageData> list = null;
            if (Utils.GetInt(Utils.GetQueryStringValue("isType")) == 0)
            {
                var ls = new BFinance().GetKingDeeSubjectLst(CurrentUserCompanyID, Utils.GetInt(Utils.GetQueryStringValue("parentSubjectId")));
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
                var ls = new BFinance().GetKingDeeChkLst(CurrentUserCompanyID, Utils.GetQueryStringValue("parentSubjectId"));
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
                pan_msg.Visible = false;
                rpt_list.DataSource = list;
                rpt_list.DataBind();
            }

            
           
        }
    }
}

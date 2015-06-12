using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.BLL.FinStructure;
using EyouSoft.Model.EnumType.KingDee;
using EyouSoft.Model.FinStructure;
using EyouSoft.Common;
using System.Text;
using EyouSoft.BLL.TourStructure;
using EyouSoft.Model.TourStructure;

namespace EyouSoft.Web.FinanceManage.Common
{
    /// <summary>
    /// 财务入账
    /// </summary>
    /// 创建人:柴逸宁
    /// 创建时间:2012-4-23
    /// 订单收款=0,已完成
    /// 计调付款=1,已完成
    /// 杂费收入=2,已完成
    /// 杂费支出=3,已完成
    /// 导游借款=4,已完成
    /// 单团核算=5,已完成
    /// 团未完导游先报账=6,已完成
    /// 后期收款=7,已完成
    public partial class InAccount : BackPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Utils.GetQueryStringValue("doType"))
            {
                case "getAdjustItem":
                    GetAdjustItemList();
                    break;
                case "getSubject":
                    GetSubjectList();
                    break;
                case "Save":
                    Save();
                    break;
            }
            DefaultProofType defaultProofType = (DefaultProofType)Utils.GetInt(Utils.GetQueryStringValue("DefaultProofType"));
            //if (defaultProofType != DefaultProofType.杂费收入 && defaultProofType != DefaultProofType.杂费支出)
            //{
            IList<MKingDeeProofDetail> ls = new BFinance().GetDefaultProofLst(
               defaultProofType,
                CurrentUserCompanyID,
                Utils.GetQueryStringValue("KeyId"));
            if (ls != null && ls.Count > 0)
            {
                pan_tempRow.Visible = false;
                rpt_list.DataSource = ls;
                rpt_list.DataBind();
            }
            //}

        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            MKingDeeProof model = new MKingDeeProof();
            if (GetModel(model))
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson(new BFinance().AddOrUpdKingDeeProof(model) ? "1" : "-1", "保存失败!"));
            }
            else
            {
                AjaxResponse(UtilsCommons.AjaxReturnJson("-1", "保存失败!已导出凭证不能修改!"));
            }
        }

        protected string GetKingDeeChk(object o)
        {
            StringBuilder sb = new StringBuilder();
            if (o != null)
            {
                IList<MKingDeeChk> list = (List<MKingDeeChk>)o;
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append("/ " + list[i].ChkCd + " - " + list[i].ChkNm);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 嵌套repeater绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rpt_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                IList<MKingDeeChk> ls = ((MKingDeeProofDetail)e.Item.DataItem).FItemList;
                if (ls != null && ls.Count > 0)
                {
                    Repeater BackList = (Repeater)e.Item.FindControl("rpt_FItemList");
                    BackList.DataSource = ls;
                    BackList.DataBind();
                }

            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        private bool GetModel(MKingDeeProof model)
        {
            model.KingDeeProofDetailLst = null;
            //系统公司
            model.FCompanyId = CurrentUserCompanyID;
            //凭证日期
            model.FDate = Utils.GetDateTime(Utils.GetFormValue("txt_FDate"));
            //业务日期
            model.FTransDate = DateTime.Now;
            //序号(此处序号是浮云)
            model.FSerialNum = 0;
            //制单人
            model.FPreparerId = SiteUserInfo.Name;

            //操作者部门编号
            model.DeptId = SiteUserInfo.DeptId;

            //操作者编号
            model.OperatorId = SiteUserInfo.UserId;

            MTourBaseInfo tourModel = new BTour().GetBasicTourInfo(Utils.GetFormValue("tourId"));
            if (tourModel != null)
            {
                model.FTourCode = tourModel.TourCode;
            }
            model.FTourId = Utils.GetFormValue("tourId") != "" ? Utils.GetFormValue("tourId") : Utils.GetFormValue("KeyId");
            model.FItemId = Utils.GetFormValue("KeyId");
            //凭证类型 团队状态为 封团之后的的类型为 后期收款,其余用传值
            model.FItemType = (DefaultProofType)Utils.GetInt(Utils.GetFormValue("DefaultProofType"));
            if (model.FItemType == DefaultProofType.单团核算)
            {
                model.FItemId = model.FTourId;
            }
            IList<MKingDeeProofDetail> ls = new List<MKingDeeProofDetail>();
            string[] fExplanation = Utils.GetFormValues("txtZy");
            string[] fAccountNum = Utils.GetFormValues("hide_KmCode");
            string[] fAccountName = Utils.GetFormValues("hide_KmName");
            string[] fItem = Utils.GetFormValues("hide_Xm");
            string[] fDebit = Utils.GetFormValues("txtJieFang");
            string[] fCredit = Utils.GetFormValues("txtDaiFang");
            string[] fId = Utils.GetFormValues("hide_KmID");
            if (fExplanation.Length > 0)
            {
                model.FId = fId[0];
                for (int i = 0; i < fExplanation.Length; i++)
                {
                    MKingDeeProofDetail detailModel = new MKingDeeProofDetail();
                    if (fAccountNum[i].Split('|').Length > 1)
                    {
                        detailModel.FDetailId = Utils.GetInt(fAccountNum[i].Split('|')[0]);
                        detailModel.FAccountNum = fAccountNum[i].Split('|')[1];

                    }
                    detailModel.FAccountName = fAccountName[i];
                    detailModel.FExplanation = fExplanation[i];
                    detailModel.FDebit = Utils.GetDecimal(fDebit[i]);
                    detailModel.FCredit = Utils.GetDecimal(fCredit[i]);
                    detailModel.FAmountFor = detailModel.FDebit + detailModel.FCredit;
                    detailModel.FEntryId = i;
                    detailModel.FItem = GetJsonByForm(fItem[i]);
                    ls.Add(detailModel);
                }
                model.KingDeeProofDetailLst = ls;
            }
            return model.KingDeeProofDetailLst != null && model.KingDeeProofDetailLst.Count > 0;
        }

        /// <summary>
        /// 从表单获得核算项目，并处理数据
        /// </summary>
        /// <returns></returns>
        protected string GetJsonByForm(string item)
        {
            string str = string.Empty;
            if (item.IndexOf("[") > 0)
            {
                item = item.Substring(item.IndexOf("["), item.Length - item.IndexOf("["));
                item = item.TrimEnd('}');
            }

            IList<NewDeeChkModel> list = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<NewDeeChkModel>>(item);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    str += list[i].PreChkNm + "---" + list[i].ChkCd + "---" + list[i].ChkNm + ",";
                }
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }


        /// <summary>
        /// 匹配科目
        /// </summary>
        private void GetAdjustItemList()
        {
            string[] str = Utils.GetQueryStringValue("q").Split('-');
            string searchKey = str[0].Trim();
            StringBuilder sb = new StringBuilder();
            BFinance bll = new BFinance();
            IList<MKingDeeSubject> ls = (bll.GetKingDeeSubjectLst(CurrentUserCompanyID) ?? new List<MKingDeeSubject>())
                .Where(item =>
                    item.MnemonicCd.Contains(searchKey) ||/*助记码*/
                    item.SubjectNm.Contains(searchKey) ||/*科目名称*/
                    item.SubjectCd.Contains(searchKey)/*科目代码*/
                    ).ToList();
            if (ls != null && ls.Count > 0)
            {
                string subjectTyps = string.Empty;

                for (int i = 0; i < ls.Count; i++)
                {
                    string chkData = string.Empty;
                    if (ls[i].ItemList != null && ls[i].ItemList.Count > 0)
                    {
                        chkData = GetJsonByList(ls[i].ItemList);
                    }
                    else
                    {
                        chkData = "{\"data\":[]}";
                    }

                    sb.Append(ls[i].SubjectNm + "|" + ls[i].SubjectId + "|" + ls[i].SubjectCd + "|" + chkData + "\n");
                }
            }
            else
            {
                sb.Append("无匹配项|-1|-1");
            }
            AjaxResponse(sb.ToString());
        }

        /// <summary>
        /// 通过核算项目集合 返回json数据
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetJsonByList(object o)
        {
            string str = "";
            if (o != null)
            {
                IList<MKingDeeChk> list = (List<MKingDeeChk>)o;
                var deeChkList = list.Select(p => new { p.ChkCate, p.ChkId, p.ChkNm, p.ChkCd, p.PreChkNm }).ToList();
                str = "{\"data\":" + Newtonsoft.Json.JsonConvert.SerializeObject(deeChkList) + "}";
                return str;
            }
            return "{\"data\":[]}";
        }


        /// <summary>
        /// 匹配核算项目
        /// </summary>
        private void GetSubjectList()
        {
            string str = Utils.GetQueryStringValue("q");
            StringBuilder sb = new StringBuilder();
            IList<MKingDeeChk> ls = (new BFinance().GetKingDeeChkLst(CurrentUserCompanyID) ?? new List<MKingDeeChk>())
                .Where(item =>
                   item.ChkCate == (FinanceAccountItem)Enum.Parse(typeof(FinanceAccountItem), Utils.GetQueryStringValue("subjectType")) &&/*项目类别*/
                   (item.ChkCd.ToLower().Contains(str) ||/*核算项目代码*/
                   item.ChkNm.ToLower().Contains(str) ||/*核算项目名称*/
                   item.MnemonicCd.ToLower().Contains(str)/*助记码*/
                   )

                    ).ToList();
            if (ls != null && ls.Count > 0)
            {
                foreach (var item in ls)
                {
                    sb.Append(item.ChkCd + " — " + item.ChkNm + " — " + item.MnemonicCd + "|" + item.ChkCd + "|" + (int)item.ChkCate + "|" + item.ChkId + "|" + item.PreChkNm + "\n");
                }
            }
            else
            {
                sb.Append("无匹配项|-1");
            }

            AjaxResponse(sb.ToString());
        }
    }

    /// <summary>
    /// 临时使用核算项目实体
    /// </summary>
    public class NewDeeChkModel
    {
        /// <summary>
        /// 科目类型
        /// </summary>
        public FinanceAccountItem ChkCate { get; set; }
        /// <summary>
        /// 核算项目编号
        /// </summary>
        public int ChkId { get; set; }
        /// <summary>
        /// 核算项目名称
        /// </summary>
        public string ChkNm { get; set; }
        /// <summary>
        /// 核算项目代码
        /// </summary>
        public string ChkCd { get; set; }
        /// <summary>
        /// 上级核算项目名称
        /// </summary>
        public string PreChkNm { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Page;

namespace Web.ContractManage
{
    /// <summary>
    /// 合同号管理-登记合同
    /// </summary>
    /// 修改人：邵权江
    /// 创建时间：2012-04-09
    public partial class ContractCodeAdd : BackPage
    {
        /// <summary>
        /// 合同类型
        /// </summary>
        protected EyouSoft.Model.EnumType.ConStructure.ContractType HeTongLeiXing = EyouSoft.Model.EnumType.ConStructure.ContractType.国内合同;

        protected void Page_Load(object sender, EventArgs e)
        {
            //权限验证
            PowerControl();
            HeTongLeiXing = Utils.GetEnumValue<EyouSoft.Model.EnumType.ConStructure.ContractType>(Utils.GetQueryStringValue("type"), EyouSoft.Model.EnumType.ConStructure.ContractType.国内合同);

            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");
            //存在ajax请求
            if (doType == "save")
            {
                PageSave();
            }
            #endregion
        }

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void PageSave()
        {
            #region 表单取值
            string msg = "";
            bool result = false;
            Response.Clear();
            string prefixCode = Utils.GetFormValue(txtPrefix.UniqueID);
            int startCode = Utils.GetInt(this.txtStart.Text.Trim());
            int endCode = Utils.GetInt(this.txtEnd.Text.Trim());           
            int xuHaoChangDu = Utils.GetInt(Utils.GetFormValue(txtXuHaoChangDu.UniqueID), 1);
            #endregion

            #region 表单验证
            if (string.IsNullOrEmpty(prefixCode))
            {
                msg += "请输入合同号前缀!<br/>";
            }
            if (startCode == 0 && endCode == 0)
            {
                msg += "请输入合同号起始序号或截止序号!<br/>";
            }
            if (startCode > 0 && endCode > 0 && System.Math.Abs(startCode - endCode) > 200)
            {
                msg += "起始序号和截止序号只能相差200!<br/>";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                result = false;
                Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
                Response.End();
                return;
            }
            #endregion

            #region 提交回应
            int i = new EyouSoft.BLL.ConStructure.BContractNum().AddContractNum(CreateListModel(prefixCode, startCode, endCode, HeTongLeiXing, xuHaoChangDu));
            if (i < 0)
            {
                result = false;
                msg = "登记失败！";
            }
            if (i == 0)
            {
                result = true;
                msg = "登记成功！";
            }
            if (i > 0)
            {
                result = false;
                msg = "登记合同号失败，其中有 " + i + " 条重复！";
            }
            Response.Clear();
            Response.Write("{\"result\":\"" + result + "\",\"msg\":\"" + msg + "\"}");
            Response.End();
            #endregion

        }
        #endregion

        #region 生成合同号集合
        /// <summary>
        /// 生成合同号集合
        /// </summary>
        /// <param name="prefixCode">合同号前缀</param>
        /// <param name="startCode">起始序号</param>
        /// <param name="endCode">截止序号</param>
        /// <param name="heTongLeiXing">合同号类型</param>
        /// <param name="xuHaoChangDu">序号长度</param>
        /// <returns></returns>
        protected IList<EyouSoft.Model.ConStructure.MContractNum> CreateListModel(string prefixCode, int startCode, int endCode,EyouSoft.Model.EnumType.ConStructure.ContractType heTongLeiXing, int xuHaoChangDu)
        {
            IList<EyouSoft.Model.ConStructure.MContractNum> listModel = new List<EyouSoft.Model.ConStructure.MContractNum>();
            EyouSoft.Model.ConStructure.MContractNum model = null;
            if (startCode > 0 && endCode > 0 && startCode != endCode)
            {
                int max = System.Math.Max(startCode, endCode);
                int min = System.Math.Min(startCode, endCode);
                for (int i = min; i <= max; i++)
                {
                    model = new EyouSoft.Model.ConStructure.MContractNum();
                    model.CompanyId = this.SiteUserInfo.CompanyId;
                    model.ContractCode = prefixCode + i.ToString().PadLeft(xuHaoChangDu, '0');
                    model.ContractType = heTongLeiXing;
                    model.ContractStatus = EyouSoft.Model.EnumType.ConStructure.ContractStatus.未领用;
                    model.DepartId = this.SiteUserInfo.DeptId;
                    model.OperatorId = this.SiteUserInfo.UserId;
                    model.IssueTime = DateTime.Now;
                    listModel.Add(model);
                    model = null;
                }
            }
            else
            {
                model = new EyouSoft.Model.ConStructure.MContractNum();
                model.CompanyId = this.SiteUserInfo.CompanyId;
                if (startCode == endCode)
                {
                    model.ContractCode = prefixCode + startCode.ToString().PadLeft(xuHaoChangDu, '0');
                }
                if (startCode > 0)
                {
                    model.ContractCode = prefixCode + startCode.ToString().PadLeft(xuHaoChangDu, '0');
                }
                if (endCode > 0)
                {
                    model.ContractCode = prefixCode + endCode.ToString().PadLeft(xuHaoChangDu, '0');
                }
                model.ContractType = heTongLeiXing;
                model.ContractStatus = EyouSoft.Model.EnumType.ConStructure.ContractStatus.未领用;
                model.DepartId = this.SiteUserInfo.DeptId;
                model.OperatorId = this.SiteUserInfo.UserId;
                model.IssueTime = DateTime.Now;
                listModel.Add(model);
                model = null;
            }
            return listModel;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        protected void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_栏目, false);
                return;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_登记))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.合同管理_合同管理_登记, false);
                return;
            }
        }
        #endregion

        #region 重写OnPreInit
        /// <summary>
        /// 重写OnPreInit 指定页面类型
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            this.PageType = PageType.boxyPage;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EyouSoft.Common.Page;
using EyouSoft.Common;
using System.Text;
using EyouSoft.Model.GovStructure;

namespace Web.ManageCenter.Hr
{
    /// <summary>
    /// 行政中心-人事档案-列表
    /// </summary>
    /// 修改人：方琪
    /// 修改时间：2012-03-29
    public partial class HrList : BackPage
    {
        #region 页面参数
        /// <summary>
        /// 页大小
        /// </summary>
        private int pageSize = 20;
        /// <summary>
        /// 页码
        /// </summary>
        private int pageIndex = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        private int recordCount = 0;

        #endregion

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理AJAX请求
            //获取ajax请求
            string doType = Utils.GetQueryStringValue("doType");

            switch (doType)
            {
                case "delete": DeleteData(); break;
                case "gonglingtongbu": GongLingTongBu(); break;
                default: break;
            }
            #endregion

            if (!IsPostBack)
            {
                //权限判断
                PowerControl();
                //初始化
                DataInit();
            }
            if (UtilsCommons.IsToXls()) ToXls();
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void DataInit()
        {
            #region 查询参数
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //档案编号
            string txtNum = Utils.GetQueryStringValue("txtNum");
            //姓名
            string txtName = Utils.GetQueryStringValue("txtName");
            //性别
            int selSex = Utils.GetInt(Utils.GetQueryStringValue("selSex"), -1);
            //出生日期
            DateTime? txtSBirth = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSBirth"));
            DateTime? txtEBirth = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEBirth"));
            //工龄
            string txtWorkYear = Utils.GetQueryStringValue("txtWorkYear");
            //职务
            int txtDutyId = Utils.GetInt(Utils.GetQueryStringValue(this.SelectDuty1.DutyIDClient));
            this.SelectDuty1.DutyID = txtDutyId.ToString();
            //职务名
            string txtDutyName = Utils.GetQueryStringValue(this.SelectDuty1.DutyNameClient);
            this.SelectDuty1.DutyName = txtDutyName;
            //类型
            int selType = Utils.GetInt(Utils.GetQueryStringValue("selType"), -1);
            //员工状态
            int selState = Utils.GetInt(Utils.GetQueryStringValue("selState"), -1);
            //婚姻状态
            int selWedState = Utils.GetInt(Utils.GetQueryStringValue("selWedState"), -1);
            #endregion
            EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
            //查询实体
            EyouSoft.Model.GovStructure.MSearchGovFile searchModel = new EyouSoft.Model.GovStructure.MSearchGovFile();
            searchModel.BirthDateBegin = txtSBirth;
            searchModel.BirthDateEnd = txtEBirth;
            searchModel.FileNumber = txtNum;
            if (selWedState != -1)
            {
                searchModel.IsMarriage = selWedState == 1 ? true : false;
            }
            searchModel.LengthService = Utils.GetIntNull(txtWorkYear);
            searchModel.Name = txtName;
            searchModel.PositionId = txtDutyId;
            searchModel.Position = txtDutyName;
            searchModel.Sex = (EyouSoft.Model.EnumType.GovStructure.Gender)selSex;
            searchModel.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)selState;
            searchModel.StaffType = (EyouSoft.Model.EnumType.GovStructure.StaffType)selType;
            searchModel.DangAnId = Utils.GetQueryStringValue("danganid");
            string depts = txtDept.SectionID = Utils.GetQueryStringValue(txtDept.SelectIDClient);
            txtDept.SectionName = Utils.GetQueryStringValue(txtDept.SelectNameClient);
            searchModel.DeptIds = Utils.GetIntArray(depts, ",");

            IList<EyouSoft.Model.GovStructure.MGovFile> lst =
                BLL.GetSearchArchivesList(searchModel, this.SiteUserInfo.CompanyId, this.pageSize, this.pageIndex, ref this.recordCount);
            if (null != lst && lst.Count > 0)
            {
                this.RepList.DataSource = lst;
                this.RepList.DataBind();
            }
            else
            {
                this.RepList.Controls.Add(new Label() { Text = "<tr><td colspan='15' align='center'>对不起，没有相关数据！</td></tr>" });
            }

            BindPage();
        }
        #endregion

        #region 绑定分页
        /// <summary>
        /// 绑定分页
        /// </summary>
        private void BindPage()
        {
            ExporPageInfoSelect1.UrlParams = Request.QueryString;
            ExporPageInfoSelect1.intPageSize = pageSize;
            ExporPageInfoSelect1.CurrencyPage = pageIndex;
            ExporPageInfoSelect1.intRecordCount = recordCount;

            ExporPageInfoSelect1.Visible = ExporPageInfoSelect1.intRecordCount > ExporPageInfoSelect1.intPageSize;
        }
        #endregion

        #region 枚举选项
        /// <summary>
        /// 返回select的options
        /// </summary>
        /// <returns></returns>
        protected string getSelOptions(int i)
        {
            string str = String.Empty;
            switch (i)
            { //需求读取数据字典
                case 1://性别
                    str = this.getEnumInfo(typeof(EyouSoft.Model.EnumType.GovStructure.Gender));
                    break;
                case 2://类型
                    str = this.getEnumInfo(typeof(EyouSoft.Model.EnumType.GovStructure.StaffType));
                    break;
                case 3://员工状态
                    str = this.getEnumInfo(typeof(EyouSoft.Model.EnumType.GovStructure.StaffStatus));
                    break;
            }
            return str.ToString();
        }
        /// <summary>
        /// 枚举选项
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected string getEnumInfo(Type t)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<option value=\"-1\">请选择</option>");
            List<EnumObj> lst = EnumObj.GetList(t);
            if (null != lst && lst.Count > 0)
            {
                foreach (EnumObj m in lst)
                {
                    str.AppendFormat("<option value=\"{0}\">{1}</option>", m.Value, m.Text);
                }
            }
            return str.ToString();
        }
        #endregion

        #region 批量删除
        /// <summary>
        /// 删除操作
        /// </summary>
        private void DeleteData()
        {
            /*string msg = String.Empty;
            int b = 0;
            if (!String.IsNullOrEmpty(id))
            {
                EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
                b = BLL.DeleteArchives(id.Split(','));
            }
            switch (b)
            {
                case 0:
                    msg = "删除成功";//成功
                    break;
                case 1:
                    msg = "已经分配用户，不能删除！";
                    break;
                case -1:
                    msg = "删除失败！";
                    break;
                default:
                    msg = "删除失败！";
                    break;
            }
            //返回ajax操作结果
            Response.Clear();
            Response.Write(UtilsCommons.AjaxReturnJson(b.ToString(), msg));
            Response.End();*/

            if (!CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_删除)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：没有操作权限！"));

            string s = Utils.GetQueryStringValue("id");
            if (string.IsNullOrEmpty(s)) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未选择任何要删除的档案信息！"));

            int bllRetCode = new EyouSoft.BLL.GovStructure.BArchives().DeleteArchives(SiteUserInfo.CompanyId, s);

            if (bllRetCode == 1) RCWE(UtilsCommons.AjaxReturnJson("1", "操作成功：人事档案信息已成功删除！"));
            else if (bllRetCode == -99) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：人事档案信息不存在或已删除！"));
            else if (bllRetCode == -98) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：同步到用户信息的人事档案不能删除！"));
            else if (bllRetCode == -97) RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：同步到导游信息的人事档案不能删除！"));
            else RCWE(UtilsCommons.AjaxReturnJson("0", "操作失败：未知异常，ERROR CODE:" + bllRetCode));
        }
        #endregion

        #region 获取部门名称或者职务名称
        /// <summary>
        /// 获取部门名称或者职务名称
        /// </summary>
        /// <param name="o"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        protected string GetMoreInfo(object o, string s)
        {
            if (null == o)
                return "";
            string[] strArr = { };
            List<string> str = new List<string>();
            switch (s)
            {
                case "depart"://部门
                    IList<MGovFileDept> lst = (IList<MGovFileDept>)o;
                    if (null != lst && lst.Count > 0)
                    {
                        foreach (MGovFileDept m in lst)
                        {
                            str.Add(m.DepartName);
                        }
                    }
                    break;
                case "position"://职位
                    IList<MGovFilePosition> lstP = (IList<MGovFilePosition>)o;
                    if (null != lstP && lstP.Count > 0)
                    {
                        foreach (MGovFilePosition m in lstP)
                        {
                            str.Add(m.Title);
                        }
                    }
                    break;
            }
            strArr = str.ToArray();
            return strArr == null ? "" : String.Join(",", strArr);
        }
        #endregion

        #region 获取合同到期时间
        /// <summary>
        /// 获取合同到期时间
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected string GetMaturityTime(object o)
        {
            if (o != null)
            {
                IList<MGovFileContract> list = (IList<MGovFileContract>)o;
                if (list.Count > 0)
                {
                    return string.Format("{0:yyyy-MM-dd}", list.Last().MaturityTime);
                }
            }
            return string.Empty;
        }
        #endregion

        #region 权限判断
        /// <summary>
        /// 权限判断
        /// </summary>
        private void PowerControl()
        {
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_栏目))
            {
                Utils.ResponseNoPermit(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_栏目, false);
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_新增))
            {
                this.ph_Add.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_修改))
            {
                this.ph_Update.Visible = false;
            }
            if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.行政中心_人事档案_删除))
            {
                this.ph_Del.Visible = false;
            }
        }
        #endregion

        void ToXls()
        {
            int toXlsRecordCount = UtilsCommons.GetToXlsRecordCount();
            if (toXlsRecordCount < 1) ResponseToXls(string.Empty);
            StringBuilder s = new StringBuilder();
            #region 查询参数
            //获取分页参数并强转
            pageIndex = Utils.GetInt(Utils.GetQueryStringValue("page"), 1);
            //档案编号
            string txtNum = Utils.GetQueryStringValue("txtNum");
            //姓名
            string txtName = Utils.GetQueryStringValue("txtName");
            //性别
            int selSex = Utils.GetInt(Utils.GetQueryStringValue("selSex"), -1);
            //出生日期
            DateTime? txtSBirth = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtSBirth"));
            DateTime? txtEBirth = Utils.GetDateTimeNullable(Utils.GetQueryStringValue("txtEBirth"));
            //工龄
            int txtWorkYear = Utils.GetInt(Utils.GetQueryStringValue("txtWorkYear"));
            //职务
            int txtDuty = Utils.GetInt(Utils.GetQueryStringValue("DutyHideID"));
            //类型
            int selType = Utils.GetInt(Utils.GetQueryStringValue("selType"), -1);
            //员工状态
            int selState = Utils.GetInt(Utils.GetQueryStringValue("selState"), -1);
            //婚姻状态
            int selWedState = Utils.GetInt(Utils.GetQueryStringValue("selWedState"), -1);
            #endregion
            EyouSoft.BLL.GovStructure.BArchives BLL = new EyouSoft.BLL.GovStructure.BArchives();
            //查询实体
            EyouSoft.Model.GovStructure.MSearchGovFile searchModel = new EyouSoft.Model.GovStructure.MSearchGovFile();
            searchModel.BirthDateBegin = txtSBirth;
            searchModel.BirthDateEnd = txtEBirth;
            searchModel.FileNumber = txtNum;
            if (selWedState != -1)
            {
                searchModel.IsMarriage = selWedState == 1 ? true : false;
            }
            searchModel.LengthService = Utils.GetIntNull(Utils.GetQueryStringValue("txtWorkYear"));
            searchModel.Name = txtName;
            searchModel.PositionId = txtDuty;
            searchModel.Sex = (EyouSoft.Model.EnumType.GovStructure.Gender)selSex;
            searchModel.StaffStatus = (EyouSoft.Model.EnumType.GovStructure.StaffStatus)selState;
            searchModel.StaffType = (EyouSoft.Model.EnumType.GovStructure.StaffType)selType;
            IList<EyouSoft.Model.GovStructure.MGovFile> lst =
                BLL.GetSearchArchivesList(searchModel, this.SiteUserInfo.CompanyId, toXlsRecordCount, 1, ref this.recordCount);
            if (lst != null && lst.Count > 0)
            {
                s.AppendFormat("<table><tr><th align='center' class='th-line'>档案编号</th><th align='center' class='th-line'>姓名</th><th align='center' class='th-line'>性别</th><th align='center' class='th-line'>出生日期</th><th align='center' class='th-line'>所属部门</th><th align='center' class='th-line'>职务</th><th align='center' class='th-line'>工龄</th><th align='center' class='th-line'>联系电话</th><th align='center' class='th-line'>手机</th><th align='center' class='th-line'>QQ</th><th align='center' class='th-line'>学历</th><th align='center' class='th-line'>合同是否签订</th><th align='center' class='th-line'>合同到期时间</th></tr>");
                foreach (var item in lst)
                {
                    s.AppendFormat("<tr><td align='center'>{0}</td><td align='center'>{1}</td><td align='center'>{2}</td><td align='center'>{3}</td><td align='center'>{4}</td><td align='center'>{5}</td><td align='center'>{6}</td><td align='center'>{7}</td><td align='center'>{8}</td><td align='center'>{9}</td><td align='center'>{10}</td><td align='center'>{11}</td><td align='center'>{12}</td></tr>", item.FileNumber, item.Name, item.Sex.ToString(), item.BirthDate.HasValue ? item.BirthDate.Value.ToString("yyyy-MM-dd") : "", item.DepartName, GetMoreInfo(item.GovFilePositionList, "position"), item.LengthService, item.Contact, item.Mobile, item.qq, item.Education, item.IsSignContract ? "是" : "否", item.MaturityTime.HasValue ? item.MaturityTime.Value.ToString("yyyy-MM-dd") : "");
                }
                s.AppendFormat("</table>");
            }
            string browser = this.Context.Request.UserAgent.ToUpper();
            string fileName = "人事档案";
            if (browser.Contains("MS") && browser.Contains("IE"))
            {
                fileName = System.Web.HttpUtility.UrlEncode(fileName, Encoding.UTF8);
            }
            ResponseToXls(s.ToString(), Encoding.UTF8, fileName);
        }

        /// <summary>
        /// 工龄同步
        /// </summary>
        void GongLingTongBu()
        {
            var bllRetCode = new EyouSoft.BLL.GovStructure.BArchives().GongLingTongBu(CurrentUserCompanyID);

            RCWE(UtilsCommons.AjaxReturnJson("1", "工龄同步成功"));
        }
    }
}
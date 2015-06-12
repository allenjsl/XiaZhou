using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.EnumType.PlanStructure;
using EyouSoft.Model.EnumType.SourceStructure;
using EyouSoft.Model.SourceStructure;
using EyouSoft.Model.TourStructure;
using EyouSoft.Model.PlanStructure;
using System.Web.UI.WebControls;
using EyouSoft.Model.EnumType.FinStructure;

using System.Data;
using System.Data.SqlTypes;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace EyouSoft.Common
{
    public class UtilsCommons
    {
        /// <summary>
        /// 获得线路区域的Options
        /// </summary>
        /// <param name="selectIndex">设置选择项</param>
        /// <returns></returns>
        public static string GetAreaLineForSelect(int selectIndex, string companyID)
        {
            StringBuilder sb = new StringBuilder();
            IList<EyouSoft.Model.ComStructure.MComArea> list = new EyouSoft.BLL.ComStructure.BComArea().GetAreaByCID(companyID);
            sb.Append("<option value='0'>-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].AreaId != selectIndex)
                    {
                        sb.Append("<option data-type='" + ((int)list[i].Type).ToString() + "' value='" + list[i].AreaId + "'>" + list[i].AreaName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option data-type='" + ((int)list[i].Type).ToString() + "' value='" + list[i].AreaId + "' selected=selected>" + list[i].AreaName + "</option>");
                    }
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// 获得线路区域的Options
        /// </summary>
        /// <param name="selectIndex">设置选择项</param>
        /// <returns></returns>
        public static string GetAreaLineBySelect(int selectIndex, string companyID)
        {
            StringBuilder sb = new StringBuilder();
            IList<MAreaTour> list = new EyouSoft.BLL.TourStructure.BTour().GetAreaTour(companyID);
            sb.Append("<option value=\"0\">-请选择-</option>");
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].AreaId != selectIndex)
                    {
                        sb.Append("<option value=\"" + list[i].AreaId + "\">" + list[i].AreaName + "</option>");
                    }
                    else
                    {
                        sb.Append("<option  value=\"" + list[i].AreaId + "\" selected=\"selected\">" + list[i].AreaName + "</option>");
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回不同颜色的订单状态
        /// </summary>
        /// <param name="state">订单状态value值</param>
        /// <returns></returns>
        public static string GetOrderStateForHtml(string state)
        {
            StringBuilder sb = new StringBuilder();
            switch (state)
            {
                case "0": sb.Append(" <font class=\"fontgray\">未处理</font>"); break;
                case "1": sb.Append(" <font class=\"fontgreen\">已留位</font>"); break;
                case "2": sb.Append(" <font class=\"fontred\">留位过期</font>"); break;
                case "3": sb.Append(" <font class=\"fontgray\">不受理</font>"); break;
                case "4": sb.Append(" <font font-size='12px'>已成交</font>"); break;
                case "5": sb.Append(" <font class=\"fontgray\">已取消</font>"); break;
                case "6": sb.Append(" <font class=\"fontred\">垫付申请审核</font>"); break;
                case "7": sb.Append(" <font class=\"fontgreen\">垫付申请审核成功</font>"); break;
                case "8": sb.Append(" <font class=\"fontgray\">垫付申请审核失败</font>"); break;
                case "9": sb.Append(" <font class=\"fontred\"><a href='javascript:void(0)' data-class='chaoxian'>资金超限</a></font>"); break;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 供应商分销商 订单状态的颜色显示
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static string GetOrderStateForHtml(int state)
        {
            StringBuilder sb = new StringBuilder();
            switch (state)
            {
                case 0: sb.Append("<font class=\"Fontred\">未处理</font>"); break;
                case 1: sb.Append("<font class=\"Fontgreen\">已留位</font>"); break;
                case 2: sb.Append("<font class=\"Fontblue\">留位过期</font>"); break;
                case 3: sb.Append("<font class=\"Fontgray\">不受理</font>"); break;
                case 4: sb.Append("<font font-size='12px'>已成交</font>"); break;
                case 5: sb.Append("<font class=\"Fontgray\">已取消</font>"); break;
                case 6: sb.Append("<font class=\"Fontred\">垫付申请审核</font>"); break;
                case 7: sb.Append("<font class=\"Fontgreen\">垫付申请审核成功</font>"); break;
                case 8: sb.Append("<font class=\"Fontgray\">垫付申请审核失败</font>"); break;
                default: break;
            }
            return sb.ToString();
        }


        /// <summary>
        /// 获取审核状态下拉HTML
        /// </summary>
        /// <param name="selectedVal">选中value的值</param>
        /// <returns></returns>
        public static string GetExamineStatusDDLHTML(string selectVal)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"-1\">-请选择-</option>");
            sb.AppendFormat("<option value=\"{0}\" {1}>-{2}-</option>", "1", selectVal.Trim() == "1" ? "selected=\"selected\"" : string.Empty, "已审核");
            sb.AppendFormat("<option value=\"{0}\" {1}>-{2}-</option>", "2", selectVal.Trim() == "2" ? "selected=\"selected\"" : string.Empty, "未审核");
            return sb.ToString();
        }

        #region 获取枚举下拉菜单 create by cyn
        /// <summary>
        ///  获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls)
        {
            return GetEnumDDL(ls, string.Empty);
        }
        /// <summary>
        ///  获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, object selectedVal)
        {
            if (selectedVal == null)
            {
                return GetEnumDDL(ls);
            }
            else
            {
                return GetEnumDDL(ls, selectedVal.ToString());
            }

        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选择value值</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal)
        {
            return GetEnumDDL(ls, selectedVal ?? "-1", false);

        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选择value值</param>
        /// <param name="isFirst">是否存在默认请选择项</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, bool isFirst)
        {

            return GetEnumDDL(ls, selectedVal, isFirst, "-1", "-请选择-");
        }
        /// <summary>
        /// 获取枚举下拉菜单
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选中的值</param>
        /// <param name="defaultKey">默认值Id</param>
        /// <param name="defaultVal">默认值文本</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, string defaultKey, string defaultVal)
        {

            return GetEnumDDL(ls, selectedVal, true, defaultKey, defaultVal);
        }
        /// <summary>
        /// 获取枚举下拉菜单(该方法isFirst为否则后2个属性无效)
        /// </summary>
        /// <param name="ls">枚举列</param>
        /// <param name="selectedVal">选中的值</param>
        /// <param name="isFirst">是否有默认值</param>
        /// <param name="defaultKey">默认值Id</param>
        /// <param name="defaultVal">默认值文本</param>
        /// <returns></returns>
        public static string GetEnumDDL(List<EnumObj> ls, string selectedVal, bool isFirst, string defaultKey, string defaultVal)
        {
            StringBuilder sb = new StringBuilder();
            if (isFirst)
            {
                sb.Append("<option value=\"" + defaultKey + "\" selected=\"selected\">" + defaultVal + "</option>");
            }

            for (int i = 0; i < ls.Count; i++)
            {
                if (ls[i].Value != selectedVal.Trim())
                {
                    sb.Append("<option value=\"" + ls[i].Value.Trim() + "\">" + ls[i].Text.Trim() + "</option>");
                }
                else
                {
                    sb.Append("<option value=\"" + ls[i].Value.Trim() + "\" selected=\"selected\">" + ls[i].Text.Trim() + "</option>");
                }
            }
            return sb.ToString();
        }
        #endregion

        /// <summary>
        /// 绑定Enum至DropDownList
        /// </summary>
        /// <param name="ddl">DropDownList下拉菜单对象</param>
        /// <param name="e">枚举</param>
        public static void BindEnumToDDL(System.Web.UI.WebControls.DropDownList ddl, Enum e)
        {
            BindEnumToDDL(ddl, e, "Value", "Text", "0", "--请选择--", 0);
        }

        /// <summary>
        /// 绑定Enum至DropDownList
        /// </summary>
        /// <param name="ddl">DropDownList下拉菜单对象</param>
        /// <param name="e">枚举</param>
        /// <param name="dataValueField">值名称</param>
        /// <param name="dataTextField">文本名称</param>
        /// <param name="insertItemValue">插入值</param>
        /// <param name="insertItemText">插入文本</param>
        /// <param name="index">插入索引</param>
        public static void BindEnumToDDL(System.Web.UI.WebControls.DropDownList ddl, Enum e, string dataValueField, string dataTextField, string insertItemValue, string insertItemText, int index)
        {
            var items = EnumObj.GetList(e.GetType());
            if (index < 0 || items == null || items.Count == 0) index = 0;

            if (items != null && items.Count > 0)
            {
                if (index > items.Count) index = items.Count;

                ddl.DataSource = items;
                ddl.DataValueField = dataValueField;
                ddl.DataTextField = dataTextField;
                ddl.DataBind();
            }

            if (!string.IsNullOrEmpty(insertItemText))
            {
                ddl.Items.Insert(index, new System.Web.UI.WebControls.ListItem(insertItemText, insertItemValue));
            }
        }

        #region 返回计调图标
        /// <summary>
        /// 获取计调安排状态显示HTML
        /// </summary>
        /// <param name="status">安排状态</param>
        /// <param name="dataType">dataType</param>
        /// <param name="name">计调安排类型名称</param>
        /// <returns></returns>
        private static string GetJiDaioIconHtml(PlanState status, string dataType, string name)
        {
            if (status == PlanState.未安排)
            {
                return string.Format("<b class=\"fontred\"><span class='a_jidiaoicon'>{0}</span></b>", name);
            }
            else if (status == PlanState.未落实)
            {
                return string.Format("<b class=\"fontred\"  data-class=\"ShowSourceName\" ><span class='a_jidiaoicon'>{0}</span></b><div data-type=\"{1}\" style=\"display: none;\"></div>", name, dataType);
            }
            else if (status == PlanState.无计调任务)
            {
                return string.Format("<b class=\"fontgray\"><span class='a_jidiaoicon'>{0}</span></b>", name);
            }
            else if (status == PlanState.已落实)
            {
                return string.Format("<b class=\"fontblue\"  data-class=\"ShowSourceName\"><span class='a_jidiaoicon'>{0}</span></b><div data-type=\"{1}\" style=\"display: none;\"></div>", name, dataType);
            }

            return string.Empty;
        }

        /// <summary>
        /// 返回计调图标
        /// 无计调任务,未安排:灰色 未落实:红色 其它:蓝色
        /// </summary>
        /// <param name="MTourPlanStatus">计调</param>
        /// <returns></returns>
        public static string GetJiDiaoIcon(MTourPlanStatus MPS)
        {
            if (MPS != null)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(GetJiDaioIconHtml(MPS.DJ, "1", "接"));
                sb.Append(GetJiDaioIconHtml(MPS.Hotel, "2", "房"));
                sb.Append(GetJiDaioIconHtml(MPS.Dining, "3", "餐"));
                sb.Append(GetJiDaioIconHtml(MPS.Spot, "4", "景"));
                sb.Append(GetJiDaioIconHtml(MPS.Car, "5", "车"));
                sb.Append(GetJiDaioIconHtml(MPS.Guide, "7", "导"));
                sb.Append(GetJiDaioIconHtml(MPS.Shopping, "9", "购"));


                string ship = string.Empty;

                if (MPS.CShip == PlanState.无计调任务 || MPS.FShip == PlanState.无计调任务)
                {
                    ship = GetJiDaioIconHtml(PlanState.无计调任务, "6", "船");
                }
                if (MPS.CShip == PlanState.未安排 || MPS.FShip == PlanState.未安排)
                {
                    ship = GetJiDaioIconHtml(PlanState.未安排, "6", "船");
                }
                if (MPS.CShip == PlanState.未落实 || MPS.FShip == PlanState.未落实)
                {
                    ship = GetJiDaioIconHtml(PlanState.未落实, "6", "船");
                }
                if (MPS.CShip == PlanState.已落实 || MPS.FShip == PlanState.已落实)
                {
                    ship = GetJiDaioIconHtml(PlanState.已落实, "6", "船");
                }
                sb.Append(ship);

                string ticket = string.Empty;

                if (MPS.TrainTicket == PlanState.无计调任务 || MPS.PlaneTicket == PlanState.无计调任务 || MPS.CarTicket == PlanState.无计调任务)
                {
                    ticket = GetJiDaioIconHtml(PlanState.无计调任务, "8", "票");
                }
                if (MPS.TrainTicket == PlanState.未安排 || MPS.PlaneTicket == PlanState.未安排 || MPS.CarTicket == PlanState.未安排)
                {
                    ticket = GetJiDaioIconHtml(PlanState.未安排, "8", "票");
                }
                if (MPS.TrainTicket == PlanState.未落实 || MPS.PlaneTicket == PlanState.未落实 || MPS.CarTicket == PlanState.未落实)
                {
                    ticket = GetJiDaioIconHtml(PlanState.未落实, "8", "票");
                }
                if (MPS.TrainTicket == PlanState.已落实 || MPS.PlaneTicket == PlanState.已落实 || MPS.CarTicket == PlanState.已落实)
                {
                    ticket = GetJiDaioIconHtml(PlanState.已落实, "8", "票");
                }
                sb.Append(ticket);

                return sb.ToString();
            }
            else
            {
                return "<b class=\"fontgray\"><span class='a_jidiaoicon'>接</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>房</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>餐</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>景</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>车</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>船</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>导</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>票</span></b><b class=\"fontgray\"><span class='a_jidiaoicon'>购</span></b>";
            }
        }
        #endregion

        #region 格式转换 create by dyz
        /// <summary>
        /// 金额显示格式处理
        /// </summary>
        /// <param name="m">金额</param>
        /// <param name="name">预定义的 System.Globalization.CultureInfo 名称或现有 System.Globalization.CultureInfo名称</param>
        /// <returns></returns>
        public static string GetMoneyString(decimal m, string name)
        {
            System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture(name);

            return m.ToString("c2", cultureInfo);
        }

        /// <summary>
        /// 金额显示格式处理
        /// </summary>
        /// <param name="o">金额</param>
        /// <param name="name">预定义的 System.Globalization.CultureInfo 名称或现有 System.Globalization.CultureInfo名称</param>
        /// <returns></returns>
        public static string GetMoneyString(object o, string name)
        {
            if (o != null)
            {
                return GetMoneyString(Utils.GetDecimal(o.ToString()), name);
            }

            return string.Empty;
        }

        /// <summary>
        /// 时间显示格式处理
        /// </summary>
        /// <param name="d">时间值</param>
        /// <param name="format">格式字符串。</param>
        /// <returns></returns>
        public static string GetDateString(DateTime d, string format)
        {
            if (d == null || d.ToString() == "" || d.Equals(Utils.GetDateTime("1900-1-1 0:00:00")) || d.Equals(Utils.GetDateTime("0001-01-01 0:00:00")))
            {
                return "";
            }
            else
            {
                return d.ToString(format);
            }
        }

        /// <summary>
        /// 时间显示格式处理
        /// </summary>
        /// <param name="d">时间值</param>
        /// <param name="format">格式字符串。</param>
        /// <returns></returns>
        public static string GetDateString(object d, string format)
        {
            if (d != null)
            {
                return GetDateString(Utils.GetDateTime(d.ToString()), format);
            }

            return string.Empty;
        }
        #endregion

        #region 获得计划行程集合 create by dyz
        /// <summary>
        /// 获得计划行程集合
        /// </summary>
        /// <returns></returns>
        public static IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> GetPlanList()
        {
            IList<EyouSoft.Model.TourStructure.MPlanBaseInfo> list = new List<EyouSoft.Model.TourStructure.MPlanBaseInfo>();
            //区间
            string[] section = Utils.GetFormValues("txt_qujin");
            //酒店
            string[] hotelName = Utils.GetFormValues("txtHotelName");
            //早餐
            string[] breakfast = Utils.GetFormValues("eatFrist");
            //中餐
            string[] lunch = Utils.GetFormValues("eatSecond");
            //晚餐
            string[] supper = Utils.GetFormValues("eatThird");
            //景区
            string[] spotId = Utils.GetFormValues("hd_scenery_spot");
            string[] sportName = Utils.GetFormValues("show_scenery_spot");
            //交通
            string[] traffic = Utils.GetFormValues("txtTraffic");
            //行程内容
            string[] txtRemarks = Utils.GetFormEditorValues("txtContent");
            //附件
            string[] files = Utils.GetFormValues("hide_Journey_file");


            if (section.Length > 0)
            {
                for (int i = 0; i < section.Length; i++)
                {
                    EyouSoft.Model.TourStructure.MPlanBaseInfo model = new EyouSoft.Model.TourStructure.MPlanBaseInfo();
                    model.Section = section[i].Trim();
                    model.Hotel = hotelName[i].Trim();

                    model.Breakfast = breakfast[i] == "1" ? true : false;
                    model.Lunch = lunch[i] == "1" ? true : false;
                    model.Supper = supper[i] == "1" ? true : false;

                    model.Content = Utils.EditInputText(txtRemarks[i]);
                    model.Traffic = traffic[i];
                    model.Days = (i + 1);
                    //声明景区集合对象
                    IList<EyouSoft.Model.TourStructure.MTourPlanSpot> spotList = new List<EyouSoft.Model.TourStructure.MTourPlanSpot>();
                    if (spotId.Length > 0 && spotId[i].Length > 0)
                    {
                        string[] spotIdArray = spotId[i].Split(',');
                        string[] spotNameArray = sportName[i].Split(',');
                        if (spotIdArray.Length > 0)
                        {
                            for (int j = 0; j < spotIdArray.Length; j++)
                            {
                                EyouSoft.Model.TourStructure.MTourPlanSpot spotModel = new EyouSoft.Model.TourStructure.MTourPlanSpot();
                                spotModel.SpotId = spotIdArray[j];
                                spotModel.SpotName = System.Web.HttpContext.Current.Server.UrlDecode(spotNameArray[j]);
                                spotList.Add(spotModel);
                            }
                        }
                        else
                        {
                            EyouSoft.Model.TourStructure.MTourPlanSpot spotModel = new EyouSoft.Model.TourStructure.MTourPlanSpot();
                            spotModel.SpotId = spotId[i];
                            spotModel.SpotName = sportName[i];
                            spotList.Add(spotModel);
                        }
                    }
                    model.TourPlanSpot = spotList;
                    if (files[i].Split('|').Length > 1)
                    {
                        model.FilePath = files[i].Split('|')[1];
                    }
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion

        #region 获得报价等级 create by dyz
        /// <summary>
        /// 获得散拼报价等级
        /// </summary>
        /// <returns></returns>
        public static IList<EyouSoft.Model.TourStructure.MTourPriceStandard> GetPriceStandard()
        {
            IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list = null;
            //获得页面等级
            string[] standardAllList = Utils.GetFormValues("hide_PriceStand_LevelData");
            //获得选择的类型
            string[] priceStandardType = Utils.GetFormValues("sel_PriceStandard_type");
            if (priceStandardType.Length > 0)
            {
                list = new List<EyouSoft.Model.TourStructure.MTourPriceStandard>();
                for (int i = 0; i < priceStandardType.Length; i++)
                {
                    EyouSoft.Model.TourStructure.MTourPriceStandard m = new EyouSoft.Model.TourStructure.MTourPriceStandard();
                    m.Standard = Utils.GetInt(priceStandardType[i]);

                    //等级
                    m.PriceLevel = new List<EyouSoft.Model.TourStructure.MTourPriceLevel>();
                    //获得页面等级数量
                    if (standardAllList.Length > 0)
                    {
                        for (int j = 0; j < standardAllList.Length; j++)
                        {
                            EyouSoft.Model.TourStructure.MTourPriceLevel l = new EyouSoft.Model.TourStructure.MTourPriceLevel();
                            string[] priceLevelArray = standardAllList[j].Split('|');
                            l.AdultPrice = Utils.GetDecimal(Utils.GetFormValue("txt_PriceStand_Adult_" + priceLevelArray[0] + "_" + i.ToString()));
                            l.ChildPrice = Utils.GetDecimal(Utils.GetFormValue("txt_PriceStand_Child_" + priceLevelArray[0] + "_" + i.ToString()));

                            l.LevelId = Utils.GetInt(priceLevelArray[0]);
                            l.LevelName = priceLevelArray[1];
                            l.LevType = (EyouSoft.Model.EnumType.ComStructure.LevType)Utils.GetInt(priceLevelArray[2]);
                            m.PriceLevel.Add(l);
                        }
                    }
                    int count = list.Where(p => p.Standard == m.Standard).Count();
                    if (count == 0)
                    {
                        list.Add(m);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 获得计划服务 create by dyz
        /// <summary>
        /// 从表单中获得计划服务
        /// </summary>
        /// <param name="costCalculation">out 成本核算</param>
        /// <returns></returns>
        public static EyouSoft.Model.TourStructure.MTourService GetTourService(out string costCalculation)
        {
            EyouSoft.Model.TourStructure.MTourService model = new EyouSoft.Model.TourStructure.MTourService();
            costCalculation = Utils.GetFormValue("txt_CostAccounting_CostCalculation");
            model.ChildServiceItem = Utils.GetFormValue("txt_CostAccounting_ChildServiceItem");
            model.InsiderInfor = Utils.GetFormValue("txt_CostAccounting_InsiderInfor");
            model.NeedAttention = Utils.GetFormValue("txt_CostAccounting_NeedAttention");
            model.NoNeedItem = Utils.GetFormValue("txt_CostAccounting_NoNeedItem");
            model.OwnExpense = Utils.GetFormValue("txt_CostAccounting_OwnExpense");
            model.ServiceStandard = Utils.GetFormValue("txt_CostAccounting_ServiceStandard");
            model.ShoppingItem = Utils.GetFormValue("txt_CostAccounting_ShoppingItem");
            model.WarmRemind = Utils.GetFormValue("txt_CostAccounting_WarmRemind");
            return model;
        }
        #endregion

        #region 获得报价服务 create by dyz
        /// <summary>
        /// 获得对外报价信息
        /// </summary>
        /// <returns>key=IsTourOrSubentry,Service</returns>
        public static Dictionary<string, object> GetServiceType()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            bool IsTourOrSubentry = Utils.GetFormValue("rdo_ForeignQuote_SeviceType") == "1" ? true : false;
            dic.Add("IsTourOrSubentry", (object)IsTourOrSubentry);

            bool IsFromTourOrRoute = Utils.GetFormValue("hide_IsFromTourOrRoute") == "True" ? true : false;
            if (IsTourOrSubentry)
            {
                dic.Add("Service", (object)Utils.GetFormValue("txt_ForeignQuote_ZtService"));
                return dic;
            }
            else
            {
                string[] type = Utils.GetFormValues("slt_ForeignQuote_PlanProject");
                string[] price = Utils.GetFormValues("txt_ForeignQuote_Price");
                string[] service = Utils.GetFormValues("txt_ForeignQuote_Service");
                string[] unit = Utils.GetFormValues("slt_ForeignQuote_Price");  //1= 元/人 2=元/团

                IList<EyouSoft.Model.SourceStructure.MRouteStandard> listRoute = new List<EyouSoft.Model.SourceStructure.MRouteStandard>();
                IList<EyouSoft.Model.TourStructure.MTourTeamPrice> listTour = new List<EyouSoft.Model.TourStructure.MTourTeamPrice>();

                if (type.Length > 0)
                {
                    for (int i = 0; i < type.Length; i++)
                    {
                        EyouSoft.Model.SourceStructure.MRouteStandard modelRoute = new EyouSoft.Model.SourceStructure.MRouteStandard();
                        modelRoute.Standard = service[i];
                        modelRoute.Type = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(type[i].Split('|')[0]);
                        modelRoute.Unit = (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)Utils.GetInt(unit[i]);
                        modelRoute.UnitPrice = Utils.GetDecimal(price[i]);
                        listRoute.Add(modelRoute);

                        EyouSoft.Model.TourStructure.MTourTeamPrice modelTour = new EyouSoft.Model.TourStructure.MTourTeamPrice();
                        modelTour.ServiceStandard = service[i];
                        modelTour.ServiceType = (EyouSoft.Model.EnumType.ComStructure.ContainProjectType)Utils.GetInt(type[i].Split('|')[0]);
                        modelTour.Unit = (unit.Length >= i + 1) ? (EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit)Utils.GetInt(unit[i]) : EyouSoft.Model.EnumType.ComStructure.ContainProjectUnit.人;
                        modelTour.Quote = Utils.GetDecimal(price[i]);
                        listTour.Add(modelTour);
                    }
                }
                if (IsFromTourOrRoute)
                {
                    dic.Add("Service", (object)listTour);
                    return dic;
                }
                else
                {
                    dic.Add("Service", (object)listRoute);
                    return dic;
                }
            }
        }
        #endregion

        #region 获得散拼计划价格信息
        public static string GetPriceStandardTable(IList<EyouSoft.Model.TourStructure.MTourPriceStandard> list, string providerToMoney)
        {
            StringBuilder sb = new StringBuilder();
            if (list != null && list.Count > 0)
            {
                #region 拼接表头
                sb.Append("<table cellspacing='0' cellpadding='0' border='1' bordercolor='#EAB25A' width='100%' class='pp-tableclass2'>");
                sb.Append("<tr class='pp-table-title'>");
                sb.Append("<th height='23' align='center' rowspans='2'>标准</th>");
                #endregion
                //统计是否是第一次循环intcount=0为第一次加载 循环表头 intcount=1第二次 不循环表头
                int intcount = 0;
                //循环报价标准
                foreach (MTourPriceStandard item in list)
                {
                    if (intcount == 0)
                    {
                        //循环表头
                        foreach (MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                        {
                            sb.Append("<th align='center' colspan='2'>" + modelMTourPriceLevel.LevelName + "</th>");
                        }
                        sb.Append("<tr class='pp-table-title'>");
                        sb.Append("<td align='center'></td>");
                        //循环表头
                        foreach (MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                        {
                            sb.Append("<td align='center'>成人</td><td align='center'>儿童</td>");
                        }
                        sb.Append("<tr>");
                        intcount = 1;
                    }
                    sb.Append("<td align='center' nowrap='nowrap'>" + item.StandardName + "</td>");
                    //循环表体数据
                    foreach (MTourPriceLevel modelMTourPriceLevel in item.PriceLevel)
                    {
                        sb.Append("<td align='center' nowrap='nowrap'>" + UtilsCommons.GetMoneyString(modelMTourPriceLevel.AdultPrice, providerToMoney) + "</td><td align='center'>" + UtilsCommons.GetMoneyString(modelMTourPriceLevel.ChildPrice, providerToMoney) + "</td>");
                    }
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
            }
            return sb.ToString();
        }
        #endregion

        #region 财务 销售 获取支付方式  create by cyn
        /// <summary>
        /// 财务 销售 获取支付方式
        /// </summary>
        /// <param name="currentUserCompanyID">公司Id</param>
        /// <param name="itemType">收入支出枚举</param>
        /// <returns>支付方式下拉字符串</returns>
        public static string GetStrPaymentList(string currentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType itemType)
        {
            var setting = EyouSoft.Security.Membership.UserProvider.GetComSetting(currentUserCompanyID);
            int zffs = 0;

            if (setting != null)
            {
                if (itemType == EyouSoft.Model.EnumType.ComStructure.ItemType.收入) zffs = setting.ShouKuanZhiFuFangShi;
                else zffs = setting.FuKuanZhiFuFangShi;
            }

            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(currentUserCompanyID, null, itemType);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ls)
                {
                    if (item.PaymentId == zffs)
                    {
                        sb.Append("<option value='" + item.PaymentId + "' selected='selected'>" + item.Name + "</option>");
                    }
                    else
                    {
                        sb.Append("<option value='" + item.PaymentId + "'>" + item.Name + "</option>");
                    }
                }
                return sb.ToString();
            }

            return "<option value=-1>-无支付方式-</option>";
        }



        /// <summary>
        /// 财务 销售 获取支付方式 
        /// create by wl
        /// </summary>
        /// <param name="currentUserCompanyID">公司Id</param>
        /// <param name="itemType">收入支出枚举</param>
        /// <returns>支付方式下拉字符串</returns>
        public static string GetStrPaymentList(string currentUserCompanyID, string currentPayModel, EyouSoft.Model.EnumType.ComStructure.ItemType itemType)
        {
            IList<EyouSoft.Model.ComStructure.MComPayment> ls = new EyouSoft.BLL.ComStructure.BComPayment().GetList(currentUserCompanyID, null, itemType);
            if (ls != null && ls.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in ls)
                {
                    if (!string.IsNullOrEmpty(currentPayModel) && Utils.GetInt(currentPayModel) == item.PaymentId)
                    {

                        sb.Append("<option selected='selected' value=" + item.PaymentId + ">" + item.Name + "</option>");

                    }
                    else
                    {
                        sb.Append("<option value=" + item.PaymentId + ">" + item.Name + "</option>");
                    }
                }
                return sb.ToString();
            }
            return "<option value=-1>-无支付方式-</option>";
        }
        /// <summary>
        /// 财务 销售 获取支付方式
        /// </summary>
        /// <param name="currentUserCompanyID">公司Id</param>
        /// <param name="itemType">收入支出枚举</param>
        /// <returns>支付方式实体列表</returns>
        public static IList<EyouSoft.Model.ComStructure.MComPayment> GetPaymentList(string currentUserCompanyID, EyouSoft.Model.EnumType.ComStructure.ItemType itemType)
        {
            return new EyouSoft.BLL.ComStructure.BComPayment().GetList(currentUserCompanyID, null, itemType);
        }
        #endregion

        #region 获得游客集合 create by dyz
        public static IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTravelList()
        {
            //旅客编号
            string[] treavelID = Utils.GetFormValues("txt_TravelControl_TreavelID");
            //旅客姓名
            string[] txtUserName = Utils.GetFormValues("txt_TravelControl_Name");
            //旅客类型
            string[] peopleType = Utils.GetFormValues("slt_TravelControl_PeopleType");
            //证件类型
            string[] proveType = Utils.GetFormValues("slt_TravelControl_ProveType");
            //证件号码
            string[] txtProveNumber = Utils.GetFormValues("txt_TravelControl_Prove");
            //性别
            string[] txtUserSex = Utils.GetFormValues("slt_TravelControl_UserSex");
            //电话
            string[] txtPhone = Utils.GetFormValues("txt_TravelControl_Phone");
            //备注
            string[] txtRemarks = Utils.GetFormValues("txt_TravelControl_Remarks");
            //出团提醒
            string[] cbxLeaveMsg = Utils.GetFormValues("cbx_TravelControl_LeaveMsg");
            //回团提醒
            string[] cbxBackMsg = Utils.GetFormValues("cbx_TravelControl_BackMsg");
            //保险
            string[] hideInsuranceID = Utils.GetFormValues("hideInsuranceID");
            string[] hideInsuranceName = Utils.GetFormValues("hideInsuranceName");
            string[] hideInsurancePrice = Utils.GetFormValues("hideInsurancePrice");
            string[] hideInsuranceCount = Utils.GetFormValues("hideInsuranceCount");

            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
            if (txtUserName.Length > 0 && peopleType.Length > 0 && proveType.Length > 0 && txtProveNumber.Length > 0 && txtUserSex.Length > 0 && txtPhone.Length > 0 && txtRemarks.Length > 0 && cbxBackMsg.Length > 0 && cbxLeaveMsg.Length > 0)
            {
                for (int i = 0; i < txtUserName.Length; i++)
                {
                    if (!string.IsNullOrEmpty(txtUserName[i]))
                    {
                        EyouSoft.Model.TourStructure.MTourOrderTraveller model = new EyouSoft.Model.TourStructure.MTourOrderTraveller();
                        //旅客姓名
                        model.CnName = txtUserName[i];
                        //证件号码
                        model.CardNumber = txtProveNumber[i];
                        //联系电话
                        model.Contact = txtPhone[i];
                        //编号
                        model.TravellerId = treavelID[i];
                        //游客类型
                        model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(peopleType[i]);
                        //证件类型
                        model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)Utils.GetInt(proveType[i]);
                        //旅客性别
                        model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(txtUserSex[i]);
                        //备注
                        model.Remark = txtRemarks[i];
                        //出团提醒
                        model.LNotice = cbxLeaveMsg[i] == "1" ? true : false;
                        //回团提醒
                        model.RNotice = cbxBackMsg[i] == "1" ? true : false;
                        #region 保险设置
                        if (hideInsuranceID.Length > 0 && hideInsuranceName.Length > 0 && hideInsurancePrice.Length > 0 && hideInsuranceCount.Length > 0)
                        {
                            if (hideInsuranceID[i].Trim() != "" && hideInsuranceName[i].Trim() != "" && hideInsurancePrice[i].Trim() != "" && hideInsuranceCount[i].Trim() != "")
                            {
                                IList<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance> insuranceList = new List<EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance>();

                                string[] idList = hideInsuranceID[i].Split(',');
                                string[] nameList = hideInsuranceName[i].Split(',');
                                string[] priceList = hideInsurancePrice[i].Split(',');
                                string[] countList = hideInsuranceCount[i].Split(',');
                                if (idList.Length > 0 && nameList.Length > 0 && priceList.Length > 0 && countList.Length > 0)
                                {
                                    for (int j = 0; j < idList.Length; j++)
                                    {
                                        EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance itemModel = new EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance();
                                        itemModel.TravellerId = treavelID[i];
                                        itemModel.InsuranceId = idList[j];
                                        itemModel.UnitPrice = Utils.GetDecimal(priceList[j]);
                                        itemModel.BuyNum = Utils.GetInt(countList[j]);
                                        itemModel.SumPrice = itemModel.UnitPrice * itemModel.BuyNum;
                                        insuranceList.Add(itemModel);
                                    }
                                }
                                else
                                {
                                    EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance itemModel = new EyouSoft.Model.TourStructure.MTourOrderTravellerInsurance();
                                    itemModel.InsuranceId = hideInsuranceID[0];
                                    itemModel.UnitPrice = Utils.GetDecimal(hideInsurancePrice[0]);
                                    itemModel.BuyNum = Utils.GetInt(hideInsuranceCount[0]);
                                    itemModel.SumPrice = itemModel.UnitPrice * itemModel.BuyNum;
                                    insuranceList.Add(itemModel);
                                }

                                model.OrderTravellerInsuranceList = insuranceList;
                            }
                        }
                        #endregion
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public static IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> GetTravelListS()
        {

            //旅客编号
            string[] travellerId = Utils.GetFormValues("txt_TravelControlS_TreavelID");
            //旅客姓名Cn
            string[] userNameCn = Utils.GetFormValues("txt_TravelControlS_TreavelID_NameCn");
            //旅客姓名En
            string[] userNameEn = Utils.GetFormValues("txt_TravelControlS_TreavelID_NameEn");
            //证件类型
            string[] proveType = Utils.GetFormValues("slt_TravelControlS_ProveType");
            //身份证
            string[] userSfz = Utils.GetFormValues("slt_TravelControlS_Sfz");
            //旅客类型
            string[] peopleType = Utils.GetFormValues("slt_TravelControlS_PeoType");

            //证件号码
            string[] txtProveNumber = Utils.GetFormValues("txt_TravelControlS_CardNum");
            //性别
            string[] txtUserSex = Utils.GetFormValues("slt_TravelControlS_Sex");
            //电话
            string[] txtPhone = Utils.GetFormValues("txt_TravelControlS_ContactTel");
            //备注
            string[] txtRemarks = Utils.GetFormValues("txt_TravelControlS_Remarks");
            //出团提醒
            string[] cbxLeaveMsg = Utils.GetFormValues("cbx_TravelControlS_LeaveMsg");
            //回团提醒
            string[] cbxBackMsg = Utils.GetFormValues("cbx_TravelControlS_BackMsg");
            //证件有效期
            string[] txtValidDate = Utils.GetFormValues("txt_TravelControlS_ValidDate");
            //是否办理
            string[] cbxHandle = Utils.GetFormValues("cbx_TravelControlS_IsBan");
            //签证状体
            string[] visaStatus = Utils.GetFormValues("slt_TravelControlS_State");
            IList<EyouSoft.Model.TourStructure.MTourOrderTraveller> list = new List<EyouSoft.Model.TourStructure.MTourOrderTraveller>();
            if (userNameCn.Length > 0 && peopleType.Length > 0 && proveType.Length > 0 && txtProveNumber.Length > 0 && txtUserSex.Length > 0 && txtPhone.Length > 0 && txtRemarks.Length > 0 && cbxBackMsg.Length > 0 && cbxLeaveMsg.Length > 0)
            {

                for (int i = 0; i < userNameCn.Length; i++)
                {

                    EyouSoft.Model.TourStructure.MTourOrderTraveller model = new EyouSoft.Model.TourStructure.MTourOrderTraveller();

                    //旅客姓名
                    model.CnName = userNameCn[i];
                    model.EnName = userNameEn[i];
                    //证件号码
                    model.CardNumber = txtProveNumber[i];
                    //身份证
                    model.CardId = userSfz[i];
                    //联系电话
                    model.Contact = txtPhone[i];
                    //编号
                    model.TravellerId = travellerId[i];
                    //游客类型
                    model.VisitorType = (EyouSoft.Model.EnumType.TourStructure.VisitorType)Utils.GetInt(peopleType[i]);
                    //证件类型
                    model.CardType = (EyouSoft.Model.EnumType.TourStructure.CardType)Utils.GetInt(proveType[i]);
                    //旅客性别
                    model.Gender = (EyouSoft.Model.EnumType.GovStructure.Gender)Utils.GetInt(txtUserSex[i]);
                    //备注
                    model.Remark = txtRemarks[i];
                    //出团提醒
                    model.LNotice = cbxLeaveMsg[i] == "1" ? true : false;
                    //回团提醒
                    model.RNotice = cbxBackMsg[i] == "1" ? true : false;
                    //证件有效期
                    model.CardValidDate = txtValidDate[i];
                    //是否办理
                    model.IsCardTransact = cbxHandle[i] == "1" ? true : false;
                    //签证状态
                    model.VisaStatus = (EyouSoft.Model.EnumType.TourStructure.VisaStatus)Utils.GetInt(visaStatus[i]);
                    list.Add(model);

                }
            }
            return list;
        }
        #endregion

        #region 获取公司是是否被推荐或被签单
        /// <summary>
        /// 给属于推荐的公司加上推荐的图标
        /// </summary>
        /// <param name="IsRecommend">推荐</param>
        /// <param name="IsPermission">签单</param>
        /// <returns></returns>
        public static string GetCompanyRecommend(object IsRecommend, object IsPermission)
        {
            bool isRecommend = Convert.ToBoolean(IsRecommend);
            bool isPermission = Convert.ToBoolean(IsPermission);
            string Imgstr = string.Empty;
            if (isRecommend)
            {
                Imgstr += "<img src=\"/images/jian.gif\" width=\"13px\" height=\"13px\" title=\"推荐\"/> ";
            }
            if (isPermission)
            {
                Imgstr += " <img src=\"/images/qian.gif\" width=\"13px\" height=\"13px\" title=\"签单\"/>";
            }
            return Imgstr;
        }

        /// <summary>
        /// 给属于推荐的公司加上对应的图标
        /// </summary>
        /// <param name="IsRecommend">推荐</param>
        /// <param name="IsPermission">签单</param>
        /// <param name="IsCommission">返利</param>
        /// <returns></returns>
        public static string GetCompanyRecommend(object IsRecommend, object IsPermission, object IsCommission)
        {
            bool isRecommend = Convert.ToBoolean(IsRecommend);
            bool isPermission = Convert.ToBoolean(IsPermission);
            bool isCommission = Convert.ToBoolean(IsCommission);
            string Imgstr = string.Empty;
            if (isRecommend)
            {
                Imgstr += "<img src=\"/images/jian.gif\" width=\"13px\" height=\"13px\" title=\"推荐\"/> ";
            }
            if (isPermission)
            {
                Imgstr += " <img src=\"/images/qian.gif\" width=\"13px\" height=\"13px\" title=\"签单\"/>";
            }
            if (isCommission)
            {
                Imgstr += " <img src=\"/images/fan.gif\" width=\"13px\" height=\"13px\" title=\"返利\"/>";
            }
            return Imgstr;
        }

        #endregion

        #region 供应商列表联系人信息（泡泡显示）
        /// <summary>
        /// 获取联系人信息
        /// </summary>
        /// <param name="id">SourceId</param>
        /// <returns></returns>
        public static string GetContactInfo(object LinkManList)
        {
            IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = (IList<EyouSoft.Model.CrmStructure.MCrmLinkman>)LinkManList;
            StringBuilder contactinfo = new StringBuilder();
            contactinfo.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>编号</th><th align='center'>联系人</th><th align='center'>电话</th><th align='center'>手机</th><th align='center'>传真</th><th align='center'>E-mail</th></tr>");
            for (int i = 0; i < list.Count; i++)
            {
                contactinfo.Append("<tr><td align='center'>" + (i + 1).ToString() + "</td><td align='center'>" + list[i].Name + "</td><td align='center' >" + list[i].Telephone + "</td><td align='center'>" + list[i].MobilePhone + "</td><td align='center'>" + list[i].Fax + "</td><td align='center'>" + list[i].EMail + "</td></tr>");
            }
            contactinfo.Append("</table>");
            return contactinfo.ToString();
        }

        /// <summary>
        /// 获取供应商联系人浮动信息HTML
        /// </summary>
        /// <param name="lxrs"></param>
        /// <returns></returns>
        public static string GetGysLxrFuDongHtml(object lxrs)
        {
            if (lxrs == null) return string.Empty;
            IList<EyouSoft.Model.GysStructure.MLxrInfo> items = (IList<EyouSoft.Model.GysStructure.MLxrInfo>)lxrs;
            
            StringBuilder s = new StringBuilder();
            s.Append("<table cellspacing='0' cellpadding='0' border='0' width='100%' class='pp-tableclass'><tr class='pp-table-title'><th height='23' align='center'>编号</th><th align='center'>联系人</th><th align='center'>电话</th><th align='center'>手机</th><th align='center'>传真</th><th align='center'>E-mail</th></tr>");
            if (items != null && items.Count > 0)
            {
                int i = 1;
                foreach (var item in items)
                {
                    s.Append("<tr><td align='center'>" + i.ToString() + "</td><td align='center'>" + item.Name + "</td><td align='center' >" + item.Telephone + "</td><td align='center'>" + item.Mobile + "</td><td align='center'>" + item.Fax + "</td><td align='center'>" + item.Email + "</td></tr>");
                    i++;
                }
            }
            s.Append("</table>");

            return s.ToString();
        }
        #endregion

        #region  联系人信息

        /// <summary>
        /// 获取联系人信息
        /// </summary>
        public static IList<EyouSoft.Model.CrmStructure.MCrmLinkman> GetDataList()
        {
            //联系人编号
            string[] UserId = Utils.GetFormValues("UserId");
            //联系人姓名
            string[] ContactName = Utils.GetFormValues("Name");
            //联系人电话
            string[] TelPhone = Utils.GetFormValues("TelPhone");
            //联系人手机
            string[] Mobel = Utils.GetFormValues("Mobel");
            //联系人QQ
            string[] QQ = Utils.GetFormValues("QQ");
            //联系人Email
            string[] Email = Utils.GetFormValues("Email");
            //联系人职务
            string[] Post = Utils.GetFormValues("Post");
            //生日
            string[] Birthday = Utils.GetFormValues("Birthday");
            //传真
            string[] Fax = Utils.GetFormValues("Fax");
            //是否短信提醒生日
            string[] IsRemind = Utils.GetFormValues("hdremind");
            string errorMsg = "";

            if (ContactName.Length > 0)
            {
                IList<EyouSoft.Model.CrmStructure.MCrmLinkman> list = new List<EyouSoft.Model.CrmStructure.MCrmLinkman>();
                for (int i = 0; i < ContactName.Length; i++)
                {
                    if (!String.IsNullOrEmpty(ContactName[i].ToString()))
                    {
                        EyouSoft.Model.CrmStructure.MCrmLinkman model = new EyouSoft.Model.CrmStructure.MCrmLinkman();
                        if (ContactName[i].Trim() == "")
                        {
                            errorMsg += "第" + (i + 1).ToString() + "个联系人姓名不能为空! &nbsp;";
                            return null;
                        }
                        else
                            model.Name = ContactName[i];//姓名
                        model.EMail = Email[i];
                        //联系电话
                        model.Telephone = TelPhone[i];
                        //QQ
                        model.QQ = QQ[i];
                        //职务
                        model.Post = Post[i];
                        //手机
                        model.MobilePhone = Mobel[i];
                        model.Birthday = Utils.GetDateTimeNullable(Birthday[i]);
                        model.Fax = Fax[i];
                        model.IsRemind = IsRemind[i].ToString() == "1" ? true : false;
                        model.IsDeleted = false;
                        list.Add(model);
                    }
                }
                if (errorMsg != "")
                    return null;
                else
                    return list;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 处理默认时间
        /// <summary>
        /// 处理默认时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string SetDateTimeFormart(DateTime? dt)
        {
            if (dt == null || dt.Equals(Utils.GetDateTime("1900-1-1 0:00:00")) || dt.Equals(Utils.GetDateTime("0001-01-01 0:00:00")))
            {
                return "";
            }
            else
            {
                return UtilsCommons.GetDateString(dt, "yyyy-MM-dd");
            }
        }
        #endregion

        #region 获取分页页索引
        /// <summary>
        /// 获取分页页索引，url页索引查询参数为Page
        /// </summary>
        /// <returns></returns>
        public static int GetPadingIndex()
        {
            return GetPadingIndex("Page");
        }

        /// <summary>
        /// 获取分页页索引
        /// </summary>
        /// <param name="s">url页索引查询参数</param>
        /// <returns></returns>
        public static int GetPadingIndex(string s)
        {
            int index = Utils.GetInt(Utils.GetQueryStringValue(s), 1);

            return index < 1 ? 1 : index;
        }
        #endregion

        #region 获取URI，拼接sl查询参数
        /// <summary>
        /// 获取URI，拼接sl查询参数
        /// </summary>
        /// <param name="uri">uri</param>
        /// <returns></returns>
        public static string GetSLUri(string uri)
        {
            return GetSLUri(uri, Utils.GetQueryStringValue("sl"));
        }

        /// <summary>
        /// 获取URI，拼接sl查询参数
        /// </summary>
        /// <param name="uri">uri</param>
        /// <param name="sl">sl查询参数</param>
        /// <returns></returns>
        public static string GetSLUri(string uri, string sl)
        {
            if (string.IsNullOrEmpty(sl)) sl = Utils.GetQueryStringValue("sl");
            if (string.IsNullOrEmpty(uri)) return "?sl=" + sl;

            if (uri.IndexOf('?') > -1) return uri + "&sl=" + sl;

            return uri + "?sl=" + sl;
        }
        #endregion

        #region ajax request,response josn data.  create by cyn
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode)
        {
            return AjaxReturnJson(retCode, string.Empty);
        }
        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <param name="retMsg">return msg</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode, string retMsg)
        {
            return AjaxReturnJson(retCode, retMsg, null);
        }

        /// <summary>
        /// ajax request,response josn data
        /// </summary>
        /// <param name="retCode">return code</param>
        /// <param name="retMsg">return msg</param>
        /// <param name="retObj">return object</param>
        /// <returns></returns>
        public static string AjaxReturnJson(string retCode, string retMsg, object retObj)
        {
            string output = "{}";

            if (retObj != null)
            {
                output = Newtonsoft.Json.JsonConvert.SerializeObject(retObj);
            }

            return string.Format("{{\"result\":\"{0}\",\"msg\":\"{1}\",\"obj\":{2}}}", retCode, retMsg, output);
        }
        #endregion
        #region 拼接 导游,计调 字符串
        /// <summary>
        /// 拼接导游字符串
        /// </summary>
        /// <param name="ls">导游实体集合</param>
        /// <returns></returns>
        public static string PingGuide(object ls)
        {
            try
            {
                return PingGuide((IList<MGuidInfo>)ls);
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// 拼接导游字符串
        /// </summary>
        /// <param name="ls">导游实体集合</param>
        /// <returns></returns>
        public static string PingGuide(IList<MGuidInfo> ls)
        {
            if (ls == null || ls.Count == 0) return string.Empty;

            string s = string.Empty;

            foreach (var item in ls)
            {
                if (s.IndexOf(item.Name) > -1) continue;
                s += item.Name + ",";
            }

            if (string.IsNullOrEmpty(s)) return string.Empty;

            return s.Substring(0, s.Length - 1);
        }
        /// <summary>
        /// 拼接游客字符串
        /// </summary>
        /// <param name="ls">游客实体集合</param>
        /// <returns></returns>
        public static string PinTravel(object ls)
        {
            try
            {
                return PinTravel((IList<MTourOrderTraveller>)ls);
            }
            catch
            {

                return string.Empty;
            }
        }
        /// <summary>
        /// 拼接游客字符串
        /// </summary>
        /// <param name="ls">游客实体集合</param>
        /// <returns></returns>
        public static string PinTravel(IList<MTourOrderTraveller> ls)
        {
            if (ls != null && ls.Count > 0)
            {
                string retStr = string.Empty;
                foreach (var item in ls)
                {
                    retStr += item.CnName + ",";
                }
                return retStr.Substring(0, retStr.Length - 1);
            }
            return string.Empty;
        }
        /// <summary>
        /// 拼接计调人员字符串
        /// </summary>
        /// <param name="ls"></param>
        /// <returns></returns>
        public static string PingPlaner(object ls)
        {
            try
            {
                return PingPlaner((IList<MTourPlaner>)ls);
            }
            catch
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// 拼接计调人员字符串
        /// </summary>
        /// <param name="ls">计调实体集合</param>
        /// <returns></returns>
        public static string PingPlaner(IList<MTourPlaner> ls)
        {
            if (ls != null && ls.Count > 0)
            {
                string retStr = string.Empty;
                foreach (var item in ls)
                {
                    retStr += item.Planer + ",";
                }
                return retStr.Substring(0, retStr.Length - 1);
            }
            return string.Empty;
        }
        #endregion
        /// <summary>
        /// 是否导出列表
        /// </summary>
        /// <returns></returns>
        public static bool IsToXls()
        {
            return Utils.GetQueryStringValue("toxls") == "1";
        }

        /// <summary>
        /// 获取导出列表导出的记录数
        /// </summary>
        /// <returns></returns>
        public static int GetToXlsRecordCount()
        {
            return Utils.GetInt(Utils.GetQueryStringValue("toxlsrecordcount"));
        }


        /// <summary>
        /// 根据团号获取计划的计调员
        /// </summary>
        /// <param name="tourID"></param>
        /// <returns></returns>
        public static bool GetTourPlanItemBytourID(string tourID, string UserId)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.TourStructure.MTourBaseInfo tourinfo = new EyouSoft.BLL.TourStructure.BTour().GetBasicTourInfo(tourID);
                if (tourinfo != null)
                {
                    if (tourinfo.TourPlaner != null && tourinfo.TourPlaner.Count > 0)
                    {
                        for (int i = 0; i < tourinfo.TourPlaner.Count; i++)
                        {
                            if (tourinfo.TourPlaner[i].PlanerId == UserId)
                            {
                                ret = true;
                            }
                        }
                    }
                }
            }
            return ret;
        }


        #region 显示或者隐藏修改删除
        /// <summary>
        /// 显示或者隐藏修改删除
        /// </summary>
        /// <param name="status">计调项状态</param>
        /// <returns></returns>
        public static bool GetUpdateAndDeleteByStatus(string tourID, string UserId)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(tourID) && !string.IsNullOrEmpty(UserId))
            {
                EyouSoft.Model.TourStructure.MTourBaseInfo tourinfo = new EyouSoft.BLL.TourStructure.BTour().GetBasicTourInfo(tourID);
                if (tourinfo != null)
                {
                    if (tourinfo.TourPlaner != null && tourinfo.TourPlaner.Count > 0)
                    {
                        for (int i = 0; i < tourinfo.TourPlaner.Count; i++)
                        {
                            if (tourinfo.TourPlaner[i].PlanerId == UserId)
                            {
                                ret = true;
                            }
                        }
                    }

                    if (tourinfo.TourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置)
                    {
                        ret = false;
                    }
                }
            }

            return ret;
        }
        public static bool GetUpdateAndDeleteByStatus(EyouSoft.Model.EnumType.PlanStructure.PlanState status)
        {
            return status != PlanState.已落实;
        }
        #endregion

        /// <summary>
        /// 计调状态
        /// </summary>
        /// <param name="selindex">状态id</param>
        /// <returns></returns>
        public static string GetOperaterStatusList(string selindex)
        {
            return GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanState)).Where(item => (Utils.GetInt(item.Value)) > 2).ToList(), selindex);
        }

        /// <summary>
        /// 计调支付方式
        /// </summary>
        /// <param name="seleIndex">支付方式id</param>
        /// <returns></returns>
        public static string GetOperaterPanyMentList(string seleIndex)
        {
            return GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment)), seleIndex);
        }


        /// <summary>
        /// 供应商中心（线路区域的下拉框绑定）
        /// 王磊
        /// 2012-4-23
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="sourceCompanyId">供应商编号</param>
        /// <returns></returns>
        public static string GetSuppliersArea(string areaId, string sourceCompanyId)
        {
            System.Text.StringBuilder area = new System.Text.StringBuilder();
            area.Append("<option value=''>-请选择线路区域-</option>");

            var info = new EyouSoft.BLL.GysStructure.BGys().GetGysUserInfo(sourceCompanyId);

            if (info != null&&info.Areas!=null&&info.Areas.Count>0)
            {
                foreach (var item in info.Areas)
                {
                    if (item.AreaId.ToString()==areaId)
                    {
                        area.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", item.AreaId, item.AreaName);
                    }
                    else
                    {
                        area.AppendFormat("<option value='{0}' >{1}</option>", item.AreaId, item.AreaName);
                    }
                }
            }
            return area.ToString();
        }

        /// <summary>
        /// 订单状态的绑定(用于供应商、分销商，有别于订单状态的枚举)
        ///  王磊 2012-4-23
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetOrderStatus(string status)
        {
            return GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.GroupOrderStatus)), status, true, "", "-请选择订单状态-");
        }

        /// <summary>
        /// 订单状态(用于供应商、分销商，有别于订单状态的枚举)
        /// 王磊 2012-7-30
        /// desc：分销商供应商的订单状态同系统后台的订单状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetGroupEndOrderStatus(string value)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value=''>-请选择订单状态-</option>");

            Array values = Enum.GetValues(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus));

            //   int[] not = new int[] { (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核, (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功, (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核失败 };

            if (values.Length != 0)
            {
                foreach (var item in values)
                {
                    int Value = (int)Enum.Parse(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus), item.ToString());

                    if (Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核 && Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核成功 && Value != (int)EyouSoft.Model.EnumType.TourStructure.OrderStatus.垫付申请审核失败)
                    {
                        string Text = Enum.GetName(typeof(EyouSoft.Model.EnumType.TourStructure.OrderStatus), item);
                        if (value.Equals(Value.ToString()))
                        {
                            sb.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", Value, Text);
                        }
                        else
                        {
                            sb.AppendFormat("<option value='{0}' >{1}</option>", Value, Text);
                        }
                    }


                }
            }
            return sb.ToString();

        }




        /// <summary>
        /// 根据公司编号和部门编号获取该公司该部门所在的一级部门的编号
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="departId">部门编号</param>
        /// <returns>一级部门的编号</returns>
        public static int GetFirstDepartId(string companyId, int departId)
        {
            if (string.IsNullOrEmpty(companyId) || departId <= 0) return 0;
            var list = Security.Membership.UserProvider.GetDepts(companyId);
            if (list == null || !list.Any()) return 0;
            var model = list.Find(t => (t.DeptId == departId));
            if (model == null) return 0;
            return model.FirstId;
        }

        /// <summary>
        /// 根据团号获取团队状态
        /// </summary>
        /// <param name="tourID">团号</param>
        /// <returns></returns>
        public static bool GetTourStatusByTourID(string companyId, string tourID)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(tourID))
            {
                EyouSoft.Model.EnumType.TourStructure.TourStatus tourStatus = new EyouSoft.BLL.TourStructure.BTour().GetTourStatus(companyId, tourID);

                if (tourStatus != EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置)
                {
                    ret = true;
                }
            }
            return ret;
        }
        /// <summary>
        /// 计调页面使用
        /// </summary>
        /// <param name="tourState"></param>
        /// <returns></returns>
        public static bool GetTourStatusByTourID(EyouSoft.Model.EnumType.TourStructure.TourStatus tourState)
        {
            return tourState != EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置;
        }

        /// <summary>
        /// 获取下拉
        /// </summary>
        /// <param name="l">枚举列表</param>
        /// <param name="v">被选项</param>
        /// <returns></returns>
        public static string GetEnumXiaLaDL(List<EnumObj> l, string v)
        {
            var s = new StringBuilder();

            if (string.IsNullOrEmpty(v))
            {
                s.Append("<dt>请选择</dt>");
            }
            else
            {
                s.AppendFormat("<dt>{0}</dt>", l.Where(e => e.Value == v).Select(e => e.Text).ToList()[0]);
            }

            foreach (var e in l)
            {
                s.AppendFormat("<dd data-value=\"{0}\">{1}</dd>", e.Value, e.Text);
            }
            return s.ToString();
        }
    }


}

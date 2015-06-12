<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="CustomerPlan.aspx.cs" Inherits="Web.SellCenter.CustomerPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--内容-->
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="mainbox">
        <div class="sk-mainbox">
            <ul class="sk-lineboxT fixed">
                <li>线路区域</li>
            </ul>
            <div class="sk-layout fixed">
                <div class="sk-searchbox">
                    <form name="form" action="/SellCenter/CustomerPlan/CustomerPlanList.aspx" method="get">
                    <span class="sk-searchboxT">线路搜索</span>
                    <ul class="sk-searchboxC">
                        <li>
                            <label>
                                团号：</label><input type="text" name="txtTourID" class="inputtext formsize140" id="txtTourID"
                                    class="bk" /></li>
                        <li>
                            <label>
                                线路名称：</label><input type="text" name="txtAreaName" class="inputtext formsize140"
                                    id="txtAreaName" class="bk" /></li>
                        <li>
                            <label>
                                线路区域：</label><select style="width:145px" name="dropAreaID" id="dropAreaID" class="inputselect"><%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(0,SiteUserInfo.CompanyId) %>
                                </select></li>
                        <li>
                            <label>
                                时间：</label><input type="text" name="txtstardate" onfocus="WdatePicker()" class="inputtext formsize80"
                                    id="txtstardate" style="width: 65px;" />-<input type="text" name="txtenddate" id="txtenddate"
                                        onfocus="WdatePicker()" class="inputtext formsize80" style="width: 65px;" /></li>
                        <li>
                            <label>
                                天数：</label><input type="text" name="txtday" class="inputtext formsize40" id="txtday"
                                    class="bk" /></li>
                        <li>
                            <label>
                                销售员：</label><input type="text" name="txtsalesMan" class="inputtext formsize40" id="txtsalesMan"
                                    class="bk" /></li>
                    </ul>
                    <span class="searchbtnbox">
                        <button type="button" class="search-btn" onclick="javascript:form.submit();">
                            搜索</button>
                    </span>
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    </form>
                </div>
                <div class="sk-linelistbox">
                    <div class="sk-linelistbox-in fixed">
                        <a href="/SellCenter/CustomerPlan/CustomerPlanList.aspx?sl=<%=Request.QueryString["sl"] %>&keyID=">
                            全部关键字</a>
                        <asp:Repeater ID="rptList" runat="server">
                            <ItemTemplate>
                                <a title="<%#Eval("Key").ToString() %>" href="/SellCenter/CustomerPlan/CustomerPlanList.aspx?sl=<%#Request.QueryString["sl"] %>&keyID=<%#Eval("KeyId")%>">
                                    <%#EyouSoft.Common.Utils.InputText(Eval("Key").ToString(),5)%>(<%#Eval("TourNum")%>)</a>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Literal ID="licMsg" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <!-- InstanceEndEditable -->
    <!--版权-->
</asp:Content>

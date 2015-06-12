<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideRecord.aspx.cs" Inherits="Web.GuideCenter.GuideRecord"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">
    <style type="text/css">
        .page
        {
            text-align: center;
        }
    </style>
    <div class="mainbox">
        <form id="formSearch" method="get" action="GuideRecord.aspx">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=Utils.GetQueryStringValue("sl") %>" />
                    导游姓名：<input class="inputtext" type="text" size="20" name="txtguidName" value="<%=Request.QueryString["txtguidName"] %>" />
                    性别：<select name="seGender" class="inputselect"><%= this.GetGender(Request.QueryString["seGender"])%></select>
                    类别：
                    <select name="seType" class="inputselect">
                        <%=this.GetGuidType(Request.QueryString["seType"])%>
                    </select>
                    级别：
                    <select name="seLevel" class="inputselect">
                        <%=this.GetGuidLeavel(Request.QueryString["seLevel"])%>
                    </select>
                    语种：
                    <input type="text" class="inputtext formsize80" name="txtguidlanguage" value="<%=Request.QueryString["txtguidlanguage"] %>" />
                    <input type="submit" id="search" class="search-btn" value="" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead">
            <ul class="fixed" id="actionBtn">
                <asp:PlaceHolder ID="add" runat="server">
                    <li><s class="addicon"></s><a href="javascript:" hidefocus="true" class="toolbar_add">
                        <span>新增</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="update" runat="server">
                    <li><s class="updateicon"></s><a href="javascript:" hidefocus="true" class="toolbar_update">
                        <span>修改</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="daochu"></s><a href="javascript:" hidefocus="true" class="toolbar_daochu">
                    <span>导出</span></a> </li>
                <li class="line"></li>
                <asp:PlaceHolder ID="delete" runat="server">
                    <li><a href="javascript:" hidefocus="true" class="toolbar_delete"><s class="delicon">
                    </s><span>删除</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        姓名
                    </th>
                    <th align="center" class="th-line">
                        性别
                    </th>
                    <th align="center" class="th-line">
                        类别
                    </th>
                    <th align="center" class="th-line">
                        级别
                    </th>
                    <th align="center" class="th-line">
                        语种
                    </th>
                    <th align="center" class="th-line">
                        手机
                    </th>
                    <th align="center" class="th-line">
                        带团次数
                    </th>
                    <th align="center" class="th-line">
                        带团天数
                    </th>
                    <th align="center" class="th-line">
                        挂靠单位
                    </th>
                    <th align="center" class="th-line">
                        擅长线路
                    </th>
                    <th align="center" class="th-line">
                        年审状态
                    </th>
                </tr>
                <asp:Repeater ID="replist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="hidden" name="ItemUserID" value="<%# Eval("OperatorId") %>" />
                                <input type="checkbox" name="guideID" id="guideID" value="<%# Eval("GuideId")  %>" />
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%# Eval("Gender").ToString()%>
                            </td>
                            <td align="center">
                                <%# GetGuidType(Eval("SourceGuideCategoryList"))%>
                            </td>
                            <td align="center">
                                <%# Eval("Level").ToString()%>
                            </td>
                            <td align="center">
                                <%# Eval("Language")%>
                            </td>
                            <td align="center">
                                <%# Eval("Mobile")%>
                            </td>
                            <td align="center">
                                <a href="javascript:" data-class="recordNums" data-id="<%# Eval("GuideId") %>">
                                    <%# Eval("Counts")%></a>
                            </td>
                            <td align="center">
                                <%# Eval("Days")%>
                            </td>
                            <td align="left">
                                <%# Eval("AnchoredCom").ToString()%>
                            </td>
                            <td align="left">
                                <a title="<%# Eval("TourExperience")%>">
                                    <%# EyouSoft.Common.Utils.GetText(Eval("TourExperience").ToString(),8)%></a>
                            </td>
                            <td align="center" <%# (bool)Eval("AnnualReview") == true ? "已审" : "class='fontred'"%>>
                                <%# (bool)Eval("AnnualReview") == true ? "已审" : "未审"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="litdatamsg" runat="server"></asp:Literal>
            </table>
        </div>
        <div class="tablehead" style="border: 0 none;">
            <ul class="fixed">

                <script type="text/javascript">
                    document.write(document.getElementById("actionBtn").innerHTML);
                </script>

            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var GuidListPage = {
            _sl: '<%=Utils.GetQueryStringValue("sl")%>',
            OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            AddGuidInfo: function() {
                this.OpenBoxy("导游中心-导游档案-新增", '/GuideCenter/GuideRecordInfo.aspx?sl=' + GuidListPage._sl, "970px", "510px", true);
            },
            UpdateGuidInfo: function(guId) {
                this.OpenBoxy("导游中心-导游档案-修改", '/GuideCenter/GuideRecordInfo.aspx?sl=' + GuidListPage._sl + '&Guid=' + guId + '&action=update', "970px", "510px", true);
            },
            GuidRecordNums: function(guId) {
                this.OpenBoxy("导游中心-导游档案-导游带团", '/GuideCenter/GuidRecordNums.aspx?sl=' + GuidListPage._sl + '&Guid=' + guId + "&companyID=<%=this.SiteUserInfo.CompanyId %>", "620px", "400px", true);
            },
            DeleteGuid: function(arrayIds) {
                var list = new Array();
                for (var i = 0; i < arrayIds.length; i++) {
                    if (arrayIds[i].find("input[type='checkbox']").val() != "on") {
                        list.push(arrayIds[i].find("input[type='checkbox']").val());
                    }
                }

                //删除(批量)
                $.newAjax({
                    type: "get",
                    url: '/GuideCenter/GuideRecord.aspx?sl=' + GuidListPage._sl + '&type=delguid&idlist=' + list.join(','),
                    cache: false,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = '/GuideCenter/GuideRecord.aspx?sl=' + GuidListPage._sl;
                            });
                            return false;
                        }
                        else {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = '/GuideCenter/GuideRecord.aspx?sl=' + GuidListPage._sl;
                            });
                            return false;
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
            },
            BindBtn: function() {
                tableToolbar.init({
                    tableContainerSelector: ".tablelist-box", //表格选择器
                    objectName: "导游信息",
                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(arr) {
                        GuidListPage.UpdateGuidInfo(arr[0].find(":checkbox").val());
                    },
                    deleteCallBack: function(arrayId) {
                        GuidListPage.DeleteGuid(arrayId);
                    }
                });
                //新增导游信息
                $(".toolbar_add").unbind("click");
                $(".toolbar_add").click(function() {
                    GuidListPage.AddGuidInfo();
                    return false;
                });

                //导游带团
                $("#liststyle").find("[data-class='recordNums']").unbind("click");
                $("#liststyle").find("[data-class='recordNums']").click(function() {
                    var guidID = $(this).attr("data-Id");
                    if (guidID) {
                        GuidListPage.GuidRecordNums(guidID);
                    }
                    return false;
                });

            },
            GuidPageInit: function() {
                this.BindBtn();
                toXls.init({ "selector": ".toolbar_daochu" });
            }
        }



        $(function() {
            //初始化
            GuidListPage.GuidPageInit();
        });

        //
        tableToolbar.IsHandleElse = true;
    </script>

</asp:Content>

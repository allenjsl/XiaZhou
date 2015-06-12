<%@ Page Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="WorkReportList.aspx.cs" Inherits="Web.UserCenter.WorkExchange.WorkReportList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="grzxtabelbox">
        <div class="searchbox_01 fixed">
            <form id="form1" method="get">
            <span class="searchT">
                <p>
                    标题：
                    <input name="txtTitle" type="text" class="inputtext formsize120" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTitle")%>" />
                    &nbsp;汇报人：<input name="txtName" type="text" class="inputtext formsize80" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtName")%>" />
                    汇报时间：<input type="text" onfocus="WdatePicker()" name="txtBegin" class="inputtext formsize80"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBegin")%>" />
                    至<input type="text" name="txtEnd" onfocus="WdatePicker()" class="inputtext formsize80"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEnd")%>" />
                    <input type="submit" class="search-btn" value="搜索" /></p>
            </span>
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            </form>
        </div>
        <div style="background: none #f6f6f6;" class="tablehead">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="javascript:void(0);"><span>工作汇报</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="/UserCenter/WorkExchange/WorkPlanList.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_工作交流_栏目 %>">
                    <span>工作计划</span></a></li>
            </ul>
        </div>
        <div class="tablehead" id="btnAction">
            <ul class="fixed">
                <asp:PlaceHolder ID="phdAdd" runat="server">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="javascript:void(0);"
                        ref="/UserCenter/WorkExchange/WorkReportEdit.aspx"><span>新增</span></a></li></asp:PlaceHolder>
                <li class="line"></li>
                <asp:PlaceHolder ID="phdUpdate" runat="server">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0);"
                        ref="/UserCenter/WorkExchange/WorkReportEdit.aspx"><span>修改</span></a></li></asp:PlaceHolder>
                <li class="line"></li>
                <asp:PlaceHolder ID="phdDelete" runat="server">
                    <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="javascript:void(0);"
                        ref="/UserCenter/WorkExchange/WorkReportList.aspx"><span>删除</span></a></li></asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellspacing="0" cellpadding="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" class="th-line">
                            汇报标题
                        </th>
                        <th align="center" class="th-line">
                            汇报部门
                        </th>
                        <th align="center" class="th-line">
                            汇报人
                        </th>
                        <th align="center" class="th-line">
                            审批人
                        </th>
                        <th align="center" class="th-line">
                            汇报时间
                        </th>
                        <th align="center" class="th-line">
                            汇报状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId") %>" />
                                    <input type="checkbox" id="checkbox" name="checkbox" value="<%# Eval("Id") %>" />
                                </td>
                                <td align="center">
                                    <%# Eval("Title")%>
                                </td>
                                <td align="center">
                                    <%# GetDepartmentById(EyouSoft.Common.Utils.GetInt(Eval("DepartmentId").ToString()), Eval("CompanyId").ToString())%>
                                </td>
                                <td align="center">
                                    <%# Eval("OperatorName")%>
                                </td>
                                <td align="center" class="IsApprover">
                                    <asp:Repeater ID="rptCheckList" runat="server">
                                        <ItemTemplate>
                                            <a href="javascript:void(0)" onclick="ApproverClick('<%# Eval("WorkId")%>')" class="<%# Eval("Status").ToString()==EyouSoft.Model.EnumType.IndStructure.Status.已审批.ToString()?"link3":"link2"%>">
                                                <strong class="<%# Eval("Status").ToString()==EyouSoft.Model.EnumType.IndStructure.Status.已审批.ToString()?"":"fontred"%>">
                                                    <%# Eval("Approver")%></strong></a>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.Utils.GetDateTime(Eval("IssueTime").ToString()).ToShortDateString()%>
                                </td>
                                <td align="center">
                                    <%# Eval("Status")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border: 0;" class="tablehead">

            <script type="text/javascript">
                document.write($("#btnAction").html());
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var sl = $("input[type='hidden'][name=sl]").val();
        //页面初始化必须存在方法
        $(function() {

            //绑定功能按钮
            BindBtn();
            //当列表页面出现横向滚动条时使用以下方法
            //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
            $('.tablelist-box').moveScroll();
        })
        function ApproverClick(WorkId) {
            var data = new dataBoxy();
            data.title = "审核";
            data.url = "/UserCenter/WorkExchange/WorkReportApproval.aspx?WorkId=" + WorkId + "&sl=" + sl;
            data.width = "650px";
            data.height = "250px";
            ShowBoxy(data);
        }

        //绑定功能按钮
        function BindBtn() {
            //绑定Add事件
            $(".toolbar_add").click(function() {
                Add();
            })
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                updateCallBack: function(objsArr) {
                    //修改
                    Update(objsArr);
                },
                deleteCallBack: function(objsArr) {
                    //删除(批量)
                    DelAll(objsArr);
                },
                copyCallBack: function(objsArr) {
                    //复制
                    Copy(objsArr)
                }
            })
        }
       
       
    </script>

    <script type="text/javascript">
        //使用弹窗方式添加，修改
        //添加(弹窗)
        function Add() {
            var data = dataBoxy();
            data.title = "新增";
            data.url = "/UserCenter/WorkExchange/WorkReportEdit.aspx?sl=" + sl;
            data.width = "650px";
            data.height = "250px";
            ShowBoxy(data);
        }



        //修改(弹窗)---objsArr选中的TR对象
        function Update(objsArr) {
            var data = dataBoxy();
            data.title = "修改";
            data.url = "/UserCenter/WorkExchange/WorkReportEdit.aspx?doType=update&sl=" + sl + "&id=" + objsArr[0].find("input[type='checkbox']").val();
            data.width = "650px";
            data.height = "250px";
            ShowBoxy(data);
        }

        //复制(弹窗)---objsArr选中的TR对象
        function Copy(objsArr) {
            alert("执行了复制")
        }

        //删除(批量)
        function DelAll(objArr) {



            //ajax执行文件路径,默认为本页面
            var ajaxUrl = "/UserCenter/WorkExchange/WorkReportList.aspx";
            //定义数组对象
            var list = new Array();
            var IsIsApprover = false;
            //遍历按钮返回数组对象
            for (var i = 0; i < objArr.length; i++) {
                //从数组对象中找到数据所在，并保存到数组对象中
                if (objArr[i].find("input[type='checkbox']").val() != "on") {
                    objArr[i].find('.IsApprover a').each(function() {
                        if ($(this).attr("class") == "link3") {
                            IsIsApprover = true;
                            return false;
                        }
                    })
                    list.push(objArr[i].find("input[type='checkbox']").val());
                }
            }
            //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
            ajaxUrl += "?sl=" + sl + "&doType=delete&argument=" + list.join(',');
            //执行ajax
            if (IsIsApprover == true)
                tableToolbar._showMsg("请检查数据，是否含有已审核的数据");
            else
                GoAjax(ajaxUrl, EnglishToChanges.Ping("Del"))
        }

        //弹窗参数
        //弹窗默认参数
        function dataBoxy() {
            return {
                url: "",
                title: "",
                width: "",
                height: ""
            }
        };
        //显示弹窗
        function ShowBoxy(data) {
            Boxy.iframeDialog({
                iframeUrl: data.url,
                title: data.title,
                modal: true,
                width: data.width,
                height: data.height
            });
            return false;
        }
       
    </script>

    <script type="text/javascript">
        //ajax请求
        function GoAjax(url, msg) {
            $.newAjax({
                type: "post",
                cache: false,
                url: url,
                dataType: "json",
                success: function(ret) {
                    tableToolbar._showMsg(ret.msg);
                    if (ret.result == "true") {
                        location.reload();
                    }
                },
                error: function() {
                    if (arguments[1] != null)
                        tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                    else
                        tableToolbar._showMsg("服务器忙");
                }
            });
        }
    </script>

</asp:Content>

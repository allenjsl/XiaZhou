<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="task.aspx.cs" Inherits="Web.GuideCenter.task" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导游安排选择-导游排班-导游中心</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
          <form id="formSearch" method="get">
            <div style="padding-left: 20px; padding-bottom: 5px; height: 20px;">
                <span class="searchT">
                    <p>
                        任务名称：
                        <input type="text" size="20"  id="txtTaskName" name="txtTaskName" value="<%=TaskName %>"/>
                        团号：<input type="text" size="20"  id="txtTourCode" name="txtTourCode" value="<%=TourCode %>"/>                     
                       <input  type="submit" id="btnSubmit" name="btnSubmit" class="search-btn" value="查询"/></p>
                </span>
             </div>
            </form>
            <table id="Guidtasklist" width="99%" border="0" cellspacing="0" cellpadding="0">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        序号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        任务名称
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        出发时间
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        返回时间
                    </td>
                </tr>
                <tr>
                    <td height="28" align="center">
                        <input type="checkbox" value="1"/>
                        1
                    </td>
                    <td align="center">
                        昆明、大理、丽江、香格里拉双飞七日游测试
                    </td>
                    <td align="center">
                        2010202202522
                    </td>
                    <td align="center">
                        2008-9-18 15:00
                    </td>
                    <td align="center">
                        2008-9-22
                    </td>
                </tr>
                 <tr>
                    <td height="28" align="center">
                        <input type="checkbox" value="1"/>
                        1
                    </td>
                    <td align="center">
                        昆明、大理、丽江、香格里拉双飞七日游测试
                    </td>
                    <td align="center">
                        2010202202522
                    </td>
                    <td align="center">
                        2008-9-18 15:00
                    </td>
                    <td align="center">
                        2008-9-22
                    </td>
                </tr>
            </table>
        </div>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
           </div>
        </div>
        <div class="alertbox-btn" style="text-align: center;">
            <a href="javascript:" class="taskOk" hidefocus="true"><s class="baochun"></s>确 定</a>
            <a href="javascript:" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <script type="text/javascript">
        $(function() {
            $("a.taskOk").click(function() {
                var idArry = new Array();
                var TextArray = new Array();
                $("#Guidtasklist").find("input[type='checkbox']:checked").each(function() {
                    idArry.push($.trim($(this).val()));
                    TextArray.push($.trim($(this).parent().next().html()));
                });
                parent.document.getElementById("hidTaskId").value = idArry;
                parent.document.getElementById("txtTaskName").value = TextArray;
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                return false;
            });
        });
    </script>
</body>
</html>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OperaterMenu.ascx.cs"
    Inherits="Web.UserControl.OperaterMenu" %>
<%@ Import Namespace="EyouSoft.Common" %>
<div class="jdcz-menu">
    <ul>
        <li class="jdczmenu-t jdczmenu-first"><s></s>计调操作</li>
        <li><a id="configpage" href="/OperaterCenter/OperaterConfigPage.aspx?&sl=<%=Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>">
            计调配置</a></li>
        <li><a id="globalPage" href="/OperaterCenter/Operaterglobal.aspx?sl=<%=Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>">
            全局计调</a></li>
        <li class="jdczmenu-t jdczmenu-Second"><s></s>导游相关</li>
        <li><a id="pattyCashPage" href="/OperaterCenter/PettyCashList.aspx?sl=<%=Utils.GetQueryStringValue("sl") %>&tourId=<%=TourId %>">
            备用金申请</a></li>
        <li><a id="baozhangPage" href="/OperaterCenter/OperaReimbursement.aspx?sl=21&tourid=<%=TourId %>">
            计调报账 </a></li>
        <li class="jdczmenu-t jdczmenu-Third"><s></s>表单打印</li>
        <li><a href='<%=Print_DaoYouRenWuDan %>' target="_blank">导游任务单</a></li>
        <li><a href='<%=Print_XingChengDan %>' target="_blank">游客行程单</a></li>
        <li><a href='<%=Print_YouKeMingDan %>' target="_blank">游客名单</a></li>
        <li><a href='<%=Print_DingDanXingXiHuiZongBiao %>' target="_blank">
            订单汇总表</a></li>
    </ul>
</div>

<script type="text/javascript">
    $(document).ready(function() {
        switch ('<%=this.IndexClass %>') {
            case "1":
                $("#configpage").addClass("jdapdefault");
                break;
            case "2":
                $("#globalPage").addClass("jdapdefault");
                break;
            case "3":
                $("#pattyCashPage").addClass("jdapdefault");
                break;
            case "4":
                $("#baozhangPage").addClass("baozhangPage");
                break;
            default:
                $("#configpage").addClass("jdapdefault");
                break;
        }
    });
</script>


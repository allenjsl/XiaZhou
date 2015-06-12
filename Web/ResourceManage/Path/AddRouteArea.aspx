<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRouteArea.aspx.cs" Inherits="Web.ResourceManage.Path.AddRouteArea" %>

<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加线路区域</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="12%" height="28" align="right" class="alertboxTableT">
                        <span id="AreaTip" style="color: Red;">*</span> 区域名称：
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtAreaName" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <tr>
                        <td align="right" class="alertboxTableT">
                            责任计调：
                        </td>
                        <td align="left">
                            <uc1:SellsSelect ID="SellsSelect1" runat="server" SMode="true" SetTitle="选择计调员" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="alertboxTableT">
                            区域类别：
                        </td>
                        <td align="left">
                            <select name="selecttype" id="selecttype" class="inputselect">
                                <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.AreaType)), "1", false)%>
                            </select>
                        </td>
                    </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSave"><s class="baochun"></s>保
                存</a><a hidefocus="true" href="javascript:void(0)" onclick="AreaPage.close()"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
     $(function() {
       AreaPage.PageInit();
    })
    var AreaPage={
        PageInit:function(){
            AreaPage.BindBtn();
        },
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        GoAjax:function(url){
         $("#btnSave").html("提交中...");
         $("#btnSave").unbind("click");
         $.newAjax({
                type: "post",
                cache: false,
                url: url,
                dataType: "json",
                data:$("#btnSave").closest("form").serialize(),
                success: function(ret) {
                    if (ret.result== "1") {
                        parent.tableToolbar._showMsg(ret.msg,function(){AreaPage.BackFun(ret.obj)});
                    }
                    else {
                        parent.tableToolbar._showMsg(ret.msg,function(){AreaPage.BindBtn()});
                    }
                },
                error: function() {
                   tableToolbar._showMsg(tableToolbar.errorMsg,function(){AreaPage.BindBtn()});
                }
            });
        },
        BackFun:function(obj){
             var data = {callBack: Boxy.queryString("callBack"), iframeID: Boxy.queryString("iframeId")}
                if (data.callBack != null && data.callBack.length > 0) {
                    args={id:obj.id,name:obj.name,type:obj.type}
                    window.parent[data.callBack](args);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        },
        BindBtn:function(){
            $("#btnSave").click(function(){
                if($("#<%=txtAreaName.ClientID %>").val()==""){
                    parent.tableToolbar._showMsg("请输入区域名称");
                    return false;
                }
                else{
                    $("#AreaTip").hide();
                }
                AreaPage.GoAjax("/ResourceManage/Path/AddRouteArea.aspx?doType=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路资源 %>");
            return false;
            })
            $("#btnSave").attr("class","");
            $("#btnSave").html("<s class='baochun'></s>保存");
        }
    }
</script>


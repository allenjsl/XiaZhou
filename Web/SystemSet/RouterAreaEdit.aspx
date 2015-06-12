<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="RouterAreaEdit.aspx.cs"
    Inherits="Web.SystemSet.RouterAreaEdit" %>

<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ޱ����ĵ�</title>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="9%" height="28" align="right" class="alertboxTableT">
                        <font class="fontbsize12">* </font>�������ƣ�
                    </td>
                    <td width="35%" height="28" align="left">
                        <asp:TextBox ID="txtAreaName" class="formsize120" Width="150px" runat="server" CssClass=" inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        ���μƵ���
                    </td>
                    <td align="left">
                        <uc1:SellsSelect ID="SellsSelect1" runat="server" SMode="true" SetTitle="�Ƶ�Ա" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <td align="right" class="alertboxTableT">
                        �������
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlAreaType" runat="server" CssClass=" inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="text-align: center;" class="alertbox-btn">
            <a hidefocus="true" href="javascript:" id="btnSave" onclick="return RouterAreaEdit.Save();">
                <s class="baochun"></s>�� ��</a> <a hidefocus="true" href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
                    <s class="chongzhi"></s>�� ��</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var RouterAreaEdit = {
        
            Params:{sl:"<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",memuid:"<%=EyouSoft.Common.Utils.GetQueryStringValue("memuid") %>",id:"<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>"},
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>�ύ��...");
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0-57px");
            },
            //��ť���¼�
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").css("background-position", "0-28px");
                $("#btnSave").html("<s class='baochun'></s>�� ��");
                $("#btnSave").click(function() {

                    RouterAreaEdit.Save();
                    return false;
                });
            },
            CheckDate:function(){
                var msg="";
                if($("#<%=txtAreaName.ClientID%>").val()==""){
                    msg+="�������Ʋ���Ϊ��!<br/>";
                }
                if($("#<%=txtAreaName.ClientID%>").val()==""){
                    msg+="���������Ϊ��!";
                }
                if(msg!=""){
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                return true;
            },
            Save: function() {
                RouterAreaEdit.CheckDate();
                RouterAreaEdit.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/SystemSet/RouterAreaEdit.aspx?dotype=save&"+$.param(RouterAreaEdit.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax�ط���ʾ
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg,function(){parent.window.location.href="/SystemSet/RouterAreaList.aspx?&"+$.param(RouterAreaEdit.Params);});
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                        RouterAreaEdit.BindBtn();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("����ʧ�ܣ����Ժ�����!");
                        RouterAreaEdit.BindBtn();
                    }
                });
            }
        }
    </script>

</body>
</html>

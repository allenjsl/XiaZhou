<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectXiangmuxybox.aspx.cs"
    Inherits="Web.TeamCenter.selectXiangmuxybox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>项目选用</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/table-toolbar.js"></script>

    <style type="text/css">
        .bumenbox
        {
            line-height: 200%;
        }
        .bumenbox a
        {
            color: #000000;
        }
        .bumenbox a:hover
        {
            color: #FF0000;
        }
        .bottomline
        {
            border-bottom: 1px #b7e0f3 solid;
            border-collapse: collapse;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table id="tableData" width="99%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;">
            <tbody>
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td width="12%" height="23" bgcolor="#B7E0F3" align="left" class="alertboxTableT">
                        序号
                    </td>
                    <td width="88%" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                        内容
                    </td>
                </tr>
                <asp:Repeater ID="rpt_List" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td height="28" align="left" bgcolor="#E9F4F9" class="bottomline">
                                <input name="cbxProID" data-index="<%#Container.ItemIndex+1 %>" type="checkbox" value="<%#Eval("id")%>" />
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td align="left" bgcolor="#E9F4F9" class="bottomline">
                                <span data-class="returnValue">
                                    <%#Eval("Content")%></span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <form>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSelect"><s class="xuanzhe"></s>
                选 择</a><a hidefocus="true" href="javascript:void(0);" id="btnSave"><s class="baochun"></s>保存</a>
        </div>
        <br />
        <table width="100%" cellspacing="5" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td>
                        <textarea id="txtNewInfo" style="width:98%; height:60px;" class="inputtext formsize800" rows=""
                            cols="" name="txtNewInfo">&nbsp;</textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
    </div>

    <script type="text/javascript">
        $(function() {
            $("#btnSelect").click(function() {
                var obj = parent.$("#" + SelectInformation.aid);
                if (obj.length > 0) {

                    var str = "";
                    $("#tableData").find("input[name='cbxProID']:checked").each(function(i) {
                        str += (i + 1) + "." + $.trim($(this).parent().next().find("span[data-class='returnValue']").html()) + " \n";
                    })
                    if (str.length > 0) {
                        var txt = obj.closest("tr").find("textarea");
                        if (txt.length > 0) {
                            obj.closest("tr").find("textarea").val(str);
                        } else {
                            parent.$("#divGroup").find("textarea").val(str);
                        }
                    }
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            })

            $("#btnSave").click(function() {
                SelectInformation.Save();
                return false;
            })
            var SelectInformation = {
                type: '<%=Request.QueryString["type"] %>',
                proType: '<%=Request.QueryString["proType"] %>',
                aid: '<%=Request.QueryString["id"] %>',
                sl: '<%=Request.QueryString["sl"] %>',
                Save: function() {
                    if ($.trim($("#txtNewInfo").val()) == "") {
                        parent.tableToolbar._showMsg("保存内容不能为空!");
                        return false;
                    }
                    $("#btnSave").unbind("click");
                    $("#btnSave").html('<s class="baochun"></s>提交中..');
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "/TeamCenter/selectXiangmuxybox.aspx?sl=" + SelectInformation.sl + "&type=" + SelectInformation.type + "&doType=save&proType=" + SelectInformation.proType,
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                parent.tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = window.location.href;
                                });

                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                            SelectInformation.BindBtn();
                        }
                    });

                },
                BindBtn: function() {
                    $("#btnSave").click(function() {
                        SelectInformation.Save();
                        return false;
                    })
                    $("#btnSave").html('<s class="baochun"></s>保存');
                }
            }
        });
    </script>

</body>
</html>

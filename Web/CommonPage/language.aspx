<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="language.aspx.cs" Inherits="Web.CommonPage.language" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="99%" border="1" cellpadding="0" cellspacing="0" bordercolor="#85C1DD"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【选择语言】</span>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="tablelist" width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#B8C5CE"
                        bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                        <tr>
                            <td align="left">
                                <input type="checkbox" value="1690" name="checkLangu"/>
                                <span>英语</span>
                            </td>
                            <td align="left">
                                <input type="checkbox" value="3500" name="checkLangu"/>
                                <span>日语</span>
                            </td>
                            <td align="left">
                                <input type="checkbox" value="3501" name="checkLangu"/>
                                <span>韩语</span>
                            </td>
                            <td align="left">
                                <input type="checkbox" value="3505" name="checkLangu"/>
                                <span>汉语</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 20px;">
        </div>
        <div class="alertbox-btn" style="text-align: center;">
            <a href="javascript:" hidefocus="true" class="choose"><s class="xuanzhe"></s>选 择</a></div>
    </div>
    </form>
    <script type="text/javascript">
        $(function() {
            $("a.choose").click(function() {
                var idArray = new Array();
                var textArray = new Array();
                $(".tablelist").find("input[type='checkbox']:checked").each(function() {
                    idArray.push($(this).val());
                    textArray.push($(this).siblings("span").html());
                });

                parent.document.getElementById("hidlanguageid").value = idArray;
                parent.document.getElementById("txtlanguage").value = textArray;
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                return false;
            });
        });
    </script>
</body>
</html>

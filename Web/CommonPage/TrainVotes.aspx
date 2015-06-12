<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainVotes.aspx.cs" Inherits="Web.CommonPage.TrainVotes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>
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
    </style>
</head>
<body style="background: 0 none;">
   <form id="formSearch" method="get">
    <div class="alertbox-outbox02">
        <table width="99%" border="1" cellpadding="0" cellspacing="0" bordercolor="#85C1DD"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td height="30">
                    出票点：
                    <input name="Txtvotes" type="text" class="formsize80 bk" id="Txtvotes"  value="<%=votes %>"/>
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                </td>
            </tr>
        </form>
            <tr>
                <td>
                    <table name="noborderInput" width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#B8C5CE"
                        bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                        <tr>
                            <td align="left">
                                <input type="checkbox" name="check" value="1" />
                                <span>新时代票务中心1</span>
                            </td>
                            <td align="left">
                                <input type="checkbox" name="radiobutton" value="2" />
                                <span>新时代票务中心2</span>
                            </td>
                            <td align="left">
                                <input type="checkbox" name="radiobutton" value="3" />
                                <span>新时代票务中心3</span>
                            </td>
                            <td align="left">
                                <input type="checkbox" name="radiobutton" value="4" />
                                <span>新时代票务中心4</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:" hidefocus="true" class="choose"><s class="xuanzhe"></s>选 择</a></div>
    </div>
    <script type="text/javascript">
        $(function() { 
         $("a.choose").click(function() {
                var idArray = new Array();
                var textArray = new Array();
                $("[name='noborderInput']").find("input[type='checkbox']:checked").each(function() {
                    idArray.push($.trim($(this).val()));
                    textArray.push($.trim($(this).siblings("span").html()));
                });                
                parent.document.getElementById("<%=Request.QueryString["hiddPID"] %>").value = idArray;
                parent.document.getElementById("<%=Request.QueryString["txtdrawerPoint"] %>").value = textArray;
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                return false;
            });
        });
    </script>
</body>
</html>

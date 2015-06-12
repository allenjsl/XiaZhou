<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="Web.ResourceCenter.Tavern.Control" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>总控</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div>
        <form id="form1" runat="server">
        <div class="alertbox-outbox02">
            <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="27%" align="left">
                            <input type="radio" name="radio" id="radio_share" value="共享" onclick="ShowOrHide(this)" />
                            共享
                            <input type="radio" name="radio" id="radio_NoShare" value="不共享" onclick="ShowOrHide(this)" />
                            不共享 <span id="sp_SellsSelect1" style="display: none;">
                                <asp:TextBox ID="txt_houseMan" runat="server" class="formsize80" Enabled="false"></asp:TextBox>
                                <input type="hidden" id="hd_houseManId" /><a id="a_showMan" class="xuanyong" href="javascript:void(0);"
                                    onclick=""></a> </span>
                        </td>
                    </tr>
                </table>
                <div class="alertbox-btn" style="text-align: center;">
                    <a href="javascript:;" hidefocus="true" onclick="Save()"><s class="xuanzhe"></s>选 择</a><a
                        href="#" hidefocus="true"><s class="chongzhi"></s>重 置</a>
                </div>
            </div>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        $(function() {
            var selectT = new selectToobar();
            selectT.init({
                clickObj: $("#a_showMan"),
                hideObj: $("#hd_houseManId"),
                showObj: $("#txt_houseMan"),
                iframeUrl: "/CommonPage/OrderSells.aspx",
                title: "总控-不共享-用房人选择-",
                width: "650px",
                height: "500px",
                pModel: "true",
                otherPara: 'pIframeId=<%=Request.QueryString["iframeId"] %>'
            });
        })
        function ShowOrHide(thiss) {
            if ($(thiss).attr("id") == "radio_share") {
                $("#sp_SellsSelect1").css("display", "none")
            }
            else {
                $("#sp_SellsSelect1").css("display", "inline")
            }
        }
        function Save() {
            var data = {
                pIframeID: Boxy.queryString("pIframeID"),
                iframeID: Boxy.queryString("iframeID"),
                hideID: Boxy.queryString("hideID"),
                showID: Boxy.queryString("showID")
            }

            var returnVal = GetVal();
            if (data.pIframeID != null && data.pIframeID.length > 0) {
                //定义父级弹窗
                var boxyParent = window.parent.Boxy.getIframeDocument(data.pIframeID);
                $(boxyParent.getElementById(data.hideID)).val(returnVal);
                $(boxyParent.getElementById(data.showID)).val(returnVal);
            }
            parent.Boxy.getIframeDialog(data.iframeID).hide();
            return false;

        }
        function GetVal() {
            var ret;
            $("[name='radio']:checked").each(function() {
                ret = $(this).val();
                if ($(this).next("span").find("#txt_houseMan").val() != null) {
                    ret += "--" + $(this).next("span").find("#txt_houseMan").val();
                }
            })
            return ret;
        }
    </script>

</body>
</html>

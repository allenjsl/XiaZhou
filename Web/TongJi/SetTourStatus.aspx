<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetTourStatus.aspx.cs"
    Inherits="EyouSoft.Web.TongJi.SetTourStatus" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <form id="form1">
    <div class="alertbox-outbox">
        <div style="width: 99%; margin: 0px auto;">
            <input type="hidden" id="txtTourType" runat="server" />
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td class="addtableT" style="width: 70px;">
                        当前团号：
                    </td>
                    <td class="kuang2">
                        <asp:Literal runat="server" ID="ltrTourCode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT" style="width: 70px;">
                        线路名称：
                    </td>
                    <td class="kuang2">
                        <asp:Literal runat="server" ID="ltrRouteName"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT" style="width: 70px;">
                        当前状态：
                    </td>
                    <td class="kuang2">
                        <asp:Literal runat="server" ID="ltrYuanStatus"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT" style="width: 70px;">
                        计划状态：
                    </td>
                    <td class="kuang2">
                        <select id="txtStatus" name="txtStatus" valid="required" errmsg="请选择状态" class="inputselect">
                        <option value="">请选择</option>
                        <asp:Literal runat="server" ID="ltrStatusOption"></asp:Literal>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT" style="width: 70px;">
                        变更原因：
                    </td>
                    <td class="kuang2">
                        <textarea rows="5" cols="60" name="txtYuanYin" id="txtYuanYin" valid="required" errmsg="请输入变更原因"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <div class="alertbox-btn">
            <asp:Literal runat="server" ID="ltrOperatorHtml"></asp:Literal>&nbsp;&nbsp;
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var iPage = {
            close: function() {
                top.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            submit: function(obj) {
                var validatorResult = ValiDatorForm.validator($("form").get(0), "parent");
                if (!validatorResult) return false;

                if (!confirm("状态变更操作不可逆，你确定要变更该计划状态吗？")) return false;

                $(obj).unbind("click").text("处理中....");

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit",
                    data: $("form").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            parent.location.href = parent.location.href;
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.submit(obj); }).text("保存");
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.submit(obj); }).text("保存");
                    }
                });
            }
        };

        $(document).ready(function() {            
            $("#i_a_submit").click(function() { iPage.submit(this); });
        });
    </script>

</asp:Content>

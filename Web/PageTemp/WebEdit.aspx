<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebEdit.aspx.cs" Inherits="Web.PageT.WebEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="" runat="server" Text="保存" OnClientClick="return ThisPage.FormCheck()" />
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            //FV_onBlur.initValid($("#tblVisitorList").closest("form").get(0));
            ThisPage.BindBtn();
        })
        var ThisPage = {
            Form: null,
            //数据验证
            FormCheck: function() {
                this.Form = $("#<%=btnSave.ClientID %>").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "alert");
            },
            Save: function() {
                if (ThisPage.FormCheck()) {
                    $("#<%=btnSave.ClientID %>").unbind("click")
                    $("#<%=btnSave.ClientID %>").attr("class", "cun-cyactive")//cun-cyactive  2中class
                    $("#<%=btnSave.ClientID %>").text("提交中...");
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: "",
                        data: this.Form,
                        dataType: "json", /*这个参数可随意,尽量使用json*/
                        success: function(ret) {
                            //ajax回发提示
                            ThisPage.BindBtn();
                        },
                        error: function() {
                            //ajax异常--你懂得
                            alert("服务器忙！");
                            ThisPage.BindBtn();
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#<%=btnSave.ClientID %>").click(function() {
                    ThisPage.Save();
                    return false;
                })
                $("#<%=btnSave.ClientID %>").attr("class", "");
                $("#<%=btnSave.ClientID %>").text("提交");
            }

        }
    </script>

</body>
</html>

<%@ Page Language="C#" MasterPageFile="~/MasterPage/Print.Master" AutoEventWireup="true"
    CodeBehind="zutuan.aspx.cs" ValidateRequest="false" Inherits="EyouSoft.Web.PrintPage.xz.zutuan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrintC1" runat="server">
    <div id="i_div_to">
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="borderbot_2 inputbot">
        <tr>
            <td align="left">
                <span class="font14">团队编号：<asp:Label ID="lbTourCode" runat="server" Text=""></asp:Label>
                </span>
            </td>
            <td colspan="2">
                <span class="font14">人数：<asp:Label ID="lbPeoNum" runat="server"></asp:Label></span>
            </td>
        </tr>
        <tr>
            <td width="50%" align="left">
                <span class="font14">敬呈：<asp:TextBox ID="txtunitname" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                    ID="txtunitContactname" runat="server" CssClass="w70"></asp:TextBox></span>
            </td>
            <td width="25%" align="left">
                <span class="font14">电话：<asp:TextBox ID="txtunittel" runat="server" CssClass="input120"></asp:TextBox></span>
            </td>
            <td width="25%" align="right">
                <span class="font14">传真：<asp:TextBox ID="txtunitfax" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
        </tr>
        <tr>
            <td width="50%" align="left">
                <span class="font14">发自：<asp:TextBox ID="txtsourcename" runat="server" CssClass="w220"></asp:TextBox>/<asp:TextBox
                    ID="txtname" runat="server" CssClass="w70"></asp:TextBox></span>
            </td>
            <td width="25%" align="left">
                <span class="font14">电话：<asp:TextBox ID="txttel" runat="server" CssClass="input120"></asp:TextBox></span>
            </td>
            <td width="25%" align="right">
                <span class="font14">传真：<asp:TextBox ID="txtfax" runat="server" CssClass="input100"></asp:TextBox></span>
            </td>
        </tr>
    </table>
    </div>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="40" align="center">
                <b class="font24">
                    <asp:Label ID="lbRouteName" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
    </table>
    <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
        id="TPlanFeature" class="borderline_2">
        <tr>
            <td height="30" class="small_title">
                <b class="font16">
                    <input type="checkbox" name="checkbox" id="checkbox" data-check="check" />
                    行程特色</b>
            </td>
        </tr>
        <tr>
            <td class="td_text">
                <asp:Literal ID="lbPlanFeature" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <asp:Literal ID="lbtourplan" runat="server"></asp:Literal>
    <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" id="TAll"
        runat="server" class="borderline_2">
        <tbody>
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" id="checkbox1" name="checkbox" checked="checked" data-check="check" />
                        服务标准</b> <b class="fontred16">整团</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lballservice" runat="server"></asp:Literal>
                </td>
            </tr>
        </tbody>
    </table>
    <div id="TOption" runat="server">
        <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
            <tbody>
                <tr>
                    <td height="30" class="small_title">
                        <b class="font16">
                            <input type="checkbox" id="checkbox9" name="checkbox_t" data-check="check" />
                            服务标准</b> <b class="fontred16">分项</b>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
            <tbody>
                <asp:Literal ID="lboptionservice" runat="server"></asp:Literal>
            </tbody>
        </table>
    </div>
    <div id="TTourPrice" runat="server">
        <table width="696" cellspacing="0" cellpadding="0" border="0" align="center">
            <tbody>
                <tr>
                    <td height="30" class="small_title">
                        <b class="font16">
                            <input type="checkbox" id="checkbox1" name="checkbox_t" data-check="check" />
                            团队报价</b>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="696" cellspacing="0" cellpadding="0" border="0" align="center" class="list_2">
            <tr>
                <td>
                    价格组成
                </td>
                <td>
                    成人价：<span class="fontred16"><asp:Literal ID="lbAdultPrice" runat="server"></asp:Literal></span>&nbsp;&nbsp;&nbsp;成人数：<span
                        class="fontred16"><asp:Label ID="lbAdultsNum" runat="server"></asp:Label></span>
                    <br />
                    儿童价：<span class="fontred16"><asp:Literal ID="lbChildPrice" runat="server"></asp:Literal></span>&nbsp;&nbsp;&nbsp;儿童数：<span
                        class="fontred16"><asp:Label ID="lbChildsNum" runat="server"></asp:Label></span>
                    <br />
                    其他费用：<span class="fontred16"><asp:Literal ID="lbotherprice" runat="server"></asp:Literal></span>
                    &nbsp;&nbsp;合计：<span class="fontred16"><asp:Literal ID="lbtotleprice" runat="server"></asp:Literal></span>
                </td>
            </tr>
            <tr>
                <td>
                    价格备注
                </td>
                <td class="hanggao">
                    <asp:Literal ID="lbremark" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="TPlanService">
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
            id="TService" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox2" data-check="check" />
                        服务标准</b>
                </td>
            </tr>
            <tr>
                <td class="td_text borderline_2">
                    <asp:Literal ID="lbService" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" runat="server"
            id="TNoService" class="borderline_2">
            <tr>
                <td class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox3" data-check="check" />
                        服务不含</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbnoService" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TShopping"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox4" data-check="check" />
                        购物安排</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbshopping" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TChildren"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox5" data-check="check" />
                        儿童安排</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbchildren" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TSelfProject"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox6" data-check="check" />
                        自费项目</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbselfproject" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TNeedAttention"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox7" data-check="check" />
                        注意事项</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbneedattention" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" id="TWarmRemind"
            runat="server" class="borderline_2">
            <tr>
                <td height="30" class="small_title">
                    <b class="font16">
                        <input type="checkbox" name="checkbox" id="checkbox8" data-check="check" />
                        温馨提示</b>
                </td>
            </tr>
            <tr>
                <td class="td_text">
                    <asp:Literal ID="lbwarmremind" runat="server"></asp:Literal>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function PrevFun() {
            $("input[type='checkbox'][name='checkbox']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='checkbox' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("tr").hide();
                }
                self.remove();
            })
            $("input[type='checkbox'][name='checkbox_t']").each(function() {
                var self = $(this);
                var id = self.attr("id");
                var checked = self.attr("checked");
                self.after("<label data-id='" + id + "' data-type='checkbox_t' data-checked='" + checked + "'></label>");
                if (!self.attr("checked")) {
                    self.closest("table").hide();
                }
                self.remove();
            })
            setTimeout(function() {
                $("label[data-type='checkbox']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' />")
                    }
                    else {
                        $(this).after("<input type='checkbox' name='checkbox' data-class='checkbox' id='" + id + "' checked='checked' />")
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][name='checkbox']").click(function() { showorhide(this, "1") });
                $("input[type='checkbox'][name='checkbox']").closest("tr").show();
                $("label[data-type='checkbox_t']").each(function() {
                    var id = $(this).attr("data-id");
                    var checded = $(this).attr("data-checked");
                    if (checded == 'false') {
                        $(this).after("<input type='checkbox' name='checkbox_t' data-class='checkbox' id='" + id + "' />");
                    }
                    else {
                        $(this).after("<input type='checkbox' name='checkbox_t' data-class='checkbox' id='" + id + "' checked='checked' />");
                    }
                    $(this).remove();
                })
                $("input[type='checkbox'][name='checkbox_t']").click(function() { showorhide(this, "2") });
                $("input[type='checkbox'][name='checkbox_t']").closest("table").show();
            }, 1000);
        }

        function showorhide(obj, name) {
            var self = $(obj);
            var id = self.attr("id");
            if (!self.attr("checked")) {
                if (name == "1") {
                    self.closest("tr").next("tr").hide();
                }
                else {
                    self.closest("table").next("table").hide();
                }
            }
            else {
                if (name == "1") {
                    self.closest("tr").next("tr").show();
                }
                else {
                    self.closest("table").next("table").show();
                }
            }
        }
        $(function() {
            $("input[type='checkbox'][data-check='check']").attr("checked", "checked")
            $("input[type='checkbox'][name='checkbox']").click(function() { showorhide(this, "1") });
            $("input[type='checkbox'][name='checkbox_t']").click(function() { showorhide(this, "2") });
        })
    </script>

</asp:Content>

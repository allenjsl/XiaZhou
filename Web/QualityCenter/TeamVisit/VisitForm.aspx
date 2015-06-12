<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisitForm.aspx.cs" Inherits="Web.QualityCenter.TeamVisit.VisitForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <form id="form1" method="post" runat="server">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>回访类型：
                </td>
                <td width="20%" align="left">
                    <asp:Label runat="server" ID="lbVisitType" data-class="chakan"></asp:Label>
                    <asp:DropDownList ID="visitType" runat="server" class="inputselect">
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="hidKeyId" />
                    <asp:HiddenField runat="server" ID="hidTourId" />
                </td>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>被访人身份：
                </td>
                <td width="20%" height="28">
                    <asp:Label runat="server" ID="lbBeVisitType" data-class="chakan"></asp:Label>
                    <asp:DropDownList ID="beVisitType" runat="server" class="inputselect">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>被访人：
                </td>
                <td width="20%" align="left">
                    <asp:Label runat="server" ID="lbVisitor" data-class="chakan"></asp:Label>
                    <asp:TextBox runat="server" ID="txtVisitor" class="inputtext formsize120" valid="required"
                        errmsg="被访人不能为空！"></asp:TextBox>
                </td>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    被访人电话：
                </td>
                <td width="20%" align="left">
                    <asp:Label runat="server" ID="lbVisitorTel" data-class="chakan"></asp:Label>
                    <asp:TextBox runat="server" ID="txtVisitorTel" class="inputtext formsize120"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    计调安排项：
                </td>
                <td height="28" colspan="3" align="left">
                    <table width="95%" border="0" align="left" style="margin-top: 5px; margin-bottom: 5px;">
                        <tr>
                            <td width="20%" height="28" align="center" bgcolor="#B7E0F3">
                                项目
                            </td>
                            <td width="30%" align="center" bgcolor="#B7E0F3">
                                供应商
                            </td>
                            <td width="10%" align="center" bgcolor="#B7E0F3">
                                得分
                            </td>
                            <td width="40%" align="center" bgcolor="#B7E0F3">
                                总体评价
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="rpt_VisitDetail">
                            <ItemTemplate>
                                <tr>
                                    <td height="28" align="center">
                                        <%#Eval("PlanType")%>
                                        <input type="hidden" name="planType" value='<%#(int)(EyouSoft.Model.EnumType.PlanStructure.PlanProject)Eval("PlanType")%>' />
                                    </td>
                                    <td align="center">
                                        <%#Eval("SourceName") %>
                                        <input type="hidden" name="sourceName" value='<%#Eval("SourceName")%>'' />
                                        <input type="hidden" name="sourceId" value='<%#Eval("SourceId")%>'' />
                                        <input type="hidden" name="planId" value='<%#Eval("PlanId")%>'' />
                                    </td>
                                    <td align="center">
                                        <input name="txtScore" type="text" class="inputtext formsize50" valid="isNumber"
                                            errmsg="请输入数字！" />
                                        <span data-class="chakan">
                                            <%#Eval("Score").ToString() == "0" ? "" : Eval("Score")%></span>
                                    </td>
                                    <td align="center">
                                        <input name="txtTotalDesc" type="text" class="inputtext formsize300" />
                                        <span data-class="chakan">
                                            <%#Eval("TotalDesc") %></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    团队均分：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" ID="lbQualityScore" data-class="chakan"></asp:Label>
                    <asp:TextBox runat="server" ID="txtQualityScore" valid="isNumber" errmsg="请输入数字！"
                        class="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    总评：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:Label runat="server" ID="lbTotal" data-class="chakan"></asp:Label>
                    <asp:TextBox runat="server" ID="txtTotal" TextMode="MultiLine" Height="45" class="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a> </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
        </form>
    </div>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            DelFile: function(n) {
                tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
                    $(".labelFiles .divFile_" + n).hide();
                    $(".labelFiles #hideFileInfo_" + n).val("");
                });
            },
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/QualityCenter/TeamVisit/VisitForm.aspx?";
                    url += $.param({
                        doType: this.Query.doType,
                        save: "save",
                        sl: this.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(that.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });

                            }
                            else { parent.tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); }); }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                PageJsData.BindBtn();
                            });

                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            },
            AutoCount:function(){
                $("input[name='txtScore']").change(function(){
                    var TotleScore=0;
                    $("input[name='txtScore']").each(function(){
                        TotleScore+=parseFloat($(this).val()==""?0:$(this).val());
                    })
                    $("#txtQualityScore").val(parseFloat(TotleScore/<%=VisitListCount %>).toFixed(2));
                    TotleScore=null;
                })
                $("#txtQualityScore").change(function(){
                    $("input[name='txtScore']").each(function(){
                        if($(this).val()==""){
                            $(this).val($("#txtQualityScore").val());
                        }
                    })
                })
            }
        }

        $(function() {
            PageJsData.BindBtn();
            PageJsData.AutoCount();
            if (PageJsData.Query.doType == "add") {
                $("span[data-class='chakan']").hide();
            }
            if (PageJsData.Query.doType == "chakan") {
                $(".inputtext").hide();
                $("select").hide();
                $(".alertbox-btn").hide();
            }
        });
       
    </script>

</body>
</html>

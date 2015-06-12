<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="meetAdd.aspx.cs" Inherits="Web.ManageCenter.Meet.meetAdd" %>

<%@ Register Src="../../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>
    <style type="text/css">  	
        #HrSelect1_txtSelectName{ width:550px;}
    </style>

</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>会议编号：
                </td>
                <td width="45%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtNum" valid="required|custom" class="inputtext formsize120"
                        custom="PageJsData.CheckNum" errmsg="会议编号不能为空！|对不起，该会议编号已存在！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidId" />
                </td>
                <td width="15%" align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>会议类别：
                </td>
                <td width="25%" align="left" bgcolor="#e0e9ef">
                    <asp:DropDownList ID="selType" runat="server" class="inputselect" valid="custom"
                        custom="PageJsData.CheckSelect" errmsg="会议类别必须选择！">
                        <asp:ListItem Value="-1">-请选择-</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>会议主题：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtTitle" valid="required" class="inputtext formsize600"
                        errmsg="会议主题不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>参会人员：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef" class="UserInfo">
                    <uc2:HrSelect runat="server" ID="HrSelect1" />
                </td>
            </tr>
            <tr>
                <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>会议时间：
                </td>
                <td width="45%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtStartTime" class="inputtext formsize100" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtEndTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})"
                        valid="required" errmsg="会议开始时间不能为空！"></asp:TextBox>
                    <a href="javascript:document.getElementById('txtStartTime').focus();" class="timesicon">
                        开始时间</a> 至
                    <asp:TextBox runat="server" ID="txtEndTime" class="inputtext formsize100" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})"
                        valid="required" errmsg="会议结束时间不能为空！"></asp:TextBox>
                    <a href="javascript:document.getElementById('txtEndTime').focus();" class="timesicon">
                        结束时间</a>
                </td>
                <td width="13%" align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>会议地点：
                </td>
                <td width="33%" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtPlace" valid="required" errmsg="会议地点不能为空！" class="inputtext formsize140"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    会议纪要：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" class="inputtext formsize600" TextMode="MultiLine" Width="600"
                        Height="93" ID="txtContent"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.CloseForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            CloseForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            CheckNum: function(obj) {
                var s = "";
                var url = 'meetAdd.aspx?';
                url += $.param({
                    sl: this.Query.sl,
                    doType: "checkNum",
                    id: '<%=Request.QueryString["id"]%>',
                    num: $(obj).val()
                })
                $.newAjax({
                    type: "get",
                    url: url,
                    success: function(data) {
                        s = data;
                    },
                    async: false
                })
                return s != "1" ? true : false;
            },
            CheckSelect: function(obj) {
                if ($(obj).val() == "-1") return false;
                else return true;
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
                    var url = "/ManageCenter/Meet/meetAdd.aspx?";
                    url += $.param({
                        doType: '<%=Request.QueryString["doType"] %>',
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
                            if (result.result == 1) {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });

                            }
                            else {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    PageJsData.BindBtn();
                                });
                            }
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
            }
        }
        $(function() {
            PageJsData.BindBtn();

        })
    </script>

</body>
</html>

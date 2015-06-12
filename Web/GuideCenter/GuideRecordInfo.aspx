<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuideRecordInfo.aspx.cs"
    Inherits="Web.GuideCenter.GuideRecordInfo" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="SingleFileUpload" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="100" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>用户名：
                </td>
                <td align="left">
                    <asp:HiddenField ID="hidUserId" runat="server" />
                    <asp:TextBox ID="txtUserNames" runat="server" CssClass="formsize120 inputtext" valid="required"
                        errmsg="请输入用户名!"></asp:TextBox>
                </td>
                <td width="120" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>密码：
                </td>
                <td>
                    <input type="password" class="formsize120 inputtext" id="txtuserpwd" runat="server" />
                    <asp:HiddenField ID="hidUserPwd" runat="server" />
                </td>
                <td width="100" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>确认密码：
                </td>
                <td width="188">
                    <input type="password" class="formsize120 inputtext" id="txtConfirmPwd" runat="server" />
                    <asp:HiddenField ID="hidConfirmPwd" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>姓名：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <input name="txtName" type="text" class="formsize80 inputtext" id="txtName" runat="server"
                        valid="required" errmsg="请输入姓名!" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    性别：
                </td>
                <td height="28" bgcolor="#e0e9ef">
                    <asp:DropDownList ID="seGenerlist" runat="server" CssClass="inputselect">
                    </asp:DropDownList>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>级别：
                </td>
                <td height="28" bgcolor="#e0e9ef">
                    <asp:DropDownList ID="ddlleaver" runat="server" CssClass="inputselect">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    类别：
                </td>
                <td colspan="3">
                    <asp:Literal ID="litguidType" runat="server"></asp:Literal>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    语种：
                </td>
                <td>
                    <input name="txtLanguage" runat="server" type="text" class="formsize120 inputtext"
                        id="txtLanguage" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>身份证号：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <input name="txtCardCode" type="text" class="formsize120 inputtext" id="txtCardCode"
                        runat="server" valid="required|isIdCard" errmsg="请输入身份证号!|身份证号码有误！" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>导游证号：
                </td>
                <td bgcolor="#e0e9ef">
                    <input name="txtGuidCode" type="text" class="formsize120 inputtext" id="txtGuidCode"
                        runat="server" valid="required" errmsg="请输入导游证号!" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    领队证号：
                </td>
                <td bgcolor="#e0e9ef">
                    <input name="txtTeamHeadCode" type="text" class="formsize120 inputtext" id="txtTeamHeadCode"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    导游证挂靠单位：
                </td>
                <td align="left">
                    <asp:DropDownList ID="dAnchoredComlist" runat="server" CssClass="inputselect">
                    </asp:DropDownList>
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    年审状态：
                </td>
                <td>
                    <asp:DropDownList ID="ddlAnnualReview" runat="server" CssClass="inputselect">
                        <asp:ListItem Value="1">已审</asp:ListItem>
                        <asp:ListItem Value="0">未审</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    家庭电话：
                </td>
                <td>
                    <input name="txtHomeTel" type="text" class="formsize120 inputtext" id="txtHomeTel"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>手机：
                </td>
                <td width="137" align="left" bgcolor="#E0E9EF">
                    <input name="txtMoblie" type="text" class="formsize120 inputtext" id="txtMoblie"
                        runat="server" valid="required|isMobile" errmsg="请输入手机号!|手机号码有误！" />
                </td>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="fontred">*</span>QQ：
                </td>
                <td bgcolor="#E0E9EF">
                    <input name="txtQQ" type="text" class="formsize120 inputtext" id="txtQQ" runat="server"
                        valid="required|isQQ" errmsg="请输入QQ号!|QQ号码有误！" />
                </td>
                <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    <span class="fontred">*</span>E-mail：
                </td>
                <td bgcolor="#E0E9EF">
                    <input name="txtEmail" type="text" class="formsize140 inputtext" id="txtEmail" runat="server"
                        valid="required|isEmail" errmsg="请输入Email!|邮箱有误！" />
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    家庭住址：
                </td>
                <td>
                    <input name="txtHomeAddress" type="text" class="formsize300 inputtext" id="txtHomeAddress"
                        runat="server" />
                </td>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    所属部门：
                </td>
                <td colspan="3">
                    <uc1:SelectSection ID="BelongDepart" runat="server" ReadOnly="true" SetTitle="所属部门"
                        SModel="1" />
                    <span class="fontred">*</span>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    照片：
                </td>
                <td colspan="5" bgcolor="#E0E9EF">
                    <%if (!string.IsNullOrEmpty(PhonePath))
                      { %>
                    <span id="spanFilePath"><a href="<%=PhonePath %>" target="_blank">查看附件</a>
                        <input type="hidden" name="hidFilePath" id="hidFilePath" runat="server" />
                        <img src="/Images/cha.gif" alt="" style="cursor: pointer" width="14" height="13"
                            class="delete" /></span>
                    <%} %>
                    <uc2:SingleFileUpload runat="server" ID="SingleFileUpload1" IsUploadMore="false"
                        FileTypes="*.jpg;*.gif;*.jpeg;*.png;*.bmp;" IsUploadSelf="true" CompanyID="<%=this.SiteUserInfo.CompanyId %>" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    性格特点：
                </td>
                <td height="40" colspan="5">
                    <textarea rows="6" class="inputtext formsize600" style="height: 35px;" id="txtcharacter"
                        runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    擅长线路：
                </td>
                <td height="40" colspan="5" bgcolor="#e0e9ef">
                    <textarea id="txtExperienceRoute" runat="server" rows="6" class="inputtext formsize600"
                        style="height: 35px;"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    客户评价：
                </td>
                <td height="40" colspan="5">
                    <textarea id="txtCommentAssess" runat="server" rows="6" class="inputtext formsize600"
                        style="height: 35px;"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    特长说明：
                </td>
                <td height="40" colspan="5" bgcolor="#e0e9ef">
                    <textarea rows="6" class="inputtext formsize600" style="height: 35px;" id="txtSpecialty"
                        runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td height="40" colspan="5">
                    <textarea id="txtRemark" runat="server" rows="6" class="inputtext formsize600" style="height: 35px;"></textarea>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <asp:PlaceHolder runat="server" ID="phBtnSave">
                <a id="btnSave" href="javascript:void(0);"><s class="baochun"></s>保 存</a> 
            </asp:PlaceHolder>
            <a href="javascript:void(0);"
                hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <asp:HiddenField ID="GovFileId" runat="server" />
    </form>

    <script type="text/javascript">
        var GuidInfoPage = {
            _ret: false,
            _check: function() {
                //确认密码
                var pwd = $.trim($("#<%=txtuserpwd.ClientID %>").val());
                var cofrimPwd = $.trim($("#<%=txtConfirmPwd.ClientID %>").val());
                var guid = '<%=Utils.GetQueryStringValue("Guid") %>';
                if (guid == "") {
                    if (pwd == "") {
                        parent.tableToolbar._showMsg("请输入密码!");
                        $("#<%=txtuserpwd.ClientID %>").focus();
                        return false;
                    }
                    if (cofrimPwd != pwd) {
                        parent.tableToolbar._showMsg("2次密码不一致!");
                        $("#<%=txtConfirmPwd.ClientID %>").focus();
                        return false;
                    }
                }
                return true;
            },
            _CheckIDCode: function() {
                $.newAjax({
                    type: "POST",
                    url: '/GuideCenter/GuideRecordInfo.aspx?action=CheckIDNumber&sl=<%=Utils.GetQueryStringValue("sl") %>&guideID=<%=Utils.GetQueryStringValue("Guid") %>&IDNumber=' + $("#txtCardCode").val(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar.ShowConfirmMsg("身份证号码已经存在，确认要保存吗?", function() {
                                $("#btnSave").html('<s class="baochun"></s>保存中.').css("background-position", "0 -57px").unbind("click");
                                var guid = '<%=Utils.GetQueryStringValue("Guid") %>';
                                GuidInfoPage._Save(guid);
                            });
                        } else {
                            $("#btnSave").html('<s class="baochun"></s>保存中.').css("background-position", "0 -57px").unbind("click");
                            var guid = '<%=Utils.GetQueryStringValue("Guid") %>';
                            GuidInfoPage._Save(guid);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        GuidInfoPage._BindBtn();
                    }
                });
                return false;
            },
            _Save: function(guid) {
                $.newAjax({
                    type: "POST",
                    url: '/GuideCenter/GuideRecordInfo.aspx?action=save&sl=<%=Utils.GetQueryStringValue("sl") %>&guideID=' + guid,
                    cache: false,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                if ('<%=EyouSoft.Common.Utils.GetQueryStringValue("pIframeID") %>' != "") {
                                    parent.Boxy.getIframeWindow('<%=Request.QueryString["pIframeID"] %>').location.reload();
                                }
                                else {
                                    parent.window.location.reload();
                                }

                            });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                GuidInfoPage._BindBtn();
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                            GuidInfoPage._BindBtn();
                        });
                        return false;
                    }
                });
            },
            _BindBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0 0").html('<s class="baochun"></s>保存').click(function() {
                    if (ValiDatorForm.validator($("#<%=form1.ClientID %>").get(0), "parent") && GuidInfoPage._check()) {
                        GuidInfoPage._CheckIDCode();
                    }
                });

                $("img.delete").unbind("click");
                $("img.delete").click(function() {
                    parent.tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
                        $("#spanFilePath").html("");
                    });
                    return false;
                });
            },
            _PageInit: function() {
                if ('<%=Utils.GetQueryStringValue("Guid") %>' != "") {
                    $("#<%=txtUserNames.ClientID %>").attr("readonly", "readonly").css("background-color", "#dadada");
                    $("#<%=txtuserpwd.ClientID %>").attr("readonly", "readonly").css("background-color", "#dadada");
                    $("#<%=txtConfirmPwd.ClientID %>").attr("readonly", "readonly").css("background-color", "#dadada");
                    $("#<%=txtuserpwd.ClientID %>").val("******");
                    $("#<%=txtConfirmPwd.ClientID %>").val("******");
                }
                GuidInfoPage._BindBtn();
            }
        }


        $(document).ready(function() {
            GuidInfoPage._PageInit();
        });
    </script>

</body>
</html>

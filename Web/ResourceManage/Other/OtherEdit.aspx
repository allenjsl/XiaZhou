<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherEdit.aspx.cs" Inherits="Web.ResourceManage.Other.OtherEdit"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-其它</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <input type="hidden" runat="server" id="Agreement" />
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        国家：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="inputselect" errmsg="请选择国家名称!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        省份：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProvice" runat="server" CssClass="inputselect" errmsg="请选择省份名称!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        城市：
                    </td>
                    <td width="29%" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="inputselect" errmsg="请选择城市名称!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="11%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        县区：
                    </td>
                    <td width="45%" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        单位名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUnitName" MaxLength="30" name="txtHotleName" runat="server" class="inputtext formsize250"
                            errmsg="请输入单位名称!" valid="required"></asp:TextBox><font class="fontred">*</font>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        地址：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtaddress" name="txtHotleName" runat="server" class="inputtext formsize350"
                            errmsg="请输入地址!" valid="required"></asp:TextBox><font class="fontred">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否推荐：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:RadioButton ID="RadRecommend_yes" runat="server" name="RadRecommend_yes" Style="border: none"
                            GroupName="radgroupRecom" />
                        是
                        <asp:RadioButton ID="RadRecommend_no" Checked="true" runat="server" name="RadRecommend_no"
                            Style="border: none" GroupName="radgroupRecom" />
                        否
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签单：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:RadioButton ID="RadSign_yes" runat="server" name="RadSign_yes" Style="border: none"
                            GroupName="radgroupSign" />
                        是
                        <asp:RadioButton ID="RadSign_no" Checked="true" runat="server" name="RadSign_no"
                            Style="border: none" GroupName="radgroupSign" />
                        否
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签订合同：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="Radio_hd_yes" runat="server" name="Radio_hd_yes" Style="border: none"
                            GroupName="Rad_hd" />
                        是
                        <label style="display: none;" id="lbContract">
                            合同号：<asp:TextBox ID="txtContractNum" runat="server" Class="inputtext formsize120"></asp:TextBox></label>
                        <asp:RadioButton ID="Radio_hd_no" runat="server" name="Radio_hd_no" Style="border: none"
                            GroupName="Rad_hd" Checked="true" />
                        否
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合同期限：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtContractDate_Start" onfocus="WdatePicker({minDate:'%y-%M-{%d}'})"
                            name="txtContractDate_Start" runat="server" Class="inputtext formsize120"></asp:TextBox>-<asp:TextBox
                                ID="txtContractDate" name="txtContractDate" runat="server" Class="inputtext formsize120"
                                onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtContractDate_Start\')}'})"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="13%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合同附件上传：
                    </td>
                    <td bgcolor="#e0e9ef" align="left" colspan="3">
                        <uc2:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label ID="lbhd" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="13%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合作协议：
                    </td>
                    <td align="left" colspan="3">
                        <uc2:UploadControl ID="UploadControl2" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label ID="lbFiles" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="13%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td height="58" bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox TextMode="MultiLine" ID="txtRemarks" name="txtRemarks" runat="server"
                            CssClass="inputtext formsize600" Style="height: 40px; padding: 3px;" Columns="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        最后维护人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        维护时间：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLastDate" name="txtLastDate" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
        <uc1:Contact ID="Contact1" runat="server" />
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">其他类别</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="autoAdd" id="other_tbl">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td width="25" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            编号
                        </td>
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            名称
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            价格
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            结算价
                        </td>
                        <td width="32%" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            简要描述
                        </td>
                        <td width="105" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            操作
                        </td>
                    </tr>
                    <%if (SetListCount == 0)
                      { %>
                    <tr class="tempRow">
                        <td bgcolor="#FFFFFF" align="center">
                            <span class="index">1</span>
                        </td>
                        <td height="28" bgcolor="#FFFFFF" align="center">
                            <input type="text" valid="required" errmsg="请输入其他类别名称" class="inputtext formsize140"
                                name="TypeName" value=""><font class="fontred">*</font>
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" class="inputtext formsize80" valid="isMoney" errmsg="价格格式错误" name="Price"
                                value="">
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" class="inputtext formsize80" valid="isMoney" errmsg="价格格式错误" name="ClosingCost"
                                value="">
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <textarea style="height: 30px;" class="inputtext formsize180" name="Desc" value=""></textarea>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <a class="addother" href="javascript:void(0)">
                                <img width="48" height="20" src="/images/addimg.gif"></a> <a href="javascript:void(0)">
                                    <img src="/images/delimg.gif" class="delother"></a>
                        </td>
                    </tr>
                    <%} %>
                    <asp:Repeater ID="rptlist" runat="server">
                        <ItemTemplate>
                            <tr class="tempRow">
                                <td bgcolor="#FFFFFF" align="center">
                                    <span class="index">
                                        <%#Container.ItemIndex+1 %></span>
                                </td>
                                <td height="28" bgcolor="#FFFFFF" align="center">
                                    <input type="text" valid="required" errmsg="请输入其他类别名称" class="inputtext formsize140"
                                        name="TypeName" value="<%#Eval("Name") %>"><font class="fontred">*</font>
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <input type="text" class="inputtext formsize80" valid="isMoney" errmsg="价格格式错误" name="Price"
                                        value='<%#Convert.ToDecimal(Eval("Price"))==0?"":EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("Price"))) %>'>
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <input type="text" class="inputtext formsize80" valid="isMoney" errmsg="价格格式错误" name="ClosingCost"
                                        value='<%#Convert.ToDecimal(Eval("ClosingCost"))==0?"":EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("ClosingCost"))) %>'>
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <textarea style="height: 30px;" class="inputtext formsize180" name="Desc" value=""><%#Eval("Desc") %></textarea>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <a class="addother" href="javascript:void(0)">
                                        <img width="48" height="20" src="/images/addimg.gif"></a> <a href="javascript:void(0)">
                                            <img src="/images/delimg.gif" class="delother"></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSave" runat="server"><s class="baochun">
            </s>保 存</a> <a hidefocus="true" href="javascript:void(0);" onclick="OtherEditPage.close()">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <input type="hidden" id="hidSourceID" value="" runat="server" />
    </form>
</body>
</html>

<script type="text/javascript">
    if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
        $("#<%=btnSave.ClientID %>").hide();
    }
    $(function() {
        OtherEditPage.PageInit();
        FV_onBlur.initValid($("#<%=form1.ClientID %>"));
        $("#Agreement").val($("#lbFiles").find("a").attr("href"));
        pcToobar.init({
            gID: "#<%=ddlCountry.ClientID %>",
            pID: "#<%=ddlProvice.ClientID %>",
            cID: "#<%=ddlCity.ClientID %>",
            xID: "#<%=ddlArea.ClientID %>",
            gSelect: '<%=this.Countryindex %>',
            pSelect: '<%=this.Provinceindex %>',
            cSelect: '<%=this.Cityindex %>',
            xSelect: '<%=this.Areaindex %>',
            comID: '<%=this.SiteUserInfo.CompanyId %>'
        })
        $("#other_tbl").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addother", delButtonClass: "delother", indexClass: "index" })
        OtherEditPage.BindBtn();

    })
    var OtherEditPage = {
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        PageInit: function() {
            if ($("#<%=Radio_hd_yes.ClientID %>").attr("checked") == true) {
                $("#lbContract").show();
            }

            $("#<%=Radio_hd_yes.ClientID %>").click(function() {
                $("#lbContract").show();
            })
            $("#<%=Radio_hd_no.ClientID %>").click(function() {
                $("#lbContract").hide();
            })
        },
        ajaxurl: "/ResourceManage/Other/OtherEdit.aspx",
        CheckForm: function() {
            return ValiDatorForm.validator($("#<%=btnSave.ClientID %>").closest("form").get(0), "parent");
        },
        GoAjax: function(url) {
            $("#<%=btnSave.ClientID %>").html("提交中...").unbind("click");
            $.newAjax({
                type: "post",
                cache: false,
                url: url,
                dataType: "json",
                data: $("#<%=btnSave.ClientID %>").closest("form").serialize(),
                success: function(ret) {
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg, function() { parent.location.href = parent.location.href; });
                    }
                    else {
                        tableToolbar._showMsg(ret.msg, function() { OtherEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { OtherEditPage.BindBtn() });
                }
            });
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!OtherEditPage.CheckForm()) {
                    return false;
                }
                OtherEditPage.GoAjax(OtherEditPage.ajaxurl + '?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_其它 %>&id=<%=Request.QueryString["id"]%>');
                return false;
            })
            $("#<%=btnSave.ClientID %>").html("<s class='baochun'></s>保存").attr("class", "");
        }
    }
</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotleEdit.aspx.cs" Inherits="Web.ResourceManage.Hotle.HotleEdit"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/SupplierControl.ascx" TagName="SupplierControl"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/CuisineSelect.ascx" TagName="CuisineSelect" TagPrefix="uc3" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-餐馆</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto" id="liststyle">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    国家：
                </td>
                <td width="15%" align="left">
                    <asp:DropDownList ID="ddlCountry" runat="server" class="inputselect" errmsg="请选择国家!"
                        valid="required">
                    </asp:DropDownList>
                    <font class="fontbsize12">*</font>
                </td>
                <td width="8%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    省份：
                </td>
                <td width="14%" align="left">
                    <asp:DropDownList ID="ddlProvice" runat="server" class="inputselect" errmsg="请选择省份!"
                        valid="required">
                    </asp:DropDownList>
                    <font class="fontbsize12">*</font>
                </td>
                <td width="7%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    城市：
                </td>
                <td width="17%" align="left">
                    <asp:DropDownList ID="ddlCity" runat="server" class="inputselect" errmsg="请选择城市!"
                        valid="required">
                    </asp:DropDownList>
                    <font class="fontbsize12">*</font>
                </td>
                <td width="7%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    县区：
                </td>
                <td width="20%" align="left">
                    <asp:DropDownList ID="ddlArea" runat="server" class="inputselect">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    餐馆名称：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtHotleName" MaxLength="30" name="txtHotleName" runat="server"
                        CssClass="inputtext formsize180" errmsg="请输入餐馆名称!" valid="required"></asp:TextBox><font
                            color="#FF0000">*</font>
                </td>
                <td width="7%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    菜系：
                </td>
                <td width="17%" align="left" bgcolor="#e0e9ef">
                    <uc3:CuisineSelect ID="CuisineSelect1" runat="server" SetTitle="菜系" SModel="2" />
                </td>
                <td width="7%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    餐标：
                </td>
                <td width="20%" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtPmin" rel="txtP" runat="server" class="inputtext formsize40"
                        valid="isNumber" errmsg="餐标必须是数字!" onblur="HotleEditPage.CheckPrice()"></asp:TextBox>
                    -<asp:TextBox ID="txtPmax" rel="txtP" runat="server" class="inputtext formsize40"
                        onblur="HotleEditPage.CheckPrice()" valid="isNumber" errmsg="餐标必须是数字!"></asp:TextBox>元/人
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    地址：
                </td>
                <td colspan="7" align="left">
                    <asp:TextBox ID="txtaddress" height="17px" name="txtaddress" runat="server" class="inputtext formsize600"
                        errmsg="请输入地址!" valid="required"></asp:TextBox><font color="#FF0000">*</font>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    餐馆简介：
                </td>
                <td height="58" colspan="7" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox TextMode="MultiLine" ID="txthotleinfo" name="txthotleinfo" runat="server"
                        CssClass="inputtext formsize600" Style="height: 45px; padding: 3px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td height="58" colspan="7" align="left">
                    <asp:TextBox TextMode="MultiLine" ID="txtRemarks" name="txtRemarks" runat="server"
                        CssClass="inputtext formsize600" Style="height: 45px; padding: 3px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    返利：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton ID="radyes" runat="server" GroupName="radgroup" Style="border: none" />
                    是
                    <asp:RadioButton ID="radno" runat="server" GroupName="radgroup" Style="border: none"
                        Checked="true" />
                    否
                </td>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否签订合同：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton ID="Radio_hd_yes" runat="server" name="Radio_hd_yes" Style="border: none"
                        GroupName="Rad_hd" />
                    是
                    <label style="display: none;" id="lbContract">
                        合同号：<asp:TextBox ID="txtContractNum" runat="server" Class="inputtext formsize120"></asp:TextBox></label>
                    <asp:RadioButton ID="Radio_hd_no" runat="server" name="Radio_hd_no" Style="border: none"
                        GroupName="Rad_hd" />
                    否
                </td>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    合同附件上传：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <uc4:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label ID="lbFiles" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    合同期限：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtContractDate_Start" onfocus="WdatePicker({minDate:'%y-%M-{%d}'})"
                        name="txtContractDate_Start" runat="server" Class="inputtext formsize120"></asp:TextBox>-<asp:TextBox
                            ID="txtContractDate" name="txtContractDate" runat="server" Class="inputtext formsize120"
                            onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtContractDate_Start\')}'})"></asp:TextBox>
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    是否签单：
                </td>
                <td align="left" colspan="5">
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
                    是否推荐：
                </td>
                <td bgcolor="#E0E9EF" align="left">
                    <asp:RadioButton ID="RadRecommend_yes" runat="server" name="RadRecommend_yes" Style="border: none"
                        GroupName="radgroupRecom" />
                    是
                    <asp:RadioButton ID="RadRecommend_no" Checked="true" runat="server" name="RadRecommend_no"
                        Style="border: none" GroupName="radgroupRecom" />
                    否
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    <font class="fontred"></font>最后维护人：
                </td>
                <td bgcolor="#E0E9EF" align="left">
                    <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext formsize80"></asp:TextBox>
                </td>
                <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    维护时间：
                </td>
                <td bgcolor="#E0E9EF" align="left" colspan="3">
                    <asp:TextBox ID="txtLastDate" name="txtLastDate" runat="server" class="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <uc2:Contact ID="Contact1" runat="server" />
        <div class="alertbox-btn">
            <a id="btnSave" href="javascript:void(0);" runat="server"><s class="baochun"></s>保 存</a> <a hidefocus="true"
                href="javascript:void(0);" onclick="HotleEditPage.close()"><s class="chongzhi"></s>
                关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
        $("#<%=btnSave.ClientID %>").hide();
    }
    $(function() {
        HotleEditPage.PageInit();

    })
    var HotleEditPage = {
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        PageInit: function() {
            FV_onBlur.initValid($("#<%=form1.ClientID %>"));
            HotleEditPage.BindBtn();
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
        CheckPrice: function() {
            var pmin = $("#txtPmin").val();
            var pmax = $("#txtPmax").val();
            if (parseFloat(pmax) < parseFloat(pmin)) {
                tableToolbar._showMsg("餐标区间有误");
                $(this).focus();
                return false;
            }
            else {
                return true;
            }
        },
        ajaxurl: "/ResourceManage/Hotle/HotleEdit.aspx",
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
                        tableToolbar._showMsg(ret.msg, function() { HotleEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { HotleEditPage.BindBtn() });
                }
            });
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!HotleEditPage.CheckForm()) {
                    return false;
                }
                if (!HotleEditPage.CheckPrice()) {
                    return false;
                }
                HotleEditPage.GoAjax(HotleEditPage.ajaxurl + '?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_餐馆 %>&id=<%=Request.QueryString["id"] %>');
                return false;
            })
            $("#<%=btnSave.ClientID %>").html("<s class='baochun'></s>保存").attr("class", "");
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        }

    }
</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopEdit.aspx.cs" Inherits="Web.ResourceManage.Shopping.ShopEdit"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-购物-新增</title>
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
    <style type="text/css">
        .progressWrapper
        {
            width: 260px;
            overflow: hidden;
        }
    </style>
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
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        商店名称：
                    </td>
                    <td width="29%" align="left">
                        <asp:TextBox ID="txtShopName" MaxLength="30" runat="server" class="inputtext formsize180"
                            errmsg="请输入商店名称!" valid="required"></asp:TextBox><font class="fontbsize12">*</font>
                    </td>
                    <td width="11%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        商品类别：
                    </td>
                    <td width="45%" align="left">
                        <asp:TextBox ID="txtSellType" runat="server" class="inputtext formsize180"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        地址：
                    </td>
                    <td bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox ID="txtaddress" runat="server" class="inputtext formsize450" errmsg="请输入地址!"
                            valid="required"></asp:TextBox><font class="fontbsize12">*</font>
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
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
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
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合同附件上传：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <uc2:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label ID="lbhd" runat="server" Text=""></asp:Label>
                    </td>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
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
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否推荐：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="RadRecommend_yes" runat="server" name="RadRecommend_yes" Style="border: none"
                            GroupName="radgroupRecom" />
                        是
                        <asp:RadioButton ID="RadRecommend_no" Checked="true" runat="server" name="RadRecommend_no"
                            Style="border: none" GroupName="radgroupRecom" />
                        否
                    </td>
                    <td width="11%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返利：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="radyes" runat="server" name="radgroup" Style="border: none"
                            GroupName="radgroup" />
                        是
                        <asp:RadioButton ID="radno" Checked="true" runat="server" name="radgroup" Style="border: none"
                            GroupName="radgroup" />
                        否
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否返单：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:RadioButton ID="radBackSingleyes" runat="server" name="radBackSingleyes" Style="border: none"
                            GroupName="radBacksingle" />
                        是
                        <asp:RadioButton ID="radBackSingleno" Checked="true" runat="server" name="radBackSingleno"
                            Style="border: none" GroupName="radBacksingle" />
                        否
                    </td>
                    <td width="11%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合作协议：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <uc2:UploadControl ID="UploadControl2" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label ID="lbFiles" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返佣政策：
                    </td>
                    <td height="58" align="left" colspan="3">
                        <asp:TextBox TextMode="MultiLine" ID="txtPolicy" Columns="6" class="inputtext formsize600"
                            runat="server" Style="height: 36px; padding: 3px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td height="58" bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox TextMode="MultiLine" ID="txtRemarks" Columns="6" class="inputtext formsize600"
                            runat="server" Style="height: 40px; padding: 3px;"></asp:TextBox>
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
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSave" runat="server"><s class="baochun"></s>保
                存</a><a hidefocus="true" href="javascript:void(0);" onclick="ShopEditPage.close()"><s
                    class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <input type="hidden" id="hidSourceID" runat="server" />
    </form>
</body>
</html>

<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

<script type="text/javascript">
    if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
        $("#<%=btnSave.ClientID %>").hide();
    }
    $(function() {
        ShopEditPage.PageInit();

    })

    var ShopEditPage = {
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        ajaxurl: "/ResourceManage/Shopping/ShopEdit.aspx",
        PageInit: function() {
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
            FV_onBlur.initValid($("#<%=form1.ClientID %>"));
            $("#Agreement").val($("#lbFiles").find("a").attr("href"));
            ShopEditPage.BindBtn();
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
        CheckForm: function() {
            var form = $("#<%=btnSave.ClientID %>").closest("form").get(0);

            return ValiDatorForm.validator(form, "parent");
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
                        tableToolbar._showMsg(ret.msg, function() { ShopEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { ShopEditPage.BindBtn() });
                }
            });
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!ShopEditPage.CheckForm()) {
                    return false;
                }
                ShopEditPage.GoAjax(ShopEditPage.ajaxurl + "?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_购物 %>&id=" + $("#hidSourceID").val());
                return false;
            })
            $("#<%=btnSave.ClientID %>").attr("class", "").html("<s class='baochun'></s>保存");
        }
    }
    function delFile(obj) {
        tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
            $(obj).siblings("#lbFiles").hide()
            $(obj).hide();
            $("#Agreement").val("");
        });
    }
</script>


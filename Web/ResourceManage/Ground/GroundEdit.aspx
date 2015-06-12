<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroundEdit.aspx.cs" Inherits="Web.ResourceManage.Ground.GroundEdit"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-地接社</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <input type="hidden" id="address" runat="server" />
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        国家：
                    </td>
                    <td width="25%" align="left">
                        <asp:DropDownList ID="ddlCountry" runat="server" errmsg="请选择国家!" valid="required"
                            class="inputselect">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        省份：
                    </td>
                    <td width="45%" align="left">
                        <asp:DropDownList ID="ddlProvice" errmsg="请选择省份!" valid="required" runat="server"
                            class="inputselect">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        城市：
                    </td>
                    <td width="25%" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlCity" errmsg="请选择城市!" valid="required" runat="server" class="inputselect">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        县区：
                    </td>
                    <td width="45%" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlArea" runat="server" class="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontred">*</font>单位名称：
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtUnitName" MaxLength="30" name="txtUnitName" runat="server" Class="inputtext formsize180"
                            errmsg="请输入单位名称!" valid="required"></asp:TextBox>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        许可证号：
                    </td>
                    <td width="45%" align="left">
                        <asp:TextBox ID="txtPermitNum" name="txtPermitNum" runat="server" Class="inputtext formsize180"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        法人代表：
                    </td>
                    <td width="25%" bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtLegalName" name="txtPermitNum" runat="server" Class="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        电话：
                    </td>
                    <td width="45%" bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtTel" name="txtTel" valid="isTelephone" errmsg="电话格式错误!" runat="server"
                            Class="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontred">*</font>地址：
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtaddress" name="txtaddress" runat="server" class="inputtext formsize300"
                            Style="width: 300px" errmsg="请输入地址!" valid="required"></asp:TextBox>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签订合同：
                    </td>
                    <td width="45%" align="left">
                        <asp:RadioButton ID="Radio_hd_yes" runat="server" name="Radio_hd_yes" Style="border: none"
                            GroupName="Rad_hd" />
                        是
                        <label style="display: none;" id="lbContract">
                            合同号：<asp:TextBox ID="txtContractNum" runat="server" Class="inputtext formsize120"></asp:TextBox></label>
                        <asp:RadioButton ID="Radio_hd_no" runat="server" name="Radio_hd_no" Style="border: none"
                            GroupName="Rad_hd" Checked="true" />
                        否
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合同有效期：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtContractDate_Start" onfocus="WdatePicker({minDate:'%y-%M-{%d}'})"
                            name="txtContractDate_Start" runat="server" Class="inputtext formsize120"></asp:TextBox>-<asp:TextBox
                                ID="txtContractDate" name="txtContractDate" runat="server" Class="inputtext formsize120"
                                onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtContractDate_Start\')}'})"></asp:TextBox>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        上传合同附件：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <uc1:UploadControl ID="UploadControl1" IsUploadMore="false" IsUploadSelf="true" runat="server" />
                        <asp:Label runat="server" ID="lbFiles" class="labelFiles"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        特色线路：
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox TextMode="MultiLine" name="txtCharacter" ID="txtCharacter" Style="height: 35px;
                            padding: 3px;" runat="server" class="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
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
                    <td bgcolor="#B7E0F3" align="right">
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
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否返佣：
                    </td>
                    <td align="left" colspan="3">
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
                        返佣政策：
                    </td>
                    <td bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox TextMode="MultiLine" ID="txtpolicy" name="txtpolicy" Style="height: 35px;
                            padding: 3px;" runat="server" class="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        最后维护人：
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        维护时间：
                    </td>
                    <td width="45%" align="left">
                        <asp:TextBox ID="txtLastDate" name="txtLastDate" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
        <uc2:Contact ID="Contact1" runat="server" />
        <div class="alertbox-btn">
            <a id="btnSave" href="javascript:void(0);" runat="server"><s class="baochun"></s>保 存</a> <a hidefocus="true"
                href="javascript:void(0);" onclick="GroundEditPage.CloseForm()"><s class="chongzhi">
                </s>关 闭</a>
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
        GroundEditPage.BindBtn()
        if ($("#<%=Radio_hd_yes.ClientID %>").attr("checked") == true) {
            $("#lbContract").show();
        }

        $("#<%=Radio_hd_yes.ClientID %>").click(function() {
            $("#lbContract").show();
        })
        $("#<%=Radio_hd_no.ClientID %>").click(function() {
            $("#lbContract").hide();
        })

    })

    var GroundEditPage = {
        ajaxurl: "/ResourceManage/Ground/GroundEdit.aspx",
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
                        tableToolbar._showMsg(ret.msg, function() { GroundEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { GroundEditPage.BindBtn() });
                }
            });
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        CloseForm: function() {
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!GroundEditPage.CheckForm()) {
                    return false;
                }
                GroundEditPage.GoAjax(GroundEditPage.ajaxurl + "?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_地接社 %>&id=" + $("#hidSourceID").val());
                return false;
            })
            $("#<%=btnSave.ClientID %>").html("<s class='baochun'></s>保存").attr("class", "");
        }
    }

</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarEdit.aspx.cs" Inherits="Web.ResourceManage.Car.CarEidt"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-车队-新增</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        .progressWrapper
        {
            width: 178px;
            overflow: hidden;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="Agreement" />
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        国家：
                    </td>
                    <td width="15%" align="left">
                        <asp:DropDownList ID="ddlCountry" errmsg="请选择国家!" valid="required" runat="server"
                            CssClass="inputselect">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        省份：
                    </td>
                    <td width="15%" align="left">
                        <asp:DropDownList ID="ddlProvice" errmsg="请选择省份!" valid="required" runat="server"
                            CssClass="inputselect">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        城市：
                    </td>
                    <td width="15%" align="left">
                        <asp:DropDownList ID="ddlCity" errmsg="请选择城市!" valid="required" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        县区：
                    </td>
                    <td width="15%" align="left">
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        车队名称：
                    </td>
                    <td bgcolor="#E0E9EF" align="left" colspan="3">
                        <asp:TextBox ID="txtteamName" MaxLength="30" runat="server" class="inputtext formsize140"
                            errmsg="请输入车队名称!" valid="required"></asp:TextBox><font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返利：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="RadioButton1" runat="server" name="Rebateradgroup" Style="border: none"
                            GroupName="Rebateradgroup" />
                        是
                        <asp:RadioButton ID="RadioButton2" runat="server" name="Rebateradgroup" Style="border: none"
                            GroupName="Rebateradgroup" Checked="true" />
                        否
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签单：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="radyes" runat="server" name="signradgroup" Style="border: none"
                            GroupName="signradgroup" />
                        是
                        <asp:RadioButton ID="radno" runat="server" name="signradgroup" Style="border: none"
                            GroupName="signradgroup" Checked="true" />
                        否
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否推荐：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="radRecommendyes" runat="server" name="radRecommendyes" Style="border: none"
                            GroupName="Recommend" />
                        是
                        <asp:RadioButton ID="radRecommendno" runat="server" name="radRecommendno" Style="border: none"
                            GroupName="Recommend" Checked="true" />
                        否
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签订合同：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:RadioButton ID="Radio_hd_yes" runat="server" name="Radio_hd_yes" Style="border: none"
                            GroupName="Rad_hd" />
                        是
                        <label style="display: none;" id="lbContract">
                            合同号：<asp:TextBox ID="txtContractNum" runat="server" Class="inputtext formsize120"></asp:TextBox></label>
                        <asp:RadioButton ID="Radio_hd_no" runat="server" name="Radio_hd_no" Style="border: none"
                            GroupName="Rad_hd" Checked="true" />
                        否
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        合同期限：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtContractDate_Start" onfocus="WdatePicker({minDate:'%y-%M-{%d}'})"
                            name="txtContractDate_Start" runat="server" Class="inputtext formsize120"></asp:TextBox>-<asp:TextBox
                                ID="txtContractDate" name="txtContractDate" runat="server" Class="inputtext formsize120"
                                onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtContractDate_Start\')}'})"></asp:TextBox>
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        <span class="alertboxTableT">合同附件上传：</span>
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <uc2:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label ID="lbFiles" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        备注：
                    </td>
                    <td height="43" align="left" colspan="5">
                        <asp:TextBox ID="txtRemark" name="txtRemark" runat="server" TextMode="MultiLine"
                            Style="height: 30px; padding: 3px;" class="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontred"></font>最后维护人：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        维护时间：
                    </td>
                    <td bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox ID="txtLastDate" name="txtLastDate" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">车辆信息</span>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" id="car_tbl">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        编号
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        车型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        座位数
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        车号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        司机
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        司机电话
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        是否保险
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        保险金额
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        保险期限
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <%if (!(this.SetListCount > 0))
                  {%>
                <tr class="tempRow">
                    <td height="28" align="center">
                        <span class="index">1</span>
                    </td>
                    <td height="28" align="center">
                        <input type="text" value="" errmsg="请输入车型名称!" valid="required" class="inputtext formsize80"
                            name="TypeName"><font class="fontbsize12">*</font>
                        <input type="hidden" name="txtCheXingId" value="" />
                    </td>
                    <td align="center">
                        <input type="text" value="" class="inputtext formsize40" onafterpaste="this.value=this.value.replace(/\D/g,'')"
                            onkeyup="this.value=this.value.replace(/\D/g,'')" name="SeatCount">
                    </td>
                    <td align="center">
                        <input type="text" value="" class="inputtext formsize80" name="CarNumber" />
                    </td>
                    <td align="center">
                        <input type="text" value="" class="inputtext formsize80" name="Driver" />
                    </td>
                    <td align="center">
                        <input type="text" value="" valid="isTelephone" errmsg="司机电话格式错误!" class="inputtext formsize80"
                            name="DriverTel" />
                    </td>
                    <td align="center">
                        <input type="hidden" name="IsInsurance" value="1" />
                        <input type="radio" data-name="IsIns" checked="checked" />是
                        <input type="radio" data-name="noIns" />否
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input type="text" class="inputtext formsize40" name="InsAmount" value="" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input type="text" class="inputtext formsize80" name="Timelimit" value="" onfocus="WdatePicker()" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <a class="addcar" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                <img src="/images/delimg.gif" class="delcar" /></a>
                    </td>
                </tr>
                <%} %>
                <asp:Repeater ID="rptListcar" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td height="28" align="center">
                                <span class="index">
                                    <%#Container.ItemIndex + 1 %></span>
                            </td>
                            <td height="28" align="center">
                                <input type="text" errmsg="请输入车型名称!" valid="required" value="<%#Eval("TypeName") %>"
                                    class="inputtext formsize80" name="TypeName"><font class="fontbsize12">*</font>
                                <input type="hidden" name="txtCheXingId" value="<%#Eval("CarId") %>" />
                            </td>
                            <td align="center">
                                <input type="text" value="<%#Convert.ToInt32(Eval("SeatNumber"))==0?"":Eval("SeatNumber") %>"
                                    class="inputtext formsize40" name="SeatCount" onafterpaste="this.value=this.value.replace(/\D/g,'')"
                                    onkeyup="this.value=this.value.replace(/\D/g,'')" />
                            </td>
                            <td align="center">
                                <input type="text" value="<%#Eval("CarNumber") %>" class="inputtext formsize80" name="CarNumber">
                            </td>
                            <td align="center">
                                <input type="text" value="<%#Eval("Driver") %>" class="inputtext formsize80" name="Driver">
                            </td>
                            <td align="center">
                                <input type="text" valid="isTelephone" errmsg="司机电话格式错误!" value="<%#Eval("DriverTel") %>"
                                    class="inputtext formsize80" name="DriverTel">
                            </td>
                            <td align="center">
                                <input type="hidden" name="IsInsurance" value="<%#Convert.ToBoolean(Eval("IsInsurance"))==true?"1":"0" %>" />
                                <input type="radio" data-name="IsIns" <%#Convert.ToBoolean(Eval("IsInsurance"))==true?"checked=checked":"" %> />是
                                <input type="radio" <%#Convert.ToBoolean(Eval("IsInsurance"))==false?"checked=checked":"" %>
                                    data-name="noIns" />否
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" valid="isNumber" errmsg="金额必须是数字!" class="inputtext formsize40"
                                    name="InsAmount" value="<%#Convert.ToDecimal(Eval("InsuranceAmount"))==0?"":EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Convert.ToDecimal(Eval("InsuranceAmount"))) %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" class="inputtext formsize80" name="Timelimit" value="<%#Eval("InsuranceLimit")==null?"":EyouSoft.Common.UtilsCommons.SetDateTimeFormart((DateTime?)Eval("InsuranceLimit")) %>"
                                    onfocus="WdatePicker()" />
                            </td>
                            <td align="center">
                                <a class="addcar" href="javascript:void(0)">
                                    <img width="48" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                        <img src="/images/delimg.gif" class="delcar" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <uc1:Contact ID="Contact1" runat="server" />
        <div class="alertbox-btn">
            <a id="btnSave" href="javascript:void(0);" runat="server"><s class="baochun"></s>保 存</a><a href="javascript:void(0)"
                hidefocus="true" onclick="CarEditPage.close()"><s class="chongzhi"></s>取 消</a>
        </div>
    </div>
    <input type="hidden" id="address" runat="server" />
    </form>
</body>
</html>

<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

<script type="text/javascript">
    if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
        $("#<%=btnSave.ClientID %>").hide();
    }
    $(function() {
        CarEditPage.PageInit();

    })

    var CarEditPage = {
        ajaxurl: "/ResourceManage/Car/CarEdit.aspx",
        addressPro: "",
        addressCity: "",
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
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
                        tableToolbar._showMsg(ret.msg, function() { CarEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { CarEditPage.BindBtn() });
                }
            });
        },
        PageInit: function() {
            FV_onBlur.initValid($("#<%=form1.ClientID %>"));
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
            CarEditPage.BindBtn();
            $("#car_tbl").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addcar", delButtonClass: "delcar", indexClass: "index" })
            $("#<%=ddlProvice.ClientID %>").change(function() {
                if ($('#<%=ddlProvice.ClientID %> option:selected').text() != "--请选择--") {
                    CarEditPage.addressPro = $('#<%=ddlProvice.ClientID %> option:selected').text();
                }
                else {
                    CarEditPage.addressPro = "";
                }
            })
            $("#<%=ddlCity.ClientID %>").change(function() {
                if ($('#<%=ddlCity.ClientID %> option:selected').text() != "--请选择--") {
                    CarEditPage.addressCity = $('#<%=ddlCity.ClientID %> option:selected').text();
                }
                else {
                    CarEditPage.addressCity = "";
                }
            })
            $("#car_tbl").find("input[data-name='IsIns']").change(function() {
                if (this.checked) {
                    $(this).parent().find("input[type='hidden']").val("1");
                    $(this).attr("checked", "checked")
                    $(this).next().attr("checked", "");
                }

            })
            $("#car_tbl").find("input[data-name='noIns']").change(function() {
                if (this.checked) {
                    $(this).parent().find("input[type='hidden']").val("0");
                    $(this).prev().attr("checked", "");
                    $(this).attr("checked", "checked")
                }
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
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!CarEditPage.CheckForm()) {
                    return false;
                }
                $("#address").val(CarEditPage.addressPro + CarEditPage.addressCity);
                CarEditPage.GoAjax(CarEditPage.ajaxurl + '?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_车队 %>&id=<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>');
                return false;
            })
            $("#<%=btnSave.ClientID %>").html("<s class='baochun'></s>保存").attr("class", "");
        }

    }
</script>


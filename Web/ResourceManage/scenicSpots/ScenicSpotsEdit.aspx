<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicSpotsEdit.aspx.cs"
    Inherits="Web.ResourceManage.scenic_spots.Scenic_spotsEdit" EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-景点-新增</title>
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
            width: 178px;
            overflow: hidden;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server" enctype="multipart/form-data">
    <input type="hidden" id="imgcount" runat="server" />
    <input type="hidden" runat="server" id="Agreement" />
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto" id="tablist">
            <tbody>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        国家：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="inputselect" errmsg="请选择国家!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        省份：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProvice" runat="server" CssClass="inputselect" errmsg="请选择省份!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        城市：
                    </td>
                    <td width="23%" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="inputselect" errmsg="请选择城市!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        县区：
                    </td>
                    <td width="46%" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        景区单位名称：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtUnitName" MaxLength="30" runat="server" class="inputtext formsize180"
                            errmsg="请输入单位名称!" valid="required"></asp:TextBox><font class="fontred">*</font>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
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
                </tr>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合同期限：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtContractDate_Start" onfocus="WdatePicker({minDate:'%y-%M-{%d}'})"
                            name="txtContractDate_Start" runat="server" Class="inputtext inputtext formsize120"></asp:TextBox>-<asp:TextBox
                                ID="txtContractDate" name="txtContractDate" runat="server" Class="inputtext inputtext formsize120"
                                onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtContractDate_Start\')}'})"></asp:TextBox>
                    </td>
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        合同附件上传：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <uc2:UploadControl ID="UploadControl2" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                        <asp:Label ID="lbFiles" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
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
                    <td bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签单：
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="RadSign_yes" runat="server" name="RadSign_yes" Style="border: none"
                            GroupName="radgroupSign" />
                        是
                        <asp:RadioButton ID="RadSign_no" Checked="true" runat="server" name="RadSign_no"
                            Style="border: none" GroupName="radgroupSign" />
                        否
                    </td>
                </tr>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
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
                    <td width="15%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返利：
                    </td>
                    <td bgcolor="#e0e9ef" align="left">
                        <asp:RadioButton ID="radyes" runat="server" name="radgroup" Style="border: none"
                            GroupName="radgroup" />
                        是
                        <asp:RadioButton ID="radno" Checked="true" runat="server" name="radgroup" Style="border: none"
                            GroupName="radgroup" />
                        否
                    </td>
                </tr>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        景区图片上传：
                    </td>
                    <td align="left" colspan="3">
                        <uc2:UploadControl ID="UploadControl1" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                            IsUploadMore="true" IsUploadSelf="true" />
                        <asp:Literal ID="imgFile" runat="server"></asp:Literal></br>
                        <div style="width: 560px; float: left; margin-left: 5px;">
                            <asp:Repeater ID="rplimg" runat="server">
                                <ItemTemplate>
                                    <img height="75" width="100" alt="" class="addpic img" style="vertical-align: bottom;"
                                        src="<%#Eval("FilePath") %>" rel="<%#Eval("Name") %>">
                                    <span style="vertical-align: bottom;">&nbsp<img alt="" class="pand4" style="vertical-align: bottom;"
                                        src="/images/cha.gif" data-delimg="delimg" /><input type="hidden" name="hideimg"
                                            value="<%#Eval("Name") %>|<%#Eval("FilePath") %>" /></span>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td width="16%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返利政策描述：
                    </td>
                    <td height="52" bgcolor="#e0e9ef" align="left" colspan="3">
                        <asp:TextBox TextMode="MultiLine" ID="txtPolicy" Columns="6" class="inputtext inputtext formsize600"
                            runat="server" Style="height: 36px; padding: 3px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        最后维护人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext inputtext formsize80"></asp:TextBox>
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        维护时间：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLastDate" name="txtLastDate" runat="server" class="inputtext inputtext formsize80"></asp:TextBox>
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
            <span class="formtableT formtableT02">景点</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" class="autoAdd" id="Spot_tbl">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td width="25" height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            编号
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            景点名称
                        </td>
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            景点星级
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            挂牌价格
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            散客价
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            团队价
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            儿童价
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            60-70老人价
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            70以上老人价
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            学生价
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            军人价
                        </td>
                        <td width="105" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            操作
                        </td>
                    </tr>
                    <%if (!(this.SpotListCount > 0))
                      {%>
                    <tbody class="tempRow">
                        <tr>
                            <td bgcolor="#FFFFFF" align="center" rowspan="2">
                                <span class="index">1</span>
                            </td>
                            <td bgcolor="#FFFFFF" align="center" rowspan="2">
                                <input type="text" class="inputtext formsize140" name="SpotName" value="" /><font
                                    class="fontred">*</font>
                                <input type="hidden" name="txtJingDianId" value="" />
                            </td>
                            <td height="28" bgcolor="#FFFFFF" align="center">
                                <select name="SpotStar" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.SpotStar)))%>
                                </select>
                            </td>
                            <td height="28" bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceGP" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceSK" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceTD" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceRT" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceLR1" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceLR2" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceXS" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center">
                                <input type="text" class="inputtext formsize40" name="PriceJR" value="" />
                            </td>
                            <td bgcolor="#FFFFFF" align="center" rowspan="2">
                                <a class="addscenic" id="spot_add" href="javascript:void(0)">
                                    <img width="48" alt="" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                        <img alt="" src="/images/delimg.gif" class="delscenic" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td height="23" bgcolor="#B7E0F3" align="right" class="alertboxTableT" colspan="2">
                                景点描述
                            </td>
                            <td bgcolor="#FFFFFF" align="left" colspan="7">
                                <textarea style="height: 36px; padding: 3px;" class="inputtext formsize450" cols="6"
                                    name="SpotDesc" value=""></textarea>
                            </td>
                        </tr>
                    </tbody>
                    <%} %>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tbody class="tempRow">
                                <tr>
                                    <td bgcolor="#FFFFFF" align="center" rowspan="2">
                                        <span class="index">
                                            <%#Container.ItemIndex+1 %></span>
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center" rowspan="2">
                                        <input type="text" class="inputtext formsize140" name="SpotName" value="<%#Eval("SpotName") %>" /><font
                                            class="fontred">*</font>
                                        <input type="hidden" name="txtJingDianId" value="<%#Eval("Id") %>" />
                                    </td>
                                    <td height="28" bgcolor="#FFFFFF" align="center">
                                        <select name="SpotStar" class="inputselect">
                                            <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.SpotStar)), (int)((EyouSoft.Model.EnumType.SourceStructure.SpotStar)(Eval("Star"))))%>
                                        </select>
                                    </td>
                                    <td height="28" bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceGP" value="<%#GetPrice(Eval("PriceGP")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceSK" value="<%#GetPrice(Eval("PriceSK")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceTD" value="<%#GetPrice(Eval("PriceTD")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceRT" value="<%#GetPrice(Eval("PriceRT")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceLR1" value="<%#GetPrice(Eval("PriceLR1")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceLR2" value="<%#GetPrice(Eval("PriceLR2")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceXS" value="<%#GetPrice(Eval("PriceXS")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center">
                                        <input type="text" class="inputtext formsize40" name="PriceJR" value="<%#GetPrice(Eval("PriceJR")) %>" />
                                    </td>
                                    <td bgcolor="#FFFFFF" align="center" rowspan="2">
                                        <a class="addscenic" id="spot_add" href="javascript:void(0)">
                                            <img width="48" alt="" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                                <img alt="" src="/images/delimg.gif" class="delscenic" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="23" bgcolor="#B7E0F3" align="right" class="alertboxTableT" colspan="2">
                                        景点描述
                                    </td>
                                    <td bgcolor="#FFFFFF" align="left" colspan="7">
                                        <textarea style="height: 36px; padding: 3px;" class="inputtext formsize450" cols="6"
                                            name="SpotDesc" value="<%#Eval("GuideWord") %>"><%#Eval("GuideWord") %></textarea>
                                    </td>
                                </tr>
                            </tbody>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSave" runat="server"><s class="baochun">
            </s>保 存</a><a hidefocus="true" href="javascript:void(0);" onclick="ScenicEditPage.close()"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>
</body>
</html>

<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

<script type="text/javascript">
    if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
        $("#<%=btnSave.ClientID %>").hide();
    }
    var imgstr = "";
    $(function() {
        FV_onBlur.initValid($("#<%=form1.ClientID %>"));
        $(".img").each(function() {
            imgstr += (this).src + "|" + $(this).attr("rel") + "&";
        })
        $("#Agreement").val($("#lbFiles").find("a").attr("href"));
        $("#imgcount").val(imgstr);
        $("#Spot_tbl").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addscenic", delButtonClass: "delscenic", indexClass: "index" })
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
        $("#tablist").find("img[data-delimg='delimg']").click(function() {
            var thisimg = $(this).parent();
            tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
                thisimg.prev("img").hide();
                thisimg.find("input[type='hidden']").val("");
                thisimg.hide();
            });
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
        ScenicEditPage.BindBtn();

    })

    var ScenicEditPage = {
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        ajaxurl: "/ResourceManage/ScenicSpots/ScenicSpotsEdit.aspx",
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
                        tableToolbar._showMsg(ret.msg, function() { ScenicEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { ScenicEditPage.BindBtn() });
                }
            });
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!ScenicEditPage.CheckForm()) {
                    return false;
                }
                ScenicEditPage.GoAjax(ScenicEditPage.ajaxurl + '?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_景点 %>&id=<%=Request.QueryString["id"] %>');
                return false;
            })
            $("#<%=btnSave.ClientID %>").attr("class", "").html("<s class='baochun'></s>保存");
        },
        CheckForm: function() {
            if (!ScenicEditPage.CheckSpot()) {
                return false;
            }
            var form = $("#<%=btnSave.ClientID %>").closest("form").get(0);
            return ValiDatorForm.validator(form, "parent");
        },
        CheckSpot: function() {
            var msgSpot = ScenicEditPage.isCheckingSpot("True", "SpotName", "PriceGP", "PriceTD", "PriceSK", "PriceRT", "PriceLR1", "PriceLR2", "PriceXS", "PriceJR");
            if (msgSpot.isYesSpot) {
                return true;
            } else {
                tableToolbar._showMsg(msgSpot.msgSpot);
                return false;
            }
        },
        isCheckingSpot: function(ischecking, SpotName, PriceGP, PriceTD, PriceSK, PriceRT, PriceLR1, PriceLR2, PriceXS, PriceJR) {
            var isYesSpot = false;
            var msgSpot = "";
            var msgsSpot = "";
            if (ischecking.toString() == "True") {
                msgSpot = ScenicEditPage.checkingSpot(SpotName, PriceGP, PriceTD, PriceSK, PriceRT, PriceLR1, PriceLR2, PriceXS, PriceJR);
                if (msgSpot.length > 0) {
                    isYesSpot = false;
                }
                else {
                    isYesSpot = true;
                }
                msgsSpot = msgSpot;
            }
            else {
                isYesSpot = true;
            }
            msgSpot = { isYesSpot: isYesSpot, msgSpot: msgsSpot }
            return msgSpot;
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        checkingSpot: function(SpotName, PriceGP, PriceTD, PriceSK, PriceRT, PriceLR1, PriceLR2, PriceXS, PriceJR) {
            var msgSpot = "";
            var regseatcount = /\d+\.?\d*/;
            //验证景区名称不能为空
            $("[name=" + SpotName + "]").each(function() {
                if ($.trim($(this).val()) == "") {
                    msgSpot += "*景区名称不能为空! <br />";
                    $(this).focus();
                    return false;
                }
            })
            //验证
            $("[name=" + PriceGP + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*前台价格应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            //验证团队价格格式
            $("[name=" + PriceTD + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*团队价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            $("[name=" + PriceSK + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*散客价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            $("[name=" + PriceRT + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*儿童价应为数字! <br />";
                    $(this).focus();
                    return false;
                }

            })
            $("[name=" + PriceLR1 + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*60到70岁老人价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })

            $("[name=" + PriceLR2 + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*70岁以上老人价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            $("[name=" + PriceXS + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*学生价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            $("[name=" + PriceJR + "]").each(function() {
                if ($(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgSpot += "*军人价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            return msgSpot;
        }
    }
</script>


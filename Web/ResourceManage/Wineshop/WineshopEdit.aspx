<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WineshopEdit.aspx.cs" Inherits="Web.ResourceManage.Wineshop.WineshopEdit"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-酒店-新增</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

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
    <form id="form1" runat="server" enctype="multipart/form-data">
    <input type="hidden" runat="server" id="imgcount" />
    <input type="hidden" runat="server" id="Agreement" />
    <input type="hidden" runat="server" id="StarValue" />
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto" id="tablist">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    国家：
                </td>
                <td width="15%" align="left">
                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="inputselect" errmsg="请选择国家!"
                        valid="required">
                    </asp:DropDownList>
                    <font class="fontbsize12">*</font>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    省份：
                </td>
                <td width="15%" align="left">
                    <asp:DropDownList ID="ddlProvice" runat="server" CssClass="inputselect" errmsg="请选择省份!"
                        valid="required">
                    </asp:DropDownList>
                    <font class="fontbsize12">*</font>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    城市：
                </td>
                <td width="15%" align="left">
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="inputselect" errmsg="请选择城市!"
                        valid="required">
                    </asp:DropDownList>
                    <font class="fontbsize12">*</font>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    县区：
                </td>
                <td width="15%" align="left">
                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="inputselect">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    酒店名称：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtHotelName" MaxLength="30" runat="server" CssClass="inputtext formsize180"
                        errmsg="请输入酒店名称!" valid="required"></asp:TextBox><font class="fontbsize12">*</font>
                </td>
                <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    星级：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <select id="selectStar" name="selectStar" class="inputselect">
                        <asp:Literal ID="litStar" runat="server"></asp:Literal>
                    </select>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    地址：
                </td>
                <td colspan="7" align="left">
                    <asp:TextBox ID="txtaddress" runat="server" height="17px" CssClass="inputtext formsize600" errmsg="请输入地址!"
                        valid="required"></asp:TextBox><font class="fontbsize12">*</font>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    酒店图片：
                </td>
                <td colspan="7" align="left">
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
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    酒店简介：
                </td>
                <td height="43" colspan="7" align="left">
                    <asp:TextBox TextMode="MultiLine" ID="txtHotelDesc" class="inputtext formsize600"
                        runat="server" Style="height: 30px; padding: 3px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td height="43" colspan="7" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox TextMode="MultiLine" ID="txtRemarks" class="inputtext formsize600" runat="server"
                        Style="height: 30px; padding: 3px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
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
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
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
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    返利：
                </td>
                <td align="left">
                    <asp:RadioButton ID="RadioButton1" runat="server" name="Rebateradgroup" Style="border: none"
                        GroupName="Rebateradgroup" />
                    有
                    <asp:RadioButton ID="RadioButton2" runat="server" name="Rebateradgroup" Style="border: none"
                        GroupName="Rebateradgroup" Checked="true" />
                    无
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    是否签订合同：
                </td>
                <td bgcolor="#E0E9EF" align="left">
                    <asp:RadioButton ID="Radio_hd_yes" runat="server" name="Radio_hd_yes" Style="border: none"
                        GroupName="Rad_hd" />
                    是
                    <label id="lbContract" style="display: none">
                        合同号：<asp:TextBox ID="txtContractNum" runat="server" Class="inputtext formsize120"></asp:TextBox></label>
                    <asp:RadioButton ID="Radio_hd_no" runat="server" name="Radio_hd_no" Style="border: none"
                        GroupName="Rad_hd" Checked="true" />
                    否
                </td>
                <td width="13%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    合同期限：
                </td>
                <td height="28" bgcolor="#E0E9EF" align="left">
                    <asp:TextBox ID="txtContractDate_Start" onfocus="WdatePicker({minDate:'%y-%M-{%d}'})"
                        name="txtContractDate_Start" runat="server" Class="inputtext formsize120"></asp:TextBox>-<asp:TextBox
                            ID="txtContractDate" name="txtContractDate" runat="server" Class="inputtext formsize120"
                            onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtContractDate_Start\')}'})"></asp:TextBox>
                </td>
                <td height="28" bgcolor="#B7E0F3" align="right">
                    <span class="alertboxTableT">合同附件：</span>
                </td>
                <td height="28" bgcolor="#E0E9EF" align="left" colspan="3">
                    <uc2:UploadControl ID="UploadControl2" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label ID="lbFiles" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                    总台电话：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtTel" name="txtTel" valid="isTelephone" errmsg="公司电话格式错误!" runat="server"
                        Class="inputtext formsize80"></asp:TextBox>
                </td>
                <td bgcolor="#B7E0F3" align="right">
                    <span class="alertboxTableT">最后维护人：</span>
                </td>
                <td height="28" align="left">
                    <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext formsize80"></asp:TextBox>
                </td>
                <td height="28" bgcolor="#B7E0F3" align="right">
                    维护时间：
                </td>
                <td height="28" align="left" colspan="3">
                    <asp:TextBox ID="txtLastDate" name="txtLastDate" runat="server" class="inputtext formsize80"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <uc1:Contact ID="Contact1" runat="server" />
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">价格</span>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" id="hotel_tbl">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="23" rowspan="2" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        编号
                    </td>
                    <td height="23" rowspan="2" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        房型
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        前台销售价
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        网络价
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        散客价
                    </td>
                    <td height="23" colspan="3" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        团队价
                    </td>
                    <td align="center" rowspan="2" bgcolor="#B9DBEB" class="alertboxTableT">
                        含早
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <tr>
                    <td height="20" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        淡季
                    </td>
                    <td height="20" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        旺季
                    </td>
                    <td height="20" align="center" bgcolor="#B9DBEB" class="alertboxTableT">
                        平季
                    </td>
                </tr>
                <%if (!(this.SetListCount > 0))
                  {%>
                <tr class="tempRow">
                    <td height="28" align="center" bgcolor="#FFFFFF">
                        <span class="index">1</span>
                    </td>
                    <td height="28" align="center" bgcolor="#FFFFFF">
                        <input name="TypeName" type="text" class="inputtext formsize100" /><font class="fontbsize12">*</font>
                        <input type="hidden" name="txtRoomId" value="" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="PriceQT" type="text" class="inputtext formsize50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="PriceWL" type="text" class="inputtext formsize50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="PriceSK" type="text" class="inputtext formsize50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="PriceDJ" type="text" class="inputtext formsize50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="PriceWJ" type="text" class="inputtext formsize50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="PricePJ" type="text" class="inputtext formsize50" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <select name="IsBreakFast" class="inputselect">
                            <option value="1">是</option>
                            <option value="0">否</option>
                        </select>
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <a class="addhotel" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                <img src="/images/delimg.gif" class="delhotel" /></a>
                    </td>
                </tr>
                <%}%>
                <asp:Repeater ID="rptList" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <span class="index">
                                    <%#Container.ItemIndex + 1 %></span>
                            </td>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <input name="TypeName" type="text" class="inputtext formsize100" value="<%#Eval("TypeName") %>" /><font
                                    class="fontbsize12">*</font>
                                <input type="hidden" name="txtRoomId" value="<%#Eval("RoomId") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="PriceQT" type="text" class="inputtext formsize50" value="<%#GetPrice(Eval("PriceQT"))%>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="PriceWL" type="text" class="inputtext formsize50" value="<%#GetPrice(Eval("PriceWL"))%>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="PriceSK" type="text" class="inputtext formsize50" value="<%#GetPrice(Eval("PriceSK"))%>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="PriceDJ" type="text" class="inputtext formsize50" value="<%#GetPrice(Eval("PriceDJ"))%>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="PriceWJ" type="text" class="inputtext formsize50" value="<%#GetPrice(Eval("PriceWJ"))%>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input name="PricePJ" type="text" class="inputtext formsize50" value="<%#GetPrice(Eval("PricePJ"))%>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <select name="IsBreakFast" class="inputselect">
                                    <option value="1" <%#Convert.ToBoolean(Eval("IsBreakFast"))==true?"selected='selected'":"" %>>
                                        是</option>
                                    <option value="0" <%#Convert.ToBoolean(Eval("IsBreakFast"))==false?"selected='selected'":"" %>>
                                        否</option>
                                </select>
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <a class="addhotel" href="javascript:void(0)">
                                    <img width="48" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                        <img src="/images/delimg.gif" class="delhotel" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <a id="btnSave" href="javascript:void(0);" runat="server"><s class="baochun"></s>保 存</a>
            <a hidefocus="true" href="javascript:void(0);" onclick="Controlwine.close()"><s class="chongzhi">
            </s>关 闭</a>
        </div>
    </div>
    <input type="hidden" runat="server" id="hidSourceID" />
    </form>
</body>
</html>

<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

<script type="text/javascript">
    if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
        $("#<%=btnSave.ClientID %>").hide();
    }
    $(function() {
        Controlwine.PageInit();

    })

    var Controlwine = {
        ajaxurl: "/ResourceManage/Wineshop/WineshopEdit.aspx",
        imgstr: null,
        close: function() { parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide() },
        CheckForm: function() {
            if (!Controlwine.Checkwine()) {
                return false;
            }
            var form = $("#<%=btnSave.ClientID %>").closest("form").get(0);

            return ValiDatorForm.validator(form, "parent");
        },
        Checkwine: function() {
            var msgwine = Controlwine.isCheckingwine("True", "TypeName", "PriceQT", "PriceWL", "PriceSK", "PriceDJ", "PriceWJ", "PricePJ");
            if (msgwine.isYeswine) {
                return true;
            } else {
                tableToolbar._showMsg(msgwine.msgwine);
                return false;
            }
        },
        isCheckingwine: function(ischecking, TypeName, PriceQT, PriceWL, PriceSK, PriceDJ, PriceWJ, PricePJ) {
            var isYeswine = false;
            var msgwine = "";
            var msgswine = "";
            if (ischecking.toString() == "True") {
                msgwine = Controlwine.checkingwine(TypeName, PriceQT, PriceWL, PriceSK, PriceDJ, PriceWJ, PricePJ);
                if (msgwine.length > 0) {
                    isYeswine = false;
                }
                else {
                    isYeswine = true;
                }
                msgswine = msgwine;
            }
            else {
                isYeswine = true;
            }
            msgwine = { isYeswine: isYeswine, msgwine: msgswine }
            return msgwine;
        },
        checkingwine: function(TypeName, PriceQT, PriceWL, PriceSK, PriceDJ, PriceWJ, PricePJ) {
            var msgwine = "";
            var regseatcount = /\d+\.?\d*/;
            //验证房型不能为空
            $("[name=" + TypeName + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $.trim($(this).val()) == "") {
                    msgwine += "*房型不能为空! <br />";
                    $(this).focus();
                    return false;
                }
            })
            //验证座位数不能为空
            $("[name=" + PriceQT + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgwine += "*前台价格应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            //验证电话格式
            $("[name=" + PriceWL + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgwine += "*网络价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            $("[name=" + PriceSK + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgwine += "*散客价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            $("[name=" + PriceDJ + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgwine += "*淡季价应为数字! <br />";
                    $(this).focus();
                    return false;
                }

            })
            $("[name=" + PriceWJ + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgwine += "*旺季价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })

            $("[name=" + PricePJ + "]").each(function() {
                if ($(this).parent().parent().hasClass("tempRow") && $(this).val() != "" && !regseatcount.exec(($(this).val()))) {
                    msgwine += "*平季价应为数字! <br />";
                    $(this).focus();
                    return false;
                }
            })
            return msgwine;
        },
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
            $("#hotel_tbl").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addhotel", delButtonClass: "delhotel", indexClass: "index" })
            var checkvalues = "";
            $("#selectStar").change(function() {
                $("#<%=StarValue.ClientID %>").val($(this).val());
            })

            $("#tablist").find("img[data-delimg='delimg']").click(function() {
                var thisimg = $(this).parent();
                thisimg.prev("img").hide();
                thisimg.find("input[type='hidden']").val("");
                thisimg.hide();
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
            Controlwine.BindBtn();
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
                        tableToolbar._showMsg(ret.msg, function() { Controlwine.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { location.reload(); });
                }
            });
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").unbind("click").click(function() {
                if (!Controlwine.CheckForm()) {
                    return false;
                }
                Controlwine.GoAjax(Controlwine.ajaxurl + '?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_酒店 %>&id=<%=Request.QueryString["id"]%>');
                return false;
            })
            $("#<%=btnSave.ClientID %>").attr("class", "").html("<s class='baochun'></s>保存");
        }
    }


</script>


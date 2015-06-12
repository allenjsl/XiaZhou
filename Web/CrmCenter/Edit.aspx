<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Web.CrmCenter.Edit"
    EnableEventValidation="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>客户资料添加修改</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: none;">
    <div class="alertbox-outbox">
        <form id="form1" method="post" runat="server">
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">基本信息</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        国家：
                    </td>
                    <td width="23%" align="left">
                        <asp:DropDownList ID="ddlCountry" name="ddlCountry" valid="required|RegInteger" errmsg="请选择国家|请选择国家"
                            runat="server" CssClass="inputselect" />
                        <font color="red">*</font>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        省份：
                    </td>
                    <td style="width:23%">
                        <asp:DropDownList ID="ddlProvice" name="ddlProvice" valid="required|RegInteger" errmsg="请选择省份|请选择省份"
                            runat="server" CssClass="inputselect" />
                        <font color="red">*</font>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        城市：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCity" name="ddlCity" valid="required|RegInteger" errmsg="请选择城市|请选择城市"
                            runat="server" CssClass="inputselect" />
                        <font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        县区：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:DropDownList ID="ddlCounty" name="ddlCounty" runat="server" CssClass="inputselect" />
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单位名称：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtName" name="txtName" runat="server" valid="required" errmsg="请填写单位名称"
                            CssClass="formsize180 inputtext" MaxLength="50" />
                        <font color="red">*</font>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        地址：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtAddress" name="txtAddress" runat="server" Style="width: 220px;"
                            CssClass="inputtext" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        机构代码：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtOrganizationCode" name="txtOrganizationCode" runat="server" CssClass="formsize180 inputtext"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        客户等级：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlLevId" name="ddlLevId" valid="isPIntegers" errmsg="请选择客户等级"
                            runat="server" CssClass="inputselect" />
                        <font color="red">*</font>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        法人：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLegalRepresentative" name="txtLegalRepresentative" runat="server"
                            CssClass="formsize180 inputtext" MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        法人电话：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtLegalRepresentativePhone" name="txtLegalRepresentativePhone"
                            valid="isPhone" errmsg="请填写正确的法人代表电话" runat="server" CssClass="formsize180 inputtext"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        法人手机：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtLegalRepresentativeMobile" name="txtLegalRepresentativeMobile"
                            valid="isMobile" errmsg="请填写正确的法人代表手机" runat="server" CssClass="formsize180 inputtext"
                            MaxLength="20"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        许可证号：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtLicense" name="txtLicense" runat="server" CssClass="formsize180 inputtext"
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                
                <%--<tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        返利政策：
                    </td>
                    <td colspan="5" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtRebatePolicy" Wrap="true" TextMode="MultiLine" name="txtRebatePolicy"
                            runat="server" CssClass="formsize600 inputtext" Style="margin-top: 3px; margin-bottom: 3px;
                            height: 89px; width: 680px;"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        责任销售：
                    </td>
                    <td align="left">
                        <Uc1:Seller ID="Seller1" runat="server" CssClass="formsize180 inputtext"  />
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        简码：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtBrevityCode" name="txtBrevityCode" runat="server" CssClass="formsize180 inputtext"
                            MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">常用设置</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        欠款额度：
                    </td>
                    <td width="23%" align="left">
                        <asp:TextBox ID="txtAmountOwed" name="txtAmountOwed" runat="server" CssClass="formsize180 inputtext"
                            Text="5000" MaxLength="10" />
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单团账龄期限：
                    </td>
                    <td  align="left">
                        <asp:TextBox ID="txtDeadline" valid="RegInteger" errmsg="请填写单团账龄期限" name="txtDeadline"
                            runat="server" CssClass="formsize180 inputtext" Text="10" MaxLength="3" />
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        是否签订协议：
                    </td>
                    <td align="left" bgcolor="#e0e9ef">
                        <asp:RadioButton ID="rbtnIsSignContractYes" GroupName="IsSignContract" Style="border: none;"
                            runat="server" Text="是" />
                        <asp:RadioButton ID="rbtnIsSignContractNo" GroupName="IsSignContract" Style="border: none;"
                            runat="server" Text="否" />
                    </td>
                    <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        协议文件上传：
                    </td>
                    <td bgcolor="#e0e9ef">
                        <uc2:UploadControl ID="UploadControl1" runat="server" />
                        <asp:Label ID="lblFile" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div class="hr_5"></div>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        财务姓名：
                    </td>
                    <td width="23%" align="left">
                        <asp:TextBox ID="txtFinancialName" name="txtFinancialName" runat="server" CssClass="formsize180 inputtext"
                            MaxLength="10"></asp:TextBox>
                    </td>
                    <td width="10%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        财务电话：
                    </td>
                    <td width="23%" align="left">
                        <asp:TextBox ID="txtFinancialPhone" valid="isPhone" errmsg="请填写正确的财务电话" name="txtFinancialPhone"
                            runat="server" CssClass="formsize180 inputtext" MaxLength="20"></asp:TextBox>
                    </td>
                    <td width="10%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        财务手机：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFinancialMobile" valid="isMobile" errmsg="请填写正确的财务手机" name="txtFinancialMobile"
                            runat="server" CssClass="formsize180 inputtext" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
            </table>            
            <div class="hr_5"></div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td height="28" align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width:43%">
                        开户行
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" >
                        银行账号
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <asp:Repeater ID="rptJSZHList" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <input type="hidden" value='<%# Eval("BankId") %>' name="hidBankId" /><input type="text"
                                    name="txtBankName" <%# !IsSetChangYong?"readonly='readonly'":"" %> class="formsize180 inputtext"
                                    value='<%# Eval("BankName") %>' maxlength="50" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" name="txtBankAccount" <%# !IsSetChangYong?"readonly='readonly'":"" %>
                                    class="formsize180 inputtext" value='<%# Eval("BankAccount") %>' maxlength="50" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <asp:PlaceHolder runat="server" Visible='<%#IsSetChangYong%>'><a href="javascript:void(0)"
                                    class="addbtn">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delbtn">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a> </asp:PlaceHolder>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">联系人</span><span style="color: #666">&nbsp;&nbsp;注：至少填写一个联系人信息，不填写部门的联系人信息将视为无效数据。</span>
            <table id="tab_lxr" width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td width="30" align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="height:25px;">
                        编号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        部门<font color="red">*</font>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        地址
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        联系人
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        电话
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        传真
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        手机
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        生日
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        QQ
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <asp:Repeater ID="rptCYLXRList" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td style="height: 25px; text-align: center;">
                                <span class="selector_index">
                                    <%# (Container.ItemIndex + 1)%></span><input type="hidden" value='<%# Eval("Id") %>'
                                        name="hidlLinkManId" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlDepartment" style="width: 60px" class="inputtext" value='<%# Eval("Department") %>'
                                    maxlength="20" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlAddress" class="formsize140 inputtext" value='<%# Eval("Address") %>'
                                    maxlength="255" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtllinkManName" class="formsize40 inputtext" value='<%# Eval("Name") %>'
                                    maxlength="10" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlTel" valid="isPhone" errmsg="请填写正确的联系电话" class="formsize80 inputtext"
                                    value='<%# Eval("Telephone") %>' maxlength="20" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlFax" class="formsize80 inputtext" value='<%# Eval("Fax") %>'
                                    maxlength="20" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlMobilePhone" class="formsize80 inputtext" value='<%# Eval("MobilePhone") %>'
                                    maxlength="20" valid="isMobile" errmsg="请填写正确的手机号码" />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlBirthday" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'});"
                                    valid="isDate" errmsg="请填写正确的生日" class="inputtext" value='<%# EyouSoft.Common.UtilsCommons.SetDateTimeFormart((DateTime?)Eval("Birthday")) %>' style="width:65px;" />
                                <label>
                                    <input type="checkbox" name="chkIsTiXing" <%# (bool)(Eval("IsRemind"))?"checked=\"checked\"":"" %> />提醒</label>
                            </td>
                            <td align="center" class="fontblue">
                                <input type="text" name="txtlQQ" class="formsize80 inputtext" value='<%# Eval("QQ") %>'
                                    maxlength="20" valid="isQQ" errmsg="请输入正确的QQ号码" />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="addbtn">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a>&nbsp; <a href="javascript:void(0)"
                                        class="delbtn">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <asp:PlaceHolder ID="phdSave" runat="server"><a id="btnSave" href="javascript:void(0);">
                保 存</a></asp:PlaceHolder>
            <asp:PlaceHolder ID="phChangYongSheZhi" runat="server" Visible="false">
                <a id="i_a_changyongshezhi" href="javascript:void(0);"><s class='baochun'></s>常用设置</a>
            </asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="return CustomerEdit.closeWindow();" hidefocus="true">
                <s class="chongzhi"></s>关闭</a></div>
        </form>
    </div>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">

        var CustomerEdit = {
            //查询参数
            winParams: {}
            //删除已上传合作协议附件
            , delLatestAttach: function() {
                $("#spanLatestAttach").remove();
            }
            //重新设置联系人索引
            , reSetLianXiRenIndex: function() {
                $("#tab_lxr span.selector_index").each(function(i, n) {
                    $(this).html(i + 1);
                });
                //$(window).scrollTop(3000); 
                //需要设置滚动条位置
            }
            //绑定保存按钮事件
            , bindSaveBtn: function() {
                $("#btnSave").bind("click", function() { CustomerEdit.submit(false); });
                $("#btnSave").html("<s class='baochun'></s>保存").attr("class", "");
            }
            //取消保存按钮事件
            , unBindSaveBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").text("保存中....");
            }
            //自定义验证
            , selfValidator: function() {
                var _v = { "result": 0, msg: "" };

                //部门验证：至少填写一个部门，部门名称重复验证
                var _obj = {};
                var _has = false;
                var _exists = [];

                $("#tab_lxr input[name='txtlDepartment']").each(function() {
                    var dept = $.trim($(this).val());
                    if (dept.length == 0) return;

                    if (!_obj[dept]) _obj[dept] = 1;
                    else _obj[dept] = _obj[dept] + 1;
                });

                for (var item in _obj) {
                    if (_obj[item] > 1) {
                        _exists.push(item);
                    }

                    _has = true;
                }

                if (!_has) {
                    _v.result = -1;
                    _v.msg = "至少要填写一个联系人的部门信息！";

                    return _v;
                }

                if (_exists.length > 0) {
                    _v.result = -2;
                    _v.msg = "部门" + _exists.join(",") + "重复";

                    return _v;
                }

                //客户单位名称重复验证
                var params = { "doType": "isexists", "sl": this.winParams["sl"] };
                $.newAjax({
                    type: "post"
                    , cache: false
                    , url: Boxy.createUri("Edit.aspx", params)
                    , dataType: "json"
                    , data: { "crmname": $("#<%=txtName.ClientID %>").val(), "crmid": '<%=Utils.GetQueryStringValue("crmId") %>' }
                    , async: false
                    , success: function(ret) {
                        if (ret.result == "0") {
                            _v.result = -3;
                            _v.msg = "客户单位名称重复";
                        }
                    }
                });

                if (_v.result < 0) return _v;

                _v.result = 1;

                return _v;
            }
            //提交表单
            , submit: function(_issubmit) {
                var _self = this;
                var _win = top || window;
                this.unBindSaveBtn();

                if (!_issubmit) {
                    var validatorResult = ValiDatorForm.validator($("#btnSave").closest("form").get(0), "alert");

                    if (!validatorResult) {
                        this.bindSaveBtn();
                        return false;
                    }

                    var validatorResult = this.selfValidator();

                    if (validatorResult.result == -1) {
                        parent.tableToolbar._showMsg(validatorResult.msg);
                        this.bindSaveBtn();
                        return false;
                    }

                    if (validatorResult.result == -2) {
                        parent.tableToolbar.ShowConfirmMsg(validatorResult.msg + "，确定要保存吗？", function() {
                            CustomerEdit.submit(true);
                        })
                        this.bindSaveBtn();
                        return false;
                    }

                    if (validatorResult.result == -3) {
                        parent.tableToolbar.ShowConfirmMsg("您填写的客户单位名称在系统中已经存在，确定要保存吗？", function() {
                            CustomerEdit.submit(true);
                        })
                        this.bindSaveBtn();
                        return false;
                    }
                }

                if (!_issubmit) {
                    var sfu1 = window["<%=UploadControl1.ClientID %>"];

                    if (!sfu1) {
                        //CustomerEdit.submit(true);
                    } else {
                        if (sfu1.getStats().files_queued > 0) {
                            sfu1.customSettings.UploadSucessCallback = function() {
                                CustomerEdit.submit(true);
                            };
                            sfu1.startUpload();
                            return false;
                        }
                        else {
                            CustomerEdit.submit(true);
                            return false;
                        }
                    }
                }

                //是否生日提醒checkbox处理
                $("#tab_lxr input[name='chkIsTiXing']").each(function() {
                    $(this).after('<input type="hidden" name="txtIsTiXing" value="' + (this.checked ? "1" : "0") + '" />');
                });

                var params = { "doType": "save", "sl": this.winParams["sl"], "crmid": '<%=Request.QueryString["CrmId"]%>' }

                $.newAjax({
                    type: "post",
                    cache: false,
                    url: Boxy.createUri("Edit.aspx", params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            var _reloadwindow = parent;
                            parent.tableToolbar._showMsg("操作成功", function() {
                                if ('<%=Request.QueryString["pIframeID"] %>' != '') {
                                    var addCallBack = _self.winParams["callbackfun"], addPageWin = _reloadwindow.Boxy.getIframeWindow(_self.winParams["pIframeID"]);
                                    if (addPageWin[addCallBack] != null && typeof addPageWin[addCallBack] == 'function') {
                                        addPageWin[addCallBack]();
                                    }
                                    _self.closeWindow();
                                } else {
                                    _reloadwindow.location.href = _reloadwindow.location.href;
                                }

                            })

                        } else {
                            parent.tableToolbar._showMsg("操作失败！");
                            this.bindSaveBtn();
                        }
                    }
                });
            }
            //关闭窗口
            , closeWindow: function() {
                var _win = top || window;
                _win.Boxy.getIframeDialog(this.winParams["iframeId"]).hide();
                return false;
            }
            , bindSaveChangYongSheZhiBtn: function() {
                $("#i_a_changyongshezhi").bind("click", function() { CustomerEdit.saveChangYongSheZhi(false); });
                $("#i_a_changyongshezhi").html("<s class='baochun'></s>&nbsp;&nbsp;常用设置").attr("class", "");
            }
            , unBindSaveChangYongSheZhiBtn: function() {
                $("#i_a_changyongshezhi").unbind("click").text("保存中....");
            }
            //保存常用设置
            , saveChangYongSheZhi: function(_issubmit) {
                if (!_issubmit) if (!confirm("你确定要保存常用设置信息吗？")) return;
                var _self = this;
                var _win = top || window;
                _self.unBindSaveChangYongSheZhiBtn();

                if (!_issubmit) {
                    var sfu1 = window["<%=UploadControl1.ClientID %>"];

                    if (!sfu1) {
                        //CustomerEdit.submit(true);
                    } else {
                        if (sfu1.getStats().files_queued > 0) {
                            sfu1.customSettings.UploadSucessCallback = function() {
                                CustomerEdit.saveChangYongSheZhi(true);
                            };
                            sfu1.startUpload();
                            return false;
                        }
                        else {
                            CustomerEdit.saveChangYongSheZhi(true);
                            return false;
                        }
                    }
                }

                var params = { "doType": "savechangyongshezhi", "sl": this.winParams["sl"], "crmid": '<%=Request.QueryString["CrmId"]%>' }

                $.newAjax({
                    type: "post",
                    cache: false,
                    url: Boxy.createUri("Edit.aspx", params),
                    data: $("#i_a_changyongshezhi").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            var _reloadwindow = parent;
                            parent.tableToolbar._showMsg("常用设置保存成功", function() {
                                if ('<%=Request.QueryString["pIframeID"] %>' != '') {
                                    var addCallBack = _self.winParams["callbackfun"], addPageWin = _reloadwindow.Boxy.getIframeWindow(_self.winParams["pIframeID"]);
                                    if (addPageWin[addCallBack] != null && typeof addPageWin[addCallBack] == 'function') {
                                        addPageWin[addCallBack]();
                                    }
                                    _self.closeWindow();
                                } else {
                                    _reloadwindow.location.href = _reloadwindow.location.href;
                                }

                            })

                        } else {
                            parent.tableToolbar._showMsg("常用设置保存失败！");
                            _self.bindSaveChangYongSheZhiBtn();
                        }
                    }
                });
            }
            //页面加载
            , init: function() {
                this.winParams = Boxy.getUrlParams();
                this.winParams["reloadiframeid"] = this.winParams["reloadiframeid"] || "";

                $("#tab_lxr").autoAdd({ addCallBack: CustomerEdit.reSetLianXiRenIndex, delCallBack: CustomerEdit.reSetLianXiRenIndex });

                pcToobar.init({
                    gID: "#<%=ddlCountry.ClientID %>",
                    pID: "#<%=ddlProvice.ClientID %>",
                    cID: "#<%=ddlCity.ClientID %>",
                    xID: "#<%=ddlCounty.ClientID %>",
                    gSelect: '<%=this.Country %>',
                    pSelect: '<%=this.Province %>',
                    cSelect: '<%=this.City %>',
                    xSelect: '<%=this.County %>',
                    comID: '<%=this.SiteUserInfo.CompanyId %>'
                });

                $("input[readonly='readonly']").css({ "background-color": "#dadada" });

                this.bindSaveBtn();
                this.bindSaveChangYongSheZhiBtn();
            }
        };

        $(document).ready(function() {
            CustomerEdit.init();
        });
    </script>

</body>
</html>

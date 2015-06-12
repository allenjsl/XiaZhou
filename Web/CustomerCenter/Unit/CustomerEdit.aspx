<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="Web.CustomerCenter.Unit.CustomerEdit"
    EnableEventValidation="false" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
</head>
<body style="background: none;">
    <div class="alertbox-outbox">
        <form id="form1" method="post" runat="server">
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">基本信息</span>
            <table width="100%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        国家：
                    </td>
                    <td width="23%" align="left">
                        <%-- <select id="select3" name="select3">
                            <option value="0" selected="selected">选择国家</option>
                            <option value="1274">中国</option>
                            <option value="1275">美国</option>
                            <option value="1276">日本</option>
                            <option value="1277">澳大利亚</option>
                            <option value="1278">西班牙</option>
                        </select>--%>
                        <asp:DropDownList ID="ddlCountry" name="ddlCountry" valid="required|RegInteger" errmsg="请选择国家|请选择国家"
                            runat="server">
                        </asp:DropDownList><font color="red">*</font>
                    </td>
                    <td width="9%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        省份：
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProvice" name="ddlProvice" valid="required|RegInteger" errmsg="请选择省份|请选择省份"
                            runat="server">
                        </asp:DropDownList><font color="red">*</font>
                    </td>
                    <td width="9%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        城市：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlCity" name="ddlCity" valid="required|RegInteger" errmsg="请选择城市|请选择城市"
                            runat="server">
                        </asp:DropDownList><font color="red">*</font>
                    </td>
                </tr>
                <tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        县区：
                    </td>
                    <td width="23%" align="left" bgcolor="#e0e9ef">
                        <%--<select id="select2" name="select2">
                            <option value="0" selected="selected">选择县区</option>
                        </select>--%>
                        <asp:DropDownList ID="ddlCounty" name="ddlCounty" valid="required|RegInteger" errmsg="请选择县区|请选择县区"
                            runat="server">
                        </asp:DropDownList><font color="red">*</font>
                    </td>
                    <td width="9%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单位名称：
                    </td>
                    <td width="23%" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtName" name="txtName" runat="server" valid="required" errmsg="请填写单位名称"
                            CssClass="formsize180"></asp:TextBox><font color="red">*</font>
                    </td>
                    <td width="9%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        地址：
                    </td>
                    <td width="27%" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtAddress" name="txtAddress" runat="server" Style="width: 260px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        机构代码：
                    </td>
                    <td width="23%" align="left">
                        <asp:TextBox ID="txtOrganizationCode" name="txtOrganizationCode" runat="server" CssClass="formsize180"></asp:TextBox>
                    </td>
                    <td width="9%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        客户等级：
                    </td>
                    <td width="23%" align="left">
                        <asp:DropDownList ID="ddlLevId" name="ddlLevId" valid="required|RegInteger" errmsg="请选择客户等级|请选择客户等级"
                            runat="server">
                        </asp:DropDownList><font color="red">*</font>
                    </td>
                    <td width="9%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        法人：
                    </td>
                    <td width="27%" align="left">
                        <asp:TextBox ID="txtLegalRepresentative" name="txtLegalRepresentative" runat="server"
                            CssClass="formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        法人电话：
                    </td>
                    <td width="23%" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtLegalRepresentativePhone" name="txtLegalRepresentativePhone"
                            valid="isPhone" errmsg="请填写正确的法人代表电话" runat="server" CssClass="formsize120"></asp:TextBox>
                    </td>
                    <td width="9%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        法人手机：
                    </td>
                    <td width="23%" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtLegalRepresentativeMobile" name="txtLegalRepresentativeMobile"
                            valid="isMobile" errmsg="请填写正确的法人代表手机" runat="server" CssClass="formsize120"></asp:TextBox>
                    </td>
                    <td width="9%" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        许可证号：
                    </td>
                    <td width="27%" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtLicense" name="txtLicense" runat="server" CssClass="formsize180"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        财务姓名：
                    </td>
                    <td width="23%" align="left">
                        <asp:TextBox ID="txtFinancialName" name="txtFinancialName" runat="server" CssClass="formsize120"></asp:TextBox>
                    </td>
                    <td width="9%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        财务电话：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFinancialPhone" valid="isPhone" errmsg="请填写正确的财务电话" name="txtFinancialPhone"
                            runat="server" CssClass="formsize120"></asp:TextBox>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        财务手机：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtFinancialMobile" valid="isMobile" errmsg="请填写正确的财务手机" name="txtFinancialMobile"
                            runat="server" CssClass="formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="9%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        返利政策：
                    </td>
                    <td colspan="5" align="left" bgcolor="#e0e9ef">
                        <asp:TextBox ID="txtRebatePolicy" Wrap="true" TextMode="MultiLine" name="txtRebatePolicy"
                            runat="server" CssClass="formsize600" Style="padding: 3px;" Height="89px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        责任销售：
                    </td>
                    <td align="left">
                        <Uc1:Seller ID="Seller1" runat="server" CssClass="formsize120" />
                        <%if (!this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改责任销售))
                          { %>

                        <script type="text/javascript">
                            $("#Seller1_a_btn").css("display", "none");
                        </script><font color="red">*</font>

                        <%} %>
                    </td>
                    <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                        简码：
                    </td>
                    <td colspan="3" align="left">
                        <asp:TextBox ID="txtBrevityCode" name="txtBrevityCode" runat="server" CssClass="formsize180"></asp:TextBox>
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
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        欠款额度：
                    </td>
                    <td width="25%" align="left">
                        <asp:TextBox ID="txtAmountOwed" name="txtAmountOwed" runat="server" CssClass="formsize80"></asp:TextBox>
                    </td>
                    <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        单团账龄期限：
                    </td>
                    <td width="45%" align="left">
                        <asp:TextBox ID="txtDeadline" valid="RegInteger" errmsg="请填写单团账龄期限" name="txtDeadline"
                            runat="server" CssClass="formsize80"></asp:TextBox>
                        （非必填）
                    </td>
                </tr>
                <tr>
                    <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                        是否签订协议：
                    </td>
                    <td width="25%" align="left" bgcolor="#e0e9ef">
                        <asp:RadioButton ID="rbtnIsSignContractYes" GroupName="IsSignContract" Style="border: none;"
                            runat="server" Text="是" />
                        <asp:RadioButton ID="rbtnIsSignContractNo" GroupName="IsSignContract" Style="border: none;"
                            runat="server" Text="否" />
                        <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            协议文件上传：
                        </td>
                        <td>
                            <uc2:UploadControl ID="UploadControl1" runat="server" />
                            <%--<input type="file" name="fileCooperativeAgreements" id="fileCooperativeAgreements"
                                runat="server" />--%><br />
                            <%if (!string.IsNullOrEmpty(FilePath))
                              {%>
                            <a href="<%=FilePath %>" target="_blank">
                                <%=System.IO.Path.GetFileName(FilePath)%></a>
                            <%} %>
                        </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd">
                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        开户行
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        银行账号
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <asp:Repeater ID="rptJSZHList" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td align="center">
                                <input type="hidden" value='<%# Eval("BankId") %>' name="hidBankId" /><input type="text"
                                    name="txtBankName" <%# GetPower() %> class="formsize80" value='<%# Eval("BankName") %>' />
                            </td>
                            <td align="center">
                                <input type="text" name="txtBankAccount" <%# GetPower() %> class="formsize80" value='<%# Eval("BankAccount") %>' />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" <%# GetBtnPower() %> class="addbtn">
                                    <img src="../../images/addimg.gif" width="48" height="20" /></a><a href="javascript:void(0)"
                                        <%# GetBtnPower() %> class="delbtn"><img src="../../images/delimg.gif" width="48"
                                            height="20" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">联系人</span>
            <table id="tab_lxr" width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd">
                <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                    <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        编号
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        部门
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
                            <td>
                                <%# (Container.ItemIndex + 1)%><input type="hidden" value='<%# Eval("Id") %>' name="hidlLinkManId" /><input
                                    type="hidden" value='<%# Eval("UserId") %>' name="hidlUserId" /><input type="hidden"
                                        value='<%# Eval("IsEnableDelete") %>' name="hidIsEnableDelete" />
                            </td>
                            <td align="center">
                                <input type="text" <%# GetEnable(Eval("IsEnableDelete")) %> name="txtlDepartment"
                                    class="formsize80" value='<%# Eval("Department") %>' />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlAddress" class="formsize80" value='<%# Eval("Address") %>' />
                            </td>
                            <td align="center">
                                <input type="text" name="txtllinkManName" class="formsize80" value='<%# Eval("Name") %>' />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlTel" valid="isPhone" errmsg="请填写正确的联系电话" class="formsize80"
                                    value='<%# Eval("Telephone") %>' />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlFax" class="formsize80" value='<%# Eval("Fax") %>' />
                            </td>
                            <td align="center">
                                <input type="text" name="txtlMobilePhone" class="formsize80" value='<%# Eval("MobilePhone") %>' />
                            </td>
                            <td align="center">
                                <input type="hidden" name="hidIsRemind" value='<%# Eval("IsRemind") %>' /><input
                                    type="text" name="txtlBirthday" onfocus="WdatePicker();" valid="isDate" errmsg="请填写正确的生日"
                                    class="formsize80" value='<%# GetDate(Eval("Birthday")) %>' /><input type="checkbox"
                                        name="lchbIsRemind" <%# GetIsRemind(Eval("IsRemind")) %> /><lable>提醒</lable>
                            </td>
                            <td align="center" class="fontblue">
                                <input type="text" name="txtlQQ" class="formsize80" value='<%# Eval("QQ") %>' />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="addbtn">
                                    <img src="../../images/addimg.gif" width="48" height="20" /></a><a href="javascript:void(0)"
                                        class="delbtn"><img src="../../images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <a class="baochun" id="btnSave" href="javascript:void(0)">保存</a><a href="javascript:void(0);"
                onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                class="close" hidefocus="true"><s></s>返回</a></div>
        </form>
    </div>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkSelect(obj) {
            if (obj.value == "-1") return false; else return true;
        }

        $(function() {

            $("#tab_lxr input[type='checkbox']").each(function(i) {
                $(this).bind("click", function() {

                    if ($(this).attr("checked") == false) {

                        $("#tab_lxr input[name='hidIsRemind']").eq(i).val("");
                    } else {
                        $("#tab_lxr input[name='hidIsRemind']").eq(i).val("true");
                    }

                });

                $("#tab_lxr lable").eq(i).bind("click", function() {
                    $("#tab_lxr input[type='checkbox']").eq(i).attr("checked", "false");
                });
            });

            $("#tab_lxr input[name='hidIsEnableDelete']").each(function(i) {
                var IsEnable = $(this).val() == "False" ? false : true;
                if (IsEnable == false) {
                    $("#tab_lxr .delbtn").eq(i).css("display", "none");
                }
            });
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));
            //$("#Agreement").val($("#lbFiles").find("a").attr("href"));
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
            })
            /*$("#delAgreem").click(function(){
            tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
            $(obj).siblings("#lbFiles").hide()
            $(obj).hide();
            $("#Agreement").val("");
            })

         })*/

            $("#btnSave").click(function() {
                if (CustomerEdit.IsUpload) {
                    CustomerEdit.Save('false');
                    return false;
                }
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "alert")) {
                    if ($("#ddlCountry").val() == "0") {
                        tableToolbar._showMsg("请选择城市!");
                        return false;
                    }

                    if ($("#ddlProvice").val() == "0") {
                        tableToolbar._showMsg("请选择省份!");
                        return false;
                    }
                    if ($("#ddlCity").val() == "0") {
                        tableToolbar._showMsg("请选择城市!");
                        return false;
                    }
                    if ($("#ddlCounty").val() == "0") {
                        tableToolbar._showMsg("请选择县区!");
                        return false;
                    }
                    if ($("#ddlLevId").val() == "0") {
                        tableToolbar._showMsg("请选择客户等级!");
                        return false;
                    }
                    
                    $("#tab_lxr input[name='txtllinkManName']").each(function (i) {
                        if($(this).val()==""||$("#tab_lxr input[name='txtlTel']").eq(i).val()==""||$("#tab_lxr input[name='txtlAddress']").eq(i).val()==""||$("#tab_lxr input[name='txtlFax']").eq(i).val()==""||$("#tab_lxr input[name='txtlMobilePhone']").eq(i).val()==""||$("#tab_lxr input[name='hidIsRemind']").eq(i).val()==""||$("#tab_lxr input[name='txtlBirthday']").eq(i).val()==""||$("#tab_lxr input[name='txtlQQ']").eq(i).val()=="")
                        {
                        
                        }
                        else
                        {
                            if($("#tab_lxr input[name='txtlDepartment']").eq(i).val()=="")
                            {
                                tableToolbar._showMsg("联系人部门不能为空!");
                                CustomerEdit.IsReturn=true;
                                return false;
                            }
                        }
                        if($("#tab_lxr input[name='txtlDepartment']").eq(i).val()!="")
                        {
                            if($(this).val()=="")
                            {
                                tableToolbar._showMsg("联系人名称不能为空!");
                                CustomerEdit.IsReturn=true;
                                return false;
                            }
                        }
                    });
                    if(CustomerEdit.IsReturn)
                    return false;
                    var sfu1 = <%=UploadControl1.ClientID %>;
                    //if (sfu1.getStats().files_queued <= 0) {
                    //                   
                    //                    return  false;                        
                    //                }
                   if (sfu1.getStats().files_queued > 0) {//有图片
                        sfu1.customSettings.UploadSucessCallback = function() {
                            CustomerEdit.IsUpload = true;
                            $("#btnSave").click();
                        };

                        sfu1.startUpload();
                        return false;
                    }
                    else
                    {
                        CustomerEdit.Save('false');
                        return false;
                    }
                }
                else {
                    return false;
                }
            });

            // $("#tab_lxr input[type='checkbox']")



        });


        var CustomerEdit = {
            IsUpload: false,
            IsReturn:false,
            Save: function(isadd) {
                $("#btnSave").text("提交中...");
                //ajax执行文件路径,默认为本页面
                var ajaxUrl = "/CustomerCenter/Unit/CustomerEdit.aspx";

                //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
                ajaxUrl += "?doType=save&sl=" + '<%=Request.QueryString["sl"]%>&CrmId=<%=Request.QueryString["CrmId"]%>&isadd=' + isadd;
                //执行ajax
                CustomerEdit.GoAjax(ajaxUrl)
            },
            //ajax请求
            GoAjax: function(url) {
                $.ajax({
                    type: "post",
                    cache: false,
                    url: url,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(res) {
                        //ajax回发提示
                        if (res.result == "-5") {
                            //tableToolbar.ShowConfirmMsg(res.msg, CustomerEdit.Save("true"));
                            $("#btnSave").text("保存");
                            if (confirm(res.msg + ",是否还继续添加?"))
                                CustomerEdit.Save("true");
                        }
                        else if (res.result == "1") {
                            tableToolbar._showMsg(res.msg, function() { window.parent.location.href = '/CustomerCenter/Unit/CustomerList.aspx?sl=<%=Request.QueryString["sl"]%>' });
                        }
                        else {
                            tableToolbar._showMsg(res.msg);
                            $("#btnSave").text("保存");
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        alert("服务器忙！");
                        $("#btnSave").text("保存");
                    }
                });
            }


        };

        function resetForm() {
            document.getElementById('form1').reset();
        }

        
      
        
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CruiseEdit.aspx.cs" Inherits="Web.ResourceManage.Cruise.CruiseEdit"
    EnableEventValidation="false" %>

<%@ Register Src="../../UserControl/Contact.ascx" TagName="Contact" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源管理-游轮</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

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
            width: 200px;
            overflow: hidden;
        }
        #cruise_tbl .progressName
        {
            overflow: hidden;
            width: 150px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="10%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        国家：
                    </td>
                    <td width="20%" align="left">
                        <asp:DropDownList ID="ddlCountry" runat="server" class="inputselect" errmsg="请选择国家!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="10%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        省份：
                    </td>
                    <td width="20%" align="left">
                        <asp:DropDownList runat="server" ID="ddlProvince" class="inputselect" errmsg="请选择省份!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                    <td width="12%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        城市：
                    </td>
                    <td width="28%" align="left">
                        <asp:DropDownList runat="server" ID="ddlCity" class="inputselect" errmsg="请选择城市!"
                            valid="required">
                        </asp:DropDownList>
                        <font class="fontbsize12">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        县区：
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <asp:DropDownList runat="server" ID="ddlArea" class="inputselect">
                        </asp:DropDownList>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right">
                        <span class="alertboxTableT">游船公司：</span>
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtCruiseCompany" MaxLength="30" runat="server" class="inputtext formsize140"
                            errmsg="请输入游轮公司!" valid="required"></asp:TextBox><font class="fontred">*</font>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right">
                        <span class="alertboxTableT">公司电话：</span>
                    </td>
                    <td height="28" bgcolor="#e0e9ef" align="left">
                        <asp:TextBox ID="txtCruiseTel" runat="server" class="inputtext formsize100" valid="isTelephone"
                            errmsg="公司电话格式错误!"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        地址：
                    </td>
                    <td height="28" align="left" colspan="5">
                        <asp:TextBox ID="txtaddress" runat="server" height="17px" class="inputtext formsize600" valid="required"
                            errmsg="请输入地址!"></asp:TextBox><font class="fontred">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        价格体系：
                    </td>
                    <td width="20%" bgcolor="#E0E9EF" align="left">
                        <asp:TextBox ID="txtPriceSystem" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td width="10%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        开航时间段：
                    </td>
                    <td bgcolor="#E0E9EF" align="left" colspan="3">
                        <asp:TextBox ID="txtstarttime" runat="server" class="inputtext formsize120" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'%y-%M-#{%d}'})"
                            errmsg="请输入开航时间!" valid="required"></asp:TextBox><font class="fontred">*</font>
                        -
                        <asp:TextBox ID="txtendtime" runat="server" class="inputtext formsize120" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss',minDate:'#F{$dp.$D(\'txtstarttime\')}'})"
                            errmsg="请输入返航时间!" valid="required"></asp:TextBox><font class="fontred">*</font>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT" rowspan="2">
                        航线：
                    </td>
                    <td height="28" align="left" colspan="5">
                        游览景点：
                        <asp:TextBox ID="txtRoutes" runat="server" class="inputtext formsize450"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#e0e9ef" align="left" colspan="5">
                        自费景点：
                        <asp:TextBox ID="txtOwerRoutes" runat="server" class="inputtext formsize450"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返利：
                    </td>
                    <td height="28" align="left">
                        <asp:RadioButton ID="radyes" runat="server" name="radgroup" Style="border: none"
                            GroupName="radgroup" />
                        是
                        <asp:RadioButton ID="radno" Checked="true" runat="server" name="radgroup" Style="border: none"
                            GroupName="radgroup" />
                        否
                    </td>
                    <td width="10%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否推荐：
                    </td>
                    <td height="28" align="left">
                        <asp:RadioButton ID="RadRecommend_yes" runat="server" name="RadRecommend_yes" Style="border: none"
                            GroupName="radgroupRecom" />
                        是
                        <asp:RadioButton ID="RadRecommend_no" Checked="true" runat="server" name="RadRecommend_no"
                            Style="border: none" GroupName="radgroupRecom" />
                        否
                    </td>
                    <td width="12%" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        是否签单：
                    </td>
                    <td height="28" align="left">
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
                        &nbsp;<uc2:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false"
                            IsUploadSelf="true" />
                        <br />
                        <asp:Label ID="lbFiles" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        返点政策描述：
                    </td>
                    <td height="45" align="left" colspan="5">
                        <asp:TextBox runat="server" ID="txtUnitPolicy" TextMode="MultiLine" Style="height: 30px;
                            padding: 3px" class="inputtext formsize600"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontred"></font>最后维护人：
                    </td>
                    <td bgcolor="#E0E9EF" align="left">
                        <asp:TextBox ID="txtLastHuman" name="txtLastHuman" runat="server" class="inputtext formsize80"></asp:TextBox>
                    </td>
                    <td bgcolor="#B7E0F3" align="right">
                        维护时间：
                    </td>
                    <td bgcolor="#E0E9EF" align="left" colspan="3">
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
            <span class="formtableT formtableT02">旗下游轮</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0" id="cruise_tbl">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td width="25" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            编号
                        </td>
                        <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            船名
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            星级
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            舱位数
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            船载电话
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            船载联系人
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            航线
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            游船图片附件
                        </td>
                        <td width="105" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            操作
                        </td>
                    </tr>
                    <%if (!(this.SetShipList != null && this.SetShipList.Count > 0))
                      {%>
                    <tr class="tempRow">
                        <td bgcolor="#FFFFFF" align="center">
                            <span class="index">1</span>
                        </td>
                        <td height="28" bgcolor="#FFFFFF" align="center">
                            <input type="text" class="inputtext formsize80" errmsg="请输入轮船名称!" valid="required"
                                name="txtShipName" value=""><font class="fontred">*</font>
                            <input type="hidden" name="txtYouLunId" value="" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <select name="selShipStar" class="inputselect">
                                <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.ShipStar)),"0")%>
                            </select>
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" value="" class="inputtext formsize40" onafterpaste="this.value=this.value.replace(/\D/g,'')"
                                onkeyup="this.value=this.value.replace(/\D/g,'')" name="txtShipSpace" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" value="" valid="isTelephone" errmsg="舰载电话格式错误!" class="inputtext formsize80"
                                name="txtContactTel" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" value="" class="inputtext formsize80" name="txtContactName" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" value="" class="inputtext formsize80" name="txtShipRoute" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox">
                                <div>
                                    <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                        name="hide_Cruiseimg_file" value="|" />
                                    <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                        data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                </div>
                            </div>
                        </td>
                        <td align="center">
                            <a class="addbtn" href="javascript:void(0)">
                                <img width="48" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                    <img src="/images/delimg.gif" class="delbtn" /></a>
                        </td>
                    </tr>
                    <%} %>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="tempRow">
                                <td bgcolor="#FFFFFF" align="center">
                                    <span class="index">
                                        <%#Container.ItemIndex + 1 %></span>
                                </td>
                                <td height="28" bgcolor="#FFFFFF" align="center">
                                    <input type="text" class="inputtext formsize80" errmsg="请输入轮船名称!" valid="required"
                                        name="txtShipName" value="<%#Eval("ShipName") %>"><font class="fontred">*</font>
                                    <input type="hidden" name="txtYouLunId" value="<%#Eval("SubId") %>" />
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <select name="selShipStar" class="inputselect">
                                        <%#EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SourceStructure.ShipStar)), (int)((EyouSoft.Model.EnumType.SourceStructure.ShipStar)(Eval("ShipStar"))))%>
                                    </select>
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <input type="text" value="<%#Eval("ShipSpace") %>" onafterpaste="this.value=this.value.replace(/\D/g,'')"
                                        onkeyup="this.value=this.value.replace(/\D/g,'')" class="inputtext formsize40"
                                        name="txtShipSpace" />
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <input type="text" valid="isTelephone" errmsg="舰载电话格式错误!" value="<%#Eval("Telephone") %>"
                                        class="inputtext formsize80" name="txtContactTel" />
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <input type="text" value="<%#Eval("ContactName") %>" class="inputtext formsize80"
                                        name="txtContactName" />
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <input type="text" value="<%#Eval("ShipRoute") %>" class="inputtext formsize80" name="txtShipRoute" />
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox">
                                        <div>
                                            <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                                name="hide_Cruiseimg_file" value="|<%#((EyouSoft.Model.ComStructure.MComAttach)Eval("AttachModel"))!=null?((EyouSoft.Model.ComStructure.MComAttach)Eval("AttachModel")).FilePath:"" %>" />
                                            <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                                data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                        </div>
                                    </div>
                                    <%#((EyouSoft.Model.ComStructure.MComAttach)Eval("AttachModel")) != null && ((EyouSoft.Model.ComStructure.MComAttach)Eval("AttachModel")).FilePath.Trim()!="" ? "<div data-class='span_Cruiseimg_file' class='upload_filename'><a target='_blank' href='" + ((EyouSoft.Model.ComStructure.MComAttach)Eval("AttachModel")).FilePath + "'>查看附件</a><a href='javascript:void(0);' title='删除附件' onclick='CruiseEditPage.RemoveFile(this);'><img src='/images/cha.gif' border='0'></a> </div>" : ""%>
                                </td>
                                <td align="center">
                                    <a class="addbtn" href="javascript:void(0)">
                                        <img width="48" height="20" src="/images/addimg.gif" /></a> <a href="javascript:void(0)">
                                            <img src="/images/delimg.gif" class="delbtn" /></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div style="margin: 0px 10px; display: none;" id="divCruiseimgUploadTemp">
            <div>
                <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                    name="hide_Cruiseimg_file" value="|" />
                <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                    data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
            </div>
            <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
            </div>
            <div data-class="Cruiseimg_upload" data-id="thumbnails">
            </div>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSave" runat="server"><s class="baochun"></s>保
                存</a><a hidefocus="true" href="javascript:void(0);" onclick="CruiseEditPage.close()"><s
                    class="chongzhi"></s>取 消</a>
        </div>
    </div>
    </form>
</body>
</html>

<script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function() {
        CruiseEditPage.PageInit();
        if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
            $("#<%=btnSave.ClientID %>").hide();
        }
    })

    var CruiseEditPage = {
        ajaxurl: "/ResourceManage/Cruise/CruiseEdit.aspx",
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
                        tableToolbar._showMsg(ret.msg, function() { parent.location.href = parent.location.href });
                    }
                    else {
                        tableToolbar._showMsg(ret.msg, function() { CruiseEditPage.BindBtn() });
                    }
                },
                error: function() {
                    tableToolbar._showMsg(tableToolbar.errorMsg, function() { CruiseEditPage.BindBtn() });
                }
            });
        },
        BindBtn: function() {
            $("#<%=btnSave.ClientID %>").click(function() {
                if (!CruiseEditPage.CheckForm()) {
                    return false;
                }
                CruiseEditPage.GoAjax(CruiseEditPage.ajaxurl + '?doType=update&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_游轮 %>&id=<%=Request.QueryString["id"]%>');
                return false;
            })
            $("#<%=btnSave.ClientID %>").html("<s class='baochun'></s>保存").attr("class", "");
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        RemoveFile: function(obj) {
            $(obj).closest("td").find("input[name='hide_Cruiseimg_file']").val("");
            $(obj).closest("div[class='upload_filename']").remove();
            return false;
        },
        PageInit: function() {
            this.InitSwfUpload();
            pcToobar.init({
                gID: "#<%=ddlCountry.ClientID %>",
                pID: "#<%=ddlProvince.ClientID %>",
                cID: "#<%=ddlCity.ClientID %>",
                xID: "#<%=ddlArea.ClientID %>",
                gSelect: '<%=this.Countryindex %>',
                pSelect: '<%=this.Provinceindex %>',
                cSelect: '<%=this.Cityindex %>',
                xSelect: '<%=this.Areaindex %>',
                comID: '<%=this.SiteUserInfo.CompanyId %>'
            })
            FV_onBlur.initValid($("#<%=form1.ClientID %>"));
            CruiseEditPage.BindBtn();
            if ($("#<%=Radio_hd_yes.ClientID %>").attr("checked") == true) {
                $("#lbContract").show();
            }

            $("#<%=Radio_hd_yes.ClientID %>").click(function() {
                $("#lbContract").show();
            })
            $("#<%=Radio_hd_no.ClientID %>").click(function() {
                $("#lbContract").hide();
            })

            $("#cruise_tbl").autoAdd({ addCallBack: CruiseEditPage.AddRowCallBack })
        },
        CreateFlashUpload: function(flashUpload, idNo) {
            flashUpload = new SWFUpload({
                upload_url: "/CommonPage/upload.aspx",
                file_post_name: "Filedata",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>"
                },

                file_size_limit: "2 MB",
                file_types: "*.jpg;*.gif;*.jpeg;*.png;",
                file_types_description: "附件上传",
                file_upload_limit: 1,
                swfupload_loaded_handler: function() { document.title = "" },
                file_dialog_start_handler: uploadStart,
                upload_start_handler: uploadStart,
                file_queued_handler: fileQueued,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_placeholder_id: "spanButtonPlaceholder_" + idNo,
                button_image_url: "/images/swfupload/XPButtonNoText_92_24.gif",
                button_width: 92,
                button_height: 24,
                button_text: '<span ></span>',
                button_text_style: '',
                button_text_top_padding: 1,
                button_text_left_padding: 5,
                button_cursor: SWFUpload.CURSOR.HAND,
                flash_url: "/js/swfupload/swfupload.swf",
                custom_settings: {
                    upload_target: "divFileProgressContainer_" + idNo,
                    HidFileNameId: "hide_Cruiseimg_file_" + idNo,
                    HidFileName: "hide_Cruiseimg_file_Old",
                    ErrMsgId: "errMsg_" + idNo,
                    UploadSucessCallback: null
                },
                debug: false,
                minimum_flash_version: "9.0.28",
                swfupload_pre_load_handler: swfUploadPreLoad,
                swfupload_load_failed_handler: swfUploadLoadFailed
            });
        },
        UploadArgsList: [],
        InitSwfUpload: function(tr) {
            var $box = tr || $("#cruise_tbl");
            $box.find("div[data-class='Cruiseimg_upload_swfbox']").each(function() {
                var idNo = parseInt(Math.random() * 100000);
                $(this).find("[data-class='Cruiseimg_upload']").each(function() {
                    if ($(this).attr("id") == "") {
                        $(this).attr("id", $(this).attr("data-id") + "_" + idNo);
                    }
                })
                var swf = null;
                CruiseEditPage.CreateFlashUpload(swf, idNo);
                if (swf) {
                    CruiseEditPage.UploadArgsList.push(swf);
                }
            })
        },
        AddRowCallBack: function(tr) {
            var $tr = tr;
            $tr.find("div[data-class='Cruiseimg_upload_swfbox']").html($("#divCruiseimgUploadTemp").html());
            $tr.find("span[class='errmsg']").html("");
            $tr.find("div[data-class='span_Cruiseimg_file']").remove();
            CruiseEditPage.InitSwfUpload($tr);
        }
    }
</script>


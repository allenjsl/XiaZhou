<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductOperating.aspx.cs"
    ValidateRequest="false" EnableEventValidation="false" Inherits="Web.GroupEnd.Suppliers.ProductOperating" %>

<%@ Register Src="~/UserControl/Suppliers.ascx" TagName="Suppliers" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/CostAccounting.ascx" TagName="CostAccounting" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title>供应商产品投放-新增</title>
    <link type="text/css" rel="stylesheet" href="/Css/fx_style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        td.noboder td
        {
            border: 0px;
            margin: 0;
            padding: 1px;
            white-space: normal;
            height: auto;
        }
        #flashUploadTd .progressWrapper
        {
            overflow: hidden;
            width: 345px;
        }
        #flashUploadTd .progressName
        {
            overflow: hidden;
            width: 290px;
        }
    </style>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    
    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <uc1:Suppliers runat="server" ID="Suppliers1" ProcductClass="default Producticon"
        CompanyId="<%=SiteUserInfo.CompanyId %>" />
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <div class="hr_10">
    </div>
    <div class="list-maincontent">
        <div class="listtablebox">
            <form id="formPlan" runat="server">
            <table width="100%" cellspacing="0" cellpadding="0"  border="0">
                <tbody>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font> 线路区域：
                        </td>
                        <td>
                            <select id="sltArea" name="sltArea" valid="isNo" errmsg="请选择线路区域" novalue="-1">
                                <asp:Literal ID="litArea" runat="server"></asp:Literal>
                            </select>
                        </td>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font>线路名称：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRouteName" runat="server" CssClass="searchInput size170" valid="required"
                                errmsg="请填写线路名称"></asp:TextBox>
                        </td>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font> 天数：
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTourDays" runat="server" CssClass="searchInput size68" valid="required|RegInteger"
                                errmsg="请填写天数|天数格式不正确"></asp:TextBox>
                            <button class="addtimebtn" type="button" id="btnAddDay">
                                增加日程</button>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font>预控人数：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlanPeopleNumber" runat="server" CssClass="searchInput size68"
                                valid="required|RegInteger" errmsg="请填写预控人数|人数格式不正确"></asp:TextBox>
                        </td>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                           <asp:Literal ID="ltMsg" runat="server" Text="发班周期："></asp:Literal>
                        </td>
                        <td align="left" colspan="3">
                            <asp:Label ID="lblLeaveDate" runat="server" Text=""></asp:Label>
                            <asp:HiddenField ID="hideLeaveDate" runat="server" Value="" />
                            <asp:PlaceHolder ID="phdSelectDate" runat="server"><a style="font-weight: bold; color: #fff;
                                background-color: #ff6600; display: block; width: 80px; height: 17px; text-align: center;
                                padding-top: 5px;" id="a_SelectData" href="javascript:void(0);">请选择日期</a></asp:PlaceHolder>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            出发交通：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtLTraffic" CssClass="searchInput" Width="800px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            返程交通：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtRTraffic" Width="800px" CssClass="searchInput" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            集合方式：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtGather" runat="server" CssClass="searchInput" Width="800px" Height="55px"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            附件上传：
                        </td>
                        <td id="flashUploadTd">
                            <uc4:UploadControl runat="server" ID="UploadControl1" />
                            <asp:Label runat="server" ID="lblFile"></asp:Label>
                        </td>
                        <td width="100" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font>停收提前天数：
                        </td>
                        <td colspan="3">
                            <span class="kuang2">
                                <asp:TextBox ID="txtStopDays" runat="server" CssClass="searchInput" valid="required|RegInteger"
                                    errmsg="停收提前的天数|天数格式不正确"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            行程特色：
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtPlanFeature" CssClass="searchInput" Width="800px" Height="55px"
                                runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
            <table width="100%" cellspacing="0" cellpadding="0"  border="0">
                <tbody>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font>成人结算价：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPeerAdultPrice" valid="required|isMoney" errmsg="请填写成人结算价格|价格格式不正确"
                                CssClass="searchInput size68" runat="server"></asp:TextBox>
                        </td>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            <font class="fontbsize12">*</font>儿童结算价：
                        </td>
                        <td>
                            <asp:TextBox ID="txtPeerChildPrice" valid="required|isMoney" errmsg="请填写儿童结算价格|价格格式不正确"
                                CssClass="searchInput size68" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
            <uc2:Journey runat="server" ID="Journey1" />
            <div class="hr_10">
            </div>
            <table width="100%" cellspacing="0" cellpadding="0"  border="0">
                <tbody>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            服务标准：
                        </td>
                        <td>
                            <asp:TextBox ID="txtServiceStandard" runat="server" CssClass="textAreaSize textAreaSize800"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            不含项目：
                        </td>
                        <td>
                            <asp:TextBox ID="txtNoNeedItem" CssClass="textAreaSize textAreaSize800" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            购物安排：
                        </td>
                        <td>
                            <asp:TextBox ID="txtShoppingItem" CssClass="textAreaSize textAreaSize800" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            儿童安排：
                        </td>
                        <td>
                            <asp:TextBox ID="txtChildServiceItem" CssClass="textAreaSize textAreaSize800" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            自费项目：
                        </td>
                        <td>
                            <asp:TextBox ID="txtOwnExpense" CssClass="textAreaSize textAreaSize800" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" height="14" bgcolor="#DCEFF3" align="right">
                            注意事项：
                        </td>
                        <td>
                            <asp:TextBox ID="txtNeedAttention" CssClass="textAreaSize textAreaSize800" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="88" bgcolor="#DCEFF3" align="right">
                            温馨提醒：
                        </td>
                        <td>
                            <asp:TextBox ID="txtWarmRemind" CssClass="textAreaSize textAreaSize800" runat="server"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_5">
            </div>
            <table width="100%" cellspacing="0" cellpadding="0"  border="0">
                <tbody>
                    <tr>
                        <th bgcolor="#DCEFF3" align="left">
                            内部信息（内部注意事项，内部成本资料等）
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtInsiderInfor" runat="server" TextMode="MultiLine" CssClass="textAreaSize textAreaSize800"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
            <div class="mainbox cunline" style="width: 100%">
                <asp:PlaceHolder runat="server" ID="phMsg" Visible="false">
                <div class="tishi_info">
                   该计划已审核，不能进行操！
                 </div>
                 </asp:PlaceHolder>
                <ul style="margin: 0 auto; width: 250px;">
                    <asp:PlaceHolder runat="server" ID="phSave">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnSave">保存</a></li>
                        <li class="quxiao-cy"><a href="javascript:void(0);" id="btnCancle">取消</a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="phBack" Visible="false">
                        <li class="cun-cy"><a href="javascript:void(0);" id="btnBack">返回列表</a></li>
                    </asp:PlaceHolder>
                </ul>
            </div>
            </form>
        </div>
        <div class="hr_10">
        </div>
    </div>
    <!-- InstanceEndEditable -->
</body>
</html>

<script type="text/javascript">
    var AddSupplierPlan = {
        //预留的数据
        Data: {
            'sl': '<%=Request.QueryString["sl"] %>',
            'Type': '<%=Request.QueryString["_Type"] %>',
            'TourId': '<%=Request.QueryString["TourId"] %>'
        },
        //提交保存
        Save: function() {
            $("#btnSave").text("提交中...");
            AddSupplierPlan.UnBindSave();
            KEditer.sync();
            $.newAjax({
                type: "post",
                url: "/GroupEnd/Suppliers/ProductOperating.aspx?" + $.param(AddSupplierPlan.Data),
                data: $("#formPlan").serialize(),
                dataType: "json",
                success: function(data) {
                    if (data.result == "1") {
                        tableToolbar._showMsg(data.msg, function() {
                            window.location.href = "/GroupEnd/Suppliers/ProductList.aspx?sl=" + AddSupplierPlan.Data.sl;
                        });
                    }
                    else {
                        tableToolbar._showMsg(data.msg);
                        AddSupplierPlan.BindSave();
                    }
                },
                error: function() {
                    tableToolbar._showMsg("服务器忙！");
                    AddSupplierPlan.BindSave();
                }

            });

        },
        //取消操作
        Cancle: function() {
            window.location.href = "/GroupEnd/Suppliers/ProductList.aspx?sl=" + AddSupplierPlan.Data.sl;
        },
        //移除Click事件
        UnBindSave: function() {
            $("#btnSave").unbind("click");
            $("#btnSave").attr("class", "cun-cyactive");
            $("#btnCancle").unbind("click");

        },
        //绑定Click事件
        BindSave: function() {
            $("#btnSave").attr("class", "cun-cy");
            $("#btnSave").unbind("click");
            $("#btnSave").text("保存");
            $("#btnSave").click(function() {
                if (ValiDatorForm.validator($("#formPlan").get(0), "alert")) {
                    AddSupplierPlan.Save();
                }
                return false;
            });

            $("#btnCancle").click(function() {
                AddSupplierPlan.Cancle();
                return false;
            });
        },
        //选择发班周期
        ShowDate: function() {
            Boxy.iframeDialog({
                iframeUrl: "/TeamCenter/SelectChildTourDate.aspx?hide=<%=hideLeaveDate.ClientID %>&show=<%=lblLeaveDate.ClientID %>&old=" + $("#<%=hideLeaveDate.ClientID %>").val(),
                title: "选择发班周期",
                modal: true,
                width: "925px",
                height: "315px"
            });
            return false;
        },
        ValidateDay: function(day) {
            var day = tableToolbar.getInt($("#<%=txtTourDays.ClientID %>").val());
            day++;
            $("#<%=txtTourDays.ClientID %>").val(day);
            $("#<%=txtTourDays.ClientID %>").change();
        },
        DelFile: function(obj) {

            $(obj).parent().remove();
        },
        PageInit: function() {
            //行程安排
            $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txtTourDays.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack });

            //表单验证初始化
            FV_onBlur.initValid($("#formPlan").get(0));

            //选择发班周期
            $("#a_SelectData").click(function() {
                AddSupplierPlan.ShowDate();
            });

            //保存
            $("#btnSave").click(function() {
                if (ValiDatorForm.validator($("#formPlan").get(0), "alert")) {
                    AddSupplierPlan.Save();
                }
            });

            //取消
            $("#btnCancle").click(function() {

                AddSupplierPlan.Cancle();
            });
            //返回列表
            $("#btnBack").click(function() {
                AddSupplierPlan.Cancle();
            });


            $("#btnAddDay").click(function() {
                AddSupplierPlan.ValidateDay($("#<%=txtTourDays.ClientID %>").val());
            });
        }
    };


    $(function() {
        AddSupplierPlan.PageInit();
    });
        
    
        
</script>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PathEidt.aspx.cs" Inherits="Web.ResourceManage.Path.PathEidt" ValidateRequest="false" %>

<%@ Register Src="../../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc1" %>
<%@ Register Src="../../UserControl/ForeignQuote.ascx" TagName="ForeignQuote" TagPrefix="uc2" %>
<%@ Register Src="../../UserControl/CostAccounting.ascx" TagName="CostAccounting"
    TagPrefix="uc3" %>
<%@ Register Src="../../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        td.noboder td
        {
            border: 0px;
            margin: 0;
            padding: 0;
            white-space: normal;
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server" id="form1" enctype="multipart/form-data">
    <input type="hidden" runat="server" id="Agreement" />
    <input type="hidden" runat="server" id="imgCount" />
    <input type="hidden" runat="server" id="HidRouteAreaID" value="" />

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <div class="wrap">
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box" id="AddCoutent">
                <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                    <tbody>
                        <tr>
                            <td class="addtableT">
                                是否共享：
                            </td>
                            <td width="150" class="kuang2">
                                <label>
                                    <asp:RadioButton ID="radyes" runat="server" Checked="true" GroupName="radgroup" Style="border: none" />
                                    共享</label>
                                <label>
                                    <asp:RadioButton ID="radno" runat="server" GroupName="radgroup" Style="border: none" />
                                    不共享</label>
                            </td>
                            <td width="134" class="addtableT">
                                线路区域：
                            </td>
                            <td width="196" class="kuang2">
                                <select id="routeAreaList" name="routeAreaList" class="inputselect">
                                    <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(AreaID,this.SiteUserInfo.CompanyId)%>
                                </select>
                                <a id="link12" href="javascript:void(0)">添加线路区域</a> <font class="fontbsize12">*</font>
                            </td>
                            <td width="117" class="addtableT">
                                线路名称：
                            </td>
                            <td width="273" class="kuang2">
                                <asp:TextBox ID="txtPathName" MaxLength="30" name="txtPathName" runat="server" class="inputtext formsize180"
                                    errmsg="请输入路线名称!" valid="required"></asp:TextBox>
                                <font class="fontbsize12">*</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="addtableT">
                                旅游天数：
                            </td>
                            <td class="kuang2">
                                <asp:TextBox ID="txtDayCount" MaxLength="2" name="txtDayCount" onafterpaste="this.value=this.value.replace(/\D/g,'')"
                                    onkeyup="this.value=this.value.replace(/\D/g,'')" runat="server" class="inputtext formsize40"
                                    valid="required" errmsg="请输入旅游天数!"></asp:TextBox>
                                <button class="addtimebtn" type="button" id="btnAddDays">
                                    增加日程</button>
                                <font class="fontbsize12">*</font>
                            </td>
                            <td class="addtableT">
                                添加附件：
                            </td>
                            <td class="kuang2" colspan="3">
                                <uc4:UploadControl ID="UploadControl1" runat="server" IsUploadMore="false" IsUploadSelf="true" />
                                <asp:Label ID="lbFiles" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="14" class="addtableT">
                                出发交通：
                            </td>
                            <td class="kuang2" colspan="5">
                                <asp:TextBox ID="txtStartTraffic" name="txtStartTraffic" runat="server" class="inputtext formsize450"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="14" class="addtableT">
                                返程交通：
                            </td>
                            <td class="kuang2" colspan="5">
                                <asp:TextBox ID="txtEndTraffic" name="txtEndTraffic" runat="server" class="inputtext formsize450"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="14" class="addtableT">
                                签证资料：
                            </td>
                            <td class="kuang2" colspan="5">
                                <uc4:UploadControl ID="UploadControl2" runat="server" IsUploadMore="true" IsUploadSelf="true" />
                                <asp:Label ID="lbqzinfo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="122" height="180" class="addtableT">
                                行程特色：
                            </td>
                            <td class="kuang2" colspan="5">
                                <asp:TextBox TextMode="MultiLine" ID="txtPathDesc" name="txtPathDesc" runat="server"
                                    CssClass="inputtext formsize800"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="14" class="addtableT">
                                集合方式：
                            </td>
                            <td class="kuang2" colspan="5">
                                <asp:TextBox TextMode="MultiLine" ID="txtCollection" name="txtCollection" runat="server"
                                    CssClass="inputtext formsize800"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="hr_5">
            </div>
            <uc1:Journey ID="Journey1" runat="server" />
            <div class="hr_5">
            </div>
            <uc2:ForeignQuote ID="ForeignQuote1" runat="server" />
            <div class="addxc-box">
                <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
                    <tbody>
                        <tr>
                            <td bgcolor="#E0F3FC" colspan="2">
                                <img width="16" height="15" align="absmiddle" src="/images/chengren.gif">
                                成人价
                                <asp:TextBox ID="txtAdultPrice" name="txtAdultPrice" runat="server" class="inputtext formsize40"></asp:TextBox>
                                元/人 &nbsp;&nbsp;
                                <img align="absmiddle" src="/images/child.gif">
                                儿童价
                                <asp:TextBox ID="txtChildPrice" name="txtChildPrice" runat="server" class="inputtext formsize40"></asp:TextBox>
                                元/人&nbsp;&nbsp;&nbsp;其它费用：&nbsp;
                                <asp:TextBox ID="txtOtherPrice" name="txtOtherPrice" runat="server" class="inputtext formsize40"></asp:TextBox>
                                元&nbsp;&nbsp;&nbsp;&nbsp;<span class="fontred">*</span>合计价格：
                                <asp:TextBox ID="txtTotalPrice" name="txtTotalPrice" runat="server" class="inputtext formsize40"
                                    valid="required|isMoney" errmsg="请输入合计金额!|请输入正确的格式！"></asp:TextBox>
                                元
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#E0F3FC" align="left" class="lefttd">
                                价格备注：
                            </td>
                            <td bgcolor="#E0F3FC" class="kuang2">
                                <asp:TextBox ID="txtPathRemark" runat="server" TextMode="MultiLine" CssClass="inputtext inputtext formsize800"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <uc3:CostAccounting ID="CostAccounting1" runat="server" />
            <div class="mainbox cunline fixed">
                <ul>
                    <li class="cun-cy"><a href="javascript:void(0)" id="btnSave" runat="server">保存</a></li>
                    <li class="quxiao-cy"><a href="/ResourceManage/Path/PathList.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路资源 %>">
                        返回</a></li>
                </ul>
                <div class="hr_10">
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" runat="server" id="hiddenSourceID" value="" />
    </form>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        if ('<%=Request.QueryString["chakan"] %>' == 'chakan') {
            $("#<%=btnSave.ClientID %>").hide();
        }
        $(function() {
            PathEditPage.PageInit();
        })
        function CallBackFun(obj) {
            $("#routeAreaList").append("<option data-type='" + obj.type + "' value='" + obj.id + "'>" + obj.name + "</option>")
            $("#routeAreaList").val(obj.id)
            $("#routeAreaList").attr("value", obj.id);
            $("#<%=HidRouteAreaID.ClientID %>").val(obj.id);
        }

        var PathEditPage = {
            VisaPath: null,
            PageInit: function() {
                FV_onBlur.initValid($("#<%=form1.ClientID %>"));

                KEditer.init("<%=txtPathDesc.ClientID %>");

                $(".content2").autoAdd({ changeInput: $("#txtdate") });
                $(".addtimebtn").click(function() {
                    $("#txtdate").val(parseInt($("#txtdate").val() == "" ? 0 : $("#txtdate").val()) + 1);
                    $("#txtdate").change();
                });
                $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txtDayCount.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack, delCallBack: PathEditPage.DelRowCallBack });
                $("#AddCoutent").find("a[Data-Visa='VisaInfo']").each(function() {
                    PathEditPage.VisaPath += $(this).attr("Data-Path") + "|" + $(this).text() + "&";
                })
                PathEditPage.BindBtn();

                $("#routeAreaList").change(function() {
                    $("#<%=HidRouteAreaID.ClientID %>").val($(this).val());
                })
                //添加天数
                $("#btnAddDays").click(function() {
                    var day = tableToolbar.getInt($("#<%=txtDayCount.ClientID %>").val());
                    day++;
                    $("#<%=txtDayCount.ClientID %>").val(day);
                    $("#<%=txtDayCount.ClientID %>").change();
                })
                $("#link12").click(function() {
                    var url = "/ResourceManage/Path/AddRouteArea.aspx?callback=CallBackFun";
                    Boxy.iframeDialog({
                        iframeUrl: url,
                        title: "添加线路区域",
                        modal: true,
                        width: "400px",
                        height: "146px"
                    });
                    return false;
                });
                $("#divForeignQuote select").change(function() {
                    var adultPrice = 0;
                    var otherPrice = 0;
                    $("#divItems").find("tr[class='tempRow']").each(function() {
                        var tr = $(this);
                        if (tr.find("select[data-class='slt_ForeignQuote_Price']").val() == "0") {
                            adultPrice = tableToolbar.calculate(adultPrice, tr.find("input[name='txt_ForeignQuote_Price']").val(), "+");
                        } else {
                            otherPrice = tableToolbar.calculate(otherPrice, tr.find("input[name='txt_ForeignQuote_Price']").val(), "+");
                        }
                    })

                    if (adultPrice > 0) {
                        $("#<%=txtAdultPrice.ClientID %>").val(adultPrice);
                    }
                    if (otherPrice > 0) {
                        $("#<%=txtOtherPrice.ClientID %>").val(otherPrice);
                    }

                    //计算金额
                    PathEditPage.SetSumPrice();
                })
                $("#divForeignQuote").find("input[name='txt_ForeignQuote_Price']").blur(function() {
                    //计算金额
                    $(this).next().change();
                })
                $("#<%=txtAdultPrice.ClientID%>,#<%=txtChildPrice.ClientID%>,#<%=txtOtherPrice.ClientID%>").blur(function() {
                    //计算金额
                    PathEditPage.SetSumPrice();
                })
            },
            CheckForm: function() {
                return ValiDatorForm.validator($("#<%=btnSave.ClientID %>").closest("form").get(0), "parent");
            },
            GoAjax: function(url) {
                $("#<%=btnSave.ClientID %>").html("提交中...").unbind("click").css("background-position", "0 -62px");
                KEditer.sync();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    data: $("#<%=btnSave.ClientID %>").closest("form").serialize(),
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { window.location.href = "/ResourceManage/Path/PathList.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路资源 %>" });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg, function() { PathEditPage.BindBtn() });
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg, function() { PathEditPage.BindBtn() });
                    }
                });
            },
            BindBtn: function() {
                $("#<%=btnSave.ClientID %>").click(function() {
                    if (!PathEditPage.CheckForm()) {
                        return false;
                    }
                    if ($("#routeAreaList").val() == "0") {
                        tableToolbar._showMsg("请选择线路区域!");
                        return false;
                    }
                    if ($("#txtDayCount").val() == "0") {
                        tableToolbar._showMsg("旅游天数不能为0");
                    }
                    PathEditPage.GoAjax('/ResourceManage/Path/PathEidt.aspx?doType=<%=Request.QueryString["doType"] %>&type=save&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路资源 %>&id=<%=Request.QueryString["id"]%>');
                    return false;
                })
                $("#<%=btnSave.ClientID %>").html("<s class='baochun'></s>保存").css("background-position", "0 0");
            },
            delFileByItemid: function(obj) {
                tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
                    $(obj).parent().prev("a").hide();
                    //$(obj).parent().hide();
                    PathEditPage.VisaPath = PathEditPage.VisaPath.replace($(obj).prev("a").attr("Data-Path"), "");
                    PathEditPage.VisaPath = PathEditPage.VisaPath.replace($(obj).prev("a").text(), "");
                    $("#imgCount").val(PathEditPage.VisaPath);
                });
            },
            DelFile: function(obj) {
                $(obj).parent().remove();
            },
            //计算价格
            SetSumPrice: function() {
                //成人价
                var adultPrice = tableToolbar.getFloat($("#<%=txtAdultPrice.ClientID%>").val());
                //儿童价
                var childPrice = tableToolbar.getFloat($("#<%=txtChildPrice.ClientID%>").val());
                //其它费用
                var otherPrice = tableToolbar.getFloat($("#<%=txtOtherPrice.ClientID%>").val());
                //合计金额
                var sunPrice = 0;
                //获取对外报价
                sunPrice = tableToolbar.getFloat(adultPrice + childPrice + otherPrice);

                $("#<%=txtTotalPrice.ClientID%>").val(sunPrice);
            },
            DelRowCallBack: function() {
                PathEditPage.SetSumPrice();
            }
        }
    </script>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompForm.aspx.cs" Inherits="Web.QualityCenter.Complaint.CompForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="16%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>团号：
                </td>
                <td width="40%" height="28" align="left">
                    <span id="sp_dk_txt">
                        <input type="text" id="txt_oneControl" name="txt_oneControl" class="inputtext formsize180"
                            readonly="readonly" style="background-color: #dadada" valid="required" errmsg="请选择团号！" />
                        <input id="txt_TourId" type="hidden" name="txt_TourId" />
                        <a class="xuanyong" href="/CommonPage/TourListBoxy.aspx" id="a_duplexing" title="团号选择"
                            data-width="700" data-height="400"></a></span>
                </td>
                <td width="16%" height="28" align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>投诉人：
                </td>
                <td width="28%" height="28" align="left">
                    <asp:TextBox runat="server" ID="txtComplaintor" class="inputtext formsize120" valid="required"
                        errmsg="投诉人不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>投诉时间：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtComplaintime" class="inputtext formsize120" onfocus="WdatePicker()"
                        valid="required" errmsg="请输入投诉时间！"></asp:TextBox>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3">
                    投诉人身份：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtComplaintorType" class="inputtext formsize120"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    投诉人电话：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtComplainTel" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td height="28" align="right" bgcolor="#B7E0F3">
                    <span class="errmsg">*</span>投诉类型：
                </td>
                <td height="28" align="left">
                    <asp:TextBox runat="server" ID="txtComplainType" class="inputtext formsize120" valid="required"
                        errmsg="请输入投诉类型!"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    投诉意见：
                </td>
                <td height="28" colspan="3" align="left">
                    <asp:TextBox runat="server" ID="txtComplainRemark" TextMode="MultiLine" class="inputtext formsize450"
                        Height="60"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="CompFormPage.ResetForm()" hidefocus="true">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var CompFormPage = {
            Query: {
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            pageInit: function() {
                newToobar.init({
                    box: "#sp_dk_txt",
                    className: "xuanyong",
                    callBackFun: "CompFormPage.CallBackFun",
                    para: {
                        pIframeID: Boxy.queryString("iframeID"),
                        isRadio: true
                    }
                });
            },
            CallBackFun: function(data) {
                if (data) {
                    var obj = $("#txt_oneControl")
                    var btStr = "";
                    if (data[0] && data[0].split(',').length == 2) {
                        obj.val(data[0].split(',')[0]);
                        obj.next(":hidden").val(data[0].split(',')[1]);
                    }

                    for (var i in data) {
                        btStr += data[i].split(",")[0] + "<br/>";
                    }
                    $(".bt-wrapper").html("");
                    obj.bt({
                        contentSelector: function() {
                            return btStr;
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 150,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });
                }
            },
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中..");
                    var url = "/QualityCenter/Complaint/CompForm.aspx?";
                    url += $.param({
                        doType: that.Query.doType,
                        save: "save",
                        sl: that.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(that.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    CompFormPage.BindBtn();
                                });
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                CompFormPage.BindBtn();
                            });

                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    CompFormPage.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }

        $(function() {
            CompFormPage.pageInit();
            CompFormPage.BindBtn();
        });
             
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttGroup.aspx.cs" Inherits="Web.ManageCenter.Attendance.AttGroup" %>

<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: #f00;
        }
    </style>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="12%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>考勤时间：
                </td>
                <td height="28" colspan="2" align="left">
                    <asp:TextBox runat="server" ID="tbStime" valid="required" class="inputtext formsize120"
                        errmsg="考勤开始时间不能为空！" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'tbEtime\')}',dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                    <a href="javascript:document.getElementById('tbStime').focus();" class="timesicon">开始时间</a>至
                    <asp:TextBox runat="server" ID="tbEtime" valid="required" errmsg="考勤结束时间不能为空！" class="inputtext formsize120"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'tbStime\')}',dateFmt:'yyyy-MM-dd HH:mm'})"></asp:TextBox>
                    <a href="javascript:document.getElementById('tbEtime').focus();" class="timesicon">结束时间</a>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>考勤人员：
                </td>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:Label runat="server" ID="attendNames"></asp:Label>
                    <asp:HiddenField runat="server" ID="hdattenIds" />
                    <asp:HiddenField runat="server" ID="hdattenNames" />
                    <asp:HiddenField runat="server" ID="hdattenDepts" />
                </td>
            </tr>
            <tr>
                <td width="12%" rowspan="9" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    考勤情况：
                </td>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="rbquanqing" Checked="true" Text="全勤" GroupName="attendance"
                        data-value="0" data-class="radioBtn" />
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="rbchidao" Text="迟到" GroupName="attendance" data-class="radioBtn"
                        data-value="1" />
                    <asp:TextBox runat="server" ID="tbchidao" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbzaotui" Text="早退" GroupName="attendance" data-value="2"
                        data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="tbzaotui" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="rbkuanggong" Text="旷工" GroupName="attendance"
                        data-value="3" data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="tbkuanggong" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left">
                    <asp:RadioButton runat="server" ID="RadioButton1" Text="请假" GroupName="attendance"
                        data-value="4" data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="tbqjyuanyin" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                    请假天数：
                    <asp:TextBox runat="server" ID="tbqjtianshu" data-class="timeCount" class="inputtext formsize60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="RadioButton3" Text="加班" GroupName="attendance"
                        data-value="5" data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="tbjbyuanyin" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                    加班小时：
                    <asp:TextBox runat="server" ID="tbjbshishu" data-class="timeCount" class="inputtext formsize60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left">
                    <asp:RadioButton runat="server" ID="RadioButton2" Text="停职" GroupName="attendance"
                        data-value="8" data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="tbtzyuanyin" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                    停职天数：
                    <asp:TextBox runat="server" ID="tbtztianshu" data-class="timeCount" class="inputtext formsize60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="rbchuchai" Text="出差" GroupName="attendance" data-value="6"
                        data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="ccyuanyin" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                    出差天数：
                    <asp:TextBox runat="server" ID="ccTianshu" data-class="timeCount" class="inputtext formsize60"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbxiujia" Text="休假" GroupName="attendance" data-value="7"
                        data-class="radioBtn" />
                    <asp:TextBox runat="server" ID="xjyuanyin" data-class="subject" class="inputtext formsize450"></asp:TextBox>
                    休假天数：
                    <asp:TextBox runat="server" ID="xjTianshu" data-class="timeCount" class="inputtext formsize60"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="attType" />
    <asp:HiddenField runat="server" ID="sTime" />
    <asp:HiddenField runat="server" ID="eTime" />
    <asp:HiddenField runat="server" ID="timeCount" />
    <asp:HiddenField runat="server" ID="subject" />
    </form>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            //重置
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            //处理数字保留小数
            FormatNum: function(num, n) {
                //参数说明：num 要格式化的数字 n 保留小数位
                num = String(num.toFixed(n));
                // var re = /(-?\d+)(\d{3})/;//每三位用逗号分隔
                //while (re.test(num)) num = num.replace(re, "$1,$2")
                return num;
            },
            //得到天数或者小时数
            GetDays: function(startID, endID, showID, num, n) {
                var start = $("#" + startID).val();
                var end = $("#" + endID).val();
                if (start != "" && end != "") {
                    //v为毫秒数
                    var v = new Date(end.replace(/\-/g, '/')).getTime() - new Date(start.replace(/\-/g, '/')).getTime();
                    $("#" + showID).val(this.FormatNum(v / num, n)); //v为毫秒数
                }
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
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/Attendance/AttGroup.aspx?";
                    url += $.param({
                        save: "save",
                        sl: this.Query.sl
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
                                    PageJsData.BindBtn();
                                });
                            }
                            return false;
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() {
                                PageJsData.BindBtn();
                            });
                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    $("#<%=attType.ClientID %>").val($("span[data-class='radioBtn']").find(":radio:checked").parent("span").attr("data-value"));
                    var arry = $("input[type='text']:not([disabled])");
                    //考勤开始时间
                    $("#<%=sTime.ClientID %>").val($(arry[0]).val());
                    //考勤结束时间
                    $("#<%=eTime.ClientID %>").val($(arry[1]).val());
                    //缘由
                    $("#<%=subject.ClientID %>").val($(arry[2]).val());
                    if (arry.length > 3) {
                        //天数或者时数
                        $("#<%=timeCount.ClientID %>").val($(arry[3]).val());
                    }
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }
        $(function() {
            //灰掉radio后边的文本框
            $("span[data-class='radioBtn']").parent("td").find(":text").attr("disabled", "disabled");
            //控制显示
            $("span[data-class='radioBtn']").click(function() {
                $("span[data-class='radioBtn']").parent("td").find(":text").attr("disabled", "disabled");
                $(this).parent("td").find(":text").removeAttr("disabled");
                //PageJsData.GetDays('<%=tbStime.ClientID %>', '<%=tbEtime.ClientID %>', $(this).parent("td").find(":text[data-class='timeCount']").attr("id"), 24 * 3600 * 1000, 1);
            })
            //加载时选中的内容为显示
            $("span[data-class='radioBtn']").find(":radio:checked").click();
            PageJsData.BindBtn();
        })
    </script>

</body>
</html>

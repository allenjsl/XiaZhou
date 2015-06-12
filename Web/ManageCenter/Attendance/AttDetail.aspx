<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttDetail.aspx.cs" Inherits="Web.ManageCenter.Attendance.AttDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <style type="text/css">
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
        .inputtext formsize50
        {
            width: 58px;
        }
        .formsize470
        {
            width: 470px;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="12%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    考勤时间：
                </td>
                <td height="28" colspan="2" align="left">
                    <asp:TextBox runat="server" ID="tbtime" class="inputtext formsize80" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',maxDate:'%y-%M-%d',opposite:true})"
                        valid="required" errmsg="考勤时间不能为空！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="12%" rowspan="14" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    考勤情况：
                </td>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="rbquanqing" Text="全勤" GroupName="attendance"
                        data-value="0" data-class="radioBtn" />
                </td>
            </tr>
            <tr>
                <td height="28" align="left">
                    <asp:RadioButton runat="server" ID="rbchidao" Text="迟到" GroupName="attendance" data-class="radioBtn"
                        data-value="1" />
                </td>
                <td width="80%" height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="tbchidao" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButton runat="server" ID="rbzaotui" Text="早退" GroupName="attendance" data-value="2"
                        data-class="radioBtn" />
                </td>
                <td width="80%" height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="tbzaotui" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="left">
                    <asp:RadioButton runat="server" ID="rbkuanggong" Text="旷工" GroupName="attendance"
                        data-value="3" data-class="radioBtn" />
                </td>
                <td width="80%" height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="tbkuanggong" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="8%" rowspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbqingjia" Text="请假" GroupName="attendance" data-value="4"
                        data-class="radioBtn" />
                </td>
                <td width="80%" height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    请假时间：
                    <asp:TextBox runat="server" ID="tbqjStime" ReadOnly="true" errmsg="请假开始时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'tbqjEtime\')}',dateFmt:'yyyy-MM-dd HH:mm',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbqjStime')" class="timesicon">
                        开始时间</a> 至
                    <asp:TextBox runat="server" ID="tbqjEtime" ReadOnly="true" errmsg="请假结束时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'tbqjStime\')}',dateFmt:'yyyy-MM-dd HH:mm',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbqjEtime')" class="timesicon">
                        结束时间</a> 请假天数：
                    <asp:TextBox runat="server" ID="qjTianshu" class="inputtext formsize50" Style="width: 34px"></asp:TextBox>
                    <%--onchange="PageJsData.GetDays('tbqjStime','tbqjEtime','qjTianshu',24*3600*1000,1)"--%>
                </td>
            </tr>
            <tr data-class="tr_sub">
                <td height="28" align="left" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="qjyuanyin" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbjiaban" Text="加班" GroupName="attendance" data-value="5"
                        data-class="radioBtn" />
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    加班时间：
                    <asp:TextBox runat="server" ID="tbjbStime" ReadOnly="true" errmsg="加班开始时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'tbqjEtime\')}',dateFmt:'yyyy-MM-dd HH:mm',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbjbStime')" class="timesicon">
                        开始时间</a> 至
                    <asp:TextBox runat="server" ID="tbjbEtime" ReadOnly="true" errmsg="加班结束时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'tbqjStime\')}', dateFmt:'yyyy-MM-dd HH:mm',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbjbEtime')" class="timesicon">
                        结束时间</a> 加班小时：
                    <asp:TextBox runat="server" ID="jbShishu" class="inputtext formsize50" Style="width: 34px"></asp:TextBox>
                    <%--onchange="PageJsData.GetDays('tbjbStime','tbjbEtime','jbShishu',3600*1000,1)"--%>
                </td>
            </tr>
            <tr data-class="tr_sub">
                <td height="28" align="left" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="jbyuanyin" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbtingzhi" Text="停职" GroupName="attendance" data-value="8"
                        data-class="radioBtn" />
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    停职时间：
                    <asp:TextBox runat="server" ID="tbtzStime" ReadOnly="true" errmsg="停职开始时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'tbtzEtime\')}',dateFmt:'yyyy-MM-dd',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbtzStime')" class="timesicon">
                        开始时间</a> 至
                    <asp:TextBox runat="server" ID="tbtzEtime" ReadOnly="true" errmsg="停职结束时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'tbtzStime\')}',dateFmt:'yyyy-MM-dd',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbtzEtime')" class="timesicon">
                        结束时间</a> 停职天数：
                    <asp:TextBox runat="server" ID="tzTianshu" class="inputtext formsize50" Style="width: 34px"></asp:TextBox>
                    <%--onchange="PageJsData.GetDays('tbtzStime','tbtzEtime','tzTianshu',24*3600*1000,1)"--%>
                </td>
            </tr>
            <tr data-class="tr_sub">
                <td height="28" align="left" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="tzyuanyin" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbchuchai" Text="出差" GroupName="attendance" data-value="6"
                        data-class="radioBtn" />
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    出差时间：
                    <asp:TextBox runat="server" ID="tbccStime" ReadOnly="true" errmsg="出差开始时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'tbccEtime\')}',dateFmt:'yyyy-MM-dd',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbccStime')" class="timesicon">
                        开始时间</a> 至
                    <asp:TextBox runat="server" ID="tbccEtime" ReadOnly="true" errmsg="出差结束时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'tbccStime\')}',dateFmt:'yyyy-MM-dd',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbccEtime')" class="timesicon">
                        结束时间</a> 出差天数：
                    <asp:TextBox runat="server" ID="ccTianshu" class="inputtext formsize50" Style="width: 34px"></asp:TextBox>
                    <%--onchange="PageJsData.GetDays('tbccStime','tbccEtime','ccTianshu',24*3600*1000,1)" --%>
                </td>
            </tr>
            <tr data-class="tr_sub">
                <td height="28" align="left" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="ccyuanyin" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td rowspan="2" align="left">
                    <asp:RadioButton runat="server" ID="rbxiujia" Text="休假" GroupName="attendance" data-value="7"
                        data-class="radioBtn" />
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef" data-class="td_radioBtn">
                    休假时间：
                    <asp:TextBox runat="server" ID="tbxjStime" ReadOnly="true" errmsg="休假开始时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'tbxjEtime\')}',dateFmt:'yyyy-MM-dd',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbxjStime')" class="timesicon">
                        开始时间</a> 至
                    <asp:TextBox runat="server" ID="tbxjEtime" ReadOnly="true" errmsg="休假结束时间不能为空！" class="inputtext formsize100"
                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'tbxjStime\')}',dateFmt:'yyyy-MM-dd',opposite:true})"></asp:TextBox>
                    <a href="javascript:void(0);" onclick="PageJsData.GetFocues('tbxjEtime')" class="timesicon">
                        结束时间</a> 休假天数：
                    <asp:TextBox runat="server" ID="xjTianshu" class="inputtext formsize50" Style="width: 34px"></asp:TextBox>
                    <%-- onchange="PageJsData.GetDays('tbxjStime','tbxjEtime','xjyuanyin',24*3600*1000,1)"--%>
                </td>
            </tr>
            <tr data-class="tr_sub">
                <td height="28" align="left" data-class="td_radioBtn">
                    <asp:TextBox runat="server" ID="xjyuanyin" data-class="subject" class="inputtext formsize470"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <asp:PlaceHolder runat="server" ID="ph_save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.CloseForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="attentID" />
    <asp:HiddenField runat="server" ID="deptID" />
    <asp:HiddenField runat="server" ID="staffID" />
    <asp:HiddenField runat="server" ID="attType" />
    <asp:HiddenField runat="server" ID="sTime" />
    <asp:HiddenField runat="server" ID="eTime" />
    <asp:HiddenField runat="server" ID="timeCount" />
    <asp:HiddenField runat="server" ID="subject" />
    </form>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            //得到焦点
            GetFocues: function(i) {
                document.getElementById(i).focus();
            },
            //重置
            CloseForm: function() {
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
                    //计算小于4小时算半天大于4小时算一天
                    if (num == 1000 * 60 * 60 * 24) {
                        var day = parseInt(v / num);
                        var hours = (parseFloat(v / num) - parseFloat(parseInt(v / num))) * 24 > 4 ? 1 : 0.5;
                        alert(parseFloat(day) + parseFloat(hours));
                    }

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
                    var url = "/ManageCenter/Attendance/AttDetail.aspx?";
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
                                    if (that.Query.doType == "add") {
                                        parent.window.document.getElementById("btnSubmit").click();
                                    }
                                    else {
                                        //alert('<%=Request.QueryString["pIframeId"] %>')
                                        parent.Boxy.getIframeWindow('<%=Request.QueryString["pIframeId"] %>').location.reload();
                                    }
                                });

                            }
                            else {
                                parent.tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); });
                            }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() { PageJsData.BindBtn(); });

                        }
                    });
                }
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    $("#<%=attType.ClientID %>").val($("span[data-class='radioBtn']").find(":radio:checked").parent("span").attr("data-value"));
                    var arry = $("input[type='text']:not([disabled])");
                    if (arry.length > 2) {
                        //alert($(arry[0]).val());
                        $("#<%=tbtime.ClientID %>").removeAttr("valid");
                        $("#<%=sTime.ClientID %>").val($(arry[1]).val());
                        $("#<%=eTime.ClientID %>").val($(arry[2]).val());
                        $("#<%=timeCount.ClientID %>").val($(arry[3]).val());
                        $("#<%=subject.ClientID %>").val($(arry[4]).val());
                    }
                    else {
                        $("#<%=subject.ClientID %>").val($(arry[1]).val());
                    }
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }

        $(function() {
            //灰掉radio后边的文本框
            $("td[data-class='td_radioBtn'] :text").attr("disabled", "disabled");
            //控制显示
            $("span[data-class='radioBtn']").click(function() {
                $("td[data-class='td_radioBtn'] :text").attr("disabled", "disabled");
                $(this).parent("td").next().find(":text").removeAttr("disabled");
                $(this).closest("tr").next("tr[data-class='tr_sub']").find(":text").removeAttr("disabled");
            })
            PageJsData.BindBtn();
            //页面赋值
            var attType = $("#<%=attType.ClientID %>").val();
            var sub = $("#<%=subject.ClientID %>").val();
            var stime = $("#<%=sTime.ClientID %>").val();
            var etime = $("#<%=eTime.ClientID %>").val();
            var timeCount = $("#<%=timeCount.ClientID %>").val();
            //全勤
            if (attType == 0) {
                $("#<%=rbquanqing.ClientID %>").attr("checked", "checked");
            }
            //迟到
            if (attType == 1) {
                $("#<%=rbchidao.ClientID %>").attr("checked", "checked");
                $("#<%=tbchidao.ClientID %>").val(sub);
            }
            //早退
            if (attType == 2) {
                $("#<%=rbzaotui.ClientID %>").attr("checked", "checked");
                $("#<%=tbzaotui.ClientID %>").val(sub);
            }
            //旷工
            if (attType == 3) {
                $("#<%=rbkuanggong.ClientID %>").attr("checked", "checked");
                $("#<%=tbkuanggong.ClientID %>").val(sub);
            }
            //请假
            if (attType == 4) {
                $("#<%=rbqingjia.ClientID %>").attr("checked", "checked");
                $("#<%=tbqjStime.ClientID %>").val(stime);
                $("#<%=tbqjEtime.ClientID %>").val(etime);
                $("#<%=qjTianshu.ClientID %>").val(timeCount);
                $("#<%=qjyuanyin.ClientID %>").val(sub);
            }
            //加班
            if (attType == 5) {
                $("#<%=rbjiaban.ClientID %>").attr("checked", "checked");
                $("#<%=tbjbStime.ClientID %>").val(stime);
                $("#<%=tbjbEtime.ClientID %>").val(etime);
                $("#<%=jbShishu.ClientID %>").val(timeCount);
                $("#<%=jbyuanyin.ClientID %>").val(sub);
            }
            //出差
            if (attType == 6) {
                $("#<%=rbchuchai.ClientID %>").attr("checked", "checked");
                $("#<%=tbccStime.ClientID %>").val(stime);
                $("#<%=tbccEtime.ClientID %>").val(etime);
                $("#<%=ccTianshu.ClientID %>").val(timeCount);
                $("#<%=ccyuanyin.ClientID %>").val(sub);
            }
            //休假
            if (attType == 7) {
                $("#<%=rbxiujia.ClientID %>").attr("checked", "checked");
                $("#<%=tbxjStime.ClientID %>").val(stime);
                $("#<%=tbxjEtime.ClientID %>").val(etime);
                $("#<%=xjTianshu.ClientID %>").val(timeCount);
                $("#<%=xjyuanyin.ClientID %>").val(sub);
            }
            if (attType == 8) {
                $("#<%=rbtingzhi.ClientID %>").attr("checked", "checked");
                $("#<%=tbtzStime.ClientID %>").val(stime);
                $("#<%=tbtzEtime.ClientID %>").val(etime);
                $("#<%=tzTianshu.ClientID %>").val(timeCount);
                $("#<%=tzyuanyin.ClientID %>").val(sub);
            }
            //加载时选中的内容为显示
            //alert($("span[data-class='radioBtn']").find(":radio").attr("checked"));
            $("span[data-class='radioBtn']").find(":radio:checked").click();

        });
    </script>

</body>
</html>

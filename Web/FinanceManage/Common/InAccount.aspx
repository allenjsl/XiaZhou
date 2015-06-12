<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InAccount.aspx.cs" Inherits="EyouSoft.Web.FinanceManage.Common.InAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>财务入账</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form>
    <div class="alertbox-outbox">
        <div class="tanchuT">
            <strong>记账凭证</strong><br />
            日期：
            <input id="txt_FDate" name="txt_FDate" class="inputtext formsize80" onfocus="WdatePicker();"
                value="<%=EyouSoft.Common.UtilsCommons.GetDateString(DateTime.Now,ProviderToDate) %>"
                type="text">
        </div>
        <table style="margin: 0 auto" id="tab_list" width="98%" bgcolor="#FFFFFF" cellpadding="0"
            cellspacing="0" align="center">
            <tbody>
                <tr class="odd" style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td width="25%" class="alertboxTableT" height="23" bgcolor="#b7e0f3" align="center">
                        摘要
                    </td>
                    <td class="alertboxTableT" bgcolor="#b7e0f3" align="center">
                        科目
                    </td>
                    <td width="10%" class="alertboxTableT" bgcolor="#b7e0f3" align="right">
                        借方
                    </td>
                    <td width="10%" class="alertboxTableT" bgcolor="#b7e0f3" align="right">
                        贷方
                    </td>
                    <td width="15%" class="alertboxTableT" bgcolor="#b7e0f3" align="center">
                        操作
                    </td>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow" data-fisexport="<%#Eval("FIsExport") %>">
                            <td height="28" align="center">
                                <input name="txtZy" class="inputtext" style="width: 90%; margin: 2px 0;" value="<%#Eval("FExplanation") %>"
                                    type="text" />
                            </td>
                            <td align="center">
                                <input type="hidden" name="hide_KmID" value="<%#Eval("FId") %>" />
                                <input type="hidden" name="hide_KmCode" value="<%#Eval("FDetailId") %>|<%#Eval("FAccountNum") %>" />
                                <input type="hidden" name="hide_KmName" value="<%#Eval("FAccountName") %>" />
                                <textarea data-faccountnum="<%#Eval("FAccountNum") %>" data-faccountname="<%#Eval("FAccountName") %>"
                                    name="txt_Km_Xm" class="inputtext" style="width: 90%; height: 65px; margin: 2px 0;"><%#Eval("FAccountNum") %> - <%#Eval("FAccountName") %><%#GetKingDeeChk(Eval("FItemList"))%>
                                </textarea>
                                <input type="hidden" name="hide_Xm" value='<%#GetJsonByList(Eval("FItemList")) %>' />
                            </td>
                            <td align="right" data-class="td_borrow">
                                <input name="txtJieFang" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal) Eval("FDebit")) %>"
                                    type="text" />
                            </td>
                            <td align="right" data-class="td_loan">
                                <input name="txtDaiFang" class="inputtext formsize50" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal) Eval("FCredit")) %>"
                                    type="text" />
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="addbtn">
                                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delbtn">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_tempRow" runat="server">
                    <tr class="tempRow" data-fisexport="False">
                        <td height="28" align="center">
                            <input name="txtZy" class="inputtext" style="width: 90%; margin: 2px 0;" value=""
                                type="text" />
                        </td>
                        <td>
                            <input type="hidden" name="hide_KmID" value="" />
                            <input type="hidden" name="hide_KmCode" value="" />
                            <input type="hidden" name="hide_KmName" value="" />
                            <textarea name="txt_Km_Xm" class="inputtext" style="width: 90%; height: 65px; margin: 2px 0;"></textarea>
                            <input type="hidden" name="hide_Xm" value='{"data":[]}' />
                        </td>
                        <td align="right" data-class="td_borrow">
                            <input name="txtJieFang" class="inputtext formsize50" value="" type="text" />
                        </td>
                        <td align="right" data-class="td_loan">
                            <input name="txtDaiFang" class="inputtext formsize50" value="" type="text" />
                        </td>
                        <td align="center">
                            <a href="javascript:void(0)" class="addbtn">
                                <img src="/images/addimg.gif" width="48" height="20"></a> <a href="javascript:void(0)"
                                    class="delbtn">
                                    <img src="/images/delimg.gif" width="48" height="20"></a>
                        </td>
                    </tr>
                </asp:Panel>
            </tbody>
        </table>
        <table style="margin: 15px auto" id="table_hsxm" width="98%" bgcolor="#FFFFFF" cellpadding="0"
            cellspacing="0" align="center">
            <tbody>
                <tr class="odd">
                    <td class="alertboxTableT" height="23" bgcolor="#b7e0f3" align="center" style="width: 80px">
                        核算项目：
                    </td>
                    <td align="left" height="40px">
                        <div id="div_MxContent" style="width: 800px; padding: 10px 0;">
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="baochun"></s>保
                存</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
    <input type="hidden" name="KeyId" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("KeyId")%>' />
    <input type="hidden" name="DefaultProofType" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("DefaultProofType")%>' />
    <input type="hidden" name="tourId" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId")%>' />
    </form>

    <script type="text/javascript">
        var InAccount = {
            SelectTxt: null, //当前操作的科目文本框
            EmueList: "",
            Init: function() {
                var that = this;
                that.InitAdjustItem();

                $("#tab_list").autoAdd({ addCallBack: function(tr) {
                    tr.find(".delbtn").css("display", "");
                    tr.attr("data-fisexport", "False");
                    tr.find("input[name='hide_Xm']").val('{"data":[]}')
                    that.InitAdjustItem(tr);
                    InAccount.ShowSaveBtn();
                    return false;
                }, delCallBack: function() {
                    InAccount.ShowSaveBtn();
                }
                })
                that.InitBtn();
                $("tr[data-fisexport='True']").each(function() {
                    var obj = $(this);
                    obj.find(":text").attr("disabled", "disabled");
                    obj.find("textarea").attr("disabled", "disabled");
                    obj.find("input[type='hidden']").val("");
                    obj.find(".delbtn").css("display", "none");
                })
            },
            InitBtn: function() {
                var that = this;
                obj = $("#a_Save");
                obj.css({ "background-position": "", "text-decoration": "" });
                obj.html("<s class=baochun></s>保 存");
                obj.unbind("click");
                obj.click(function() {
                    that.Save(this);
                    return false;
                })
            },
            //为项目文本框创建一个ID
            MakeNewID: function(type) {
                return type + "_" + parseInt(Math.random() * 10000 + Math.random() * 10000);
            },
            //为科目文本框添加 匹配功能，触发事件
            InitAdjustItem: function(tr) {/*初始化科目*/

                var that = this;
                tr = tr ? $(tr) : $("#tab_list");
                var obj = tr.find("textarea[name='txt_Km_Xm']");
                obj.unbind();
                obj.autocomplete("/FinanceManage/Common/InAccount.aspx?" + $.param({
                    doType: "getAdjustItem"
                }), {
                    width: 120,
                    selectFirst: true,
                    blur: true
                });
                obj.result(function(e, data) {
                    var obj = $(this);
                    if (data[1] == "-1") {
                        obj.attr("data-faccountnum", "");
                        obj.val("");
                    }
                    else {
                        var hsList = $.parseJSON(data[3]).data, //核算项目 集合
                         cacheList = [],
                         pTr = obj.closest("tr");
                        obj.attr("data-FDetailId", data[1]); //科目编号
                        obj.attr("data-FAccountName", data[0]); //科目名称
                        obj.attr("data-FAccountNum", data[2]); //科目代码
                        pTr.find("input[name='hide_KmCode']").val(data[1] + "|" + data[2]);
                        pTr.find("input[name='hide_KmName']").val(data[0]);
                        if (hsList.length > 0) {
                            for (var i = 0; i < hsList.length; i++) {
                                hsList[i].ChkNm = "";
                            }
                        }
                        that.CreateHsTxt(hsList);
                        that.CreateJsonData();
                        that.BackTextAreaValue(obj, []);
                    }

                });
                obj.focus(function() {
                    var _self = InAccount.SelectTxt = $(this);
                    if ($.trim(_self.val()) != "") {
                        _self.val(_self.attr("data-FAccountNum"));
                        var hsList = $.parseJSON(_self.next().val()).data;
                        that.CreateHsTxt(hsList);
                    } else {
                        $("#div_MxContent").html("请输入科目!");
                    }

                    $("#tab_list").find("tr").css("background-color", "");
                    _self.closest("tr").css("background-color", "#FFF3BF");
                })
                obj.blur(function() {
                    var _self = $(this);
                    if ($.trim(this.value) != "") {
                        var data = $.parseJSON(_self.next().val()).data;
                        that.BackTextAreaValue(_self, data);
                    }
                });
            },
            //科目文本框离开焦点时 重新设置值
            BackTextAreaValue: function(txtObj, data) {
                var hsList = data || [], cacheStr = "";
                cacheStr = txtObj.attr("data-FAccountNum") + " - " + txtObj.attr("data-FAccountName");
                if (hsList.length > 0) {
                    for (var i = 0; i < hsList.length; i++) {
                        if (hsList[i].ChkNm != "") {
                            cacheStr += "/ " + hsList[i].ChkCd + " - " + hsList[i].ChkNm;
                        }
                    }
                }
                txtObj.val(cacheStr);
            },
            //创建 核算项目 文本框
            CreateHsTxt: function(data) {
                var cacheList = [], divBox = $($("#div_MxContent"));
                divBox.find("input[type='text']").unbind();
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        cacheList.push("<label>" + data[i].PreChkNm + ": </label><input id='" + this.MakeNewID("txt") + "' value='" + data[i].ChkNm + "' data-class='txt_Subject' data-ChkCate='" + data[i].ChkCate + "' data-ChkId='" + data[i].ChkId + "' data-ChkCd='" + data[i].ChkCd + "' data-PreChkNm='" + data[i].PreChkNm + "' class='inputtext formsize80' type='text'>&nbsp;&nbsp;");
                    }
                    divBox.html(cacheList.join(''));
                    this.InitSubject();
                } else {
                    divBox.html("该科目下没有核算项目!");
                }
            },
            //为核算项目 文本框设置自动匹配功能
            InitSubject: function() {
                var oL = $("#div_MxContent").find("input[type='text']");
                oL.each(function() {
                    var _self = $(this);
                    _self.autocomplete("/FinanceManage/Common/InAccount.aspx?" + $.param({
                        doType: "getSubject",
                        adjustitemid: _self.attr("data-ChkId"),
                        subjectType: _self.attr("data-ChkCate")
                    }), {
                        width: 150,
                        selectFirst: true,
                        blur: true
                    }).result(function(e, data) {
                        var obj = $(this);
                        if (data[1] == "-1") {
                            obj.val("");
                            obj.attr("data-ChkCd", "");
                        }
                        else {
                            obj.val($.trim(data[0].split('—')[1]));
                            obj.attr("data-ChkCd", data[1]);
                            obj.attr("data-ChkCate", data[2]);
                            obj.attr("data-ChkId", data[3]);
                            InAccount.CreateJsonData();
                        }
                    })
                })

                oL.blur(function() {
                    var _self = $(this);
                    if ($.trim(_self.val()) == "") {
                        _self.attr("data-ChkCd", "");
                        InAccount.CreateJsonData();
                        parent.tableToolbar._showMsg("请输入核算项目!");
                    }
                })
            },
            //为隐藏域创建核算项目json值
            CreateJsonData: function() {
                var str = '{"data":[', txt = $("#div_MxContent").find("input[type='text']");
                if (txt.length > 0) {
                    txt.each(function() {
                        var _self = $(this);
                        str += '{"ChkCate":' + _self.attr("data-ChkCate") + ',"ChkId":' + _self.attr("data-ChkId") + ',"ChkNm":"' + _self.val() + '","ChkCd":"' + _self.attr("data-ChkCd") + '","PreChkNm":"' + _self.attr("data-PreChkNm") + '"},';
                    })
                    str = str.substring(0, str.length - 1);
                }
                str += ']}';
                this.SelectTxt.next().val(str);
                var data = $.parseJSON(this.SelectTxt.next().val()).data;
                this.BackTextAreaValue(this.SelectTxt, data);
            },
            FormCheck: function() {
                var msg = { errorMsg: "", jPrice: 0, dPrice: 0 }, data = [], isMx;
                $("#tab_list").find("tr[class='tempRow'][data-fisexport='False']").each(function(i) {
                    var tr = $(this);
                    isMx = true;
                    data[0] = $.trim(tr.find("textarea[name='txt_Km_Xm']").attr("data-faccountnum")); //科目
                    data[1] = $.parseJSON($.trim(tr.find("input[name='hide_Xm']").val())).data; //核算项目
                    data[2] = parent.tableToolbar.getFloat(tr.find("input[name='txtJieFang']").val());
                    data[3] = parent.tableToolbar.getFloat(tr.find("input[name='txtDaiFang']").val());
                    msg.jPrice += data[2];
                    msg.dPrice += data[3];

                    if (data[0] == "") {
                        msg.errorMsg = "第" + (i + 1) + "行科目不能为空!";
                        return msg;
                    }

                    if (data[1].length > 0) {
                        for (var m = 0; m < data[1].length; m++) {
                            if (data[1][m].ChkNm == "" || data[1][m].ChkCd == "") {
                                msg.errorMsg = "第" + (i + 1) + "行类型为 (" + data[1][m].PreChkNm + ") 的核算项目不能为空!";
                                break;
                            }
                        }

                        if (msg.errorMsg != "") {
                            return msg;
                        }
                    }
                    if (data[2] > 0 && data[3] > 0) {
                        msg.errorMsg = "第" + (i + 1) + "行 借方和贷方不能同时大于0!";
                    }
                })

                return msg;
            },
            Save: function(obj) {
                var that = this;
                var obj = $(obj);
                obj.unbind("click")
                obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                obj.html("<s class=baochun></s>保存中...");
                var msg = that.FormCheck();
                if (msg.errorMsg.length > 0) {
                    parent.tableToolbar._showMsg(msg.errorMsg);
                    that.InitBtn();
                    return false;
                }
                if (msg.dPrice != msg.jPrice) {
                    parent.tableToolbar._showMsg("借方"+msg.jPrice+"和贷方"+msg.dPrice+"金额不相等!");
                    that.InitBtn();
                    return false;
                }
                $("tr[data-fisexport='True']").each(function() {
                    $(this).find("input[type='hidden']").remove();
                })
                $.newAjax({
                    type: "post",
                    data: $("form").serialize(),
                    cache: false,
                    url: "/FinanceManage/Common/InAccount.aspx?" + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', doType: "Save" }),
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('保存成功!', function() {
                                window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                            that.InitBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(parent.tableToolbar.errorMsg);
                        that.InitBtn();
                    }
                });

            },
            ShowSaveBtn: function() {
                if ($("#tab_list").find("tr[data-fisexport='False']").length > 0) {
                    $("#a_Save").show();
                } else {
                    $("#a_Save").hide();
                }
            }
        }
        $(function() {
            InAccount.Init();
        })
    </script>

</body>
</html>

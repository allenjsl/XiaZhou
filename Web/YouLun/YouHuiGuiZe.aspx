<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouHuiGuiZe.aspx.cs" Inherits="EyouSoft.Web.YouLun.YouHuiGuiZe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background:#fff">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" style="margin: 0 auto;">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <th align="left">
                    所有规则都满足按照:
                    <select id="ddlyouhuifangshi" name="ddlyouhuifangshi" class="inputselect" errmsg="请选择优惠规则!"
                                valid="required">
                                <option value="">请选择</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YouHuiGongXiangFangShi)), xiangyong)%>
                    </select>
                </th>
            </tr>
        </table>
        <table width="98%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" style="margin: 0 auto;">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td width="25" height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    优惠名称
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    优惠描述
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    优惠价格
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    优惠方式
                </td>
                <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    操作
                </td>
            </tr>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                <tbody class="tempRow">
                    <tr>
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            <input type="text" name="youhuimingcheng" value="" class="inputtext formsize80" errmsg="优惠名称不可为空!"
                                valid="required" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input type="text" name="youhuimiaoshu" value="" class="inputarea formsize350" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input type="text" name="youhuijiage" value="" class="inputtext formsize80" errmsg="优惠价格不可为空!|请填写正确的优惠价格"
                                valid="required|isMoney" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <select name="ddlfangshi" class="inputselect">
                                 <option value="0">按订单</option>
                                <option value="1">按人</option>
                            </select>
                        </td>
                        <td align="center" bgcolor="#FFFFFF" rowspan="2">
                            <a href="javascript:void(0)" class="addbtn">
                                <img src="../images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                    class="delbtn">
                                    <img src="../images/delimg.gif" width="48" height="20" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#FFFFFF" colspan="4">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" style="margin: 0 auto;">
                                <tr>
                                    <td>
                                        下单时间(提前天数)
                                    </td>
                                    <td>
                                        <select name="ddlxiadanshijian" class="inputselect">
                                            <option value="">请选择</option>
                                            <option value="1">></option>
                                            <option value="2">=</option>
                                            <option value="3"><</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input type="text" name="txtxiadanshijiant" value="" class="inputtext formsize80" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        下单时间(指定日期)
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="text" name="txtxiadanshijians" value="" class="inputtext formsize80"
                                            onfocus="WdatePicker()" />-<input type="text" name="txtxiadanshijiane" value="" class="inputtext formsize80"
                                                onfocus="WdatePicker()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        出港时间
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <input type="text" name="txtchugangshijians" value="" class="inputtext formsize80"
                                            onfocus="WdatePicker()" />-<input type="text" name="txtchugangshijiane" value=""
                                                class="inputtext formsize80" onfocus="WdatePicker()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        人数
                                    </td>
                                    <td>
                                        <select name="ddlrenshu" class="inputselect">
                                            <option value="">请选择</option>
                                            <option value="1">></option>
                                            <option value="2">=</option>
                                            <option value="3"><</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input type="text" name="txtrenshu" value="" class="inputtext formsize80" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        游客年龄
                                    </td>
                                    <td>
                                        <select name="ddlyoukenianling" class="inputselect">
                                            <option value="">请选择</option>
                                            <option value="1">></option>
                                            <option value="2">=</option>
                                            <option value="3"><</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input type="text" name="txtyoukenianling" value="" class="inputtext formsize80" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        游客区域
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td data-class="quyu">
                                        <%--<select id="ddlPrv" name="ddlPrv" class="inputselect ddlPrv noneDDL">
                                            <option value="">请选择</option>
                                            <option value="1">否</option>
                                            <option value="2">是</option>
                                        </select>
                                        <input type="hidden" value="" />--%>
                                        <%=GetQuYu()%><input type="hidden" value="" name="txtquyu" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        是否会员
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <select name="ddlchugangshijian" class="inputselect noneDDL">
                                            <option value="">请选择</option>
                                            <option value="1">否</option>
                                            <option value="2">是</option>
                                        </select>
                                        <input type="hidden" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        游客性别
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        <select name="ddlxingbie" class="inputselect">
                                            <option value="">请选择</option>
                                            <option value="0">男</option>
                                            <option value="1">女</option>
                                        </select>
                                        <input type="hidden" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        订单金额
                                    </td>
                                    <td>
                                        <select name="ddldingdanjine" class="inputselect">
                                            <option value="">请选择</option>
                                            <option value="1">></option>
                                            <option value="2">=</option>
                                            <option value="3"><</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input type="text" name="txtdingdanjine" value="" class="inputtext formsize80" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </asp:PlaceHolder>
            <asp:Repeater ID="rptlist" runat="server">
                <ItemTemplate>
                    <tbody class="tempRow">
                        <tr>
                            <td height="28" align="center" bgcolor="#FFFFFF">
                                <input type="text" name="youhuimingcheng" class="inputtext formsize80" value=" <%# Eval("MingCheng") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" name="youhuimiaoshu" class="inputarea formsize350" value=" <%# Eval("MiaoShu") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <input type="text" name="youhuijiage" class="inputtext formsize80" value=" <%# Eval("JinE","{0:F2}") %>" />
                            </td>
                            <td align="center" bgcolor="#FFFFFF">
                                <select name="ddlfangshi" class="inputselect">
                                    <%# getSelectFangShi(Eval("FangShi"))%>
                                </select>
                            </td>
                            <td align="center" bgcolor="#FFFFFF" rowspan="2">
                                <a href="javascript:void(0)" class="addbtn">
                                    <img src="../images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="delbtn">
                                        <img src="../images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" bgcolor="#FFFFFF" colspan="4">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="autoAdd" style="margin: 0 auto;">
                                    <tr>
                                        <td>
                                            下单时间(提前天数)
                                        </td>
                                        <td>
                                            <select name="ddlxiadanshijian" class="inputselect">
                                                <%# getSelectOpt(getJSModel(Eval("GuiZe").ToString()).XiaDanShiJianTiaoJian) %>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" name="txtxiadanshijiant" value="<%# getJSModel(Eval("GuiZe").ToString()).XiaDanShiJianTianShu%>"
                                                class="inputtext formsize80" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            下单时间(指定日期)
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <input type="text" name="txtxiadanshijians" value="<%# getJSModel(Eval("GuiZe").ToString()).XiaDanShiJianS%>"
                                                class="inputtext formsize80" onfocus="WdatePicker()" />-<input type="text" name="txtxiadanshijiane"
                                                    value="<%# getJSModel(Eval("GuiZe").ToString()).XiaDanShiJianE%>" class="inputtext formsize80"
                                                    onfocus="WdatePicker()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            出港时间
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <input type="text" name="txtchugangshijians" value="<%# getJSModel(Eval("GuiZe").ToString()).ChuGangShiJianS%>"
                                                class="inputtext formsize80" onfocus="WdatePicker()" />-<input type="text" name="txtchugangshijiane"
                                                    value="<%# getJSModel(Eval("GuiZe").ToString()).ChuGangShiJianE%>" class="inputtext formsize80"
                                                    onfocus="WdatePicker()" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            人数
                                        </td>
                                        <td>
                                            <select name="ddlrenshu" class="inputselect">
                                                <%# getSelectOpt(getJSModel(Eval("GuiZe").ToString()).RenShuTiaoJian) %>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" name="txtrenshu" value="<%# getJSModel(Eval("GuiZe").ToString()).RenShu%>"
                                                class="inputtext formsize80" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            游客年龄
                                        </td>
                                        <td>
                                            <select name="ddlyoukenianling" class="inputselect">
                                                <%# getSelectOpt(getJSModel(Eval("GuiZe").ToString()).YouKeNianLingTiaoJian) %>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" name="txtyoukenianling" value="<%#  getJSModel(Eval("GuiZe").ToString()).YouKeNianLing %>"
                                                class="inputtext formsize80" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            游客区域
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td data-class="quyu">
                                            <%--<select name="ddlPrv" class="inputselect ddlPrv noneDDL">
                                            </select>
                                            <input type="hidden" value="<%#  getJSModel(Eval("GuiZe").ToString()).YouKeQuYu  %>" />--%>
                                            <%#GetQuYu()%>
                                            <input type="hidden" value="<%#  getJSModel(Eval("GuiZe").ToString()).YouKeQuYu  %>" name="txtquyu" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            是否会员
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <select name="ddlchugangshijian" class="inputselect noneDDL">
                                                <option value="">请选择</option>
                                                <option value="1">否</option>
                                                <option value="2">是</option>
                                            </select>
                                            <input type="hidden" value="<%#  getJSModel(Eval("GuiZe").ToString()).ShiFouHuiYuan  %>" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            游客性别
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <select name="ddlxingbie" class="inputselect">
                                                <%# getSelectXingBie(getJSModel(Eval("GuiZe").ToString()).XingBie)%>
                                            </select>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            订单金额
                                        </td>
                                        <td>
                                            <select name="ddldingdanjine" class="inputselect">
                                                <%# getSelectOpt(getJSModel(Eval("GuiZe").ToString()).DingDanJinETiaoJian) %>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" name="txtdingdanjine" value="<%#  getJSModel(Eval("GuiZe").ToString()).DingDanJinE  %>"
                                                class="inputtext formsize80" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="alertbox-btn">
            <a id="btnSave" href="javascript:;" hidefocus="true"><s class="baochun"></s>保 存</a>
            <a href="javascript:;" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                <s class="chongzhi"></s>关 闭</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var pageDataJS = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' },


            Save: function() {
                KEditer.sync();
                pageDataJS.UnBindBtn();


                $("td[data-class='quyu']").each(function() {
                    var _quyu = [];
                    $(this).find("input[name='chkquyu']:checked").each(function() { _quyu.push($(this).attr("value")) });
                    $(this).find("input[name='txtquyu']").val(_quyu.join(','));
                });

                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "YouHuiGuiZe.aspx?save=save&" + $.param(pageDataJS.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = parent.window.location.href; });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            pageDataJS.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJS.BindBtn();
                    }
                })
            },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJS.Save();
                    } else {
                        return false;
                    }
                });
            }

        }


        $(document).ready(function() {
            //pcToobar.init({ pID: ".ddlPrv" });
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJS.Save();
                } else {
                    return false;
                }
            });


            $("td[data-class='quyu']").each(function() {
                var _s = $(this).find("input[name='txtquyu']").val();
                var _quyu = _s.split(",");

                for (var i = 0; i < _quyu.length; i++) {
                    $(this).find("input[name='chkquyu'][value='" + _quyu[i] + "']").attr("checked", "checked");
                }

            });

        })
        window.onload = function() {
            $(".noneDDL").each(function() {
                var i = $(this).closest("td").find("input").val();
                $(this).find("option[value=" + i + "]").attr("selected", "selected");
            })
        }
    </script>

</body>
</html>

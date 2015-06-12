<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetMoney.aspx.cs" Inherits="Web.FinanceManage.Common.SetMoney" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>收款</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: auto; text-align: center; font-size: 12px; color: #FF0000; font-weight: bold;
            padding-bottom: 5px;">
            应收金额：￥2,400.00 已收金额：￥0.00 未收金额：￥2,400.00
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin: 0 auto">
            <tr>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">序号</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT">收款日期</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT">收款人</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT">收款金额</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="fontred">*</span><span class="alertboxTableT">收款方式</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    <span class="alertboxTableT">开票</span>
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    开票金额
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    票据号
                </td>
                <td align="center" bgcolor="#B7E0F3">
                    审核状态
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    备注
                </td>
                <td height="25" align="center" bgcolor="#B7E0F3">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center">
                            1
                        </td>
                        <td align="center">
                            <label>
                                <input name="txt_setMoneyDate" type="text" value="2011-8-11" size="10" />
                            </label>
                        </td>
                        <td align="center">
                            <input name="txt_setMoneyMan" type="text" size="8" value="黄芳（小）" />
                            <input name="hd_setMoneyManId" type="hidden" />
                            <a href="javascript:;" class="xuanyong"></a>
                        </td>
                        <td align="center">
                            <input name="txt_setMoney" type="text" id="text3" value="￥0.00" size="10" />
                        </td>
                        <td align="center">
                            <label>
                                <select name="sel_setMoneyWay" style="width: 80px;">
                                    <option selected="selected">财务现收</option>
                                </select>
                            </label>
                        </td>
                        <td align="center">
                            <label>
                                <input name="chk_openTicket" type="checkbox" checked="checked" style="border: none;" />
                            </label>
                        </td>
                        <td align="center">
                            <input name="txt_openTicketMoney" type="text" id="text4" size="10" value="￥0.00" />
                        </td>
                        <td align="center">
                            <input name="txt_remark" type="text" id="textfield6" size="8" />
                        </td>
                        <td align="center">
                            已审
                        </td>
                        <td align="center">
                            <textarea name="textarea" id="textarea" style="height: 25px; width: 110px;"></textarea>
                        </td>
                        <td align="center">
                            <a href="#">审核</a><a href="#"> 修改</a><a href="#"> 删除 </a><a href="#" target="_blank">
                                打印</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td align="center">
                    1
                </td>
                <td align="center">
                    <label>
                        <input name="txt_setMoneyDate" type="text" value="2011-8-11" size="10" />
                    </label>
                </td>
                <td align="center">
                    <input name="txt_setMoneyMan" type="text" size="8" value="黄芳（小）" />
                    <input name="hd_setMoneyManId" type="hidden" />
                    <a href="javascript:;" class="xuanyong"></a>
                </td>
                <td align="center">
                    <input name="txt_setMoney" type="text" id="text3" value="￥0.00" size="10" />
                </td>
                <td align="center">
                    <label>
                        <select name="sel_setMoneyWay" style="width: 80px;">
                            <option selected="selected">财务现收</option>
                        </select>
                    </label>
                </td>
                <td align="center">
                    <label>
                        <input name="chk_openTicket" type="checkbox" checked="checked" style="border: none;" />
                    </label>
                </td>
                <td align="center">
                    <input name="txt_openTicketMoney" type="text" id="text4" size="10" value="￥0.00" />
                </td>
                <td align="center">
                    <input name="txt_remark" type="text" id="textfield6" size="8" />
                </td>
                <td align="center">
                    已审
                </td>
                <td align="center">
                    <textarea name="textarea" id="textarea" style="height: 25px; width: 110px;"></textarea>
                </td>
                <td align="center">
                    <a href="#">审核</a><a href="#"> 修改</a><a href="#"> 删除 </a><a href="#" target="_blank">
                        打印</a>
                </td>
            </tr>
            <%if (CheckGrant(Common.Enum.TravelPermission.账务管理_应收管理_收款登记))
              {%><tr>
                  <td align="center">
                      2
                  </td>
                  <td align="center">
                      <label>
                          <input name="txt_setMoneyDate" type="text" size="10" />
                      </label>
                  </td>
                  <td align="center">
                      <input name="txt_setMoneyMan" type="text" size="8" />
                      <input name="hd_setMoneyManId" type="hidden" />
                      <a href="javascript:;" class="xuanyong"></a>
                  </td>
                  <td align="center">
                      <input name="txt_setMoney" type="text" size="10" />
                  </td>
                  <td align="center">
                      <label>
                          <select name="sel_setMoneyWay" style="width: 80px;">
                              <option selected="selected">财务现收</option>
                          </select>
                      </label>
                  </td>
                  <td align="center">
                      <label>
                          <input name="chk_openTicket" type="checkbox" checked="checked" style="border: none;" />
                      </label>
                  </td>
                  <td align="center">
                      <input name="txt_openTicketMoney" type="text" size="10" />
                  </td>
                  <td align="center">
                      <input name="txt_remark" type="text" size="8" />
                  </td>
                  <td align="center">
                      已审
                  </td>
                  <td align="center">
                      <textarea name="textarea" style="height: 25px; width: 110px;"></textarea>
                  </td>
                  <td align="center">
                      <a href="javascript:;" hidefocus="true" onclick="Submit(this,'Add')"><s class="baochun">
                      </s>保 存</a>
                  </td>
              </tr>
            <%} %>
        </table>
        <div class="alertbox-btn">
            <a href="#" hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>
</body>

<script type="text/javascript">
    $(function() {
        var selectT = new newToobar();
        selectT.initMore({
            className: "xuanyong",
            hideName: "hd_setMoneyManId",
            showName: "txt_setMoneyMan",
            iframeUrl: "/CommonPage/OrderSells.aspx",
            title: "应收管理-收款-收款人",
            width: "700px",
            height: "400px"
        });
        $("[name='txt_name']").autocomplete("/ashx/GetOrderSells.ashx", {
            width: 130,
            selectFirst: true,
            blur: true
        }).result(function(e, data) {
            $(this).siblings("[name='hd_nameId']").val(data[1]);
        });
    })

    //提交
    function Submit(thiss, type) {

        //获取当前tr对象
        var obj = $(thiss).closest("tr");
        var ajaxdata = {
            txt_setMoneyDate: $(obj).find("[name='txt_setMoneyDate']").val(), //收款时间
            txt_setMoneyMan: $(obj).find("[name='txt_setMoneyMan']").val(), //收款人
            hd_setMoneyManId: $(obj).find("[name='hd_setMoneyManId']").val(), //收款人ID
            txt_setMoney: $(obj).find("[name='txt_setMoney']").val(), //收款金额
            sel_setMoneyWay: $(obj).find("[name='sel_setMoneyWay']").val(), //收款类型
            chk_openTicket: $(obj).find("[name='chk_openTicket']").attr("checked"), //是否开票
            txt_openTicketMoney: $(obj).find("[name='txt_openTicketMoney']").val(), //开票金额
            txt_remark: $(obj).find("[name='txt_remark']").val(), //备注
            iframeId: '<%=Request.QueryString["iframeId"] %>'//iframeId
        }
        GoAjax("/FinanceManage/Common/SetMoney.aspx", type, ajaxdata);

    }
    //ajax请求
    function GoAjax(url, msg, ajaxdata) {
        $.ajax({
            type: "get",
            data: $.param(ajaxdata),
            cache: false,
            url: url,
            dataType: "html",
            success: function(ret) {
                alert("执行了" + msg);
            },
            error: function() {
                //ajax异常--你懂得
                alert("服务器忙！");
            }
        });
        return false;
    }
    
</script>

</html>

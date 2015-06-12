<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ShowDetail.aspx.cs" Inherits="Web.FinanceManage.OneTeamAdjustAccounts.ShowDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="tablehead" style="background: none; margin-left: 5px; padding-top: 10px;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="javascript:();" hidefocus="true" class="ztorderform">
                    <span>团队复核</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:();" hidefocus="true" class="ztorderform">
                    <span>核算结束</span></a></li>
                <li><s class="orderformicon"></s><a href="javascript:();" hidefocus="true" class="ztorderform">
                    <span>退回计调</span></a></li>
            </ul>
        </div>
        <div class="addContent-box">
            <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                    <td align="right" class="addtableT">
                        线路名称：
                    </td>
                    <td colspan="5" class="kuang2">
                        张家界青山绿水三日游
                    </td>
                </tr>
                <tr>
                    <td align="right" class="addtableT">
                        团号：
                    </td>
                    <td class="kuang2">
                        HHL5-0001-0011
                    </td>
                    <td align="right" class="addtableT">
                        天数：
                    </td>
                    <td class="kuang2">
                        15
                    </td>
                    <td align="right" class="addtableT">
                        操作员：
                    </td>
                    <td class="kuang2">
                        张三
                    </td>
                </tr>
                <tr>
                    <td align="right" class="addtableT">
                        人数：
                    </td>
                    <td class="kuang2">
                        26+0+0
                    </td>
                    <td align="right" class="addtableT">
                        销售员：
                    </td>
                    <td align="right" class="kuang2">
                        李四
                    </td>
                    <td class="addtableT">
                        出团时间：
                    </td>
                    <td class="kuang2">
                        2011-4-5
                    </td>
                </tr>
            </table>
        </div>
        <div class="tablelist-box " style="width: 98.5%; margin: 0 auto;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <th rowspan="2" align="center" class="th-line">
                        团款收入
                    </th>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>订单号</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>客源单位</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>财务收款</b>
                    </td>
                    <td colspan="2" align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>导游收款</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>小计</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>备注</b>
                    </td>
                </tr>
                <tr>
                    <td height="29" align="center">
                        HHL5-0001-0011
                    </td>
                    <td align="center">
                        杭州易诺科技
                    </td>
                    <td align="center">
                        <input type="text" class="formsize40" value="1280" />
                    </td>
                    <td colspan="2" align="center">
                        <input type="text" class="formsize40" value="1000" />
                    </td>
                    <td align="center">
                        2280
                    </td>
                    <td align="center">
                        <input type="text" style="width: 300px;" />
                    </td>
                </tr>
                <tr>
                    <th rowspan="2" align="center" class="th-line">
                        其它收入<br />
                        <a href="javascript:();" onclick="Add('income')">
                            <img src="/images/addimg.gif" /></a>
                    </th>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>收入类别</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>收款单位</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>金额</b>
                    </td>
                    <td colspan="4" align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>备注</b>
                    </td>
                </tr>
                <tr>
                    <td height="29" align="center">
                        车费
                    </td>
                    <td align="center">
                        杭州易诺科技
                    </td>
                    <td align="center">
                        2280
                    </td>
                    <td colspan="4" align="center">
                        <input type="text" style="width: 300px;" />
                    </td>
                </tr>
                <tr>
                    <th rowspan="2" align="center" class="th-line">
                        团款支出
                    </th>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>支出类别</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>供应商</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>财务付款</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>导游现付</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>导游签单</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>小计</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>备注</b>
                    </td>
                </tr>
                <tr>
                    <td height="29" align="center">
                        车费
                    </td>
                    <td align="center">
                        杭州易诺科技
                    </td>
                    <td align="center">
                        <input type="text" class="formsize40" value="1280" />
                    </td>
                    <td align="center">
                        <input type="text" class="formsize40" value="1000" />
                    </td>
                    <td align="center">
                        <span class="th-line">导游签单</span>
                    </td>
                    <td align="center">
                        2280
                    </td>
                    <td align="center">
                        <input type="text" style="width: 300px;" />
                    </td>
                </tr>
                <tr>
                    <th rowspan="2" align="center" class="th-line">
                        其它支出<br />
                        <a href="javascript:();" onclick="Add('pay')">
                            <img src="/images/addimg.gif" /></a>
                    </th>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>支出类别</b>
                    </td>
                    <td colspan="3" align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>收款单位</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>金额</b>
                    </td>
                    <td colspan="2" align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>备注</b>
                    </td>
                </tr>
                <tr>
                    <td height="29" align="center">
                        车费
                    </td>
                    <td colspan="3" align="center">
                        杭州易诺科技
                    </td>
                    <td align="center">
                        2280
                    </td>
                    <td colspan="2" align="center">
                        <input type="text" style="width: 300px;" />
                    </td>
                </tr>
                <tr>
                    <th rowspan="2" align="center" class="th-line">
                        利润分配<br />
                        <a href="javascript:();" onclick="Add('distributeprofit')">
                            <img src="/images/addimg.gif" /></a>
                    </th>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>分配类别</b>
                    </td>
                    <td colspan="2" align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>金额</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>部门</b>
                    </td>
                    <td align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>人员</b>
                    </td>
                    <td colspan="2" align="center" bgcolor="#BCE0F1" class="th-line">
                        <b>备注</b>
                    </td>
                </tr>
                <tr>
                    <td height="29" align="center">
                        红利
                    </td>
                    <td colspan="2" align="center">
                        2280
                    </td>
                    <td align="center">
                        销售部
                    </td>
                    <td align="center">
                        15
                    </td>
                    <td colspan="2" align="center">
                        <input type="text" style="width: 300px;" />
                    </td>
                </tr>
                <tr>
                    <td height="29" colspan="8" align="right">
                        <strong class="fontred">毛利：10000 &nbsp; &nbsp; 毛利率：40%</strong>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_5">
        </div>
        <div class="mainbox cunline fixed">
            <ul>
                <li class="cun-cy"><a href="#">保存</a></li>
                <li class="quxiao-cy"><a href="">取消</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Add(type) {
            var data = new dataBoxy();

            switch (type) {
                case "income":
                    data.title += "收入" + Ping("Add");
                    data.url += "/AddDetail.aspx?toType=" + type;
                    break;
                case "pay":
                    data.title += "支出" + Ping("Add");
                    data.url += "/AddDetail.aspx?toType=" + type;
                    break;
                case "distributeprofit":
                    data.title = "利润分配" + Ping("Add");
                    data.url += "/AddDistributeProfit.aspx";
                    break
            }


            ShowBoxy(data);
            return false;
        }
        //显示弹窗
        function ShowBoxy(data) {
            Boxy.iframeDialog({
                iframeUrl: data.url,
                title: data.title,
                modal: true,
                width: data.width,
                height: data.height
            });
        }
        //弹窗默认参数
        function dataBoxy() {
            this.url = "/FinanceManage/OneTeamAdjustAccounts";
            this.title = "其他";
            this.width = "500px";
            this.height = "270px";
        };
    </script>

</asp:Content>

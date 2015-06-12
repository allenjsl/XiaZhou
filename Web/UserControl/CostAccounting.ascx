<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CostAccounting.ascx.cs"
    Inherits="Web.UserControl.CostAccounting" %>
<div class="addContent-box" id="divCostAcconting">
    <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
        <tbody>
            <%if (!this.IsSanPin && !this.IsRoute)
              { %>
            <tr>
                <td class="addtableT da">
                    成本核算：
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_CostCalculation"><%=CostCalculation%></textarea>
                </td>
            </tr>
            <%} %>
            <%if (this.IsSanPin && !this.IsRoute)
              { %>
            <tr>
                <td class="addtableT da">
                    服务标准：<a class="xuanyong" title="服务标准" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.服务标准 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_ServiceStandard"><%=ServiceStandard%></textarea>
                </td>
            </tr>
            <%} %>
            <tr>
                <td class="addtableT da">
                    不含项目：<a class="xuanyong" title="不含项目" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.不含项目 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_NoNeedItem"><%=NoNeedItem %></textarea>
                </td>
            </tr>
            <tr>
                <td class="addtableT da">
                    购物安排：<a class="xuanyong" title="购物安排" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.购物安排 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_ShoppingItem"><%=ShoppingItem%></textarea>
                </td>
            </tr>
            <tr>
                <td class="addtableT da">
                    儿童安排：<a class="xuanyong" title="儿童安排" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.儿童安排 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_ChildServiceItem"><%=ChildServiceItem%></textarea>
                </td>
            </tr>
            <tr>
                <td width="137" class="addtableT da">
                    自费项目：<a class="xuanyong" title="自费项目" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.自费项目 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_OwnExpense"><%=OwnExpense%></textarea>
                </td>
            </tr>
            <tr>
                <td width="137" height="14" class="addtableT da">
                    注意事项：<a class="xuanyong" title="注意事项" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.注意事项 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_NeedAttention"><%=NeedAttention %></textarea>
                </td>
            </tr>
            <tr>
                <td height="14" class="addtableT da">
                    温馨提醒：<a class="xuanyong" title="温馨提醒" href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.温馨提醒 %>"></a>
                </td>
                <td class="kuang2 pand4">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_WarmRemind"><%=WarmRemind %></textarea>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="hr_5">
    </div>
    <table width="100%" cellspacing="1" cellpadding="0" bgcolor="#5898B7" class="add-neibuxx">
        <tbody>
            <tr>
                <td class="addtableT da">
                    内部信息（内部注意事项，内部成本资料等）
                </td>
            </tr>
        </tbody>
    </table>
    <table width="100%" cellspacing="1" cellpadding="0">
        <tbody>
            <tr>
                <td class="addtableT da">
                    <textarea class="inputtext formsize800" style="height: 80px;" cols="3" rows="3" name="txt_CostAccounting_InsiderInfor"><%=InsiderInfor%></textarea>
                </td>
            </tr>
        </tbody>
    </table>
</div>

<script type="text/javascript">
    $(function() {
        newToobar.init({ box: "#divCostAcconting", className: "xuanyong", width: "700", height: "400", para: { sl: '<%=Request.QueryString["sl"] %>'} });
    });

    //    function CostFastFun(obj) {

    //    }

    //    function CallBackFun(obj) {

    //    }
</script>


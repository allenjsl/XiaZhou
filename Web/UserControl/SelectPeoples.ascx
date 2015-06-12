<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectPeoples.ascx.cs" Inherits="Web.UserControl.SelectPeoples" %>
<input type="text" errmsg="请输入<%=this.SetTitle %>!" valid="required" id="<%=this.ClientID %>_txtSellName"
    class="formsize80" name="<%=this.ClientID %>_txtSellName" value="<%=this.SellsName %>">
<input type="hidden" id="<%=this.ClientID %>_hideSellID" name="<%=this.ClientID %>_hideSellID"
    value="<%=this.SellsID %>" />
<a id="<%=this.ClientID %>_a_btn" class="xuanyong" href="javascript:void(0);"></a>

<script type="text/javascript">
    $(function() {
        var selectT = new selectToobar();
        selectT.init({ clickObj: $("#<%=this.ClientID %>_a_btn"), hideObj: $("#<%=this.ClientID %>_hideSellID"), showObj: $("#<%=this.ClientID %>_txtSellName"), iframeUrl: "/CommonPage/OrderSells.aspx", sModel: "<%=this.SMode %>", title: "<%=this.SetTitle %>", width: "650px", height: "430px", pModel: '<%=this.IsParentOpen== true?"2":""%>', otherPara: "pIframeId=<%=this.ParentIframeID %>" });
        $("#<%=this.ClientID %>_txtSellName").autocomplete("/ashx/GetOrderSells.ashx", {
            width: 130,
            selectFirst: true,
            blur: true
        }).result(function(e, data) {
            $("#hideID").val(data[1]);
        });
    })  
</script>
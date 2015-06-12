<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PickUpPoint.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.PickUpPoint" %>
<span id="span<%=this.SetPriv %>">
    <div class="shangche-didian fixed">
        <ul>
            <%=this.PickUpOptions %>
        </ul>
        <input data-class="pickuptourId" type="hidden" id="<%=this.PickUpTourIdClientID %>"
            name="<%=this.PickUpTourIdClientID %>" value="<%=this.PickUpTourId %>" />
        <input data-class="pickupId" type="hidden" id="<%=this.PickUpIDClientID %>" name="<%=this.PickUpIDClientID %>"
            value="<%=this.PickUpID %>" />
        <input data-class="pickupText" type="hidden" id="<%=this.PickUpTextClientID %>" name="<%=this.PickUpTextClientID %>"
            value="<%=this.PickUpText %>" />
        <input data-class="pickupPriceJ" type="hidden" id="<%=this.PickUpPriceJClientID %>"
            name="<%=this.PickUpPriceJClientID %>" value="<%=this.PickUpPriceJ %>" />
        <input data-class="pickupPriceS" type="hidden" id="<%=this.PickUpPriceSClientID %>"
            name="<%=this.PickUpPriceSClientID %>" value="<%=this.PickUpPriceS %>" />
        <input data-class="pickupDesc" type="hidden" id="<%=this.PickUpDescClientID %>" name="<%=this.PickUpDescClientID %>"
            value="<%=this.PickUpDesc %>" />
        <a href="javascript:void(0)" class="xuanyong" data-width="620" data-height="450"
            style="margin-top: 4px;" id="<%=this.SetPriv %>_a_btn">&nbsp;</a>
    </div>
</span>

<script type="text/javascript">
    $(function() {
        if ($("#<%=this.SetPriv %>_a_btn").length > 0) {
            $("#<%=this.SetPriv %>_a_btn").get(0).onclick = function() {
                window["pickUpData"] = {id: [], text: [], priceJ: [], priceS: [], desc: [] };
            }
        }
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/PickUpPoint.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", setTitle: "<%=this.SetTitle %>" }
        });
    })
</script>


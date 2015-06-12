<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SupplierControl.ascx.cs"
    Inherits="Web.UserControl.SupplierControl" %>
<input type="text" id="<%=ClientText %>" readonly="readonly" style="background-color:#dadada" class="inputtext formsize140" name="<%=ClientText %>" value="<%=this.Name %>"
    <%if(IsMust){ %><%=NoticeHTML %><%} %> />
<input type="hidden" id="<%=ClientValue %>" name="<%=ClientValue %>" value="<%=this.HideID %>" />
<input type="hidden" id="<%=ClientzyykValue %>" name="<%=ClientzyykValue %>" value="<%=this.HideID_zyyk %>" />
<input type="hidden" id="<%=ClientType %>" name="<%=ClientType %>" value="<%=this.SupplierType %>" />
<input type="hidden" id="<%=ClientFlag %>" name="<%=ClientFlag %>" value="<%=this.Flag %>" />
<input type="hidden" id="<%=ClientIsyukong %>" name="<%=ClientIsyukong %>" value="<%=this.isyukong %>" />
<a id="<%=btnID %>" class="xuanyong Offers" href="javascript:void(0);"></a>

<script type="text/javascript" language="javascript">
    $(function() {
        $("#<%=btnID %>").live("click", function() {
            var url = "/CommonPage/UseSupplier.aspx?";
            var hideObj =$("#<%=ClientValue %>");
            var hidezyykid=$("#<%=ClientzyykValue %>").attr("id");
            var showObj = $("#<%=ClientText %>").attr("id");
            var flag = "<%=Flag %>";
            var isyukong = $("#<%=ClientIsyukong %>").val();
            var type = "<%=SupplierType %>";
            url += $.param({ suppliertype: type, isyukong: isyukong, callBack: "<%=CallBack %>", ShowID: showObj, flag: flag,pIframeID: "<%=IframeID %>",zyykid:hidezyykid,tourid:"<%=TourID %>" })
            parent.Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择供应商",
                modal: true,
                width: "880",
                height: "378"
            });
        })

    })
    var ControlPage={
        GetVal: function() {
            return {
                SourceName: $("#<%=Name %>").val(),
                SourceType: $("#<%=SupplierType %>").val(),
                Flag:$("<%=Flag %>").val()
            };
        }
    }
</script>


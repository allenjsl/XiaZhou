<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeTongHao.ascx.cs" Inherits="EyouSoft.Web.UserControl.HeTongHao" %>
<input type="hidden" id="<%=HeTongIdClientID %>" name="<%=HeTongIdClientID %>" value="<%=HeTongId %>" />
<input type="text" class="inputtext formsize120" id="<%=HeTongCodeClientID %>" name="<%=HeTongCodeClientID %>"
    value="<%=HeTongCode %>" i_yuancode="<%=HeTongCode %>" />
    
<script type="text/javascript">
    $(document).ready(function() {
        $("#<%=HeTongCodeClientID %>").autocomplete("/ashx/handler.ashx?dotype=GetHeTongHao", {
            width: 130,
            selectFirst: true,
            blur: true,
            extraParams: { yuanhetongid: $("#<%=HeTongIdClientID %>").val() }
        }).result(function(e, data) {
            if (data.length > 1 && data[1] != "") {
                $("#<%=HeTongCodeClientID %>").val(data[0]).attr("i_yuancode", data[0]);
                $("#<%=HeTongIdClientID %>").val(data[1]);
            } else {
                $("#<%=HeTongCodeClientID %>").val("").attr("i_yuancode", "");
                $("#<%=HeTongIdClientID %>").val("");
            }
        });

        $("#<%=HeTongCodeClientID %>").blur(function() {
            var _$obj = $(this);
            if (_$obj.attr("i_yuancode") != _$obj.val()) {
                _$obj.val("");
                $("#<%=HeTongIdClientID %>").val("");
            }
        });
    });
</script>

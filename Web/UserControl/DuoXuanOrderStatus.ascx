<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuoXuanOrderStatus.ascx.cs" Inherits="EyouSoft.Web.UserControl.DuoXuanOrderStatus" %>
<input type="hidden" name="<%=ZTIdClientID %>" id="<%=ZTIdClientID %>" />
<input type="text" name="<%=ZTNameClientID %>" id="<%=ZTNameClientID %>" class="inputtext formsize120"
    readonly="readonly" style="background: #dadada;" />
<a class="xuanyong" href="javascript:void(0)" id="<%=AClientID %>">&nbsp;</a>

<script type="text/javascript">
    window["<%=ClientID %>"] = {
        //选用按钮事件
        xuanYong: function() {
            Boxy.iframeDialog({ iframeUrl: "/commonpage/duoxuanorderstatus.html?ClientID=<%=ClientID %>", title: "订单状态", modal: true, width: "500px", height: "200px" });
        },
        //设置值 _v:{value:"",text:""}
        setValue: function(_v) {
        $("#<%=ZTIdClientID %>").val(_v.value);
        $("#<%=ZTNameClientID %>").val(_v.text).attr("title", _v.text);
        },
        //获取值 return:{value:"",text:""}
        getValue: function() {
        return { value: $("#<%=ZTIdClientID %>").val(), text: $("#<%=ZTNameClientID %>").val() };
        }
    };

    $(document).ready(function() {
        $("#<%=AClientID %>").click(function() { window["<%=ClientID %>"].xuanYong(); });
        $("#<%=ZTNameClientID %>").click(function() { window["<%=ClientID %>"].xuanYong(); });
    });
    
</script>


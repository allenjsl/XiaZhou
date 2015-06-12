<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuoXuanTourStatus.ascx.cs" Inherits="EyouSoft.Web.UserControl.DuoXuanTourStatus" %>

<input type="hidden" name="<%=ValueClientID %>" id="<%=ValueClientID %>" />
<input type="text" name="<%=TextClientID %>" id="<%=TextClientID %>" class="inputtext formsize120"
    readonly="readonly" style="background: #dadada;" />
<a class="xuanyong" href="javascript:void(0)" id="<%=AClientID %>">&nbsp;</a>

<script type="text/javascript">
    window["<%=ClientID %>"] = {
        //选用按钮事件
        xuanYong: function() {
            Boxy.iframeDialog({ iframeUrl: "/commonpage/duoxuantourstatus.html?ClientID=<%=ClientID %>", title: "团队状态", modal: true, width: "500px", height: "200px" });
        },
        //设置值 _v:{value:"",text:""}
        setValue: function(_v) {
            $("#<%=ValueClientID %>").val(_v.value);
            $("#<%=TextClientID %>").val(_v.text).attr("title", _v.text);
        },
        //获取值 return:{value:"",text:""}
        getValue: function() {
            return { value: $("#<%=ValueClientID %>").val(), text: $("#<%=TextClientID %>").val() };
        }
    };

    $(document).ready(function() {
        $("#<%=AClientID %>").click(function() { window["<%=ClientID %>"].xuanYong(); });
        $("#<%=TextClientID %>").click(function() { window["<%=ClientID %>"].xuanYong(); });
    });   
    
</script>

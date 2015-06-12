<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysXuanYong.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysXuanYong" %>

<input type="hidden" name="<%=GysIdClientID %>" id="<%=GysIdClientID %>" />
<input type="text" name="<%=GysNameClientID %>" id="<%=GysNameClientID %>" class="inputtext formsize120"
    readonly="readonly" style="background: #dadada;" />
<a class="xuanyong" href="javascript:void(0)" id="<%=AClientID %>">&nbsp;</a>

<script type="text/javascript">
    window["<%=ClientID %>"] = {
        //选用按钮事件
        xuanYong: function() {
            var _data = this.getValue();
            top.Boxy.iframeDialog({ iframeUrl: '/commonpage/GysXuanYong.aspx?ClientID=<%=ClientID %>&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&pIframeId=<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>', title: "供应商选用", modal: true, width: "800px", height: "500px", data: _data });
        },
        //设置值 _v:{gysId:"",gysName:""}
        setValue: function(_v) {
            $("#<%=GysIdClientID %>").val(_v.gysId);
            $("#<%=GysNameClientID %>").val(_v.gysName).attr("title", _v.gysName);
        },
        //获取值 return:{gysId:"",gysName:""}
        getValue: function() {
            return { gysId: $("#<%=GysIdClientID %>").val(), gysName: $("#<%=GysNameClientID %>").val() };
        }
    };

    $(document).ready(function() {
        $("#<%=AClientID %>").click(function() { window["<%=ClientID %>"].xuanYong(); });
        $("#<%=GysNameClientID %>").click(function() { window["<%=ClientID %>"].xuanYong(); });
    });   
    
</script>


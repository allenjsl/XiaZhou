<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="selectGuid.ascx.cs"
    Inherits="Web.UserControl.selectGuid" %>
<span id="span<%=this.SetPriv %>">
    <input type="text" errmsg="请输入<%=this.SetTitle %>!"  valid="required" id="<%=this.GuidNameClient %>"
        class="inputtext formsize80" name="<%=this.GuidNameClient %>" value="<%=this.GuidName %>">
    <input type="hidden" id="<%=this.GuidIDClient %>" name="<%=this.GuidIDClient %>"
        value="<%=this.GuidID %>" />
    <a id="<%=this.SetPriv %>_a_btn" title="<%=this.SetTitle %>" data-width="720px" data-height="450px"
        class="xuanyong" href="/CommonPage/SelectGuid.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>"></a></span>

<script type="text/javascript">
    $(document).ready(function() {
        newToobar.init({ box: "#span<%=this.SetPriv %>", className: "xuanyong", callBackFun: '<%=CallBackFun %>', para: { sModel: '<%=this.SMode?"2":"1" %>', pIframeId: "<%=this.ParentIframeID %>"} });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.GuidNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        $("#<%=this.GuidNameClient %>").blur(function() {
            if ($.trim($(this).val()) == "") {
                $(this).parent().find("input[type='hidden']").val("");
            }
        });
    });
</script>


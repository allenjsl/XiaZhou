<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectObject.ascx.cs"
    Inherits="Web.UserControl.SelectObject" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.SelectNameClient %>" class="inputtext formsize120" name="<%=this.SelectNameClient %>"
        type="text" value="<%=this.ObjectName %>" />
    <input type="hidden" id="<%=this.SelectIDClient %>" name="<%=this.SelectIDClient %>"
        value='<%=this.ObjectID %>' />
    <a href="javascript:void(0)" class="xuanyong" data-width="550" data-height="260"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a> </span>

<script type="text/javascript">
    $(function() {
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/SelectObject.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", sModel: "<%= this.SModel %>" }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.SelectNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.SelectNameClient %>").keydown(function() {
            $(this).parent().find("input[type='hidden']").val("");
        })
    })
</script>


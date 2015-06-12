<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LineAreaSelect.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.LineAreaSelect" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.LineAreaSelectNameClient %>" errmsg="请输入<%=this.SetTitle %>!"
        data-old="<%=this.LineAreaSelectName %>" valid="required" class="inputtext formsize120"
        name="<%=this.LineAreaSelectNameClient %>" type="text" value="<%=this.LineAreaSelectName %>" />
    <input type="hidden" id="<%=this.LineAreaSelectIDClient %>" name="<%=this.LineAreaSelectIDClient %>"
        value='<%=this.LineAreaSelectID %>' />
    <a href="javascript:void(0)" class="xuanyong" data-width="600" data-height="410"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a> </span>

<script type="text/javascript">
    $(function() {
        window["selectoption"] = { value: [], text: [] };
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/LineAreaSelect.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", sModel: "<%=this.SModel %>" }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.LineAreaSelectNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.LineAreaSelectNameClient %>").change(function() {
            $(this).parent().find("input[type='hidden']").val("");
        })
    })
</script>


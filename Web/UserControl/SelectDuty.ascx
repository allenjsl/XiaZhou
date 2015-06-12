<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectDuty.ascx.cs"
    Inherits="Web.UserControl.SelectDuty" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.DutyNameClient %>" class="inputtext formsize120" name="<%=this.DutyNameClient %>"
        data-old="<%=this.DutyName %>" type="text" value="<%=this.DutyName %>" />
    <input type="hidden" id="<%=this.DutyIDClient %>" name="<%=this.DutyIDClient %>"
        value='<%=this.DutyID %>' />
    <a href="javascript:void(0)" class="xuanyong" data-width="680" data-height="300"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a> </span>

<script type="text/javascript">
    $(function() {
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/ManageCenter/Hr/SelectDuty.aspx",
            title: "<%=this.SetTitle %>",
            para: { pIframeId: "<%=this.ParentIframeID %>", sModel: "<%=this.SModel %>", setTitle: "<%=this.SetTitle %>" }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.DutyNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.DutyNameClient %>").change(function() {
            $(this).parent().find("input[type='hidden']").val("");
        })
    })
</script>


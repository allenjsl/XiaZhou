<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HrSelect.ascx.cs" Inherits="Web.UserControl.HrSelect" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.HrSelectNameClient %>" errmsg="请选择<%=this.SetTitle %>!" valid="required"
        class="inputtext formsize120" name="<%=this.HrSelectNameClient %>" type="text"
        value="<%=this.HrSelectName %>" />
    <input type="hidden" id="<%=this.HrSelectIDClient %>" name="<%=this.HrSelectIDClient %>"
        value='<%=this.HrSelectID %>' />
        
    <a href="javascript:void(0)" class="xuanyong" data-width="500" data-height="300"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a><span data-class="hideDeptInfo"
            data-deptid="<%=this.ClientDeptID %>" data-deptname="<%=this.ClientDeptName %>">
        </span> </span>

<script type="text/javascript">
    $(function() {
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/HrSelect.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", isShow: '<%=Isshow %>', sModel: "<%=this.SModel %>", setTitle: "<%=this.SetTitle %>" }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.HrSelectNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        if ("<%=this.IsDisplay.ToString().ToLower() %>" == "true") {
            $("#span<%=this.SetPriv %>").find("a").css("display", "none");
        }
        if ('<%=this.IsValid.ToString().ToLower() %>' == "false") {
            $("#<%=this.HrSelectNameClient %>").removeAttr("valid");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.HrSelectNameClient %>").change(function() {
            $(this).parent().find("input[type='hidden']").val("");
        })
    })
</script>


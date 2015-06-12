<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CuisineSelect.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.CuisineSelect" %>
<span id="span<%=this.SetPriv %>">
    <input id="<%=this.CuisineSelectNameClient %>" data-old="<%=this.CuisineSelectName %>"
        errmsg="请选择<%=this.SetTitle %>!" valid="required" class="inputtext formsize80"
        name="<%=this.CuisineSelectNameClient %>" type="text" value="<%=this.CuisineSelectName %>" /><font color="#FF0000">*</font>
    <input type="hidden" id="<%=this.CuisineSelectIDClient %>" name="<%=this.CuisineSelectIDClient %>"
        value='<%=this.CuisineSelectID %>' />
    <a href="javascript:void(0)" class="xuanyong" data-width="500" data-height="270"
        id="<%=this.SetPriv %>_a_btn">&nbsp;</a> </span>

<script type="text/javascript">
    $(function() {
        newToobar.init({
            box: "#span<%=this.SetPriv %>",
            className: "xuanyong",
            iframeUrl: "/CommonPage/CuisineSelect.aspx",
            title: "<%=this.SetTitle %>",
            callBackFun: '<%=CallBackFun %>',
            para: { pIframeId: "<%=this.ParentIframeID %>", sModel: "<%=this.SModel %>" }
        });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.CuisineSelectNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
        }
        //文本框的值被改变了就清空隐藏域的值
        $("#<%=this.CuisineSelectNameClient %>").change(function() {

            $(this).parent().find("input[type='hidden']").val("");

        })
    })
</script>


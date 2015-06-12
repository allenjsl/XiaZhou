<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YLCompanySelect.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.YLCompanySelect" %>
<span id="span<%=this.SetPriv %>">
    <input type="text" errmsg="请输入<%=this.SetTitle %>!" valid="required" id="<%=this.CompNameClient %>"
        class="inputtext formsize80" name="<%=this.CompNameClient %>" value="<%=this.CompName %>"
        readonly="readonly">
    <input type="hidden" id="<%=this.CompIDClient %>" name="<%=this.CompIDClient %>"
        value="<%=this.ComPid %>" errmsg="<%=this.SetTitle %>输入无效,请重新输入或选择<%=this.SetTitle %>!"
        valid="required" />
    <%if (IsShowSelect)
      { %>
    <a id="<%=this.SetPriv %>_a_btn" title="<%=this.SetTitle %>" data-width="850" data-height="550"
        class="xuanyong youlunxuanyong" href="javascript:;"></a>
    <%} %>
</span>

<script type="text/javascript">
    $(function() {
    $(".youlunxuanyong").click(function() {
            var win = top || window;
            win.Boxy.iframeDialog({
            iframeUrl: '/YouLun/Commpage/YLCompanySelect.aspx?' + $.param({ id: $(this).attr("id"), pIframeId: "<%=this.ParentIframeID %>", CallBackFun: "<%=this.CallBackFun %>", t: '<%=T1%>' }),
                title: '选用',
                modal: true,
                width: '850',
                height: '550'
            });
        })

    })  
</script>


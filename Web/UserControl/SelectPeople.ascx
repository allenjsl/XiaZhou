<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectPeople.ascx.cs" Inherits="Web.UserControl.SelectPeople"%>
<input name="txtShowID" type="text" class="formsize120" id="txtShowID" runat="server"/>
<input type="hidden"  name="hideID" id="hideID" value='<%=this.UserID%>'/>
<a href="javascript:void(0);" class="xuanyong" id="linkSelectPeople">&nbsp;</a>
 <script type="text/javascript">
     $(function() {
        var selectT = new selectToobar();
        selectT.init({ clickObj: $("#linkSelectPeople"), hideObj: $("#hideID"), showObj: $('#<%=this.getTxtShowID%>'), iframeUrl: "/CommonPage/SelectPeople.aspx", sModel: "2", title: "选择员工", width: "650px", height: "600px" });
        $('#<%=this.getTxtShowID%>').autocomplete("/ashx/GetPeople.ashx", {
             width: 130,
             selectFirst: true,
             blur: true,
         }).result(function(e, data) {
             $("#hideID").val(data[1]);
         });
     })
</script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdviceAdd.aspx.cs" Inherits="Web.ManageCenter.Advice.AdviceAdd" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script type="text/javascript" src="/js/table-toolbar.js"></script>
    <style type="text/css">
        .errmsg{color:#f00;font-size:12px;}
    </style>
</head>
<body style="background: 0 none;">
    <form id="form1" runat="server"  enctype="multipart/form-data">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="13%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>标&nbsp;&nbsp;&nbsp; 题：
                </td>
                <td width="87%" height="28" align="left" bgcolor="#e0e9ef">
                    <input name="txtTitle" type="text" class="formsize120" id="txtTitle" valid="required" errmsg="标题不能为空！" runat="server"/>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    内&nbsp;&nbsp;&nbsp; 容：
                </td>
                <td height="65" align="left" bgcolor="#e0e9ef">
                    <textarea name="txtContent" class="formsize450" id="txtContent" style="height: 60px;" runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件上传：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:FileUpload runat="server" ID="txtFile" Height="22"   valid="filter" allow="txt,doc,docx,xls,xlsx,jpg,bmp,gif,jpeg,rar,zip" errmsg="附件格式不正确！"/>
                    <asp:Label runat="server" ID="lbFiles"  CssClass="labelFiles"></asp:Label><br />
                    <span style="color:#555;font-size:12px;">[仅限：txt,doc,docx,xls,xlsx,jpg,bmp,gif,jpeg,rar,zip]</span>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>接 收 人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <uc1:SellsSelect ID="SellsSelect1" runat="server" SMode="2"/>
                </td>
            </tr>
            <%if (String.Equals(Request.QueryString["dotype"], "update", StringComparison.InvariantCultureIgnoreCase))
              {
                  if (this.CheckGrant(Common.Enum.TravelPermission.行政中心_意见建议箱_处理))
                  {%>
            <tr>
                <td width="13%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    处理意见：
                </td>
                <td width="87%" height="45" align="left" bgcolor="#e0e9ef">
                    <textarea name="txtHandleContent" class="formsize450" id="txtHandleContent" style="height: 40px;"  runat="server"></textarea>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    处理时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <input name="txtTime" type="text" class="formsize120" id="txtTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"  runat="server"/>
                </td>
            </tr>
            <%}
              } %>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否公开：
                </td>
                <td height="46" align="left" bgcolor="#e0e9ef">
                    <asp:RadioButtonList runat="server" ID="rdIsOpen" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Text="公开" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="0" Text="不公开"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    提 交 人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label runat="server" ID="submitName"></asp:Label>
                    <input type="checkbox" name="ckHide" id="ckHide"  runat="server"/>
                    匿名&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>提交时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <input name="txtSubmitTime" type="text" class="formsize120" id="txtSubmitTime" valid="required" errmsg="提交时间不能为空！"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"  runat="server"/>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <asp:LinkButton ID="btnSubmit" runat="server" hidefocus="true"  OnClick="btnClick"><s class="baochun"></s>保 存</asp:LinkButton>
            <a href="javascript:resetForm();" hidefocus="true"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="/js/jquery.boxy.js"></script>
    <script type="text/javascript" src="/js/jquery.blockUI.js"></script>    
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>    
    <script type="text/javascript">
        $(function() {
            $("#jyx_xz").click(function() {
                var url = $(this).attr("href");
                parent.Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "选择接收人",
                    modal: true,
                    width: "500px",
                    height: "522px"
                });
                return false;
            });
        })
        $(function() {
            $("#btnSubmit").click(function() {
                var form = $(this).closest("form").get(0);
                //点击按纽触发执行的验证函数
                return ValiDatorForm.validator(form, "alert");
            });
            //初始化表单元素失去焦点时的行为，当需验证的表单元素失去焦点时，验证其有效性。
            FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
        })
        function resetForm() {
            document.getElementById('form1').reset();
        }
        function delFile(n) {
            tableToolbar.ShowConfirmMsg("您确定要删除该附件吗？", function() {
                $(".labelFiles .divFile_" + n).hide();
                $(".labelFiles #hideFileInfo_" + n).val("");
            });
        }
    </script>    
</body>
</html>
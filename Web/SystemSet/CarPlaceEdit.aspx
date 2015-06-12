<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarPlaceEdit.aspx.cs" Inherits="EyouSoft.Web.SystemSet.CarPlaceEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
</head>
<body style="background:0 none;">
    <form id="form1" runat="server">
      <div class="alertbox-outbox02">
          <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" style="margin:0 auto">
             <tr>
                 <td width="14%" height="28" align="right" class="alertboxTableT">
                   <font class="fontbsize12">* </font>上车地点：
                 </td>
                 <td height="28" colspan="3" align="left">
                   <asp:TextBox ID="txtCarPlace" runat="server" calss="formsize180" CssClass="inputtext formsize180" valid="required" errmsg="上车地点不能为空!"></asp:TextBox>
                 </td>
              </tr>
              <tr>
                 <td align="right" class="alertboxTableT">
                   <font class="fontbsize12">* </font>接价：
                 </td>
                 <td width="38%" align="left">
                   <asp:TextBox ID="txtJiePrice" runat="server" calss="formsize80" CssClass="inputtext formsize80" valid="required|isMoney" errmsg="接价不能为空！|接价格式有误！"></asp:TextBox>
                 </td>
                 <td width="10%" align="right">
                   <font class="fontbsize12">* </font>送价：
                 </td>
                 <td width="38%" align="left">
                   <asp:TextBox ID="txtSongPrice" runat="server" calss="formsize80" CssClass="inputtext formsize80" valid="required|isMoney" errmsg="送价不能为空！|送价格式有误！"></asp:TextBox>
                 </td>
              </tr>
              <tr>
                 <td align="right" class="alertboxTableT">
                   描述：
                 </td>
                 <td colspan="3" align="left">
                   <asp:TextBox ID="txtDescribe" Style=" width: 280px;" class="inputtext formsize450"
                            Height="60px" runat="server" TextMode="MultiLine"></asp:TextBox>
                 </td>
              </tr>
              <tr>
                 <td align="right" class="alertboxTableT">
                   状态：
                 </td>
                 <td colspan="3" align="left">
                   <asp:RadioButton ID="rbtnStatus1" GroupName="Statu" Style="border: none;" Checked="true" runat="server" Text="启用" />
                   <asp:RadioButton ID="rbtnStatus0" GroupName="Statu" Style="border: none;" runat="server" Text="禁用" />
                 </td>
              </tr>
           </table> 
          <div class="alertbox-btn" style="text-align: center;"><a href="javascript:" hidefocus="true" id="btnSave"><s class="baochun"></s>保 存</a><a href="javascript:" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关 闭</a>
          </div>
      </div>
    </form>
    <script type="text/javascript">
        var CarPlaceEdit = {
            Params:{sl:'<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',memuid:'<%=EyouSoft.Common.Utils.GetQueryStringValue("memuid") %>',id:'<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>'},
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position","0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position","0-28px").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form=$(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form","parent")) {
                      CarPlaceEdit.Save();
                    }else
                    {
                      return false;
                    } 
                });
            },
            Save:function () {
                   CarPlaceEdit.UnBindBtn();
                   $.newAjax({
                      type:"post",
                      cache:false,
                      url:"/SystemSet/CarPlaceEdit.aspx?dotype=save&"+$.param(CarPlaceEdit.Params),
                      data:$("#btnSave").closest("form").serialize(),
                      dataType:"json",
                      success:function(ret){
                         if (ret.result=="1") {
                            parent.tableToolbar._showMsg(ret.msg,function () {parent.window.location.href="/SystemSet/CarPlaceList.aspx?"+$.param(CarPlaceEdit.Params);});
                         }else{
                            parent.tableToolbar._showMsg(ret.msg);
                            CarPlaceEdit.BindBtn();
                         }
                      },
                      error:function () {
                         parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                         CarPlaceEdit.BindBtn();
                      }
                   });
              }
        };
        $(function () {
           $("#btnSave").click(function () {
           var form = $(this).closest("form").get(0);
            if (ValiDatorForm.validator(form, "parent")) {
               CarPlaceEdit.Save();
            }else{
               return false; 
            }              
           });
        });
    </script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarTypeEdit.aspx.cs" Inherits="EyouSoft.Web.SystemSet.CarTypeEdit" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:0 none;">
<form id="form1" runat="server">
   <div class="alertbox-outbox02">
      <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" style="margin:0 auto">
        <tr>
          <td width="9%" height="28" align="right" class="alertboxTableT">
            <font class="fontbsize12">* </font>车型名称：
          </td>
          <td width="35%" height="28" align="left">
              <asp:TextBox ID="txtCarType" class="formsize180" CssClass="inputtext formsize180" runat="server" valid="required" errmsg="车型名称不能为空！"></asp:TextBox>
          </td>
        </tr>
        <tr>
          <td align="right" class="alertboxTableT">
            <font class="fontbsize12">* </font>座位数：
          </td>
          <td align="left">
              
              <asp:DropDownList ID="ddlCarSeatNum" runat="server" CssClass="inputselect">
              </asp:DropDownList>
              <a href="/SystemSet/CarSeatSet.aspx?sl=<%=Utils.GetQueryStringValue("sl") %>&memuid=<%=Utils.GetQueryStringValue("memuid") %>&id=<%=Utils.GetQueryStringValue("id") %>&piframeId=<%=Request.QueryString["iframeId"]%>"  class="zuoweishz" id="zuowei_shezhi">座位设置</a>
              <input id="templateId" type="hidden" value="" runat="Server" />
              <input id="oldseatnum" type="hidden" value="" runat="Server" />
              
          </td>
        </tr>
        <tr>
          <td align="right" class="alertboxTableT">
            描述：
          </td>
          <td align="left">
             <asp:TextBox ID="txtDescribe" Style=" width: 240px;" class="inputtext formsize450"
                            Height="60px" runat="server" TextMode="MultiLine"></asp:TextBox>
          </td>
        </tr>
      </table>
      <div class="alertbox-btn" style="text-align: center;"><a href="javascript:" hidefocus="true" id="btnSave"><s class="baochun"></s>保 存</a><a href="javascript:"hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s class="chongzhi"></s>关 闭</a>
      </div>
   </div>
</form>
<script type="text/javascript">
    var CarTypeEdit = {
        Params: { sl: '<%=Utils.GetQueryStringValue("sl") %>', memuid: '<%=Utils.GetQueryStringValue("memuid") %>', id: '<%=Utils.GetQueryStringValue("id") %>' },
        UnBindBtn: function() {
            $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
        },
        //按钮绑定事件
        BindBtn: function() {
            $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator("form", "parent")) {
                    CarTypeEdit.Save();
                }
                return false;
            });
        },
        Init: function() {
            if ($("#templateId").val() == "") {
                CarTypeEdit.Change();
            }
        },
        Change: function() {
            var newseatnum = $("#<%=ddlCarSeatNum.ClientID %> :selected").text();
            $.newAjax({
                type: "get",
                cache: false,
                url: "/SystemSet/CarTypeEdit.aspx?dotype=gettemplateid&" + $.param(CarTypeEdit.Params) + "&newseatnum=" + newseatnum,
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        var data = ret.obj;
                        if (data.length > 0) {
                            for (var i = 0; i < data.length; i++) {
                                //把第一个的模板编号赋值给隐藏域
                                $("#templateId").val(data[i].TemplateId);
                                break;
                            }
                        }
                    } else {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                    }
                },
                error: function() {
                    parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                }
            });
        },
        Save: function() {
            CarTypeEdit.UnBindBtn();
            $.newAjax({
                type: "post",
                cache: false,
                url: "/SystemSet/CarTypeEdit.aspx?dotype=save&ddlCarSeatNum=" + $("#<%=ddlCarSeatNum.ClientID %> :selected").text() + "&" + $.param(CarTypeEdit.Params),
                data: $("#btnSave").closest("form").serialize(),
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = "/SystemSet/CarTypeList.aspx?" + $.param(CarTypeEdit.Params); });
                    } else {
                        parent.tableToolbar._showMsg(ret.msg);
                        CarTypeEdit.BindBtn();
                    }
                },
                error: function() {
                    parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                    CarTypeEdit.BindBtn();
                }
            });
        }
    };
    $(function() {
        CarTypeEdit.Init();
        $("#btnSave").click(function() {
            var form = $(this).closest("form").get(0);
            var newseatnum = $("#<%=ddlCarSeatNum.ClientID %> :selected").text();
            var oldseatnum = $("#oldseatnum").val();
            if (ValiDatorForm.validator(form, "parent")) {
                //判断是否选择的座位数
                if (newseatnum == "") { parent.tableToolbar._showMsg("请先选择座位数！"); return false; }
                if (oldseatnum == "" || newseatnum == oldseatnum) {
                    CarTypeEdit.Save();
                } else {
                    parent.tableToolbar.ShowConfirmMsg("您选择的座位数与模板座位数不一致，确定要保存吗？", function() {
                        CarTypeEdit.Save();
                    })
                }
            } else {
                return false;
            }

        });
        $("#ddlCarSeatNum").change(function() {
            CarTypeEdit.Change();
        });
        $("#zuowei_shezhi").click(function() {
            var ddlCarSeatNum = $("#<%=ddlCarSeatNum.ClientID %> :selected").text();
            var url = $(this).attr("href");
            url += "&seatnum=" + ddlCarSeatNum;
            url += "&templateid=" + $("#templateId").val();
            parent.Boxy.iframeDialog({
                iframeUrl: url,
                title: "座位设置",
                modal: true,
                width: "930px",
                height: "540px"
            });
            return false;
        });
    })
</script>
</body>
</html>

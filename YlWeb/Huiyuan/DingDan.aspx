<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDan.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.DingDan" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user_headbox">
          <div class="user_head"><img src="<%=TuXiang %>" width="104px" height="100px" /></div>
          <div class="user_msg">
	          <p>欢迎您，<font class="font_blue"><%=this.HuiYuanInfo.XingMing %></font></p>
		        <ul>
		          <li>当前积分 <em class="user_jf"><%=KeYongJiFen.ToString("F2") %></em></li>
		          <li><a href="DingDan.aspx">待付款订单 <em class="user_dindan_num"><%=DaiFuKuanDingDanShu %></em></a></li>
        		  
		          <li><a href="YouKe.aspx">常用游客信息管理</a></li>
		          <li style="text-align:right;"><a href="ShouCang.aspx">查看我的收藏<em class="fontgreen"><%=ShouCangShu %></em></a></li>
		        </ul>
          </div>
        </div>

        <div class="menu_T">
         <h3>查看我的订单</h3>
        </div>

        <div class="user_table">
         <table width="100%" border="0" class="tablelist">
           <tr>
             <th align="left">订单</th>
             <th align="center">订单金额</th>
             <th align="center">获得积分</th>
             <th align="center">状态</th>
           </tr>
           <asp:Literal runat="server" ID="ltr0"></asp:Literal>           
           <asp:PlaceHolder ID="phdNoDat" runat="server" Visible="false">
                   <tr>
                     <td align="center" colspan="4">暂无订单</td>
                   </tr>
           </asp:PlaceHolder>
         </table>
         
           <table width="100%" border="0" class="margin_T16">
	          <tr>
		        <td>
        <div>
            <div id="page_change" style="width: 100%; text-align: right; margin: 0px auto 0px;
                margin:0; clear: both">
            </div>    
        </div>
		        </td>
	          </tr>
           </table>
        </div>
    </form>
    
    <script type="text/javascript">
        $(document).ready(function() {
            $(".i_dianping").click(function() {
                var _dingDanId = $(this).closest("tr").attr("i_dingdanid");
                var _dingDanLeiXing = $(this).closest("tr").attr("i_dingdanleixing");

                var _data = { txtDingDanId: _dingDanId, txtDingDanLeiXing: _dingDanLeiXing, txtBiaoTi: "", txtNeiRong: "", txtFenShu: 5, txtDianPingId: "" };

                //showZZ('<div class="dianping_boxy"><input type="hidden" id="txt_dp_id"><p>总体评分：<input type="radio" name="txt_dp_fen" value="1" />1分<input type="radio" name="txt_dp_fen" value="2" />2分<input type="radio" name="txt_dp_fen" value="3" />3分<input type="radio" name="txt_dp_fen" value="4" />4分<input type="radio" name="txt_dp_fen" value="5" checked="checked" />5分</p><p>点评标题：<input type="text" id="txt_dp_bt" /></p><p>点评内容：</p><p><textarea id="txt_dp_nr" style="width:300px; height:100px"></textarea></p><p></p><p><input type="button" value="我要评价" id="i_btn_dp_01" /><input type="button" value="关闭窗口" id="i_btn_dp_02" /></p><p></p></div>');
                showZZ('<div class="user_boxy dianping_boxy"><a href="javascript:void(0)" id="i_btn_dp_02" class="close_btn"><em>X</em>关闭</a><input type="hidden" id="txt_dp_id"><ul class="login_form"><li>总体评分</li><li style="font-size: 14px;"><input type="radio" name="txt_dp_fen" value="1" />1分<input type="radio" name="txt_dp_fen" value="2" />2分<input type="radio" name="txt_dp_fen" value="3" />3分<input type="radio" name="txt_dp_fen" value="4" />4分<input type="radio" name="txt_dp_fen" value="5" checked="checked" />5分</li><li>点评标题</li><li><input type="text" class="formsize270 input_style" id="txt_dp_bt" /></li><li>点评内容</li><li><textarea id="txt_dp_nr" class="formsize270 input_style" style="height: 100px"></textarea></li><li class="login_txt"><input type="button" value="我要评价" id="i_btn_dp_01" class="user_loginbtn" /></li></ul></div>');

                function __setdianping(data) {
                    $("#txt_dp_id").val(data.DianPingId);
                    $("#txt_dp_bt").val(data.BiaoTi);
                    $("#txt_dp_nr").val(data.NeiRong);
                    $("input[name='txt_dp_fen'][value='" + data.FenShu + "']").attr("checked", "checked");
                }

                $.ajax({
                    type: "POST", url: "dingdan.aspx?dotype=getdp", cache: false, async: false, dataType: "json", data: { txtDingDanId: _dingDanId },
                    success: function(response) {
                        if (response.result == "1") {
                            __setdianping(response.obj);
                        }
                    }
                });

                function __dianping() {
                    if ($.trim($("#txt_dp_bt").val()).length < 1) { tableToolbar._showMsg("请输入点评标题"); return; }
                    _data.txtBiaoTi = $.trim($("#txt_dp_bt").val());
                    _data.txtNeiRong = $.trim($("#txt_dp_nr").val());
                    _data.txtFenShu = $("input[name='txt_dp_fen']:checked").val();
                    _data.txtDianPingId = $.trim($("#txt_dp_id").val());
                    $.ajax({
                        type: "POST", url: "dingdan.aspx?dotype=dp", cache: false, async: false, dataType: "json", data: _data,
                        success: function(response) {
                            if (response.result == "1") { tableToolbar._showMsg("点评成功！"); }
                            else { tableToolbar._showMsg("点评失败！"); }
                            closeZZ();
                        }
                    });
                }

                $("#i_btn_dp_01").click(function() { __dianping(); });
                $("#i_btn_dp_02").click(function() { closeZZ(); });

            });
        });
    </script>
</asp:Content>
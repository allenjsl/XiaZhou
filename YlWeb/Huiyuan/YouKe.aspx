<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouKe.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.YouKe" MasterPageFile="~/MasterPage/HuiYuan.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
    <script type="text/javascript" language="javascript">
        var YouKe={
            Ids:[],
            Delete:function(){
                $.newAjax({
                    url: "YouKe.aspx?del=1&ids=" + YouKe.Ids.join(","),
                    dataType: "json",
                    type: "post",
                    success: function(ret) {
                        YouKe.Ids=[];
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { window.location.href = window.location.href; });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            }
        };
        $(document).ready(function(){
            tableToolbar.init({ tableContainerSelector: "#liststyle"});
            $(".price_fontred").unbind("click").bind("click",function(){
                YouKe.Ids.push($(this).attr("data-id"));
                YouKe.Delete();
            });
            $(".color_5").unbind("click").bind("click",function(){
                $("input[name='checkbox']:checked").each(function(){
                    YouKe.Ids.push($(this).val());
                });
                if(YouKe.Ids.length<=0){
                    tableToolbar._showMsg("请选择要删除的常旅客！");
                }
                else{
                    YouKe.Delete();
                }
            })
        });
    </script>
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
         <h3>常用游客信息管理</h3>
         <div class="menu_Rtxt"><a href="YouKeEdit.aspx" class="user_add_btn">新增游客信息</a></div>
        </div>

        <div class="user_table">
           <table width="100%" border="0" class="tablelist" id="liststyle">
	          <tr>
	            <th width="30"><input type="checkbox" name="checkbox" /></th>
		        <th align="center">姓名</th>
		        <th align="center">国籍</th>
		        <th align="center">性别</th>
		        <th align="center">联系方式</th>
		        <th align="center">状态</th>
	            <th align="center">操作</th>
	          </tr>
           <asp:Repeater ID="rpt" runat="server">
               <ItemTemplate>
	          <tr>
	            <td><input type="checkbox" name="checkbox" value="<%#Eval("LvkeId") %>" /></td>
		        <td align="center"><%#Eval("XingMing") %></td>
		        <td align="center"><%#Eval("GuoJi")%></td>
		        <td align="center"><%#Eval("LeiXing")%></td>
		        <td align="center"><%#Eval("ShouJi")%></td>
		        <td align="center"><font class='<%#(int)Eval("ZhuangTai")==(int)EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus.可用?"font_yellow":""%>'><%#Eval("ZhuangTai")%></font></td>
	            <td align="center"><a href="YouKeEdit.aspx?type=edit&id=<%#Eval("LvkeId") %>" class="color_8">【编辑】</a> <a href="YouKeEdit.aspx?type=view&id=<%#Eval("LvkeId") %>" class="green">【查看】</a> <a href="javascript:void(0);" class="price_fontred" data-id="<%#Eval("LvkeId") %>">【删除】</a></td>
	          </tr>
                </ItemTemplate>
           </asp:Repeater>
           <asp:PlaceHolder ID="phdNoDat" runat="server" Visible="false">
                   <tr>
                     <td align="center" colspan="7">暂无常用旅客</td>
                   </tr>
           </asp:PlaceHolder>
         </table>
           
           <table width="100%" border="0" class="margin_T16">
	          <tr>
	            <td>
        <a href="javascript:void(0);" class="color_5">【批量删除】</a></td>
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
</asp:Content>
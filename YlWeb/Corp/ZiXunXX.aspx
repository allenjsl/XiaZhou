<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZiXunXX.aspx.cs" Inherits="EyouSoft.YlWeb.Corp.ZiXunXX"
    MasterPageFile="~/MasterPage/M1.Master" Title="资讯" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<%@ Register Src="/UserControl/FLDH.ascx" TagName="FLDH" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/GuanZhu.ascx" TagName="GuanZhu" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/BangZhu.ascx" TagName="BangZhu" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">
<script type="text/javascript" src="/js/ajaxpagecontrols.js"></script>
<script type="text/javascript">
    var pConfig = { pageSize: 1, pageIndex: 1, recordCount: 0, showPrev: true, showNext: true, showDisplayText: false, cssClassName: 'page_change' }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
<form id="form1" runat="server">
    <div class="step_mainbox fixed">
        <div class="basicT">您的位置：维诗达游轮 > <%=ltr.Text %></div>

        <div class="leftside q_sideL">
          
          <div class="basic_leftT">
            <h5><%=ltr.Text %></h5>
           </div>
           
           <ul class="q_list">
           <asp:Repeater ID="rpt" runat="server">
           <ItemTemplate>
               <li><a href="ZiXunXX.aspx?s=<%#Eval("ZiXunId") %>" target="_parent"><%#(this.pageIndex-1)*this.pageSize+Container.ItemIndex+1 %>、<%#Eval("BiaoTi") %></a></li>
           </ItemTemplate>
           </asp:Repeater>
           </ul>
           
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
        
        <div class="rightside">
            <%if (!string.IsNullOrEmpty(EyouSoft.Common.Utils.GetQueryStringValue("s"))) %>
            <%{ %>
            <div class="basic_rightT">
                <h5><asp:Literal runat="server" ID="ltr"></asp:Literal></h5>
            </div>
            <div class="contxt">
                <h3>
                    <asp:Literal runat="server" ID="ltr0"></asp:Literal>
                </h3>
                <h5>
                    <asp:Literal runat="server" ID="ltr1"></asp:Literal></h5>
                <asp:Literal runat="server" ID="ltr2"></asp:Literal>
            </div>
            <%} %>
            
              <div class="basic_rightT"><h5>热门推荐</h5></div> 
                          
              <div class="hangxianjs q_Rlist">
                 <ul>
                 <asp:Repeater ID="rptHot" runat="server">
                 <ItemTemplate>
                      <li>
                                    <div class="hanxian_img"><a href="<%#GetHQURL(Eval("LeiXing"),Eval("HangQiId")) %>"><img src="<%#GetTu(Eval("FuJians")) %>" /></a></div>
                                    <div class="hanxian_R">
                                        <div class="hanxian_T"><a href="<%#GetHQURL(Eval("LeiXing"),Eval("HangQiId")) %>"><%# Eval("MingCheng")%></a></div>
                                        <ul class="hanxian_list">
                                           <li><label>航线性质：</label><%#Eval("HangXianXingZhi")%></li>
                                           <li><label>邮轮公司：</label><%#Eval("GongSiName")%></li>
                                           <li><label>行程天数：</label><%#Eval("TianShu1")%>天<%#Eval("TianShu2")%>晚</li>
                                           <li><label>船只名称：</label><%#Eval("ChuanZhiName")%></li>
                                           <li><label>登船港口：</label><%#Eval("ChuFaGangKouMingCheng")%></li>
                                           <li><label>登下船港口：</label><%#Eval("DiDaGangKouMingCheng")%></li>
                                           <li class="width100"><div class="floatR"><a href="<%#GetHQURL(Eval("LeiXing"),Eval("HangQiId")) %>" class="q_yudin">立即预订</a></div><label>途经城市：</label><%#Eval("TuJingChengShi")%></li>
                                        </ul>
                                        
                                    </div>                 
                      </li>
                 </ItemTemplate>
                 </asp:Repeater>

                 </ul>
           </div>
            
        </div>
    </div>
<script type="text/javascript" language="javascript">
    $(function(){
        if (pConfig.recordCount > 0) {
                AjaxPageControls.replace("page_change", pConfig);
            }
    })
</script>
</form>
</asp:Content>


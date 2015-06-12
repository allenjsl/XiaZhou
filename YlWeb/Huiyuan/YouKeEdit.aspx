<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouKeEdit.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.YouKeEdit" MasterPageFile="~/MasterPage/HuiYuan.Master" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user-title">常旅客编辑 <em>*必须填写</em></div>
        <div class="user_Rbox">
          <ul class="login_form fixed">
	          <li><label>姓名</label><input class="formsize270 input_style" id="txtXingMing" name="txtXingMing" runat="server" valid="required" errmsg="请输入姓名"/><font color="red">*</font></li>
	          <li><label>类型</label>
                        <select id="ddlLeiXing" name="ddlLeiXing" class="formsize270 input_style">
                        <option value="">--请选择--</option>
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), this.LeiXing)%>
                    </select>
	            <%--<div class="selecttagbox" style="left:76.8%">
	            <input type="hidden" id="hidLeiXing" runat="server" value="-1" />
                    <div class="selectitem">
                        <dl>
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumXiaLaDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.VisitorType)), this.LeiXing)%>
                        </dl>
                    </div>
                </div>--%>
	          </li>
	          <li><label>证件类型</label>
                        <select id="ddlZhengJianLeiXing" name="ddlZhengJianLeiXing" class="formsize270 input_style">
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.YKZJLeiXing)), this.ZhengJianLeiXing)%>
                    </select>
<%--	            <div class="selecttagbox" style="left:44.3%">
	            <input type="hidden" id="hidZhengJianLeiXing" runat="server" value="-1" />
                    <div class="selectitem">
                        <dl>
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumXiaLaDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.CardType)), this.ZhengJianLeiXing)%>
	                    </dl>
                    </div>
                </div>
--%>              </li>
	          <li><label>证件号码</label><input class="formsize270 input_style" id="txtZhengJianHaoMa" name="txtZhengJianHaoMa" runat="server" /></li>
	          <li><label>证件有效期</label><input class="formsize270 input_style" id="txtZhengJianYouXiaoQi" name="txtZhengJianYouXiaoQi" runat="server" onfocus="WdatePicker();" /></li>
	          <li><label>出生日期</label><input class="formsize270 input_style" id="txtChuShengRiQi" name="txtChuShengRiQi" runat="server" onfocus="WdatePicker();" /></li>
	          <li><label>联系电话</label><input class="formsize270 input_style" id="txtDianHua" name="txtDianHua" runat="server" /></li>
	          <li><label>联系手机</label><input class="formsize270 input_style" id="txtShouJi" name="txtShouJi" runat="server" /></li>
	          <li><label>国家</label>
                        <asp:DropDownList ID="ddlCountry" name="ddlCountry" valid="required|RegInteger" errmsg="请选择国家|请选择国家"
                            runat="server" CssClass="formsize270 input_style" />
                        <font color="red">*</font>
	          </li>
	          <li><label>省份</label>
                        <asp:DropDownList ID="ddlProvice" name="ddlProvice" valid="required|RegInteger" errmsg="请选择省份|请选择省份"
                            runat="server" CssClass="formsize270 input_style" />
                        <font color="red">*</font>
	          </li>
	          <li><label>城市</label>
                        <asp:DropDownList ID="ddlCity" name="ddlCity" valid="required|RegInteger" errmsg="请选择城市|请选择城市"
                            runat="server" CssClass="formsize270 input_style" />
                        <font color="red">*</font>
	          </li>
	          <li><label>县区</label>
                        <asp:DropDownList ID="ddlCounty" name="ddlCounty" runat="server" CssClass="formsize270 input_style" />
	          </li>
	          <li><label>状态</label>
                        <select id="ddlZhuangTai" name="ddlZhuangTai" class="formsize270 input_style" valid="required|RegInteger" errmsg="请选择状态|请选择转台">
                        <option value="">--请选择--</option>
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.HuiYuanChangLvKeStatus)), this.LeiXing)%>
                    </select>
                        <font color="red">*</font>
	          </li>
	          <li><label>国籍</label>
                        <asp:DropDownList ID="ddlGuoJi" name="ddlGuoJi" runat="server" CssClass="formsize270 input_style" />
	            
	          </li>
	          <li><label>性别</label>
                        <select id="ddlXingBie" name="ddlXingBie" class="formsize270 input_style">
                        <option value="">--请选择--</option>
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender)), this.XingBie)%>
                    </select>
	          </li>
          </ul>			     
        </div>

        <div class="user_Rbox" style='text-align:center;visibility:<%=Request.QueryString["type"]=="view"?"hidden":"visible"%>'>
            <a href="javascript:void(0)" onclick="Save();" class="user_add_btn user_btn02">保存信息</a>
        </div>  
    </form>
<script type="text/javascript">
    $(document).ready(function(){            
        pcToobar.init({
            gID: "#<%=ddlCountry.ClientID %>",
            pID: "#<%=ddlProvice.ClientID %>",
            cID: "#<%=ddlCity.ClientID %>",
            xID: "#<%=ddlCounty.ClientID %>",
            gSelect: '<%=this.GuoJia %>',
            pSelect: '<%=this.ShengFen %>',
            cSelect: '<%=this.ChengShi %>',
            xSelect: '<%=this.XianQu %>',
            comID: '<%=this.HuiYuanInfo.CompanyId %>'
        });
    });
    
    function Save(){
        if (ValiDatorForm.validator("form","parent")) {
            $.newAjax({
                type: "post",
                cache: false,
                url: '/HuiYuan/YouKeEdit.aspx?doType=save&id=<%=Request.QueryString["Id"]%>',
                data: $("form").serialize(),
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg,function(){window.location.href = "/HuiYuan/YouKe.aspx";})

                    } else {
                        tableToolbar._showMsg(ret.msg);
                    }
                },
                error: function() {
                    tableToolbar._showMsg("操作失败，请稍后重试！");
                }
            });
        } else {
            return false;
        }
    }
</script>  
</asp:Content>
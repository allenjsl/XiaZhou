<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/HuiYuan.Master" AutoEventWireup="true"  EnableEventValidation="false"
    CodeBehind="MyInformation.aspx.cs" Inherits="EyouSoft.YlWeb.Huiyuan.MyInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HuiYuanHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HuiYuanBody" runat="server">
    <form id="form1" runat="server">
        <div class="user-title">更改密码 <em>*必须填写</em></div>
        <div class="user_Rbox">
          <ul class="login_form fixed">
	          <li><label>用户名</label><input id="txtUsername" runat="server" readonly="readonly" class="formsize270 input_style" /></li>
	          <li><label>性别</label><select name="ddlXingBie" class="formsize270 input_style">
                        <option value="">--请选择--</option>
	                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GovStructure.Gender),new string[]{"2"}), this.XingBie)%>
                    </select></li>
	          <li><label>真实姓名</label><input id="txtXingMing" runat="server" class="formsize270 input_style" valid="required" errmsg="请输入姓名" /><font color="red">*</font></li>
	          <li><label>所属国籍</label><input id="txtGuoJi" runat="server" class="formsize270 input_style" /></li>
	          <li><label>常用邮箱</label><input id="txtYouXiang" runat="server" class="formsize270 input_style" /></li>
	          <li><label>出生日期</label><input id="txtShengRi" runat="server" onfocus="WdatePicker();" class="formsize270 input_style" /></li>
	          <li><label>手机号码</label><input id="txtShouJi" runat="server" class="formsize270 input_style" valid="required|isMobile" errmsg="请填写手机号码|请填写正确的手机号码" /><font color="red">*</font></li>
	          <li><label>联系电话</label><input id="txtDianHua" runat="server" class="formsize270 input_style" /></li>
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
	          <li style="width:99%;"><label>收件地址</label><textarea id="txtDiZhi" runat="server" class="input_style" style="width:680px; height:100px;"></textarea>
	          </li>
          </ul>			     
        </div>

        <div class="user_Rbox" style="text-align:center;">
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
                url: '/HuiYuan/MyInformation.aspx?doType=save',
                data: $("form").serialize(),
                dataType: "json",
                success: function(ret) {
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg)
                        window.location.href = window.location.href;

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

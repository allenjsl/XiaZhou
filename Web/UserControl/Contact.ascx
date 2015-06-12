<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Contact.ascx.cs" Inherits="Web.UserControl.Contact" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<span class="formtableT formtableT02">联系人</span>
<table width="99%" cellspacing="0" cellpadding="0" border="0" style="height: auto;
    margin: 0 auto; zoom: 1; overflow: hidden;" id="travel_tbl">
    <tbody>
        <tr style="height: 28px;">
            <td width="25" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                编号
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                姓名
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                职务
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                电话
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                手机
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                生日
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                QQ
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                E-mail
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                传真
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                操作
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="ph_showorhide">
            <tr class="showTr">
                <td height="28" align="center">
                    <span class="index">1</span>
                </td>
                <td height="28" align="center">
                    <input type="text" value="" errmsg="请输入联系人名称!" valid="required" class="inputtext formsize60" name="Name"><font class="fontbsize12">*</font>
                </td>
                <td align="center">
                    <input type="text" value="" class="inputtext formsize80" name="Post">
                </td>
                <td align="center">
                    <input type="text" value="" valid="isTelephone" errmsg="电话格式错误!" class="inputtext formsize80" name="TelPhone">
                </td>
                <td align="center">
                    <input type="text" value="" valid="isTelephone" errmsg="手机格式错误!" class="inputtext formsize80" name="Mobel">
                </td>
                <td align="center">
                        <input type="text" value="" class="inputtext formsize60" name="Birthday" onfocus="WdatePicker()">
                        <input type="hidden" name="hdremind" value="0" />
                       <label><input type="checkbox" name="Remind" class='ckremind' value="0" />提醒</label>
                </td>
                <td align="center">
                    <input type="text" valid="isQQ" errmsg="QQ格式错误!" class="inputtext formsize80" value="" digits="true" errmsg="请输入正确的QQ!"
                        name="QQ">
                </td>
                <td align="center">
                    <input type="text" value="" valid="isEmail" errmsg="邮箱格式错误!" class="inputtext formsize120" name="EMail">
                </td>
                <td align="center">
                    <input type="text" value="" class="inputtext formsize80" name="Fax">
                </td>
                <td align="center">
                    <a class="addbtn" id="A1" href="javascript:void(0)">
                    <img width="48" height="20" src="/images/addimg.gif"></a> <a id="A2" class="delbtn"
                        href="javascript:void(0)">
                        <img src="/images/delimg.gif"></a>
                </td>
            </tr>
        </asp:PlaceHolder>
        <cc1:CustomRepeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr class="showTr">
                    <td height="28" align="center">
                       <span class="index"><%#Container.ItemIndex + 1 %></span> 
                    </td>
                    <td height="28" align="center">
                        <input type="text" errmsg="请输入联系人名称!" valid="required" value="<%#Eval("Name")%>" class="inputtext formsize60" name="Name"><font class="fontbsize12">*</font>
                        <input type="hidden" name="UserId" value="<%#Eval("UserId")%>" />
                    </td>
                    <td align="center">
                        <input type="text" value="<%#Eval("Post")%>" class="inputtext formsize80" name="Post">
                    </td>
                    <td align="center">
                        <input type="text" valid="isTelephone" errmsg="电话格式错误!" value="<%#Eval("Telephone")%>" class="inputtext formsize80" name="TelPhone">
                    </td>
                    <td align="center">
                        <input type="text" valid="isTelephone" errmsg="手机格式错误!" value="<%#Eval("MobilePhone")%>" class="inputtext formsize80"
                            name="Mobel">
                    </td>
                    <td align="center">
                       
                            <input type="text" value="<%#EyouSoft.Common.UtilsCommons.SetDateTimeFormart((DateTime?)Eval("Birthday"))%>" class="inputtext formsize60" name="Birthday"
                                onfocus="WdatePicker()"><input type="hidden" name="hdremind" value='<%#Convert.ToBoolean(Eval("IsRemind"))==true?"1":"0" %>' />
                           <label> <input class='ckremind' type="checkbox" name="Remind" value="0" <%#Convert.ToBoolean(Eval("IsRemind"))==true?"checked='checked'":"" %> />提醒</label>
                    </td>
                    <td align="center">
                        <input type="text" valid="isQQ" errmsg="QQ格式错误!" class="inputtext formsize80" value="<%#Eval("QQ")%>"
                            digits="true" name="QQ">
                    </td>
                    <td align="center">
                        <input type="text" valid="isEmail" errmsg="邮箱格式错误!" value="<%#Eval("EMail")%>" class="inputtext formsize120" name="EMail">
                    </td>
                    <td align="center">
                        <input type="text" value="<%#Eval("Fax") %>" class="inputtext formsize80" name="Fax">
                    </td>
                    <td align="center">
                         <a class="addbtn" id="travel_add" href="javascript:void(0)">
                    <img width="48" height="20" src="/images/addimg.gif"></a> <a id="travel_del" class="delbtn"
                        href="javascript:void(0)">
                        <img src="/images/delimg.gif"></a>
                    </td>
                </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
    </tbody>
</table>

<script type="text/javascript">
    $(function() {
        $("#travel_tbl").autoAdd({tempRowClass:"showTr",addButtonClass:"addbtn",delButtonClass:"delbtn",indexClass: "index"})

        $(".ckremind").live("change", function() {
            if (this.checked) {
               
                $(this).closest("td").find("input:[name='hdremind']").val("1")
            }
            else {
                $(this).closest("td").find("input:[name='hdremind']").val("0")
            }
        })

    })
    
</script>


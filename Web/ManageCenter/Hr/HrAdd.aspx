<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrAdd.aspx.cs" Inherits="Web.ManageCenter.Hr.HrAdd"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectDuty.ascx" TagName="SelectDuty" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .STYLE1
        {
            font-size: 18px;
            color: #000;
            font-weight: bold;
        }
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
        .firsttable .progressWrapper
        {
            overflow: hidden;
            width: 260px;
        }
        .firsttable .progressName
        {
            width: 200px;
        }
    </style>
    <div class="mainbox mainbox-whiteback">
        <form id="form1" method="post" runat="server">
        <span class="formtableT formtableT02">基本信息</span>
        <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
            <tr>
                <td width="10%" class="addtableT">
                    <span class="errmsg">*</span>档案编号：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbNum" class="inputtext formsize180" valid="required|custom"
                        custom="PageJsData.CheckFileNum" errmsg="档案编号不能为空！|对不起，该档案编号已存在！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hdKeyid" />
                </td>
                <td width="10%" class="addtableT">
                    <span class="errmsg">*</span>姓&nbsp;&nbsp;&nbsp; 名：
                </td>
                <td width="25%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbName" class="inputtext formsize120" valid="required"
                        errmsg="姓名不能为空！"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    <span class="errmsg">*</span>身 份 证：
                </td>
                <td width="19%" class="kuang2">
                     <asp:TextBox runat="server" ID="tbIdcard" class="inputtext formsize180" valid="required|isIdCard"
                        errmsg="身份证不能为空！|身份证格式不正确！"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" class="addtableT">
                    <span class="errmsg">*</span>性&nbsp;&nbsp;&nbsp; 别：
                </td>
                <td width="24%" class="kuang2">                   
                    <asp:DropDownList ID="selSex" runat="server" class="inputselect formsize120">
                    </asp:DropDownList>    
                </td>
                <td width="10%" class="addtableT">
                    出生日期：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbBirthDay" class="inputtext formsize120" onfocus="WdatePicker()"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    <span class="errmsg">*</span>所属部门：
                </td>
                <td width="19%" class="kuang2">
                    <uc1:SelectSection ID="SelectSection1" runat="server" ReadOnly="true" SetTitle="所属部门"
                        SModel="1" />
                </td>
            </tr>
            <tr>
                <td width="10%" class="addtableT">
                    职&nbsp;&nbsp;&nbsp; 务：
                </td>
                <td width="24%" class="kuang2">
                    <uc2:SelectDuty runat="server" ID="SelectDuty1" ReadOnly="true" SetTitle="员工职务" SModel="2" />
                </td>
                <td width="10%" class="addtableT">
                    类&nbsp;&nbsp;&nbsp; 型：
                </td>
                <td width="24%" class="kuang2">
                    <asp:DropDownList ID="selType" class="inputselect formsize120" runat="server">
                    </asp:DropDownList>
                </td>
                <td width="10%" class="addtableT">
                    <span class="errmsg">*</span>员工状态：
                </td>
                <td width="19%" class="kuang2">
                    <asp:DropDownList ID="selState" class="inputselect formsize120" runat="server" onchange="showInputCause(this)">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="10%" class="addtableT">
                    入职时间：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbIntime" class="inputtext formsize180" onfocus="WdatePicker({maxDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd',onpicked:function(){PageJsData.setGongLing(this);} })"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    工&nbsp;&nbsp;&nbsp; 龄：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbWorkyear" class="inputtext formsize120" valid="isInt|range"
                        min="0" errmsg="工龄只能为正整数！|工龄大于等于0！"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    <span id="leaveTitleSpan" runat="server">离职时间：</span>
                </td>
                <td width="19%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbLeavetime" class="inputtext formsize120" onfocus="WdatePicker()"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" class="addtableT">
                    婚姻状态：
                </td>
                <td width="24%" class="kuang2">
                    <asp:DropDownList ID="selWed" runat="server" class="inputselect formsize180">
                        <asp:ListItem Value="0" Text="">未婚</asp:ListItem>
                        <asp:ListItem Value="1" Text="">已婚</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="10%" class="addtableT">
                    民&nbsp;&nbsp;&nbsp; 族：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbNation" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    籍&nbsp;&nbsp;&nbsp; 贯：
                </td>
                <td width="19%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbOrigin" class="inputtext formsize120"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" class="addtableT">
                    政治面貌：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbGovface" class="inputtext formsize180"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    联系电话：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbTelnum" class="inputtext formsize120" valid="isPhone"
                        errmsg="联系电话格式不正确！"></asp:TextBox>
                </td>
                <td class="addtableT">
                    电话短号：
                </td>
                <td width="19%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbShortel" class="inputtext formsize120"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="10%" class="addtableT">
                    手&nbsp;&nbsp;&nbsp; 机：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbPhonenum" class="inputtext formsize180" valid="isMobile"
                        errmsg="手机号码格式不正确！"></asp:TextBox>
                </td>
                <td width="10%" class="addtableT">
                    手机短号：
                </td>
                <td width="24%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbShortphone" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td class="addtableT">
                    QQ 帐号：
                </td>
                <td width="19%" class="kuang2">
                    <asp:TextBox runat="server" ID="tbQnum" class="inputtext formsize120" valid="isQQ"
                        errmsg="QQ号码格式不正确"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="addtableT">
                    MSN 帐号：
                </td>
                <td class="kuang2 pand4">
                    <asp:TextBox runat="server" ID="tbMsn" class="inputtext formsize180"></asp:TextBox>
                </td>
                <td class="addtableT">
                    E- mail：
                </td>
                <td class="kuang2 pand4">
                    <asp:TextBox runat="server" ID="tbEmail" valid="isEmail" errmsg="Email格式不正确！" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td class="addtableT">
                    地&nbsp;&nbsp;&nbsp;址：
                </td>
                <td width="19%" class="kuang2">
                    <asp:TextBox runat="server" class="inputtext formsize120" ID="tbAddress"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="addtableT">
                    员工照片：
                </td>
                <td class="kuang2 pand4">
                    <uc2:UploadControl runat="server" ID="SingleFileUpload2" IsUploadMore="false" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbPhoto" CssClass="labelFiles"></asp:Label>
                </td>
                <td class="addtableT">
                    附件上传：
                </td>
                <td width="56%" colspan="3" class="kuang2 pand4">
                    <uc2:UploadControl runat="server" ID="SingleFileUpload1" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbFiles" CssClass="labelFiles"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td class="addtableT">
                    备&nbsp;&nbsp;&nbsp; 注：
                </td>
                <td height="45" colspan="5" class="kuang2 pand4" align="left" bgcolor="#FFFFFF">
                    <asp:TextBox runat="server" ID="tbRemark" class="inputtext formsize350" TextMode="MultiLine"
                        Width="300" Height="40"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="hr_5">
        </div>
        <table cellspacing="0" cellpadding="0" border="0" width="100%" style="background: #F0FAFE;
            border: #abd7eb solid 1px;">
            <tr>
                <td align="right" height="28" width="35%">
                    是否分配系统账号:
                </td>
                <td align="left" width="15%">
                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="rbSetSysID">
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                        <asp:ListItem Value="0" Text="否" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:HiddenField runat="server" ID="hdUserid" />
                </td>
                <td align="right" width="15%">
                    是否同步导游档案:
                </td>
                <td align="left" width="45%">
                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="rbGuid">
                        <asp:ListItem Value="1" Text="是"></asp:ListItem>
                        <asp:ListItem Value="0" Text="否" Selected="True"></asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:HiddenField runat="server" ID="hidGuid" />
                </td>
            </tr>
        </table>
        <div style="display: none; text-align: center; vertical-align: middle; float: none;"
            id="showboxy">
            <table style="width: 245px; height: 100px; text-align: center">
                <tr>
                    <td align="right" style="width: 35%">
                        <span class="errmsg">*</span>用户名：
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox runat="server" ID="tbUsername" CssClass="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 35%">
                        <span class="errmsg" id="pwdFlag" runat="server">*</span>密码：
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox runat="server" ID="tbUserpwd" TextMode="Password" CssClass="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 35%">
                        重复密码：
                    </td>
                    <td align="left" style="width: 50%">
                        <asp:TextBox runat="server" ID="tbrepeatpwd" TextMode="Password" CssClass="inputtext formsize120"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="alertbox-btn" style="position: static">
                <a href="javascript:void(0)" id="aSave" hidefocus="true"><s class="baocun"></s>确 定</a>
                <a href="javascript:void(0)" onclick="PageJsData.HideBoxy()" hidefocus="true"><s
                    class="chongzhi"></s>关 闭</a>
            </div>
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT formtableT02">家庭关系</span>
            <table width="100%" cellspacing="0" cellpadding="0" class="tableHome">
                <tr>
                    <th width="10%" valign="middle" class="th-line">
                        关系
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        姓名
                    </th>
                    <th width="20%" valign="middle" class="th-line">
                        电话
                    </th>
                    <th width="16%" valign="middle" class="th-line">
                        工作单位
                    </th>
                    <th width="36%" valign="middle" class="th-line">
                        住址
                    </th>
                    <th width="8%" valign="middle" class="th-line">
                        操作
                    </th>
                </tr>
                <%--<%=this.getTables("homeList")%>--%>
                <asp:PlaceHolder runat="server" ID="ph_homelistUpdate">
                    <asp:Repeater runat="server" ID="rpt_homelist">
                        <ItemTemplate>
                            <tr class='HomeTr'>
                                <td height='28' align='center' bgcolor='#FFFFFF'>
                                    <input type='text' name='txtHomeRelation' class='inputtext formsize80' value='<%#Eval("Relationship") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input type='text' name='txtHomeName' class='inputtext formsize80' value='<%#Eval("Name") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input type='text' name='txtHomeTel' class='inputtext formsize120' value='<%#Eval("Phone") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input type='text' name='txtHomeUnit' class='inputtext formsize120' value='<%#Eval("WorkUnit") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input type='text' name='txtHomeAddress' class='inputtext formsize350' value='<%#Eval("Address") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <a href='javascript:void(0)' class='addbtnHome'>添加</a>|<a href='javascript:void(0);'
                                        class='delbtnHome'>删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_homelistAdd">
                    <tr class='HomeTr'>
                        <td height='28' align='center' bgcolor='#FFFFFF'>
                            <input type='text' name='txtHomeRelation' class='inputtext formsize80' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input type='text' name='txtHomeName' class='inputtext formsize80' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input type='text' name='txtHomeTel' class='inputtext formsize120' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input type='text' name='txtHomeUnit' class='inputtext formsize120' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input type='text' name='txtHomeAddress' class='inputtext formsize350' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <a href='javascript:void(0)' class='addbtnHome'>添加</a>|<a href='javascript:void(0);'
                                class='delbtnHome'>删除</a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT formtableT02">学历信息</span>
            <table width="100%" cellspacing="0" cellpadding="0" class="tableEdu">
                <tr>
                    <th width="10%" valign="middle" class="th-line">
                        开始时间
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        结束时间
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        学历
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        所学专业
                    </th>
                    <th width="16%" valign="middle" class="th-line">
                        毕业院校
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        状态
                    </th>
                    <th width="26%" valign="middle" class="th-line">
                        备注
                    </th>
                    <th width="8%" valign="middle" class="th-line">
                        操作
                    </th>
                </tr>
                <%--<%=this.getTables("schoolList")%>--%>
                <asp:PlaceHolder runat="server" ID="ph_schoollistUpdate">
                    <asp:Repeater runat="server" ID="rpt_schoollist">
                        <ItemTemplate>
                            <tr class='EduTr'>
                                <td height='28' align='center' bgcolor='#FFFFFF'>
                                    <input name='txtStartTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()'
                                        value='<%#Eval("StartTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtEndTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()'
                                        value='<%#Eval("EndTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input type='text' name='selEdu' id='selEdu' class='inputtext formsize80' value='<%#Eval("Education") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtSpeciality' type='text' class='inputtext formsize80' value='<%#Eval("Profession") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtSchool' type='text' class='inputtext formsize120' value='<%#Eval("Graduated") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <select name='selEduState' class="inputselect formsize80">
                                        <%#this.getOptions(1, (int)Eval("Statue"))%>
                                    </select>
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtEduRemark' type='text' class='inputtext formsize180' value='<%#Eval("Remarks") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <a href='javascript:void(0)' class='addbtn'>添加</a>|<a href='javascript:void(0);'
                                        class='delbtn'>删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_schoollistAdd">
                    <tr class='EduTr'>
                        <td height='28' align='center' bgcolor='#FFFFFF'>
                            <input name='txtStartTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtEndTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input type='text' name='selEdu' id='selEdu' class='inputtext formsize80' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtSpeciality' type='text' class='inputtext formsize80' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtSchool' type='text' class='inputtext formsize120' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <select name='selEduState' class="inputselect formsize80">
                                <%=this.getOptions(1,-1)%>
                            </select>
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtEduRemark' type='text' class='inputtext formsize180' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <a href='javascript:void(0)' class='addbtn'>添加</a>|<a href='javascript:void(0);'
                                class='delbtn'>删除</a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT formtableT02">履历信息</span>
            <table width="100%" cellspacing="0" cellpadding="0" class="tableWork">
                <tr>
                    <th width="10%" valign="middle" class="th-line">
                        开始时间
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        结束时间
                    </th>
                    <th width="20%" valign="middle" class="th-line">
                        工作地点
                    </th>
                    <th width="16%" valign="middle" class="th-line">
                        工作单位
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        从事职业
                    </th>
                    <th width="26%" valign="middle" class="th-line">
                        备注
                    </th>
                    <th width="8%" valign="middle" class="th-line">
                        操作
                    </th>
                </tr>
                <%--<%=this.getTables("workList")%>--%>
                <asp:PlaceHolder runat="server" ID="ph_worklistUpdate">
                    <asp:Repeater runat="server" ID="rpt_worllist">
                        <ItemTemplate>
                            <tr class='WorkTr'>
                                <td height='28' align='center' bgcolor='#FFFFFF'>
                                    <input name='txtWorkStartTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()'
                                        value='<%#Eval("StartTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtWorkEndTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()'
                                        value='<%#Eval("EndTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtWorkPlace' type='text' class='inputtext formsize180' value='<%#Eval("Location") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtWorkUnit' type='text' class='inputtext formsize120' value='<%#Eval("WorkUnit") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtWorkDuty' type='text' class='inputtext formsize80' value='<%#Eval("Occupation") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <input name='txtWorkRemark' type='text' class='inputtext formsize180' value='<%#Eval("Remarks") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <a href='javascript:void(0)' class='addbtnWork'>添加</a>|<a href='javascript:void(0);'
                                        class='delbtnWork'>删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_worklistAdd">
                    <tr class='WorkTr'>
                        <td height='28' align='center' bgcolor='#FFFFFF'>
                            <input name='txtWorkStartTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtWorkEndTime' type='text' class='inputtext formsize80' onfocus='WdatePicker()' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtWorkPlace' type='text' class='inputtext formsize180' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtWorkUnit' type='text' class='inputtext formsize120' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtWorkDuty' type='text' class='inputtext formsize80' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <input name='txtWorkRemark' type='text' class='inputtext formsize180' />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <a href='javascript:void(0)' class='addbtnWork'>添加</a>|<a href='javascript:void(0);'
                                class='delbtnWork'>删除</a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT formtableT02">劳动合同</span>
            <table width="100%" cellspacing="0" cellpadding="0" class="tableContact">
                <tr>
                    <th width="10%" valign="middle" class="th-line">
                        合同编号
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        姓名
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        签订时间
                    </th>
                    <th width="10%" valign="middle" class="th-line">
                        到期时间
                    </th>
                    <th width="16%" valign="middle" class="th-line">
                        状态
                    </th>
                    <th width="36%" valign="middle" class="th-line">
                        备注
                    </th>
                    <th width="8%" valign="middle" class="th-line">
                        操作
                    </th>
                </tr>
                <%--<%=this.getTables("filecontract")%>--%>
                <asp:PlaceHolder runat="server" ID="ph_contractUpdate">
                    <asp:Repeater runat="server" ID="rpt_contract">
                        <ItemTemplate>
                            <tr class="ContactTr">
                                <td height="28" align="center" bgcolor="#FFFFFF">
                                    <input name="txtPackNum" type="text" class="inputtext formsize80" id="txtPackNum"
                                        value='<%#Eval("ContractNumber") %>' />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <input name="txtPackName" type="text" class="inputtext formsize80" id="txtPackName"
                                        value='<%#Eval("Name") %>' />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <input name="txtPackStartTime" type="text" class="inputtext formsize80" id="txtPackStartTime"
                                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtPackEndTime\')}'})" value='<%#Eval("SignedTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <input name="txtPackEndTime" type="text" class="inputtext formsize80" id="txtPackEndTime"
                                        onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtPackStartTime\')}'})" value='<%#Eval("MaturityTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <select name="selContactState" class="inputselect formsize120">
                                        <%#this.getOptions(2, (int)Eval("Status"))%>
                                    </select>
                                </td>
                                <td align="center" bgcolor="#FFFFFF">
                                    <input name="txtPackRemark" type="text" class="inputtext formsize350" id="txtPackRemark"
                                        value='<%#Eval("Remarks") %>' />
                                </td>
                                <td align='center' bgcolor='#FFFFFF'>
                                    <a href='javascript:void(0)' class='addbtnContact'>添加</a>|<a href='javascript:void(0);'
                                        class='delbtnContact'>删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="ph_contractAdd">
                    <tr class="ContactTr">
                        <td height="28" align="center" bgcolor="#FFFFFF">
                            <input name="txtPackNum" type="text" class="inputtext formsize80" id="txtPackNum" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="txtPackName" type="text" class="inputtext formsize80" id="txtPackName" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="txtPackStartTime" type="text" class="inputtext formsize80" id="txtPackStartTime"
                                onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtPackEndTime\')}'})" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="txtPackEndTime" type="text" class="inputtext formsize80" id="txtPackEndTime"
                                onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtPackStartTime\')}'})" />
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <select name="selContactState" class="inputselect formsize120">
                                <%=this.getOptions(2, -1)%>
                            </select>
                        </td>
                        <td align="center" bgcolor="#FFFFFF">
                            <input name="txtPackRemark" type="text" class="inputtext formsize350" id="txtPackRemark" />
                        </td>
                        <td align='center' bgcolor='#FFFFFF'>
                            <a href='javascript:void(0)' class='addbtnContact'>添加</a>|<a href='javascript:void(0);'
                                class='delbtnContact'>删除</a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="mainbox cunline fixed" style="position: static">
            <ul>
                <asp:PlaceHolder runat="server" ID="ph_Save">
                    <li class="cun-cy"><a href="javascript:void(0);" hidefocus="true" id="btnSave">保 存</a>
                    </li>
                </asp:PlaceHolder>
                <li class="quxiao-cy"><a href="javascript:void(0);" onclick="PageJsData.GoBack()"
                    hidefocus="true">关 闭</a> </li>
            </ul>
        </div>
        </form>
    </div>
    <script src="/Js/IdCard.Validate.js" type="text/javascript"></script>
    <script type="text/javascript">
        var PageJsData = {
            //URL参数对象
            Query: {
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            UserData: {
                u: "", //帐号
                p: ""  //密码
            },
            DelFile: function(obj) {
                var self = $(obj);
                self.closest("span").hide();
                self.next(":hidden").val("");
            },
            Box: null,
            //检查档案编号是否重复
            CheckFileNum: function(obj) {
                var s = "";
                var url = "HrAdd.aspx?";
                url += $.param({
                    doType: "checkFileNum",
                    id: '<%=Request.QueryString["id"]%>',
                    sl: this.Query.sl,
                    num: $(obj).val()
                });
                $.newAjax({
                    type: "get",
                    url: url,
                    success: function(data) { s = data; },
                    async: false
                })
                return s != "1" ? true : false;
            },
            ShowBoxy: function() {
                this.Box = new Boxy($("#showboxy"), { width: "650px", height: "450px", title: "分配系统帐号", modal: true, closeable: false });
            },
            HideBoxy: function() {
                if (this.Box != null) {
                    this.Box.hide();
                }
            },
            //检查分配用户名是否重复
            isExistsUsername: function() {
                var _retCode = true;
                $.newAjax({ type: "post", cache: false, url: '/ashx/isexistsusername.ashx', dataType: "json", async: false
                    , data: { "username": $.trim($("#<%=tbUsername.ClientID %>").val()) }
                    , success: function(ret) {
                        if (ret.result == "1") {
                            _retCode = false;
                        }
                    }
                });

                return _retCode;
            },
            //返回
            GoBack: function() {
                window.location = "/ManageCenter/Hr/HrList.aspx?sl=" + PageJsData.Query.sl;
            },
            //表单
            Form: null,
            //表单验证
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0);
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "alert");
            },
            //表单提交保存
            Save: function() {
                if (PageJsData.FormCheck()) {
                    var birthDay = $("#<%=tbBirthDay.ClientID %>").val();
                    var startDate = "1900-1-1";
                    var v = new Date(startDate.replace(/\-/g, '/')).getTime() - new Date(birthDay.replace(/\-/g, '/')).getTime();
                    if (v > 0) {
                        tableToolbar._showMsg("出生日期不合法！", function() {
                            $("#<%=tbBirthDay.ClientID %>").focus();
                        })
                        return false;
                    }

                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/ManageCenter/Hr/HrAdd.aspx?";
                    url += $.param({
                        doType: PageJsData.Query.doType,
                        save: "save",
                        sl: PageJsData.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(PageJsData.Form).serialize() + "&" + $.param(this.UserData),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                tableToolbar._showMsg(result.msg, function() {
                                    var url = "/ManageCenter/Hr/HrList.aspx?sl=" + PageJsData.Query.sl;
                                    var refererUrl = '<%=Request.QueryString["refererUrl"] %>';
                                    if (refererUrl.length > 10) url = refererUrl;
                                    window.location = url;
                                });
                            }
                            else { tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); }); }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            tableToolbar._showMsg(tableToolbar.errorMsg, function() { PageJsData.BindBtn(); });

                        }
                    });
                }
            },
            Sure: function() {
                var that = this, msg = "",
                 username = $.trim($("#<%=tbUsername.ClientID %>").val()),
                 pwd = $.trim($("#<%=tbUserpwd.ClientID %>").val()),
                 pwd1 = $.trim($("#<%=tbrepeatpwd.ClientID %>").val());

                if (username == "") {
                    msg = "-用户名不能为空!<br/>";
                    $("#<%=tbUsername.ClientID %>").focus();
                }
                if (pwd == "") {
                    msg += "-密码不能为空!<br/>";
                    $("#<%=tbUserpwd.ClientID %>").focus();
                } else {
                    if (pwd != pwd1) {
                        msg += "-两次密码不一致!<br/>";
                        $("#<%=tbrepeatpwd.ClientID %>").focus();
                    }
                }
                if (msg != "") {
                    tableToolbar._showMsg(msg);
                    return false;
                }
                else {
                    if (!this.isExistsUsername()) {
                        this.UserData.u = username;
                        this.UserData.p = pwd;
                        that.HideBoxy();
                    } else {
                        tableToolbar._showMsg("已经存在相同的用户名");
                        $("#<%=tbUsername.ClientID %>").val("");
                        $("#<%=tbUserpwd.ClientID %>").val("");
                        $("#<%=tbrepeatpwd.ClientID %>").val("");
                    }
                }
            },
            //绑定按钮功能
            BindBtn: function() {
                $("#btnSave").unbind("click");
                $("#btnSave").click(function() {
                    PageJsData.Save();
                    return false;
                })
                $("#aSave").unbind("click");
                $("#aSave").click(function() {
                    PageJsData.Sure();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            },
            //设置工龄
            setGongLing: function(obj) {
                var _year = "<%=DateTime.Now.Year %>";
                var _date = $(obj).val();
                var _arr = _date.split("-");
                if (_arr.length != 3) return;

                var _v = tableToolbar.getInt(_year) - tableToolbar.getInt(_arr[0]);

                $("#<%=tbWorkyear.ClientID %>").val(_v);
            },
            //设置生日
            setShengRi: function(obj) {
                var _data = getShenFenZhengInfo($(obj).val());
                var _$birtyday = $("#<%=tbBirthDay.ClientID %>");
                var _$gender = $("#<%=selSex.ClientID %>");

                if (_data.isYouXiao) {
                    _$birtyday.val(_data.birthday);
                    if (_data.gender == "female") _$gender.val("1");
                    else if (_data.gender == "male") _$gender.val("0");
                    else _$gender.val("2");
                }
                else {
                    //_$birtyday.val("");
                    //_$gender.val("2");
                }
            }
        }

        $(function() {
            PageJsData.BindBtn();
            //alert($("#tbUsername").val());
            //自动添加行
            $(".tableEdu").autoAdd({
                tempRowClass: "EduTr",
                addButtonClass: "addbtn",
                delButtonClass: "delbtn"
            })
            $(".tableWork").autoAdd({
                tempRowClass: "WorkTr",
                addButtonClass: "addbtnWork",
                delButtonClass: "delbtnWork"
            })
            $(".tableHome").autoAdd({
                tempRowClass: "HomeTr",
                addButtonClass: "addbtnHome",
                delButtonClass: "delbtnHome"
            })
            $(".tableContact").autoAdd({
                tempRowClass: "ContactTr",
                addButtonClass: "addbtnContact",
                delButtonClass: "delbtnContact"
            })
            //加载出来为是分配系统帐号的话 则禁用掉否
            if ($("#<%=hdUserid.ClientID %>").val() != "") {
                $("#<%=rbSetSysID.ClientID %>").find(":radio:not(:checked)").attr("disabled", "disabled");
            }
            //加载出来为是同步导游帐号的话 则禁用掉否
            if ($("#<%=hidGuid.ClientID %>").val() != "") {
                $("#<%=rbGuid.ClientID %>").find(":radio:not(:checked)").attr("disabled", "disabled");
            }
            //是否分配帐号弹窗
            $("#<%=rbSetSysID.ClientID %>").find(":radio").click(function() {
                if (this.value == 1 && this.checked == true) {
                    PageJsData.ShowBoxy();
                }
            })

            if ($.trim($("#<%=tbUsername.ClientID %>").val()) != "") {
                $("#<%=tbUsername.ClientID %>").attr("readonly", "readonly").css("background-color", "#dadada");
                $("#<%=tbUserpwd.ClientID %>").attr("readonly", "readonly").css("background-color", "#dadada");
                $("#<%=tbrepeatpwd.ClientID %>").attr("readonly", "readonly").css("background-color", "#dadada");
                $("#aSave").remove();
                PageJsData.UserData.u = $.trim($("#<%=tbUsername.ClientID %>").val());
            }

            //$("#<%=tbIntime.ClientID %>").change(function() { PageJsData.setGongLing(this); });
            $("#<%=tbIdcard.ClientID %>").change(function() { PageJsData.setShengRi(this); });
        })
    </script>

</asp:Content>

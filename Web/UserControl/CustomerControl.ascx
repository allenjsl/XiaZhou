<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerControl.ascx.cs"
    Inherits="Web.UserControl.CustomerControl" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<span class="formtableT formtableT02">游客信息</span>
<table width="99%" cellspacing="0" cellpadding="0" border="0" style="height: auto;
    zoom: 1; overflow: hidden;">
    <tbody>
        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                姓名
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                类型
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                证件类型
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                证件号码
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                性别
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                联系方式
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                保险
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                备注
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                短信通知
            </td>
            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                操作
            </td>
        </tr>
        <cc1:CustomRepeater ID="rptList" runat="server">
            <AlternatingItemTemplate>
                <tr>
                    <td height="28" align="center">
                        <input type="text" value="夏丽" id="textfield15" class="formsize50" name="textfield15">
                    </td>
                    <td align="center">
                        <select>
                            <option value="0">成人</option>
                            <option value="1">儿童</option>
                            <option value="2">其他</option>
                        </select>
                    </td>
                    <td align="center">
                        <select name="select">
                            <option value="0">-请选择-</option>
                            <option value="1">身份证</option>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" style="width: 110px;" value="330011879048754031" id="textfield20"
                            name="textfield20">
                    </td>
                    <td align="center">
                        <select name="select2">
                            <option>男</option>
                            <option selected="selected">女</option>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" value="13897620345" id="textfield17" class="formsize80" name="textfield17">
                    </td>
                    <td align="center">
                        <a id="link2" href="baoxiangm-xybox.html">
                            <img src="/images/y-cuohao.gif"></a>
                    </td>
                    <td align="center">
                        <input type="text" id="textfield2" style="width: 120px;" name="textfield2">
                    </td>
                    <td align="center">
                        <input type="checkbox" checked="checked" value="" class="fuxuank" name="">
                        出团通知
                        <input type="checkbox" class="fuxuank" value="" name="">
                        回团通知
                    </td>
                    <td align="right">
                        <input type="checkbox" style="border: none;" value="checkbox" name="checkbox">
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <ItemTemplate>
                <tr>
                    <td height="28" align="center">
                        <input type="text" value="夏丽" id="textfield15" class="formsize50" name="textfield15">
                    </td>
                    <td align="center">
                        <select>
                            <option value="0">成人</option>
                            <option value="1">儿童</option>
                            <option value="2">其他</option>
                        </select>
                    </td>
                    <td align="center">
                        <select name="select">
                            <option value="0">-请选择-</option>
                            <option value="1">身份证</option>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" style="width: 110px;" value="330011879048754031" id="textfield20"
                            name="textfield20">
                    </td>
                    <td align="center">
                        <select name="select2">
                            <option>男</option>
                            <option selected="selected">女</option>
                        </select>
                    </td>
                    <td align="center">
                        <input type="text" value="13897620345" id="textfield17" class="formsize80" name="textfield17">
                    </td>
                    <td align="center">
                        <a id="link2" href="baoxiangm-xybox.html">
                            <img src="/images/y-cuohao.gif"></a>
                    </td>
                    <td align="center">
                        <input type="text" id="textfield2" style="width: 120px;" name="textfield2">
                    </td>
                    <td align="center">
                        <input type="checkbox" checked="checked" value="" class="fuxuank" name="">
                        出团通知
                        <input type="checkbox" class="fuxuank" value="" name="">
                        回团通知
                    </td>
                    <td align="right">
                        <input type="checkbox" style="border: none;" value="checkbox" name="checkbox">
                    </td>
                </tr>
            </ItemTemplate>
        </cc1:CustomRepeater>
        <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top #B9DBEB;">
            <td height="28" align="right" colspan="10">
                <a class="addbtn" href="javascript:void(0)">
                    <img border="0" width="48" height="20" src="/images/addimg.gif" /></a> <a class="addbtn"
                        href="javascript:void(0)">
                        <img border="0" src="/images/delimg.gif" /></a> <a class="delbtn" href="javascript:void(0)">
                        </a>
                <input type="checkbox" style="border: none;" value="checkbox" name="checkbox4" />
            </td>
        </tr>
    </tbody>
</table>

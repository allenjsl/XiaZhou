<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttBookIn.aspx.cs" Inherits="Web.ManageCenter.Attendance.AttBookIn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
        .errmsg{color:#f00;font-size:12px;}
   </style>
</head>
<body style="background: 0 none;">
<form id="form1" method="post" runat="server">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9" id="tableForm"
            style="margin: 0 auto">
            <tr>
                <td width="12%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg" >*</span>考勤时间：
                </td>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <input name="txtTime" id="txtTime"  type="text" class="formsize120"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" valid="required" errmsg="考勤时间不能为空！" runat="server"/>
                </td>
            </tr>
            <tr>
                <td width="12%" rowspan="9" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    考勤情况：
                </td>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <input id="ckZuiDian" type="checkbox" name="ckType" style="border: 0" runat="server"/>
                    准点
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <input id="ckChiDao" type="checkbox" name="ckType" style="border: 0"  runat="server"/>
                    迟到
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <input id="ckZaoTui" type="checkbox" name="ckType" style="border: 0"  runat="server"/>
                    早退
                </td>
            </tr>
            <tr>
                <td height="28" colspan="2" align="left" bgcolor="#e0e9ef">
                    <input id="ckKuangGong" type="checkbox" name="ckType" style="border: 0"  runat="server" valid="requireChecked" min="1" errmsg="考勤类别必须选择！"/>
                    旷工
                </td>
            </tr>
            <tr>
                <td width="7%" rowspan="2" align="left" bgcolor="#e0e9ef">
                    <label>
                        <input type="checkbox" id="ckLeave" style="border: 0;" runat="server"/>
                        请假
                    </label>
                </td>
                <td width="81%" height="28" align="left" bgcolor="#e0e9ef">
                    请假事由：
                    <input name="txtLeaveCause" id="txtLeaveCause" type="text" class="formsize450"  runat="server"/>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <span class="errmsg" >*</span>请假时间：
                    <input name="txtLeaveStartTime" id="txtLeaveStartTime" type="text" class="formsize120" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtLeaveEndTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})" valid="required" errmsg="请假开始时间不能为空！" readonly="readonly"  onchange="getDays('txtLeaveStartTime','txtLeaveEndTime','txtLeaveDays',24*3600*1000,1)"  runat="server"/>
                    <a href="javascript:getFocus('txtLeaveStartTime');" class="timesicon">开始时间</a>至
                    <input name="txtLeaveEndTime" id="txtLeaveEndTime" type="text" class="formsize120"  onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtLeaveStartTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})" valid="required" errmsg="请假结束时间不能为空！"  readonly="readonly"  onchange="getDays('txtLeaveStartTime','txtLeaveEndTime','txtLeaveDays',24*3600*1000,1)"  runat="server"/>
                    <a href="javascript:getFocus('txtLeaveEndTime');" class="timesicon">结束时间</a> 请假天数：
                    <input name="txtLeaveDays" id="txtLeaveDays" type="text" class="formsize80" readonly="readonly" runat="server"/>
                </td>
            </tr>
            <tr>
                <td height="28" rowspan="2" align="left" bgcolor="#e0e9ef">
                    <input type="checkbox" id="ckAdd" style="border: 0;"  runat="server"/>
                    加班
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    加班内容：
                    <input name="txtAddContent" id="txtAddContent" type="text" class="formsize450"  runat="server"/>
                </td>
            </tr>
            <tr>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <span class="errmsg" >*</span>加班时间：
                    <input name="txtAddStartTime" id="txtAddStartTime" type="text" class="formsize120" onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtAddEndTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})" valid="required" errmsg="加班开始时间不能为空！"  readonly="readonly"  onchange="getDays('txtAddStartTime','txtAddEndTime','txtAddDays',3600*1000,1)"  runat="server"/>
                    <a href="javascript:getFocus('txtAddStartTime');" class="timesicon">开始时间</a>至
                    <input name="txtAddEndTime" id="txtAddEndTime" type="text" class="formsize120" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtAddStartTime\')}',dateFmt:'yyyy-MM-dd HH:mm'})" valid="required" errmsg="加班结束时间不能为空！"  readonly="readonly"   onchange="getDays('txtAddStartTime','txtAddEndTime','txtAddDays',3600*1000,1)"  runat="server"/>
                    <a href="javascript:getFocus('txtAddEndTime');" class="timesicon">结束时间</a> 加班时间（小时）：
                    <input name="txtAddDays" id="txtAddDays" type="text" class="formsize80" readonly="readonly"  runat="server"/>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: center;">
            <asp:LinkButton ID="btnSubmit" runat="server" hidefocus="true"  OnClick="btnClick"><s class="baochun"></s>保 存</asp:LinkButton>
            <a href="javascript:resetForm();" hidefocus="true"><s class="chongzhi"></s>重 置</a>
        </div>
    </div>
    </form>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>
    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>
    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>
       <script type="text/javascript">
           $(function() {
               $("#btnSubmit").click(function() {
                   checkValid();
                   var form = $(this).closest("form").get(0);
                   return ValiDatorForm.validator(form, "alert");
               });
               FV_onBlur.initValid($("#btnSubmit").closest("form").get(0));
           });
           function getFocus(i) {
               document.getElementById(i).focus();
           }
           function checkValid() {
               $obj = $("#ckZuiDian,#ckChiDao,#ckZaoTui,#ckKuangGong,#ckLeave,#ckAdd");
               var f = 0;
               for (var i = 0; i < $obj.length; i++) {
                   if ($obj[i].checked) {
                       f = 1;
                       break;
                   }
               }
               if (f == 1) {
                   $("#ckKuangGong").removeAttr("valid");
               }
               else {
                   $("#ckKuangGong").attr("valid", "requireChecked");
               }
           
               $objLeave = $("#txtLeaveStartTime,#txtLeaveEndTime");
               $objAdd = $("#txtAddStartTime,#txtAddEndTime");
               if ($("#ckLeave")[0].checked == false) {
                   $objLeave.removeAttr("valid");
               }
               else {
                   $objLeave.attr("valid", "required");
               }
               if ($("#ckAdd")[0].checked == false) {
                   $objAdd.removeAttr("valid");
               }
               else {
                   $objAdd.attr("valid", "required");
               }
           }
           $(function() {
               $obj = $("#ckZuiDian,#ckChiDao,#ckZaoTui,#ckKuangGong");
               $objType = $("#ckLeave,#ckAdd");
               $obj.click(function() {
                   if (this.checked) {
                       $obj.not(this).removeAttr("checked");
                   }
                   if ($("#ckKuangGong")[0].checked) {
                       if ($objType[0].checked && $objType[1].checked) { 
                           $objType[0].checked = true;
                           $objType[1].checked = false;                        
                       }
                   }
               })
               //其中，在非旷工状态下请假和加班可复选
               $objType.click(function() {
                   var f = 0;
                   if (!$("#ckKuangGong")[0].checked) {
                       f = 1;
                   }
                   if (this.checked && f == 0) {
                       $objType.not(this).removeAttr("checked");
                   }
               })
           })
           //自动计算天数或秒数等间隔.n为保留几位小数
           function getDays(startID, endID, showID, num, n) {
               var start = $("#" + startID).val();
               var end = $("#" + endID).val();
               if (start != "" && end != "") {
                   var v = new Date(end.replace(/\-/g, '/')).getTime() - new Date(start.replace(/\-/g, '/')).getTime();
                   $("#" + showID).val(formatNum(v / num, n)); //v为毫秒数
               }
           }
           //保留小的小数位
           function formatNum(num, n) {//参数说明：num 要格式化的数字 n 保留小数位
               num = String(num.toFixed(n));
               // var re = /(-?\d+)(\d{3})/;//每三位用逗号分隔
               //while (re.test(num)) num = num.replace(re, "$1,$2")
               return num;
           }
           function resetForm() {
               document.getElementById('form1').reset();
           }      
           </script>        
</body>
</html>
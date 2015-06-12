<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePresetBusType.aspx.cs"
    Inherits="EyouSoft.Web.TeamCenter.UpdatePresetBusType" %>

<%@ Register Src="../UserControl/PresetBusType.ascx" TagName="PresetBusType" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="wrap">
        <div style="width: 99%" class="tablelist-box ">
            <div class="alertbox-outbox">
                <uc1:presetbustype id="PresetBusType1" runat="server" />
                <input type="hidden" runat="server" id="OperateID" value="" />
            </div>
            <div class="alertbox-btn" style="text-align: right;">
                <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                    id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
                <a href="javascript:void(0);" onclick="UpdatePageObj.HideForm()" hidefocus="true"><s
                    class="chongzhi"></s>关 闭</a>
            </div>
        </div>
    </div>
    </form>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            UpdatePageObj.BindBtn();
        })
        var UpdatePageObj = {
            HideForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Msg: "",
            CheckCarType: function() {
                $(".alertbox-outbox").find("select[name='selPre']").each(function(i) {
                    if ($(this).val() == "") {
                        UpdatePageObj.Msg += "请选择第" + parseInt(i + 1) + "个车型!</br>";
                    }
                })
            },
            Save: function() {
                 if (tableToolbar.IsHandleElse == "false") {
                            var msgList = [],opeaterid=$("#<%=OperateID.ClientID %>").val();
                            if (opeaterid != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法修改车型!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                 }                
                $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                var url = "/TeamCenter/UpdatePresetBusType.aspx?";
                url += $.param({
                    save: "save", tourId: '<%=Request.QueryString["tourId"] %>', sl: '<%=Request.QueryString["sl"] %>'
                });
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(result) {
                        if (result.result == "1") {
                            top.tableToolbar._showMsg(result.msg, function() {
                                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                parent.location.href = parent.location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(result.msg, function() {
                                UpdatePageObj.BindBtn();
                            });
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        UpdatePageObj.BindBtn();
                    }
                });
            },
            BindBtn: function() {
                UpdatePageObj.GetOldCarTypeName();
                $("#btnSave").click(function() {
                    UpdatePageObj.MatchCarType();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            },
            oldcartypename:[],
            GetOldCarTypeName:function(){
                $(".alertbox-outbox").find("input:[data-class='oldcarTypeName']").each(function(){
                    var self=$.trim($(this).val());
                    if(self!=""){
                         UpdatePageObj.oldcartypename.push(self);
                     }
                })
            },
            MatchCarType:function(){
                var newcartypename=[];
                $(".alertbox-outbox").find("input:[data-class='carTypeName']").each(function(){
                    newcartypename.push($(this).val());
                })
                var flag=false;
                for(var i=0;i<UpdatePageObj.oldcartypename.length;i++){
                    for(var j=0;j<newcartypename.length;j++){
                       if(UpdatePageObj.oldcartypename[i]==newcartypename[j]){
                            flag=true;
                            break;
                       }
                    }
                    if(!flag){
                        UpdatePageObj.Msg="update";
                    }
                    else{
                        flag=false;
                    }
                }
                if($.trim(UpdatePageObj.Msg)=="update" && UpdatePageObj.oldcartypename.length>0){
                    parent.tableToolbar.ShowConfirmMsg("修改车型可能会影响订单中车位,确定要修改吗？",function(){
                        UpdatePageObj.Save();
                    })
                }else{
                    UpdatePageObj.Save();
                }
            }
        }
    </script>

</body>
</html>

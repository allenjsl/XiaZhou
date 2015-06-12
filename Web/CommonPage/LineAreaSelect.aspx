<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineAreaSelect.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.LineAreaSelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <form id="form1">
    <div class="alertbox-outbox">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="89%" height="28" align="left" bgcolor="#C1E5F5">
                    <span class="alertboxTableT">区域名称：</span>
                    <input name="txtlineName" type="text" class="inputtext formsize120" id="txtlineName"
                        value="<%=Request.QueryString["txtlineName"] %>" />
                    <input type="hidden" name="iframeId" id="iframeId" value='<%=Request.QueryString["iframeId"] %>' />
                    <input type="hidden" name="pIframeId" id="pIframeId" value='<%=Request.QueryString["pIframeId"] %>' />
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                    <input type="hidden" name="callBackFun" id="callBackFun" value='<%=Request.QueryString["callBackFun"] %>' />
                    <input type="hidden" name="id" id="id" value='<%=Request.QueryString["id"] %>' />
                    区域类别：
                    <select class="inputselect" id="areaType" name="areaType">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.ComStructure.AreaType)), EyouSoft.Common.Utils.GetQueryStringValue("areaType"))%>
                    </select>
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button><br />
                    <%if (EyouSoft.Common.Utils.GetQueryStringValue("sModel") != "1")
                      { %>
                    已选择：<span id="SelectOption"></span>
                    <%} %>
                </td>
            </tr>
        </table>
        <div class="hr_10">
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" id="tblList">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <th height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    <input type="checkbox" id="cbxSelectAll" />全选
                </th>
                <th align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    区域名称
                </th>
                <th align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    区域类型
                </th>
            </tr>
            <asp:Repeater runat="server" ID="rpt_list">
                <ItemTemplate>
                    <tr>
                        <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                            <input type="checkbox" dataclass="checkoption" name="contactID" value='<%#Eval("AreaId") %>'
                                data-areaname='<%#Eval("AreaName") %>' />
                        </td>
                        <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                            <%#Eval("AreaName")%>
                        </td>
                        <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                            <%#(int)Eval("Type")==0?"":Eval("Type")%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var SelectLineAreaPage = {
            selectTxt: "",
            selectValue: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var txtArray = new Array();
                var valArray = new Array();
                $("#SelectOption").find("input[name='contactID']:checked").each(function() {
                    txtArray.push($.trim($(this).attr("data-areaname")));
                    valArray.push($.trim($(this).val()));
                })
                SelectLineAreaPage.selectTxt = txtArray.join(',');
                SelectLineAreaPage.selectValue = valArray.join(',');

            },
            InitSetSelect: function() {
                if (SelectLineAreaPage.aid) {
                    var parentSpan = SelectLineAreaPage.parentWindow.$('#' + SelectLineAreaPage.aid).parent();
                    var oldValue=parentSpan.find("input[type='hidden']").val().split(',');
                    var oldVtext=parentSpan.find("input[type='text']").val().split(',');
                    var selectoption = SelectLineAreaPage.parentWindow["selectoption"];
                    if (selectoption && selectoption.value.length > 0) {
                        for (var i = 0; i < oldValue.length; i++) {
                            $("input[name='contactID'][value='" + selectoption.value[i] + "']").attr("checked", "checked");
                        }
                    }
                    if (selectoption.value.length>0 && selectoption.text.length>0) {
                        SelectLineAreaPage.AppendSelect(selectoption.value, selectoption.text);
                        return false;
                    }
                    SelectLineAreaPage.AppendSelect(oldValue,oldVtext);
                }
            },
            AppendSelect:function(oldvalue,oldtext){
                var selectoption = { value: [], text: [] };
                if(oldvalue.length>0 && oldvalue.length>0){
                    for(var i=0;i<oldvalue.length;i++){
                        if(oldvalue[i]!="" || oldtext[i]!=""){
                            selectoption.text.push(oldtext[i]);
                            selectoption.value.push(oldvalue[i]);
                            $("input[name='contactID'][value='" + oldvalue[i] + "']").attr("checked", "checked");
                             $("#SelectOption").append("<label><input type='checkbox' name='contactID' checked='checked' value='"+oldvalue[i]+"' data-areaname='"+oldtext[i]+"' /><span>"+oldtext[i]+"</span></label>");
                         }
                    }
                     SelectLineAreaPage.parentWindow["selectoption"] = selectoption;
                }
                SelectLineAreaPage.BindCheckBoxClick();
            },
            BindCheckBoxClick: function() {
                $("#SelectOption").find("input[type='checkbox']").click(function(){
                     if (this.value != "") {
                        $("input[name='contactID'][value='" + this.value + "']").attr("checked", "");
                       SelectLineAreaPage.RemoveCheckbox(this.value );
                    }
                })
            },
            RemoveCheckbox: function(val) {
               var selectoption = SelectLineAreaPage.parentWindow["selectoption"];
                if (selectoption) {
                    for (var i = 0; i < selectoption.value.length; i++) {
                        if (selectoption.value[i] == val) {
                            selectoption.value.splice(i, 1);
                            selectoption.text.splice(i, 1);
                        }
                    }
                    SelectLineAreaPage.parentWindow["selectoption"] = selectoption;
                }
                $("#SelectOption").find("[value='" + val + "']").parent().remove();
            }
        }
        $(function() {
            tableToolbar.init({
                //tableContainerSelector: "#tblList" //表格选择器
            })
            //判断是否为二级弹窗
            if (SelectLineAreaPage.pIframeID) {
                SelectLineAreaPage.parentWindow = window.parent.Boxy.getIframeWindow(SelectLineAreaPage.pIframeID);
            }
            else {
                SelectLineAreaPage.parentWindow = parent.window;
            }
            SelectLineAreaPage.InitSetSelect();
            $("#a_btn").click(function() {
                SelectLineAreaPage.SetValue();

                //根据父级是否为弹窗传值
                var data = { id: '<%=Request.QueryString["id"] %>', value: SelectLineAreaPage.selectValue, text: SelectLineAreaPage.selectTxt };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>'
                if (callBackFun.indexOf('.') == -1) {
                    SelectLineAreaPage.parentWindow[callBackFun](data);
                } else {
                    SelectLineAreaPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectLineAreaPage.iframeID).hide();
                return false;
            })
            //判断是否只能选中一项
            $("#tblList").find("input[type='checkbox']").click(function() {
                if ($("#sModel").val() != "2") {
                    $("#tblList").find("input[type='checkbox']").attr("checked", "");
                    $(this).attr("checked", "checked");
                }
            })
             $("input[name='contactID']").click(function() {
                   
                    var selectoption = SelectLineAreaPage.parentWindow["selectoption"];
                    if (this.checked) {
                        var value = new Array(), text = new Array();
                        value.push($.trim($(this).val()));
                        text.push($(this).attr("data-areaname"));
                        if (selectoption) {
                            selectoption.value.push($.trim($(this).val()));
                            selectoption.text.push($(this).attr("data-areaname"));
                        }
                        SelectLineAreaPage.AppendSelect(value, text);
                        SelectLineAreaPage.parentWindow["selectoption"] = selectoption;
                    } else {
                        var _s = this;
                        SelectLineAreaPage.RemoveCheckbox(_s.value);
                    }
                })
                
                $("#cbxSelectAll").click(function(){
                     var selectoption = SelectLineAreaPage.parentWindow["selectoption"];
                     
                     if (this.checked) {
                        var value = new Array(), text = new Array();
                        $("#tblList input[name='contactID']").each(function(){
                        if(!$(this).attr("checked")){
                                value.push($.trim($(this).val()));
                                text.push($.trim($(this).attr("data-areaname")));
                            }
                        }) 
                        if (selectoption) {
                            $("#tblList input[name='contactID']").each(function(){
                              if(!$(this).attr("checked")){
                                selectoption.value.push($.trim($(this).val()));
                                selectoption.text.push($.trim($(this).attr("data-areaname")));
                                }
                            })    
                        }
                        SelectLineAreaPage.AppendSelect(value, text);
                        SelectLineAreaPage.parentWindow["selectoption"] = selectoption;
                        $("#tblList input[name='contactID']").attr("checked","checked");
                    } else {
                         $("#tblList input[name='contactID']").each(function(){
                                SelectLineAreaPage.RemoveCheckbox($(this).val());
                            })    
                            $("#tblList input[name='contactID']").attr("checked","");
                        
                    }
                })
        })
    </script>

</body>
</html>

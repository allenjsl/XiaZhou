<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubjectManageEdit.aspx.cs"
    Inherits="EyouSoft.Web.FinanceManage.KingdeeSubject.SubjectManageEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>财务管理-金蝶科目-科目管理</title>
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/bt.min.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="99%" align="center" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9"
                style="margin: 0 auto">
                <tbody>
                    <tr>
                        <td width="13%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            科目类型：
                        </td>
                        <td align="left" colspan="3" class="inputselect">
                            <asp:DropDownList ID="sel_Xy" runat="server" CssClass="inputselect">
                            </asp:DropDownList>
                            <select id="sel_SourceType" style="display: none" class="inputselect">
                                <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.PlanStructure.PlanProject)))%>
                            </select>
                            <a id="a_FinanceAccountItem" class="xuanyong" href="javascript:void(0);" style="display: none">
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            科目类别：
                        </td>
                        <td width="43%" align="left">
                            <asp:TextBox ID="txt_SubjectType" CssClass="inputtext formsize140" runat="server"></asp:TextBox>
                            <a class="xuanyong" id="a_Subject" href="javascript:void(0);"></a>
                        </td>
                        <td width="13%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            科目代码：
                        </td>
                        <td width="29%" align="left">
                            <asp:TextBox ID="txt_SubjectCd" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="13%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            科目名称：
                        </td>
                        <td width="29%" align="left">
                            <asp:TextBox ID="txt_SubjectNm" CssClass="inputtext formsize140" runat="server"></asp:TextBox>
                        </td>
                        <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            助记码：
                        </td>
                        <td width="43%" align="left">
                            <asp:TextBox ID="txt_MnemonicCd" CssClass="inputtext formsize140" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            核算科目：
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txt_ChkItem" runat="server" CssClass="inputtext formsize450" ReadOnly="true"></asp:TextBox>
                            <a class="xuanyong" id="a_ChkItem" href="javascript:void(0);"></a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="a_Save"><s class="baochun"></s>保
                存</a><a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>关 闭</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var SubjectManageEdit = {
            KingDeeSubjectDataObj: {}, /*科目类别数据*/
            CustomerUnitData: {}, /*客户单位选用数据*/
            SubjectLXSelect: {
                "0": function() {/*单位直客选用*/
                    return "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                        typeid: '<%=(int)EyouSoft.Model.EnumType.CrmStructure.CrmType.单位直客 %>',
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        IsMultiple: "1",
                        callBackFun: "SubjectManageEdit.SubjectLXCustomerUnitCallBack",
                        thisClientID: "SubjectManageEdit",
                        customerUnitid: SubjectManageEdit.CustomerUnitData.CustomerUnitId || ""
                    })
                },
                "1": function() {/*同行客户选用*/
                    return "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                        typeid: '<%=(int)EyouSoft.Model.EnumType.CrmStructure.CrmType.单位直客 %>',
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        IsMultiple: "1",
                        callBackFun: "SubjectManageEdit.SubjectLXCustomerUnitCallBack",
                        thisClientID: "SubjectManageEdit",
                        customerUnitid: SubjectManageEdit.CustomerUnitData.CustomerUnitId || ""
                    })
                },
                "2": function() {/*部门*/
                    return "/CommonPage/SelectSection.aspx?" + $.param({
                        pIframeId: '<%=Request.QueryString["iframeId"]%>',
                        sModel: "1",
                        callBackFun: "SubjectManageEdit.SubjectSectionCallBack"
                    })
                },
                "3": function() {/*个人会员选用*/
                    return "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                        typeid: '<%=(int)EyouSoft.Model.EnumType.CrmStructure.CrmType.单位直客 %>',
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        IsMultiple: "1",
                        callBackFun: "SubjectManageEdit.SubjectLXCustomerUnitCallBack",
                        thisClientID: "SubjectManageEdit",
                        customerUnitid: SubjectManageEdit.CustomerUnitData.CustomerUnitId || ""
                    })
                },
                "4": function(SourceType) {/*供应商*/
                    switch (SourceType) {
                        case '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游 %>':
                            return "/CommonPage/SelectGuid.aspx?" + $.param({
                                callBackFun: "SubjectManageEdit.SubjectUseSupplierCallBack",
                                title: "导游-选用-",
                                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                                pIframeID: '<%=Request.QueryString["iframeId"]%>'
                            });
                        case '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料 %>':
                            return "/CommonPage/SelectObject.aspx?" + $.param({
                                callBackFun: "SubjectManageEdit.SubjectUseSupplierCallBack",
                                pIframeId: '<%=Request.QueryString["iframeId"]%>'
                            });
                        default:
                            return "/CommonPage/UseSupplier.aspx?" + $.param({
                                suppliertype: SourceType,
                                pIframeID: '<%=Request.QueryString["iframeId"]%>',
                                callBack: "SubjectManageEdit.SubjectUseSupplierCallBack"
                            })
                    }
                    //                	switch (SourceType) {
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.地接社 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.酒店 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.餐馆 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.游轮 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.国内游轮 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.车队 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.票务 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.景点 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.购物 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.购物 %>';
                    //                		break;
                    //                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.其他 %>':
                    //                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它 %>';
                    //                		break;
                    //                	}
                    //                    return "/CommonPage/UseSupplier.aspx?" + $.param({
                    //                        suppliertype: SourceType,
                    //                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
                    //                        callBack: "SubjectManageEdit.SubjectUseSupplierCallBack"
                    //                    })                
                },
                "5": function() {/*职员*/
                    return "/CommonPage/HrSelect.aspx?" + $.param({
                        pIframeId: '<%=Request.QueryString["iframeId"]%>',
                        sModel: "1",
                        callBackFun: "SubjectManageEdit.SubjectSectionCallBack",
                        isShow: "true"
                    })
                },
                "7": function() {/*团号*/
                    return "/CommonPage/TourListBoxy.aspx?" + $.param({
                        typeid: 1,
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        IsMultiple: "",
                        callBackFun: "SubjectManageEdit.SubjectTourCallBack",
                        isRadio: "true"
                    })
                },
                "8": function() {/*支付方式*/
                    return "/FinanceManage/KingdeeSubject/PayStyle.aspx?" + $.param({
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        callBackFun: "SubjectManageEdit.SubjectPayStyleCallBack"
                    })
                },
                "9": function() {//导游
                    return '/CommonPage/SelectGuid.aspx?sl=<%=Request.QueryString["sl"] %>&' + $.param({
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        callBackFun: "SubjectManageEdit.DaoYouCallBack",
                        id: "a_FinanceAccountItem"
                    })
                }
            },
            _callBack: function(name) {/*回调赋值*/
                $("#txt_SubjectNm").val(name);
                //            	$("#txt_MnemonicCd").val(Pinyin.GetJP(name));
            },

            SubjectLXCustomerUnitCallBack: function(data) {/*客户单位回调*/
                SubjectManageEdit.CustomerUnitData = data;
                SubjectManageEdit.KingDeeSubjectDataObj.ItemId = data.CustomerUnitId;
                SubjectManageEdit._callBack(data.CustomerUnitName);
                $("#<%=sel_Xy.ClientID%>").val(1);
            },
            SubjectSectionCallBack: function(data) {/*部门,职员回调*/
                SubjectManageEdit.KingDeeSubjectDataObj.ItemId = data.id || data.value;
                SubjectManageEdit._callBack(data.text);
            },
            SubjectUseSupplierCallBack: function(data) {/*供应商回调*/
                SubjectManageEdit.KingDeeSubjectDataObj.ItemId = data.id || data.value;
                SubjectManageEdit._callBack(data.name || data.text);
            },
            SubjectPayStyleCallBack: function(data) {/*支付方式回调*/
                if (data) {
                    var name = [], id = [];
                    for (var i in data) {
                        name.push(data[i].split(',')[0]);
                        id.push(data[i].split(',')[1]);
                    }
                    SubjectManageEdit.KingDeeSubjectDataObj.ItemId = id.join(",");
                    SubjectManageEdit._callBack(name.join(","));
                }

            },
            SubjectTourCallBack: function(data) {/*团号回调*/
                if (data) {
                    var name = [], id = [];
                    for (var i in data) {
                        name.push(data[i].split(',')[0]);
                        id.push(data[i].split(',')[1]);
                    }
                    SubjectManageEdit.KingDeeSubjectDataObj.ItemId = id.join(",");
                    SubjectManageEdit._callBack(name.join(','));
                }
            },
            KingDeeSubjectCallBack: function(data) {/*科目类别回调*/
                if (data) {
                    SubjectManageEdit.KingDeeSubjectDataObj.Id = data.Id;
                    SubjectManageEdit.KingDeeSubjectDataObj.dotSum = data.Cd.split('.').length - 1;
                    SubjectManageEdit.KingDeeSubjectDataObj.Cd = data.Cd;
                    $("#txt_SubjectType").val(data.Name);
                    $("#txt_SubjectCd").val(data.Cd);
                }
            },
            ChkItemCallBack: function(data) {
                SubjectManageEdit.KingDeeSubjectDataObj.ChkItemId = data.ids;
                if (data && data.names) {
                    SubjectManageEdit.KingDeeSubjectDataObj.ChkItem = data.names;
                    $("#txt_ChkItem").val(data.names);
                } else {
                    $("#txt_ChkItem").val("");
                }
            },
            DaoYouCallBack: function(data) {
                SubjectManageEdit.KingDeeSubjectDataObj.ItemId = data.value;
                SubjectManageEdit._callBack(data.text);
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#a_Save")
                obj.css({ "background-position": "", "text-decoration": "" })
                obj.html("<s class=baochun></s> 保 存");
                obj.unbind("click").click(function() {
                    var obj = $(this);
                    obj.unbind("click")
                    obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                    obj.html("<s class=baochun></s>&nbsp;&nbsp;提交中...");
                    var data = {};

                    //科目类型
                    data.SubjectTyp = parseInt($("#<%=sel_Xy.ClientID%>").val());
                    //科目类型对应关联编号（客户编号、部门编号、职员编号、计划编号、支付方式编号、供应商编号）
                    data.ItemId = SubjectManageEdit.KingDeeSubjectDataObj.ItemId || "";
                    //父级科目Id
                    data.PreSubjectId = SubjectManageEdit.KingDeeSubjectDataObj.Id || "";
                    //父级科目名称
                    data.PreSubjectNm = SubjectManageEdit.KingDeeSubjectDataObj.Name || "";
                    //核算项目Id
                    data.ChkItemId = SubjectManageEdit.KingDeeSubjectDataObj.ChkItemId || "";
                    //核算项目
                    data.ChkItem = SubjectManageEdit.KingDeeSubjectDataObj.ChkItem || "";
                    //科目代码
                    data.SubjectCd = $("#txt_SubjectCd").val();
                    //科目名称
                    data.SubjectNm = $("#txt_SubjectNm").val();
                    //助记码
                    data.MnemonicCd = $("#txt_MnemonicCd").val();
                    var msg = "";
                    /*没有父级表示顶级*/
                    if ('<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SubjectId")) %>' == "0") {
                        var p1 = data.PreSubjectId == "";
                        msg += p1 && data.SubjectCd == "" ? "科目代码不能为空!<br/>" : "";
                        /*验证顶级科目代码*/
                        msg += p1 && data.SubjectCd.indexOf('.') >= 0 ? "顶级科目代码不能存在“.”!<br/>" : "";
                        msg += p1 && !RegExps.RegInteger.test(data.SubjectCd) ? "顶级科目代码只能是整数!<br/>" : "";
                        /*科目代码内的点的个数比选用父级科目代码的点多一个*/
                        var p2 = data.SubjectCd.split('.').length - 2 == SubjectManageEdit.KingDeeSubjectDataObj.dotSum
                        msg += !p1 && !p2 ? "请不要越级添加修改!<br/>" : "";

                        /*科目代码中不包含父级代码*/
                        var p3 = data.SubjectCd.indexOf(SubjectManageEdit.KingDeeSubjectDataObj.Cd) < 0;
                        msg += !p1 && p3 ? "请不要修改父级科目代代码!<br/>" : ""

                        /*科目代码等于父级科目代码*/
                        var p4 = data.SubjectCd == SubjectManageEdit.KingDeeSubjectDataObj.Cd;
                        msg += !p1 && p4 ? "请输入本级科目代代码!<br/>" : ""
                    }
                    if (msg.length > 0) {
                        parent.tableToolbar._showMsg(msg);
                        that.BindBtn();
                        return false;
                    }
                    else {
                        var dataSubjectCdArr = data.SubjectCd.split('.');
                        for (var i in dataSubjectCdArr) {

                            if (!RegExps.RegInteger.test(dataSubjectCdArr[i])) {
                                parent.tableToolbar._showMsg("科目代码点与点之间只能是整数!");
                                that.BindBtn();
                                return false;
                            }
                        }
                    }
                    $.newAjax({
                        type: "post",
                        data: data,
                        cache: false,
                        url: "/FinanceManage/KingdeeSubject/SubjectManageEdit.aspx?" + $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            doType: "Save",
                            SubjectId: Boxy.queryString("SubjectId") || ""
                        }),
                        dataType: "json",
                        success: function(ret) {
                            if (parseInt(ret.result) === 1) {
                                parent.tableToolbar._showMsg('保存成功!', function() {
                                    window.parent.Boxy.getIframeDialog(Boxy.queryString("IframeId")).hide();
                                    parent.location.href = parent.location.href;
                                });
                            }
                            else {
                                parent.tableToolbar._showMsg(ret.msg);
                                that.BindBtn();
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            that.BindBtn();
                        }
                    });

                    return false;
                })
            },
            PageInit: function() {
                var that = this;
                $("#<%=sel_Xy.ClientID%>").click(function() {
                    if (parseInt($(this).val()) == '<%=(int)EyouSoft.Model.EnumType.KingDee.FinanceAccountItem.供应商 %>') {
                        $("#sel_SourceType").css("display", "");
                        $("#a_FinanceAccountItem").css("display", "");
                        $("#<%=txt_SubjectNm.ClientID %>").attr("disabled", "disabled");
                    }
                    else if (parseInt($(this).val()) == '<%=(int)EyouSoft.Model.EnumType.KingDee.FinanceAccountItem.科目类别 %>') {
                        $("#a_FinanceAccountItem").css("display", "none");
                        $("#sel_SourceType").css("display", "none");
                        $("#<%=txt_SubjectNm.ClientID %>").removeAttr("disabled");
                    }
                    else {
                        $("#a_FinanceAccountItem").css("display", "");
                        $("#sel_SourceType").css("display", "none");
                        $("#<%=txt_SubjectNm.ClientID %>").attr("disabled", "disabled");
                    }
                })
                $("#<%=sel_Xy.ClientID%>").click();
                if ('<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SubjectId")) %>' == "0") {
                    //科目 类别 选用
                    $("#a_Subject").unbind("click").click(function() {
                        parent.Boxy.iframeDialog({
                            iframeUrl: '/CommonPage/KingDeeSubject.aspx?' + $.param({
                                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                                pIframeID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>'
                            }),
                            title: "科目类别选用",
                            modal: true,
                            width: "400px",
                            height: "440px"
                        });
                        return false;
                    });
                    //科目 类型 选用
                    $("#a_FinanceAccountItem").unbind("click").click(function() {
                        var selId = parseInt($("#<%=sel_Xy.ClientID%>").val()) || 0;
                        parent.Boxy.iframeDialog({
                            iframeUrl: that["SubjectLXSelect"][selId]($("#sel_SourceType").val()),
                            title: "科目类型选用",
                            modal: true,
                            width: "700px",
                            height: "400px"
                        });
                        return false;
                    });
                }
                else {
                    $("#a_Subject").prev(":text").attr("disabled", "disabled");
                    $("#<%=sel_Xy.ClientID%>").attr("disabled", "disabled");
                }
                //核算项目选用
                $("#a_ChkItem").unbind("click").click(function() {
                    parent.Boxy.iframeDialog({
                        iframeUrl: "/CommonPage/KingDeeItem.aspx?" + $.param({ pIframeID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>', aid: "a_ChkItem", ids: SubjectManageEdit.KingDeeSubjectDataObj.ChkItemId }),
                        title: "核算项目选用",
                        modal: true,
                        width: "700px",
                        height: "400px"
                    });
                    return false;
                });
                //初始化 父级信息
                var parentSubject = $("#txt_SubjectType").val();
                if ((parentSubject = parentSubject.split('|')).length == 2) {
                    if (parentSubject[1] != "0") {
                        that.KingDeeSubjectCallBack({
                            Id: parentSubject[1],
                            Name: parentSubject[0],
                            Cd: $("#txt_SubjectCd").val()
                        })
                    }
                    $("#txt_SubjectType").val(parentSubject[0])
                }
                //初始化核算科目信息
                var ChkItemArr = $("#<%=txt_ChkItem.ClientID %>").val();
                if ((ChkItemArr = ChkItemArr.split('|')).length == 2) {
                    if (ChkItemArr[1] != "0") {
                        $.extend(SubjectManageEdit.KingDeeSubjectDataObj, {
                            ChkItemId: ChkItemArr[1],
                            ChkItem: ChkItemArr[0]
                        })
                    }
                    $("#txt_ChkItem").val(ChkItemArr[0])
                }
                //初始化科目名称信息
                var Item = $("#<%=txt_SubjectNm.ClientID %>").val();
                if ((Item = Item.split('|')).length == 2) {
                    if (Item[1] != "0") {
                        $.extend(SubjectManageEdit.KingDeeSubjectDataObj, {
                            ItemId: Item[1]
                        })
                    }
                    $("#txt_SubjectNm").val(Item[0]);
                }
                that.BindBtn();
            }
        }
        $(function() {
            SubjectManageEdit.PageInit();
        })              
    </script>

</body>
</html>

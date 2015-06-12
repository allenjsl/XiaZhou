<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdjustItemEdit.aspx.cs"
    Inherits="EyouSoft.Web.FinanceManage.KingdeeSubject.AdjustItemEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������-�����Ŀ-������Ŀ����</title>
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
                        <td width="15%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            ������Ŀ���ͣ�
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
                            ������Ŀ���
                        </td>
                        <td width="43%" align="left">
                            <asp:TextBox ID="txt_SubjectType" CssClass="inputtext formsize140" runat="server"></asp:TextBox>
                            <a class="xuanyong" id="a_Subject" href="javascript:void(0);"></a>
                        </td>
                        <td width="15%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            ������Ŀ���룺
                        </td>
                        <td width="29%" align="left">
                            <asp:TextBox ID="txt_ChkCd" runat="server" CssClass="inputtext formsize140"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" align="right" height="28" bgcolor="#b7e0f3" class="alertboxTableT">
                            ������Ŀ���ƣ�
                        </td>
                        <td width="29%" align="left">
                            <asp:TextBox ID="txt_ChkNm" CssClass="inputtext formsize140" runat="server"></asp:TextBox>
                        </td>
                        <td width="15%" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                            �����룺
                        </td>
                        <td width="43%" align="left">
                            <asp:TextBox ID="txt_MnemonicCd" CssClass="inputtext formsize140" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="a_Save"><s class="baochun"></s>��
                ��</a><a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                    class="chongzhi"></s>�� ��</a></div>
    </div>
    </form>

    <script type="text/javascript">

        var test = "";

        var AdjustItemEdit = {
            KingDeeSubjectDataObj: { ItemId: "" }, /*��Ŀ�������*/
            CustomerUnitData: {}, /*�ͻ���λѡ������*/
            SubjectLXSelect: {
                "0": function() {/*��λֱ��ѡ��*/
                    return "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                        typeid: '<%=(int)EyouSoft.Model.EnumType.CrmStructure.CrmType.��λֱ�� %>',
                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
                        IsMultiple: "1",
                        callBackFun: "AdjustItemEdit.SubjectLXCustomerUnitCallBack",
                        customerUnitid: AdjustItemEdit.CustomerUnitData.CustomerUnitId || ""
                    })
                },
                "1": function() {/*ͬ�пͻ�ѡ��*/
                    return "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                        typeid: '<%=(int)EyouSoft.Model.EnumType.CrmStructure.CrmType.��λֱ�� %>',
                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
                        IsMultiple: "1",
                        callBackFun: "AdjustItemEdit.SubjectLXCustomerUnitCallBack",
                        customerUnitid: AdjustItemEdit.CustomerUnitData.CustomerUnitId || ""
                    })
                },
                "2": function() {/*����*/
                    return "/CommonPage/SelectSection.aspx?" + $.param({
                        pIframeId: '<%=Request.QueryString["iframeId"] %>',
                        sModel: "1",
                        callBackFun: "AdjustItemEdit.SubjectSectionCallBack"
                    })
                },
                "3": function() {/*���˻�Աѡ��*/
                    return "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                        typeid: '<%=(int)EyouSoft.Model.EnumType.CrmStructure.CrmType.��λֱ�� %>',
                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
                        IsMultiple: "1",
                        callBackFun: "AdjustItemEdit.SubjectLXCustomerUnitCallBack",
                        customerUnitid: AdjustItemEdit.CustomerUnitData.CustomerUnitId || ""
                    })
                },
                "4": function(SourceType) {/*��Ӧ��*/
                    switch (SourceType) {
                        case '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.���� %>':
                            return "/CommonPage/SelectGuid.aspx?" + $.param({
                                callBackFun: "AdjustItemEdit.SubjectUseSupplierCallBack",
                                title: "����-ѡ��-",
                                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                                pIframeID: '<%=Request.QueryString["iframeId"] %>'
                            });
                        case '<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.���� %>':
                            return "/CommonPage/SelectObject.aspx?" + $.param({
                                callBackFun: "AdjustItemEdit.SubjectUseSupplierCallBack",
                                pIframeId: '<%=Request.QueryString["iframeId"] %>'
                            });
                        default:
                            return "/CommonPage/UseSupplier.aspx?" + $.param({
                                suppliertype: SourceType,
                                pIframeID: '<%=Request.QueryString["iframeId"] %>',
                                callBack: "AdjustItemEdit.SubjectUseSupplierCallBack"
                            })
                    }
//                	switch (SourceType) {
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.�ؽ��� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�ؽ� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.�Ƶ� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�Ƶ� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.�͹� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�ò� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.���� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�������� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.���� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�ó� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.Ʊ�� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.�ɻ� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.���� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.���� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.���� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.���� %>';
//                		break;
//                	case '<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.���� %>':
//                		SourceType='<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.���� %>';
//                		break;
//                	}
//                    return "/CommonPage/UseSupplier.aspx?" + $.param({
//                        suppliertype: SourceType,
//                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
//                        callBack: "AdjustItemEdit.SubjectUseSupplierCallBack"
//                    })
                },
                "5": function() {/*ְԱ*/
                    return "/CommonPage/HrSelect.aspx?" + $.param({
                        pIframeId: '<%=Request.QueryString["iframeId"] %>',
                        sModel: "1",
                        callBackFun: "AdjustItemEdit.SubjectSectionCallBack",
                        isShow: "true"
                    })
                },
                "7": function() {/*�ź�*/
                    return "/CommonPage/TourListBoxy.aspx?" + $.param({
                        typeid: 1,
                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
                        IsMultiple: "",
                        callBackFun: "AdjustItemEdit.SubjectTourCallBack",
                        isRadio: "1"
                    })
                },
                "8": function() {/*֧����ʽ*/
                    return "/FinanceManage/KingdeeSubject/PayStyle.aspx?" + $.param({
                        pIframeID: '<%=Request.QueryString["iframeId"] %>',
                        callBackFun: "AdjustItemEdit.SubjectPayStyleCallBack"
                    })
                },
                "9": function() {//����
                    return '/CommonPage/SelectGuid.aspx?sl=<%=Request.QueryString["sl"] %>&' + $.param({
                        pIframeID: '<%=Request.QueryString["iframeId"]%>',
                        callBackFun: "AdjustItemEdit.DaoYouCallBack",
                        id: "a_FinanceAccountItem"
                    })
                }
            },
            _callBack: function(name) {/*�ص���ֵ*/
                $("#txt_ChkNm").val(name);
            },
            SubjectLXCustomerUnitCallBack: function(data) {/*�ͻ���λ�ص�*/
                AdjustItemEdit.CustomerUnitData = data;
                AdjustItemEdit.KingDeeSubjectDataObj.ItemId = data.CustomerUnitId;
                AdjustItemEdit._callBack(data.CustomerUnitName);
                //$("#<%=sel_Xy.ClientID%>").val(data.CustomerUnitType);
                $("#<%=sel_Xy.ClientID%>").val(1);
            },
            SubjectSectionCallBack: function(data) {/*����,ְԱ�ص�*/
                AdjustItemEdit.KingDeeSubjectDataObj.ItemId = data.id || data.value;
                AdjustItemEdit._callBack(data.text);
            },
            SubjectUseSupplierCallBack: function(data) {/*��Ӧ�̻ص�*/
                AdjustItemEdit.KingDeeSubjectDataObj.ItemId = data.id || data.value;
                AdjustItemEdit._callBack(data.name || data.text);
            },
            SubjectPayStyleCallBack: function(data) {/*֧����ʽ�ص�*/
                if (data) {
                    var name = [], id = [];
                    for (var i in data) {
                        name.push(data[i].split(',')[0]);
                        id.push(data[i].split(',')[1]);
                    }
                    AdjustItemEdit.KingDeeSubjectDataObj.ItemId = id.join(",");
                    AdjustItemEdit._callBack(name.join(","));
                }
            },
            SubjectTourCallBack: function(data) {/*�źŻص�*/
                if (data) {
                    var name = [], id = [];
                    for (var i in data) {
                        name.push(data[i].split(',')[0]);
                        id.push(data[i].split(',')[1]);
                    }
                    AdjustItemEdit.KingDeeSubjectDataObj.ItemId = id.join(",");
                    AdjustItemEdit._callBack(name.join(','));
                }
                if (data) {
                    var name = [];
                    for (var i in data) {
                        name.push(data[i].split(',')[0]);
                    }
                    AdjustItemEdit._callBack(name.join(','));
                }
            },
            KingDeeSubjectCallBack: function(data) {/*��Ŀ���ص�*/
                if (data) {
                    AdjustItemEdit.KingDeeSubjectDataObj.Id = data.Id;
                    AdjustItemEdit.KingDeeSubjectDataObj.dotSum = data.Cd.split('.').length - 1
                    AdjustItemEdit.KingDeeSubjectDataObj.Cd = data.Cd;
                    $("#txt_SubjectType").val(data.Name);
                    $("#txt_ChkCd").val(data.Cd);


                }
            },
            DaoYouCallBack: function(data) {
                AdjustItemEdit.KingDeeSubjectDataObj.ItemId = data.value;
                AdjustItemEdit._callBack(data.text);
            },
            BindBtn: function() {
                var that = this;
                var obj = $("#a_Save");
                obj.css({ "background-position": "", "text-decoration": "" })
                obj.html("<s class=baochun></s> �� ��");
                obj.unbind("click").click(function() {
                    var obj = $(this);
                    obj.unbind("click")
                    obj.css({ "background-position": "0 -57px", "text-decoration": "none" })
                    obj.html("<s class=baochun></s>&nbsp;&nbsp;�ύ��...");
                    var data = {};
                    //��Ŀ���Ͷ�Ӧ������ţ��ͻ���š����ű�š�ְԱ��š��ƻ���š�֧����ʽ��š���Ӧ�̱�ţ�
                    data.ItemId = AdjustItemEdit.KingDeeSubjectDataObj.ItemId || "";
                    //������Ŀ����
                    data.ChkCate = parseInt($("#<%=sel_Xy.ClientID%>").val());
                    //����������ĿId
                    data.PreChkId = AdjustItemEdit.KingDeeSubjectDataObj.Id || "";
                    //����������Ŀ����
                    data.PreChkNm = AdjustItemEdit.KingDeeSubjectDataObj.Name || "";
                    //������Ŀ����
                    data.ChkCd = $("#txt_ChkCd").val();

                    var msg = "";
                    if ('<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SubjectId")) %>' == "0") {
                        /*û�и�����ʾ����*/
                        var p1 = data.PreChkId == "";
                        msg += p1 && data.ChkCd == "" ? "������Ŀ���벻��Ϊ��!<br/>" : "";

                        /*��֤������Ŀ����*/
                        msg += p1 && data.ChkCd.indexOf('.') >= 0 ? "����������Ŀ���벻�ܴ��ڡ�.��!<br/>" : "";
                        msg += p1 && !RegExps.RegInteger.test(data.ChkCd) ? "����������Ŀ����ֻ��������!<br/>" : "";
                        /*������Ŀ�����ڵĵ�ĸ�����ѡ�ø���������Ŀ����ĵ��һ��*/
                        var p2 = data.ChkCd.split('.').length - 2 == AdjustItemEdit.KingDeeSubjectDataObj.dotSum
                        msg += !p1 && !p2 ? "�벻ҪԽ������޸�!<br/>" : "";

                        /*������Ŀ�����в�������������*/
                        var p3 = data.ChkCd.indexOf(AdjustItemEdit.KingDeeSubjectDataObj.Cd) < 0;
                        
                        msg += !p1 && p3 ? "�벻Ҫ�޸ĸ���������Ŀ������!<br/>" : ""

                        /*������Ŀ������ڸ���������Ŀ����*/
                        var p4 = data.ChkCd == AdjustItemEdit.KingDeeSubjectDataObj.Cd;
                        msg += !p1 && p4 ? "�����뱾��������Ŀ������!<br/>" : ""
                    }
                    if (msg.length > 0) {
                        parent.tableToolbar._showMsg(msg);
                        that.BindBtn();
                        return false;
                    }
                    else {
                        var dataChkCdArr = data.ChkCd.split('.');
                        for (var i in dataChkCdArr) {
                            if (!RegExps.RegInteger.test(dataChkCdArr[i])) {
                                parent.tableToolbar._showMsg("������Ŀ��������֮��ֻ��������!");
                                that.BindBtn();
                                return false;
                            }
                        }
                    }
                    //������Ŀ����
                    data.ChkNm = $("#txt_ChkNm").val();
                    //������
                    data.MnemonicCd = $("#txt_MnemonicCd").val();
                    $.newAjax({
                        type: "post",
                        data: data,
                        cache: false,
                        url: "/FinanceManage/KingdeeSubject/AdjustItemEdit.aspx?" + $.param({
                            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                            doType: "Save",
                            SubjectId: '<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("SubjectId")) %>' || ""
                        }),
                        dataType: "json",
                        success: function(ret) {
                            if (parseInt(ret.result) === 1) {
                                parent.tableToolbar._showMsg('����ɹ�!', function() {
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
                    if (parseInt($(this).val()) == '<%=(int)EyouSoft.Model.EnumType.KingDee.FinanceAccountItem.��Ӧ�� %>') {
                        $("#sel_SourceType").css("display", "");
                        $("#a_FinanceAccountItem").css("display", "");
                        $("#<%=txt_ChkNm.ClientID %>").attr("disabled", "disabled");
                    }
                    else if (parseInt($(this).val()) == '<%=(int)EyouSoft.Model.EnumType.KingDee.FinanceAccountItem.��Ŀ��� %>') {
                        $("#a_FinanceAccountItem").css("display", "none");
                        $("#<%=txt_ChkNm.ClientID %>").removeAttr("disabled");
                    }
                    else {
                        $("#a_FinanceAccountItem").css("display", "");
                        $("#sel_SourceType").css("display", "none");
                        $("#<%=txt_ChkNm.ClientID %>").attr("disabled", "disabled");
                    }
                })
                $("#<%=sel_Xy.ClientID%>").click();
                if ('<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("ChkId")) %>' == "0") {
                    //������Ŀ ��� ѡ��
                    $("#a_Subject").unbind("click").click(function() {
                        parent.Boxy.iframeDialog({
                            iframeUrl: '/CommonPage/KingDeeSubject.aspx?' + $.param({
                                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                                pIframeID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>',
                                isType: 1
                            }),
                            title: "������Ŀ���ѡ��",
                            modal: true,
                            width: "400px",
                            height: "440px"
                        });
                        return false;
                    });
                    //������Ŀ ���� ѡ��
                    $("#a_FinanceAccountItem").unbind("click").click(function() {
                        var selId = parseInt($("#<%=sel_Xy.ClientID%>").val()) || 0;
                        parent.Boxy.iframeDialog({
                            iframeUrl: that["SubjectLXSelect"][selId]($("#sel_SourceType").val()),
                            title: "��Ŀ����ѡ��",
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
                var ChkCd = $("#<%=txt_ChkCd.ClientID %>").val();
                if ((ChkCd = ChkCd.split('|')).length == 2) {
                    if (ChkCd[1] != "0") {
                        $.extend(AdjustItemEdit.KingDeeSubjectDataObj, {
                            PreChkId: ChkCd[1]
                        })
                    }
                    $("#txt_ChkCd").val(ChkCd[0]);
                }
                //��ʼ�� ������Ϣ
                var parentSubject = $("#txt_SubjectType").val();
                if ((parentSubject = parentSubject.split('|')).length == 2) {
                    if (parentSubject[1] != "0") {
                        that.KingDeeSubjectCallBack({
                            Id: parentSubject[1],
                            Name: parentSubject[0],
                            Cd: $("#txt_ChkCd").val()
                        })
                    }
                    $("#txt_SubjectType").val(parentSubject[0])
                }
                //��ʼ��������Ŀ��Ϣ
                var Item = $("#<%=txt_ChkNm.ClientID %>").val();
                if ((Item = Item.split('|')).length == 2) {
                    if (Item[1] != "0") {
                        $.extend(AdjustItemEdit.KingDeeSubjectDataObj, {
                            ItemId: Item[1]
                        })
                    }
                    $("#txt_ChkNm").val(Item[0]);
                }

                that.BindBtn();
            }
        }
        $(function() {
            AdjustItemEdit.PageInit();
        })
    </script>

</body>
</html>

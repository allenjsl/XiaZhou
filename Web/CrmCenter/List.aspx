<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Web.CrmCenter.List"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    ��λ���ƣ�
                    <input type="text" size="30" class="inputtext formsize120" name="txtUnitName" value='<%=Utils.GetQueryStringValue("txtUnitName") %>' />
                    ʡ�ݣ�
                    <select id="ddlProvice" name="ddlProvice" class="inputselect">
                    </select>
                    ���У�
                    <select id="ddlCity" name="ddlCity" class="inputselect">
                    </select>
                    �ͻ��ȼ���
                    <select name="ddlLevId" class="inputselect" id="ddlLevId">
                        <%=BindCrmLevId()%>
                    </select>
                    �������ۣ�
                    <uc1:SellsSelect ID="txtXiaoShouYuan" runat="server" SelectFrist="false" />
                    ��ϵ�ˣ�<input type="text" class="inputtext formsize80" name="txtLxrName" id="txtLxrName" value="<%=Utils.GetQueryStringValue("txtLxrName") %>" />
                    <input type="submit" value="����" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="phXinZeng" Visible="false">
                    <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                        <span>����</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phXiuGai" Visible="false">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                        <span>�޸�</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phShanChu" Visible="false">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                    </s><span>ɾ��</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--�б���-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" rowspan="2">
                            <input type="checkbox" id="checkbox" name="checkbox" />
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            ���ڵ�
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            ��˾����
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            �Ƿ�ǩ����ͬ
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            ��������
                        </th>
                        <th align="center" class="th-line" colspan="4">
                            ���׼�¼
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            �������ʱ��
                        </th>
                        <th align="right" class="th-line" rowspan="2">
                            ����
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            ����
                        </th>
                    </tr>
                    <tr>
                        <th align="center" class="th-line">
                            ������
                        </th>
                        <th align="center" class="th-line">
                            ����
                        </th>
                        <th align="right" class="th-line">
                            ���
                        </th>
                        <th align="right" class="th-line">
                            ��Ƿ��
                        </th>                        
                    </tr>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <ItemTemplate>
                            <tr i_crmid="<%#Eval("CrmId") %>">
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("CrmId") %>'>
                                </td>
                                <td align="center">
                                    <asp:Literal runat="server" ID="ltrSuoZaiDi"></asp:Literal>
                                </td>
                                <td align="center">
                                    <%# Eval("Name")%>
                                </td>
                                <td align="center">
                                    <%# Convert.ToBoolean(Eval("IsXieYi"))?"��":"��"%>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("DingDanShu")%>
                                </td>
                                <td align="center">
                                    <%# Eval("DingDanRenShu")%>
                                </td>
                                <td align="right">
                                    <b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DingDanJinE"), this.ProviderToMoney)%></b>
                                </td>
                                <td align="right">
                                    <b class="fontbsize12">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TuoQianJinE"), this.ProviderToMoney)%></b>
                                </td>                                
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LastTime"), this.ProviderToDate)%>
                                </td>
                                <td align="right">
                                    <a href="javascript:void(0)" class="i_jifen">
                                        <%# Eval("JiFen","{0:F2}")%></a>
                                </td>
                                <td align="center">
                                    <a onclick="crmList.openAccountWindow('<%# Eval("CrmId") %>')" href="javascript:void(0)">
                                        �ʺŹ���</a>&nbsp;&nbsp; <a onclick="crmList.openDetailsWindow('<%# Eval("CrmId") %>')"
                                            href="javascript:void(0)">�鿴</a>
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("SellerId") %>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                        <tr>
                            <td colspan="50" align="center">
                                �������ݡ�
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
        </div>
        <!--�б����-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var crmList = {
            //��ѯ����
            winParams: {}
            //���
            , openAddWindow: function() {
                var params = { "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Edit.aspx", params),
                    title: "����-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "550px"
                });
            }
            //�޸�
            , openUpdateWindow: function(objsArr) {
                var params = { "crmId": objsArr[0].find("input[type='checkbox']").val(), "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Edit.aspx", params),
                    title: "�޸�-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "550px"
                });
            }
            //ɾ��
            , del: function(objArr) {
                var deleteids = [];
                //������ť�����������
                for (var i = 0; i < objArr.length; i++) {
                    var _crmid = objArr[i].find("input[type='checkbox']").val();
                    //������������ҵ��������ڣ������浽���������
                    if (_crmid != "on") {
                        deleteids.push(_crmid);
                    }
                }

                var params = { "doType": "delete", "deleteids": deleteids.join(","), "sl": "<%=SL %>" };

                $.newAjax({
                    type: "GET",
                    cache: false,
                    url: "List.aspx",
                    data: params,
                    dataType: "json",
                    async: false,
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg("ɾ���ɹ���")
                            window.location.reload();
                        }
                        else {
                            tableToolbar._showMsg("ɾ��ʧ��")
                        }
                    }
                });
            }
            //�˺Ź���
            , openAccountWindow: function(crmId) {
                var params = { "crmId": crmId, "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Account.aspx", params),
                    title: "�˺Ź���-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "390px"
                });
            }
            //�鿴
            , openDetailsWindow: function(crmId) {
                var params = { "crmId": crmId, "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Details.aspx", params),
                    title: "�ͻ����ϲ鿴-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "550px"
                });
            }
            //��ʼ��
            , init: function() {
                this.winParams = Boxy.getUrlParams();

                $("#ddlLevId").val(this.winParams["ddlLevId"]);
                $("#<%=txtXiaoShouYuan.SellsIDClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient) %>');
                $("#<%=txtXiaoShouYuan.SellsNameClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient) %>');
                $(".toolbar_add").bind("click", function() { crmList.openAddWindow(); });

                tableToolbar.init({ tableContainerSelector: "#liststyle"
                    , objectName: "�ͻ���λ !"
                    , updateCallBack: function(objsArr) { crmList.openUpdateWindow(objsArr); }
                    , deleteCallBack: function(objsArr) { crmList.del(objsArr); }
                });

                $('.tablelist-box').moveScroll();

                //init province and city
                pcToobar.init({ pID: "#ddlProvice"
                    , cID: "#ddlCity"
                    , pSelect: crmList.winParams["ddlProvice"]
                    , cSelect: crmList.winParams["ddlCity"]
                });

                //clone toolbar��paging
                $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            },
            openJiFenWindow: function(obj) {
                var _$tr = $(obj).closest("tr");

                var para = { sl: "<%=SL %>", crmId: _$tr.attr("i_crmid") };
                
                Boxy.iframeDialog({
                    iframeUrl: "/CrmCenter/GeRenHuiYuan/JiFen.aspx?" + $.param(para),
                    title: "������ϸ",
                    modal: true,
                    width: "860px",
                    height: "400px"
                });
            }
        };

        $(document).ready(function() {
            crmList.init();
            $(".i_jifen").click(function() { crmList.openJiFenWindow(this); });
        });
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>

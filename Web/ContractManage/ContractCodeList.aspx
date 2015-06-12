<%@ Page Title="合同管理" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ContractCodeList.aspx.cs" Inherits="Web.ContractManage.ContractCodeList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/HrSelect.ascx" TagName="HrSelect" TagPrefix="Uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="wrap">
        <div class="mainbox">
            <div class="tablehead" style="background: none #f6f6f6;">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a href="ContractCodeList.aspx?sl=<%=SL %>&type=0"
                        hidefocus="true" class="ztorderform" id="i_a_hetongleixing_0">
                        <span>国内合同</span></a></li>
                    <li><s class="orderformicon"></s><a href="ContractCodeList.aspx?sl=<%=SL %>&type=1"
                        hidefocus="true" class="ztorderform" id="i_a_hetongleixing_1">
                        <span>境外合同</span></a></li>
                    <li><s class="orderformicon"></s><a href="ContractCodeList.aspx?sl=<%=SL %>&type=2"
                        hidefocus="true" class="ztorderform" id="i_a_hetongleixing_2">
                        <span>单项合同</span></a></li>
                </ul>
            </div>
            <form id="form1" method="get">
            <input type="hidden" name="type" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>" />
            <input type="hidden" size="8" name="sl" value="<%=SL %>" />
            <div class="searchbox fixed">
                <span class="searchT">
			  <p>
			合同号：<input type="text" class="inputtext formsize100" name="txt_ConCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txt_ConCode") %>"/>  
        	领用人：<Uc1:HrSelect ID="HrSelect1" runat="server"  SetTitle="选择领用人"/>
			领用时间：<input class="inputtext formsize80" name="txt_SDate" type="text" onclick="WdatePicker()" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txt_SDate") %>"/>
			        - <input type="text" class="inputtext formsize80" name="txt_EDate" onclick="WdatePicker()" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txt_EDate") %>"/> 
			合同状态：
			    <select name="sltStatus" class="inputselect">
                    <asp:Literal ID="litStatue" runat="server"></asp:Literal>
                </select>
			 <input type="submit" id="btnSubmit" class="search-btn" /></p></span>
            </div>
            </form>
            <div class="tablehead">
                <ul class="fixed" id="btnAction">
                    <asp:PlaceHolder ID="phForAdd" runat="server">
                        <li><s class="dengji"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_add">
                            <span>登记合同</span></a></li>
                        <li class="line"></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phForCol" runat="server">
                        <li><s class="lyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_collar">
                            <span>领用</span></a></li>
                        <li class="line"></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phForDes" runat="server">
                        <li><s class="delicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_destroy">
                            <span>销号</span></a></li>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="phDelete">
                    <li><a href="javascript:void(0)" hidefocus="true" id="i_a_delete"><s class="delicon">
                    </s><span>删除</span></a></li>
                    </asp:PlaceHolder>
                </ul>
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
                </div>
            </div>
            <!--列表表格-->
            <div class="tablelist-box">
                <table width="100%" id="liststyle">
                    <tr>
                        <th class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox" />
                        </th>
                        <th align="center" class="th-line">
                            合同号
                        </th>
                        <th align="center" class="th-line">
                            领用时间
                        </th>
                        <th align="center" class="th-line">
                            领用人
                        </th>
                        <th align="center" class="th-line">
                            订单号
                        </th>
                        <th align="center" class="th-line">
                            线路名称
                        </th>
                        <th align="center" class="th-line">
                            客户单位
                        </th>
                        <th align="center" class="th-line">
                            人数
                        </th>
                        <th align="right" class="th-line">
                            合同金额
                        </th>
                        <th align="center" class="th-line">
                            销售员
                        </th>
                        <th align="center" class="th-line">
                            合同状态
                        </th>
                        <th align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr i_status="<%#(int)Eval("ContractStatus") %>" i_hetongid="<%#Eval("ContractId") %>">
                                <td align="center">
                                    <span class="thinputbg">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("ContractId")%>" data-code='<%#Eval("ContractCode") %>' />
                            </span>
                                </td>
                                <td align="center">
                                    <%#Eval("ContractCode")%>
                                </td>
                                <td align="center">
                                    <%#Eval("CollarTime", "{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("UseName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderCode")%>
                                </td>
                                <td align="center">
                                    <a href='<%#PrintPages %>?tourid=<%#Eval("TourId") %>' target="_blank">
                                        <%#Eval("RouteName")%></a>
                                </td>
                                <td align="center">
                                    <%#Eval("BuyCompanyName")%>
                                </td>
                                <td align="center">
                                    <%#((int)Eval("Adults") > 0 || (int)Eval("Childs") > 0) ? Eval("Adults") : ""%>
                                    <sup>
                                        <%#((int)Eval("Adults") > 0 || (int)Eval("Childs") > 0) ? "+" + Eval("Childs") : ""%></sup>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%#(decimal)Eval("SumPrice") > 0 ? (EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), this.ProviderToMoney)) : ""%>
                                    </b>
                                </td>
                                <td align="center">
                                    <%#Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContractStatus")%>
                                </td>
                                <td align="center">
                                    <%#GetCaoZuo(Eval("ContractStatus"))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <!--列表结束-->
            <div class="tablehead" style="border: 0 none;">
                <ul class="fixed">

                    <script type="text/javascript">
                        document.write($("#btnAction").html());
                    </script>

                </ul>
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                </div>
            </div>
        </div>
        <!-- InstanceEndEditable -->
    </div>

    <script type="text/javascript">
        var ContractCodeList = {
            type: '<%=(int)HeTongLeiXing %>',
            sl: '<%=SL %>',
            reload: function() {
                window.location.href = window.location.href;
            },
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗参数*/
                return {
                    url: "",
                    title: "",
                    width: "",
                    height: ""
                }
            },
            Add: function() {/*登记弹窗*/
                var data = this.DataBoxy();
                data.title += "合同登记";
                data.url = "/ContractManage/ContractCodeAdd.aspx?" + $.param({ sl: "<%=SL %>", type: ContractCodeList.type });
                data.width = "630px";
                data.height = "190px";
                this.ShowBoxy(data);
                return false;
            },

            Collar: function(objsArr) {/*领用弹窗*/
                var ids = new Array();
                var codes = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    ids.push(objsArr[i].find("input[type='checkbox']").val());
                    codes.push(objsArr[i].find("input[type='checkbox']").attr("data-code"));
                }
                var data = this.DataBoxy();
                data.title += "合同领用";
                data.url += "/ContractManage/ContractCodeCollar.aspx?" + $.param({ sl: ContractCodeList.sl, type: ContractCodeList.type, ids: ids.join(','), codes: codes.join(',') });
                data.width = "450px";
                data.height = "200px";
                this.ShowBoxy(data);
                return false;
            },
            Destroy: function(objsArr) {/*销毁作废弹窗*/
                var ids = new Array();
                var codes = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    ids.push(objsArr[i].find("input[type='checkbox']").val());
                    codes.push(objsArr[i].find("input[type='checkbox']").attr("data-code"));
                }
                var data = this.DataBoxy();
                data.title += "合同销号";
                data.url += "/ContractManage/ContractCodeDestroy.aspx?" + $.param({ sl: ContractCodeList.sl, type: ContractCodeList.type, ids: ids.join(','), codes: codes.join(',') });
                data.width = "850px";
                data.height = "400px";
                this.ShowBoxy(data);
                return false;
            },
            delHeTong: function(arr) {
                var _data = { txtHeTongId: [] };

                for (var i = 0; i < arr.length; i++) {
                    var _$tr = arr[i];
                    if (_$tr.attr("i_status") == "<%= (int)EyouSoft.Model.EnumType.ConStructure.ContractStatus.未领用 %>") _data.txtHeTongId.push(_$tr.attr("i_hetongid"));
                    else _$tr.find("input[type='checkbox']").removeAttr("checked");
                }

                if (_data.txtHeTongId.length == 0) { tableToolbar._showMsg("只能删除未领用的合同信息，请重新选择。"); return; }
                if (!confirm("合同删除操作不可恢复，你确定要删除吗？")) return;

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=deletehetong",
                    cache: false, dataType: "json", async: false, data: _data,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            ContractCodeList.reload();
                        } else {
                            alert(response.msg);
                        }
                    }
                });
            },
            BindBtn: function() {
                var that = this;
                $(".toolbar_add").click(function() {
                    that.Add();
                    return false;
                });
                var _options = { tableContainerSelector: "#liststyle", objectName: "合同号", otherButtons: [] };
                _options.otherButtons.push({ button_selector: '.toolbar_collar', sucessRulr: 2, msg: '未选中任何合同', buttonCallBack: function(objsArr) { ContractCodeList.Collar(objsArr); return false; } });
                _options.otherButtons.push({ button_selector: '.toolbar_destroy', sucessRulr: 2, msg: '未选中任何合同', buttonCallBack: function(objsArr) { ContractCodeList.Destroy(objsArr); return false; } });
                _options.otherButtons.push({ button_selector: '#i_a_delete', sucessRulr: 2, msg: '未选中任何合同', buttonCallBack: function(objsArr) { ContractCodeList.delHeTong(objsArr); return false; } });

                tableToolbar.init(_options);
            },
            PageInit: function() {
                this.BindBtn();
            },
            caoZuo: function(obj) {
                var _$obj = $(obj);
                var _$tr = _$obj.closest("tr");
                var _fs = _$obj.attr("data-fs");
                var _heTongId = _$tr.attr("i_hetongid");

                var _xiaoXi = "";

                switch (_fs) {
                    case "guanliandingdan": break;
                    case "quxiaoguanlian": _xiaoXi = "你确定要取消该合同与订单的关联吗？"; break;
                    case "xiaohao": _xiaoXi = "你确定要销号吗？"; break;
                    case "zuofei": _xiaoXi = "你确定要销号吗？"; break;
                    case "lingyong": break;
                    case "quxiaoxiaohao": _xiaoXi = "你确定要取消销号吗？"; break;
                    case "quxiaozuofei": _xiaoXi = "你确定要取消作废吗？"; break;
                }

                if (!confirm(_xiaoXi)) return false;

                $.ajax({ type: "POST", url: window.location.href + "&doType=shezhistatus", data: { txtHeTongId: _heTongId, txtFS: _fs },
                    cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        window.location.href = window.location.href;
                    }
                });
            }
        };

        $(function() {
            //初始化页面
            ContractCodeList.PageInit();
            $("#i_a_hetongleixing_<%=(int)HeTongLeiXing %>").addClass("de-ztorderform");
            $("a[data-class='caozuo']").click(function() { ContractCodeList.caoZuo(this); });
        })
    </script>

</asp:Content>

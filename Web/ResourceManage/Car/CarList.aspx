<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="CarList.aspx.cs" Inherits="Web.ResourceManage.Car.CarList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('#tablelist').find("a[data-contact='contact']").bt({
                contentSelector: function() {
                    return $(this).prev("span").html();
                },

                positions: ['bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 5,
                spikeGirth: 15,
                width: 550,
                overlap: 0,
                centerPointY: 4,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '200%' }
            });
        });
    </script>

    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <span class="searchT">
                <p>
                    省份：
                    <select name="ddlProvice" id="ddlProvice" class="inputselect">
                    </select>
                    城市：
                    <select name="ddlCity" id="ddlCity" class="inputselect">
                    </select>
                    车队名称：
                    <input type="text" size="30" maxlength="30" class="inputtext formsize120" name="txtteamName"
                        value='<%=Request.QueryString["txtteamName"] %>'>
                    车型：
                    <input type="text" class="inputtext formsize80" name="txtmodel" value='<%=Request.QueryString["txtmodel"]%>' />
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    <input type="submit" value="搜索" class="search-btn" /></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="ToolBar_1">
            <ul class="fixed">
                <asp:PlaceHolder ID="phForAdd" runat="server">
                    <li><s class="addicon"></s><a class="toolbar_add" id="link1" hidefocus="true" href="javascript:void(0)">
                        <span>新增</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForUpdate" runat="server">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForDel" runat="server">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                    </s><span>删除</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForImport" runat="server">
                    <li><s class="daochu"></s><a class="toolbar_daochu" onclick="toXls1();return false;"
                        hidefocus="true" href="javascript:void(0)"><span>导出</span></a></li>
                </asp:PlaceHolder>
                <li><s class="daochu"></s><a class="i_jiaoyixx_all" hidefocus="true" href="javascript:void(0)">
                    <span>全部交易明细</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box" id="tablelist">
            <table width="100%" cellspacing="0" border="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" rowspan="2">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th valign="middle" align="center" class="th-line h20" rowspan="2">
                            所在地
                        </th>
                        <th valign="middle" align="center" class="th-line h20" rowspan="2">
                            车队名称
                        </th>
                        <th valign="middle" align="center" class="th-line h20" rowspan="2">
                            联系人
                        </th>
                        <th valign="middle" align="center" class="th-line h20" colspan="5">
                            交易明细
                        </th>
                    </tr>
                    <tr>
                        <th valign="middle" align="center" class="th-line h20 nojiacu">
                            交易次数
                        </th>
                        <th valign="middle" align="center" class="th-line h20 nojiacu">
                            车队数量
                        </th>
                        <th valign="middle" align="center" class="th-line h20 nojiacu">
                            结算金额
                        </th>
                        <th valign="middle" align="center" class="th-line h20 nojiacu">
                            未付金额
                        </th>
                        <th valign="middle" align="center" class="th-line h20 nojiacu">
                            质检均分
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                                    <input type="checkbox" id="checkbox" name="checkbox" value="<%#Eval("GysId")%>">
                                </td>
                                <td align="center">
                                    <%#Eval("CPCD.ProvinceName")%>-<%#Eval("CPCD.CityName")%>
                                </td>
                                <td align="left">
                                    <a href="javascript:void(0);" class="a_UnitName" data-id='<%#Eval("GysId") %>'>
                                        <%#Eval("GysName")%></a>
                                    <%#EyouSoft.Common.UtilsCommons.GetCompanyRecommend(Eval("IsTuiJian"), Eval("IsQianDan"), Eval("IsFanLi"))%>
                                </td>
                                <td align="center">
                                    <span style="display: none;"><%#EyouSoft.Common.UtilsCommons.GetGysLxrFuDongHtml(Eval("Lxrs"))%></span> 
                                    <a data-contact="contact" href="javascript:void(0)" title=""><%#Eval("LxrName")%></a>
                                </td>
                                <td align="center">
                                    <%#Eval("JiaoYiXX.JiaoYiCiShu")%>
                                </td>
                                <td align="center">
                                    <%#Eval("JiaoYiXX.JiaoYiShuLiang")%>
                                </td>
                                <td align="center">
                                    <b class="fontgreen"><a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("GysId")%>'
                                        data-unitname='<%#Eval("GysName") %>'>
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("JiaoYiXX.JieSuanJinE"), ProviderToMoney)%></a></b>
                                </td>
                                <td align="center">
                                    <b class="fontred"><a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("GysId")%>'
                                        data-unitname='<%#Eval("GysName") %>'>
                                        <%#  EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("JiaoYiXX.WeiZhiFuJinE"), ProviderToMoney)%>
                                    </a></b>
                                </td>
                                <td align="center">
                                    <%#Eval("JiaoYiXX.ZhiJianJunFen","{0:F2}") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0pt none;" class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("ToolBar_1").innerHTML);
			</script>

        </div>
    </div>

    <script type="text/javascript">
        //页面初始化必须存在方法
        $(function() {
            CarPage.PageInit();
            tableToolbar.IsHandleElse = true;
            //当列表页面出现横向滚动条时使用以下方法
            //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
            $('.tablelist-box').moveScroll();
            $(".a_UnitName").click(function() {
                var url = "/ResourceManage/Car/CarEdit.aspx?";
                var self = this;
                url += $.param({
                    sl: '<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_车队 %>',
                    chakan: "chakan",
                    dotype: "update",
                    id: $(self).attr("data-id")
                })
                Boxy.iframeDialog({ iframeUrl: url, title: "查看", modal: true, width: "990px", height: "590px" });
                return false;
            });

            $(".i_jiaoyixx_all").click(function() {
                var url = "/resourcemanage/jiaoyimingxi.aspx?sl=" + '<%=SL %>&';
                var _data = { gysid: "all", gysname: "所有车队供应商", gysleixing: "<%=(int)EyouSoft.Model.EnumType.SourceStructure.SourceType.车队 %>" };
                Boxy.iframeDialog({ iframeUrl: url, title: "全部车队供应商-交易明细", modal: true, data: _data, width: "900px", height: "525px" });
                return false;
            });
        });
        
        var CarPage = {
            PageInit: function() {
                pcToobar.init({
                    pID: "#ddlProvice",
                    cID: "#ddlCity",
                    comID: '<%=this.SiteUserInfo.CompanyId %>',
                    gSelect: "1",
                    pSelect: '<%=Request.QueryString["ddlProvice"] %>',
                    cSelect: '<%=Request.QueryString["ddlCity"] %>'
                })
                $(".transactions").click(function() {
                    var url = "/ResourceManage/jiaoyimingxi.aspx?sl=" + '<%=SL %>&';
                    var para = { gysid: $(this).attr("Data-rel"), gysname: $(this).attr("Data-unitname") };
                    url += $.param(para);
                    Boxy.iframeDialog({
                        iframeUrl: url,
                        title: "交易情况",
                        modal: true,
                        width: "850px",
                        height: "525px"
                    });
                    return false;
                });
                //绑定功能按钮
                CarPage.BindBtn();
            },
            BindBtn: function() {
                //绑定Add事件
                $(".toolbar_add").click(function() {
                    CarPage.Add();
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        CarPage.Update(objsArr);
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        CarPage.DelAll(objsArr);
                    }
                })
            },
            Add: function() {
                CarPage.ShowBoxy({ iframeUrl: "/ResourceManage/Car/CarEdit.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_车队 %>", title: "新增车队", width: "990px", height: "440px" });
            },
            Update: function(objsArr) {
                CarPage.ShowBoxy({ iframeUrl: '/ResourceManage/Car/CarEdit.aspx?doType=update&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_车队 %>&id=' + objsArr[0].find("input[type='checkbox']").val(), title: "修改车队", width: "990px", height: "440px" });
            },
            DelAll: function(objArr) {
                var ajaxUrl = "/ashx/handler.ashx";
                var list = new Array();
                for (var i = 0; i < objArr.length; i++) {
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(objArr[i].find("input[type='checkbox']").val());
                    }
                }
                ajaxUrl += "?doType=DeleteGys&deletegysids=" + list.join(',') + "&sl=" + '<%=SL%>';
                CarPage.GoAjax(ajaxUrl, EnglishToChanges.Ping("Del"));
            },
            ShowBoxy: function(data) {
                Boxy.iframeDialog({
                    iframeUrl: data.iframeUrl,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { location.reload(); });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            }
        }
    </script>

</asp:Content>

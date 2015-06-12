<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="OtherList.aspx.cs" Inherits="Web.ResourceManage.Other.OtherList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('#liststyle').find("a[data-contact='contact']").bt({
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
                    单位名称：
                    <input type="text" size="20" class="inputtext formsize180" name="txtUnitName" value='<%=Request.QueryString["txtUnitName"]%>'>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    <input type="submit" value="搜索" class="search-btn" /></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="ToolBar_1">
            <ul class="fixed">
                <asp:PlaceHolder ID="other_add" runat="server">
                    <li><s class="addicon"></s><a id="link1" class="toolbar_add" hidefocus="true" href="javascript:void(0)">
                        <span>新增</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="other_edit" runat="server">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0);">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="other_del" runat="server">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0);"><s class="delicon">
                    </s><span>删除</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" onclick="toXls1();return false;"
                    href="javascript:void(0);"><span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
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
                            单位名称
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
                            交易人数
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
                                    <input type="checkbox" name="checkbox" value="<%#Eval("GysId") %>">
                                </td>
                                <td align="center">
                                    <%#Eval("CPCD.ProvinceName")%>-<%#Eval("CPCD.CityName")%>
                                </td>
                                <td align="left">
                                    <a href="javascript:void(0);" class="a_UnitName" data-id='<%#Eval("GysId") %>'><%#Eval("GysName")%></a>
                                    <%#EyouSoft.Common.UtilsCommons.GetCompanyRecommend(Eval("IsTuiJian"), Eval("IsQianDan"), Eval("IsFanLi"))%>
                                </td>
                                <td align="center">
                                    <span style="display: none;"><%#EyouSoft.Common.UtilsCommons.GetGysLxrFuDongHtml(Eval("Lxrs"))%></span> 
                                    <a data-contact="contact" href="javascript:void(0);" ctitle=""><%#Eval("LxrName")%></a>
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
            OtherPage.PageInit();
            $(".a_UnitName").click(function() {
                var url = "/ResourceManage/Other/OtherEdit.aspx?";
                var self = this;
                url += $.param({
                    sl: '<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_其它 %>',
                    chakan: "chakan",
                    dotype: "update",
                    id: $(self).attr("data-id")
                })
                Boxy.iframeDialog({ iframeUrl: url, title: "查看", modal: true, width: "990px", height: "570px" });
                return false;
            })
        })
        var OtherPage = {
            PageInit: function() {
                tableToolbar.IsHandleElse = true;
                OtherPage.BindBtn();
                $('.tablelist-box').moveScroll();
            },
            BindBtn: function() {
                //绑定Add事件
                $(".toolbar_add").click(function() {
                    OtherPage.Add();
                })
                $(".transactions").click(function() {
                var url = "/ResourceManage/jiaoyimingxi.aspx?sl=" + '<%=SL %>&';
                    var para = { gysid: $(this).attr("Data-rel"), gysname: $(this).attr("Data-unitname") };
                    url += $.param(para);
                    Boxy.iframeDialog({
                        iframeUrl: url,
                        title: "交易情况",
                        modal: true,
                        width: "780px",
                        height: "525px"
                    });
                    return false;
                });
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        OtherPage.Update(objsArr);
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        OtherPage.DelAll(objsArr);
                    }
                })
            },
            Add: function() {
                OtherPage.ShowBoxy({ iframeUrl: "/ResourceManage/Other/OtherEdit.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_其它 %>", title: "新增其他资源", width: "990px", height: "570px" });
            },

            Update: function(objsArr) {
                OtherPage.ShowBoxy({ iframeUrl: "/ResourceManage/Other/OtherEdit.aspx?doType=update&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_其它 %>&id=" + objsArr[0].find("input[type='checkbox']").val(), title: "修改其他资源数据", width: "990px", height: "570px" });
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
                OtherPage.GoAjax(ajaxUrl, EnglishToChanges.Ping("Del"));
            },
            dataBoxy: function() {
                this.url = "";
                this.title = "";
                this.width = "";
                this.height = "";
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
                $(".toolbar_delete").unbind("click");
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

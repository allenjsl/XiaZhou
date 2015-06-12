<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="GroundList.aspx.cs" Inherits="Web.ResourceManage.Ground.GroundList" %>

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
            <input type="hidden" value='<%=SL %>' />
            <form id="form1" method="get">
            <span class="searchT">
                <p>
                    省份：
                    <select id="ddlProvice" class="inputselect" name="ddlProvice">
                    </select>
                    城市：
                    <select id="ddlCity" class="inputselect" name="ddlCity">
                    </select>
                    单位名称：
                    <input type="text" class="inputtext formsize140" name="txtUnitName" value='<%=Request.QueryString["txtUnitName"]%>' />
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    <input type="submit" value="搜索" class="search-btn" /></p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="ToolBar_1">
            <ul class="fixed">
                <asp:PlaceHolder ID="phForAdd" runat="server">
                    <li><s class="addicon"></s><a class="toolbar_add" id="A3" hidefocus="true" href="javascript:void(0)">
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
                <asp:PlaceHolder ID="phstop" runat="server">
                    <li><a class="toolbar_stop" hidefocus="true" href="javascript:void(0);"><s class="stop">
                    </s><span>停用</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phstart" runat="server">
                    <li><a class="toolbar_start" hidefocus="true" href="javascript:void(0);"><s class="qyicon">
                    </s><span>启用</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pfblack" runat="server">
                    <li><a class="toolbar_hmd" hidefocus="true" href="javascript:void(0);"><s class="hmdicon">
                    </s><span>黑名单</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForImport" runat="server">
                    <li><s class="daochu"></s><a class="toolbar_daochu" onclick="toXls1();return false;"
                        hidefocus="true" href="javascript:void(0)"><span>导出</span></a></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box" id="tablelist">
            <table cellspacing="0" border="0" width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg" rowspan="2">
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
                        <th valign="middle" align="center" class="th-line h20" rowspan="2">
                            账号分配
                        </th>
                        <th valign="middle" align="center" class="th-line h20" rowspan="2">
                            账号状态
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
                            <tr class="<%#Container.ItemIndex%2==0 ? "odd":"" %>" i_userid="<%#Eval("UserInfo.UserId") %>" i_gysid="<%#Eval("GysId") %>">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                                    <input type="checkbox" id="checkbox" name="checkbox" value="<%#Eval("GysId")%>">
                                </td>
                                <td align="center">
                                    <%#Eval("CPCD.ProvinceName")%>-<%#Eval("CPCD.CityName")%>
                                </td>
                                <td align="left">
                                    <a href="javascript:void(0);" class="a_UnitName" data-id='<%#Eval("GysId") %>'>
                                        <%#Eval("GysName")%></a><%#EyouSoft.Common.UtilsCommons.GetCompanyRecommend(Eval("IsTuiJian"), Eval("IsQianDan"), Eval("IsFanLi"))%>
                                </td>
                                <td align="center">
                                    <span style="display: none;">
                                        <%#EyouSoft.Common.UtilsCommons.GetGysLxrFuDongHtml(Eval("Lxrs"))%></span>
                                    <a data-contact="contact" href="javascript:void(0)" title="">
                                        <%#Eval("LxrName")%></a>
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
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("JiaoYiXX.JieSuanJinE"),ProviderToMoney )%></a></b>
                                </td>
                                <td align="center">
                                    <b class="fontred"><a href="javascript:void(0)" class="transactions" data-rel='<%#Eval("GysId")%>'
                                        data-unitname='<%#Eval("GysName") %>'>
                                        <%#  EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("JiaoYiXX.WeiZhiFuJinE"),ProviderToMoney)%>
                                    </a></b>
                                </td>
                                <td align="center">
                                    <%#Eval("JiaoYiXX.ZhiJianJunFen","{0:F2}") %>
                                </td>
                                <td align="center">
                                    <%# string.IsNullOrEmpty(Eval("UserInfo.UserId").ToString()) ? ("<b class='fontred'><a class='fenpei_zhh' href='javascript:void(0)' Data-account='' Data-rel='" + Eval("GysId") + "' >分配帐号</a></b>") : ("<a class='fenpei_zhh' href='javascript:void(0)' Data-rel='" + Eval("GysId") + "'>" + Eval("UserInfo.Username") + "</a>")%>
                                </td>
                                <td align="center">
                                    <%#Eval("UserInfo.Status")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="lbMsg" Visible="false" runat="server" Text=""></asp:Label>
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
            tableToolbar.IsHandleElse = true;
            pcToobar.init({
                pID: "#ddlProvice",
                cID: "#ddlCity",
                comID: '<%=this.SiteUserInfo.CompanyId %>',
                gSelect: "1",
                pSelect: '<%=Request.QueryString["ddlProvice"] %>',
                cSelect: '<%=Request.QueryString["ddlCity"] %>'
            })
            //绑定功能按钮
            GroundListPage.BindBtn();
            //当列表页面出现横向滚动条时使用以下方法
            //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
            $('.tablelist-box').moveScroll();
        })
        $(".a_UnitName").click(function() {
            var url = "/ResourceManage/Ground/GroundEdit.aspx?";
            var self = this;
            url += $.param({
                sl: '<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_地接社 %>',
                chakan: "chakan",
                dotype: "update",
                id: $(self).attr("data-id")
            })
            Boxy.iframeDialog({ iframeUrl: url, title: "查看", modal: true, width: "990px", height: "535px" });
            return false;
        })

        $(".transactions").click(function() {
            var url = "/ResourceManage/jiaoyimingxi.aspx?sl=" + '<%=SL %>&';
            var para = { gysid: $(this).attr("Data-rel"), gysname: $(this).attr("data-unitname") };
            url += $.param(para);
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "交易情况",
                modal: true,
                width: "900px",
                height: "525px"
            });
            return false;
        });
        $(".fenpei_zhh").click(function() {
            var url = "/ResourceManage/Ground/Account.aspx?sourceid=" + $(this).attr("Data-rel") + "&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_地接社 %>";
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "分配帐号",
                modal: true,
                width: "400px",
                height: "160px"
            });
            return false;
        })

        var GroundListPage =
        {
            //ajax执行文件路径,默认为本页面
            ajaxurl: "/ResourceManage/Ground/GroundList.aspx",
            //添加
            Add: function() {
                GroundListPage.ShowBoxy({ iframeUrl: "/ResourceManage/Ground/GroundEdit.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_地接社 %>", title: "新增地接社", width: "990px", height: "535px" });
            },
            //修改(弹窗)---objsArr选中的TR对象
            Update: function(ObjsArr) {
                GroundListPage.ShowBoxy({ iframeUrl: "/ResourceManage/Ground/GroundEdit.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_地接社 %>&dotype=update&id=" + ObjsArr[0].find("input[type='checkbox']").val(), title: "修改地接社", width: "990px", height: "535px" });
            },
            //删除(可多行)
            DelAll: function(objArr) {
                var _data = [];
                for (var i = 0; i < objArr.length; i++) {
                    var _$tr = objArr[i];
                    if (_$tr.attr("i_gysid").length > 0) _data.push(_$tr.attr("i_gysid"));
                    else { _$tr.find("input[type='checkbox']").removeAttr("checked"); }
                }
                
                var _url = "/ashx/handler.ashx?dotype=DeleteGys&deletegysids=" + _data.join(',') + "&sl=" + '<%=SL%>';
                GroundListPage.GoAjax(_url);
            },
            //禁用
            Stop: function(objArr) {
                var _data = [];
                for (var i = 0; i < objArr.length; i++) {
                    var _$tr = objArr[i];
                    if (_$tr.attr("i_userid").length > 0) _data.push(_$tr.attr("i_gysid"));
                    else _$tr.find("input[type='checkbox']").removeAttr("checked");
                }

                if (_data.length == 0) { tableToolbar._showMsg("请选择已分配过账号的供应商信息"); return; }

                GroundListPage.ajaxurl += "?dotype=Stop&id=" + _data.join(',') + "&sl=" + '<%=SL%>';
                GroundListPage.GoAjax(GroundListPage.ajaxurl);
            },
            //启用
            Start: function(objArr) {
                var _data = [];
                for (var i = 0; i < objArr.length; i++) {
                    var _$tr = objArr[i];
                    if (_$tr.attr("i_userid").length > 0) _data.push(_$tr.attr("i_gysid"));
                    else _$tr.find("input[type='checkbox']").removeAttr("checked");
                }

                if (_data.length == 0) { tableToolbar._showMsg("请选择已分配过账号的供应商信息"); return; }

                GroundListPage.ajaxurl += "?dotype=Start&id=" + _data.join(',') + "&sl=" + '<%=SL%>';
                GroundListPage.GoAjax(GroundListPage.ajaxurl);
            },
            SetBlack: function(objArr) {
                var _data = [];
                for (var i = 0; i < objArr.length; i++) {
                    var _$tr = objArr[i];
                    if (_$tr.attr("i_userid").length > 0) _data.push(_$tr.attr("i_gysid"));
                    else _$tr.find("input[type='checkbox']").removeAttr("checked");
                }

                if (_data.length == 0) { tableToolbar._showMsg("请选择已分配过账号的供应商信息"); return; }
                GroundListPage.ajaxurl += "?dotype=black&id=" + _data.join(',') + "&sl=" + '<%=SL%>';
                GroundListPage.GoAjax(GroundListPage.ajaxurl);
            },
            //Ajax请求
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
                            tableToolbar._showMsg(ret.msg, function() { location.reload(); });
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            //显示弹窗
            ShowBoxy: function(data) {
                Boxy.iframeDialog({
                    iframeUrl: data.iframeUrl,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            BindBtn: function() {
                //绑定Add事件
                $(".toolbar_add").click(function() {
                    GroundListPage.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "行!", //

                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(obj) {
                        //修改
                        GroundListPage.Update(obj);
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        GroundListPage.DelAll(objsArr);
                    },
                    copyCallBack: function(obj) {
                        //复制
                        GroundListPage.Copy(obj)
                    },
                    otherButtons: [
		{
		    button_selector: '.toolbar_stop',
		    sucessRulr: 2,
		    msg: '未选中任何信息 ',
		    buttonCallBack: function(objArr) {
		        GroundListPage.Stop(objArr);
		    }
		},
		{
		    button_selector: '.toolbar_start',
		    sucessRulr: 2,
		    msg: '未选中任何 信息 ',
		    buttonCallBack: function(objArr) {
		        GroundListPage.Start(objArr);
		    }
		},
		{
		    button_selector: '.toolbar_hmd',
		    sucessRulr: 2,
		    msg: '未选中任何 信息 ',
		    buttonCallBack: function(objArr) {
		        GroundListPage.SetBlack(objArr);
		    }
		}
		]
                })
            }
        }
    </script>

</asp:Content>

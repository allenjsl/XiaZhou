<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="Scenic_spotsList.aspx.cs" Inherits="Web.ResourceManage.scenic_spots.scenic_spotsList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('.contact').bt({
                contentSelector: function(val) {
                    var url = "Contact.ashx?name=" + val;
                    $.ajax({
                        type: "Get",
                        url: url,
                        success: function(msg) {
                            return msg;
                        }
                    });
                },
                positions: ['bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 5,
                spikeGirth: 15,
                width: 405,
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
                    <select name="ddlProvice" value='<%=Request.QueryString["ddlProvice"]%>'>
                        <option value="-1">-请选择省份-</option>
                        <option value="0">云南</option>
                        <option value="1">黄山</option>
                    </select>
                    城市：
                    <select name="ddlCity" value='<%=Request.QueryString["ddlCity"]%>'>
                        <option value="-1">-请选择城市-</option>
                        <option value="0">杭州</option>
                        <option value="1">上海</option>
                    </select>
                    景点名称：
                    <input type="text" size="30" name="txtscenicName" value='<%=Request.QueryString["txtscenicName"]%>'>
                    <input type="submit" value="搜索" class="search-btn" /></p>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a id="link1" hidefocus="true" class="toolbar_add" href="javascript:void(0)">
                    <span>新增</span></a></li><li class="line"></li>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                </s><span>删除</span></a></li><li class="line"></li>
                <li><s class="daoru"></s><a id="link3" hidefocus="true" href="javascript:void(0)"><span>
                    导入</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" href="javascript:void(0)">
                    <span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table cellspacing="0" border="0" width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            所在地
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            景点名称
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            星级
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            散客价
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            团队价
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            联系人
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            电话
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            传真
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            政策
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            交易情况
                        </th>
                    </tr>
                    <tr class="">
                        <td align="center">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </td>
                        <td align="center">
                            湖南安乡
                        </td>
                        <td align="left">
                            <a href="#">湖南未来世界</a>
                        </td>
                        <td align="center">
                            AAAAA
                        </td>
                        <td align="right">
                            <b class="fontgreen">100.00</b>
                        </td>
                        <td align="right">
                            <b class="fontred">90.00</b>
                        </td>
                        <td align="center">
                            <a class="contact" href="javascript:void(0)" title="">胡小明</a>
                        </td>
                        <td align="center">
                            1376265800
                        </td>
                        <td align="center">
                            0571- 7626580
                        </td>
                        <td align="center">
                            加盟返利8%
                        </td>
                        <td align="center">
                            <a id="link2" href="jiaoy-jingdianbox.html">共10次</a>
                        </td>
                    </tr>
                    <tr class="odd">
                        <td align="center">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </td>
                        <td align="center">
                            湖南常德
                        </td>
                        <td align="left">
                            <a href="#">百龙天梯</a>
                        </td>
                        <td align="center">
                            AAAA
                        </td>
                        <td align="right">
                            <b class="fontgreen">100.00</b>
                        </td>
                        <td align="right">
                            <b class="fontred">90.00</b>
                        </td>
                        <td align="center">
                            <a class="contact" href="javascript:void(0)" title="">胡小明</a>
                        </td>
                        <td align="center">
                            1376265800
                        </td>
                        <td align="center">
                            0571- 7626580
                        </td>
                        <td align="center">
                            加盟返利8%
                        </td>
                        <td align="center">
                            <a href="#">共5次</a>
                        </td>
                    </tr>
                    <tr class="">
                        <td align="center">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </td>
                        <td align="center">
                            湖南安乡
                        </td>
                        <td align="left">
                            <a href="#">宝峰湖</a>
                        </td>
                        <td align="center">
                            AAA
                        </td>
                        <td align="right">
                            <b class="fontgreen">100.00</b>
                        </td>
                        <td align="right">
                            <b class="fontred">90.00</b>
                        </td>
                        <td align="center">
                            <a class="contact" href="javascript:void(0)" title="">胡小明</a>
                        </td>
                        <td align="center">
                            1376265800
                        </td>
                        <td align="center">
                            0571- 7626580
                        </td>
                        <td align="center">
                            加盟返利8%
                        </td>
                        <td align="center">
                            <a href="#">共5次</a>
                        </td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    <input type="checkbox" id="checkbox1" name="checkbox">
                                </td>
                                <td align="center">
                                    湖南常德
                                </td>
                                <td align="left">
                                    <a href="#">百龙天梯</a>
                                </td>
                                <td align="center">
                                    AAAA
                                </td>
                                <td align="right">
                                    <b class="fontgreen">100.00</b>
                                </td>
                                <td align="right">
                                    <b class="fontred">90.00</b>
                                </td>
                                <td align="center">
                                    <a class="contact" href="javascript:void(0)" title="">胡小明</a>
                                </td>
                                <td align="center">
                                    1376265800
                                </td>
                                <td align="center">
                                    0571- 7626580
                                </td>
                                <td align="center">
                                    加盟返利8%
                                </td>
                                <td align="center">
                                    <a href="#">共5次</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0pt none;" class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a hidefocus="true" class="toolbar_add" href="javascript:void(0)">
                    <span>新增</span></a></li><li class="line"></li>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                </s><span>删除</span></a></li><li class="line"></li>
                <li><s class="daoru"></s><a hidefocus="true" href="javascript:void(0)"><span>导入</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a class="toolbar_daochu" hidefocus="true" href="javascript:void(0)">
                    <span>导出</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //页面初始化必须存在方法
        $(function() {
            //绑定功能按钮
            BindBtn();
            //当列表页面出现横向滚动条时使用以下方法
            //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
            $('.tablelist-box').moveScroll();
        })

        //绑定功能按钮
        function BindBtn() {
            //绑定Add事件
            $(".toolbar_add").click(function() {
                Add();
            })
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                updateCallBack: function(objsArr) {
                    //修改
                    Update(objsArr);
                },
                deleteCallBack: function(objsArr) {
                    //删除(批量)
                    DelAll(objsArr);
                },
                copyCallBack: function(objsArr) {
                    //复制
                    Copy(objsArr)
                }
            })
        }
        //根据类型返回title改变部分
        function Ping(type) {
            switch (type) {
                case "Add":
                    return "-添加-";
                case "Update":
                    return "-修改-";
                case "Del":
                    return "-删除-"
                default:
                    return "-其他-";
            }
        }
       
    </script>

    <script type="text/javascript">
        //使用弹窗方式添加，修改
        //添加(弹窗)
        function Add() {
            var data = new dataBoxy();
            data.title = "新增景点";
            data.url = "/ResourceManage/scenic spots/Scenic_spotsEdit.aspx";
            data.width = "750px";
            data.height = "489px";
            ShowBoxy(data);
            return false;
        }


        //修改(弹窗)---objsArr选中的TR对象
        function Update(objsArr) {
            var data = new dataBoxy();
            data.title = "修改景点";
            data.url = "/ResourceManage/scenic spots/Scenic_spotsEdit.aspx?doType=update&id=" + objsArr[0].find("input[type='checkbox']").val();
            data.width = "750px";
            data.height = "489px";
            ShowBoxy(data);
            return false;
        }

        //复制(弹窗)---objsArr选中的TR对象
        function Copy(objsArr) {
            alert("执行了复制")
        }

        //删除(批量)
        function DelAll(objArr) {
            //ajax执行文件路径,默认为本页面
            var ajaxUrl = "/ResourceManage/scenic spots/Scenic_spotsList.aspx";
            //定义数组对象
            var list = new Array();
            //遍历按钮返回数组对象
            for (var i = 0; i < objArr.length; i++) {
                //从数组对象中找到数据所在，并保存到数组对象中
                if (objArr[i].find("input[type='checkbox']").val() != "on") {
                    list.push(objArr[i].find("input[type='checkbox']").val());
                }
            }
            //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
            ajaxUrl += "?doType=delete&idList=" + list.join(',');
            //执行ajax
            GoAjax(ajaxUrl, Ping(type))
        }

        //弹窗参数
        //弹窗默认参数
        function dataBoxy() {
            this.url = "";
            this.title = "";
            this.width = "";
            this.height = "";
        };
        //显示弹窗
        function ShowBoxy(data) {
            Boxy.iframeDialog({
                iframeUrl: data.url,
                title: data.title,
                modal: true,
                width: data.width,
                height: data.height
            });
        }
       
    </script>

    <script type="text/javascript">
        //ajax请求
        function GoAjax(url, msg) {
            $.ajax({
                type: "post",
                cache: false,
                url: url,
                dataType: "html",
                success: function(ret) {
                    //ajax回发提示
                    if (ret.toString() == "1") {
                        alert(msg + "成功！")
                        location.reload();
                    }
                    else {
                        alert(msg + ret)
                    }
                },
                error: function() {
                    //ajax异常--你懂得
                    alert("服务器忙！");
                }
            });
        }
    </script>

</asp:Content>

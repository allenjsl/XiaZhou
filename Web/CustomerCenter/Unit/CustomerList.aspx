<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Web.CustomerCenter.Unit.CustomerList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css">
    <link type="text/css" rel="stylesheet" href="../../css/boxynew.css">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <span class="searchT">
                <p>
                    单位名称：
                    <input type="text" size="30"  class="formsize120" name="txtUnitName" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtUnitName") %>' />
                    省份：
                    <select id="ddlProvice" name="ddlProvice">
                    </select>
                    城市：
                    <select id="ddlCity" name="ddlCity">
                    </select>
                    <%--联系人：
                    <input type="text" class="formsize80" name="txtlinkManName" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtlinkManName")%>' />--%>
                    客户等级：
                    <select name="ddlLevId" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("ddlLevId")%>'>
                        <%=BindCrmLevId()%> 
                    </select>
                   <%-- 客户类型：
                    <select name="ddlType">
                        <option value="5">--未选择--</option>
                        <option value="1">组团商</option>
                        <option value="2">组团社</option>
                    </select>--%>
                    责任销售：
                    <input type="text" class="formsize80" name="txtSellerId" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtSellerId")%>' />
                    <input type="submit" value="搜索" class="search-btn" />
                    
                </p>
            </span>
            <input type="hidden" name="sl" value='<%= EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' />
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
                <%if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_新增))
                  {%>
                <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                    <span>新增</span></a></li><li class="line"></li>
                <%}%>
                <%if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改))
                  {%>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <%}%>
                <%if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_删除))
                  {%>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                </s><span>删除</span></a></li><li class="line"></li>
                <%}%>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" rowspan="2">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            所在地
                        </th>
                       
                        <th align="center" class="th-line" rowspan="2">
                            公司名称
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            是否签订合同
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            责任销售
                        </th>
                       
                        <th align="center" class="th-line" colspan="4">
                            交易记录
                        </th>
                       <th align="center" class="th-line" rowspan="2">
                            最后消费时间
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            操作
                        </th>
                    </tr>
                    <tr>
                        <th align="center" class="th-line">
                            订单数
                        </th>
                        <th align="center" class="th-line">
                            人数
                        </th>
                        <th align="right" class="th-line">
                            金额
                        </th>
                        <th align="right" class="th-line">
                            拖欠款
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("CrmId") %>'>
                                </td>
                                <td align="center">
                                    <%# Eval("Address")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Name")%>
                                </td>
                                <td align="center">
                                    <%# Convert.ToBoolean(Eval("IsSignContract"))?"是":"否"%>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                </td>
                                
                                <td align="center">
                                    <%# Eval("TourCount")%>
                                </td>
                                <td align="center">
                                    <%# Eval("PeopleCount")%>
                                </td>
                                <td align="right">
                                    <b class="fontblue">
                                        ￥<%# string.Format("{0:N2}", Eval("Money"))%></b>
                                </td>
                                <td align="right">
                                    <b class="fontbsize12">
                                        ￥<%# string.Format("{0:N2}", Eval("DefaultDeposits"))%></b>
                                </td>
                                <td align="center">
                                    <%# Eval("LastTime") != null ? Convert.ToDateTime(Eval("LastTime")).ToShortDateString() : string.Empty %>
                                </td>
                                <td align="center">
                                    <a href="javascript:SameCustomerList.ShowAccount('<%# Eval("CrmId") %>')">帐号管理</a> <a href="javascript:SameCustomerList.ShowData('<%# Eval("CrmId") %>')" >查看</a>
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
                <%if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_新增))
                  {%>
                <li><s class="addicon"></s><a class="toolbar_add" id="A2" hidefocus="true" href="javascript:void(0)">
                    <span>新增</span></a></li><li class="line"></li>
                <%}%>
                <%if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_修改))
                  {%>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <%}%>
                <%if (this.CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.客户管理_单位直客_删除))
                  {%>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                </s><span>删除</span></a></li><li class="line"></li>
                <%}%>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <div id="scrollBar" style="width: 50px; position: absolute; display: none; font-size: 0px;
            height: 595px;">
            &gt;
        </div>

        

        <script type="text/javascript">
            //页面初始化必须存在方法
            $(function() {
                //绑定功能按钮
                SameCustomerList.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法
                //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();

                pcToobar.init({
                    pID: "#ddlProvice",
                    cID: "#ddlCity",
                    pSelect: '<%=Request.QueryString["ddlProvice"]%>', //省份ID，设置1 为选中状态
                    cSelect: '<%=Request.QueryString["ddlCity"]%>', //城市ID，设置1 为选中状态
                    comID: '<%=base.SiteUserInfo.CompanyId %>'
                })
            })


            var SameCustomerList = {
            //绑定功能按钮
            BindBtn: function() {
                //绑定Add事件
                $(".toolbar_add").click(function() {
                    SameCustomerList.Add();
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        SameCustomerList.Update(objsArr);
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        SameCustomerList.DelAll(objsArr);
                    },
                    copyCallBack: function(objsArr) {
                        //复制
                        SameCustomerList.Copy(objsArr)
                    }
                })
            },
            
              //使用弹窗方式添加，修改
            //添加(弹窗)
           Add: function () {
                 Boxy.iframeDialog({
                            iframeUrl: "CustomerEdit.aspx?sl=<%=Request.QueryString["sl"] %>",
                            title: "新增单位直客",
                            modal: true,
                            width: "920px",
                            height:  "550px"
                        });
            },
             //修改(弹窗)---objsArr选中的TR对象
            Update:function (objsArr) {
                 Boxy.iframeDialog({
                            iframeUrl: "CustomerEdit.aspx?" + "crmId=" + objsArr[0].find("input[type='checkbox']").val()+"&sl=<%=Request.QueryString["sl"] %>",
                            title: "修改单位直客",
                            modal: true,
                            width: "920px",
                            height:  "550px"
                        });
            },
              //复制(弹窗)---objsArr选中的TR对象
            Copy:function (objsArr) {
                alert("执行了复制")
            },
            //删除(批量)
            DelAll:function (objArr) {
                //ajax执行文件路径,默认为本页面
                var ajaxUrl = "/CustomerCenter/Unit/CustomerList.aspx";
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
                ajaxUrl += "?doType=delete&id=" + list.join(',')+ "&sl=" + '<%=Request.QueryString["sl"]%>';
                //执行ajax
                SameCustomerList.GoAjax(ajaxUrl, EnglishToChanges.Ping("Del"))
            },
   
             ShowAccount:function  (crmId) {
                 Boxy.iframeDialog({
                            iframeUrl: "ContractList.aspx?" + "crmId=" +crmId+"&sl=<%=Request.QueryString["sl"] %>",
                            title: "账号管理",
                            modal: true,
                            width: "800px",
                            height:  "300px"
                        });
            },
            ShowData:function (crmId) {
                 Boxy.iframeDialog({
                            iframeUrl: "CustomerSelect.aspx?" + "crmId=" +crmId+"&sl=<%=Request.QueryString["sl"] %>",
                            title: "查看",
                            modal: true,
                            width: "800px",
                            height:  "550px"
                        });
            },
             //ajax请求
            GoAjax:function (url, msg) {
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
            };

            
            
           
       
        </script>

        

    </div>
</asp:Content>

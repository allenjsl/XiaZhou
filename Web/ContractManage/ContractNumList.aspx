<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true" CodeBehind="ContractNumList.aspx.cs" Inherits="Web.ContractManage.ContractNumList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="Seller" TagPrefix="Uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div class="mainbox">
        <form id="formsearch" method="get">
    	<div class="searchbox fixed"><span class="searchT"><p>
    	合同类型：
    	<select name="sel_conType"><%=sel_conType%> </select>
    	号段：<input type="text" size="10" name="txtConStart" value='<%=Request.QueryString["txtConStart"] %>'/>--
    	        <input type="text" size="10" name="txtConEnd" value='<%=Request.QueryString["txtConEnd"] %>'/> 
    	领用人：<Uc1:Seller ID="Seller1" runat="server" />
    	<input type="hidden" name="sl" value='<%= Request.QueryString["sl"] %>' />
		 <button type="submit" value="搜索" class="search-btn">搜索</button></p></span>
        </div>
        </form>
		
      	<div class="tablehead">
         <ul class="fixed">
            <%if (this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_登记))
              { %>
            <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add">
                <span>登记合同号</span></a></li><li class="line"></li>
            <%} if (this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_销号))
              { %>
            <li><s class="updateicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_xh">
                <span>合同销号</span></a></li>
            <%} %>
		 </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
		
        <!--列表表格-->
     <div class="tablelist-box">
       	  <table width="100%" id="liststyle">
			<tr>
              <th>序号</th>
              <th align="center" class="th-line">合同类型</th>
              <th align="center" class="th-line">合同编号</th>
              <th align="center" class="th-line">合同号段</th>
              <th align="center" class="th-line">合同总份数</th>
              <th align="center" class="th-line">已领用份数</th>
              <th align="center" class="th-line">已使用份数</th>
              <th align="center" class="th-line">合同领用</th>
			</tr>
              <asp:Repeater ID="RepList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td align="center"><%# (pageIndex - 1) * pageSize + (Container.ItemIndex + 1)%></td>
                        <td align="center">
                            <%#Eval("ContractType")%>
                        </td>
                        <td align="center">
                            <%#Eval("Code")%>
                        </td>
                        <td align="center">
                            <%#Eval("StartNum")%>-<%#Eval("EndNum")%>
                        </td>
                        <td align="center">
                            <%#Eval("Sum")%>
                        </td>
                        <td align="center">
                            <a href="javascript:void(0)" rel='<%#Eval("ContractId")%>' class="ht_CollarList"><%#Eval("CollarNum")%></a>
                        </td>
                        <td align="center">
                            <a href="javascript:void(0)" rel='<%#Eval("ContractId")%>' class="ht_UsedList"><%#Eval("UsedNum")%></a>
                        </td>
                        <td align="center">
                            <a href="javascript:void(0)" rel='<%#Eval("ContractId")%>&code=<%#Eval("Code")%>' class="ht_Collar">领用</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
   </div>
      <!--列表结束-->
      	<div class="tablehead" style="border:0 none;">
        	<ul class="fixed">
			<%if (this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_登记))
              { %>
            <li><s class="addicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_add">
                <span>登记合同号</span></a></li><li class="line"></li>
            <%} if (this.CheckGrant(Common.Enum.TravelPermission.合同管理_合同管理_销号))
              { %>
            <li><s class="updateicon"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_xh">
                <span>合同销号</span></a></li>
            <%} %>
		 </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
 </div>
 
<div id="scrollBar" style="width: 50px; position: absolute; display: none; font-size: 0px; height: 595px;">&gt;</div>
 
 
 <%--公共方法与必须方法--%>

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
            //绑定Destroy事件
            $(".toolbar_xh").click(function() {
                Destroy();
            })
            //绑定CollarList事件
            $(".ht_CollarList").click(function() {
                CollarList($(this).attr("rel"));
            })
            //绑定UsedList事件
            $(".ht_UsedList").click(function() {
                UsedList($(this).attr("rel"));
            })
            //绑定Collar事件
            $(".ht_Collar").click(function() {
                Collar($(this).attr("rel"));
            })
            tableToolbar.init({
                tableContainerSelector: "#liststyle", //表格选择器
                objectName: "合同", //这个参数讲不明白，请联系柴逸宁，哈哈
                //默认按钮
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
                //cancelCallBack: function() {
                //alert("执行了取消！");
                //}


                //自定义按钮
                //otherButtons: [
                //{
                //button_selector: '', //按钮选择器
                //sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                //msg: '未选中任何 线路 ', //验证未通过提示文本
                //buttonCallBack: function(ojb) {//验证通过执行函数
                //alert("toolbar_paiduan");
                //}
                //}, {
                //同上
                //button_selector: '',
                //sucessRulr: 2,
                //msg: '未选中任何 custom ',
                //buttonCallBack: function(ojb) {
                //alert("toolbar_custom");
                //}
                //}
                //]

            })
        }
        //根据类型返回title改变部分
        function Ping(type) {
            switch (type) {
                case "Add":
                    return "-添加1-";
                case "Update":
                    return "-修改-";
                case "Del":
                    return "-删除-"
                default:
                    return "-其他-";
            }
        }

    </script>

    <%--添加修改等页面跳转方式--%>

    <script type="text/javascript">
        //使用弹窗方式添加，修改
        //添加(弹窗)
        function Add() {
            var data = new dataBoxy();
            data.title = "登记合同号";
            data.url = "ContractNumAdd.aspx?sl=<%=Request.QueryString["sl"]%>";
            data.width = "480px";
            data.height = "250px";
            ShowBoxy(data);
            //alert("执行了添加");
        }

        //销号(弹窗)
        function Destroy() {
            var data = new dataBoxy();
            data.title = "销毁合同号";
            data.url = "ContractNumDestroy.aspx?sl=<%=Request.QueryString["sl"]%>";
            data.width = "300px";
            data.height = "155px";
            ShowBoxy(data);
            //alert("执行了销号"); //
        }
        
        //领用列表(弹窗)
        function CollarList(ContractId) {
            var data = new dataBoxy();
            data.title = "领用合同号列表";
            data.url="ContractNumCollarList.aspx?sl=<%=Request.QueryString["sl"]%>&id="+ContractId;
            data.width = "500px";
            data.height ="300px";
            ShowBoxy(data);
            return false;
        }
        
        //使用列表(弹窗)
        function UsedList(ContractId) {
            var data = new dataBoxy();
            data.title = "使用合同号列表";
            data.url="ContractNumUsedList.aspx?sl=<%=Request.QueryString["sl"]%>&id="+ContractId;
            data.width = "500px";
            data.height ="300px";
            ShowBoxy(data);
            return false;
        }
        
        //领用(弹窗)
        function Collar(ContractId) {
            var data = new dataBoxy();
            data.title = "领用合同号";
            data.url="ContractNumCollar.aspx?sl=<%=Request.QueryString["sl"]%>&id="+ContractId;
            data.width = "500px";
            data.height ="250px";
            ShowBoxy(data);
            return false;
        }

        //修改(弹窗)---objsArr选中的TR对象
        function Update(objsArr) {
            alert("执行了修改");
        }

        //复制(弹窗)---objsArr选中的TR对象
        function Copy(objsArr) {
            alert("执行了复制")
        }


        //AJAX操作规范
        //使用通用按钮下获取数据并执行异步删除
        //删除(批量)
        function DelAll(objArr) {
            //ajax执行文件路径,默认为本页面
            var ajaxUrl = "XXXXX.aspx";
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

    <%--添加修改等页面所需数据获取---参照ajax删除参数获取方法--%>
    <%--AJAX请求--%>

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

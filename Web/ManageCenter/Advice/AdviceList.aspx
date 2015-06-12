<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdviceList.aspx.cs" Inherits="Web.ManageCenter.Advice.AdviceList"
    MasterPageFile="~/MasterPage/Front.Master" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    标题：<input type="text" class="formsize120" size="30" id="txtTitle" name="txtTitle"
                        value='<%=Request.QueryString["txtTitle"]%>' />
                    提交人：<%--<input type="text" class="formsize80" size="35" id="txtPeople" name="txtPeople"
                        value='<%=Request.QueryString["txtPeople"]%>' />--%>
                        <uc1:SellsSelect runat="server" ID="SellsSelect1"/>
                    提交时间：<input type="text" class="formsize140" size="35" id="txtTime" name="txtTime"
                        onfocus="WdatePicker()" value='<%=Request.QueryString["txtTime"]%>' />
                    接收人：<%--<input type="text" class="formsize80" size="35" id="txtGetor" name="txtGetor"
                        value='<%=Request.QueryString["txtGetor"]%>' />--%>
                        <uc1:SellsSelect runat="server" ID="SellsSelect2"/>
                    处理时间：<input type="text" class="formsize140" size="35" id="txtHandleTime" name="txtHandleTime"
                        onfocus="WdatePicker()" value='<%=Request.QueryString["txtHandleTime"]%>' />
                    状态：<select name="selState" id="selState">
                            <option value="-1" selected="selected">请选择</option>
                            <option value="1">未处理</option>
                            <option value="2">已处理</option>    
                    </select>
                    <input type="hidden" name="sl" value='<%=Request.QueryString["sl"]%>' />
                    <button type="submit" class="search-btn">
                        搜索</button></p>
                <script type="text/javascript">
                    function setValue(obj, v) {
                        for (var i = 0; i < obj.options.length; i++) {
                            if (obj.options[i].value == v) {
                                obj.options[i].selected = true;
                            }
                        }
                    }
                    setValue($("#selState")[0], '<%=Request.QueryString["selState"] %>');
                </script>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed">
           
                <li><s class="addicon"></s><a href="AdviceAdd.aspx?sl=<%=Request.QueryString["sl"]%>" hidefocus="true" class="toolbar_add add_yj">
                    <span>新增</span></a></li>
                    
                <li class="line"></li>
               
                <li><s class="updateicon"></s><a href="AdviceAdd.aspx?sl=<%=Request.QueryString["sl"]%>" hidefocus="true" class="toolbar_update">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><a href="#" hidefocus="true" class="toolbar_delete"><s class="delicon"></s><span>
                    删除</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th width="429" align="center" class="th-line">
                        标题
                    </th>
                    <th width="185" align="center" class="th-line">
                        提交人
                    </th>
                    <th width="206" align="center" class="th-line">
                        提交时间
                    </th>
                    <th width="154" align="center" class="th-line">
                        接收人
                    </th>
                    <th width="176" align="center" class="th-line">
                        处理时间
                    </th>
                    <th width="197" align="center" class="th-line">
                        状态
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("OpinionId")%>"/>
                            </td>
                            <td align="center">
                                <%#Eval("Title")%>
                            </td>
                            <td align="center">
                                <%#String.IsNullOrEmpty(Eval("Submit").ToString().Trim()) ? "匿名" : Eval("Submit")%>
                            </td>
                            <td align="center">
                                <%#Eval("SubmitTime", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="center">
                                <%#this.getMoreInfo(Eval("MGovOpinionUserList"))%>
                            </td>
                            <td align="center">
                                <%#Eval("ProcessTime", "{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                            <td align="center">
                                <%#Eval("Status").ToString() == "1" ? "未处理" : "<span class=\"fontgray\">已处理</span>"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border: 0 none;">
            <ul class="fixed">
           
                <li><s class="addicon"></s><a href="AdviceAdd.aspx?sl=<%=Request.QueryString["sl"]%>" hidefocus="true" class="toolbar_add add_yj">
                    <span>新增</span></a></li>
                    
                <li class="line"></li>
                
                <li><s class="updateicon"></s><a href="AdviceAdd.aspx?sl=<%=Request.QueryString["sl"]%>" hidefocus="true" class="toolbar_update">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><a href="#" hidefocus="true" class="toolbar_delete"><s class="delicon"></s><span>
                    删除</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            $(".add_yj").click(function() {
                var url = $(this).attr("href");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "添加",
                    modal: true,
                    width: "630px",
                    height: "402px"
                });
                return false;
            });

        })
        var fun1 = function(objArr) {
            var list = new Array();
            for (var i = 0; i < objArr.length; i++) {
                if (objArr[i].find("input[type='checkbox']").val() != "on") {
                    list.push(objArr[i].find("input[type='checkbox']").val());
                }
            }        
            Boxy.iframeDialog({
            iframeUrl: "AdviceAdd.aspx?doType=update&id=" + list.join(',') + '&sl=<%=Request.QueryString["sl"]%>',
                title: "修改",
                modal: true,
                width: "600px",
                height: "390px"
            });
        };
        $(function() {
            tableToolbar.init({
                objectName: "意见",
                otherButtons: [{
                    button_selector: '.toolbar_paiduan',
                    sucessRulr: 2,
                    msg: '未选中任何 意见 ',
                    buttonCallBack: function(obj) {
                        Boxy.iframeDialog({
                            iframeUrl: "AdviceAdd.aspx",
                            title: "添加",
                            modal: true,
                            width: "630px",
                            height: "402px"
                        });
                        return false;
                    }
}],
                    updateCallBack: function(obj) {
                        fun1(obj);
                    },
                    deleteCallBack: function(obj) {
                        DelAll(obj);
                    }
                });
            });
        //删除(批量)
        function DelAll(objArr) {
            var ajaxUrl = "AdviceList.aspx";
            var list = new Array();
            for (var i = 0; i < objArr.length; i++) {
                if (objArr[i].find("input[type='checkbox']").val() != "on") {
                    list.push(objArr[i].find("input[type='checkbox']").val());
                }
            }
            //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
            ajaxUrl += "?doType=delete&id=" + list.join(',') + '&sl=<%=Request.QueryString["sl"]%>';
            GoAjax(ajaxUrl, EnglishToChanges.Ping("Del"));
        }
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
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
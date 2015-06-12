<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HrList.aspx.cs" Inherits="Web.ManageCenter.Hr.HrList"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Src="~/UserControl/SelectDuty.ascx" TagName="SelectDuty" TagPrefix="uc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <span class="searchT">
                <p>
                    档案编号：<input type="text" id="txtNum" name="txtNum" class="inputtext formsize80" size="35"
                        value='<%=Request.QueryString["txtNum"]%>' />
                    姓名：<input type="text" class="inputtext formsize80" size="35" id="txtName" name="txtName"
                        value='<%=Request.QueryString["txtName"]%>' />
                    工龄：<input type="text" class="inputtext formsize40" size="35" id="txtWorkYear" name="txtWorkYear"
                        value='<%=Request.QueryString["txtWorkYear"]%>' />
                    职务：<uc2:SelectDuty runat="server" ID="SelectDuty1" SetTitle="员工职务" SModel="1" />
                    出生日期：
                    <input type="text" class="inputtext formsize80" size="35" id="txtSBirth" name="txtSBirth"
                        onfocus="WdatePicker({maxDate:'#F{$dp.$D(\'txtEBirth\')||\'%y-%M-%d\'}',dateFmt:'yyyy-MM-dd',opposite:true})")"
                        value='<%=Request.QueryString["txtSBirth"]%>' />-<input type="text" class="inputtext formsize80"
                            size="35" id="txtEBirth" name="txtEBirth" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtSBirth\')}',maxDate:'%y-%M-%d',dateFmt:'yyyy-MM-dd',opposite:true})"
                            value='<%=Request.QueryString["txtEBirth"]%>' /><br />
                    性 &nbsp; &nbsp;别：
                    <select name="selSex" id="selSex" class="inputselect">
                        <%=this.getSelOptions(1)%>
                    </select>
                    类型：<select name="selType" id="selType" class="inputselect">
                        <%=this.getSelOptions(2)%>
                    </select>
                    员工状态：<select name="selState" id="selState" class="inputselect">
                        <%=this.getSelOptions(3)%>
                    </select>
                    婚姻状况：<select name="selWedState" id="selWedState" class="inputselect">
                        <option value="-1">请选择</option>
                        <option value="0">未婚</option>
                        <option value="1">已婚</option>
                    </select>
                    部门：<uc1:SelectSection ID="txtDept" runat="server" ReadOnly="true" SetTitle="部门" SModel="1" />
                    <button type="submit" id="btnSubmit" class="search-btn">
                        搜索</button></p>

                <script type="text/javascript">
                    function setValue(obj, v) {
                        for (var i = 0; i < obj.options.length; i++) {
                            if (obj.options[i].value == v) {
                                obj.options[i].selected = true;
                            }
                        }
                    }
                    setValue($("#selSex")[0], '<%=Request.QueryString["selSex"] %>');
                    setValue($("#selType")[0], '<%=Request.QueryString["selType"] %>');
                    setValue($("#selState")[0], '<%=Request.QueryString["selState"] %>');
                    setValue($("#selWedState")[0], '<%=Request.QueryString["selWedState"] %>');
                </script>

            </span>
            <input type="hidden" name="sl" value='<%=SL%>' />
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="ph_Add">
                    <li><a href="javascript:void()" hidefocus="true" class="toolbar_add add_gg"><s class="addicon">
                    </s><span>添加</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ph_Update" runat="server">
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_update"><s class="updateicon">
                    </s><span>修改</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ph_Del" runat="server">
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_delete"><s class="delicon">
                    </s><span>删除</span></a> </li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <li><a href="javascript:window.print()" hidefocus="true" class="toolbar_dayin"><s
                    class="dayin"></s><span>打印</span></a></li>
                <li class="line"></li>
                <li><s class="daochu"></s><a href="javascript:void(0)" hidefocus="true" class="toolbar_daochu"
                    onclick="toXls1(); return false;"><span>导出</span></a></li>
                <li class="line"></li>
                <li><a href="javascript:void(0)" hidefocus="true" class="i_gonglingtongbu"><s class="updateicon">
                </s><span>工龄同步</span></a> </li>
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
                    <th align="center" class="th-line">
                        档案编号
                    </th>
                    <th align="center" class="th-line">
                        姓名
                    </th>
                    <th align="center" class="th-line">
                        性别
                    </th>
                    <th align="center" class="th-line">
                        出生日期
                    </th>
                    <th align="center" class="th-line">
                        所属部门
                    </th>
                    <th align="center" class="th-line">
                        职务
                    </th>
                    <th align="center" class="th-line">
                        工龄
                    </th>
                    <th align="center" class="th-line">
                        联系电话
                    </th>
                    <th align="center" class="th-line">
                        手机
                    </th>
                    <th align="center" class="th-line">
                        QQ
                    </th>
                    <th align="center" class="th-line">
                        学历
                    </th>
                    <th align="center" class="th-line">
                        合同是否签订
                    </th>
                    <th align="center" class="th-line">
                        合同到期时间
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("ID")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("FileNumber")%>
                            </td>
                            <td align="center">
                                <%#Eval("Name")%>
                            </td>
                            <td align="center">
                                <%#Eval("Sex")%>
                            </td>
                            <td align="center">
                                <%#Eval("BirthDate", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <%#Eval("DepartName")%>
                            </td>
                            <td align="center">
                                <%#this.GetMoreInfo(Eval("GovFilePositionList"),"position")%>
                            </td>
                            <td align="center">
                                <%#Eval("LengthService")%>
                            </td>
                            <td align="center">
                                <%#Eval("Contact")%>
                            </td>
                            <td align="center">
                                <%#Eval("Mobile")%>
                            </td>
                            <td align="center">
                                <%#Eval("qq")%>
                            </td>
                            <td align="center">
                                <%#Eval("Education")%>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%# ((bool)Eval("IsSignContract")) ? "是" : "<p style ='color:Red'>否</p>"%>
                            </td>
                            <td align="center">
                                <%#Eval("MaturityTime", "{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <a href="HrShow.aspx?id=<%#Eval("ID")%>&sl=<%=SL%>" target="_blank">
                                    查看</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("select_Toolbar_Paging_1").innerHTML);
			</script>

        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: '<%=SL %>'
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/ManageCenter/Hr',
                    title: "",
                    width: "1200px",
                    height: "900px"
                }
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
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(result) {
                        if (result.result == "1") {
                            tableToolbar._showMsg(result.msg, function() {
                                window.location.href = window.location.href;
                            });
                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            Add: function() {
                var data = this.DataBoxy();
                data.url += '/HrAdd.aspx?';
                data.title = '添加档案';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                location.href = data.url;
            },
            Update: function(objsArr) {
                var data = this.DataBoxy();
                data.url += '/HrAdd.aspx?';
                data.title = '档案修改';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val(),
                    refererUrl: decodeURIComponent(window.location.href)
                });
                location.href = data.url;
            },
            Delete: function(objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }

                if (list.length > 1) {
                    tableToolbar._showMsg("一次只能删除一个人事档案数据");
                    return;
                }

                var data = this.DataBoxy();
                data.url += "/HrList.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsDataObj.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "档案信息",
                    updateCallBack: function(objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        PageJsDataObj.Delete(objsArr);
                    }
                });
            },
            PageInit: function() {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            },
            //工龄同步
            gongLingTongBu: function() {
                if (!confirm("工龄=当前年份-入职年份，同步操作不可撤销，你确定要同步吗？")) return;
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=gonglingtongbu",
                    data: $("form").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        tableToolbar._showMsg(response.msg, function() {
                            window.location.href = window.location.href;
                        });
                    },
                    error: function() {
                        window.location.href = window.location.href;
                    }
                });

            }
        }
        $(function() {
            tableToolbar.IsHandleElse = true;
            PageJsDataObj.BindBtn();

            $(".i_gonglingtongbu").click(function() { PageJsDataObj.gongLingTongBu(); });
        });
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>

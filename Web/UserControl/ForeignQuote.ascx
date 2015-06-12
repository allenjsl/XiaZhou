<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignQuote.ascx.cs"
    Inherits="Web.UserControl.ForeignQuote" %>

<script type="text/javascript">
    /*
    @分项整团分类显示
    1 整团
    2 分项
    */
    var ForeignQuote = {
        PriceItemType: function(ItemType) {
            switch (ItemType) {
                case "1":
                    $("#divGroup").fadeIn("slow");
                    $("#divItems").hide();
                    break;
                case "2":
                    $("#divGroup").hide();
                    $("#divItems").fadeIn("slow");
                    break;
            }
        },
        PriceTemp:
        [
          "<option value='0'>元/人</option>",
          "<option value='1'>元/团</option>",
          "<option value='0'>元/人</option><option value='1'>元/团</option>"
        ],
        OpenWin: function(obj) {

            $(obj).attr("id", "a_ForeignQuote_" + parseInt(Math.random() * 10000));

            Boxy.iframeDialog({
                iframeUrl: $(obj).attr("href") + "&id=" + $(obj).attr("id") + '&sl=<%=Request.QueryString["sl"] %>',
                title: $(obj).attr("title"),
                modal: true,
                width: "700px",
                height: "400px"
            });
        }

    }

    $(function() {
        //newToobar.init({ box: "#divGroup", className: "xuanyong", width: "525", height: "300" });

        $("#divItems table").autoAdd();

        $("#divItems").find("select[data-class='slt_ForeignQuote_PlanProject']").change(function() {
            var data = $(this).val().split('|');
            $(this).closest("tr").find("select[data-class='slt_ForeignQuote_Price']").html(ForeignQuote.PriceTemp[data[1]]);
            $(this).closest("tr").find("a[data-class='xuanyong']").attr("title", "项目选用-" + $(this).find("option:selected").text());
            $(this).closest("tr").find("a[data-class='xuanyong']").attr("href", "/TeamCenter/selectXiangmuxybox.aspx?proType=" + data[0]);
        })

        $("#divItems").find("select[data-class='slt_ForeignQuote_PlanProject']").each(function() {
            var _s = $(this);
            if (_s.attr("data-id")) {
                _s.find("option").each(function() {
                    if (this.value.split('|')[0] == _s.attr("data-id")) {
                        $(this).attr("selected", "selected");
                    }
                })
            }
        })


        if ('<%=IsTourOrSubentry.ToString().ToLower() %>' == "true") {
            ForeignQuote.PriceItemType('1');
            $("#divForeignQuote").find("input[data-type='1']").attr("checked", "checked");
        } else {
            ForeignQuote.PriceItemType('2');
            $("#divForeignQuote").find("input[data-type='2']").attr("checked", "checked");
        }

        $("#divItems").find("select[data-class='slt_ForeignQuote_PlanProject']").change();

        $("#divItems").find("select[data-class='slt_ForeignQuote_Price']").each(function() {
            var self = $(this);
            try {
                self.val(self.attr("data-old"));

                setTimeout(function() {
                    if (self.val() == "") {
                        self.html(ForeignQuote.PriceTemp[0]);
                    }
                }, 200)
            } catch (e) {

            }
        })
    });
    
</script>

<div class="addxc-box" id="divForeignQuote">
    <span class="formtableT formtableT02"><s></s>对外报价</span><span style="padding-left: 15px;
        font-weight: bold;">
        <label>
            <input type="radio" data-type="1" value="1" onclick="ForeignQuote.PriceItemType('1')"
                name="rdo_ForeignQuote_SeviceType" />
            整团</label>
        &nbsp; &nbsp;
        <label>
            <input type="radio" data-type="2" value="2" onclick="ForeignQuote.PriceItemType('2')"
                name="rdo_ForeignQuote_SeviceType" />
            分项</label></span>
    <div style="display: none;" id="divGroup">
        <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
            <tbody>
                <tr>
                    <th>
                        服务标准 <a class="xuanyong" title="服务标准" onclick="ForeignQuote.OpenWin(this);return false;"
                            href="/TeamCenter/selectXiangmuxybox.aspx?type=<%=(int)EyouSoft.Model.EnumType.ComStructure.ProjectType.服务标准 %>">
                        </a>
                    </th>
                </tr>
                <tr>
                    <td align="center">
                        <textarea class="inputtext" style="width: 95%; height: 100px;" rows="" cols="" name="txt_ForeignQuote_ZtService"><%=GroupService%></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="divItems" style="display: block;">
        <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
            <tbody>
                <tr>
                    <th style=" width:20%">
                        项目
                    </th>
                    <th style=" width:50%">
                        服务标准
                    </th>
                    <th style=" width:15%">
                        单项报价
                    </th>
                    <th style=" width:15%">
                        操作
                    </th>
                </tr>
                <asp:PlaceHolder ID="phrPanel" runat="server">
                    <tr class="tempRow">
                        <td bgcolor="#BCE0F1" align="center">
                            <select class="inputselect" data-id="0" name="slt_ForeignQuote_PlanProject" data-class='slt_ForeignQuote_PlanProject'>
                                <%=GetOptionByPlanProject("0")%>
                            </select>
                            <a class="xuanyong" onclick="ForeignQuote.OpenWin(this);return false;" data-class="xuanyong"
                                href="/TeamCenter/selectXiangmuxybox.aspx">&nbsp;</a>
                        </td>
                        <td align="center">
                            <textarea class="inputtext" cols="" rows="2" style="width: 95%; height: 60px;"
                                name="txt_ForeignQuote_Service"></textarea>
                        </td>
                        <td align="center">
                            <input type="text" class="inputtext formsize40" valid="isMoney" errmsg="请填写正确的价格格式"
                                name="txt_ForeignQuote_Price" />
                            <select class="inputselect" name="slt_ForeignQuote_Price" data-class="slt_ForeignQuote_Price"
                                data-old='0'>
                            </select>
                        </td>
                        <td align="center">
                            <a class="addbtn" href="javascript:void(0)">
                                <img width="48" border="0" height="20" src="/images/addimg.gif"></a> <a class="delbtn"
                                    href="javascript:void(0)">
                                    <img width="48" border="0" height="20" src="/images/delimg.gif"></a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:Repeater ID="rptListTour" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td bgcolor="#BCE0F1" align="center">
                                <select class="inputselect" data-id="<%#(int)Eval("ServiceType") %>" name="slt_ForeignQuote_PlanProject"
                                    data-class='slt_ForeignQuote_PlanProject'>
                                    <%#GetOptionByPlanProject(((int)Eval("ServiceType")).ToString())%>
                                </select>
                                <a class="xuanyong" data-class="xuanyong" onclick="ForeignQuote.OpenWin(this);return false;"
                                    href="/TeamCenter/selectXiangmuxybox.aspx">&nbsp;</a>
                            </td>
                            <td align="center">
                                <textarea class="inputtext" cols="" rows="2" style="width: 95%; height: 60px;"
                                    name="txt_ForeignQuote_Service"><%#Eval("ServiceStandard")%></textarea>
                            </td>
                            <td align="center">
                                <input type="text" class="inputtext formsize40" valid="isMoney" errmsg="请填写正确的价格格式"
                                    name="txt_ForeignQuote_Price" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroString( Eval("Quote").ToString()) %>" />
                                <select class="inputselect" name="slt_ForeignQuote_Price" data-class="slt_ForeignQuote_Price"
                                    data-old='<%#(int)Eval("Unit") %>'>
                                </select>
                            </td>
                            <td align="center">
                                <a class="addbtn" href="javascript:void(0)">
                                    <img width="48" border="0" height="20" src="/images/addimg.gif"></a> <a class="delbtn"
                                        href="javascript:void(0)">
                                        <img width="48" border="0" height="20" src="/images/delimg.gif"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptListRoute" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td bgcolor="#BCE0F1" align="center">
                                <select class="inputselect" data-id="<%#(int)Eval("Type") %>" name="slt_ForeignQuote_PlanProject"
                                    data-class='slt_ForeignQuote_PlanProject'>
                                    <%#GetOptionByPlanProject(((int)Eval("Type")).ToString())%>
                                </select>
                                <a class="xuanyong" data-class="xuanyong" onclick="ForeignQuote.OpenWin(this);return false;"
                                    href="/TeamCenter/selectXiangmuxybox.aspx">&nbsp;</a>
                            </td>
                            <td align="center">
                                <textarea class="inputtext" cols="" rows="2" style="width: 95%; height: 60px;"
                                    name="txt_ForeignQuote_Service"> <%#Eval("Standard")%></textarea>
                            </td>
                            <td align="center">
                                <input type="text" class="inputtext formsize40" valid="isMoney" errmsg="请填写正确的价格格式"
                                    name="txt_ForeignQuote_Price" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroString( Eval("UnitPrice").ToString()) %>" />
                                <select class="inputselect" name="slt_ForeignQuote_Price" data-class="slt_ForeignQuote_Price"
                                    data-old='<%#(int)Eval("Unit") %>'>
                                </select>
                            </td>
                            <td align="center">
                                <a class="addbtn" href="javascript:void(0)">
                                    <img width="48" border="0" height="20" src="/images/addimg.gif"></a> <a class="delbtn"
                                        href="javascript:void(0)">
                                        <img width="48" border="0" height="20" src="/images/delimg.gif"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <input type="hidden" value="<%=IsFromTourOrRoute.ToString() %>" name="hide_IsFromTourOrRoute" />
</div>

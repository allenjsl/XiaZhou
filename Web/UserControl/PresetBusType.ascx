<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PresetBusType.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.PresetBusType" %>
<span class="formtableT-car"><s></s>&nbsp;预设车型</span>
<table width="100%" cellpadding="0" cellspacing="0" class="tab_autoAdd">
    <tr>
        <th valign="middle" class="th-line" width="37%">
            车型
        </th>
        <th valign="middle" class="th-line" width="19%">
            座位总数
        </th>
        <th valign="middle" class="th-line" width="19%">
            座位查看
        </th>
        <asp:PlaceHolder runat="server" ID="ph_Contrltr">
            <th width="25%" valign="middle" class="th-line">
                操作
            </th>
        </asp:PlaceHolder>
    </tr>
    <asp:PlaceHolder runat="server" ID="ph_AddPreSetBusType">
        <tr class="tr_tempRow" style="height: 30px" data-type="">
            <td align="center">
                <select name="selPre" id="selPreSetbus" class="inputselect">
                    <%=GetPreSetBus()%>
                </select>
                <input type="hidden" data-class="tourCarTypeId" name="<%=this.hideTourCarTypeIdClientID %>"
                    id="<%=this.hideTourCarTypeIdClientID %>" value="" />
                <input type="hidden" data-class="carTypeId" name="<%=this.hidCarTypeIdClientID %>"
                    id="<%=this.hidCarTypeIdClientID %>" value="" />
                <input type="hidden" data-class="carTypeName" name="<%=this.hidCarTypeNameClientID %>"
                    id="<%=this.hidCarTypeNameClientID %>" value="" />
                <input type="hidden" data-class="desc" name="<%=this.hidDescClientID %>" id="<%=this.hidDescClientID %>"
                    value="" />
                <input type="hidden" data-class="seatNum" name="<%=this.hidSeatNumClientID %>" id="<%=this.hidSeatNumClientID %>"
                    value="" />
                <input type="hidden" data-class="oldcarTypeName" value="" />
            </td>
            <td align="center">
                -
            </td>
            <td align="center">
                <a class="seatcheck" href="javascript:void(0)">座位查看</a>
            </td>
            <td align="center">
                <a href="javascript:void(0)" class="a_addbtn">
                    <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                        class="a_delbtn">
                        <img src="/images/delimg.gif" width="48" height="20" /></a>
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="ph_UpdateFalsePresetBusType">
        <asp:Repeater runat="server" ID="prt_PreSetTypeList1">
            <ItemTemplate>
                <tr class="tr_tempRow" style="height: 30px" data-type="">
                    <td align="center">
                        <%#Eval("CarTypeName")%>
                        <input type="hidden" data-class="tourCarTypeId" name="<%=this.hideTourCarTypeIdClientID %>"
                            id="<%=this.hideTourCarTypeIdClientID %>" value="<%#Eval("TourCarTypeId") %>" />
                        <input type="hidden" data-class="carTypeId" name="<%=this.hidCarTypeIdClientID %>"
                            id="<%=this.hidCarTypeIdClientID %>" value="<%#Eval("CarTypeId") %>" />
                        <input type="hidden" data-class="carTypeName" name="<%=this.hidCarTypeNameClientID %>"
                            id="<%=this.hidCarTypeNameClientID %>" value="<%#Eval("CarTypeName") %>" />
                        <input type="hidden" data-class="desc" name="<%=this.hidDescClientID %>" id="<%=this.hidDescClientID %>"
                            value="<%#Eval("Desc") %>" />
                        <input type="hidden" data-class="seatNum" name="<%=this.hidSeatNumClientID %>" id="<%=this.hidSeatNumClientID %>"
                            value="<%#Eval("SeatNum") %>" />
                        <input type="hidden" data-class="oldcarTypeName" value="<%#Eval("CarTypeName") %>" />
                    </td>
                    <td align="center">
                        <%#Eval("SeatNum") %>
                    </td>
                    <td align="center">
                        <a class="seatcheck" href="javascript:void(0)">座位查看</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="ph_UpdateTruePresetBusType">
        <asp:Repeater runat="server" ID="prt_PreSetTypeList2">
            <ItemTemplate>
                <tr class="tr_tempRow" style="height: 30px" data-type="">
                    <td align="center">
                        <select name="selPre" id="selPreSetbus" class="inputselect">
                            <%#GetPreSetBus(Eval("CarTypeId").ToString())%>
                        </select>
                        <input type="hidden" data-class="tourCarTypeId" name="<%=this.hideTourCarTypeIdClientID %>"
                            id="<%=this.hideTourCarTypeIdClientID %>" value="<%#Eval("TourCarTypeId") %>" />
                        <input type="hidden" data-class="carTypeId" name="<%=this.hidCarTypeIdClientID %>"
                            id="<%=this.hidCarTypeIdClientID %>" value="<%#Eval("CarTypeId") %>" />
                        <input type="hidden" data-class="carTypeName" name="<%=this.hidCarTypeNameClientID %>"
                            id="<%=this.hidCarTypeNameClientID %>" value="<%#Eval("CarTypeName") %>" />
                        <input type="hidden" data-class="desc" name="<%=this.hidDescClientID %>" id="<%=this.hidDescClientID %>"
                            value="<%#Eval("Desc") %>" />
                        <input type="hidden" data-class="seatNum" name="<%=this.hidSeatNumClientID %>" id="<%=this.hidSeatNumClientID %>"
                            value="<%#Eval("SeatNum") %>" />
                        <input type="hidden" data-class="oldcarTypeName" value="<%#Eval("CarTypeName") %>" />
                    </td>
                    <td align="center">
                        <%#Eval("SeatNum") %>
                    </td>
                    <td align="center">
                        <a class="seatcheck" href="javascript:void(0)">座位查看</a>
                    </td>
                    <td align="center">
                        <a href="javascript:void(0)" class="a_addbtn">
                            <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                class="a_delbtn">
                                <img src="/images/delimg.gif" width="48" height="20" /></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phdMsg" runat="server">
        <tr>
            <td colspan="4" align="center">
                未设置车型
            </td>
        </tr>
    </asp:PlaceHolder>
</table>

<script type="text/javascript">
    var PresetPageObj = {
        tourId: '<%=this.TourId %>',
        addRow: function(obj) {
            var self = $(obj);
            var tempRow = self.closest("tr").clone(true);
            if (self.closest("tr").find("select").val() != "") {
                tempRow.find("input[type='hidden'][data-class='tourCarTypeId']").val("");
                tempRow.find("select option").each(function() {
                    var value = $(this).val();
                    if (value == self.closest("tr").find("select").val()) {
                        $(this).attr("selected", "selected");
                    }
                })
            }
            else {
                tempRow.find("select").next("td").text("-");
            }
            tempRow.closest("tr").attr("data-type", "newrow").insertAfter(self.closest("tr"));
        },
        delRow: function(obj) {
            var self = $(obj);
            if ($(".tr_tempRow").length > 1) {
                self.closest("tr").remove();
            } else { parent.tableToolbar._showMsg("需至少保留 1 行记录!") }
        },
        seatCheck1: function(obj) {
            var win = top || window, self = $(obj).closest("tr"), url = "/CommonPage/SetSeat.aspx?",
            tourCarTypeId = self.first("td").find("input[type='hidden'][data-class='tourCarTypeId']").val(),
            dataType = self.attr("data-type");
            if (dataType == "newrow") { PresetPageObj.seatCheck2(obj); return false; }
            url += $.param({
                tourId: PresetPageObj.tourId,
                tourCarTypeId: tourCarTypeId
            });
            win.Boxy.iframeDialog({
                iframeUrl: url,
                title: "座位查看",
                modal: true,
                width: "930px",
                height: "400px"
            })
        },
        seatCheck2: function(obj) {
            var win = top || window, url = "/SystemSet/CarSeatSet.aspx?", closesttr = $(obj).closest("tr"),
            tempid = $.trim(closesttr.find("select option:selected").attr("data-temid")),
            seatnum = $.trim(closesttr.find("input[type='hidden'][data-class='seatNum']").val());
            if (tempid == "") { parent.tableToolbar._showMsg("请先选择预设车型"); return false; }
            url += $.param({
                type: 'sel',
                templateid: tempid,
                seatnum: seatnum
            })
            win.Boxy.iframeDialog({
                iframeUrl: url,
                title: "座位查看",
                modal: true,
                width: "930px",
                height: "400px"
            })
        }
    }
    $(function() {
        $(".tab_autoAdd tr td select[name='selPre']").change(function() {
            var self = $(this), closesttd = self.closest("td");
            var num = self.find("option:selected").attr("data-num"),
            typeName = self.find("option:selected").attr("data-carTypeName"),
            desc = self.find("option:selected").attr("data-desc"),
            typeId = self.find("option:selected").val();
            closesttd.next().text(num);
            closesttd.find("input[type='hidden'][data-class='seatNum']").val(num);
            closesttd.find("input[type='hidden'][data-class='carTypeName']").val(typeName);
            closesttd.find("input[type='hidden'][data-class='desc']").val(desc);
            closesttd.find("input[type='hidden'][data-class='carTypeId']").val(typeId);
            self.closest("tr").attr("data-type", "newrow");
        })
        //添加删除行（根据预设车型的调用情况来判断是否可以删除）
        $(".tab_autoAdd tr[class='tr_tempRow']").find("a[class='a_addbtn']").click(function() {
            PresetPageObj.addRow(this);
        })
        $(".tab_autoAdd tr[class='tr_tempRow']").find("a[class='a_delbtn']").click(function() {
            PresetPageObj.delRow(this);
        })
        $(".tab_autoAdd tr[class='tr_tempRow']").find("a[class='seatcheck']").click(function() {
            if ('<%=this.ContrloModel %>' != 'add' && '<%=this.ContrloModel %>' != 'copy') {
                PresetPageObj.seatCheck1(this);
            } else {
                PresetPageObj.seatCheck2(this);
            }

        })
    })
</script>


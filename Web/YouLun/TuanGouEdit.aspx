<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TuanGouEdit.aspx.cs" Inherits="EyouSoft.Web.YouLun.TuanGouEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" ValidateRequest="false" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        航期产品
                    </td>
                    <td>
                        <input type="hidden" runat="server" id="txtHangQiId" valid="required" errmsg="请选择航期" />
                        <input type="text" runat="server" id="txtHangQi" class="inputtext" style="width: 380px;
                            background: #bababa" readonly="readonly" valid="required" errmsg="请选择航期" />
                        <a href="javascript:void(0);" class="xuanyong Offers" id="i_a_xuanyong">
                        </a>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        出港日期
                    </td>
                    <td>
                        <select name="txtRiQi" id="txtRiQi" class="inputselect" valid="required" errmsg="请选择出港日期">
                            <option value="">请选择出港日期</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT" >
                        房型
                    </td>
                    <td>
                        <select name="txtFangXing" id="txtFangXing" class="inputselect" valid="required"
                            errmsg="请选择房型">
                            <option value="">请选择房型</option>
                            <asp:Literal runat="server" ID="ltrFangXing"></asp:Literal>
                        </select>
                    </td>
                </tr>
                <tr data-id="guoji" style="display:none;">
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        国籍
                    </td>
                    <td>
                        <select name="txtGuoJi" id="txtGuoJi" class="inputselect" valid="required" errmsg="请选择国籍">
                            <option value="">请选择国籍</option>
                            <asp:Literal runat="server" ID="ltrGuoJi"></asp:Literal>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        宾客类型
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtBKLX" class="inputtext" style="width: 380px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        团购名称
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtMingCheng" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请输入团购名称" />
                    </td>
                </tr>                
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        原价
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtYuanJia" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请输入原价" />
                    </td>
                </tr>                
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        现价
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtXianJia" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请输入现价" />
                    </td>
                </tr>
                
                <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                <tr class="i_jiage">
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        价格
                    </td>
                    <td>
                        <select name="txtBinKeLeiXing" class="inputselect" i_val='<%#Eval("BinKeLeiXingId") %>'
                            valid="required" errmsg="请选择人员类型">
                            <option value="">请选择人员类型</option>
                        </select>
                        <input type="text" class="inputtext" name="txtJiaGe" value='<%#Eval("JiaGe","{0:F2}") %>' />
                        <a href="javascript:void(0)" onclick="iPage.insertJiaGe()">添加</a>
                        <a href="javascript:void(0)" onclick="iPage.deleteJiaGe(this)">删除</a>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                
                <asp:PlaceHolder runat="server" ID="phJiaGe">
                <tr class="i_jiage">
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        价格
                    </td>
                    <td>
                        <select name="txtBinKeLeiXing" class="inputselect" i_val="" valid="required" errmsg="请选择人员类型">
                            <option value="">请选择人员类型</option>
                        </select>
                        <input type="text" class="inputtext" name="txtJiaGe" />
                        <a href="javascript:void(0)" onclick="iPage.insertJiaGe()">添加</a>
                        <a href="javascript:void(0)" onclick="iPage.deleteJiaGe(this)">删除</a>
                    </td>
                </tr>
                </asp:PlaceHolder>
                
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        截止时间
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtJieZhiShiJian" class="inputtext" style="width: 380px;"
                            onfocus="WdatePicker()" valid="required" errmsg="请输入截止时间" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        团购封面
                    </td>
                    <td>
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        团购数量
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtTuanGouShu" class="inputtext" style="width: 380px;" valid="required" errmsg="请输入团购数" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: right;" class="alertbox-btn">
            <a href="javascript:void(0)" id="i_baocun"><s class="baochun"></s>保 存</a>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        var iPage = {
            binKeLeiXing: '<option value="">请选择宾客类型</option>',
            reload: function() {
                window.location.href = window.location.href;
            },
            close: function() {
                var _win = top || window;
                _win.Boxy.getIframeDialog('<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>').hide();
                return false;
            },
            baoCun: function(obj) {
                var validatorResult = ValiDatorForm.validator($("#i_form").get(0), "parent");
                if (!validatorResult) return;
                if ($.trim($("#<%=txtBKLX.ClientID %>").val()) == "如中国籍成人") $("#<%=txtBKLX.ClientID %>").val("");
                $(obj).unbind("click").css({ "color": "#999999" });
                KEditer.sync();
                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=baocun",
                    data: $("#i_baocun").closest("form").serialize(),
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.close();
                        } else {
                            alert(response.msg);
                            $(obj).bind("click", function() { iPage.baoCun(obj); }).css({ "color": "" });
                        }
                    },
                    error: function() {
                        $(obj).bind("click", function() { iPage.baoCun(obj); }).css({ "color": "" });
                    }
                });
            },
            xuanYong: function() {
                var _data = { RefererIfrId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeId") %>' }
                parent.Boxy.iframeDialog({ iframeUrl: "XuanZeHangQi.aspx?sl=<%=SL %>", title: "选择航期", modal: true, width: "910px", height: "400px", data: _data });
            },
            initRiQi: function() {
                var hangQiId = $("#<%=txtHangQiId.ClientID %>").val();
                if (hangQiId.length < 1) return;

                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=getriqi&hangqiid=" + hangQiId,
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        $("#txtRiQi").empty();
                        $("#txtRiQi").append(response.obj);
                        $("#txtRiQi").val("<%=RiQiId %>");
                    }
                });
            },
            initFangXing: function() {
                var hangQiId = $("#<%=txtHangQiId.ClientID %>").val();
                if (hangQiId.length < 1) return;

                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=getfangxing&hangqiid=" + hangQiId,
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        $("#txtFangXing").empty();
                        $("#txtFangXing").append(response.obj);
                        $("#txtFangXing").val("<%=FangXingId %>");
                    }
                });
            },
            initBinKeLeiXing: function() {
                var hangQiId = $("#<%=txtHangQiId.ClientID %>").val();
                if (hangQiId.length < 1) return;

                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=getbinkeleixing&hangqiid=" + hangQiId,
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        $("select[name='txtBinKeLeiXing']").empty();
                        $("select[name='txtBinKeLeiXing']").append(response.obj);
                        iPage.binKeLeiXing = response.obj;
                        $("select[name='txtBinKeLeiXing']").each(function() {
                            if ($(this).attr("i_val").length > 0) $(this).val($(this).attr("i_val"));
                        });
                    }
                });
            },
            setHangQi: function(data) {
                $("#<%=txtHangQiId.ClientID %>").val(data.hangQiId);
                $("#<%=txtHangQi.ClientID %>").val(data.mingCheng);

                this.initRiQi();
                this.initFangXing();
                this.initBinKeLeiXing();
                this.initYLLX();
            },
            getHangQi: function() {
                return { hangQiId: $("#<%=txtHangQiId.ClientID %>").val(), mingCheng: $("#<%=txtHangQi.ClientID %>").val() };
            },
            insertJiaGe: function() {
                var _length = $(".i_jiage").length;
                var _$tr = $(".i_jiage").eq(0).clone();
                _$tr.find("input").val('');
                _$tr.find("select").empty().append(this.binKeLeiXing).attr("i_val", "");
                $(".i_jiage").eq(_length - 1).after(_$tr);
            },
            deleteJiaGe: function(obj) {
                var _length = $(".i_jiage").length;
                if (_length == 1) {
                    alert("至少要有一个价格信息");
                    return;
                }
                $(obj).closest("tr").remove();
            },
            initYLLX: function() {
                var hangQiId = $("#<%=txtHangQiId.ClientID %>").val();
                if (hangQiId.length < 1) return;

                $.newAjax({
                    type: "POST",
                    url: window.location.href + "&doType=getyoulunleixing&hangqiid=" + hangQiId,
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function(response) {
                        if (response.obj == "1") {
                            $('tr[data-id="guoji"]').hide();
                            $("#txtGuoJi").removeAttr("valid");
                        } else {
                            $('tr[data-id="guoji"]').show();
                            $("#txtGuoJi").attr("valid", "required");
                        }
                    }
                });
            }
        };

        $(document).ready(function() {
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
            $("#i_a_xuanyong").bind("click", function() { iPage.xuanYong(this); });
            iPage.setHangQi({ hangQiId: "<%=HangQiId %>", mingCheng: "<%=HangQiMingCheng %>" });
            $("#txtGuoJi").val("<%=GuoJiId %>");

            if ($.trim($("#<%=txtBKLX.ClientID %>").val()).length == 0) $("#<%=txtBKLX.ClientID %>").val("如中国籍成人");
            $("#<%=txtBKLX.ClientID %>").focus(function() { if ($.trim(this.value) == "如中国籍成人") { this.value = ""; } });
            $("#<%=txtBKLX.ClientID %>").blur(function() { if ($.trim(this.value) == "") { this.value = "如中国籍成人"; } });
        });
    </script>

</asp:Content>

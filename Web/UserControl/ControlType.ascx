<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlType.ascx.cs"
    Inherits="Web.UserControl.ControlType" %>
<div>
    <label>
        <span id="sp_zk" class="sp_radio" value="2">
            <input type="radio" data-span="sp_zk_txt" name="Control" style="border-width: 0;" />
            <%=EyouSoft.Model.EnumType.SourceStructure.SourceControlType.总控%>
        </span>
    </label>
    <span id="sp_zk_txt" class="sp_txt" style="display: none">
        <input type="text" id="txt_sumControl" class="inputtext formsize180" readonly="readonly" />
        <a class="xuanyong" href="/ResourceCenter/CommonPage/Control.aspx" id="a_always"
            title="<%=EyouSoft.Model.EnumType.SourceStructure.SourceControlType.总控%>" data-width="300"
            data-height="120"></a></span>
    <label>
        <span id="sp_dk" class="sp_radio" value="1">
            <input type="radio" name="Control" style="border-width: 0;" />
            <%=EyouSoft.Model.EnumType.SourceStructure.SourceControlType.单控%>
        </span>
    </label>
    <span id="sp_dk_txt" class="sp_txt" style="display: none"><span id="sp_YURQ">预控日期：
        <input type="text" id="<%=ControlDateSClientID %>" name="<%=ControlDateSClientID %>"
            onfocus="WdatePicker();" class="inputtext formsize80">
        -
        <input type="text" id="<%=ControlDateEClientID %>" name="<%=ControlDateEClientID %>"
            onfocus="WdatePicker();" class="inputtext formsize80">
    </span>团号：
        <input type="text" id="txt_oneControl" class="inputtext formsize120" readonly="readonly" />
        <a class="xuanyong" href="/CommonPage/TourListBoxy.aspx" id="a_duplexing" title="<%=EyouSoft.Model.EnumType.SourceStructure.SourceControlType.单控%>--团号选择"
            data-width="700" data-height="400"></a></span>
    <input type="hidden" id="hd_cType" value="0" />
</div>

<script type="text/javascript">
    var ControlType = {
        Data: {
            ControlType: '', /*预控类型*/
            ShareType: null, /*共享状态*/
            Names: null, /*名称,*/
            Ids: null, /*Id*/
            TourList: [],
            Date: null /*酒店总控 预控时间*/
        },
        Init: function() {
            var that = this;
            newToobar.init(
            {
                box: "#sp_zk_txt",
                className: "xuanyong",
                para: {
                    sourceControlCategory: "<%=(int)SourceControlCategory %>",
                    pIframeID: Boxy.queryString("iframeID")

                }
            });
            newToobar.init(
            {
                box: "#sp_dk_txt",
                className: "xuanyong",
                callBackFun: "ControlType.DKCallBackFun",
                para: {
                    pIframeID: Boxy.queryString("iframeID")
                }
            });



            $(".sp_radio").click(function() {
                var obj = $(this);
                that.Data.ControlType = obj.attr("value");
                $(".sp_txt").css("display", "none")
                $("#" + obj.attr("id") + "_txt").css("display", "inline");
            })
            //处理预控日期
            //除酒店外该控件其余均无预控日期
            if ('<%=SourceControlCategory %>' != "酒店") {
                $("#sp_YURQ").remove();
            }
        },
        ZKCallBackFun: function(data) {
            var that = this;

            if (data) {
                var str = $.trim(data.Date || "" + "  " + data.Type);
                /*1=共享,2=不共享*/
                that.Data.ShareType = '<%=(int)EyouSoft.Model.EnumType.SourceStructure.ShareType.共享%>';
                if (data.Type == "不共享") {
                    if (data.Names) {
                        str += (", " + data.Names + "可用");
                    }
                    that.Data.ShareType = '<%=(int)EyouSoft.Model.EnumType.SourceStructure.ShareType.不共享%>';
                }
                $("#txt_sumControl").val(str)
                that.Data.Names = data.Names;
                that.Data.Ids = data.Ids;
                that.Data.Date = data.Date;
            }
        },
        DKCallBackFun: function(data) {
            if (data) {
                var obj = $("#txt_oneControl")
                var btStr = "";
                ControlType.Data.TourList = data.join('|');
                if (data[0] && data[0].split(',').length > 0) {
                    obj.val(data[0].split(',')[0])
                }
                for (var i in data) {

                    btStr += data[i].split(",")[0] + "<br/>";
                }
                $(".bt-wrapper").html("");
                obj.bt({
                    contentSelector: function() {
                        return btStr;
                    },
                    positions: ['left', 'right', 'bottom'],
                    fill: '#FFF2B5',
                    strokeStyle: '#D59228',
                    noShadowOpts: { strokeStyle: "#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 150,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
                    cssStyles: { color: '#00387E', 'line-height': '180%' }
                });
            }
        }
    }
    $(function() {
        ControlType.Init();
    })
</script>


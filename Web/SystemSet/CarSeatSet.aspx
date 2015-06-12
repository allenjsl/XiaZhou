<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarSeatSet.aspx.cs" Inherits="EyouSoft.Web.SystemSet.CarSeatSet" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script src="../Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="../Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div id="divzuoweihao" class="zuoweihao_box">
            <%if (Utils.GetQueryStringValue("type") != "sel")
              { %>
            <div id="divmuban" class="car_moban fixed">
            </div>
            <%} %>
            <div class="hr_10">
            </div>
            <div class="zuoweibox">
                <div id="zuowei_list" class="zuowei_list">
                </div>
            </div>
        </div>
        <input type="hidden" value="" id="hideJsonData" runat="Server">
        <div class="alertbox-btn" style="text-align: center;">
            <%if (Utils.GetQueryStringValue("type") != "sel")
              { %><a href="javascript:" hidefocus="true"
                id="btnSave" onclick="return CarSeatSet.Save();"><s class="baochun"></s>选 择</a><%} %><a
                    href="javascript:" hidefocus="true" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"><s
                        class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>
</body>

<script type="text/javascript">
    var CarSeatSet = {
        Params: { sl: '<%=Utils.GetQueryStringValue("sl") %>', memuid: '<%=Utils.GetQueryStringValue("memuid") %>', id: '<%=Utils.GetQueryStringValue("id") %>', seatnum: '<%=Utils.GetQueryStringValue("seatnum") %>', templateid: '<%=Utils.GetQueryStringValue("templateid") %>' },
        DataBind: function(data) {
            var datalist1 = [], datalist2 = [], divBox1 = $("#divmuban"), divBox2 = $("#zuowei_list");
            datalist1.push("<ul>");
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    //获取传过来的模板编号
                    var template = $.trim(CarSeatSet.Params.templateid);
                    //修改
                    if (template != "") {
                        if ($.trim(data[i].TemplateId) == template) {
                            //绑定模板
                            datalist1.push("<li><img onclick='return CarSeatSet.ImgClick(this);' src='" + data[i].FilePath + "'/><p><label><input name='muban' type='radio' value='" + data[i].TemplateId + "' checked='checked' />模版" + (i + 1) + "</label></p></li>");
                            //绑定座位
                            if (data[i].list != null) {
                                for (var j = 0; j < data[i].list.length; j++) {
                                    datalist2.push("<div class='movediv' style='left:" + data[i].list[j].PointX + "px; top:" + data[i].list[j].PoinY + "px;'><a  class='graybtn'>" + (j + 1) + "</a></div>");
                                }
                            }

                        } else {
                            datalist1.push("<li><img onclick='return CarSeatSet.ImgClick(this);' src='" + data[i].FilePath + "'/><p><label><input name='muban' type='radio' value='" + data[i].TemplateId + "'/>模版" + (i + 1) + "</label></p></li>");
                        }
                    }
                    //添加
                    else {
                        datalist1.push("<li><img onclick='return CarSeatSet.ImgClick(this);' src='" + data[i].FilePath + "'/><p><label><input name='muban' type='radio' value='" + data[i].TemplateId + "'/>模版" + (i + 1) + "</label></p></li>");
                    }
                }
                datalist1.push("</ul>");
                divBox1.html(datalist1.join(''));
                divBox2.html(datalist2.join(''));
            }
        },
        BindClick: function(templateId, data) {
            var datalist = [], divBox = $("#zuowei_list");
            if (data.length > 0) {
                divBox.html("");
                for (var i = 0; i < data.length; i++) {
                    if (data[i].TemplateId == templateId) {
                        if (data[i].list != null) {
                            for (var j = 0; j < data[i].list.length; j++) {
                                datalist.push("<div class='movediv' style='left:" + data[i].list[j].PointX + "px; top:" + data[i].list[j].PoinY + "px;'><a  class='graybtn'>" + (j + 1) + "</a></div>");
                            }
                        }
                    }
                }
                divBox.html(datalist.join(''));
            }
        },
        parentWindow: null,
        pIframeID: '<%=Request.QueryString["piframeId"]%>',
        Save: function() {
            var templateid =$.trim($('input:radio[name="muban"]:checked').val());
            var oldseatnum = CarSeatSet.Params.seatnum;
            CarSeatSet.parentWindow = window.parent.Boxy.getIframeWindow(CarSeatSet.pIframeID);
            CarSeatSet.parentWindow.$('#templateId').val(templateid);
            CarSeatSet.parentWindow.$('#oldseatnum').val(oldseatnum);
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
        },
        ImgClick: function(obj) {
            var _self = $(obj);
            _self.parent().find("input[type='radio']").click();
        }
    }
    $(function() {
        var data = $.parseJSON($("#hideJsonData").val()).data;
        CarSeatSet.DataBind(data);
        $("#divmuban").find("input[type='radio']").click(function() {
            var obj = $(this);
            CarSeatSet.BindClick(obj.val(), data);
        });

    });
</script>

</html>

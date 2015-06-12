<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangQiJiaGe.aspx.cs" Inherits="EyouSoft.Web.YouLun.HangQiJiaGe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择出团日期</title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache, must-revalidate" />
    <meta http-equiv="expires" content="0" />
    <style>
        body
        {
            margin: auto 0;
            padding: 0;
        }
        td
        {
            font-size: 12px;
            line-height: 120%;
        }
        table
        {
            border-collapse: collapse;
            width: 100%;
            background-color: #FFFFFF;
            border-collapse: collapse;
        }
        img
        {
            border: none;
        }
        .hui
        {
            color: #aaaaaa;
        }
        .calendarTable td
        {
            border-collapse: collapse;
            border: solid 1px #999999;
        }
        /*input
        {
            display: none;
        }*/
        .Xdays { text-align:center;}
    </style>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/tankkuang.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="ContactBox">
    <%=stroldHangqis %>
    </form>
    <div id="div1" style="height: 400px;">
    </div>
    <div id="divTourCodeHTML" style="width: 780px; margin-top: 10px;">
    </div>
    <div class="alertbox-btn" style="position: fixed;">
        <%--<a hidefocus="true" href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;">
            <s class="chongzhi"></s>关 闭</a>--%>
        <a href="javascript:void(0)" id="i_sheding"><s class="baochun"></s>&nbsp;设定价格</a><label><input type="checkbox" id="chkAuto" checked="checked" />自动生成价格&nbsp;&nbsp;</label>
    </div>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/YLJGgroupdate.js?v=0.1"></script>

    <script type="text/javascript">
        var pageDataJS = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', IframeId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("IframeId") %>', t: '<%=EyouSoft.Common.Utils.GetQueryStringValue("t") %>', hangxianid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("hangxianid") %>' },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSelect").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
            },
            CreateBoxy: function(data) {
                top.Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            callBack: function() { rili_callback(); }
        }

        $(function() {
            $(".sheding").live("click", function() {
                var data = {};
                if (pageDataJS.Params.t == '') {
                    data = { url: 'HangQiJiaGeBox.aspx?aid=' + $("#ContactBox").find("#" + $(this).attr("id")).attr("data-id") + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '价格设定', width: '950', height: '350' }
                }
                else {
                    data = { url: 'HangQiJiaGeBoxHY.aspx?aid=' + $("#ContactBox").find("#" + $(this).attr("id")).attr("data-id") + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '价格设定', width: '950', height: '350' }
                }
                pageDataJS.CreateBoxy(data);
            })//
            $(".xiugai").live("click", function() {
                var data = {};
                var riqi = $(this).attr("data-riqi");
                var riqiid = $("#" + riqi).attr("data-id");
                if (pageDataJS.Params.t == '') {
                    data = { url: 'HangQiJiaGeBox.aspx?aid=' + riqiid + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '价格设定', width: '950', height: '350' }
                }
                else {
                    data = { url: 'HangQiJiaGeBoxHY.aspx?aid=' + riqiid + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '价格设定', width: '950', height: '350' }
                }
                pageDataJS.CreateBoxy(data);
            })






        })
    </script>

    <script type="text/javascript">

        $(document).ready(function() {
            QGD.initCalendar({
                containerId: "div1",
                currentDate: <%=CurrentDate %>,
                firstMonthDate: <%=CurrentDate %>,
                nextMonthDate: <%=NextDate %>,
                listcontainer: "divTourCodeHTML",
                arrOldLeaveDate: '<% =OldDate %>'
            });//
            

            /*$("#div1").find("a").each(function() {
            var model=$(this);
                if ($("#ContactBox").find("#" + $(this).attr("id")).length > 0) {
                        if ($("#ContactBox").find("#" + $(this).attr("id")).attr("data-Jiage") == "False") {
                            $(this).closest("td").attr("class", "Xdays");
                            $(this).attr("class", "sheding").html("设定");
                        }
                        else {
                            $(this).closest("td").attr("class", "Xdays on");
                            $(this).attr("class", "sheding").html("已设定");
                        }
                }
            });*/

    
//                 $("#sheding").find("div.jiageM").each(function() {
//                if ($("#ContactBox").find("#" + $(this).attr("data-id")).length > 0) {
//                    $("#div1").find("#" + $("#ContactBox").find("#" + $(this).attr("data-id")).attr("id")).closest("td").attr("class", "Xdays on");
//                    $("#div1").find("#" + $("#ContactBox").find("#" + $(this).attr("data-id")).attr("id")).attr("class", "yisheding").html("已设定");
//                }
//            })
           rili_callback();
        });
    </script>
    
    <script type="text/javascript">
        function rili_callback() {
            $("#div1").find('input[type="checkbox"]').each(function() {
                $(this).unbind("click").bind("click", function() { setRiQi(this); });
                if ($("#ContactBox").find("#" + $(this).attr("value")).length == 0) {
                    $(this).closest("span").hide(); return true;
                }
                if ($("#ContactBox").find("#" + $(this).attr("value")).attr("data-Jiage") != "True") return;
//                $(this).closest("span").hide();

                var s = [];

                s.push('<span><a href="javascript:void(0)" class="xiugai" data-riqi="' + $(this).attr("value") + '">修改</a></span>');

                $(this).closest("td").append(s.join(''));
            });
        }

        function setRiQi(obj) {
            var riqi = $(obj).attr("value");
            var data = getRiQi();
            var ischeck = $(obj).attr("checked");

            if (ischeck) data.push(riqi);
            else data.splice($.inArray(riqi, data), 1);

            $("div").data("data-riqi", data);
        }

        function getRiQi() {
            var data = $("div").data("data-riqi");
            if (data == null || data == "undefined") data = [];
            return data;
        }

        function getRiQiId() {
            var riqi = getRiQi();
            var riqiid = [];

            if (riqi.length > 0) {
                $(riqi).each(function() {
                    riqiid.push($("#" + this).attr("data-id"));
                });
            }

            return riqiid;
        }

        $("#i_sheding").click(function() {
            var riqi = getRiQi();
            if (riqi.length < 1) { alert("至少要选择一个日期"); return false; }

            var isauto="0",isok=true;
            if($("#chkAuto").attr("checked")){isauto="1";}

            var riqiids=getRiQiId();
            if(riqiids.length>1){
                $.newAjax({
                    type: "get",
                    cache: false,
                    async:false,
                    url: "/youlun/HangQiJiaGe.aspx?do=check&hangqiid="+pageDataJS.Params.id+"&riqiids="+riqiids.join(','),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "0") {
                            parent.tableToolbar._showMsg("您选择的航期价格不一致，请重新选择！");
                            isok=false;
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        isok=false;
                    }
                });
            }
            
            if(!isok)return isok;

            var _data = {};
            if (pageDataJS.Params.t == '') {
                //_data = { url: 'HangQiJiaGeBox.aspx?piliang=1&aid=' + riqi.join(',') + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '设置价格', width: '950px', height: '350px' }
                _data = { url: 'HangQiJiaGeBox.aspx?riqiids='+riqiids.join(',')+'&piliang='+isauto+'&aid=&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id + '&hangxianid=' + pageDataJS.Params.hangxianid, title: '设置价格', width: '950px', height: '350px' }
            } else {
                //_data = { url: 'HangQiJiaGeBoxHY.aspx?piliang=1&aid=' + riqi.join(',') + '&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '设置价格', width: '950px', height: '350px' }
                _data = { url: 'HangQiJiaGeBoxHY.aspx?piliang=1&aid=&sl=' + pageDataJS.Params.sl + '&pIframeId=' + pageDataJS.Params.IframeId + '&id=' + pageDataJS.Params.id, title: '设置价格', width: '950px', height: '350px' }
            }

            pageDataJS.CreateBoxy(_data);
        });
    </script>
    
    
</body>
</html>

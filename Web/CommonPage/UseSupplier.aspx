<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UseSupplier.aspx.cs" Inherits="Web.CommonPage.UseSupplier" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <%--<input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />--%>
    <%--<input type="hidden" name="showID" id="ShowID" value='<%=Request.QueryString["ShowID"] %>' />--%>
    <input type="hidden" name="hideID_zyyk" id="hideID_zyyk" value='<%=Request.QueryString["HideID_zyyk"] %>' />
    <input type="hidden" name="tabId" id="tabId" />
    <div class="alertbox-outbox">
        <%if (flag == "1")
          { %>
        <div class="tablehead" style="background: none; border-top: none;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="javascript:void(0)" id="gysxy" value="0"><span>从供应商选用</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" id="ykzyxy" hidefocus="true"
                    href="javascript:void(0)" value="1"><span>从预控资源选用</span></a></li>
            </ul>
        </div>
        <%} %>
        <form id="formSearch" runat="server" method="get">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="SourceName"></span>名称：
                </td>
                <td width="90%" align="left">
                    <input name="txtName" type="text" class="inputtext formsize100" id="txtName" value='<%=Request.QueryString["txtName"]%>' />
                    省份：
                    <select name="ddlProvice" id="ddlProvice" class="inputselect">
                    </select>
                    城市：
                    <select name="ddlCity" id="ddlCity" class="inputselect">
                    </select>
                    县区：<select name="ddlArea" id="ddlArea" class="inputselect">
                    </select>
                    <input type="hidden" name="suppliertype" id="suppliertype" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("suppliertype") %>" />
                    <input type="hidden" name="Isyukong" id="Isyukong" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("isyukong") %>" />
                    <input type="hidden" name="flag" id="flag" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("flag") %>" />
                    <input type="hidden" name="callback" id="callback" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("callBack") %>" />
                    <input type="hidden" name="iframeid" id="iframeid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeid") %>" />
                    <input type="hidden" name="pIframeID" id="pIframeID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("pIframeID") %>" />
                    <input type="hidden" name="zyykid" id="zyykid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("zyykid") %>" />
                    <input type="hidden" name="ShowID" id="ShowID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("ShowID") %>" />
                    <input type="hidden" name="tourid" id="tourid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>" />
                    <input type="hidden" name="aid" id="aid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("aid") %>" />
                    <input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />
                    <input type="submit" value="搜索" class="search-btn" style="cursor: pointer; height: 24px;
                        width: 64px; background: url(/images/cx.gif) no-repeat center center; border: 0 none;
                        margin-left: 5px;" />
                </td>
            </tr>
        </table>
        </form>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02 t1">选择<span class="SourceName"></span> </span>
            <div id="AjaxHotelList" style="width: 100%">
            </div>
            <div class="alertbox-btn">
                <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    var UseSupplier = {
        AjaxURLg: null,
        AjaxURLy: null,
        flag: '<%=Request.QueryString["flag"] %>',
        Isyukong: '<%=Request.QueryString["isyukong"] %>',
        type: '<%=(int)type %>',
        tourid: '<%=Request.QueryString["tourid"] %>',
        aid: '<%=Request.QueryString["aid"] %>',
        PageInit: function() {
            UseSupplier.flag = $("#flag").val();
            UseSupplier.Isyukong = $("#Isyukong").val();
            pcToobar.init({
                pID: "#ddlProvice",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: '<%=this.SiteUserInfo.CompanyId %>',
                gSelect: "1",
                pSelect: '<%=Request.QueryString["ddlProvice"]??"0" %>',
                cSelect: '<%=Request.QueryString["ddlCity"]??"0" %>',
                xSelect: '<%=Request.QueryString["ddlArea"]??"0" %>'
            })
            var param = $.param({ provice: $("#ddlProvice").val(), city: $("#ddlCity").val(), area: $("#ddlArea").val(), name: $("#txtName").val() });
            switch (UseSupplier.type) {
                case "6":
                    this.GetUrl("hotel");
                    $("span.SourceName").html("餐馆");
                    break;
                case "2":
                    this.GetUrl("car");
                    UseSupplier.AjaxURLy = "/ResourceManage/AjaxRequest/AjaxCarSupplier.aspx?type=car";
                    $("span.SourceName").html("车队");
                    break;
                case "5":
                    this.GetUrl("ground");
                    $("span.SourceName").html("地接");
                    break;
                case "7":
                    this.GetUrl("shopping");
                    $("span.SourceName").html("购物");
                    break;
                case "3":
                    this.GetUrl("scenicspots");
                    UseSupplier.AjaxURLy = "/ResourceManage/AjaxRequest/AjaxSightSupplier.aspx?type=scenicspots";
                    $("span.SourceName").html("景点");
                    break;
                case "1":
                    this.GetUrl("wineshop");
                    UseSupplier.AjaxURLy = "/ResourceManage/AjaxRequest/AjaxHotelSupplier.aspx?type=wineshop";
                    $("span.SourceName").html("酒店");
                    break;
                case "4":
                    this.GetUrl("cruise");
                    UseSupplier.AjaxURLy = "/ResourceManage/AjaxRequest/AjaxCruiseSupplier.aspx?type=cruise";
                    $("span.SourceName").html("游轮");
                    break;
                case "14":
                    this.GetUrl("cruise");
                    UseSupplier.AjaxURLy = "/ResourceManage/AjaxRequest/AjaxCruiseSupplier.aspx?type=cruise";
                    $("span.SourceName").html("游轮");
                    break;
                case "9":
                    this.GetUrl("ticket");
                    $("span.SourceName").html("票务");
                    break;
                case "15":
                    this.GetUrl("ticket");
                    $("span.SourceName").html("票务");
                    break;
                case "16":
                    this.GetUrl("ticket");
                    $("span.SourceName").html("票务");
                    break;
                case "13":
                    this.GetUrl("other");
                    UseSupplier.AjaxURLy = "/ResourceManage/AjaxRequest/AjaxOtherSupplier.aspx?type=other";
                    $("span.SourceName").html("其它");
                    break;
            }
        },
        GetUrl: function(type) {
            this.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=" + type;
        },
        GetAjaxData: function(pageIndex, url) {
            var ajaxurl = "";
            if ($("#Isyukong").val() == "1") {
                ajaxurl = UseSupplier.AjaxURLy;
                $("#gysxy").removeClass("de-ztorderform");
                $("#ykzyxy").addClass("de-ztorderform");
            }
            else {
                ajaxurl = UseSupplier.AjaxURLg;
                $("#gysxy").addClass("de-ztorderform");
                $("#ykzyxy").removeClass("de-ztorderform");
            }
            //AJAX 加载数据
            $("#AjaxHotelList").html("<div style='width:100%; text-align:center;'><img src='/images/loadingnew.gif' border='0' align='absmiddle'/>&nbsp;正在加载,请等待....&nbsp;</div>");

            var para = { provice: '<%=Request.QueryString["ddlProvice"]??"0" %>', city: '<%=Request.QueryString["ddlCity"]??"0" %>', area: '<%=Request.QueryString["ddlArea"]??"0" %>' || "-1", name: $("#txtName").val(), callback: $("#callback").val(), iframeId: $("#iframeid").val(), piframeId: $("#pIframeID").val(), aid: UseSupplier.aid, zyykid: $("#zyykid").val(), ShowID: $("#ShowID").val(), flag: $("#flag").val(), isyukong: $("#Isyukong").val(), tourid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourid") %>' };
            var url = ajaxurl + "&" + $.param(para);
            $.newAjax({
                type: "Get",
                url: url + "&Page=" + pageIndex + "&suppliertype=" + Boxy.queryString("suppliertype"),
                cache: false,
                success: function(result) {
                    $("#AjaxHotelList").html(result);
                    //初始化选中
                    var boxyParentWin = window.parent.Boxy.getIframeWindowByID('<%=Request.QueryString["pIframeID"] %>') || parent;
                    if ($("#Isyukong").val() == "1") {
                        boxyParentWin.$('.Offers').each(function() {
                            var sueID = $(this).parent().find("input[type='hidden']:eq(1)").val();
                            if (sueID != null && sueID != "") {
                                $("#AjaxHotelList").find("input[type='radio'][zyyk='" + sueID + "']").attr("checked", "checked");
                                return false;
                            }
                        })
                    }
                    else {
                        boxyParentWin.$('.Offers').each(function() {
                            var sourceId = $(this).parent().find("input[type='hidden']:eq(0)").val();
                            if (sourceId != null && sourceId != "") {
                                $("#AjaxHotelList").find("input[type='radio'][value='" + sourceId + "']").attr("checked", "checked");
                                return false;
                            }

                        })
                    }
                    var data = {
                        name: '<%=EyouSoft.Common.Utils.GetQueryStringValue("suppliername") %>',
                        id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("supplierid") %>'
                    }
                    //报账页面 团队支出 选用 2次选中功能
                    //选中单选钮
                    $(":radio[value='" + data.id + "']").attr("checked", "checked");
                    //选中复选框
                    if (data.id.split(',').length > 0) {
                        var dataidArr = data.id.split(',');
                        for (var i in dataidArr) {
                            if (dataidArr.hasOwnProperty(i)) {
                                $(":checkbox[value='" + dataidArr[i] + "']").attr("checked", "checked");
                            }
                        }
                    }
                    $("#div_AjaxPage a").click(function() {
                        var str = $(this).attr("href").match(/&[^&]+$/);
                        pageIndex = str.toString().replace("&Page=", "");
                        UseSupplier.GetAjaxData(pageIndex, url);
                        return false;
                    });
                    $("#div_AjaxPage select").removeAttr("onchange").change(function() {
                        pageIndex = $(this).val();
                        UseSupplier.GetAjaxData(pageIndex, url);
                        return false;
                    });
                }
            });
        }
    }
    function Check() {
        if ($("#gysxy").hasClass("de-ztorderform")) {
            $("#tabId").val("gysxy");
        }
        else {
            $("#tabId").val("ykzyxy");
        }
    }
    //请求供应商选用
    function Requestgys(tableid) {
        $("#gysxy").addClass("de-ztorderform");
        $("#ykzyxy").removeClass("de-ztorderform");
        Check();
        $("#Isyukong").val("0");
        UseSupplier.Isyukong = "1";
        url = UseSupplier.AjaxURLg;
        UseSupplier.GetAjaxData(tableid == 1 ? 1 : Boxy.queryString("Page"), url);
    }
    //请求资源预控选用
    function Requestykzy(tableid) {
        $("#gysxy").removeClass("de-ztorderform");
        $("#ykzyxy").addClass("de-ztorderform");
        Check();
        $("#Isyukong").val("1");
        UseSupplier.Isyukong = "0";
        url = UseSupplier.AjaxURLy;
        UseSupplier.GetAjaxData(tableid == 1 ? Boxy.queryString("Page") : 1, url);
    }

    $(function() {
        UseSupplier.PageInit();
        var tabID = '<%=Request.QueryString["isyukong"]%>';
        switch (tabID) {
            default:
                Requestgys(tabID);
                break;
            case "1":
                Requestykzy(tabID);
                break;
        }
        $("#gysxy").click(function() {
            Requestgys();
        })
        $("#ykzyxy").click(function() {
            Requestykzy();
        })

        $("#a_btn").click(function() {
            useSupplierPage.SetValue();
            useSupplierPage.SelectValue();
            return false;
        })

        $("#tblList").find("input[type='radio']").click(function() {
            if ($("#sModel").val() != "2") {
                $("#tblList").find("input[type='radio']").attr("checked", "");
                $(this).attr("checked", "checked");
            }
        });

    });
    var useSupplierPage = {
        _dataObj: {},
        selectValue: "",
        selectTxt: "",
        selecttype: "",
        selectzyyk: "",
        contactname: "",
        contacttel: "",
        contactfax: "",
        zyykid: "",
        controlNum: "",
        useNum: "",
        jiuDianQianTaiTelephone: "",
        SetValue: function() {
            var valueArray = [], txtArray = [], contactname = [], fax = [], tel = [], zyykid = [], UserNum = [], ControlNum = []; jiuDianQianTaiTelephone = [];
            $("#tblList").find("input[type='radio']:checked").each(function() {
                valueArray.push($(this).val());
                txtArray.push($(this).attr("data-show"));
                contactname.push($(this).attr("data-contactname"));
                tel.push($(this).attr("data-tel"));
                fax.push($(this).attr("data-fax"));
                zyykid.push($(this).attr("zyyk"));
                UserNum.push($(this).attr("data-alreadnum"));
                ControlNum.push($(this).attr("data-controlcount"));
                jiuDianQianTaiTelephone.push($(this).attr("data-jiudianqiantaitelephone"));
            })

            this.selectValue = valueArray.join(',');
            this.selectTxt = txtArray.join(',');
            this.contactname = contactname.join(',');
            this.contacttel = tel.join(',');
            this.contactfax = fax.join(',');
            this.zyykid = zyykid.join(',');
            this.useNum = UserNum.join(',');
            this.controlNum = ControlNum.join(',');

            this.jiuDianQianTaiTelephone = jiuDianQianTaiTelephone.join(',');
        },
        RadioClickFun: function(args) {
            var rdo = $(args);
            var data = rdo.val().split(',');
            this.selectValue = data[0];
            this.selectTxt = data[1];
            this.contactname = data[2];
            this.contacttel = data[3];
            this.contactfax = data[4];
            this.jiuDianQianTaiTelephone = data[5];
            this.SelectValue();
        },
        SelectValue: function() {
            var data = {
                callBack: Boxy.queryString("callBack"),
                hideID: Boxy.queryString("hideID"),
                hideID_zyyk: Boxy.queryString("zyykid"),
                iframeID: Boxy.queryString("iframeId"),
                pIframeID: '<%=Request.QueryString["pIframeID"] %>',
                showID: Boxy.queryString("showID"),
                flag: $("#flag").val(),
                flagObj: Boxy.queryString("flagObj")
            }

            var args = {
                aid: '<%=Request.QueryString["aid"] %>',
                id: useSupplierPage.selectValue,
                name: useSupplierPage.selectTxt,
                type: '<%=Request.QueryString["suppliertype"] %>',
                showid: data.showID,
                hideid: data.hideID,
                hideID_zyyk: useSupplierPage.zyykid,
                flag: data.flag,
                contactname: useSupplierPage.contactname,
                contacttel: useSupplierPage.contacttel,
                contactfax: useSupplierPage.contactfax,
                isYuKong: $("input[type='hidden'][name='Isyukong']").val(),
                ControlNum: useSupplierPage.controlNum,
                UserNum: useSupplierPage.useNum,
                jiuDianQianTaiTelephone: useSupplierPage.jiuDianQianTaiTelephone
            }

            //根据父级是否为弹窗传值
            if (data.pIframeID != "" && data.pIframeID.length > 0) {
                //定义父级弹窗
                var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        boxyParent[data.callBack](args);
                    }
                    else {
                        boxyParent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            else {
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        window.parent[data.callBack](args);
                    }
                    else {
                        window.parent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
    }
    

</script>


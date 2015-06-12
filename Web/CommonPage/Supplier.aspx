<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="EyouSoft.Web.CommonPage.Supplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />
    <input type="hidden" name="showID" id="ShowID" value='<%=Request.QueryString["ShowID"] %>' />
    <div class="alertbox-outbox">
        <div class="tablehead" style="background: none; border-top: none;">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="javascript:void(0)"
                    id="unit" value="0"><span>从客户单位选用</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" id="gys" hidefocus="true"
                    href="javascript:void(0)" value="1"><span>从供应商选用</span></a></li>
            </ul>
        </div>
        <form id="formSearch" method="get">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span name="SourceName"></span>名称：
                </td>
                <td width="90%" align="left">
                    省份：
                    <select name="ddlProvice" id="ddlProvice" class="inputselect">
                    </select>
                    城市：
                    <select name="ddlCity" id="ddlCity" class="inputselect">
                    </select>
                    县区：<select name="ddlArea" id="ddlArea" class="inputselect">
                    </select>
                    <select class="inputselect" name="Sourcetype" id="Sourcetype">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EnumSource, ((int)type).ToString(), false)%>
                    </select>
                    单位名称：
                    <input name="txtName" type="text" class="inputtext formsize100" id="txtName" value='<%=Request.QueryString["txtName"]%>' />
                    <input type="hidden" name="suppliertype" id="suppliertype" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("Sourcetype") %>" />
                    <input type="hidden" name="callback" id="callback" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("callBack") %>" />
                    <input type="hidden" name="iframeid" id="iframeid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeid") %>" />
                    <input type="hidden" name="pIframeID" id="pIframeID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("pIframeID") %>" />
                    <input type="hidden" name="IsUniqueness" id="IsUniqueness" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("IsUniqueness") %>" />
                    <input type="hidden" name="ShowID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("ShowID") %>" />
                    <input type="hidden" name="IsMultiple" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("IsMultiple") %>" />
                    <input type="submit" id="search" value="搜索" class="search-btn" style="cursor: pointer;
                        height: 24px; width: 64px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;" />
                </td>
            </tr>
        </table>
        </form>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02 t1">选择<span name="SourceName"></span> </span>
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
        type: '<%=(int)type %>',
        PageInit: function() {
            pcToobar.init({
                pID: "#ddlProvice",
                cID: "#ddlCity",
                xID: "#ddlArea",
                comID: '<%=this.SiteUserInfo.CompanyId %>',
                gSelect: "1",
                pSelect: '<%=Request.QueryString["ddlProvice"] %>',
                cSelect: '<%=Request.QueryString["ddlCity"] %>',
                xSelect: '<%=Request.QueryString["ddlArea"] %>'
            })
            var param = $.param({ provice: $("#ddlProvice").val(), city: $("#ddlCity").val(), area: $("#ddlArea").val(), name: $("#txtName").val() });
            switch (UseSupplier.type) {
                case "6":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=hotel&urltype=supplier";
                    $("[name='SourceName']").html("餐馆");
                    break;
                case "2":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=car&urltype=supplier";
                    $("[name='SourceName']").html("车队");
                    break;
                case "5":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=ground&urltype=supplier";
                    $("[name='SourceName']").html("地接");
                    break;
                case "7":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=shopping&urltype=supplier";
                    $("[name='SourceName']").html("购物");
                    break;
                case "3":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=scenicspots&urltype=supplier";
                    $("[name='SourceName']").html("景点");
                    break;
                case "1":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=wineshop&urltype=supplier";
                    $("[name='SourceName']").html("酒店");
                    break;
                case "4":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=cruise&urltype=supplier";
                    $("[name='SourceName']").html("游轮");
                    break;
                case "14":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=cruise&urltype=supplier";
                    $("[name='SourceName']").html("游轮");
                    break;
                case "9":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=ticket&urltype=supplier";
                    $("[name='SourceName']").html("票务");
                    break;
                case "15":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=ticket&urltype=supplier";
                    $("[name='SourceName']").html("票务");
                    break;
                case "16":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=ticket&urltype=supplier";
                    $("[name='SourceName']").html("票务");
                    break;
                case "13":
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=other&urltype=supplier";
                    $("span [name='SourceName']").html("其它");
                    break;
                default:
                    UseSupplier.AjaxURLg = "/ResourceManage/AjaxRequest/AjaxSupplier.aspx?type=wineshop&urltype=supplier";
                    $("[name='SourceName']").html("酒店");
                    break;
            }
        },
        GetAjaxData: function(pageIndex, url) {
            //AJAX 加载数据
            $("#AjaxHotelList").html("<div style='width:100%; text-align:center;'><img src='/images/loadingnew.gif' border='0' align='absmiddle'/>&nbsp;正在加载,请等待....&nbsp;</div>");
            var para = { provice: '<%=Request.QueryString["ddlProvice"] %>', city: '<%=Request.QueryString["ddlCity"] %>', area: '<%=Request.QueryString["ddlArea"] %>' || "-1", name: $("#txtName").val(), callback: $("#callback").val(), iframeId: $("#iframeid").val(), piframeId: $("#pIframeID").val(), ShowID: $("#ShowID").val(), Sourcetype: '<%=(int)type %>', tourid: '<%=Request.QueryString["tourid"] %>' };
            $.newAjax({
                type: "Get",
                url: url + "&" + $.param(para) + "&Page=" + pageIndex,
                cache: false,
                success: function(result) {
                    $("#AjaxHotelList").html(result);

                    //初始化选中
                    var boxyParentWin = window.parent.Boxy.getIframeWindow(Boxy.queryString('<%=Request.QueryString["pIframeID"] %>')) || window.parent.Boxy.getIframeWindowByID('<%=Request.QueryString["pIframeID"] %>') || parent;
                    var sourceid = boxyParentWin.$('.Offers').parent().find("input[type='hidden']:eq(0)").val();
                    if (sourceid != null && sourceid != "") {
                        $("#AjaxHotelList").find("input[type='radio'][value='" + sourceid + "']").attr("checked", "checked");
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
    $("#search").click(function() {
        UseSupplier.type = $("#Sourcetype").val();
    })


    $(function() {
        UseSupplier.PageInit();
        UseSupplier.GetAjaxData((Boxy.queryString("Page") || 1), UseSupplier.AjaxURLg);
        $("#gys").click(function() {
            UseSupplier.GetAjaxData((Boxy.queryString("Page") || 1), UseSupplier.AjaxURLg);
        })
        $("#unit").click(function() {
            var obj = $(this);
            var url = "/CommonPage/CustomerUnitSelect.aspx?" + $.param({
                iframeId: '<%=Request.QueryString["iframeId"] %>',
                pIframeID: Boxy.queryString("pIframeID"),
                isgys: "isgys",
                IsMultiple: "1"
            });
            window.location = url + "&" + $.param(Boxy.getUrlParams(["pIframeID", "iframeId"]) || {});
            return false;
        })

        $("#a_btn").click(function() {
            useSupplierPage.SetValue();

            var data = {
                callBack: Boxy.queryString("callBack"),
                callBackFun: Boxy.queryString("callBackFun"),
                hideID: Boxy.queryString("hideID"),
                hideID_zyyk: Boxy.queryString("zyykid"),
                iframeID: Boxy.queryString("iframeId"),
                pIframeID: Boxy.queryString("pIframeID"),
                showID: Boxy.queryString("showID")

            }
            //根据父级是否为弹窗传值
            if (data.pIframeID != null && data.pIframeID.length > 0) {
                //定义父级弹窗
                var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
                var args = {
                    aid: '<%=Request.QueryString["aid"] %>',
                    id: useSupplierPage.selectValue,
                    name: useSupplierPage.selectTxt,
                    type: '<%=Request.QueryString["Sourcetype"]??"1" %>',
                    showid: data.showID,
                    hideid: data.hideID,
                    hideID_zyyk: useSupplierPage.zyykid,
                    CustomerUnitContactName: useSupplierPage.ContactName,
                    CustomerUnitContactPhone: useSupplierPage.ContactTel

                }
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    var callbackarr = data.callBack.split('.');
                    if (data.callBack.indexOf('.') == -1) {
                        boxyParent[data.callBack](args);
                    }
                    else {
                        boxyParent[callbackarr[0]][callbackarr[callbackarr.length - 1]](args);
                    }
                }
                if (data.callBackFun != null && data.callBackFun.length > 0) {
                    var callBackfunarr = data.callBackFun.split('.');
                    if (data.callBackFun.indexOf('.') == -1) {
                        boxyParent[data.callBackFun](args);
                    }
                    else {
                        boxyParent[callBackfunarr[0]][callBackfunarr[callBackfunarr.length - 1]](args);
                    }
                }
                //定义回调
            }
            else {
                var args = {
                    aid: '<%=Request.QueryString["aid"] %>',
                    id: useSupplierPage.selectValue,
                    name: useSupplierPage.selectTxt,
                    type: '<%=Request.QueryString["Sourcetype"]??"1" %>',
                    showid: data.showID,
                    hideid: data.hideID,
                    hideID_zyyk: useSupplierPage.zyykid,
                    CustomerUnitContactName: useSupplierPage.ContactName,
                    CustomerUnitContactPhone: useSupplierPage.ContactTel
                }
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        window.parent[data.callBack](args);
                    }
                    else {
                        window.parent[data.callBack.split('.')[0]][data.callBack.split('.')[data.callBack.split('.').length - 1]](args);
                    }
                }
                if (data.callBackFun != null && data.callBackFun.length > 0) {
                    if (data.callBackFun.indexOf('.') == -1) {
                        window.parent[data.callBackFun](args);
                    }
                    else {
                        window.parent[data.callBackFun.split('.')[0]][data.callBackFun.split('.')[data.callBack.split('.').length - 1]](args);
                    }
                }
                //定义回调
            }
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
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
        ContactName: "",
        ContactTel: "",

        SetValue: function() {
            var valueArray = new Array();
            var txtArray = new Array();
            var contactname = new Array();
            var tel = new Array();
            var zyykid = new Array();
            $("#tblList").find("input:radio:checked").each(function() {
                valueArray.push($(this).val());
                txtArray.push($(this).attr("data-show"));
                contactname.push($(this).attr("data-contactname"));
                tel.push($(this).attr("data-tel"));

            })
            this.selectValue = valueArray.join(',');
            this.selectTxt = txtArray.join(',');
            this.ContactName = contactname.join(',');
            this.ContactTel = tel.join(',');
        }
    }
    

</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KingDeeSubject.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.KingDeeSubject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>财务管理-金蝶科目-科目管理-添加</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/tankkuang.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"> </script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div class="tablelist-box" style="margin: 0 auto; width: 99%; padding-bottom: 5px;">
            <table width="100%">
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-index="1" data-subjectid="<%#Eval("Id") %>" data-subjectcd="<%#Eval("Cd") %>">
                            <th align="left" height="28" valign="middle" style="padding-left: 10px;">
                                <span class="leftStyle">
                                    <img data-class="img_getNext" data-subjectid="<%#Eval("Id") %>" align="middle" height="9"
                                        width="9" alt="" src="/images/zzjg+.gif">
                                    <b class="zzjg_jibie01">
                                        <label>
                                            <a>
                                                <input type="radio" name="KingDeeSubject" style="border: none" data-subjectnm="<%#Eval("Name") %>"
                                                    data-subjectcd="<%#Eval("Cd") %>" data-subjectid="<%#Eval("Id") %>">
                                                <%#Eval("Name")%></a></span></label></b>
                            </th>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="a_Save"><s class="xuanzhe"></s>选
                择</a><a href="javascript:void(0);" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();return false;"
                    hidefocus="true"><s class="chongzhi"></s>关 闭</a></div>
    </div>

    <script type="text/javascript">
        var KingDeeSubject = {
            GetNextNodal: function(obj) {
                var that = this;
                var obj = $(obj);
                var index = parseInt(obj.attr("data-index")) || 1;
                $.newAjax({
                    type: "get",
                    url: "/FinanceManage/KingdeeSubject/AjaxKingDeeSubject.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        parentSubjectId: obj.attr("data-subjectid"),
                        index: (index + 1 < 4 ? (index + 1) : 4),
                        isType: '<%=EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("isType")) %>'
                    }),
                    cache: false,
                    dataType: "html",
                    success: function(html) {
                        var inHtml = $(html).find("tr");
                        inHtml.attr("data-index", index + 1);
                        inHtml.find("td").css("padding-left", ((index + 1) * 2 - 1) * 10 + "px")
                        $(obj).after(inHtml)
                        that.PageInit();
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器繁忙，请稍后在试!");
                        return false;
                    }
                });
            },
            PageInit: function() {
                var that = this;
                //加载下级数据
                $("img[data-class='img_getNext']").unbind("click").click(function() {
                    var obj = $(this);
                    if (obj.attr("src").indexOf("+") > 0) {
                        obj.attr("src", "/images/zzjg-.gif");

                        var subNodal = $("tr[data-parentid='" + (parseInt(obj.attr("data-subjectid")) || 0) + "']");
                        if (subNodal.length <= 0) {
                            that.GetNextNodal($(this).closest("tr"));
                        }
                        else {
                            subNodal.css("display", "");
                        }
                    }
                    else {
                        obj.attr("src", "/images/zzjg+.gif");

                        var tr = $(this).closest("tr");
                        var hideArr = [];
                        tr.nextAll().each(function() {
                            var subobj = $(this)
                            if (parseInt(subobj.attr("data-index")) > parseInt(tr.attr("data-index"))) {
                                subobj.css("display", "none");
                                subobj.find("img").attr("src", "/images/zzjg+.gif")
                                subobj.find(":radio").removeAttr("checked");
                            }
                            else {
                                return false;
                            }
                        })
                    }
                })
                //选择
                $("#a_Save").unbind("click").click(function() {
                    var radioObj = $(":radio:checked")
                    if (radioObj.length <= 0) {
                        window.parent.tableToolbar.ShowConfirmMsg("您未选择 科目类型 确定继续？", function() {
                            window.parent.Boxy.getIframeDialog(Boxy.queryString("iframeId")).hide();
                        })
                    }
                    else {
                        var tr = $(radioObj).closest("tr");
                        var data = {}
                        data.Id = radioObj.attr("data-subjectid");
                        data.Cd = radioObj.attr("data-subjectcd");
                        data.Name = radioObj.attr("data-subjectnm");
                        var assemblyObj = radioObj.closest("tr").prevAll("tr[data-index=1]");
                        var parentPage = window.parent.Boxy.getIframeWindow(Boxy.queryString("pIframeID"))
                        if (parentPage && parentPage["SubjectManageEdit"] && parentPage["SubjectManageEdit"]["KingDeeSubjectCallBack"]) {
                            parentPage["SubjectManageEdit"]["KingDeeSubjectCallBack"](data);
                        }
                        if (parentPage && parentPage["AdjustItemEdit"] && parentPage["AdjustItemEdit"]["KingDeeSubjectCallBack"]) {
                            parentPage["AdjustItemEdit"]["KingDeeSubjectCallBack"](data);
                        }
                        window.parent.Boxy.getIframeDialog(Boxy.queryString("iframeId")).hide();
                    }
                })
            }
        }
        $(function() {
            KingDeeSubject.PageInit();
        })
    </script>

</body>
</html>

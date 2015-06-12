<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="Web.SingleServe.CustomerList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/import.js" type="text/javascript"></script>

    <script type="text/javascript">
        var ImportPage = {
            array: [],
            CallBackFun: '<%=Request.QueryString["callbackfun"] %>',
            ParentBox: parent.$('#Customlist'),
            Type: '1'
        }
        function loadexcel(array) {
            loadXls.init(array, "#tablelist", "");

        }
        $(function() {
            $("#btnSelect").click(function() {

                ImportPage.array = loadXls.bindIndex();

                if (ImportPage.array.length == 0) {

                    parent.tableToolbar._showMsg("请选择数据!");
                    return false;
                } else {
                    switch (ImportPage.Type) {
                        case "1": //非出境游客列表
                            CerateType1(ImportPage.array);
                            break;
                    }
                }
            })
        })
        //导入客户名单--Start
        function CerateType1(array) {
            var rowN = { name: 0, type: 0, cardType: 0, cardNo: 0, sex: 0, tel: 0 };
            for (var i = 0; i < array[0].length; i++) {
                switch (array[0][i]) {

                    case "姓名": rowN.name = i; break;
                    case "类型": rowN.type = i; break;
                    case "证件名称": rowN.cardType = i; break;
                    case "证件号码": rowN.cardNo = i; break;
                    case "性别": rowN.sex = i; break;
                    case "联系电话": rowN.tel = i; break;
                }
            }

            if (ImportPage.ParentBox.length > 0) {

                for (var i = 1; i < array.length; i++) {
                    if (array[i]) {
                        var tr = ImportPage.ParentBox.find("tr[class='showlist']").eq(0).clone(true);

                        tr.find("input[name='CustomName']").val(array[i][rowN.name]);
                        tr.find("input[name='CertificatesNum']").val(array[i][rowN.cardNo]);
                        tr.find("input[name='ContactTel']").val(array[i][rowN.tel]);


                        tr.find("select[name='CustomType']").find("option").each(function() { if ($(this).text() == array[i][rowN.type]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='Certificates']").find("option").each(function() { if ($(this).text() == array[i][rowN.cardType]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='Sex']").find("option").each(function() { if ($(this).text() == array[i][rowN.sex]) { $(this).attr("selected", "selected") } });

                    }
                    ImportPage.ParentBox.append(tr);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
            }
        }
        //导入客户名单--End
    </script>

</head>
<body style="background-color: #e9f4f9">
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto;">
            <tbody>
                <tr>
                    <td bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;" class="alertboxTableT"
                        colspan="2">
                        <asp:Label ID="lblTempDown" runat="server" Text=""></asp:Label>&nbsp; （只能导入格式为.xls、xlsx的文件）
                        <span><a href="/ExcelDownTemp/custom_list.xls">下载游客名单模板</a></span>
                    </td>
                </tr>
                <tr>
                    <td width="15%" valign="middle" bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;"
                        class="alertboxTableT">
                        从Excel文件导入
                    </td>
                    <td valign="middle" bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;" class="alertboxTableT">
                        <div>
                            <input type="hidden" id="hidFileName" />
                            <span runat="server" id="spanButtonPlaceholder"></span><span id="errMsg" class="errmsg">
                            </span>
                        </div>
                        <div id="divUpload">
                            <div id="divFileProgressContainer">
                            </div>
                            <div id="thumbnails">
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 98%;">
            <span class="formtableT formtableT02">源数据预览</span>
            <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
                id="tablelist">
                <tbody>
                    <tr class="">
                        <td align="center">
                            暂无数据
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSelect"><s class="xuanzhe"></s>
                选 择</a></div>
    </div>
    </form>
</body>
</html>

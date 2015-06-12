<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WangZhan.aspx.cs" Inherits="EyouSoft.Web.YouLun.WangZhan"
    MasterPageFile="~/MasterPage/Front.Master" ValidateRequest="false" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/YouLun/WUC/wuc2.ascx" TagName="wuc2" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <uc1:wuc2 runat="server" ID="wuc2" />
        <form>
        <div class="tablelist-box">
        <table width="100%" align="center" id="liststyle">
            <tr>
                <th align="center" class="th-line">
                    网站title
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background:#fff;">
                    <input type="text" id="txtTitle" runat="server" class="inputtext" style="width:800px" />
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    网站keywords
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background:#fff;">
                    <input type="text" id="txtKeywords" runat="server" class="inputtext" style="width: 800px" />
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    网站description
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background:#fff;">
                    <input type="text" id="txtDescription" runat="server" class="inputtext" style="width: 800px" />
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    维诗达简介
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background:#fff;">
                    <textarea runat="server" id="txtJianJie"></textarea>
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    联系我们
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background: #fff;">
                    <textarea runat="server" id="txtLianXiWoMen"></textarea>
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    网站版权
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background: #fff;">
                    <textarea runat="server" id="txtBanQuan"></textarea>
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    企业文化
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background: #fff;">
                    <textarea runat="server" id="txtV7"></textarea>
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    旅游度假资质
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background: #fff;">
                    <textarea runat="server" id="txtzizhi"></textarea>
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line">
                    广告业务
                </th>
            </tr>
            <tr>
                <th align="center" class="th-line" style="background: #fff;">
                    <textarea runat="server" id="txtguanggao"></textarea>
                </th>
            </tr>
        </table>        
        </div>
        
        <div class="mainbox cunline fixed">
            <div class="hr_10">
            </div>
            <ul>
                <li class="cun-cy"><a id="i_baocun" href="javascript:void(0)">保存</a> </li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
        </form>
    </div>
    
    <link href="/css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <script src="/js/swfupload/swfupload.js" type="text/javascript"></script>
    <script src="/js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            baoCun: function() {
                KEditer.sync();
                $.ajax({ type: "post", cache: false, url: "wangzhan.aspx?dotype=baocun&sl=<%=SL %>", data: $("#i_baocun").closest("form").serialize(), dataType: "json",
                    success: function(response) {
                        alert(response.msg);
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            wuc2(0);
            KEditer["UPLOAD_P1"] = "YL";
            var _keoptions = { items: keSimpleYL, height: "350px", width: "800px" }
            KEditer.init('<%=txtJianJie.ClientID %>', _keoptions);
            KEditer.init('<%=txtLianXiWoMen.ClientID %>', _keoptions);
            KEditer.init('<%=txtBanQuan.ClientID %>', _keoptions);
            KEditer.init('<%=txtV7.ClientID %>', _keoptions);
            KEditer.init('<%=txtzizhi.ClientID %>', _keoptions);
            KEditer.init('<%=txtguanggao.ClientID %>', _keoptions);
            $("#i_baocun").click(function() { iPage.baoCun(); });
        });

        
    </script>
</asp:Content>

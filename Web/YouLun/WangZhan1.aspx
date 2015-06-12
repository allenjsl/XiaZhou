<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WangZhan1.aspx.cs" Inherits="EyouSoft.Web.YouLun.WangZhan1"
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
                        如何订票
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line" style="background: #fff;">
                        <textarea runat="server" id="txtV1"></textarea>
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line">
                        如何取票
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line" style="background: #fff;">
                        <textarea runat="server" id="txtV2"></textarea>
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line">
                        游船旅行准备
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line" style="background: #fff;">
                        <textarea runat="server" id="txtV3"></textarea>
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line">
                        旅行注意事项
                    </th>
                </tr>
                <tr>
                    <th align="center" class="th-line" style="background: #fff;">
                        <textarea runat="server" id="txtV4"></textarea>
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
                $.ajax({ type: "post", cache: false, url: "wangzhan1.aspx?dotype=baocun&sl=<%=SL %>", data: $("#i_baocun").closest("form").serialize(), dataType: "json",
                    success: function(response) {
                        alert(response.msg);
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            wuc2(5);
            KEditer["UPLOAD_P1"] = "YL";
            var _keoptions = { items: keSimpleYL, height: "350px", width: "800px" }
            KEditer.init('<%=txtV1.ClientID %>', _keoptions);
            KEditer.init('<%=txtV2.ClientID %>', _keoptions);
            KEditer.init('<%=txtV3.ClientID %>', _keoptions);
            KEditer.init('<%=txtV4.ClientID %>', _keoptions);

            $("#i_baocun").click(function() { iPage.baoCun(); });
        });

        
    </script>

</asp:Content>

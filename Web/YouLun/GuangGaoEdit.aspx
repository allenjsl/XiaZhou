<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuangGaoEdit.aspx.cs" Inherits="EyouSoft.Web.YouLun.GuangGaoEdit"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageBody">
    <div class="alertbox-outbox">
        <form id="i_form">
        <div style="width: 99%; margin: 0px auto;">
            <table cellspacing="0" cellpadding="0" style="width: 100%; margin: 0 auto" class="firsttable">
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        广告名称
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtMingCheng" class="inputtext" style="width: 380px;"
                            valid="required" errmsg="请填写广告名称" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        广告位置
                    </td>
                    <td>
                        <select name="txtWeiZhi" id="txtWeiZhi" class="inputselect" valid="required" errmsg="请选择广告位置">
                            <option value="">-请选择-</option>
                            <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi),new string[]{"0"}))%>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left;" class="addtableT">
                        广告图片
                    </td>
                    <td>
                        <uc1:wucupload runat="server" ID="upload1" IsUploadMore="false" IsUploadSelf="true"
                            FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        链接地址
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtUrl" class="inputtext" style="width: 380px;" />
                    </td>
                </tr>
                <tr>
                    <td style="background: #b7e0f3; height: 30px; text-align: left; width: 80px;" class="addtableT">
                        排序值
                    </td>
                    <td>
                        <input type="text" runat="server" id="txtPaiXuId" class="inputtext" style="width: 380px;" /><span
                            style="color: #666">（默认0 按值升序排列）</span>
                    </td>
                </tr>
            </table>
            <div style="color:#666">
            横幅类图片尺寸：1920*258px<br />
            游轮会议图片尺寸：546*323px<br />
            积分商城首页轮换图片尺寸：922*237px<br />
            登录左侧大图图片尺寸：743*495px<br />
            登录页右下图片尺寸：341*110px<br />
            注册右侧大图图片尺寸：635*372px<br />
            重置登录密码页右侧大图图片尺寸：635*372px<br />
            首页轮播图片：845*399px
            </div>
        </div>
        <div style="text-align: right;" class="alertbox-btn">
            <a href="javascript:void(0)" id="i_baocun"><s class="baochun"></s>保 存</a>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        var iPage = {
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

                $(obj).unbind("click").css({ "color": "#999999" });

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
            }
        };

        $(document).ready(function() {
            $("#i_baocun").bind("click", function() { iPage.baoCun(this); });
            $("#txtWeiZhi").val("<%=WeiZhi %>");
        });
    </script>

</asp:Content>

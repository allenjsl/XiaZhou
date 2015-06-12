<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="CityList.aspx.cs" Inherits="Web.SystemSet.CityList" %>

<%@ Register Src="../UserControl/BasciSetBar.ascx" TagName="BasciSetBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidCommonCity" runat="server" />
    <div class="mainbox mainbox-whiteback">
        <uc1:BasciSetBar ID="BasciSetBar1" runat="server" />
        <%--<div style="background: none #137CBF; text-align: center; font-size: 14px; color: #fff;
            font-weight: bold;" class="tablehead">
            <table width="30%" border="0" align="center" style="margin: 0 auto;">
                <tbody>
                    <tr>
                        <td height="30">
                            国家：
                            <select id="sltCountry" name="sltCountry" class="inputselect">
                            </select>
                            省份:
                            <select id="sltProvince" name="sltProvince" class="inputselect">
                            </select>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>--%>
        <div class="alertbox-outbox">
            <%if (CountryModel != null)
              { %>
            <% for (int i = 0; i < CountryModel.Provinces.Count; i++)
               {
                   int ProvineId = CountryModel.Provinces[i].ProvinceId; 
            %>
            <div provineid="<%=ProvineId %>" type="div" class="jichu_city">
                <%--<div class="hr_5">
                </div>--%>
                <span class="formtableT formtableT02">
                    <%=CountryModel.Provinces[i].Name%></span>
                <table width="100%" cellspacing="0" cellpadding="0" align="center">
                    <tbody>
                        <% for (int j = 0; j < CountryModel.Provinces[i].Citys.Count; j++)
                           {
                               int CityId = CountryModel.Provinces[i].Citys[j].CityId;
                               if (j % 10 == 0)
                               {%>
                        <tr>
                            <%
                                } %>
                            <td height="26" bgcolor="#FFFFFF" align="left">
                                &nbsp;
                                <input type="checkbox"  style=" border:none;" id="<% =CityId%>" name="chk" /><label for="<% =CityId%>"><%=CountryModel.Provinces[i].Citys[j].Name%></label>
                            </td>
                            <% if ((j != 0 && (j + 1) % 10 == 0) || j + 1 == CountryModel.Provinces[i].Citys.Count)
                               {%>
                        </tr>
                        <%}
                           } %>
                    </tbody>
                </table>
                <div class="hr_5">
                </div>
            </div>
            <%
                }
              }
            %>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(function() {

            //pcToobar.init({ gID: "#sltCountry", pID: "#sltProvince", comID: "<%=SiteUserInfo.CompanyId %>" });

            var ids = $("#<%=hidCommonCity.ClientID%>").val().split(',');
            for (var i = 0; i < ids.length; i++) {
                $("[id='" + ids[i] + "']:checkbox").attr("checked", "checked");
            }

            /*$("#sltProvince").change(function() {
                //省份编号
                var provineid = $(this).val();
                $("[type='div']").each(function() {

                    if ($(this).attr("provineid") == provineid) {
                        $(this).css("display", "block");
                    } else {
                        if (provineid == 0) {
                            $(this).css("display", "block");
                        } else {
                            $(this).css("display", "none");
                        }
                    }
                });
            });*/
            
            $("[name='chk']").click(function() {
                var id = $(this).attr("id");

                $.newAjax({
                    type: "GET",
                    cache: false,
                    url: window.location + '&oper=setcity&cityid=' + id,
                    dataType: "text",
                    success: function(data) {
                        tableToolbar._showMsg(data);
                    },
                    error: function() {
                        tableToolbar._showMsg(data);
                    }
                });
            });
        });
    </script>

</asp:Content>

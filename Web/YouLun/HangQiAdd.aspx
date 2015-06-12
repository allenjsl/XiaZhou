<%@ Page Title="游轮管理-新增" Language="C#" MasterPageFile="~/MasterPage/Front.Master"
    AutoEventWireup="true" CodeBehind="HangQiAdd.aspx.cs" Inherits="EyouSoft.Web.YouLun.HangQiAdd"
    EnableEventValidation="false" ValidateRequest="false" CodePage="936" %>

<%@ Register Src="/UserControl/YLXingCheng.ascx" TagName="YLXingCheng" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/YLCompanySelect.ascx" TagName="YLCompanySelect"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControl/SupplierControl.ascx" TagName="SupplierControl"
    TagPrefix="uc3" %>
<%@ Register Src="~/YouLun/WUC/wucupload.ascx" TagName="wucupload" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="wrap">
        <!--内容-->
        <!-- InstanceBeginEditable name="EditRegion3" -->
        <div class="mainbox mainbox-whiteback">
            <div class="addContent-box1">
                <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                        <td width="12%" class="addtableT">
                            SEO Titel：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSeoTitle" CssClass="formsize80 inputtext" runat="server" Width="100%"></asp:TextBox>
                        </td>
                        <td width="12%" class="addtableT">
                            SEO KeyWords：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSeoKeyword" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                        <td width="12%" class="addtableT">
                            SEO Description：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtSeoDescription" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td width="12%" class="addtableT">
                            产品名称：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtchanpinmingcheng" CssClass="formsize80 inputtext" runat="server"
                                valid="required" errmsg="请输入产品名称！" Width="100%"></asp:TextBox>
                        </td>
                        <td width="12%" class="addtableT">
                            产品编号：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtchanpinbainhao" CssClass="formsize80 inputtext" runat="server" ReadOnly="true" BackColor="#dadada"></asp:TextBox>
                        </td>
                        <td width="12%" class="addtableT">
                            起始价格：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtqishijiage" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            供应商：
                        </td>
                        <td class="kuang2">
                            <uc3:SupplierControl ID="SupplierControl1" runat="server" SupplierType="涉外游轮" CallBack="pageDataJS._AjaxContectInfo" />
                        </td>
                        <td class="addtableT">
                            游轮公司：
                        </td>
                        <td class="kuang2">
                            <uc2:YLCompanySelect ID="YLCompanySelect1" runat="server" CallBackFun="pageDataJS.ajaxYLinfo" />
                        </td>
                        <td class="addtableT">
                            排序值：
                        </td>
                        <td class="kuang2" colspan="3">
                            <asp:TextBox ID="txtpaixuid" CssClass="formsize80 inputtext" runat="server"></asp:TextBox><span style="color:#666">（默认0 按值升序排列）</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            游轮系列：
                        </td>
                        <td class="kuang2">
                            <asp:DropDownList ID="ddlxilie" runat="server" CssClass="inputselect" valid="required"
                                errmsg="请选择游轮系列！">
                                <asp:ListItem Value=""> 请选择</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="addtableT">
                            游轮船只：
                        </td>
                        <td class="kuang2">
                            <asp:DropDownList ID="ddlchuanzhi" runat="server" CssClass="inputselect" valid="required"
                                errmsg="请选择船只！">
                                <asp:ListItem Value=""> 请选择</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="addtableT">
                            游轮航线：
                        </td>
                        <td class="kuang2 imgmiddle">
                            <asp:DropDownList ID="ddlhangxian" runat="server" CssClass="inputselect" valid="required"
                                errmsg="请选择航线！">
                                <asp:ListItem Value=""> 请选择</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            出发港口：
                        </td>
                        <td class="kuang2">
                            <asp:DropDownList ID="ddlchufagangkou" runat="server" CssClass="inputselect" valid="required"
                                errmsg="请输入出发港口！">
                                <asp:ListItem Value=""> 请选择</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="addtableT">
                            抵达港口：
                        </td>
                        <td class="kuang2">
                            <asp:DropDownList ID="ddldidagangkou" runat="server" CssClass="inputselect" valid="required"
                                errmsg="请选择抵达港口！">
                                <asp:ListItem Value=""> 请选择</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="addtableT">
                            行程天数：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtxingchengtian" CssClass="formsize80 inputtext" runat="server"
                                min="1"></asp:TextBox>
                            天
                            <asp:TextBox ID="txtxingchengwan" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                            晚
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            航线性质：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="hangxianxingzhi" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            航期图片(450*278)：
                        </td>
                        <td class="kuang2" colspan="3">
                            <uc1:wucupload runat="server" ID="upload1" IsUploadMore="true" IsUploadSelf="true"
                                FileTypes="*.jpg;*.gif;*.jpeg;*.png" YangShi="1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            合同快递费用：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="kuaidifei" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            销量：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtXiaoLiang" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            好评率：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtHaoPing" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>%
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            途经城市：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox ID="txttujingchengshi" CssClass="formsize800 inputtext" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            起始价格说明：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="txtqishijiageshuoming" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            产品特色：
                        </td>
                        <td colspan="5" class="kuang2 pand4" width="90%">
                            <asp:TextBox runat="server" ID="txtchanpintese" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            优惠信息：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="txtyouhuixinxi" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            费用说明：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="txtfeiyongshuoming" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            预订须知：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="txtyudingxuzhi" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            友情提示：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="txtyouqingtishi" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            签证签注：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="qianzhengqianzhu" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            旅游攻略：
                        </td>
                        <td colspan="5" class="kuang2 pand4">
                            <asp:TextBox runat="server" ID="txtlvyougonglve" Height="65" CssClass="inputtext formsize600 editbox"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            最多可抵扣积分：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtzuiduojifen" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                        </td>
                        <td class="addtableT" title="积分兑换比例*用户下单抵扣积分=该次下单积分抵扣的金额">
                            积分兑换比例：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtjifenbili" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                            %
                        </td>
                        <td class="addtableT" title="积分累积比例*订单金额=用户该次下单可获得积分">
                            积分累积比例：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtleijibili" CssClass="formsize80 inputtext" runat="server"></asp:TextBox>
                            %
                        </td>
                    </tr>
                </table>
            </div>
            <uc1:YLXingCheng ID="YLXingCheng1" runat="server" />
            <div class="hr_10">
            </div>
            <div class="addxc-box">
                <span class="formtableT ">附加产品</span>
                <table id="fujiachanpin" width="100%" cellpadding="0" cellspacing="0" class="add-baojia autoAdd12"
                    style="background-image: none">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
                        <tbody class="tempRow2">
                            <tr>
                                <td align="right">
                                    服务类型<input type="hidden" name="fujiacpid" value="" />
                                &nbsp;</td>
                                <td align="left">
                                    <select name="selectfwlx" class="inputselect" <%--valid="required" errmsg="请选择服务类型！"--%>>
                                        <%=getOpts("")%>
                                    </select>
                                </td>
                                <td align="right">
                                    服务项目
                                </td>
                                <td align="left">
                                    <input name="txtfwxm" type="text" class="formsize120 inputtext" value="" />
                                </td>
                                <td align="right">
                                    单价
                                </td>
                                <td align="left">
                                    <input name="txtdanjia" type="text" class="formsize120 inputtext" value="" />
                                </td>
                                <td align="right">
                                    计价单位
                                </td>
                                <td align="left">
                                    <input name="txtjijiadanwei" type="text" class="formsize120 inputtext" value="" />
                                </td>
                                <td align="center" rowspan="2">
                                    <a href="javascript:void(0)" class="addbtn">
                                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                            class="delbtn">
                                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    产品介绍
                                </td>
                                <td align="center" colspan="7">
                                    <textarea name="txtchanpinjieshao" class="inputtext   editbox" style="width: 500px;"></textarea>
                                </td>
                            </tr>
                        </tbody>
                    </asp:PlaceHolder>
                    <asp:Repeater ID="rptlist" runat="server">
                        <ItemTemplate>
                            <tbody class="tempRow2">
                                <tr>
                                    <td align="right">
                                        服务类型<input type="hidden" name="fujiacpid" value="<%#Eval("FuJiaChanPinId") %>" />
                                    &nbsp;</td>
                                    <td align="left">
                                        <select name="selectfwlx" class="inputselect" <%--valid="required" errmsg="请选择服务类型！"--%>>
                                            <%#getOpts(Eval("LeiXingId").ToString())%>
                                        </select>
                                    </td>
                                    <td align="right">
                                        服务项目
                                    </td>
                                    <td align="left">
                                        <input name="txtfwxm" type="text" class="formsize120 inputtext" value="<%# Eval("XiangMu") %>" />
                                    </td>
                                    <td align="right">
                                        单价
                                    </td>
                                    <td align="left">
                                        <input name="txtdanjia" type="text" class="formsize120 inputtext" value="<%# Eval("DanJia","{0:F2}") %>" />
                                    </td>
                                    <td align="right">
                                        计价单位
                                    </td>
                                    <td align="left">
                                        <input name="txtjijiadanwei" type="text" class="formsize120 inputtext" value="<%# Eval("DanWei") %>" />
                                    </td>
                                    <td align="center" rowspan="2">
                                        <a href="javascript:void(0)" class="addbtn">
                                            <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                                class="delbtn">
                                                <img src="/images/delimg.gif" width="48" height="20" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        产品介绍
                                    </td>
                                    <td align="center" colspan="7">
                                        <textarea name="txtchanpinjieshao" class="inputtext  editbox " style="width: 500px;"><%# Eval("JieShao")%></textarea>
                                    </td>
                                </tr>
                            </tbody>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="hr_10">
            </div>
            <div class="mainbox cunline fixed">
                <ul>
                    <li class="cun-cy"><a id="btnSave" href="javascript:;">保存</a></li>
                    <li class="cun-cy"><a href="HangQi.aspx?sl=97">返回列表</a></li>
                </ul>
                <div class="hr_10">
                </div>
            </div>
        </div>
        <input type="hidden" value="" id="xlbh" runat="server" />
        <input type="hidden" value="" id="czbh" runat="server" />
        <!-- InstanceEndEditable -->
        <div class="hr_5">
        </div>
        <!--版权-->
    </div>
    </form>

    <script type="text/javascript">
        var pageDataJS = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>', t: '<%=EyouSoft.Common.Utils.GetQueryStringValue("t") %>' },
            //创建编辑器
            CreateEdit: function(obj) {
                var _self = $(obj);
                var _ke=keSimpleYL;
                if ($.trim(_self.attr("id")).length == 0) _self.attr("id", "txtRemark" + parseInt(Math.random() * 1000));
                KEditer.remove(_self.attr("id"));
//                if(_self.attr("id")=="<%=qianzhengqianzhu.ClientID %>"){
//                    keSimpleYL.push("insertfile");
//                }
//                else if(keSimpleYL.indexOf("insertfile")>=0){
//                    keSimpleYL.splice(keSimpleYL.indexOf("insertfile"),1);
//                }
                if(_self.attr("id")=="<%=qianzhengqianzhu.ClientID %>"){
                    _ke=keSimpleYL1;
                }
                else{
                    _ke=keSimpleYL;
                }
                KEditer.init(_self.attr("id"), { resizeMode: 0, items: _ke, height: "70px", width: "800px" });
            },
            DelFile: function(obj) {
                $(obj).parent().remove();
            },
            RemoveFile: function(obj) {
                $(obj).closest("td").find("input[name='hide_Cruiseimg_file']").val("");
                $(obj).closest("div[class='upload_filename']").remove();
                return false;
            },
            Save: function() {
                KEditer.sync();
                pageDataJS.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "HangQiAdd.aspx?save=save&" + $.param(pageDataJS.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { window.location.href = "HangQi.aspx?sl=97&t=" + pageDataJS.Params.t; });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            pageDataJS.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJS.BindBtn();
                    }
                })
            },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJS.Save();
                    } else {
                        return false;
                    }
                });
            },
            _AjaxContectInfo: function(obj) {
                $("#<%=SupplierControl1.ClientText %>").val(obj.name);
                $("#<%=SupplierControl1.ClientValue %>").val(obj.id);
                $("#<%=SupplierControl1.ClientzyykValue %>").val(obj.hideID_zyyk);
                $("#<%=SupplierControl1.ClientIsyukong %>").val(obj.isYuKong);

            },
            AddRowCallBack: function(tr) {
                var $tr = tr;
                pageDataJS.CreateEdit($tr.find(".editbox"));
            },
            ajaxYLinfo: function(data) {
                $.ajax({
                    url: "HangQiAdd.aspx?sl=97&getXL=1&gongsi=" + data.gongsi,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            $("#<%=ddlxilie.ClientID %>").html(ret.obj);
                            $("#<%=ddlxilie.ClientID %>").bind("change", pageDataJS.ajaxGetChuan)
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后重试！");
                    }
                })
            }, //获取游轮系列
            ajaxGetChuan: function() {
                $.ajax({
                    url: "HangQiAdd.aspx?sl=97&getCZ=1&gongsi=" + $("#<%=YLCompanySelect1.CompIDClient %>").val() + "&xilie=" + $("#<%=ddlxilie.ClientID %>").val() + "&sid=" + $("#<%=czbh.ClientID %>").val(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            $("#<%=ddlchuanzhi.ClientID %>").html(ret.obj);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后重试！");
                    }
                })
            } //获取船只列表
        }


        $(function() {
            $("#tablist1").autoAdd({ changeInput: $("#<%=txtxingchengtian.ClientID %>"), tempRowClass: "tempRow1", addCallBack: pageDataJs.AddRowCallBack, addButtonClass: "Xaddbtn", delButtonClass: "Xdelbtn" });
            $("#fujiachanpin").autoAdd({ tempRowClass: "tempRow2", addCallBack: pageDataJS.AddRowCallBack });
            $(".editbox").each(function() {
                pageDataJS.CreateEdit(this);
            })//
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJS.Save();
                } else {
                    return false;
                }
            })

        })
        window.onload = function() {

            if (pageDataJS.Params.id) {
                var i = 0;
                //初始化系列
                $.ajax({
                    url: "HangQiAdd.aspx?sl=97&getXL=1&gongsi=" + $("#<%=YLCompanySelect1.CompIDClient %>").val() + "&sid=" + $("#<%=xlbh.ClientID %>").val(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            $("#<%=ddlxilie.ClientID %>").html(ret.obj);
                            pageDataJS.ajaxGetChuan();
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后重试！");
                    }
                })//
            }
        }
        
        
        
    </script>

</asp:Content>

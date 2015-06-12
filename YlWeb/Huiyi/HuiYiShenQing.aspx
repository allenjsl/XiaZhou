<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYiShenQing.aspx.cs"
    Inherits="EyouSoft.YlWeb.YouLunMeet.HuiYiShenQing" MasterPageFile="~/MasterPage/Boxy.Master"
    Title="游轮会议" %>

<%@ MasterType VirtualPath="~/MasterPage/Boxy.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <link href="../css/style.css" rel="stylesheet" />

    <script src="/Js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../Js/foucs.js"></script>

    <script src="../Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="../Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../Js/gscx.js" type="text/javascript"></script>

    <script src="../Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="../Js/table-toolbar.js" type="text/javascript"></script>

    <div class="mainbox">
        <div class="basicT">
            您的位置：维诗达游轮 > 游轮会议</div>
        <div class="huiyi_box" style="min-height: 550px;">
            <div class="huiyi_nav">
                <ul class="fixed">
                    <li><a href="YouLunHuiYi.aspx">长江游轮会议</a></li>
                    <li><a href="/Huiyi/HuiYiAnLi.aspx?type=<%=Request.QueryString["type"] %>">会议案例精选</a></li>
                    <li><a href="/Huiyi/XiaoHuiYi.aspx?type=<%=Request.QueryString["type"] %>">小型会议接待</a></li>
                    <li><a href="/Huiyi/DaHuiYi.aspx?type=<%=Request.QueryString["type"] %>">大型会议包租</a></li>
                    <li><a href="/Huiyi/YouLunShangWu.aspx?type=<%=Request.QueryString["type"] %>">游轮商务服务</a></li>
                    <li><a href="/Huiyi/YouLunFeiYong.aspx?type=<%=Request.QueryString["type"] %>">游轮费用测算</a></li>
                    <li><a class="nav_on" href="/Huiyi/HuiYiShenQing.aspx?type=<%=Request.QueryString["type"] %>">
                        会议申请</a></li>
                </ul>
                <div class="huiyi_nav-R">
                    <a class="on" data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介 %>"
                        href="/Huiyi/HuiYiShenQing.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.长江游轮会议详介 %>">
                        长江游轮会议</a><a data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介 %>"
                            href="/Huiyi/HuiYiShenQing.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.海洋邮轮会议详介 %>">海洋游轮会议</a></div>
            </div>
            <div class="huiyi_casebox">
                <form id="form1" runat="server">
                <div class="shq_list fixed">
                    <div class="hy_shqT">
                        <h3>
                            您的需求</h3>
                    </div>
                    <div class="hy_shqR">
                        <ul>
                            <li>
                                <label>
                                    会议规模</label><input type="text" id="huiyi_guimo" name="huiyi_guimo" class="inputbk formsize270"
                                        value="例如：200人" ovalue="例如：200人" custom="InitSave.V1" onblur="this.value==''?this.value=this.ovalue:void 0;" />
                            </li>
                            <li>
                                <label>
                                    会议预计时间</label><input type="text" id="huiyi_shijian" name="huiyi_shijian" class="inputbk formsize270"
                                        value="例如：2014年3月" ovalue="例如：2014年3月" custom="InitSave.V1" onmousedown="WdatePicker();"
                                        onblur="this.value==''?this.value=this.ovalue:void 0;" />
                            </li>
                            <li>
                                <label>
                                    <input type="hidden" name="youlun_leixing" id="youlun_leixing" />
                                    游轮选择</label><a href="javascript:void 0;" id="cj_Radio" class="danxuan" onclick="this.className='danxuan_select';$('#youlun_leixing').val(0); $('#hy_Radio').attr('class','danxuan');"></a>&nbsp;长江游轮&nbsp;&nbsp;<a
                                        href="javascript:void 0;" id="hy_Radio" class="danxuan" onclick="this.className='danxuan_select';$('#youlun_leixing').val(1);$('#cj_Radio').attr('class','danxuan');"></a>&nbsp;海洋游轮</li>
                        </ul>
                    </div>
                </div>
                <div class="shq_list fixed">
                    <div class="hy_shqT">
                        <h3>
                            联系方式</h3>
                    </div>
                    <div class="hy_shqR">
                        <ul>
                            <li>
                                <label>
                                    姓名</label><input type="text" name="xingming" id="xingming" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li>
                                <label>
                                    手机号码</label><input type="text" id="shouji" name="shouji" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li>
                                <label>
                                    电子邮箱</label><input type="text" id="email" name="email" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li style="width: 99.9%;">
                                <label>
                                    联系地址</label><span>
                                        <select id="sel_province" name="sel_province" style="height: 25px;">
                                        </select>
                                        &nbsp;省&nbsp;</span> <span>
                                            <select id="sel_city" name="sel_city" style="height: 25px;">
                                            </select>
                                            &nbsp;市&nbsp;</span>
                                <input type="text" id="dizhi" name="dizhi" class="inputbk formsize370" /></li>
                        </ul>
                    </div>
                </div>
                <div class="shq_list fixed">
                    <div class="hy_shqT">
                        <h3>
                            行业信息</h3>
                        <span>填写此信息是为了我们能给您提供 更完善更适合的会议方案</span></div>
                    <div class="hy_shqR">
                        <ul>
                            <li>
                                <label>
                                    单位名称</label><input type="text" id="hangye_mingcheng" name="hangye_mingcheng" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li>
                                <label>
                                    您的行业</label><input type="text" id="hangye_job" name="hangye_job" class="inputbk formsize270"
                                        value="" />
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="hy_btnbox">
                    <a href="#" class="hy_btn" id="tijiao" onclick="InitSave.Submit();">提交申请</a></div>
                </form>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(function() {
            var type = '<%=Request.QueryString["type"] %>';
            $(".huiyi_nav-R").find("a").each(function() {
                if ($(this).attr("data-type") == type) {
                    $(this).addClass("on");
                } else {
                    $(this).removeClass("on");
                }
            });
            setNav(4);
        })

        var InitSave = {
            InitSelectFrom: function() {
                pcToobar.init({ pID: '#sel_province', cID: '#sel_city', comID: '<%=this.YuMingInfo.CompanyId %>' });
                this.bindRules();
            },
            V1: function(e) {
                return e.val() != e[0].ovalue;
            },
            checkForm: function() {
                return ValiDatorForm.validator($("#tijiao").closest("form").get(0), "alert");
            },

            bindRules: function() {
                $("#huiyi_guimo").attr({ valid: "required|custom", errmsg: "请填写会议规模！|请填写会议规模！" });
                $("#huiyi_shijian").attr({ valid: "required|custom|isDate", errmsg: "请填写会议预计时间！|请填写会议预计时间！|时间格式不正确！" });
                $('#youlun_leixing').attr({ valid: "required", errmsg: "请选择游轮类型！" });
                $("#xingming").attr({ valid: "required", errmsg: "请填写姓名！" });
                $("#email").attr({ valid: "required|isEmail", errmsg: "请填写email！|Email格式不正确！" });
                $("#shouji").attr({ valid: "required|isMobile", errmsg: "请您的手机号！|手机号格式不正确！" });
                $("#sel_province").attr({ valid: "required", errmsg: "请选择省份！" });
                $("#sel_city").attr({ valid: "required", errmsg: "请选择城市！" });
                $('#dizhi').attr({ valid: "required", errmsg: "请填写地址！" });
            },
            Submit: function() {
                //var _isLogin = iLogin.getM().isLogin;
                //if (!_isLogin) { tableToolbar._showMsg("会员未登录，进行登录！"); return; }
                if (this.checkForm()) {
                    $.newAjax({
                        type: "post",
                        url: "/HuiYi/HuiYiShenQing.aspx?dotype=shenqing",
                        dataType: "json",
                        data: $("#tijiao").closest("form").serialize(),
                        success: function(ret) {
                            if (ret.result == "1") {
                                tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = window.location.href;
                                });
                                /*$('#youlun_leixing,#xingming,#shouji,#email,select,#hangye_mingcheng,#hangye_job').val('');
                                $('#huiyi_guimo').val(function() { return this.ovalue; });
                                $('#huiyi_shijian').val(function() { return this.ovalue; });
                                $('#cj_Radio').attr('class', 'danxuan');
                                $('#hy_Radio').attr('class', 'danxuan');*/
                            }
                            else {
                                tableToolbar._showMsg(ret.msg);
                            }
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    });
                }
            },
            RadioListValue: function(name) {
                var list = $("input[name='" + name + "']:checked").val();
                return list;
            }
        };
        $(function() {
            ValiDatorForm.model = false;
            InitSave.InitSelectFrom();
        });
    </script>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuiYiShenQing.aspx.cs"
    Inherits="EyouSoft.YlWeb.YouLunMeet.HuiYiShenQing" MasterPageFile="~/MasterPage/Boxy.Master"
    Title="���ֻ���" %>

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
            ����λ�ã�άʫ������ > ���ֻ���</div>
        <div class="huiyi_box" style="min-height: 550px;">
            <div class="huiyi_nav">
                <ul class="fixed">
                    <li><a href="YouLunHuiYi.aspx">�������ֻ���</a></li>
                    <li><a href="/Huiyi/HuiYiAnLi.aspx?type=<%=Request.QueryString["type"] %>">���鰸����ѡ</a></li>
                    <li><a href="/Huiyi/XiaoHuiYi.aspx?type=<%=Request.QueryString["type"] %>">С�ͻ���Ӵ�</a></li>
                    <li><a href="/Huiyi/DaHuiYi.aspx?type=<%=Request.QueryString["type"] %>">���ͻ������</a></li>
                    <li><a href="/Huiyi/YouLunShangWu.aspx?type=<%=Request.QueryString["type"] %>">�����������</a></li>
                    <li><a href="/Huiyi/YouLunFeiYong.aspx?type=<%=Request.QueryString["type"] %>">���ַ��ò���</a></li>
                    <li><a class="nav_on" href="/Huiyi/HuiYiShenQing.aspx?type=<%=Request.QueryString["type"] %>">
                        ��������</a></li>
                </ul>
                <div class="huiyi_nav-R">
                    <a class="on" data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>"
                        href="/Huiyi/HuiYiShenQing.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>">
                        �������ֻ���</a><a data-type="<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>"
                            href="/Huiyi/HuiYiShenQing.aspx?type=<%=(int)EyouSoft.Model.EnumType.YlStructure.WzKvKey.�������ֻ������ %>">�������ֻ���</a></div>
            </div>
            <div class="huiyi_casebox">
                <form id="form1" runat="server">
                <div class="shq_list fixed">
                    <div class="hy_shqT">
                        <h3>
                            ��������</h3>
                    </div>
                    <div class="hy_shqR">
                        <ul>
                            <li>
                                <label>
                                    �����ģ</label><input type="text" id="huiyi_guimo" name="huiyi_guimo" class="inputbk formsize270"
                                        value="���磺200��" ovalue="���磺200��" custom="InitSave.V1" onblur="this.value==''?this.value=this.ovalue:void 0;" />
                            </li>
                            <li>
                                <label>
                                    ����Ԥ��ʱ��</label><input type="text" id="huiyi_shijian" name="huiyi_shijian" class="inputbk formsize270"
                                        value="���磺2014��3��" ovalue="���磺2014��3��" custom="InitSave.V1" onmousedown="WdatePicker();"
                                        onblur="this.value==''?this.value=this.ovalue:void 0;" />
                            </li>
                            <li>
                                <label>
                                    <input type="hidden" name="youlun_leixing" id="youlun_leixing" />
                                    ����ѡ��</label><a href="javascript:void 0;" id="cj_Radio" class="danxuan" onclick="this.className='danxuan_select';$('#youlun_leixing').val(0); $('#hy_Radio').attr('class','danxuan');"></a>&nbsp;��������&nbsp;&nbsp;<a
                                        href="javascript:void 0;" id="hy_Radio" class="danxuan" onclick="this.className='danxuan_select';$('#youlun_leixing').val(1);$('#cj_Radio').attr('class','danxuan');"></a>&nbsp;��������</li>
                        </ul>
                    </div>
                </div>
                <div class="shq_list fixed">
                    <div class="hy_shqT">
                        <h3>
                            ��ϵ��ʽ</h3>
                    </div>
                    <div class="hy_shqR">
                        <ul>
                            <li>
                                <label>
                                    ����</label><input type="text" name="xingming" id="xingming" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li>
                                <label>
                                    �ֻ�����</label><input type="text" id="shouji" name="shouji" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li>
                                <label>
                                    ��������</label><input type="text" id="email" name="email" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li style="width: 99.9%;">
                                <label>
                                    ��ϵ��ַ</label><span>
                                        <select id="sel_province" name="sel_province" style="height: 25px;">
                                        </select>
                                        &nbsp;ʡ&nbsp;</span> <span>
                                            <select id="sel_city" name="sel_city" style="height: 25px;">
                                            </select>
                                            &nbsp;��&nbsp;</span>
                                <input type="text" id="dizhi" name="dizhi" class="inputbk formsize370" /></li>
                        </ul>
                    </div>
                </div>
                <div class="shq_list fixed">
                    <div class="hy_shqT">
                        <h3>
                            ��ҵ��Ϣ</h3>
                        <span>��д����Ϣ��Ϊ�������ܸ����ṩ �����Ƹ��ʺϵĻ��鷽��</span></div>
                    <div class="hy_shqR">
                        <ul>
                            <li>
                                <label>
                                    ��λ����</label><input type="text" id="hangye_mingcheng" name="hangye_mingcheng" class="inputbk formsize270"
                                        value="" />
                            </li>
                            <li>
                                <label>
                                    ������ҵ</label><input type="text" id="hangye_job" name="hangye_job" class="inputbk formsize270"
                                        value="" />
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="hy_btnbox">
                    <a href="#" class="hy_btn" id="tijiao" onclick="InitSave.Submit();">�ύ����</a></div>
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
                $("#huiyi_guimo").attr({ valid: "required|custom", errmsg: "����д�����ģ��|����д�����ģ��" });
                $("#huiyi_shijian").attr({ valid: "required|custom|isDate", errmsg: "����д����Ԥ��ʱ�䣡|����д����Ԥ��ʱ�䣡|ʱ���ʽ����ȷ��" });
                $('#youlun_leixing').attr({ valid: "required", errmsg: "��ѡ���������ͣ�" });
                $("#xingming").attr({ valid: "required", errmsg: "����д������" });
                $("#email").attr({ valid: "required|isEmail", errmsg: "����дemail��|Email��ʽ����ȷ��" });
                $("#shouji").attr({ valid: "required|isMobile", errmsg: "�������ֻ��ţ�|�ֻ��Ÿ�ʽ����ȷ��" });
                $("#sel_province").attr({ valid: "required", errmsg: "��ѡ��ʡ�ݣ�" });
                $("#sel_city").attr({ valid: "required", errmsg: "��ѡ����У�" });
                $('#dizhi').attr({ valid: "required", errmsg: "����д��ַ��" });
            },
            Submit: function() {
                //var _isLogin = iLogin.getM().isLogin;
                //if (!_isLogin) { tableToolbar._showMsg("��Աδ��¼�����е�¼��"); return; }
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

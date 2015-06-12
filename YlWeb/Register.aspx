<%@ Page Title="会员注册" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Boxy.Master"
    CodeBehind="Register.aspx.cs" Inherits="EyouSoft.YlWeb.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PageHead" runat="server">

    <script src="Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="Js/table-toolbar.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <form runat="server" id="form1">
    <div class="mainbox">
        <div class="basicT">
            您的位置：维诗达游轮 &gt; 会员中心</div>
        <div class="user_box">
            <div class="reg_box" style="color: #858585">
                <h3>
                    新用户注册</h3>
                <h5>
                    请正确填写以下内容，以便您参与抽奖，各类优惠活动及预订信息查询。</h5>
                <div class="reg_L">
                    <ul class="login_form">
                        <li>&#12288;用户名<input  runat="server" name="txtUserName" maxlength="15" value="手机号码/qq号码/用户名"
                            id="txtUserName" class="formsize270 input_style" onblur="User.ckUserName();if($.trim($(this).val())==''){$(this).val('手机号码/qq号码/用户名');}" onfocus="if($.trim($(this).val())=='手机号码/qq号码/用户名'){$(this).val('');}" valid="required"
                            errmsg="请填写用户名！" /><font class="fontred">*</font></li>
                        <li>常用邮箱<input  runat="server" id="txtMail" name="txtMail" class="formsize270 input_style"
                            valid="required|isEmail" errmsg="请填写常用邮箱！|请正确填写常用邮箱！" /><font class="fontred">*</font></li>
                        <li>登录密码<input type="password" runat="server" id="txtPass1" name="txtPass1" class="formsize270 input_style"
                            maxlength="16" valid="required" errmsg="请填写登录密码！" /><font class="fontred">*</font></li>
                        <li>确认密码<input type="password" runat="server" id="txtPass2" maxlength="16" name="txtPass2"
                            class="formsize270 input_style" valid="required" errmsg="请填写确认密码！" /><font class="fontred">*</font></li>
                        <li class="login_txt">
                            <label style="width:auto; float:none;">
                                <input id="ViewCheck" type="checkbox" checked="checked" value="" />&#12288;我已仔细阅读并同意接受用户<a
                                    class="item" href="javascript:void(0)" id="zctk00">注册条款</a></label></li>
                        <li class="login_txt"><a id="btnSava" class="user_loginbtn" href="javascript:;">立即注册</a></li></ul>
                </div>
                <div class="reg_Rimg">
                    <asp:Literal runat="server" ID="ltrGuangGao"></asp:Literal></div>
                <div class="clear">
            </div>
            
           
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var User = {
            checkForm: function() {
                return ValiDatorForm.validator($("#btnSava").closest("form").get(0), "alert");
            },
            ckUserName: function() {
                var UserName = $.trim($('#<%=txtUserName.ClientID %>').val());
                var result = false;
                if (UserName != "") {
                    $.ajax({
                        type: "GET",
                        async: false,
                        dataType: 'json',
                        url: "/Register.aspx?re=ck&UserName=" + encodeURIComponent(UserName),
                        cache: false,
                        success: function(html) {
                            if (html.result != "1") {
                                tableToolbar._showMsg(html.msg);
                            } else {
                                result = true;
                            }
                        }
                    });
                } else {
                    tableToolbar._showMsg("请输入用户名!");
                }
                return result;
            },
            ckRePassWord: function() {
                var repassword = $.trim($("#<%=txtPass1.ClientID %>").val());
                var password = $.trim($("#<%=txtPass2.ClientID %>").val());
                if (repassword == "") {
                    tableToolbar._showMsg("请填写登录密码！");
                    return false;
                } else {
                    if (repassword != password) {
                        tableToolbar._showMsg("密码不一致！");
                        return false;
                    }
                    else {
                        return true;
                    }
                }

            },
            ckTiaoKuan:function(){
                var isck=$("#ViewCheck").attr("checked");
                if(!isck){
                    tableToolbar._showMsg("请阅读注册条款！");
                }
                return isck;
            },
            RegisterUser: function() {
                 
                if (this.checkForm()) {
                    if (this.ckUserName() && this.ckRePassWord()&&this.ckTiaoKuan()) {
                        $("#btnSava").val("注册中").unbind("click");
                        $.newAjax({
                            type: "post",
                            cache: false,
                            url: "/Register.aspx?re=reg",
                            dataType: "json",
                            data: $("#btnSava").closest("form").serialize(),
                            success: function(ret) {
                                if (ret.result == "1") {
                                    tableToolbar._showMsg(ret.msg, function() {
                                        location.href = "/Default.aspx";
                                    });
                                }
                                else {
                                    tableToolbar._showMsg(ret.msg);
                                    User.bindBtn();
                                }
                            },
                            error: function() {
                                tableToolbar._showMsg(tableToolbar.errorMsg);
                                User.bindBtn();
                            }
                        });
                    }
                }
            },
            bindBtn: function() {
                $("#btnSava").click(function() {
                    User.RegisterUser();
                });
            }
        }
        $(function() {
            User.bindBtn();
            $("#zctk00").click(function() {
                showZZ('<div class="user_boxy tiaokuan-box"><a href="javascript:void(0)" class="close_btn" id="zctk01"><em>X</em>关闭</a><h5>维诗达游轮网服务条款</h5><div class="tiaokuan-txt"><h5>1、服务条款的确认和接纳</h5><p>维诗达游轮网由湖北维诗达游轮管理有限公司运营。用户通过完成注册程序并点击一下“递交”的按钮，这表示用户明确知晓以上事实，并与维诗达游轮网达成协议并接受所有的服务条款。</p><h5>2、服务简介</h5><p>湖北维诗达游轮管理有限公司运用自己的操作系统通过国际互联网络为用户提供网络会员服务。用户必须： ⑴提供设备，包括个人电脑一台、调制解调器一个及配备上网装置。⑵个人上网和支付与此服务有关的电话费用。考虑到维诗达游轮网络会员服务的重要性，用户同意：⑴提供及时、详尽及准确的个人资料。⑵不断更新注册资料，符合及时、详尽准确的要求。所有原始键入的资料将引用为注册资料。另外，用户可授权湖北维诗达游轮管理有限公司向第三方透露其基本资料，但湖北维诗达游轮管理有限公司不能公开用户的补充资料。除非：⑴用户要求湖北维诗达游轮管理有限公司或授权某人通过电子邮件服务透露这些信息。⑵相应的法律要求及程序要求湖北维诗达游轮管理有限公司提供用户的个人资料。如果用户提供的资料不准确，湖北维诗达游轮管理有限公司保留结束用户使用维诗达游轮网会员服务的权利。用户在享用维诗达游轮样会员服务的同时，同意接受维诗达游轮网会员服务提供的各类信息服务。</p><h5>3、服务条款的修改</h5><p>湖北维诗达游轮管理有限公司会在必要时修改服务条款，维诗达游轮网会员服务条款一旦发生变动，公司将会在用户进入下一步使用前的页面提示修改内容。如果你同意改动，则按“我同意”按钮。如果你不接受，则及时取消你的用户使用服务资格。用户要继续使用维诗达游轮网会员服务需要两方面的确认：⑴首先确认维诗达游轮网会员服务条款及其变动。⑵同意接受所有的服务条款限制。</p><h5>4、服务修订</h5><p>湖北维诗达游轮管理有限公司保留随时修改或中断服务而不需知照用户的权利。湖北维诗达游轮管理有限公司行使修改或中断服务的权利，不需对用户或第三方负责。</p><h5>5、版权声明</h5><p>任何会员接受本协议条款，即表明该用户将其在本站发表的任何形式的信息的著作权，包括并不限于：复制权、发行权、出租权、展览权、表演权、放映权、广播权、信息网络传播权、摄制权、改编权、翻译权、汇编权以及应当由著作权人享有的其他可转让权利无偿独家转让给维诗达游轮网络运营商所有，同时表明该会员许可维诗达游轮网有权利就任何主体侵权而单独提起诉讼，并获得全部赔偿。本协议已经构成《著作权法》第二十五条所规定的书面协议，其效力及于用户在维诗达游轮网发布的任何受著作权法保护的作品内容，无论该内容形成于本协议签订前还是本协议签订后。会员同意并明确了解上述条款，不将已发表于本站的信息，以任何形式发布或授权其它网站及媒体（维诗达游轮网关联公司网站除外）使用。同时，维诗达游轮网保留删除站内各类不符合规定的信息而不通知会员的权利。湖北维诗达游轮管理有限公司是维诗达游轮网的运营商,拥有此网站内容及资源的版权,受国家知识产权保护,享有对本网站声明的解释与修改权；未经湖北维诗达游轮管理有限公司的明确书面许可,任何单位或个人不得以任何方式,以任何文字作全部和局部复制、转载、引用。否则本公司将追究其法律责任。维诗达游轮网网站页面所有信息受《中华人民共和国著作权法》及相关法律法规和中国加入的所有知识产权方面的国际条约的保护。我们制定了旨在保护知识产权权利人合法权益的相关措施和步骤，当著作权人和/或依法可以行使信息网络传播权的权利人（以下简称“权利人”）发现可能存有侵犯其信息网络传播权的情况，权利人应事先向本站发出书面权利通知，并提供有效权属证明，本站将根据相关法律规定采取措施删除相关内容。同时用户登陆携程旅行网提交相关评论、文章或其他信息通过维诗达游轮网编辑发布的，维诗达游轮网享有著作权，维诗达游轮网有权在本网站及关联公司网站使用用户在本网站上发表的信息（包括但不限于图片、游记等），但用户在本网站上发表的信息若其它浏览者要转载，需经过本网站和该用户的许可，否则视为侵权，其他任何第三人未经维诗达游轮网允许，不得以任何形式复制、转载或采用。因用户或其他第三人违反本法律声明而引发的任何一切索赔、损害等等，本网站概不负责。维诗达游轮网不保证为向用户提供便利而设置的外部链接的准确性和完整性，若由此而引起的版权问题或其他问题，请致电或电邮本网站。经核实后会立即予以删除或更改。</p><h5>6、用户隐私制度</h5><p>尊重用户个人隐私是湖北维诗达游轮管理有限公司的一项基本政策。所以，作为对以上第二点个人注册资料分析的补充，湖北维诗达游轮管理有限公司不会公开、编辑或透露用户的补充资料及保存在维诗达游轮网会员服务中的非公开内容，除非有法律许可要求或湖北维诗达游轮管理有限公司在诚信的基础上认为透露这些信件在以下三种情况是必要的：⑴遵守有关法律规定，遵从维诗达游轮网会员合法服务程序。⑵保持维护维诗达的商标所有权。⑶在紧急情况下竭力维护用户个人和社会大众的隐私安全。⑷符合其他相关的要求。</p><h5>7、用户的帐号，密码和安全性</h5><p>你一旦注册成功成为用户，你将得到一个密码和帐号。如果你不保管好自己的帐号和密码安全，将负全部责任。另外，每个用户都要对其帐户中的所有活动和事件负全责。你可随时改变你的密码和图标，也可以结束旧的帐户重开一个新帐户。用户若发现任何非法使用用户帐号或安全漏洞的情况，立即通告湖北维诗达游轮管理有限公司。</p><h5>8、拒绝提供担保</h5><p>用户明确同意邮件服务的使用由用户个人承担风险。湖北维诗达游轮管理有限公司明确表示不提供任何类型的担保，不论是明确的或隐含的，但是不对商业性的隐含担保，特定目的和不违反规定的适当担保作限制。湖北维诗达游轮管理有限公司不担保服务一定能满足用户的要求，也不担保服务不会受中断，对服务的及时性，安全性，出错发生都不作担保。湖北维诗达游轮管理有限公司拒绝提供任何担保，包括信息能否准确，及时，顺利的传送。用户理解并接受任何信息资料(下载或通过维诗达游轮网会员服务取得)，取决于用户自己并由其承担系统受损或资料丢失的所有风险和责任。用户不会从湖北维诗达游轮管理有限公司收到口头或书写的意见或信息，也不会在这里作明确担保。</p><h5>9、有限责任</h5><p>湖北维诗达游轮管理有限公司对任何直接、间接、偶然、特殊及继起的损害不负责任，这些损害来自：不正当使用网络会员服务，非法使用服务或用户传送的信息有所变动。这些损害会导致湖北维诗达游轮管理有限公司形象受损，所以湖北维诗达游轮管理有限公司早已提出这种损害的可能性。</p><h5>10、维诗达游轮网会员服务信息的储存及限制</h5><p>湖北维诗达游轮管理有限公司不对用户所发布信息的删除或储存失败负责。湖北维诗达游轮管理有限公司没有对信息的传输量规定上限，但是它有判定用户的行为是否符合携程网络会员服务条款的要求和精神的保留权利，如果用户违背了服务条款的规定，则中断其网络会员服务的帐号。本网站所有的文章版权归原文作者和湖北维诗达游轮管理有限公司共同所有，任何人需要转载网站内文章，必须征得原文作者或湖北维诗达游轮管理有限公司授权。</p><h5>11、用户管理</h5><p>用户单独承担发布内容的责任。用户对服务的使用是根据所有适用于网站服务的地方法律、国家法律和国际法律标准的。用户必须遵循：</p><p>⑴发布信息时必须符合中国有关法规。</p><p>⑵使用网络会员服务不作非法用途。</p><p>⑶不干扰或混乱网络服务。</p><p>⑷遵守所有使用网络会员服务的网络协议、规定和程序。网络会员服务是利用因特网发送和收取信息。所以，用户的行为指引是根据国家有关因特网的法规，政策和程序的。用户须承诺不传输任何非法的、骚扰性的、中伤他人的、辱骂性的、恐吓性的、伤害性的、庸俗的，淫秽等信息资料。另外，用户也不能传输任何教唆他人构成犯罪行为的资料；不能传输助长国内不利条件和涉及国家安全的资料；不能传输任何不符合当地法规、国家法律和国际法律的资料。未经许可而非法进入其它电脑系统是禁止的。若用户的行为不符合以上提到的服务条款，湖北维诗达游轮管理有限公司将作出独立判断立即取消用户服务帐号。用户需对自己在网络会员服务中的行为承担法律责任。用户若在网络会员服务中散布和传播反动、色情或其他违反国家法律的信息，网络会员服务的系统记录有可能作为用户违反法律的证据。</p><h5>12、保障</h5><p>用户同意保障和维护湖北维诗达游轮管理有限公司全体成员的利益，负责支付由用户使用超出服务范围引起的律师费用，违反服务条款的损害补偿费用，其它人使用用户的电脑、帐号和其它知识产权的追索费。</p><h5>13、结束服务</h5><p>用户或湖北维诗达游轮管理有限公司可随时根据实际情况中止网站服务。湖北维诗达游轮管理有限公司不需对任何个人或第三方负责而随时中断服务。用户若反对任何服务条款的建议或对后来的条款修改有异议，或对维诗达游轮网会员服务不满，用户只有以下的追索权：⑴不再使用维诗达游轮网会员服务。⑵结束用户使用维诗达游轮网会员服务的资格。⑶通告湖北维诗达游轮管理有限公司停止该用户的服务。结束用户服务后，用户使用维诗达游轮网会员服务的权利马上中止。从那时起，湖北维诗达游轮管理有限公司不再对用户承担任何义务。</p><h5>14、通告</h5><p>所有发给用户的通告都可通过电子邮件或常规的信件传送。湖北维诗达游轮管理有限公司会通过邮件服务发报消息给用户，告诉他们服务条款的修改、服务变更、或其它重要事情。</p><h5>15、参与广告策划</h5><p>用户可在他们发表的信息中加入宣传资料或参与广告策划，在维诗达游轮网会员服务免费服务上展示他们的产品。任何这类促销方法，包括运输货物、付款、服务、商业条件、担保及与广告有关的描述都只是在相应的用户和广告销售商之间发生。湖北维诗达游轮管理有限公司不承担任何责任，维诗达游轮网会员服务没有义务为这类广告销售负任何一部分的责任。</p><h5>16、邮件内容的所有权</h5><p>用户定义的内容包括：文字、软件、声音、相片、录象、图表；在广告中全部内容；电子邮件的全部内容；维诗达游轮网会员服务为用户提供的商业信息。所有这些内容均受版权、商标、标签和其它财产所有权法律的保护。所以，用户只能在湖北维诗达游轮管理有限公司和广告商授权下才能使用这些内容，而不能擅自复制、再造这些内容、或创造与内容有关的派生产品。</p><h5>17、法律</h5><p>邮件服务条款要与该国的国家法律解析一致，包括法律条款中有争议抵触的内容。用户和湖北维诗达游轮管理有限公司一致同意服从法院的管辖。若有任何服务条款与法律相抵触，那这些条款将按尽可能接近的方法重新解析，而其它条款则保持对用户产生法律效力和影响，因用户通过维诗达游轮网预订任何产品而导致的争议，将同意接受武汉法院的管辖。</p></div><div class="margin_T16" style="text-align: center;"><a href="javascript:void(0)" class="u-btn" id="zctk02">已阅读并同意条款</a></div></div>');
                $("#zctk01").click(function() { closeZZ(); });
                $("#zctk02").click(function() { $("#ViewCheck").attr("checked", "checked"); closeZZ(); });
            });
        });
        
        
    </script>

</asp:Content>

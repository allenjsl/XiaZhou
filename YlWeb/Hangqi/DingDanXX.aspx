﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DingDanXX.aspx.cs" Inherits="EyouSoft.YlWeb.Hangqi.DingDanXX"
    MasterPageFile="~/MasterPage/M1.Master" Title="订单信息" %>
<%@ MasterType VirtualPath="~/MasterPage/M1.Master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" runat="server">
    <div class="step_mainbox">
        <div class="step_T">
            <div class="basicT floatL">
                您的位置：维诗达游轮 > 在线预订</div>
            <div class="step_Rimg">
                <img src="../images/online_book.jpg" /></div>
            <div class="step_num">
                <ul>
                    <li>
                        <img src="../images/step01_1.jpg" /><span class="bfontblue">选择产品</span></li>
                    <li>
                        <img src="../images/step02_1.jpg" /><span class="bfontblue">核对与填写信息</span></li>
                    <li>
                        <img src="../images/step03_1.jpg" /><span class="bfontblue" style="text-indent: 79px;">客服审核</span></li>
                    <li>
                        <img src="../images/step04_1.jpg" /><span class="bfontblue" style="text-indent: 96px;">订单支付</span></li>
                    <li>
                        <img src="../images/step05_1.jpg" /><span class="bfontblue" style="text-indent: 107px;">预订成功</span></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="step_box1 margin_T16">
            <div class="basic_mainT_color">
                <h5>
                    预订成功</h5>
            </div>
            <div class="ddzf_yellbox yudin_ok">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <th align="left">
                            订单号：<asp:Label ID="dingdanhao" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;状态：<font
                                class="zht_fontgreen"><asp:Label ID="dingdanzhuangtai" runat="server" Text=""></asp:Label></font>
                        </th>
                        <th colspan="2" align="right">
                            <asp:Literal runat="server" ID="ltr01"><a href="javascript:void(0)" class="print-btn">打印订单</a> <a href="javascript:void(0)" class="print-btn">打印行程单</a></asp:Literal>
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td width="150" align="left">
                            产品金额
                        </td>
                        <td width="150" align="right">
                            <asp:Label ID="chanpinjine" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            附加产品金额
                        </td>
                        <td align="right">
                            <asp:Label ID="fujiachanpinjie" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            优惠金额
                        </td>
                        <td align="right">
                            <asp:Label ID="youhuijine" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr id="JiFenTr" runat="server">
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            使用积分
                        </td>
                        <td align="right">
                            <asp:Label ID="shiyongjifen" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            快递费用
                        </td>
                        <td align="right">
                            <asp:Label ID="kuaidifei" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="left">
                            实付金额
                        </td>
                        <td align="right">
                            <font class="font18 price_fontred">
                                <asp:Label ID="shifujine" runat="server" Text=""></asp:Label></font>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                订单信息</div>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="yudinTable_ok">
                <tr>
                    <th>
                        名称
                    </th>
                    <th>
                        出发日期
                    </th>
                    <th>
                        项目详情
                    </th>
                    <th>
                        附加产品
                    </th>
                </tr>
                <asp:Literal ID="dingdanxinxi" runat="server"></asp:Literal>
            </table>
        </div>
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                登船信息</div>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="yudinTable_ok">
                <tr>
                    <th>
                        登船时间
                    </th>
                    <th>
                        登船地点
                    </th>
                    <th>
                        离船地点
                    </th>
                    <th>
                        船上总服务台电话
                    </th>
                </tr>
                <asp:Literal ID="dengchuanxinxi" runat="server"></asp:Literal>
            </table>
        </div>
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                旅客信息</div>
            <div class="yudin_lvkebox">
                <div class="yudin_lvke">
                    <asp:Repeater ID="rptyoukes" runat="server" 
                        onitemdatabound="rptyoukes_ItemDataBound">
                        <ItemTemplate>
                            <div class="lvke_box">
                                <div class="L_jiao">
                                </div>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="center" valign="middle" class="leftT">
                                            <h3>
                                                旅客<%# Container.ItemIndex+1%></h3>
                                            <span class="chenren">
                                                <%#getGYSName(Eval("LeiXingId").ToString())%></span>
                                        </td>
                                        <td>
                                            <div class="lvke_Rbox">
                                                <ul class="lvke_form">
                                                    <li>
                                                        <label>
                                                            姓名：</label><span><%# Eval("XingMing")%>&nbsp;</span></li>
                                                    <li>
                                                        <label>
                                                            证件类型：</label><span><%# Eval("ZhengJianLeiXing")%>&nbsp;</span></li>
                                                    <li>
                                                        <label>
                                                            证件号码：</label><span><%# Eval("ZhengJianHaoMa")%>&nbsp;</span></li>
                                                    <li>
                                                        <label>
                                                            证件有效期：</label><span><asp:Literal runat="server" ID="ltrYK_ZJYXQ">&nbsp;</asp:Literal></span></li>
                                                    <li>
                                                        <label>
                                                            性别：</label><span><asp:Literal runat="server" ID="ltrYK_XB">&nbsp;</asp:Literal></span></li>
                                                    <li>
                                                        <label>
                                                            出生日期：</label><span><asp:Literal runat="server" ID="ltrYK_CSRQ">&nbsp;</asp:Literal></span></li>
                                                    <li>
                                                        <label>
                                                            联系电话：</label><span><%# Eval("ShouJi")%>&nbsp;</span></li>
                                                    <li style="text-align: right; color: #bbb;">如需修改，请联系客服</li>
                                                </ul>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                预订人信息</div>
            <div class="yudin_cont fixed">
                <ul>
                    <li><b>姓名：</b><asp:Label ID="yudingren" runat="server" Text=""></asp:Label></li>
                    <li><b>电子邮件：</b><asp:Label ID="yudingyoujian" runat="server" Text=""></asp:Label>
                    </li>
                    <li><b>手机号码：</b><asp:Label ID="yudingshouji" runat="server" Text=""></asp:Label></li>
                    <li><b>下单备注：</b><asp:Label ID="xiadanbeizhu" runat="server" Text=""></asp:Label></li>
                </ul>
            </div>
        </div>
        
        <asp:PlaceHolder runat="server" ID="phFaPiao">
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                发票信息</div>
            <div class="yudin_cont">
                <p>
                    <b>发票抬头:</b><asp:Label ID="fapiaotaitou" runat="server" Text=""></asp:Label></p>
                <p>
                    <b>发票明细:</b><asp:Label ID="fapiaomingxi" runat="server" Text=""></asp:Label></p>
            </div>
        </div>
        </asp:PlaceHolder>
        
        <asp:PlaceHolder runat="server" ID="phFaPiaoPeiSongFangShi">
        <div class="yudin_ok_box">
            <div class="yudin_Title">
                配送方式</div>
            <div class="yudin_cont">
                <p>
                    <asp:Label ID="peisongfangshi" runat="server" Text=""></asp:Label></p>
            </div>
        </div>
        </asp:PlaceHolder>
        
        <div class="yudin_ok_box" style="margin-top: 16px;">
            <div class="yudin_Title">
                旅客须知</div>
            <div class="hetong_box">
                <div class="hetong_txt">
                    <div>
                        <asp:Literal runat="server" ID="ltrLvKeXuZhi">
                        亲爱的游客：<br />
                        您好！为了让您能更好的享受涉外豪华游船给您带来的舒适享受，也为了避免不必要的争端产生，请您仔细阅读以下旅游注意事项，谢谢支持！<br />
                        一、安全事项<br />
                        1、长江三峡旅游线路主要以船为载体，大部分时间都会在游船上面度过，因游船自身空间相对较小，上下楼梯狭窄，甲板湿滑，所以请所有游客在游船上面走动或观景的时候特别注意安全，同时不要翻越游船护栏照相、看景点，有小孩的游客请照看好自己的小孩，不要让他们在游船上面嬉戏追赶，随行父母应特别注意各处安全隐患，防范于未然<br />
                        2、游船装载人数较多，游船停靠码头上下船通过跳板或趸船的时候请不要拥挤，按次序依次下船，注意安全，以免落水，如您前面有老人和小孩，请发扬尊老爱幼的传统美德<br />
                        3、上船后请客人注意防火防盗并保管好自己的行李物品，以免丢失，如有丢失，请及时报警<br />
                        二、游轮费用<br />
                        &nbsp;&nbsp;1、船票费用包含乘船期间的住宿、用餐（不含登船当天的晚餐）、行程指定的上岸景点参观费用。不含船上一切个人消费，如：酒水、洗衣、电话、美容美发、购物、自费上岸参观景点等费用。<br />
                        &nbsp;&nbsp;2、游船上正常舱位为2人间，将以一人一个床位为单位进行销售，如团队男女比例产生自然单差，游船公司会提前同其他团队客人进行调配，如客人特殊需求可提前申请一人单独使用一个标准房间，价格另议！<br />
                        &nbsp;&nbsp;3、船票的收费与机票类似，按年龄段收取，一般规则如下：2岁以下的婴儿，不单独使用床位，按照全额船票的10%收费；2-12岁儿童根据客人要求是否占床将收取50-100%不等的费用（根据游船不同收费标准存在一定差异），12岁及以上按成人结算。如您带有小孩随行，请一定提前告知报名旅行社，切勿隐瞒，以免给您的旅途带来不便<br />
                        &nbsp;&nbsp;4、因国籍不同，船票的销售价格可能会出现差异，请出发前予以说明，切勿隐瞒，我司确认的船位仅供确认书中指定的国籍的客人使用。若乘船的客人非确认书中指定的国籍，我司将根据客人的实际国籍增收或退还船票差价。如客人故意隐瞒个人身份，在游船前台登记时被查出国籍不同，将按照游船公布价格在前台补齐差额（可能会比结算价格高）。<br />
                        三、购物与自费项目<br />
                        1、行程中如安排的有购物店，游客可根据个人喜好挑选物品。进店购物需要一定时间，未进店客人可于店外自行活动，自由活动期间司机、导游可能无法提供服务敬请客人谅解。<br />
                        2、行程中所列自选景点和自理项目均由导游推荐参加，自愿选择不强迫，行程所列自费景点将根据当天船上报名人数而定，人多则停靠，人少则不停靠。未报名参加的客人可在码头附近自由活动并按照规定的时间返回船上即可，自由活动期间，车船将停止工作，不便之处敬请谅解。<br />
                        3、景区内的小商小贩所兜售的物品请客人谨慎选择，不买请不要随便触摸和试用，购买请先谈好价格，以免给自己带来不必要的麻烦。<br />
                        4、游客在旅游期间，旅行社不提倡强制消费和其他加点，行程中导游提出加点游客可以直接拒绝（导游如有强制消费和强迫加点行为请立即反应到我社），游客的自主购物及各种自费活动均属个人行为，由此产生的纠纷或连带关系游客自负，即买卖自由，风险自担。<br />
                        四、取消船位的违约金<br />
                        • 如果想取消船位，要立即书面通知我司，我司会于收到书面通知日起才正式取消订位，但必须支付下列的取消违约金：&nbsp;<br />
                        • 1、在开航之日前10天取消船位，须支付30%全额船票的违约金。<br />
                        • 2、在开航之日前6天取消船位，须支付50%全额船票的违约金。&nbsp;<br />
                        • 3、在开航之日前3天内取消船位，须支付100%全额船票的违约金。&nbsp;&nbsp;&nbsp;&nbsp;<br />
                        • 4、取消船位包括取消全部或部分船位，变更船名或航期。&nbsp;<br />
                        • 5、黄金周期间为买断舱位，取消船位须支付100%全额船票的违约金。<br />
                        五、特别说明<br />
                        1、游船经停景点的游览都将视为一个整体，用车将有游船统一调度，非局部单团独立用车；客人下船游览的各个景点都将有景区内导游负责接待讲解，下船后请紧跟景区导游，先听取讲解，后景区内自由游览或购买个人物品<br />
                        2、涉外豪华游轮实行一票制，船票包含内容为包装特惠销售，团队中持有老年证等其他相关证件的特殊人群在景点不再享受其他优惠，行程中客人放弃景点游览或不用餐不退还任何费用<br />
                        3、游船航程期间可能会举行一些见面会及行程说明会等活动，以当天航次实际公布为准。<br />
                        4、出发前请提醒客人带好个人有效身份证件/户口簿（儿童），以便办理登船入住手续及临检。<br />
                        六、特殊情况的处理<br />
                        1、因气候、水位、临时禁航和航道管制等无法控制的原因，船公司将据实际情况调整游船的始发港和终到港，并用其它交通工具（大巴）将旅客接送至游船始发站和终点站；可能调整乘船期间上岸参观的景点；<br />
                        2、三峡坝区及双线五级船闸是国防保护重点工程，属军管区，受行政管制、安全隐患、船闸设施设备检修、冲沙、临时突发问题、天气原因（特大雾）等影响，为确保三峡河段航运安全，缓解坝区通航压力，三峡通航管理局可能临时采取关闭双向或单向船闸等措施。届时将由游船公司提供旅行车将全船游客接驳至紧邻三峡大坝上游的港口或下游的宜昌港上下船（1小时高速）。<br />
                        3、遇夏季洪峰或秋冬枯水季节，轮船吨位过大而水位偏高或过低时，为确保安全，游轮将统一安排旅行车接驳全船客人自重庆市区前往涪陵港或者丰都港登船或自涪陵港（丰都港）离船前往重庆市区（1.5小时车/3小时车）<br />
                        4、若游船发生临时机械故障，为确保安全航行，船公司将调整航期或由指定安排其它游船接转。我司不承担其它的责任或赔偿。&nbsp;<br />
                        5、如因不可抗拒的外力所扰，无法提供原本保证的服务项目时，我司不负担任何责任或赔偿，这种不寻常的情况是指：战争的威胁，恐怖份子活动、自然灾害、火灾、天灾、气候、瘟疫的威胁、传染疾病和所有类似不可抗力。<br />
                        七、其他事项<br />
                        &nbsp;长江是我们的母亲河，请在旅游途中文明自己的个人行为，不要将果皮及相应的生活垃圾丢入长江，保护好我们的母亲河（违者罚款）
                        </asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:content>

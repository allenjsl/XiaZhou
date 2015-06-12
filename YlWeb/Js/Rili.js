/*	dynamicCSS.js v1.0 <http://www.bobbyvandersluis.com/articles/dynamicCSS.php>
Copyright 2005 Bobby van der Sluis
This software is licensed under the CC-GNU LGPL <http://creativecommons.org/licenses/LGPL/2.1/>
*/
/* Cross-browser dynamic CSS creation
- Based on Bobby van der Sluis' solution: http://www.bobbyvandersluis.com/articles/dynamicCSS.php
*/
function createStyleRule(selector, declaration) {
    if (!document.getElementsByTagName || !(document.createElement || document.createElementNS)) return;
    var agt = navigator.userAgent.toLowerCase();
    var is_ie = ((agt.indexOf("msie") != -1) && (agt.indexOf("opera") == -1));
    var is_iewin = (is_ie && (agt.indexOf("win") != -1));
    var is_iemac = (is_ie && (agt.indexOf("mac") != -1));
    if (is_iemac) return; // script doesn't work properly in IE/Mac
    var head = document.getElementsByTagName("head")[0];
    var style = (typeof document.createElementNS != "undefined") ? document.createElementNS("http://www.w3.org/1999/xhtml", "style") : document.createElement("style");
    if (!is_iewin) {
        var styleRule = document.createTextNode(selector + " {" + declaration + "}");
        style.appendChild(styleRule); // bugs in IE/Win
    }
    style.setAttribute("type", "text/css");
    style.setAttribute("media", "screen");
    head.appendChild(style);
    if (is_iewin && document.styleSheets && document.styleSheets.length > 0) {
        var lastStyle = document.styleSheets[document.styleSheets.length - 1];
        if (typeof lastStyle.addRule == "object") { // bugs in IE/Mac and Safari
            lastStyle.addRule(selector, declaration);
        }
    }
}
function copyDate(date) {
    var d = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    //	d.setYear(date.getFullYear());
    //	d.setMonth(date.getMonth());
    //	d.setDate(date.getDate());
    return d;
}
function formatDate(date) {
    var d = new Date(date.getFullYear(), date.getMonth(), 1);
    //	d.setYear(date.getFullYear());
    //	d.setMonth(date.getMonth());
    //	d.setDate(1);
    return d;
}
function compareDate(DateOne, DateTwo) {
    var OneMonth = DateOne.getMonth() + 1;
    var OneDay = DateOne.getDate();
    var OneYear = DateOne.getFullYear();

    var TwoMonth = DateTwo.getMonth() + 1;
    var TwoDay = DateTwo.getDate();
    var TwoYear = DateTwo.getFullYear();

    if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) >
	Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
        return 1;
    }
    else if (Date.parse(OneMonth + "/" + OneDay + "/" + OneYear) <
	Date.parse(TwoMonth + "/" + TwoDay + "/" + TwoYear)) {
        return -1;
    } else {
        return 0;
    }
}
function jsonDateToDateTime(jsonDate) {
    var reg = /\/Date\((-?[0-9]+)(\+[0-9]+)?\)\//g;
    if (reg.test(jsonDate)) {
        var ticks = Number(jsonDate.replace(reg, '$1'));
        if (!isNaN(ticks)) {
            jsonDate = new Date(Number(jsonDate.replace(reg, '$1')));
        } else {
            jsonDate = new Date();
        }
    }
    return jsonDate;
}

var QGD = {
    //个性化设置
    personalize: {},
    maxMonth: 5,
    setPersonalize: function() {
        if (this.personalize.mbkgurl != '') {
            $(".mbkgurl").css({ 'background': 'url("' + this.personalize.mbkgurl + '")', 'border': '1px solid #FF9342', 'line-height': '30px', 'font-size': '14px' })
        }
    },
    /*外部调用方法 */
    //初始化日历
    initCalendar: function(option) {
        this.option = $.extend({
            containerId: "calendarContainer",
            areatype: "1"
        }, option || {});
        if (this.option.maxMonth != "")
            this.maxMonth = this.option.maxMonth;
        this.currentDate = this.option.currentDate;
        this.maxDate = copyDate(this.option.currentDate);
        this.minDate = this.option.minDate;

        this.currentDate = formatDate(this.currentDate);
        this.maxDate = formatDate(this.maxDate);

        //个性化设置 mbkgurl:月份背景图片 wbkgc:星期背景颜色 dbkgc:日期背景颜色 dptext:价格显示字符串
        this.personalize = $.extend({ mbkgurl: '', wbkgc: '#d9d9d9', dbkgc: '#9be38e', dptext: '{0}元' }, option.personalize || {});

        //        if (this.option.areatype == "1")   // 国际线
        //        {
        this.maxDate.setMonth(this.maxDate.getMonth() + this.maxMonth);
        //        } else {
        //            this.maxDate.setMonth(this.maxDate.getMonth() + 1);
        //            $("#trSixMonth").hide();
        //        }
        this.SCD = this.option.firstMonthDate;
        this.SND = this.option.nextMonthDate;

        this.SCD = formatDate(this.SCD);
        this.SND = formatDate(this.SND);

        this._initCalendarBasic();
        this._createCalendarContainer();
        //初始化当前月日历
        this.createCalendarMonth(false);
        this.createCalendarDays(false);

        //初始化下月日历
        this.createCalendarMonth(true);
        this.createCalendarDays(true);

        //创建空行
        this._createEmptyRows();

        //绑定订单事件
        var self = this;
        $("#" + this.option.containerId).find("a[rel='addorder']").click(function() {
            if (AddOrder) {
                AddOrder($(this).attr("id"));
            }
            return false;
        });

        this.MouseoutOrOnMessage();

        //个性化设置
        this.setPersonalize();
    },
    updateCalendar: function(option) {
        this.option = $.extend(this.option, option || {});

        this.config.CR = 0;
        this.config.NR = 0;

        this.SCD = this.option.firstMonthDate;
        this.SND = this.option.nextMonthDate;

        this._initCalendarBasic();
        this._createCalendarContainer();

        //初始化当前月日历
        this.createCalendarMonth(false);
        this.createCalendarDays(false);

        //初始化下月日历
        this.createCalendarMonth(true);
        this.createCalendarDays(true);

        //创建空行
        this._createEmptyRows();

        //绑定订单事件
        var self = this;
        $("#" + this.option.containerId).find("a[rel='addorder']").click(function() {
            if (AddOrder) {
                AddOrder($(this).attr("id"));
            }
            return false;
        });

        this.MouseoutOrOnMessage();

    },
    /*以下为内部私有方法 */

    html: '<div style="margin-right:11px;" class="rili_box">' +
    '<div class="rili_Tbox"><table width="100%" border="0" align="left" cellspacing="0" cellpadding="0">' +
    '<tbody><tr><td height="45"><h3>请选择出发日期</h3></td><td>&nbsp;</td><td width="53"><a href="javascript:void(0);" id="linkPreMonth"><img src="../images/rili_left.jpg"></a></td><td class="month_t" id="titlePreMonth"></td></tr></tbody>' +
    '</table></div><div class="rili_Wbox" id="thisMonthCalendar"></div>' +
    '<div class="rili_Dbox" id="thisMonthCalendar1"></div>' +
    '</div>' +
    '<div class="rili_box">' +
    '<div class="rili_Tbox">' +
    '<table width="100%" border="0" align="left" cellspacing="0" cellpadding="0">' +
    '<tbody><tr><td class="month_t" id="titleNextMonth"></td><td width="53">' +
    '<a href="javascript:void(0);" id="linkNextMonth" >' +
    '<img src="../images/rili_right.jpg"></a></td><td width="45"></td><td></td></tr></tbody></table>' +
    '</div><div class="rili_Wbox" id="nextMonthCalendar"></div>' +
    '<div class="rili_Dbox" id="nextMonthCalendar1"></div>' +
    '</div> ',
    p: parent,
    //页面元素配置信息
    elements: {
        //生成的当前月表格id
        cMTable: 'cMonthTable',
        //生成的下一个月表格id
        nMTable: 'nMonthTable',
        //存放当前月表Title
        cMTd: "thisMonthCalendar",
        //存放下一个月表格Title
        nMTd: "nextMonthCalendar",
        //存放当前月表格容器id
        cNTd: "thisMonthCalendar1",
        //存放下一个月表格容器id
        nNTd: "nextMonthCalendar1"

    },
    maxDate: new Date(),
    //currentDate：起始日期 minDate：最小日期
    currentDate: new Date(), minDate: new Date(),
    //显示的第一个月的日期
    SCD: new Date(),
    //服务器下一个月日期
    SND: new Date(),
    //日历基础数据
    SDConfig: { CY: 2010, CM: 1, CD: 1, CDays: 31, NY: 2010, NM: 2, NDays: 28 },
    //配置信息
    config: {
        //当前月行数
        CR: 0,
        //下一个月行数
        NR: 0,
        //当前月1号在表格内td的index
        CI: 0,
        //下一个月1号在表格内td的index
        NI: 0,
        //模板团编号
        TI: 0,
        //当前子团信息
        CC: [],
        //父页面盛放子团的jQuery对象
        JPO: null,
        //日历当前月表格jQuery对象
        JCO: null,
        //日历下个月表格jQuery对象
        JNO: null,
        //是否登录
        isLogin: false,
        //子团集合
        Childrens: [],
        //端口(日历上的查看详细报价文件在网站首页项目中)
        stringPort: ""
    },
    //初始化日历基础数据
    _initCalendarBasic: function() {
        this.SDConfig.CY = this.SCD.getFullYear();
        this.SDConfig.CM = this.SCD.getMonth() + 1;
        this.SDConfig.CD = this.SCD.getDate();
        this.SDConfig.CDays = new Date(this.SDConfig.CY, this.SDConfig.CM, 0).getDate();

        this.SDConfig.NY = this.SND.getFullYear();
        this.SDConfig.NM = this.SND.getMonth() + 1;
        this.SDConfig.NDays = new Date(this.SDConfig.NY, this.SDConfig.NM, 0).getDate();
    },
    /*private*/
    _createCalendarContainer: function() {
        if (!document.getElementById(this.option.containerId)) {
            //alert("请指定日历容器");
            return false;
        }

        if (this.firstLoad == undefined || this.firstLoad == false) {
            $("#" + this.option.containerId).html(this.html);
            this.firstLoad = true;
        }
        $("#titlePreMonth").html(this.SDConfig.CY + "年&nbsp;" + "<em>" + this.SDConfig.CM + "月</em>");
        $("#titleNextMonth").html(this.SDConfig.NY + "年&nbsp;" + "<em>" + this.SDConfig.NM + "月</em>");

        $("#linkPreMonth").html("<img src=\"../images/rili_left.jpg\">");
        $("#linkNextMonth").html("<img src=\"../images/rili_right.jpg\">");

        var self = this;
        var cDate = copyDate(self.SCD);
        var nDate = copyDate(self.SND);
        var result = compareDate(self.SCD, this.minDate);
        if (result == -1 || result == 0) {
            $("#linkPreMonth").css("visibility", "hidden");
        } else {
            $("#linkPreMonth").css("visibility", "visible").unbind().click(function() {
                var a = copyDate(cDate);
                cDate.setMonth(cDate.getMonth() - 1);
                var b = copyDate(cDate);
                self.updateCalendar({
                    nextMonthDate: a,
                    firstMonthDate: b
                });

                if (typeof (self.option.prevfn) == "function") {
                    self.option.prevfn();
                }
                iPage.SetDateInfo();
            });
        }
        result = compareDate(this.SND, this.maxDate);
        if (result == 1 || result == 0) {
            $("#linkNextMonth").css("visibility", "hidden");
        } else {
            $("#linkNextMonth").css("visibility", "visible").unbind().click(function() {
                var a = copyDate(nDate);
                nDate.setMonth(nDate.getMonth() + 1)
                var b = copyDate(nDate);
                self.updateCalendar({
                    firstMonthDate: a,
                    nextMonthDate: b
                });
                if (typeof (self.option.nextfn) == "function") {
                    self.option.nextfn();
                }
                iPage.SetDateInfo();
            });
        }
    },
    //创建日历表格
    createCalendarMonth: function(isNextMonth) {
        var myself = this;
        var tableId = isNextMonth ? this.elements.nMTable : this.elements.cMTable;
        var s = [];
        s.push('<table width="100%" border="0" align="left" cellspacing="0" cellpadding="0" id="'
         + tableId + '">');

        s.push('<tr><td class="bg2"><label for="' + isNextMonth + '0">星期天</label></td><td width="5"></td><td class="bg1"><label for="' + isNextMonth + '1">星期一</label></td><td width="5"></td><td class="bg1"><label for="' + isNextMonth + '2">星期二</label></td><td width="5"></td><td class="bg1"><label for="' + isNextMonth + '3">星期三</label></td><td width="5"></td><td class="bg1"><label for="' + isNextMonth + '4">星期四</label></td><td width="5"></td><td class="bg1"><label for="' + isNextMonth + '5">星期五</label></td><td width="5"></td><td class="bg2"><label for="' + isNextMonth + '6">星期六</label></td></tr>');
        s.push('</table>');
        $("#" + (isNextMonth ? this.elements.nMTd : this.elements.cMTd)).html(s.join(''));

        if (isNextMonth) {
            this.config.JNO = $("#" + this.elements.nNTd);
        } else {
            this.config.JCO = $("#" + this.elements.cNTd);
        }

        var obj = isNextMonth ? this.config.JNO : this.config.JCO;

    },
    //创建前面的空白天数
    createStartEmptyDays: function(monthFirstDayOfWeek) {
        var s = [];
        for (var i = 0; i < monthFirstDayOfWeek; i++) {
            s.push('<td height="60px" bgcolor="#ffffff" align="center"></td>')
        }
        return s.join('');
    },
    //创建空行
    _createEmptyRows: function() {
        var s = '<tr ><td class="bg2">&nbsp;</td><td class="bg2">&nbsp;</td><td class="bg2"></td><td class="bg2"></td><td class="bg2"></td><td class="bg2"></td><td class="bg2"></td></tr>';
        if (this.config.CR > this.config.NR) {
            for (var i = 0; i < this.config.CR - this.config.NR; i++) {
                var jn = $("#" + this.config.JNO.attr("id") + "_tal");
                jn.append(s);
            }
        } else {
            for (var i = 0; i < this.config.NR - this.config.CR; i++) {
                var jc = $("#" + this.config.JCO.attr("id") + "_tal");
                jc.append(s);
            }
        }
    },
    //创建后面的空白天数
    createEndEmptyDays: function(days) {
        if (days == 7) return;
        var s = [];
        for (var i = 0; i < days; i++) {
            s.push('<td></td>')
        }
        s.push('</tr>')
        return s.join('');
    },
    //创建日历日期信息
    createCalendarDays: function(isNextMonth) {
        var myself = this;
        var obj = isNextMonth ? this.config.JNO : this.config.JCO;
        var name = obj
        var sd = 1;
        var fd = isNextMonth ? this.SDConfig.NDays : this.SDConfig.CDays;
        var sdOfWeek = isNextMonth ? new Date(this.SDConfig.NY, this.SDConfig.NM - 1, 1).getDay() : new Date(this.SDConfig.CY, this.SDConfig.CM - 1, 1).getDay();
        var s = [];
        var i = 1;
        var isCurrentMonth = false;
        if (this.SDConfig.CY == this.currentDate.getFullYear()
			&& this.SDConfig.CM == (this.currentDate.getMonth() + 1)
			&& isNextMonth == false) {
            isCurrentMonth = true;
        }


        s.push('<table width="100%" id="' + obj.attr("id") + '_tal" border="0" cellspacing="5" cellpadding="0"><tbody><tr>');
        do {
            if ((i) % (7) == 1) {

                if (isNextMonth) {
                    this.config.NR++;
                } else {
                    this.config.CR++;
                }
                var inputid = isNextMonth ? 'cw_' + sd : 'nw_' + sd;

            }

            if (i == 1) {
                s.push(this.createStartEmptyDays(sdOfWeek));
                i = i + sdOfWeek;
            }

            var thisY = isNextMonth ? this.SDConfig.NY : this.SDConfig.CY;
            var thisM = isNextMonth ? this.SDConfig.NM : this.SDConfig.CM;
            if (thisM <= 9) {
                thisM = "0" + thisM;
            }
            var thisD = sd <= 9 ? "0" + sd : sd;
            var thisDate = thisY + "-" + thisM + "-" + thisD;
            var tdTourstr = '<td id=' + thisDate + '><span class="num">' + sd + '</span></td>';
            for (var j = 0; j < this.config.Childrens.length; j++) {

                var RemnantNumber = this.config.Childrens[j]["MoreThan"];
                var TourState = this.Tour[j]["Status"];
                var strColor = this.personalize.dbkgc; //"#9be38e";
                //停收/客满 颜色
                if (TourState == 1) {
                    strColor = "#dceaef";
                }
                var strPriceList = "";
                //显示信息
                var Price = this.config.Childrens[j]["RetailAdultPrice"];
                var displayPrice = this.personalize.dptext.replace('{0}', Price);

                if (this.config.isLogin == "True" || this.config.isLogin == true) {
                    Price = this.config.Childrens[j]["SettlementAudltPrice"];
                }
                var Month = this.SDConfig.CM;
                if (isNextMonth) {
                    Month = this.SDConfig.NM;
                }

                var strleaveDate = Month + "," + sd;
                var stringPrice = '<span state=' + TourState + ' >' + sd + '</span><br/><a target="_blank" style="color:#FF6C00; font-size:14px;font-weight:bold;" title="预定此团队" href="javascript:void(0);" id="' + TourId + '" rel="addorder">剩' + RemnantNumber + '</a><br /><a style="color:#000;" rel="price" href="javascript:void(0);" tourid="' + TourId + '" leaveDate=' + strleaveDate + ' RemnantNumber=' + RemnantNumber + ' title="">' + displayPrice + '</a><div style="display:none"></div>';
                //停收
                if (TourState == 1) {//|| TourState == 3
                    stringPrice = '<font class="sizdate" state=' + TourState + '>' + sd + '</font><br/><font style="font-size:14px;font-weight:bold;">停收</font><br /><span state=' + TourState + '>' + displayPrice + '</span>'
                }
                //客满
                if (TourState == 1) {//|| TourState == 4
                    stringPrice = '<font class="sizdate" state=' + TourState + '>' + sd + '</font><br/><font style="font-size:14px;font-weight:bold;">客满</font><br /><span state=' + TourState + '>' + displayPrice + '</span>'
                }
                tdTourstr = '<td   nowrap="nowrap" height="60px" "' + strPriceList + '" bgcolor="' + strColor + '" align="center">' + stringPrice + '</td>';

            }
            s.push(tdTourstr);

            if ((i) % (7) == 0) { s.push('</tr>'); }

            if (isNextMonth) {
                this.config.NI = 9 + sdOfWeek;
            } else {
                this.config.CI = 9 + sdOfWeek;
            }

            sd++;
            i++
        }
        while (sd <= fd)
        {
            s.push(this.createEndEmptyDays(7 - (i - 1) % 7));
        }
        s.push('</tbody></table>');
        obj.html("");
        obj.append(s.join(''));


    },
    //查看子团的报价明细
    MouseoutOrOnMessage: function() {
        var stringPort = this.config.stringPort;
        var self = this;
        if (this.config.isLogin == "True") {
            $("#" + this.option.containerId).find("a[rel='price']").mouseover(function() {

                var oLink = this;
                $(oLink).addClass("show");
                var html = $(oLink).siblings("div").eq(0).html();
                if (html != "") {
                    var classN = $(oLink).attr("class");
                    if (classN == "show") {
                        wsug(oLink, html);
                    }
                } else {
                    var tourId = $(oLink).attr("tourid");
                    var RemnantNumber = $(oLink).attr("RemnantNumber");
                    var leaveDate = $(oLink).attr("leaveDate");
                    var strUrl = stringPort + "/TourManage/GetTourPriceInfo.ashx" + "?TourId=" + tourId + "&callback=?" + "&RemnantNumber=" + RemnantNumber + "&leaveDate=" + leaveDate;
                    var classN = $(oLink).attr("class");

                    if (classN == "show") {
                        $.ajax({
                            type: "GET",
                            dataType: 'json',
                            url: strUrl,
                            cache: false,
                            success: function(data) {
                                $(oLink).siblings("div").eq(0).html(data[0].PriceInfo);
                                if ($(oLink).attr("class") == "show") {
                                    wsug(oLink, data[0].PriceInfo);
                                }
                            }
                        });
                    }
                }
            }).mouseout(function() {
                $(this).removeClass("show");
                wsug(this, 0);
            });
        }
    }
};

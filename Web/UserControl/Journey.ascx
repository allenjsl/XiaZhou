<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Journey.ascx.cs" Inherits="Web.UserControl.Journey" %>

<script type="text/javascript">
    var Journey = {
        //创建编辑器
        CreateEdit: function(obj) {
            var _self = $(obj);
            if ($.trim(_self.attr("id")).length == 0) _self.attr("id", "txtRemark" + parseInt(Math.random() * 1000));
            KEditer.remove(_self.attr("id"));
            KEditer.init(_self.attr("id"), { resizeMode: 0, items: keSimple, height: "200px", width: "750px" });
        },
        //全选 用餐
        SelectAllForEat: function(obj) {
            var _self = $(obj);
            _self.closest("td").find("input[type='checkbox']").attr("checked", obj.checked);
            if (obj.checked) _self.closest("td").find("input[type='hidden']").val("1");
            else _self.closest("td").find("input[type='hidden']").val("");
        },
        //单选 用餐
        SelectOneForEat: function(obj) {
            var a = true;
            var td = $(obj).closest("td");
            switch (obj.value) {
                case "1":
                    td.find("input[name='eatFrist']").val(obj.checked ? "1" : "");
                    break;
                case "2":
                    td.find("input[name='eatSecond']").val(obj.checked ? "1" : "");
                    break;
                case "3":
                    td.find("input[name='eatThird']").val(obj.checked ? "1" : "");
                    break;
            }
            td.find("input[type='checkbox']").each(function() {
                if (this.value != "0" && !this.checked) {
                    a = false;
                }
            })
            $(obj).closest("td").find("input[type='checkbox'][value='0']").attr("checked", a ? "checked" : "");
        },
        //选用景点
        SelectSince: function(obj) {
            if ($(obj).attr("id").length == 0) {
                $(obj).attr("id", "a_Journey_" + parseInt(Math.random() * 10000));
            }
            Boxy.iframeDialog({
                iframeUrl: $(obj).attr("href") + "&aid=" + $(obj).attr("id") + '&sl=<%=Request.QueryString["sl"] %>&SMode=2',
                title: $(obj).attr("title"),
                modal: true,
                width: "850px",
                height: "500px"
            });
        },
        //选择景点以后创建
        CreateSince: function(jingDianXuanYongId, jingDian) {
            var temp = '<span class="upload_filename"><a data-class="a_Journey_Since" data-for="' + jingDian.id + '">' + jingDian.name + '</a> <a data-for="' + jingDian.id + '" href="javascript:void(0);" onclick="Journey.RemoveSince(this)"><img src="/images/cha.gif"></a></span>';
            $("#" + jingDianXuanYongId).parent().find("span[data-class='fontblue']").append(temp);
            this.SetSinveValue($("#" + jingDianXuanYongId).closest("td"));

            //将景点名称写入行程内容 【A景点】：描述
            var _$tr = $("#" + jingDianXuanYongId).closest("tr").next();
            var _$textarea = _$tr.find("textarea[name='txtContent']");
            var _textareaid = _$textarea.attr("id");
            if (KEditer.isInit(_textareaid)) KEditer.sync(_textareaid);
            else this.CreateEdit(_$textarea);
            var _html = _$textarea.val();
            _html += '<br/><b style="color:#ff0000">【' + jingDian.name + '】</b>：' + jingDian.desc ;
            _$textarea.val(_html);
            KEditer.html(_$textarea.attr("id"), _html);
        },
        //删除景点
        RemoveSince: function(args) {
            var td = $(args).closest("td");
            $(args).closest("span[class='upload_filename']").remove();
            this.SetSinveValue(td);
        },
        //删除和添加 景点后修改表单
        SetSinveValue: function(td) {
            var hideArray = new Array();
            var showArray = new Array();
            td.closest("td").find("a[data-class='a_Journey_Since']").each(function() {
                hideArray.push($(this).attr("data-for"));
                showArray.push(encodeURIComponent($.trim($(this).html())));
            })

            td.find("input[name='hd_scenery_spot']").val(hideArray.join(','));
            td.find("input[name='show_scenery_spot']").val(showArray.join(','));
        },
        //删除附件
        RemoveFile: function(obj) {
            $(obj).closest("td").find("input[name='hide_Journey_file']").val("");
            $(obj).closest("div[class='upload_filename']").remove();
            return false;
        },
        CreateFlashUpload: function(flashUpload, idNo) {
            flashUpload = new SWFUpload({
                upload_url: "/CommonPage/upload.aspx",
                file_post_name: "Filedata",
                post_params: {
                    "ASPSESSID": "<%=Session.SessionID %>"
                },

                file_size_limit: "2 MB",
                file_types: "*.bmp;*.jpg;*.gif;*.jpeg;*.png;",
                file_types_description: "附件上传",
                file_upload_limit: 100,
                swfupload_loaded_handler: function() { },
                file_dialog_start_handler: uploadStart,
                upload_start_handler: uploadStart,
                file_queued_handler: fileQueued,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                upload_complete_handler: uploadComplete,

                // Button settings
                button_placeholder_id: "spanButtonPlaceholder_" + idNo,
                button_image_url: "/images/swfupload/XPButtonNoText_178_34.gif",
                button_width: 178,
                button_height: 34,
                button_text: '<span ></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,
                button_cursor: SWFUpload.CURSOR.HAND,
                flash_url: "/js/swfupload/swfupload.swf",
                custom_settings: {
                    upload_target: "divFileProgressContainer_" + idNo,
                    HidFileNameId: "hide_Journey_file_" + idNo,
                    HidFileName: "hide_Journey_file_Old",
                    ErrMsgId: "errMsg_" + idNo,
                    UploadSucessCallback: function() { Journey.UploadOverCallBack(idNo); }
                },
                debug: false,
                minimum_flash_version: "9.0.28",
                swfupload_pre_load_handler: swfUploadPreLoad,
                swfupload_load_failed_handler: swfUploadLoadFailed
            });
        },
        UploadArgsList: [],
        InitSwfUpload: function(tr, no) {
            var $box = tr || $("#tbl_Journey_AutoAdd");
            $box.find("div[data-class='Journey_upload_swfbox']").each(function() {
                var idNo = no || parseInt(Math.random() * 100000);

                $(this).find("[data-class='Journey_upload']").each(function() {
                    if ($(this).attr("id") == "") {
                        $(this).attr("id", $(this).attr("data-id") + "_" + idNo);
                    }
                })
                var swf = null;
                Journey.CreateFlashUpload(swf, idNo);
                if (swf) {
                    Journey.UploadArgsList.push(swf);
                }
            })
        },
        AddRowCallBack: function(tr) {
            var $tr = tr;
            $tr.find("div[data-class='Journey_upload_swfbox']").html($("#divJourneyUploadTemp").html());
            $tr.find("span[class='errmsg']").html("");
            $tr.find("span[data-class='fontblue']").html("");
            $tr.find("div[data-class='span_journey_file']").remove();
            tr.find("textarea[name='txtContent']").show();
            Journey.InitSwfUpload($tr);
        },
        MoveRowCallBack: function(tr) {
            var txt = tr.find("textarea[name='txtContent']");
            KEditer.sync(txt.attr("id"));
            Journey.CreateEdit(txt);
            var eqFrist = tr.find("div[data-class='Journey_upload']").eq(0);
            try {
                //处理IE 9 下移除FLASH 异常
                tr.find("object").remove();
            } catch (e) {
                eqFrist.prev().html('<input type="hidden" data-class="Journey_upload" data-id="hide_Journey_file" name="hide_Journey_file" /><span data-class="Journey_upload" data-id="errMsg" class="errmsg"></span>');
            }
            var no = eqFrist.attr("id").split('_')[1];
            eqFrist.prev().append('<span data-class="Journey_upload" data-id="spanButtonPlaceholder"></span>');
            Journey.InitSwfUpload(tr, no);
        },
        UploadOverCallBack: function(idNo) {
            var $div = $("#divFileProgressContainer_" + idNo);
            if ($div.length > 0) {
                if ($div.find("div[class='progressWrapper']").length > 1) {
                    $div.find("div[class='progressWrapper']").eq(0).remove();
                }
            }
        }

    }

    //景点选择打开窗口选择景点后回调方法
    function jingDianXuanZe_callBack(data) {
        if (!data || $.trim(data.jingDianXuanYongAId).length == 0) return;
        for (var i = 0; i < data.jingDians.length; i++) {
            Journey.CreateSince(data.jingDianXuanYongAId, data.jingDians[i]);
        }
    }

    $(function() {
        $("#tbl_Journey_AutoAdd").find("textarea[name='txtContent']").each(function() {
            var _self = $(this);
            _self.unbind().click(function() {
                Journey.CreateEdit(this);
            })
            if ($.trim(_self.val()) != "") {
                $(this).click();
            }
        })

        Journey.InitSwfUpload(null, null);
    })
</script>

<style type="text/css">
    #tbl_Journey_AutoAdd .progressWrapper
    {
        overflow: hidden;
        width: 200px;
    }
    #tbl_Journey_AutoAdd .progressContainer
    {
        width: 195px;
        padding: 1px;
        margin: 0;
        margin-top: 3px;
    }
    #tbl_Journey_AutoAdd .progressBarComplete, .progressBarStatus
    {
        width: 190px;
        margin: 1px;
    }
    #tbl_Journey_AutoAdd .progressName
    {
        overflow: hidden;
        width: 150px;
    }
</style>
<div style="width: 98.5%" class="tablelist-box">
    <span class="formtableT"><s></s>行程安排</span>
    <table id="tbl_Journey_AutoAdd" width="100%" class="journey" cellspacing="0" cellpadding="0"
        <%=IsSuppliers ? "border=1 bordercolor=#CCCCCC":"" %>>
        <tr class="addcontentT">
            <th width="5%" valign="middle" class="th-line">
                日期
            </th>
            <th width="16%" valign="middle" class="th-line">
                区间
            </th>
            <th width="16%" valign="middle" class="th-line">
                交通
            </th>
            <th width="33%" valign="middle" class="th-line">
                住宿
            </th>
            <th width="19%" valign="middle" class="th-line">
                用餐
            </th>
            <th width="130px" valign="middle" class="th-line">
                操作
                <div style="width:150px;height:1px;"></div>
            </th>
        </tr>
        <%if (SetPlanList == null || (SetPlanList != null && SetPlanList.Count == 0))
          { %>
        <tbody class="tempRow">
            <tr>
                <td align="center" rowspan="3">
                    D<b class="index">1</b>
                </td>
                <td align="center">
                    <input type="text" name="txt_qujin" class='<%=IsSuppliers ? "searchInput size170":"inputtext formsize120" %>'
                        value="" />
                </td>
                <td align="center">
                    <input type="text" name="txtTraffic" class="<%=IsSuppliers ? "searchInput size170":"inputtext formsize120" %>">
                </td>
                <td align="center">
                    <span class="journeyHotel">
                        <input type="text" class='<%=IsSuppliers ? "searchInput size170":"inputtext formsize120" %>'
                            name="txtHotelName" />
                    </span>
                </td>
                <td align="center">
                    <input type="hidden" name="eatFrist" value="" />
                    <input type="hidden" name="eatSecond" value="" />
                    <input type="hidden" name="eatThird" value="" />
                    <label>
                        <input type="checkbox" value="0" onclick="Journey.SelectAllForEat(this);" />全</label>&nbsp;
                    <label>
                        <input type="checkbox" value="1" onclick="Journey.SelectOneForEat(this)" />早</label>&nbsp;
                    <label>
                        <input type="checkbox" value="2" onclick="Journey.SelectOneForEat(this)" />中</label>&nbsp;
                    <label>
                        <input type="checkbox" value="3" onclick="Journey.SelectOneForEat(this)" />晚</label>
                </td>
                <td align="center" rowspan="3">
                    <a href="javascript:void(0)">
                        <img src="/images/shangyiimg.gif" class="moveupbtn" height="20" width="48"></a><br>
                    <br>
                    <a href="javascript:void(0)">
                        <img src="/images/charuimg.gif" class="insertbtn" height="20" width="48"></a>&nbsp;
                    <a href="javascript:void(0)">
                        <img src="/images/delimg.gif" class="delbtn" height="20" width="48"></a><br>
                    <br>
                    <a href="javascript:void(0)">
                        <img src="/images/xiayiimg.gif" class="movedownbtn" height="20" width="48"></a>
                </td>
            </tr>
            <%if (!IsSuppliers)
              { %>
            <tr>
                <td valign="middle" align="left" colspan="4">
                    <b>选择景点</b>：&nbsp;
                    <input type="hidden" name="hd_scenery_spot" value="" />
                    <input type="hidden" name="show_scenery_spot" value="" />
                    <a class="xuanyong" title="选择景点" onclick="Journey.SelectSince(this);return false;"
                        href="/TeamCenter/SelectScenic.aspx?callback=jingDianXuanZe_callBack">&nbsp;</a>
                    <span data-class="fontblue" class="fontblue"></span>
                </td>
            </tr>
            <%} %>
            <tr>
                <td align="left" colspan="3" class="noboder">
                    <textarea name="txtContent" class="richText" style="width: 99%; height: 200px;"></textarea>
                </td>
                <td valign="middle" align="center">
                    <div style="margin: 0px 10px;" data-class="Journey_upload_swfbox">
                        <div>
                            <input type="hidden" data-class="Journey_upload" data-id="hide_Journey_file" name="hide_Journey_file" />
                            <span data-class="Journey_upload" data-id="spanButtonPlaceholder"></span><span data-class="Journey_upload"
                                data-id="errMsg" class="errmsg"></span>
                        </div>
                        <div data-class="Journey_upload" data-id="divFileProgressContainer">
                        </div>
                        <div data-class="Journey_upload" data-id="thumbnails">
                        </div>
                    </div>
                </td>
            </tr>
        </tbody>
        <%} %>
        <asp:Repeater ID="rptJourney" runat="server">
            <ItemTemplate>
                <tbody class="tempRow">
                    <tr>
                        <td align="center" rowspan="3">
                            D<b class="index"><%#Eval("Days")%></b>
                        </td>
                        <td align="center">
                            <input type="text" name="txt_qujin" class='<%#IsSuppliers ? "searchInput size170":"inputtext formsize120" %>'
                                value='<%#Eval("Section") %>' />
                        </td>
                        <td align="center">
                            <input type="text" name="txtTraffic" class="<%#IsSuppliers ? "searchInput size170":"inputtext formsize120" %>"
                                value="<%#Eval("Traffic") %>">
                        </td>
                        <td align="center">
                            <span class="journeyHotel">
                                <input type="text" class='<%#IsSuppliers ? "searchInput size170":"inputtext formsize120" %>'
                                    name="txtHotelName" value="<%#Eval("Hotel") %>" /></span>
                        </td>
                        <td align="center">
                            <input type="hidden" name="eatFrist" value="<%#(bool)Eval("Breakfast")?"1":""%>" />
                            <input type="hidden" name="eatSecond" value="<%#(bool)Eval("Lunch")?"1":""%>" />
                            <input type="hidden" name="eatThird" value="<%#(bool)Eval("Supper")?"1":""%>" />
                            <label>
                                <input type="checkbox" value="0" onclick="Journey.SelectAllForEat(this);" <%#(bool)Eval("Breakfast") && (bool)Eval("Lunch") &&(bool)Eval("Supper") ?"checked=checked":""%> />全</label>&nbsp;
                            <label>
                                <input type="checkbox" onclick="Journey.SelectOneForEat(this)" <%#(bool)Eval("Breakfast")?"checked=checked":""%>
                                    value="1" />早</label>&nbsp;
                            <label>
                                <input type="checkbox" onclick="Journey.SelectOneForEat(this)" <%#(bool)Eval("Lunch")?"checked=checked":""%>
                                    value="2" />中</label>&nbsp;
                            <label>
                                <input type="checkbox" onclick="Journey.SelectOneForEat(this)" <%#(bool)Eval("Supper")?"checked=checked":""%>
                                    value="3" />晚</label>
                        </td>
                        <td align="center" rowspan="3">
                            <a href="javascript:void(0)">
                                <img src="/images/shangyiimg.gif" class="moveupbtn" height="20" width="48"></a><br>
                            <br>
                            <a href="javascript:void(0)">
                                <img src="/images/charuimg.gif" class="insertbtn" height="20" width="48"></a>&nbsp;
                            <a href="javascript:void(0)">
                                <img src="/images/delimg.gif" class="delbtn" height="20" width="48"></a><br>
                            <br>
                            <a href="javascript:void(0)">
                                <img src="/images/xiayiimg.gif" class="movedownbtn" height="20" width="48"></a>
                        </td>
                    </tr>
                    <%if (!IsSuppliers)
                      { %>
                    <tr>
                        <td valign="middle" align="left" colspan="4">
                            <b>选择景点</b>：&nbsp; <a class="xuanyong" title="选择景点" onclick="Journey.SelectSince(this);return false;"
                                href="/TeamCenter/SelectScenic.aspx?callback=jingDianXuanZe_callBack">&nbsp;</a>
                            <%#GetSinceByList(Eval("TourPlanSpot"))%>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td align="left" colspan="3" class="noboder">
                            <textarea name="txtContent" class="inputtext" style="width: 99%; height: 200px;">
                                    <%#Eval("Content")%>
                                </textarea>
                        </td>
                        <td valign="middle" align="center">
                            <div style="margin: 0px 10px;" data-class="Journey_upload_swfbox">
                                <div>
                                    <input type="hidden" data-class="Journey_upload" data-id="hide_Journey_file" name="hide_Journey_file"
                                        value="|<%#Eval("FilePath") %>" />
                                    <span data-class="Journey_upload" data-id="spanButtonPlaceholder"></span><span data-class="Journey_upload"
                                        data-id="errMsg" class="errmsg"></span>
                                </div>
                                <div data-class="Journey_upload" data-id="divFileProgressContainer">
                                </div>
                                <div data-class="Journey_upload" data-id="thumbnails">
                                </div>
                            </div>
                            <%#Eval("FilePath").ToString().Trim() != "" ? "<div data-class='span_journey_file' class='upload_filename'><a target='_blank' href='" + Eval("FilePath").ToString() + "'>查看附件</a><a href='javascript:void(0);' title='删除附件' onclick='Journey.RemoveFile(this);'><img src='/images/cha.gif' border='0'></a> </div>" : ""%>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div style="margin: 0px 10px; display: none;" id="divJourneyUploadTemp">
        <div>
            <input type="hidden" data-class="Journey_upload" data-id="hide_Journey_file" name="hide_Journey_file" />
            <span data-class="Journey_upload" data-id="spanButtonPlaceholder"></span><span data-class="Journey_upload"
                data-id="errMsg" class="errmsg"></span>
        </div>
        <div data-class="Journey_upload" data-id="divFileProgressContainer">
        </div>
        <div data-class="Journey_upload" data-id="thumbnails">
        </div>
    </div>
</div>

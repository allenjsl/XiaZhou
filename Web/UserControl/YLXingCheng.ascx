<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YLXingCheng.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.YLXingCheng" %>

<script type="text/javascript">
    var pageDataJs = {
        //创建编辑器
        CreateEdit: function(obj) {
            var _self = $(obj);
            if ($.trim(_self.attr("id")).length == 0) _self.attr("id", "txtRemark" + parseInt(Math.random() * 1000));
            KEditer.remove(_self.attr("id"));
            KEditer.init(_self.attr("id"), { resizeMode: 0, items: keSimpleYL, height: "100px", width: "800px" });
        },
        DelFile: function(obj) {
            $(obj).parent().remove();
        },
        RemoveFile: function(obj) {
            $(obj).closest("td").find("input[name='hide_Cruiseimg_file']").val("");
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
                file_types: "*.jpg;*.gif;*.jpeg;*.png;",
                file_types_description: "附件上传",
                file_upload_limit: 1,
                swfupload_loaded_handler: function() { document.title = "" },
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
                button_image_url: "/images/swfupload/XPButtonNoText_92_24_01.gif",
                button_width: 92,
                button_height: 24,
                button_text: '<span ></span>',
                button_text_style: '',
                button_text_top_padding: 1,
                button_text_left_padding: 5,
                button_cursor: SWFUpload.CURSOR.HAND,
                flash_url: "/js/swfupload/swfupload.swf",
                custom_settings: {
                    upload_target: "divFileProgressContainer_" + idNo,
                    HidFileNameId: "hide_Cruiseimg_file_" + idNo,
                    HidFileName: "hide_Cruiseimg_file_Old",
                    ErrMsgId: "errMsg_" + idNo,
                    UploadSucessCallback: null
                },
                debug: false,
                minimum_flash_version: "9.0.28",
                swfupload_pre_load_handler: swfUploadPreLoad,
                swfupload_load_failed_handler: swfUploadLoadFailed
            });
        },
        UploadArgsList: [],
        InitSwfUpload: function(tr) {
            var $box = tr || $("#tablist1");
            $box.find("div[data-class='Cruiseimg_upload_swfbox']").each(function() {
                var idNo = parseInt(Math.random() * 100000);
                $(this).find("[data-class='Cruiseimg_upload']").each(function() {
                    if ($(this).attr("id") == "") {
                        $(this).attr("id", $(this).attr("data-id") + "_" + idNo);
                    }
                })
                var swf = null;
                pageDataJs.CreateFlashUpload(swf, idNo);
                if (swf) {
                    pageDataJs.UploadArgsList.push(swf);
                }
            })
        },
        AddRowCallBack: function(tr) {
            var $tr = tr;
            $tr.find("div[data-class='Cruiseimg_upload_swfbox']").html($("#divCruiseimgUploadTemp").html());
            $tr.find("span[class='errmsg']").html("");
            $tr.find("div[data-class='span_Cruiseimg_file']").remove();
            $tr.find(".editbox1").show();
            pageDataJs.InitSwfUpload($tr);
        }
    }


    $(function() {
        pageDataJs.InitSwfUpload();
        $("#tablist1").find(".editbox1").each(function() {
            var _self = $(this);
            _self.unbind().click(function() {
                pageDataJs.CreateEdit(this);
            })
            if ($.trim(_self.val()) != "") {
                $(this).click();
            }
        })

    })
</script>

<div style="width: 98.5%" class="tablelist-box ">
    <span class="formtableT"><s></s>行程安排</span>
    <table width="100%" cellspacing="0" cellpadding="0" class="autoAdd12" id="tablist1">
        <tr class="addcontentT">
            <th width="5%" class="th-line">
                日期
            </th>
            <th class="th-line">
                区间
            </th>
            <th class="th-line">
                交通工具
            </th>
            <th class="th-line">
                行程图片
            </th>
            <th class="th-line">
                操作
            </th>
        </tr>
        <asp:PlaceHolder ID="plaVis" runat="server">
            <tbody class="tempRow1">
                <tr>
                    <td align="center" rowspan="3">
                        D<span class="index">1</span><br />
                    </td>
                    <td align="center">
                        <%--<input name="qujians" type="text" class="formsize80 inputtext " valid="required"
                            errmsg="请输入区间开始位置！" />
                        -
                        <input name="qujiane" type="text" class="formsize80 inputtext " valid="required"
                            errmsg="请输入区间结束位置！" />--%>
                        <input name="qujian" type="text" class="formsize180 inputtext " valid="required"
                            errmsg="请输入区间！" />    
                    </td>
                    <td align="center">
                        <input name="jiaotong" type="text" class="formsize180 inputtext" />
                    </td>
                    <td align="center">
                        <%--<input name="banci" type="text" class="formsize180 inputtext" />--%>
                    </td>
                    <td align="center" rowspan="3">
                        <a class="Xaddbtn" href="javascript:void(0)">
                            <img width="48" height="20" src="/images/addimg.gif"></a> <a class="Xdelbtn" href="javascript:void(0)">
                                <img width="48" height="20" src="/images/delimg.gif"></a>
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="left" colspan="2">
                        <b>早餐
                            <input name="zaocan" type="text" class="formsize120 inputtext" />
                            <b>午餐</b>
                            <input name="wucan" type="text" class="formsize120 inputtext" />
                            <b>晚餐</b>
                            <input name="wancan" type="text" class="formsize120 inputtext" />
                            <b>住宿</b>
                            <input name="zhusu" type="text" class="formsize120 inputtext" /></b>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="left" style="padding: 5px;" colspan="2">
                        <textarea name="neirong" class="inputtext editbox1" style="width: 99%; height: 100px;"></textarea>
                    </td>
                    <td align="left">
                        <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox">
                            <div>
                                <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                    name="hide_Cruiseimg_file" value="|" />
                                <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                    data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>                                
                            </div>
                            <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                            </div>
                            <div data-class="Cruiseimg_upload" data-id="thumbnails">
                            </div>                            
                        </div>
                        <span style="color:#666">&nbsp;&nbsp;尺寸：217*149</span>
                    </td>
                </tr>
            </tbody>
        </asp:PlaceHolder>
        <asp:Repeater ID="rptlist" runat="server">
            <ItemTemplate>
                <tbody class="tempRow1">
                    <tr>
                        <td align="center" rowspan="3">
                            D<span class="index"><%#Container.ItemIndex+1%></span><br />
                        </td>
                        <td align="center">
                            <%--<input name="qujians" type="text" class="formsize80 inputtext " valid="required"
                                errmsg="请输入区间开始位置！" value="<%#Eval("QuJian1") %>" />
                            -
                            <input name="qujiane" type="text" class="formsize80 inputtext " valid="required"
                                errmsg="请输入区间结束位置！" value="<%#Eval("QuJian2") %>" />--%>
                            <input name="qujian" type="text" class="formsize180 inputtext " valid="required" errmsg="请输入区间！"
                                value="<%#Eval("QuJian1") %>" />
                        </td>
                        <td align="center">
                            <input name="jiaotong" type="text" class="formsize180 inputtext" value="<%#Eval("JiaoTongGongJu") %>" />
                        </td>
                        <td align="center">
                            <%--<input name="banci" type="text" class="formsize180 inputtext" value="<%#Eval("BanCi") %>" />--%>
                        </td>
                        <td align="center" rowspan="3">
                            <a class="Xaddbtn" href="javascript:void(0)">
                                <img width="48" height="20" src="/images/addimg.gif"></a> <a class="Xdelbtn" href="javascript:void(0)">
                                    <img width="48" height="20" src="/images/delimg.gif"></a>
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="left" colspan="2">
                            <b>早餐
                                <input name="zaocan" type="text" class="formsize120 inputtext" value="<%#Eval("Zao") %>" />
                                <b>午餐</b>
                                <input name="wucan" type="text" class="formsize120 inputtext" value="<%#Eval("Zhong") %>" />
                                <b>晚餐</b>
                                <input name="wancan" type="text" class="formsize120 inputtext" value="<%#Eval("Wan") %>" />
                                <b>住宿</b>
                                <input name="zhusu" type="text" class="formsize120 inputtext" value="<%#Eval("ZhuSu") %>" /></b>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left" style="padding: 5px;" colspan="2">
                            <textarea name="neirong" class="inputtext editbox1" style="width: 99%; height: 100px;"><%#Eval("NeiRong")%></textarea>
                        </td>
                        <td valign="middle" align="center">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="noboder">
                                <tbody>
                                    <tr>
                                        <td align="left">
                                            <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox">
                                                <div>
                                                    <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                                        name="hide_Cruiseimg_file" value="|<%#Eval("Filepath") %>" />
                                                    <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                                        data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                                </div>
                                                <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                                </div>
                                                <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                                </div>
                                            </div>
                                            <%# getFilePath(Eval("Filepath").ToString())  %>
                                            <span style="color: #666">&nbsp;&nbsp;尺寸：217*149</span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <div style="margin: 0px 10px; display: none;" id="divCruiseimgUploadTemp">
        <div>
            <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                name="hide_Cruiseimg_file" value="|" />
            <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
        </div>
        <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
        </div>
        <div data-class="Cruiseimg_upload" data-id="thumbnails">
        </div>
    </div>
</div>

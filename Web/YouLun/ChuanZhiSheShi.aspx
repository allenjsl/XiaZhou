<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChuanZhiSheShi.aspx.cs"
    Inherits="EyouSoft.Web.YouLun.ChuanZhiSheShi" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="/css/boxynew.css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <style type="text/css">
        #tabList th
        {
            border: 1px solid #46ABDC;
        }
        #tabList td
        {
            border: 1px solid #9AD2ED;
        }
    </style>
</head>
<body style="background: none;">
    <form id="form1" runat="server">
    <div class="alertbox-outbox02">
        <table width="100%" style="margin-top: 5px; margin-bottom: 5px;" id="tabList">
            <tbody>
                <asp:Repeater ID="rptlist" runat="server">
                    <ItemTemplate>
                        <tr class="tempRow">
                            <td align="center">
                                <table width="100%" style="margin-top: 5px; margin-bottom: 5px;" id="tabList">
                                    <tbody>
                                        <tr>
                                            <td width="118" align="center">
                                                设施名称
                                            </td>
                                            <td width="200" align="center">
                                                <input type="text" value="<%#Eval("MingCheng") %>" class="inputtext formsize180"
                                                    name="MingCheng" valid="required" errmsg="请输入设施名称！" />
                                            </td>
                                            <td width="168" align="center">
                                                图片
                                            </td>
                                            <td bgcolor="#FFFFFF" align="left">
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
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="118" align="center">
                                                详细介绍
                                            </td>
                                            <td width="200" align="center" colspan="3">
                                                <textarea name="MiaoShu" class="inputtext editbox" style="width: 99%; height: 100px;">
                                    <%#Eval("MiaoShu")%>
                            </textarea>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td align="center">
                                <a class="addbtn" href="javascript:void(0)">
                                    <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                        <img width="48" height="20" src="/images/delimg.gif"></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder ID="phrPanel" runat="server">
                    <tr class="tempRow">
                        <td align="center">
                            <table width="100%" style="margin-top: 5px; margin-bottom: 5px;" id="Table1">
                                <tbody>
                                    <tr>
                                        <td width="118" align="center">
                                            设施名称
                                        </td>
                                        <td width="200" align="center">
                                            <input type="text" value=" " class="inputtext formsize180" name="MingCheng" valid="required"
                                                errmsg="请输入设施名称！" />
                                        </td>
                                        <td width="168" align="center">
                                            图片
                                        </td>
                                        <td bgcolor="#FFFFFF" align="left">
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="118" align="center">
                                            详细介绍
                                        </td>
                                        <td width="200" align="center" colspan="3">
                                            <textarea name="MiaoShu" class="inputtext editbox" style="width: 99%; height: 100px;">
                                     
                            </textarea>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                        <td align="center">
                            <a class="addbtn" href="javascript:void(0)">
                                <img width="48" height="20" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                    <img width="48" height="20" src="/images/delimg.gif"></a>
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </tbody>
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
        <div style="text-align: center;" class="alertbox-btn">
            <a href="javascript:;" id="btnSave"><s class="baochun"></s>保 存</a> <a hidefocus="true"
                href="javascript:" onclick="parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide(); return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <!--列表结束-->
    </form>

    <script type="text/javascript">
        var pageDataJs = {
            Params: { sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>', id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("id") %>' },
            UnBindBtn: function() {
                $("#btnSave").html("<s class='baochun'></s>提交中...").unbind("click").css("background-position", "0-57px");
            },
            //创建编辑器
            CreateEdit: function(obj) {
                var _self = $(obj);
                if ($.trim(_self.attr("id")).length == 0) _self.attr("id", "txtRemark" + parseInt(Math.random() * 1000));
                KEditer.remove(_self.attr("id"));
                KEditer.init(_self.attr("id"), { resizeMode: 0, items: keSimple, height: "100px", width: "580px" });
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click").css("background-position", "0-28px").html("<s class='baochun'></s>保 存");
                $("#btnSave").click(function() {
                    var form = $(this).closest("form").get(0);
                    if (ValiDatorForm.validator("form", "parent")) {
                        pageDataJs.Save();
                    } else {
                        return false;
                    }
                });
            },
            Save: function() {
                KEditer.sync();
                pageDataJs.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/youlun/ChuanZhiSheShi.aspx?save=save&" + $.param(pageDataJs.Params),
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = parent.window.location.href });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                            pageDataJs.BindBtn();
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后重试！");
                        pageDataJs.BindBtn();
                    }
                });
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
                        "ASPSESSID": "<%=Session.SessionID %>",
                        "P1": "YL"
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
                var $box = tr || $("#tabList");
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
                $tr.find(".editbox").show();
                pageDataJs.InitSwfUpload($tr);
            }
        };

        $(function() {
            pageDataJs.InitSwfUpload();
            $("#tabList").autoAdd({ addCallBack: pageDataJs.AddRowCallBack })
            $("#btnSave").click(function() {
                var form = $(this).closest("form").get(0);
                if (ValiDatorForm.validator(form, "parent")) {
                    pageDataJs.Save();
                } else {
                    return false;
                }
            }); //
            $("#tabList").find(".editbox").each(function() {
                var _self = $(this);
                _self.unbind().click(function() {
                    pageDataJs.CreateEdit(this);
                })
                if ($.trim(_self.val()) != "") {
                    $(this).click();
                }
            })//
        })
    </script>

</body>
</html>

/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/
var EditImgUpload = {
    HideID: "hideEidtImgUpload",
    Create: function(obj) {
        this.SwfUpload = new SWFUpload({
            upload_url: "/CommonPage/upload.aspx",
            file_post_name: "Filedata",
            file_size_limit: "2 MB",
            file_types: "*.xls;*.rar;*.zip;*.pdf;*.doc;*.swf;*.dot;*.7z;*.docx;*.xlsx;*.txt;*.ppt",
            file_types_description: "",
            file_upload_limit: 1,    // Zero means unlimited
            swfupload_loaded_handler: function() { },
            file_dialog_start_handler: fileDialogStart,
            file_queued_handler: fileQueued,
            file_queue_error_handler: fileQueueError,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,
            button_image_url: "/images/swfupload/XPButtonNoText_178_34.gif",
            button_placeholder_id: "spanButtonPlaceholder_edit",
            button_width: 178,
            button_height: 34,
            button_text: '<span></span>',
            button_text_style: '',
            button_text_top_padding: 1,
            button_text_left_padding: 3,
            button_cursor: SWFUpload.CURSOR.HAND,
            post_params: {P1:obj.P1},

            flash_url: "/js/swfupload/swfupload.swf",

            custom_settings: {
                upload_target: "divFileProgressContainer_edit",
                HidFileNameId: EditImgUpload.HideID,
                HidFileName: "hideUploadBei",
                ErrMsgId: "errMsg_edit",
                UploadSucessCallback: function() { }
            },

            // Debug Settings
            debug: false,

            // SWFObject settings
            minimum_flash_version: "9.0.28",
            swfupload_pre_load_handler: swfUploadPreLoad,
            swfupload_load_failed_handler: swfUploadLoadFailed
        });
    },
    SwfUpload: null
}

KindEditor.plugin('insertfile', function(K) {
	var self = this, name = 'insertfile',
		allowFileUpload = K.undef(self.allowFileUpload, true),
		allowFileManager = K.undef(self.allowFileManager, false),
		formatUploadUrl = K.undef(self.formatUploadUrl, true),
		uploadJson = K.undef(self.uploadJson, self.basePath + 'php/upload_json.php'),
		lang = self.lang(name + '.');
	self.plugin.fileDialog = function(options) {
		var fileUrl = K.undef(options.fileUrl, 'http://'),
			fileTitle = K.undef(options.fileTitle, ''),
			clickFn = options.clickFn;
        var target = 'kindeditor_upload_iframe_' + new Date().getTime();
		var html = [
			'<div style="padding:20px;height:200px;">',
			'<div class="ke-dialog-row">',
//			'<label for="keUrl" style="width:60px;">' + lang.url + '</label>',
			'<input type="text" id="keUrl" name="url" class="ke-input-text" style="width:160px;display:none;" /> &nbsp;',
//			'<input type="button" class="ke-upload-button" value="' + lang.upload + '" /> &nbsp;',
        //local upload - start
			'<iframe name="' + target + '" style="display:none;"></iframe>',
			'<form class="ke-upload-area ke-form" method="post" enctype="multipart/form-data" target="' + target + '" action="' + K.addParam(uploadJson, 'dir=image') + '">',
        //file
			'<div class="ke-dialog-row" style="display:none;">',
			'<label style="width:60px;">' + lang.localUrl + '</label>',
			'<input type="text" name="localUrl" class="ke-input-text" tabindex="-1" style="width:200px;" readonly="true" /> &nbsp;',
			'<input type="button" class="ke-upload-button" value="' + lang.upload + '" />',
			'</div>',
			' <input type="hidden" id="hideEidtImgUpload" /><span id="spanButtonPlaceholder_edit"></span><span id="errMsg_edit" class="errmsg"></span><div id="divFileProgressContainer_edit"></div>',
			'</form>',
        //local upload - end
			'<span class="ke-button-common ke-button-outer">',
			'<input type="button" class="ke-button-common ke-button" name="viewServer" value="' + lang.viewServer + '" />',
			'</span>',
			'</div>',
			//title
//			'<div class="ke-dialog-row">',
//			'<label for="keTitle" style="width:60px;">' + lang.title + '</label>',
			'<input type="text" id="keTitle" class="ke-input-text" name="title" value="" style="width:160px;display:none;" /></div>',
//			'</div>',
			//form end
//			'</form>',
			'</div>'
			].join('');
		var dialog = self.createDialog({
			name : name,
			width : 450,
			title : self.lang(name),
			body : html,
			yesBtn : {
				name : self.lang('yes'),
				click : function(e) {
                    uploadbutton.submit();
                    return false;
					var url = K.trim(urlBox.val()),
						title = titleBox.val();
					if (url == 'http://' || K.invalidUrl(url)) {
						alert(self.lang('invalidUrl'));
						urlBox[0].focus();
						return;
					}
					if (K.trim(title) === '') {
						title = url;
					}
					clickFn.call(self, url, title);
				}
			}
		}),
		div = dialog.div;
        EditImgUpload.Create({ P1: KEditer["UPLOAD_P1"] });

		var urlBox = K('[name="url"]', div),
			viewServerBtn = K('[name="viewServer"]', div),
			titleBox = K('[name="title"]', div);

		if (allowFileUpload) {
			var uploadbutton = K.uploadbutton({
				button : K('.ke-upload-button', div)[0],
				fieldName : 'imgFile',
				url : K.addParam(uploadJson, 'dir=file'),
				afterUpload : function(data) {
					dialog.hideLoading();
					if (data.error === 0) {
						var url = data.url;
						var title=data.title;
						if (formatUploadUrl) {
							url = K.formatUrl(url, 'domain');
						}
						urlBox.val(url);
						titleBox.val(title);
                        clickFn.call(self, url, title);
						if (self.afterUpload) {
							self.afterUpload.call(self, url);
						}
//						alert(self.lang('uploadSuccess'));
					} else {
						alert(data.message);
					}
				},
				afterError : function(html) {
					dialog.hideLoading();
					self.errorDialog(html);
				}
			});
			uploadbutton.fileBox.change(function(e) {
				dialog.showLoading(self.lang('uploadLoading'));
				uploadbutton.submit();
			});
		} else {
			K('.ke-upload-button', div).hide();
		}
		if (allowFileManager) {
			viewServerBtn.click(function(e) {
				self.loadPlugin('filemanager', function() {
					self.plugin.filemanagerDialog({
						viewType : 'LIST',
						dirName : 'file',
						clickFn : function(url, title) {
							if (self.dialogs.length > 1) {
								K('[name="url"]', div).val(url);
								self.hideDialog();
							}
						}
					});
				});
			});
		} else {
			viewServerBtn.hide();
		}
		urlBox.val(fileUrl);
		titleBox.val(fileTitle);
		urlBox[0].focus();
		urlBox[0].select();
	};
	self.clickToolbar(name, function() {
		self.plugin.fileDialog({
			clickFn : function(url, title) {
				var html = '<a href="' + url + '" data-ke-src="' + url + '" target="_blank">' + title + '</a>';
				self.insertHtml(html).hideDialog().focus();
			}
		});
	});
});

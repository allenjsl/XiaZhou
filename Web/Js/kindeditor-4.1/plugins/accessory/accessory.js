KE.plugin['accessory'] = {
        click: function(id) {
            KE.util.selection(id);
            var dialog = new KE.dialog({
                id: id,
                cmd: 'accessory',
                width: 310,
                height: 90,
                title: KE.lang['accessory'],
                yesButton: KE.lang['yes'],
                noButton: KE.lang['no']
            });
            dialog.show();
        },
        check: function(id) {
            var dialogDoc = KE.util.getIframeDoc(KE.g[id].dialog);
            var type = KE.$('type', dialogDoc).value;
            var url = '';
            if (type == 1) {
                url = KE.$('imgFile', dialogDoc).value;
            } else {
                url = KE.$('url', dialogDoc).value;
            }

            if (url.match(/\.(doc|xls|ppt|pdf|txt|rar|zip)$/i) == null) {
                alert(KE.lang['invalidAccessory']);
                window.focus();
                KE.g[id].yesButton.focus();
                return false;
            }
            return true;
        },
        exec: function(id) {
            KE.util.select(id);
            var dialogDoc = KE.util.getIframeDoc(KE.g[id].dialog);
            var type = KE.$('type', dialogDoc).value;
            if (!this.check(id)) return false;
            if (type == 1) {
                KE.$('editorId', dialogDoc).value = id;
                dialogDoc.uploadForm.submit();
                return false;
            } else {
                var url = KE.$('url', dialogDoc).value;
                var title = KE.$('imgTitle', dialogDoc).value;

                this.insert(id, url, title, ext);
            }
        },
        insert: function(id, url, title, ext) {
            var img;
            g = KE.g[id];
            path = g.pluginsPath + 'accessory/';
            ; switch (ext) {
                case "doc":
                    // img = path + "doc.jpg";
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/doc.jpg";
                    break;
                case "xls":
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/xls.jpg";
                    break;
                case "ppt":
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/ppt.jpg";
                    break;
                case "rar":
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/rar.jpg";
                    break;
                case "zip":
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/zip.jpg";
                    break;
                case "txt":
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/txt.jpg";
                    break;
                case "pdf":
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/pdf.jpg";
                    break;
                default:
                    img = "http://www.cnblogs.com/kindeditor/plugins/accessory/else.jpg";
            }
            var html = '<img src="' + img + '" >&nbsp;';
            html += '<a href="' + url + '" >';
            if (title) html += title;
            html += '</a>';
            alert(html)
            KE.util.insertHtml(id, html);
            KE.layout.hide(id);
            KE.util.focus(id);
        }

     };
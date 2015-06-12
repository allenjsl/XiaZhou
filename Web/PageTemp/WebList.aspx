<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebList.aspx.cs" Inherits="Web.PageT.WebList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <script type="text/javascript">
        var PageJsDataObj =
        {
            //显示弹窗Boxy.iframeDialog({iframeUrl:"",title:"",modal: true,width:"",height:""});

            Add: function() {
                //添加
            },
            Update: function(objsArr) {
                //修改---objsArr选中的TR对象
            },

            Copy: function(objsArr) {
                //复制---objsArr选中的TR对象
            },
            //使用通用按钮下获取数据并执行异步删除
            //删除(批量)
            DelAll: function(objArr) {
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < objArr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(objArr[i].find("input[type='checkbox']").val());
                    }
                }
                //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
                list.join(',');
                //执行
            },

            //绑定功能按钮
            BindBtn: function() {
                //绑定Add事件
                $(".toolbar_add").click(function() {
                    PageJsDataObj.Add();
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "aa", //这个参数讲不明白参考tableToolbar.js
                    //默认按钮
                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        PageJsDataObj.Update(objsArr);
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        PageJsDataObj.DelAll(objsArr);
                    },
                    copyCallBack: function(objsArr) {
                        //复制
                        PageJsDataObj.Copy(objsArr)
                    }
                    //cancelCallBack: function() {
                    //alert("执行了取消！");
                    //},
                    //自定义按钮
                    //otherButtons: [
                    //{
                    //button_selector: '', //按钮选择器
                    //sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                    //msg: '未选中任何 线路 ', //验证未通过提示文本
                    //buttonCallBack: function(ojb) {//验证通过执行函数
                    //alert("toolbar_paiduan");
                    //}
                    //}
                    //]

                })
            },
            PageInit: function() {
                //绑定功能按钮
                PageJsDataObj.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            }

        }
    </script>

    <script type="text/javascript">
        $(function() {
            PageJsDataObj.PageInit();
        })
    </script>

</body>
</html>

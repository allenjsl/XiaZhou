<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Web.ResourceManage.Ground.Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>资源管理-地接社-交易情况</title>
<link href="/css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/js/jquery-1.4.4.js"></script>
<script type="text/javascript" src="/js/jquery.boxy.js"></script>
   <script type="text/javascript">
	 $(function(){
	 	  $("#link1").click(function(){
			var url = $(this).attr("href");
			parent.Boxy.iframeDialog({
			iframeUrl:url,
			title:"对应字段",
			modal:true,
			width:"550px",
			height:"194px"
		});
		return false;
		});
		  
		
	 })
     </script>
   <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox03">
        <table width="98%" align="center" cellpadding="0" cellspacing="0" style="margin: 0 auto">
            <tr>
                <td height="23" align="center" class="alertboxTableT">
                    <input type="file" name="file" style="height: 20px;" />
                </td>
            </tr>
            <tr>
                <td style="line-height: 18px; padding: 5px;" align="center">
                    <span class="fontred">*</span>上传的文档必须是规定格式的（扩展名为xls文件）
                    <br />
                    可以先 <a href="#">点此下载文档模板</a> （点击右键另存为下载）
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="daoru02.html" hidefocus="true" id="link1"><s class="baochun"></s>保 存</a><a
                href="#" hidefocus="true"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
</body>
</html>

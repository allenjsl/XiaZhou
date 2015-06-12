using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Globalization;

public class accessory : IHttpHandler
{

    //文件保存目录路径
    private String savePath = "http://www.cnblogs.com/Uploads/accessory/"; //这里给自己想要保存文件的相对路径
    //定义允许上传的文件扩展名，docx,xlsx,pptx为office 2007以上的格式
    private String fileTypes = "doc,xls,ppt,docx,xlsx,pptx,pdf,txt,rar,zip";
    //最大文件不能超过5M
    private int maxSize = 5242880;

    private HttpContext context;

    public void ProcessRequest(HttpContext context)
    {
        this.context = context;

        HttpPostedFile imgFile = context.Request.Files["imgFile"];
        if (imgFile == null)
            showError("请选择文件。");

        String myYearDir = DateTime.Now.ToString("yyyy"); //保存文件的年份文件夹名称
        String myMonthDir = DateTime.Now.ToString("MM");  //保存文件的月份文件夹名称
        String dirPath = savePath + myYearDir + "/";
        if (!Directory.Exists(context.Server.MapPath(dirPath)))
            Directory.CreateDirectory(context.Server.MapPath(dirPath));//如果不存在该文件夹，则创建年份文件夹

        dirPath = dirPath + myMonthDir + "/";
        if (!Directory.Exists(context.Server.MapPath(dirPath)))
            Directory.CreateDirectory(context.Server.MapPath(dirPath));//如果不存在该文件夹，则创建月份文件夹

        String fileName = imgFile.FileName;
        String fileExt = Path.GetExtension(fileName).ToLower();
        ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

        if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            showError("上传文件大小不能超过5M。");

        if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            showError("上传文件扩展名是不允许的扩展名。");

        String newFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
        String filePath = context.Server.MapPath(dirPath) + newFileName;

        try
        {
            imgFile.SaveAs(filePath);
        }
        catch (Exception ex)
        {
            showError("保存附件出错。");
        }

        String fileUrl = dirPath + newFileName;

        //Hashtable hash = new Hashtable();
        //hash["error"] = 0; //上传成功
        //hash["url"] = fileUrl;
        //context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        //context.Response.Write(JsonMapper.ToJson(hash));
        //context.Response.End();

        //////////////////////////////////////////////////////////////////////////////////////////////////
        // 插入附件到kindeditor中
        string id = context.Request["id"].Trim();           //kindeditor控件的id
        string url = fileUrl.Trim();                        //保存文件的相对路径
        string title = Path.GetFileName(fileName).Trim();   //文件名称（原名陈）
        string ext = fileExt.Substring(1).ToLower().Trim(); //文件后缀名
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<html>");
        sb.Append("<head>");
        sb.Append("<title>Insert Accessory</title>");
        sb.Append("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">");
        sb.Append("</head>");
        sb.Append("<body>");
        //sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"accessory\"].insert(\"" + id + "\", \"" + url + "\",\"" + title + "\",\"" + ext + "\");</script>");
        sb.Append("<script type=\"text/javascript\">parent.KE.plugin[\"accessory\"].insert(\"" + id + "\", \"" + url + "\",\"" + title + "\",\"" + ext + "\");</script>");
        sb.Append("</body>");
        sb.Append("</html>");
        //  context.Response.Write(sb.ToString());
        context.Response.End();
    }

    private void showError(string message)
    {
        Hashtable hash = new Hashtable();
        hash["error"] = 1; //上传错误
        hash["message"] = message;
        context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(hash));
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}

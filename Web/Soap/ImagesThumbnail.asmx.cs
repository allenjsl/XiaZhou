using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace EyouSoft.Web.Soap
{
    /// <summary>
    /// ImagesThumbnail 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ImagesThumbnail : System.Web.Services.WebService
    {

        [WebMethod]
        public string ThumbnailImg(string FilePath, int Width, int Height)
        {
            if (!String.IsNullOrEmpty(FilePath))
            {
                if (Width > 0 && Height > 0)
                {
                    string Path = System.IO.Path.GetDirectoryName(FilePath);
                    string FileName = System.IO.Path.GetFileNameWithoutExtension(FilePath);
                    string FileExt = System.IO.Path.GetExtension(FilePath);
                    string NewFileName = Path + "\\" + FileName + "_" + Width.ToString() + "_" + Height.ToString() + FileExt;
                    if (System.IO.File.Exists(Server.MapPath(NewFileName)))
                    {
                        return NewFileName;
                    }
                    else
                    {
                        //创建文件并返回文件名
                        Common.Function.Thumbnail.MakeThumbnail(Server.MapPath(FilePath), Server.MapPath(NewFileName), Width, Height, "Cut");
                        return NewFileName;
                    }
                }
                else { return FilePath; }
            }
            else
            {

                return string.Empty;
            }
        }
    }
}

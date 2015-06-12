using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using EyouSoft.Common;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.IO;

namespace EyouSoft.YlWeb
{
    public partial class Reset : EyouSoft.YlWeb.WzPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitGuangGao();
            }
            else
            {
                Submit();
            }
        }

        void InitGuangGao()
        {
            int recordCount = 0;
            var chaXun = new EyouSoft.Model.YlStructure.MWzGuangGaoChaXunInfo();
            chaXun.WeiZhi = EyouSoft.Model.EnumType.YlStructure.WzGuangGaoWeiZhi.重置登录密码页右侧大图;

            var items = new EyouSoft.BLL.YlStructure.BWz().GetGuangGaos(YuMingInfo.CompanyId, 1, 1, ref recordCount, chaXun);

            if (items != null && items.Count > 0)
            {
                ltrGuangGao.Text = string.Format("<img src=\"{0}\">", EyouSoft.YlWeb.TuPian.F1("http://" + YuMingInfo.ErpYuMing + items[0].Filepath, 635, 372));
            }
            else
            {
                ltrGuangGao.Text = string.Format("<img  src=\"\">");
            }
        }

        void Submit()
        {
            var u = Utils.GetFormValue("txtYongHuMing");
            var b = new EyouSoft.BLL.YlStructure.BHuiYuan();
            var m = b.GetHuiYuanInfo(YuMingInfo.CompanyId, u, 1);
            if (!string.IsNullOrEmpty(u) && m != null)
            {
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
                var newMM = Rand.Str(6);//随机生成6位字母与数字新密码
                if (b.SheZhiHuiYuanMiMa(m.HuiYuanId, m.MD5Password, BitConverter.ToString(hashMD5.ComputeHash(Encoding.Default.GetBytes(newMM))).Replace("-", "").ToLower()) <= 0)
                {
                    Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "提交失败！"));
                    return;
                }
                //如果手机存在则短信提醒会员
                if (!string.IsNullOrEmpty(m.ShouJi))
                {
                    string content = "您的登陆密码是："+newMM;
                    if (EyouSoft.Toolkit.Utils.ProxySMS(m.ShouJi, content))
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "提交成功！"));
                    else
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "提交失败！"));

                }
                //不存在则邮件提醒会员
                else
                {
                    var mail = new Adpost.Common.Mail.EmailHelper { Subject = "取回密码", Body = "尊敬的" + m.XingMing + "会员：您好！<br/>请妥善保管好您的新密码："+ newMM, ReceiveAddress = m.YouXiang, SenderName = "维诗达游轮" };
                    //邮件接收的地址
                    if (mail.Send())
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("1", "提交成功！"));
                    }
                    else
                    {
                        Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "提交失败！"));
                    }
                }
            }
            else
            {
                Utils.RCWE(UtilsCommons.AjaxReturnJson("0", "用户名/邮箱不存在！"));
            }
        }
    }

    public class Rand
    {
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        /// <returns></returns>
        public static string Number(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        /// <returns></returns>
        public static string Str(int Length)
        {
            return Str(Length, false);
        }
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Str(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }


        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        /// <returns></returns>
        public static string Str_char(int Length)
        {
            return Str_char(Length, false);
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        /// <returns></returns>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
    } 
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ComStructure;
using EyouSoft.Model.SSOStructure;
using EyouSoft.Model.YlStructure;
using EyouSoft.Cache.Facade;
using System.Web;

namespace EyouSoft.Security.Membership
{
    /// <summary>
    /// 游轮网站会员登录
    /// </summary>
    public class YlHuiYuanProvider
    {
        #region static constants
        //static constants
        /// <summary>
        /// 登录Cookie，用户编号
        /// </summary>
        public const string LoginCookieHuiYuanId = "XZ_YL_UID";
        /// <summary>
        /// 登录Cookie，用户账号
        /// </summary>
        public const string LoginCookieUsername = "XZ_YL_UN";
        /// <summary>
        /// 登录Cookie，公司编号
        /// </summary>
        public const string LoginCookieCompanyId = "XZ_YL_CID";
        #endregion

        #region private members
        /// <summary>
        /// 设置登录会员cache
        /// </summary>
        /// <param name="huiYuanInfo">登录会员信息</param>
        static void SetHuiYuanCache(MYlHuiYuanInfo huiYuanInfo)
        {
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.YlWzHuiYuan, huiYuanInfo.HuiYuanId);
            EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
            EyouSoft.Cache.Facade.EyouSoftCache.Add(cacheKey, huiYuanInfo, DateTime.Now.AddHours(12));
        }

        /// <summary>
        /// 移除登录会员cache
        /// </summary>
        /// <param name="huiYuanId">会员编号</param>
        static void RemoveHuiYuanCache(string huiYuanId)
        {
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.YlWzHuiYuan, huiYuanId);

            EyouSoft.Cache.Facade.EyouSoftCache.Remove(cacheKey);
        }

        /// <summary>
        /// 获取登录会员Cookie信息
        /// </summary>
        /// <param name="name">登录Cookie名称</param>
        /// <returns></returns>
        static string GetCookie(string name)
        {
            HttpRequest request = HttpContext.Current.Request;

            if (request.Cookies[name] == null)
            {
                return string.Empty;
            }

            return request.Cookies[name].Value;
        }

        /// <summary>
        /// 移除登录会员Cookie
        /// </summary>
        static void RemoveHuiYuanCookies()
        {
            HttpResponse response = HttpContext.Current.Response;

            response.Cookies.Remove(LoginCookieCompanyId);
            response.Cookies.Remove(LoginCookieHuiYuanId);
            response.Cookies.Remove(LoginCookieUsername);

            DateTime cookiesExpiresDateTime = DateTime.Now.AddDays(-1);

            response.Cookies[LoginCookieCompanyId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieHuiYuanId].Expires = cookiesExpiresDateTime;
            response.Cookies[LoginCookieUsername].Expires = cookiesExpiresDateTime;
        }

        /// <summary>
        /// 设置会员登录Cookies
        /// </summary>
        /// <param name="huiYuanInfo">登录会员信息</param>
        /// <param name="cookies_expires_lx">cookies过期类型 0:浏览器进程，1:30天，2:30分钟</param>
        static void SetHuiYuanCookies(MYlHuiYuanInfo huiYuanInfo, int cookies_expires_lx)
        {
            //Cookies生存周期为浏览器进程
            HttpResponse response = HttpContext.Current.Response;
            RemoveHuiYuanCookies();

            System.Web.HttpCookie cookie = new HttpCookie(LoginCookieCompanyId);
            if (cookies_expires_lx == 1)
                cookie.Expires = DateTime.Now.AddMonths(1);
            if (cookies_expires_lx == 2)
                cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.Value = huiYuanInfo.CompanyId;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieHuiYuanId);
            if (cookies_expires_lx == 1)
                cookie.Expires = DateTime.Now.AddMonths(1);
            if (cookies_expires_lx == 2)
                cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.Value = huiYuanInfo.HuiYuanId;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);

            cookie = new HttpCookie(LoginCookieUsername);
            if (cookies_expires_lx == 1)
                cookie.Expires = DateTime.Now.AddMonths(1);
            if (cookies_expires_lx == 2)
                cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.Value = huiYuanInfo.Username;
            cookie.HttpOnly = true;
            response.AppendCookie(cookie);
        }

        /// <summary>
        /// 自动登录处理
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="userId">用户编号</param>
        /// <param name="username">用户账号</param>  
        /// <param name="uInfo">登录用户信息</param>
        static void AutoLogin(string companyId, string huiYuanId, string username, out MYlHuiYuanInfo huiYuanInfo)
        {
            huiYuanInfo = null;
            IYlHuiYuanLogin dal = new DYlHuiYuanLogin();
            var yuMingInfo = GetYuMingInfo();
            if (yuMingInfo == null) { huiYuanInfo = null; return; }

            huiYuanInfo = dal.Login(huiYuanId);

            if (huiYuanInfo == null) return;
            if (huiYuanInfo.Username != username) { huiYuanInfo = null; return; }
            if (huiYuanInfo.CompanyId != companyId) { huiYuanInfo = null; return; }

            huiYuanInfo.LoginTime = huiYuanInfo.LatestLoginTime.HasValue ? huiYuanInfo.LatestLoginTime.Value : DateTime.Now;

            dal.LoginLogwr(huiYuanInfo,1);

            SetHuiYuanCache(huiYuanInfo);
        }
        #endregion

        #region public members
        /// <summary>
        /// 获取当前域名信息
        /// </summary>
        /// <returns></returns>
        public static MWzYuMingInfo GetYuMingInfo()
        {
            string s = System.Web.HttpContext.Current.Request.Url.Host.ToLower();

            IDictionary<string, MWzYuMingInfo> domains = (IDictionary<string, MWzYuMingInfo>)EyouSoftCache.GetCache(EyouSoft.Cache.Tag.TagName.YlWzYuMings);
            MWzYuMingInfo info = null;
            domains = domains ?? new Dictionary<string, MWzYuMingInfo>();
            if (domains.ContainsKey(s))
            {
                info = domains[s];
            }
            else
            {
                IYlHuiYuanLogin dal = new DYlHuiYuanLogin();
                info = dal.GetYuMingInfo(s);
                if (info != null)
                {
                    domains.Add(s, info);
                    EyouSoft.Cache.Facade.EyouSoftCache.Add(EyouSoft.Cache.Tag.TagName.YlWzYuMings, domains);
                }
            }

            return info;
        }

        /// <summary>
        /// 用户登录，返回1登录成功
        /// </summary>
        /// <param name="companyId">系统公司编号</param>
        /// <param name="username">用户名</param>
        /// <param name="pwdInfo">登录密码</param>
        /// <param name="huiYuanInfo">登录会员信息</param>
        /// <param name="cookies_expires_lx">cookies过期类型 0:浏览器关闭，1:30天，2:30分钟</param>
        /// <returns></returns>
        public static int Login(string companyId, string username, MPasswordInfo pwdInfo, out MYlHuiYuanInfo huiYuanInfo, int cookies_expires_lx)
        {
            IYlHuiYuanLogin dal = new DYlHuiYuanLogin();
            huiYuanInfo = null;

            if (string.IsNullOrEmpty(companyId)) return 0;
            if (string.IsNullOrEmpty(username)) return -1;
            if (pwdInfo == null || string.IsNullOrEmpty(pwdInfo.MD5Password)) return -2;
            var yuMingInfo = GetYuMingInfo();
            if (yuMingInfo == null) return -3;

            huiYuanInfo = dal.Login(companyId, username, pwdInfo);

            if (huiYuanInfo == null)
            {
                return -3;
            }
            if (huiYuanInfo.Status !=  EyouSoft.Model.EnumType.YlStructure.HuiYuanStatus.可用)
            {
                huiYuanInfo = null;
                return -4;
            }

            huiYuanInfo.LoginTime = DateTime.Now;

            dal.LoginLogwr(huiYuanInfo, 0);

            SetHuiYuanCache(huiYuanInfo);
            SetHuiYuanCookies(huiYuanInfo, cookies_expires_lx);

            return 1;
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        public static void Logout()
        {
            string companyId = GetCookie(LoginCookieCompanyId);
            string huiYuanId = GetCookie(LoginCookieHuiYuanId);

            RemoveHuiYuanCache(huiYuanId);
            RemoveHuiYuanCookies();
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public static MYlHuiYuanInfo GetHuiYuanInfo()
        {
            MYlHuiYuanInfo huiYuanInfo = null;
            string companyId = GetCookie(LoginCookieCompanyId);
            string huiYuanId = GetCookie(LoginCookieHuiYuanId);
            string username = GetCookie(LoginCookieUsername);


            if (string.IsNullOrEmpty(companyId)
                || string.IsNullOrEmpty(huiYuanId)
                || string.IsNullOrEmpty(username))
            {
                return null;
            }

            //从缓存查询登录会员信息
            string cacheKey = string.Format(EyouSoft.Cache.Tag.TagName.YlWzHuiYuan,  huiYuanId);
            //从缓存查询登录会员信息计数器
            int getCacheCount = 2;

            do
            {
                huiYuanInfo = (MYlHuiYuanInfo)EyouSoft.Cache.Facade.EyouSoftCache.GetCache(cacheKey);
                getCacheCount--;
            } while (huiYuanInfo == null && getCacheCount > 0);

            //缓存中未找到登录会员信息，自动登录处理
            if (huiYuanInfo == null)
            {
                AutoLogin(companyId, huiYuanId, username, out huiYuanInfo);
            }

            if (huiYuanInfo == null) return null;

            return huiYuanInfo;
        }

        /// <summary>
        /// 会员是否登录
        /// </summary>
        /// <param name="huiYuanInfo">登录会员信息</param>
        /// <returns></returns>
        public static bool IsLogin(out MYlHuiYuanInfo huiYuanInfo)
        {
            huiYuanInfo = GetHuiYuanInfo();

            if (huiYuanInfo == null) return false;

            return true;
        }
        #endregion
    }
}

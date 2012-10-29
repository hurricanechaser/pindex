using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

namespace Nails
{
    public static class CookieUtil
    {
        static HttpContext context
        {
            get
            {
                return Common.context;
            }
        }

        public static bool CookieExists(string cookieName)
        {

            bool exists = false;
            HttpCookie cookie = context.Request.Cookies[cookieName];
            if (cookie != null)
                exists = true;
            return exists;
        }

        public static Dictionary<string, string> GetAllCookies()
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            foreach (string key in context.Request.Cookies.AllKeys)
            {
                cookies.Add(key, context.Request.Cookies[key].Value);
            }
            return cookies;
        }

        public static void DeleteAllCookies()
        {
            var x = context.Request.Cookies;
            foreach (HttpCookie cook in x)
            {
                DeleteCookie(cook.Name);
            }
        }

        public static string ReadCookie(string cookieName)
        {
            HttpCookie tmp = context.Request.Cookies[cookieName];
            return tmp == null ? null : tmp.Value;
        }

        public static void WriteCookie(string cookieName, string cookieValue, bool isHttpCookie)
        {
            HttpCookie aCookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                HttpOnly = isHttpCookie
            };
            if (CookieExists(cookieName)) context.Response.SetCookie(aCookie);
            else
                context.Response.Cookies.Add(aCookie);
        }

        public static void WriteCookie(string cookieName, string cookieValue, bool isHttpCookie, DateTime cookieExpireDate)
        {
            HttpCookie aCookie = new HttpCookie(cookieName)
            {
                Value = cookieValue,
                Expires = cookieExpireDate,
                HttpOnly = isHttpCookie
            };
            context.Response.Cookies.Add(aCookie);
        }
        public static void DeleteCookie(string cookieName)
        {
            HttpCookie aCookie = new HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            context.Response.Cookies.Add(aCookie);
        }
    }
}
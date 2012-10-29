using System;
using System.Configuration;

namespace Nails
{
	public static partial class Common
	{
	
		public static int  PageSize
		{
			get
			{
				return    int.Parse(ConfigurationManager.AppSettings["PageSize"]);
			}
		}
	
		public static int  PassMinChars
		{
			get
			{
				return    int.Parse(ConfigurationManager.AppSettings["PassMinChars"]);
			}
		}
	
		public static string  AppCookies
		{
			get
			{
				return    ConfigurationManager.AppSettings["AppCookies"];
			}
		}
	
		public static string  UserBlankImg
		{
			get
			{
				return    ConfigurationManager.AppSettings["UserBlankImg"];
			}
		}
	
		public static string  CDN
		{
			get
			{
				return    ConfigurationManager.AppSettings["CDN"];
			}
		}
	
		public static int  RWPointsDed
		{
			get
			{
				return    int.Parse(ConfigurationManager.AppSettings["RWPointsDed"]);
			}
		}
	
		public static string  DefaultPassword
		{
			get
			{
				return    ConfigurationManager.AppSettings["DefaultPassword"];
			}
		}
	
		public static string  Domain
		{
			get
			{
				return    ConfigurationManager.AppSettings["Domain"];
			}
		}
	
		public static string  ImagePath
		{
			get
			{
				return    ConfigurationManager.AppSettings["ImagePath"];
			}
		}
	
		public static string  UploadedImagePath
		{
			get
			{
				return    ConfigurationManager.AppSettings["UploadedImagePath"];
			}
		}
	
		public static string  UploadedImageRelPath
		{
			get
			{
				return    ConfigurationManager.AppSettings["UploadedImageRelPath"];
			}
		}
	
		public static string  AuthCookie
		{
			get
			{
				return    ConfigurationManager.AppSettings["AuthCookie"];
			}
		}
	
		public static string  InfoCookie
		{
			get
			{
				return    ConfigurationManager.AppSettings["InfoCookie"];
			}
		}
	
		public static string  Temp
		{
			get
			{
				return    ConfigurationManager.AppSettings["Temp"];
			}
		}
	
		public static string  RelTemp
		{
			get
			{
				return    ConfigurationManager.AppSettings["RelTemp"];
			}
		}
	
		public static string  ContentUrl
		{
			get
			{
				return    ConfigurationManager.AppSettings["ContentUrl"];
			}
		}
		public static String DataConnectionString
		{
			get
			{
				return ConfigurationManager.ConnectionStrings["DataConnectionString"].ConnectionString;
			}
		}
	}
}


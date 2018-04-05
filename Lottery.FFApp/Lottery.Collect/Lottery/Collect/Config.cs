using System;
using System.Configuration;

namespace Lottery.Collect
{
	public class Config
	{
		public static string DefaultUrl
		{
			get
			{
				return Config._DefaultUrl;
			}
			set
			{
				Config._DefaultUrl = value;
			}
		}

		public static string DefaultUrlYoule
		{
			get
			{
				return Config._DefaultUrlYoule;
			}
			set
			{
				Config._DefaultUrlYoule = value;
			}
		}

        private static string _DefaultUrl = string.Concat(ConfigurationManager.AppSettings["RootUrl"].ToString(), "/Data/hisStory.xml");

        private static string _DefaultUrlYoule = string.Concat(ConfigurationManager.AppSettings["RootUrl"].ToString(), "/Data/lottery{0}.xml");
	}
}

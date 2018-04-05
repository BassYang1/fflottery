using System;
using System.ComponentModel;

namespace Web.MIL.Html
{
	public class HtmlAttribute
	{
		public HtmlAttribute()
		{
			this.mName = "Unnamed";
			this.mValue = "";
		}

		public HtmlAttribute(string name, string value)
		{
			this.mName = name;
			this.mValue = value;
		}

		public override string ToString()
		{
			string result;
			if (this.mValue == null)
			{
				result = this.mName;
			}
			else
			{
				result = this.mName + "=\"" + this.mValue + "\"";
			}
			return result;
		}

		[Category("Output"), Description("The HTML to represent this attribute")]
		public string HTML
		{
			get
			{
				string result;
				if (this.mValue == null)
				{
					result = this.mName;
				}
				else
				{
					result = this.mName + "=\"" + HtmlEncoder.EncodeValue(this.mValue) + "\"";
				}
				return result;
			}
		}

		[Description("The name of the attribute"), Category("General")]
		public string Name
		{
			get
			{
				return this.mName;
			}
			set
			{
				this.mName = value;
			}
		}

		[Category("General"), Description("The value of the attribute")]
		public string Value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
			}
		}

		[Description("The XHTML to represent this attribute"), Category("Output")]
		public string XHTML
		{
			get
			{
				string result;
				if (this.mValue == null)
				{
					result = this.mName.ToLower();
				}
				else
				{
					result = this.mName + "=\"" + HtmlEncoder.EncodeValue(this.mValue.ToLower()) + "\"";
				}
				return result;
			}
		}

		protected string mName;

		protected string mValue;
	}
}

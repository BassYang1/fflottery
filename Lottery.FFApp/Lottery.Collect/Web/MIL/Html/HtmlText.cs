using System;
using System.ComponentModel;

namespace Web.MIL.Html
{
	public class HtmlText : HtmlNode
	{
		public HtmlText(string text)
		{
			this.mText = text;
		}

		public override string ToString()
		{
			return this.Text;
		}

		public override string HTML
		{
			get
			{
				string result;
				if (this.NoEscaping)
				{
					result = this.Text;
				}
				else
				{
					result = HtmlEncoder.EncodeValue(this.Text);
				}
				return result;
			}
		}

		internal bool NoEscaping
		{
			get
			{
				return this.mParent != null && this.mParent.NoEscaping;
			}
		}

		[Description("The text located in this text node"), Category("General")]
		public string Text
		{
			get
			{
				return this.mText;
			}
			set
			{
				this.mText = value;
			}
		}

		public override string XHTML
		{
			get
			{
				return HtmlEncoder.EncodeValue(this.Text);
			}
		}

		protected string mText;
	}
}

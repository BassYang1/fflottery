using System;
using System.ComponentModel;
using System.Text;

namespace Web.MIL.Html
{
	public class HtmlDocument
	{
		internal HtmlDocument(string html, bool wantSpaces)
		{
			this.mNodes = new HtmlParser
			{
				RemoveEmptyElementText = !wantSpaces
			}.Parse(html);
		}

		public static HtmlDocument Create(string html)
		{
			return new HtmlDocument(html, false);
		}

		public static HtmlDocument Create(string html, bool wantSpaces)
		{
			return new HtmlDocument(html, wantSpaces);
		}

		[Category("General"), Description("This is the DOCTYPE for XHTML production")]
		public string DocTypeXHTML
		{
			get
			{
				return this.mXhtmlHeader;
			}
			set
			{
				this.mXhtmlHeader = value;
			}
		}

		[Description("The HTML version of this document"), Category("Output")]
		public string HTML
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (HtmlNode htmlNode in this.Nodes)
				{
					stringBuilder.Append(htmlNode.HTML);
				}
				return stringBuilder.ToString();
			}
		}

		public HtmlNodeCollection Nodes
		{
			get
			{
				return this.mNodes;
			}
		}

		[Description("The XHTML version of this document"), Category("Output")]
		public string XHTML
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.mXhtmlHeader != null)
				{
					stringBuilder.Append(this.mXhtmlHeader);
					stringBuilder.Append("\r\n");
				}
				foreach (HtmlNode htmlNode in this.Nodes)
				{
					stringBuilder.Append(htmlNode.XHTML);
				}
				return stringBuilder.ToString();
			}
		}

		private HtmlNodeCollection mNodes = new HtmlNodeCollection(null);

		private string mXhtmlHeader = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">";
	}
}

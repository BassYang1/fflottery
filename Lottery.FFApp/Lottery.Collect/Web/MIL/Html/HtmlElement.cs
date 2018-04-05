using System;
using System.ComponentModel;
using System.Text;

namespace Web.MIL.Html
{
	public class HtmlElement : HtmlNode
	{
		public HtmlElement(string name)
		{
			this.mNodes = new HtmlNodeCollection(this);
			this.mAttributes = new HtmlAttributeCollection(this);
			this.mName = name;
			this.mIsTerminated = false;
		}

		public override string ToString()
		{
			string str = "<" + this.mName;
			foreach (HtmlAttribute htmlAttribute in this.Attributes)
			{
				str = str + " " + htmlAttribute.ToString();
			}
			return str + ">";
		}

		[Description("The set of attributes associated with this element"), Category("General")]
		public HtmlAttributeCollection Attributes
		{
			get
			{
				return this.mAttributes;
			}
		}

		[Category("Output")]
		public override string HTML
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<" + this.mName);
				foreach (HtmlAttribute htmlAttribute in this.Attributes)
				{
					stringBuilder.Append(" " + htmlAttribute.HTML);
				}
				if (this.Nodes.Count > 0)
				{
					stringBuilder.Append(">");
					foreach (HtmlNode htmlNode in this.Nodes)
					{
						stringBuilder.Append(htmlNode.HTML);
					}
					stringBuilder.Append("</" + this.mName + ">");
				}
				else if (this.IsExplicitlyTerminated)
				{
					stringBuilder.Append("></" + this.mName + ">");
				}
				else if (this.IsTerminated)
				{
					stringBuilder.Append("/>");
				}
				else
				{
					stringBuilder.Append(">");
				}
				return stringBuilder.ToString();
			}
		}

		internal bool IsExplicitlyTerminated
		{
			get
			{
				return this.mIsExplicitlyTerminated;
			}
			set
			{
				this.mIsExplicitlyTerminated = value;
			}
		}

		internal bool IsTerminated
		{
			get
			{
				return this.Nodes.Count <= 0 && (this.mIsTerminated | this.mIsExplicitlyTerminated);
			}
			set
			{
				this.mIsTerminated = value;
			}
		}

		[Description("The name of the tag/element"), Category("General")]
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

		[Category("General"), Description("The set of child nodes")]
		public HtmlNodeCollection Nodes
		{
			get
			{
				if (base.IsText())
				{
					throw new InvalidOperationException("An HtmlText node does not have child nodes");
				}
				return this.mNodes;
			}
		}

		internal bool NoEscaping
		{
			get
			{
				return "script".Equals(this.Name.ToLower()) || "style".Equals(this.Name.ToLower());
			}
		}

		[Category("General"), Description("A concatination of all the text associated with this element")]
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (HtmlNode htmlNode in this.Nodes)
				{
					if (htmlNode is HtmlText)
					{
						stringBuilder.Append(((HtmlText)htmlNode).Text);
					}
				}
				return stringBuilder.ToString();
			}
		}

		[Category("Output")]
		public override string XHTML
		{
			get
			{
				if ("html".Equals(this.mName) && this.Attributes["xmlns"] == null)
				{
					this.Attributes.Add(new HtmlAttribute("xmlns", "http://www.w3.org/1999/xhtml"));
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<" + this.mName.ToLower());
				foreach (HtmlAttribute htmlAttribute in this.Attributes)
				{
					stringBuilder.Append(" " + htmlAttribute.XHTML);
				}
				if (this.IsTerminated)
				{
					stringBuilder.Append("/>");
				}
				else if (this.Nodes.Count > 0)
				{
					stringBuilder.Append(">");
					foreach (HtmlNode htmlNode in this.Nodes)
					{
						stringBuilder.Append(htmlNode.XHTML);
					}
					stringBuilder.Append("</" + this.mName.ToLower() + ">");
				}
				else
				{
					stringBuilder.Append("/>");
				}
				return stringBuilder.ToString();
			}
		}

		protected HtmlAttributeCollection mAttributes;

		protected bool mIsExplicitlyTerminated;

		protected bool mIsTerminated;

		protected string mName;

		protected HtmlNodeCollection mNodes;
	}
}

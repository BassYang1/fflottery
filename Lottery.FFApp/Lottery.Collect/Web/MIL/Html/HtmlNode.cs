using System;
using System.ComponentModel;

namespace Web.MIL.Html
{
	public abstract class HtmlNode
	{
		[Category("Relationships")]
		public HtmlNode GetCommonAncestor(HtmlNode node)
		{
			HtmlNode result;
			for (HtmlNode htmlNode = this; htmlNode != null; htmlNode = htmlNode.Parent)
			{
				for (HtmlNode htmlNode2 = node; htmlNode2 != null; htmlNode2 = htmlNode2.Parent)
				{
					if (htmlNode == htmlNode2)
					{
						result = htmlNode;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		[Category("Relationships")]
		public bool IsAncestorOf(HtmlNode node)
		{
			return node.IsDescendentOf(this);
		}

		[Category("Relationships")]
		public bool IsDescendentOf(HtmlNode node)
		{
			bool result;
			for (HtmlNode parent = this.mParent; parent != null; parent = parent.Parent)
			{
				if (parent == node)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		[Description("This is true if this is an element node"), Category("General")]
		public bool IsElement()
		{
			return this is HtmlElement;
		}

		[Description("This is true if this is a text node"), Category("General")]
		public bool IsText()
		{
			return this is HtmlText;
		}

		[Category("General")]
		public void Remove()
		{
			if (this.mParent != null)
			{
				this.mParent.Nodes.RemoveAt(this.Index);
			}
		}

		internal void SetParent(HtmlElement parentNode)
		{
			this.mParent = parentNode;
		}

		public abstract override string ToString();

		[Description("The first child of this node"), Category("Navigation")]
		public HtmlNode FirstChild
		{
			get
			{
				HtmlNode result;
				if (this is HtmlElement)
				{
					if (((HtmlElement)this).Nodes.Count == 0)
					{
						result = null;
					}
					else
					{
						result = ((HtmlElement)this).Nodes[0];
					}
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		[Category("Output"), Description("The HTML that represents this node and all the children")]
		public abstract string HTML
		{
			get;
		}

		[Description("The zero-based index of this node in the parent's nodes collection"), Category("Navigation")]
		public int Index
		{
			get
			{
				int result;
				if (this.mParent == null)
				{
					result = -1;
				}
				else
				{
					result = this.mParent.Nodes.IndexOf(this);
				}
				return result;
			}
		}

		[Category("Navigation"), Description("Is this node a child of another?")]
		public bool IsChild
		{
			get
			{
				return this.mParent != null;
			}
		}

		[Category("Navigation"), Description("Does this node have any children?")]
		public bool IsParent
		{
			get
			{
				return this is HtmlElement && ((HtmlElement)this).Nodes.Count > 0;
			}
		}

		[Description("Is this node a root node?"), Category("Navigation")]
		public bool IsRoot
		{
			get
			{
				return this.mParent == null;
			}
		}

		[Category("Navigation"), Description("The last child of this node")]
		public HtmlNode LastChild
		{
			get
			{
				HtmlNode result;
				if (this is HtmlElement)
				{
					if (((HtmlElement)this).Nodes.Count == 0)
					{
						result = null;
					}
					else
					{
						result = ((HtmlElement)this).Nodes[((HtmlElement)this).Nodes.Count - 1];
					}
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		[Description("The next sibling node"), Category("Navigation")]
		public HtmlNode Next
		{
			get
			{
				HtmlNode result;
				if (this.Index != -1 && this.Parent.Nodes.Count > this.Index + 1)
				{
					result = this.Parent.Nodes[this.Index + 1];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		[Description("The parent node of this one"), Category("Navigation")]
		public HtmlElement Parent
		{
			get
			{
				return this.mParent;
			}
		}

		[Description("The previous sibling node"), Category("Navigation")]
		public HtmlNode Previous
		{
			get
			{
				HtmlNode result;
				if (this.Index != -1 && this.Index > 0)
				{
					result = this.Parent.Nodes[this.Index - 1];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		[Description("The XHTML that represents this node and all the children"), Category("Output")]
		public abstract string XHTML
		{
			get;
		}

		protected HtmlElement mParent = null;
	}
}

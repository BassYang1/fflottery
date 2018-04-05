using System;
using System.Collections;

namespace Web.MIL.Html
{
	public class HtmlNodeCollection : CollectionBase
	{
		public HtmlNodeCollection()
		{
			this.mParent = null;
		}

		internal HtmlNodeCollection(HtmlElement parent)
		{
			this.mParent = parent;
		}

		public int Add(HtmlNode node)
		{
			if (this.mParent != null)
			{
				node.SetParent(this.mParent);
			}
			return base.List.Add(node);
		}

		public HtmlNodeCollection FindByAttributeName(string attributeName)
		{
			return this.FindByAttributeName(attributeName, true);
		}

		public HtmlNodeCollection FindByAttributeName(string attributeName, bool searchChildren)
		{
			HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection(null);
			foreach (HtmlNode htmlNode in base.List)
			{
				if (htmlNode is HtmlElement)
				{
					foreach (HtmlAttribute htmlAttribute in ((HtmlElement)htmlNode).Attributes)
					{
						if (htmlAttribute.Name.ToLower().Equals(attributeName.ToLower()))
						{
							htmlNodeCollection.Add(htmlNode);
							break;
						}
					}
					if (searchChildren)
					{
						foreach (HtmlNode node in ((HtmlElement)htmlNode).Nodes.FindByAttributeName(attributeName, searchChildren))
						{
							htmlNodeCollection.Add(node);
						}
					}
				}
			}
			return htmlNodeCollection;
		}

		public HtmlNodeCollection FindByAttributeNameValue(string attributeName, string attributeValue, bool isLike)
		{
			return this.FindByAttributeNameValue(attributeName, attributeValue, isLike, true);
		}

		public HtmlNodeCollection FindByAttributeNameValue(string attributeName, string attributeValue, bool isLike, bool searchChildren)
		{
			HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection(null);
			foreach (HtmlNode htmlNode in base.List)
			{
				if (htmlNode is HtmlElement)
				{
					foreach (HtmlAttribute htmlAttribute in ((HtmlElement)htmlNode).Attributes)
					{
						if (htmlAttribute.Name.ToLower().Equals(attributeName.ToLower()))
						{
							if (isLike)
							{
								if (htmlAttribute.Value.ToLower().StartsWith(attributeValue.ToLower()))
								{
									htmlNodeCollection.Add(htmlNode);
								}
							}
							else if (htmlAttribute.Value.ToLower().Equals(attributeValue.ToLower()))
							{
								htmlNodeCollection.Add(htmlNode);
							}
							break;
						}
					}
					if (searchChildren)
					{
						foreach (HtmlNode node in ((HtmlElement)htmlNode).Nodes.FindByAttributeNameValue(attributeName, attributeValue, isLike, searchChildren))
						{
							htmlNodeCollection.Add(node);
						}
					}
				}
			}
			return htmlNodeCollection;
		}

		public HtmlNodeCollection FindByName(string name)
		{
			return this.FindByName(name, true);
		}

		public HtmlNodeCollection FindByName(string name, bool searchChildren)
		{
			HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection(null);
			foreach (HtmlNode htmlNode in base.List)
			{
				if (htmlNode is HtmlElement)
				{
					if (((HtmlElement)htmlNode).Name.ToLower().Equals(name.ToLower()))
					{
						htmlNodeCollection.Add(htmlNode);
					}
					if (searchChildren)
					{
						foreach (HtmlNode node in ((HtmlElement)htmlNode).Nodes.FindByName(name, searchChildren))
						{
							htmlNodeCollection.Add(node);
						}
					}
				}
			}
			return htmlNodeCollection;
		}

		public int IndexOf(HtmlNode node)
		{
			return base.List.IndexOf(node);
		}

		public void Insert(int index, HtmlNode node)
		{
			if (this.mParent != null)
			{
				node.SetParent(this.mParent);
			}
			base.InnerList.Insert(index, node);
		}

		public HtmlNode this[string name]
		{
			get
			{
				HtmlNodeCollection htmlNodeCollection = this.FindByName(name, false);
				HtmlNode result;
				if (htmlNodeCollection.Count > 0)
				{
					result = htmlNodeCollection[0];
				}
				else
				{
					result = null;
				}
				return result;
			}
		}

		public HtmlNode this[int index]
		{
			get
			{
				return (HtmlNode)base.InnerList[index];
			}
			set
			{
				if (this.mParent != null)
				{
					value.SetParent(this.mParent);
				}
				base.InnerList[index] = value;
			}
		}

		private HtmlElement mParent;
	}
}

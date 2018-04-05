using System;
using System.Collections;

namespace Web.MIL.Html
{
	public class HtmlAttributeCollection : CollectionBase
	{
		public HtmlAttributeCollection()
		{
			this.mElement = null;
		}

		internal HtmlAttributeCollection(HtmlElement element)
		{
			this.mElement = element;
		}

		public int Add(HtmlAttribute attribute)
		{
			return base.List.Add(attribute);
		}

		public HtmlAttribute FindByName(string name)
		{
			HtmlAttribute result;
			if (this.IndexOf(name) == -1)
			{
				result = null;
			}
			else
			{
				result = this[this.IndexOf(name)];
			}
			return result;
		}

		public int IndexOf(string name)
		{
			int result;
			for (int i = 0; i < base.List.Count; i++)
			{
				if (this[i].Name.ToLower().Equals(name.ToLower()))
				{
					result = i;
					return result;
				}
			}
			result = -1;
			return result;
		}

		public HtmlAttribute this[string name]
		{
			get
			{
				return this.FindByName(name);
			}
		}

		public HtmlAttribute this[int index]
		{
			get
			{
				return (HtmlAttribute)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		private HtmlElement mElement;
	}
}

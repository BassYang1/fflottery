using System;
using System.Collections.Specialized;
using System.Text;

namespace Web.MIL.Html
{
	internal class HtmlParser
	{
		private static string DecodeScript(string script)
		{
			return script.Replace("[MIL-SCRIPT-LT]", "<").Replace("[MIL-SCRIPT-GT]", ">").Replace("[MIL-SCRIPT-CR]", "\r").Replace("[MIL-SCRIPT-LF]", "\n");
		}

		private static string EncodeScript(string script)
		{
			return script.Replace("<", "[MIL-SCRIPT-LT]").Replace(">", "[MIL-SCRIPT-GT]").Replace("\r", "[MIL-SCRIPT-CR]").Replace("\n", "[MIL-SCRIPT-LF]");
		}

		private int FindTagOpenNodeIndex(HtmlNodeCollection nodes, string name)
		{
			int result;
			for (int i = nodes.Count - 1; i >= 0; i--)
			{
				if (nodes[i] is HtmlElement && ((HtmlElement)nodes[i]).Name.ToLower().Equals(name.ToLower()) && ((HtmlElement)nodes[i]).Nodes.Count == 0 && !((HtmlElement)nodes[i]).IsTerminated)
				{
					result = i;
					return result;
				}
			}
			result = -1;
			return result;
		}

		private StringCollection GetTokens(string input)
		{
			StringCollection stringCollection = new StringCollection();
			int i = 0;
			HtmlParser.ParseStatus parseStatus = HtmlParser.ParseStatus.ReadText;
			StringCollection result;
			while (i < input.Length)
			{
				switch (parseStatus)
				{
				case HtmlParser.ParseStatus.ReadText:
					if (i + 2 < input.Length && input.Substring(i, 2).Equals("</"))
					{
						i += 2;
						stringCollection.Add("</");
						parseStatus = HtmlParser.ParseStatus.ReadEndTag;
					}
					else if (input.Substring(i, 1).Equals("<"))
					{
						i++;
						stringCollection.Add("<");
						parseStatus = HtmlParser.ParseStatus.ReadStartTag;
					}
					else
					{
						int num = input.IndexOf("<", i);
						if (num == -1)
						{
							stringCollection.Add(input.Substring(i));
							result = stringCollection;
							return result;
						}
						stringCollection.Add(input.Substring(i, num - i));
						i = num;
					}
					break;
				case HtmlParser.ParseStatus.ReadEndTag:
				{
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
					{
						i++;
					}
					int num2 = i;
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t>".ToCharArray()) == -1)
					{
						i++;
					}
					stringCollection.Add(input.Substring(num2, i - num2));
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
					{
						i++;
					}
					if (i < input.Length && input.Substring(i, 1).Equals(">"))
					{
						stringCollection.Add(">");
						parseStatus = HtmlParser.ParseStatus.ReadText;
						i++;
					}
					break;
				}
				case HtmlParser.ParseStatus.ReadStartTag:
				{
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
					{
						i++;
					}
					int num2 = i;
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t/>".ToCharArray()) == -1)
					{
						i++;
					}
					stringCollection.Add(input.Substring(num2, i - num2));
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
					{
						i++;
					}
					if (i + 1 < input.Length && input.Substring(i, 1).Equals("/>"))
					{
						stringCollection.Add("/>");
						parseStatus = HtmlParser.ParseStatus.ReadText;
						i += 2;
					}
					else if (i < input.Length && input.Substring(i, 1).Equals(">"))
					{
						stringCollection.Add(">");
						parseStatus = HtmlParser.ParseStatus.ReadText;
						i++;
					}
					else
					{
						parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
					}
					break;
				}
				case HtmlParser.ParseStatus.ReadAttributeName:
				{
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
					{
						i++;
					}
					int num3 = i;
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t/>=".ToCharArray()) == -1)
					{
						i++;
					}
					stringCollection.Add(input.Substring(num3, i - num3));
					while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
					{
						i++;
					}
					if (i + 1 < input.Length && input.Substring(i, 2).Equals("/>"))
					{
						stringCollection.Add("/>");
						parseStatus = HtmlParser.ParseStatus.ReadText;
						i += 2;
					}
					else if (i < input.Length && input.Substring(i, 1).Equals(">"))
					{
						stringCollection.Add(">");
						parseStatus = HtmlParser.ParseStatus.ReadText;
						i++;
					}
					else if (i < input.Length && input.Substring(i, 1).Equals("="))
					{
						stringCollection.Add("=");
						i++;
						parseStatus = HtmlParser.ParseStatus.ReadAttributeValue;
					}
					else if (i < input.Length && input.Substring(i, 1).Equals("/"))
					{
						i++;
					}
					break;
				}
				default:
					if (parseStatus == HtmlParser.ParseStatus.ReadAttributeValue)
					{
						while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
						{
							i++;
						}
						if (i < input.Length && input.Substring(i, 1).Equals("\""))
						{
							int num4 = i;
							i++;
							while (i < input.Length && !input.Substring(i, 1).Equals("\""))
							{
								i++;
							}
							if (i < input.Length && input.Substring(i, 1).Equals("\""))
							{
								i++;
							}
							stringCollection.Add(input.Substring(num4 + 1, i - num4 - 2));
							parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
						}
						else if (i < input.Length && input.Substring(i, 1).Equals("'"))
						{
							int num4 = i;
							i++;
							while (i < input.Length && !input.Substring(i, 1).Equals("'"))
							{
								i++;
							}
							if (i < input.Length && input.Substring(i, 1).Equals("'"))
							{
								i++;
							}
							stringCollection.Add(input.Substring(num4 + 1, i - num4 - 2));
							parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
						}
						else
						{
							int num4 = i;
							while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t/>".ToCharArray()) == -1)
							{
								i++;
							}
							stringCollection.Add(input.Substring(num4, i - num4));
							while (i < input.Length && input.Substring(i, 1).IndexOfAny(HtmlParser.WHITESPACE_CHARS) != -1)
							{
								i++;
							}
							parseStatus = HtmlParser.ParseStatus.ReadAttributeName;
						}
						if (i + 1 < input.Length && input.Substring(i, 2).Equals("/>"))
						{
							stringCollection.Add("/>");
							parseStatus = HtmlParser.ParseStatus.ReadText;
							i += 2;
						}
						else if (i < input.Length && input.Substring(i, 1).Equals(">"))
						{
							stringCollection.Add(">");
							i++;
							parseStatus = HtmlParser.ParseStatus.ReadText;
						}
					}
					break;
				}
			}
			result = stringCollection;
			return result;
		}

		private void MoveNodesDown(ref HtmlNodeCollection nodes, int node_index, HtmlElement new_parent)
		{
			for (int i = node_index; i < nodes.Count; i++)
			{
				new_parent.Nodes.Add(nodes[i]);
				nodes[i].SetParent(new_parent);
			}
			int count = nodes.Count;
			for (int i = node_index; i < count; i++)
			{
				nodes.RemoveAt(node_index);
			}
			new_parent.IsExplicitlyTerminated = true;
		}

		public HtmlNodeCollection Parse(string html)
		{
			HtmlNodeCollection htmlNodeCollection = new HtmlNodeCollection(null);
			html = this.PreprocessScript(html, "script");
			html = this.PreprocessScript(html, "style");
			html = this.RemoveComments(html);
			html = this.RemoveSGMLComments(html);
			StringCollection tokens = this.GetTokens(html);
			int i = 0;
			HtmlElement htmlElement = null;
			HtmlNodeCollection result;
			while (i < tokens.Count)
			{
				if ("<".Equals(tokens[i]))
				{
					i++;
					if (i >= tokens.Count)
					{
						result = htmlNodeCollection;
						return result;
					}
					string name = tokens[i];
					i++;
					htmlElement = new HtmlElement(name);
					while (i < tokens.Count && !">".Equals(tokens[i]) && !"/>".Equals(tokens[i]))
					{
						string name2 = tokens[i];
						i++;
						if (i < tokens.Count && "=".Equals(tokens[i]))
						{
							i++;
							string value;
							if (i < tokens.Count)
							{
								value = tokens[i];
							}
							else
							{
								value = null;
							}
							i++;
							HtmlAttribute attribute = new HtmlAttribute(name2, HtmlEncoder.DecodeValue(value));
							htmlElement.Attributes.Add(attribute);
						}
						else if (i < tokens.Count)
						{
							HtmlAttribute attribute = new HtmlAttribute(name2, null);
							htmlElement.Attributes.Add(attribute);
						}
					}
					htmlNodeCollection.Add(htmlElement);
					if (i < tokens.Count && "/>".Equals(tokens[i]))
					{
						htmlElement.IsTerminated = true;
						i++;
						htmlElement = null;
					}
					else if (i < tokens.Count && ">".Equals(tokens[i]))
					{
						i++;
					}
				}
				else if (">".Equals(tokens[i]))
				{
					i++;
				}
				else if ("</".Equals(tokens[i]))
				{
					i++;
					if (i >= tokens.Count)
					{
						result = htmlNodeCollection;
						return result;
					}
					string name = tokens[i];
					i++;
					int num = this.FindTagOpenNodeIndex(htmlNodeCollection, name);
					if (num != -1)
					{
						this.MoveNodesDown(ref htmlNodeCollection, num + 1, (HtmlElement)htmlNodeCollection[num]);
					}
					while (i < tokens.Count && !">".Equals(tokens[i]))
					{
						i++;
					}
					if (i < tokens.Count && ">".Equals(tokens[i]))
					{
						i++;
					}
					htmlElement = null;
				}
				else
				{
					string text = tokens[i];
					if (this.mRemoveEmptyElementText)
					{
						text = this.RemoveWhitespace(text);
					}
					text = HtmlParser.DecodeScript(text);
					if (!this.mRemoveEmptyElementText || text.Length != 0)
					{
						if (htmlElement == null || !htmlElement.NoEscaping)
						{
							text = HtmlEncoder.DecodeValue(text);
						}
						HtmlText node = new HtmlText(text);
						htmlNodeCollection.Add(node);
					}
					i++;
				}
			}
			result = htmlNodeCollection;
			return result;
		}

		private string PreprocessScript(string input, string tag_name)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			int length = tag_name.Length;
			while (i < input.Length)
			{
				bool flag = false;
				if (i + length + 1 < input.Length && input.Substring(i, length + 1).ToLower().Equals("<" + tag_name))
				{
					while (i < input.Length)
					{
						if (input.Substring(i, 1).Equals(">"))
						{
							stringBuilder.Append(">");
							i++;
						}
						else
						{
							if (i + 1 >= input.Length || !input.Substring(i, 2).Equals("/>"))
							{
								if (input.Substring(i, 1).Equals("\""))
								{
									stringBuilder.Append("\"");
									i++;
									while (i < input.Length && !input.Substring(i, 1).Equals("\""))
									{
										stringBuilder.Append(input.Substring(i, 1));
										i++;
									}
									if (i < input.Length)
									{
										i++;
										stringBuilder.Append("\"");
									}
								}
								else if (input.Substring(i, 1).Equals("'"))
								{
									stringBuilder.Append("'");
									i++;
									while (i < input.Length && !input.Substring(i, 1).Equals("'"))
									{
										stringBuilder.Append(input.Substring(i, 1));
										i++;
									}
									if (i < input.Length)
									{
										i++;
										stringBuilder.Append("'");
									}
								}
								else
								{
									stringBuilder.Append(input.Substring(i, 1));
									i++;
								}
								continue;
							}
							stringBuilder.Append("/>");
							i += 2;
							flag = true;
						}
                        break;
					}
                    if (i >= input.Length)
                    {
                        goto IL_314;
                    }
                    if (!flag)
                    {
                        StringBuilder stringBuilder2 = new StringBuilder();
                        while (i + length + 3 < input.Length && !input.Substring(i, length + 3).ToLower().Equals("</" + tag_name + ">"))
                        {
                            stringBuilder2.Append(input.Substring(i, 1));
                            i++;
                        }
                        stringBuilder.Append(HtmlParser.EncodeScript(stringBuilder2.ToString()));
                        stringBuilder.Append("</" + tag_name + ">");
                        if (i + length + 3 < input.Length)
                        {
                            i += length + 3;
                        }
                    }
                    goto IL_301;
                }
				stringBuilder.Append(input.Substring(i, 1));
				i++;
				IL_301:;
			}
			IL_314:
			return stringBuilder.ToString();
		}

		private string RemoveComments(string input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			bool flag = false;
			while (i < input.Length)
			{
				if (i + 4 < input.Length && input.Substring(i, 4).Equals("<!--"))
				{
					i += 4;
					i = input.IndexOf("-->", i);
					if (i == -1)
					{
						break;
					}
					i += 3;
				}
				else if (input.Substring(i, 1).Equals("<"))
				{
					flag = true;
					stringBuilder.Append("<");
					i++;
				}
				else if (input.Substring(i, 1).Equals(">"))
				{
					flag = false;
					stringBuilder.Append(">");
					i++;
				}
				else if (input.Substring(i, 1).Equals("\"") && flag)
				{
					int num = i;
					i++;
					i = input.IndexOf("\"", i);
					if (i == -1)
					{
						break;
					}
					i++;
					stringBuilder.Append(input.Substring(num, i - num));
				}
				else if (input.Substring(i, 1).Equals("'") && flag)
				{
					int num = i;
					i++;
					i = input.IndexOf("'", i);
					if (i == -1)
					{
						break;
					}
					i++;
					stringBuilder.Append(input.Substring(num, i - num));
				}
				else
				{
					stringBuilder.Append(input.Substring(i, 1));
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		private string RemoveSGMLComments(string input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			bool flag = false;
			while (i < input.Length)
			{
				if (i + 2 < input.Length && input.Substring(i, 2).Equals("<!"))
				{
					i += 2;
					i = input.IndexOf(">", i);
					if (i == -1)
					{
						break;
					}
					i += 3;
				}
				else if (input.Substring(i, 1).Equals("<"))
				{
					flag = true;
					stringBuilder.Append("<");
					i++;
				}
				else if (input.Substring(i, 1).Equals(">"))
				{
					flag = false;
					stringBuilder.Append(">");
					i++;
				}
				else if (input.Substring(i, 1).Equals("\"") && flag)
				{
					int num = i;
					i++;
					i = input.IndexOf("\"", i);
					if (i == -1)
					{
						break;
					}
					i++;
					stringBuilder.Append(input.Substring(num, i - num));
				}
				else if (input.Substring(i, 1).Equals("'") && flag)
				{
					int num = i;
					i++;
					i = input.IndexOf("'", i);
					if (i == -1)
					{
						break;
					}
					i++;
					stringBuilder.Append(input.Substring(num, i - num));
				}
				else
				{
					stringBuilder.Append(input.Substring(i, 1));
					i++;
				}
			}
			return stringBuilder.ToString();
		}

		private string RemoveWhitespace(string input)
		{
			return input.Replace("\r", "").Replace("\n", "").Replace("\t", " ").Trim();
		}

		public bool RemoveEmptyElementText
		{
			get
			{
				return this.mRemoveEmptyElementText;
			}
			set
			{
				this.mRemoveEmptyElementText = value;
			}
		}

		private bool mRemoveEmptyElementText = false;

		private static char[] WHITESPACE_CHARS = " \t\r\n".ToCharArray();

		private enum ParseStatus
		{
			ReadText,
			ReadEndTag,
			ReadStartTag,
			ReadAttributeName,
			ReadAttributeValue
		}
	}
}

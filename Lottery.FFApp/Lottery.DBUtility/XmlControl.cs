// Decompiled with JetBrains decompiler
// Type: Lottery.DBUtility.XmlControl
// Assembly: Lottery.DBUtility, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: 41391965-66A5-4DE4-8203-13B298F4A572
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.DBUtility.dll

using System;
using System.Data;
using System.IO;
using System.Xml;

namespace Lottery.DBUtility
{
  public class XmlControl
  {
    private XmlDocument _objXmlDoc = new XmlDocument();
    private string _strXmlFile;

    public XmlControl(string _XmlFile)
    {
      try
      {
        this._objXmlDoc.Load(_XmlFile);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this._strXmlFile = _XmlFile;
    }

    public XmlNodeList GetList(string _XmlPathNode)
    {
      return this._objXmlDoc.SelectNodes(_XmlPathNode);
    }

    public bool ExistNode(string _XmlPathNode)
    {
      return this._objXmlDoc.SelectSingleNode(_XmlPathNode) != null;
    }

    public DataView GetData(string _XmlPathNode)
    {
      DataSet dataSet = new DataSet();
      StringReader stringReader = new StringReader(this._objXmlDoc.SelectSingleNode(_XmlPathNode).OuterXml);
      int num = (int) dataSet.ReadXml((TextReader) stringReader);
      return dataSet.Tables[0].DefaultView;
    }

    public DataTable GetTable(string _XmlPathNode)
    {
      try
      {
        DataSet dataSet = new DataSet();
        StringReader stringReader = new StringReader(this._objXmlDoc.SelectSingleNode(_XmlPathNode).OuterXml);
        int num = (int) dataSet.ReadXml((TextReader) stringReader);
        return dataSet.Tables[0];
      }
      catch
      {
        return (DataTable) null;
      }
    }

    public string GetText(string _XmlPathNode)
    {
      return this.GetText(_XmlPathNode, false);
    }

    public string GetText(string _XmlPathNode, bool isCDATA)
    {
      XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_XmlPathNode);
      if (xmlNode == null)
        return "";
      if (isCDATA)
        return xmlNode.FirstChild.InnerText;
      return xmlNode.InnerText;
    }

    public void Update(string _XmlPathNode, string _Content)
    {
      this.Update(_XmlPathNode, _Content, false);
    }

    public void Update(string _XmlPathNode, string _Content, bool isCDATA)
    {
      if (isCDATA)
        this._objXmlDoc.SelectSingleNode(_XmlPathNode).FirstChild.InnerText = _Content;
      else
        this._objXmlDoc.SelectSingleNode(_XmlPathNode).InnerText = _Content;
    }

    public void Delete(string _XmlPathNode)
    {
      XmlNode oldChild = this._objXmlDoc.SelectSingleNode(_XmlPathNode);
      if (oldChild == null)
        return;
      oldChild.ParentNode.RemoveChild(oldChild);
    }

    public void RemoveAll(string _MainNode)
    {
      XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
      if (xmlNode == null)
        return;
      xmlNode.RemoveAll();
    }

    public void InsertNode(string _MainNode, string _ChildNode, string _Element, string _Content)
    {
      XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
      XmlElement element1 = this._objXmlDoc.CreateElement(_ChildNode);
      if (xmlNode == null)
        return;
      xmlNode.AppendChild((XmlNode) element1);
      XmlElement element2 = this._objXmlDoc.CreateElement(_Element);
      element2.InnerText = _Content;
      element1.AppendChild((XmlNode) element2);
    }

    public void InsertElement(string _MainNode, string _Element, string _Attrib, string _AttribContent, string _Content)
    {
      XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
      XmlElement element = this._objXmlDoc.CreateElement(_Element);
      element.SetAttribute(_Attrib, _AttribContent);
      element.InnerText = _Content;
      if (xmlNode == null)
        return;
      xmlNode.AppendChild((XmlNode) element);
    }

    public void InsertElement(string _MainNode, string _Element, string _Content, bool isCDATA)
    {
      XmlNode xmlNode = this._objXmlDoc.SelectSingleNode(_MainNode);
      XmlElement element = this._objXmlDoc.CreateElement(_Element);
      if (isCDATA)
        element.InnerXml = "<![CDATA[" + _Content + "]]>";
      else
        element.InnerText = _Content;
      if (xmlNode == null)
        return;
      xmlNode.AppendChild((XmlNode) element);
    }

    public void Save()
    {
      try
      {
        this._objXmlDoc.Save(this._strXmlFile);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void Dispose()
    {
      this._objXmlDoc = (XmlDocument) null;
    }
  }
}

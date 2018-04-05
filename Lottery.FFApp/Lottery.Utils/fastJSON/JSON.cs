// Decompiled with JetBrains decompiler
// Type: Lottery.Utils.fastJSON.JSON
// Assembly: Lottery.Utils, Version=1.0.1.1, Culture=neutral, PublicKeyToken=null
// MVID: E7A9C185-AF0A-4444-AE46-9A73782D0A74
// Assembly location: F:\pros\tianheng\bf\WebAppOld\bin\Lottery.Utils.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Lottery.Utils.fastJSON
{
  public class JSON
  {
    public static readonly JSON Instance = new JSON();
    private SafeDictionary<string, Type> _typecache = new SafeDictionary<string, Type>();
    private SafeDictionary<string, PropertyInfo> _propertycache = new SafeDictionary<string, PropertyInfo>();
    private SafeDictionary<Type, List<Getters>> _getterscache = new SafeDictionary<Type, List<Getters>>();
    private SafeDictionary<PropertyInfo, JSON.GenericSetter> _settercache = new SafeDictionary<PropertyInfo, JSON.GenericSetter>();

    private JSON()
    {
    }

    public string ToJSON(object obj)
    {
      return new JSONSerializer().ConvertToJSON(obj);
    }

    public object ToObject(string json)
    {
      return (object) (Dictionary<string, object>) JsonParser.JsonDecode(json) ?? (object) null;
    }

    private Type GetTypeFromCache(string typename)
    {
      if (this._typecache.ContainsKey(typename))
        return this._typecache[typename];
      Type type = Type.GetType(typename);
      this._typecache.Add(typename, type);
      return type;
    }

    private PropertyInfo getproperty(Type type, string propertyname)
    {
      if (propertyname == "$type")
        return (PropertyInfo) null;
      StringBuilder stringBuilder1 = new StringBuilder();
      stringBuilder1.Append(type.Name);
      stringBuilder1.Append("|");
      stringBuilder1.Append(propertyname);
      string key1 = stringBuilder1.ToString();
      if (this._propertycache.ContainsKey(key1))
        return this._propertycache[key1];
      foreach (PropertyInfo property in type.GetProperties())
      {
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.Append(type.Name);
        stringBuilder2.Append("|");
        stringBuilder2.Append(property.Name);
        string key2 = stringBuilder2.ToString();
        if (!this._propertycache.ContainsKey(key2))
          this._propertycache.Add(key2, property);
      }
      return this._propertycache[key1];
    }

    private static JSON.GenericSetter CreateSetMethod(PropertyInfo propertyInfo)
    {
      MethodInfo setMethod = propertyInfo.GetSetMethod();
      if (setMethod == (MethodInfo) null)
        return (JSON.GenericSetter) null;
      Type[] parameterTypes = new Type[2];
      parameterTypes[0] = parameterTypes[1] = typeof (object);
      DynamicMethod dynamicMethod = new DynamicMethod("_Set" + propertyInfo.Name + "_", typeof (void), parameterTypes, propertyInfo.DeclaringType);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
      ilGenerator.Emit(OpCodes.Ldarg_1);
      if (propertyInfo.PropertyType.IsClass)
        ilGenerator.Emit(OpCodes.Castclass, propertyInfo.PropertyType);
      else
        ilGenerator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
      ilGenerator.EmitCall(OpCodes.Callvirt, setMethod, (Type[]) null);
      ilGenerator.Emit(OpCodes.Ret);
      return (JSON.GenericSetter) dynamicMethod.CreateDelegate(typeof (JSON.GenericSetter));
    }

    private static JSON.GenericGetter CreateGetMethod(PropertyInfo propertyInfo)
    {
      MethodInfo getMethod = propertyInfo.GetGetMethod();
      if (getMethod == (MethodInfo) null)
        return (JSON.GenericGetter) null;
      Type[] parameterTypes = new Type[1]
      {
        typeof (object)
      };
      DynamicMethod dynamicMethod = new DynamicMethod("_Get" + propertyInfo.Name + "_", typeof (object), parameterTypes, propertyInfo.DeclaringType);
      ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
      ilGenerator.DeclareLocal(typeof (object));
      ilGenerator.Emit(OpCodes.Ldarg_0);
      ilGenerator.Emit(OpCodes.Castclass, propertyInfo.DeclaringType);
      ilGenerator.EmitCall(OpCodes.Callvirt, getMethod, (Type[]) null);
      if (!propertyInfo.PropertyType.IsClass)
        ilGenerator.Emit(OpCodes.Box, propertyInfo.PropertyType);
      ilGenerator.Emit(OpCodes.Ret);
      return (JSON.GenericGetter) dynamicMethod.CreateDelegate(typeof (JSON.GenericGetter));
    }

    internal List<Getters> GetGetters(Type type)
    {
      if (this._getterscache.ContainsKey(type))
        return this._getterscache[type];
      PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      List<Getters> gettersList = new List<Getters>();
      foreach (PropertyInfo propertyInfo in properties)
      {
        JSON.GenericGetter getMethod = JSON.CreateGetMethod(propertyInfo);
        if (getMethod != null)
          gettersList.Add(new Getters()
          {
            Name = propertyInfo.Name,
            Getter = getMethod
          });
      }
      this._getterscache.Add(type, gettersList);
      return gettersList;
    }

    private JSON.GenericSetter GetSetter(PropertyInfo prop)
    {
      if (this._settercache.ContainsKey(prop))
        return this._settercache[prop];
      JSON.GenericSetter setMethod = JSON.CreateSetMethod(prop);
      this._settercache.Add(prop, setMethod);
      return setMethod;
    }

    private object ParseDictionary(Dictionary<string, object> d)
    {
      Type typeFromCache = this.GetTypeFromCache(string.Concat(d["$type"]));
      object instance1 = Activator.CreateInstance(typeFromCache);
      foreach (string key in d.Keys)
      {
        PropertyInfo prop = this.getproperty(typeFromCache, key);
        if (prop != (PropertyInfo) null)
        {
          object obj1 = d[key];
          if (obj1 != null)
          {
            Type propertyType = prop.PropertyType;
            object obj2 = (object) propertyType.GetInterface("IDictionary");
            object obj3;
            if (propertyType.IsGenericType && !propertyType.IsValueType && obj2 == null)
            {
              IList instance2 = (IList) Activator.CreateInstance(propertyType);
              foreach (object obj4 in (ArrayList) obj1)
                instance2.Add(this.ParseDictionary((Dictionary<string, object>) obj4));
              obj3 = (object) instance2;
            }
            else if (propertyType == typeof (byte[]))
              obj3 = (object) Convert.FromBase64String((string) obj1);
            else if (propertyType.IsArray && !propertyType.IsValueType)
            {
              ArrayList arrayList = new ArrayList();
              foreach (object obj4 in (ArrayList) obj1)
                arrayList.Add(this.ParseDictionary((Dictionary<string, object>) obj4));
              obj3 = (object) arrayList.ToArray(prop.PropertyType.GetElementType());
            }
            else
              obj3 = propertyType == typeof (Guid) || propertyType == typeof (Guid?) ? (object) new Guid(string.Concat(obj1)) : (!(propertyType == typeof (DataSet)) ? (!(propertyType == typeof (Hashtable)) ? (obj2 == null ? this.ChangeType(obj1, propertyType) : this.CreateDictionary((ArrayList) obj1, propertyType)) : this.CreateDictionary((ArrayList) obj1, propertyType)) : (object) this.CreateDataset((Dictionary<string, object>) obj1));
            this.GetSetter(prop)(instance1, obj3);
          }
        }
      }
      return instance1;
    }

    private object CreateDictionary(ArrayList reader, Type pt)
    {
      IDictionary instance = (IDictionary) Activator.CreateInstance(pt);
      Type[] genericArguments = instance.GetType().GetGenericArguments();
      foreach (Dictionary<string, object> dictionary in reader)
      {
        object key = !(dictionary["k"] is Dictionary<string, object>) ? this.ChangeType(dictionary["k"], genericArguments[0]) : this.ParseDictionary((Dictionary<string, object>) dictionary["k"]);
        object obj = !(dictionary["v"] is Dictionary<string, object>) ? this.ChangeType(dictionary["v"], genericArguments[1]) : this.ParseDictionary((Dictionary<string, object>) dictionary["v"]);
        instance.Add(key, obj);
      }
      return (object) instance;
    }

    public object ChangeType(object value, Type conversionType)
    {
      if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof (Nullable<>)))
        conversionType = new NullableConverter(conversionType).UnderlyingType;
      return Convert.ChangeType(value, conversionType);
    }

    private Hashtable CreateHashtable(ArrayList reader)
    {
      Hashtable hashtable = new Hashtable();
      foreach (Dictionary<string, object> dictionary in reader)
        hashtable.Add(this.ParseDictionary((Dictionary<string, object>) dictionary["k"]), this.ParseDictionary((Dictionary<string, object>) dictionary["v"]));
      return hashtable;
    }

    private DataSet CreateDataset(Dictionary<string, object> reader)
    {
      DataSet dataSet = new DataSet();
      TextReader reader1 = (TextReader) new StringReader(string.Concat(reader["$schema"]));
      dataSet.ReadXmlSchema(reader1);
      foreach (string key1 in reader.Keys)
      {
        if (!(key1 == "$type") && !(key1 == "$schema"))
        {
          object obj = reader[key1];
          if (obj != null && obj.GetType() == typeof (ArrayList))
          {
            foreach (Dictionary<string, object> dictionary in (ArrayList) obj)
            {
              DataRow row = dataSet.Tables[key1].NewRow();
              foreach (string key2 in dictionary.Keys)
                row[key2] = dictionary[key2];
              dataSet.Tables[key1].Rows.Add(row);
            }
          }
        }
      }
      return dataSet;
    }

    private delegate void GenericSetter(object target, object value);

    public delegate object GenericGetter(object target);
  }
}

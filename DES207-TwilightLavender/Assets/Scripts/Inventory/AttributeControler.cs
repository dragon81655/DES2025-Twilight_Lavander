using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttributeControler 
{
    [SerializeReference]
    public List<AttributeBase> list = new List<AttributeBase>();
    private Dictionary<string, AttributeBase> attributesDic;

    public AttributeBase GetAttribute(string attribute)
    {
        ConvertToDic();
        AttributeBase toReturn= null;
        return attributesDic.TryGetValue(attribute, out toReturn) ? toReturn : null;
    }

    public void SetAttribute(string attribute, AttributeBase value)
    {
        ConvertToDic();
        if(attributesDic.ContainsKey(attribute))
        {
            attributesDic[attribute] = value;
        }
        else
        {
            attributesDic.Add(attribute, value);
        }
    }
    public AttributeControler Copy()
    {
        AttributeControler copy = new AttributeControler();
        foreach(AttributeBase attr in list)
        {
            copy.list.Add(attr.Copy());
        }
        return copy;
    }
    public void Concatonate(AttributeControler attr)
    {
        if(list.Count == 0)
        list.AddRange(attr.list);
        else
        {
            foreach(AttributeBase i in attr.list)
            {
                bool add = true;
                foreach(AttributeBase y in list)
                {
                    if(i.attributeName == y.attributeName)
                    {
                        add = false;
                        break;
                    }
                }
                if (add) list.Add(i);
            }
        }
    }
    
    public void ConvertToDic()
    {
        if(attributesDic == null)
        {
            attributesDic = new Dictionary<string, AttributeBase>();
            for (int i = 0; i < list.Count; i++)
            {
                attributesDic.Add(list[i].attributeName, list[i]);
            }
        }
    }
}

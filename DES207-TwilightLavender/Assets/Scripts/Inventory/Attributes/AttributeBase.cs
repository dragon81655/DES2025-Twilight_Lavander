using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public abstract class AttributeBase
{
    public string attributeName;
    public abstract void Init(InventoryController controller, Item item);

    public abstract void RunAttribute(InventoryController controller, Item item);

    public abstract void TickAttribute(InventoryController controller, Item item);

    public abstract AttributeBase Copy();
}

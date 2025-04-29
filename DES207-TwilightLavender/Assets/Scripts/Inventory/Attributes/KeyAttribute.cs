using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAttribute : AttributeBase
{
    private InteractionController controller;
    public override AttributeBase Copy()
    {
        DamagerAttribute at = new DamagerAttribute();
        at.attributeName = attributeName;
        return at;
    }

    public override void Init(InventoryController controller, Item item)
    {
        this.controller = controller.GetComponent<InteractionController>();
    }

    public override void RunAttribute(InventoryController controller, Item item)
    {
        if (this.controller == null) this.controller = controller.GetComponent<InteractionController>();
        if (this.controller == null)
        {
            Debug.LogWarning("Damage attribute being used without user having an interaction area!");
        }
        List<GameObject> list = (List<GameObject>)this.controller.GetInteractables();

        foreach (GameObject obj in list)
        {
            if (obj.TryGetComponent(out IUnlockable un))
            {
                un.Unlock();
                return;
            }
        }
    }

    public override void TickAttribute(InventoryController controller, Item item)
    {
    }
}

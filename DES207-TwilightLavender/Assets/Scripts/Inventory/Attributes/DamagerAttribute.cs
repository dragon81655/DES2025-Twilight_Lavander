using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerAttribute : AttributeBase
{
    [SerializeField] private float damageValue;
    private InteractionController controller;
    public override AttributeBase Copy()
    {
        DamagerAttribute at = new DamagerAttribute();
        at.damageValue = damageValue;
        at.attributeName = attributeName;
        return at;
    }

    public override void Init(InventoryController controller, Item item)
    {
        this.controller = controller.GetComponent<InteractionController>();
    }

    public override void RunAttribute(InventoryController controller, Item item)
    {
        if(this.controller == null) this.controller = controller.GetComponent<InteractionController>();
        if(this.controller == null) 
        {
            Debug.LogWarning("Damage attribute being used without user having an interaction area!");
        }
        List<GameObject> list =(List<GameObject>) this.controller.GetInteractables();

        foreach( GameObject obj in list )
        {
            if(obj.TryGetComponent(out IDamageable dmg))
            {
                dmg.Damage(damageValue);
            }
        }
    }

    public override void TickAttribute(InventoryController controller, Item item)
    {
    }
}

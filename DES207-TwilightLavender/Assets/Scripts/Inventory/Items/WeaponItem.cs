using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Weapon")]
public class WeaponItem : ItemBase
{
    [Header("Weapon Item fields")]
    [SerializeField] private float testField;
    public override bool CanUse(InventoryController controller, Item item)
    {
        return true;
    }
    public override void Use(InventoryController controller, Item item)
    {
        Debug.Log("Attack the bitch!");
    }
    public override void Tick(InventoryController controller, Item item)
    {
    }
}

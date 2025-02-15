using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameAttribute : AttributeBase
{
    public EndGameStatus status;
    public override AttributeBase Copy()
    {
        EndGameAttribute t = new EndGameAttribute();
        t.status = status;
        t.attributeName= attributeName;
        return t;
    }

    public override void Init(InventoryController controller, Item item)
    {
        
    }

    public override void RunAttribute(InventoryController controller, Item item)
    {
        GameStateManager.instance.FinishGame(status);
    }

    public override void TickAttribute(InventoryController controller, Item item)
    {

    }

    public EndGameAttribute()
    {
    }
}

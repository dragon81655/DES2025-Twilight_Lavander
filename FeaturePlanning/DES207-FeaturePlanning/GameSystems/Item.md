Item class is the base class of every item. They have a virtual method with no implementation for the majority of items.
Use and CanUse methods are declared as virtual and can be override in custom items.

### Properties
itemName(string)
sprite(Sprite)
durability (uint)
amount(uint)
maxAmount(uint)

### Methods
Use (void, [[Inventory Controller]])
CanUse (bool, [[Inventory Controller]])

OnCreateItem(void, [[Inventory Controller]]) (When item is crafted)

OnDestroyItem(void, [[Inventory Controller]]) (When item durability ends or is destroyed another way)




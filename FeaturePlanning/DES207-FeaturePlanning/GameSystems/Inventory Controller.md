The inventory controller is the script that controls and stores information about a certain inventory and it's common to players, chests or npcs (if they exist).
These controllers have an array holding [[Item]] scriptable objects.
If an item needs to be used, the request will be sent to the Inventory Controller.
Items can be searched by:

Name (string)
Id (int)
Index(On slots)

The inventories will use arrays since they will have a maximum amount of slots they can hold.
To make it more fun, the **OnItemPlace** and **OnItemRemove** events will exist to cast events when items are placed or removed from these containers.

### Properties:
numberOfSlots (int)
Items ([[Item]] array)
isUnlocked(bool)

OnItemPlace(Unity Event)
OnItemRemove(Unity Event)

### Methods
AddItem([[Item]])
AddItemToSlot([[Item]], int slot)
AddItemRange( List [[Item]])
AddItemRange( Array [[Item]])

RemoveItem([[Item]])
RemoveAllItemsOfType(int id)

TakeItemAmount(int id, amount)
TakeItemAmount([[Item]])
RemoveItemOnSlot(int index)

ClearInventory()

DropItem([[Item]])
DropItem(int id)
DropItem(int slot)












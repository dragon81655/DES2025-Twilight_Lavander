using PlasticGui.Gluon.WorkspaceWindow.Views.IncomingChanges;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CraftItem))]

public class CraftItemPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        CraftItem ingredient = property.managedReferenceValue as CraftItem;
        if (ingredient == null) property.managedReferenceValue = new CraftItem();
        if (ingredient != null)
        {
            bool hasTag = !string.IsNullOrEmpty(ingredient.tag);
            bool hasItem = ingredient.item != null;
            float t = position.size.y;
            position.size = new Vector2(position.size.x, 15);
            if (!hasItem)
            {
                ingredient.tag = EditorGUI.TextField(position, "Tag", ingredient.tag);
                position.position += new Vector2(0, 20);
            }
            if (!hasTag)
            {
                ingredient.item = (ItemBase)EditorGUI.ObjectField(position, "Item", ingredient.item, typeof(ItemBase), false);
                position.position += new Vector2(0, 20);
            }
            ingredient.amount = EditorGUI.IntField(position,"Amount", ingredient.amount);
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        CraftItem ingredient = property.managedReferenceValue as CraftItem;
        if (ingredient == null) property.managedReferenceValue = new CraftItem();
        if (ingredient != null)
        {
            bool hasTag = !string.IsNullOrEmpty(ingredient.tag);
            bool hasItem = ingredient.item != null;

            if (hasTag || hasItem)
                return EditorGUIUtility.singleLineHeight * 3;
            else
                return EditorGUIUtility.singleLineHeight * 4;
        }
        else return 0;
    }

}

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

[CustomPropertyDrawer(typeof(AttributeBase))]
public class AttributePropertyDrawer : PropertyDrawer
{

    private static List<Type> types = new List<Type>();
    private static string[] typeNames;

    private static Dictionary<Type, int> typeMap = new Dictionary<Type, int>();

    SerializedObject serializedObject;

    private int popUpInt;
    private int dropDownIndex = -1;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        GUILayout.Label("Select the type:");
        if (property.managedReferenceValue != null)
        {
            popUpInt = typeMap[property.managedReferenceValue.GetType()];
            dropDownIndex = popUpInt;
        }
        popUpInt = EditorGUILayout.Popup(popUpInt, typeNames);
        if (dropDownIndex != popUpInt)
        {
            dropDownIndex = popUpInt;
            AttributeBase attributeBase = (AttributeBase)System.Activator.CreateInstance(types[dropDownIndex]);
            property.managedReferenceValue = attributeBase;
            property.serializedObject.ApplyModifiedProperties();
        }
        if (property.managedReferenceValue != null)
        {
            EditorGUILayout.PropertyField(property, true);

        }
        EditorGUI.EndProperty();
    }
    [DidReloadScripts]
    public static void GetAllItemBaseTypes()
    {
        typeMap.Clear();
        types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(type => (type.IsSubclassOf(typeof(AttributeBase)) && !type.IsAbstract))
            .ToList();

        typeNames = new string[types.Count];
        for (int i = 0; i < types.Count; i++)
        {
            typeNames[i] = types[i].Name;
            typeMap.Add(types[i], i);
        }
    }
}

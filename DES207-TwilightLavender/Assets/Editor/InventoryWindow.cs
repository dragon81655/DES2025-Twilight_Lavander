using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


public class InventoryWindow : EditorWindow
{
    private bool onCreationMenu = false;
    private InventoryManager inventoryManager;


    //Types reflection (Only updated when script reloads)
    private static List<Type> types = new List<Type>();
    private static string[] typeNames;

    //New Item stuff
    private int dropDownIndex = 0;
    int popUpInt = 0;

    private ItemBase selectedItemBase;
    private SerializedObject serializedObject;


    private const string path = "Assets/ScriptableObjects/Items";

    [MenuItem("Window/OurTools/InventoryManagerWindow")]
    public static void OpenWindow()
    {
        GetWindow<InventoryWindow>("Inventory Manager");
    }


    private void OnGUI()
    {
        GUILayout.Label("Press to create new item", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(onCreationMenu ? "Back" : "New Item", GUILayout.Height(30), GUILayout.Width(position.width / 2)))
        {
            NewItem();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        if (!onCreationMenu)
            SelectedItemBase();
        else
            NewItemMenu();

    }

    private void NewItem()
    {
        if (inventoryManager == null)
        {
            inventoryManager = AssetDatabase.LoadAssetAtPath<InventoryManager>("Assets/Prefabs/InventoryManager.prefab");
            if (inventoryManager == null)
            {
                Debug.LogError("InventoryManager not in the folder!");
                return;
            }
        }
        onCreationMenu = !onCreationMenu;
        popUpInt = 0;
        dropDownIndex = 0;
        SaveEditorWindow(serializedObject);
    }

    private void NewItemMenu()
    {
        popUpInt = EditorGUILayout.Popup("Item Type", popUpInt, typeNames);
        if (popUpInt != 0)
        {
            if (dropDownIndex != popUpInt)
            {
                SaveEditorWindow(serializedObject);
                dropDownIndex = popUpInt;
                selectedItemBase = (ItemBase)CreateInstance(types[dropDownIndex - 1]);
                serializedObject = new SerializedObject(selectedItemBase);
            }
            if (serializedObject == null) return;

            SerializedProperty property = serializedObject.GetIterator();
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save", GUILayout.Height(30), GUILayout.Width(position.width / 5)))
            {
                SaveItem(path);
            }
            GUILayout.EndHorizontal();
        }
    }

    private void SelectedItemBase()
    {
        GUILayout.Label("Item to edit:", EditorStyles.boldLabel);
        selectedItemBase = (ItemBase)EditorGUILayout.ObjectField(selectedItemBase, typeof(ItemBase), false, GUILayout.Height(30));
        if (selectedItemBase == null)
            return;

        if (serializedObject == null || serializedObject.targetObject != selectedItemBase)
        {
            SaveEditorWindow(serializedObject);
            serializedObject = new SerializedObject(selectedItemBase);
        }

        //serializedItemBase.Update();

        SerializedProperty property = serializedObject.GetIterator();
        property.NextVisible(true);

        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property, true);
        }
    }


    private void OnLostFocus()
    {
        SaveEditorWindow(serializedObject);
    }
    private void OnFocus()
    {
        SaveEditorWindow(serializedObject);
        if (serializedObject != null && serializedObject.targetObject != null)
            serializedObject.Update();
    }

    private void OnDestroy()
    {
        Debug.Log("Saved!");
        SaveEditorWindow(serializedObject);

    }


    private void SaveEditorWindow(SerializedObject t)
    {
        if (t == null) return;
        t.ApplyModifiedProperties();
    }

    private void SaveItem(string path)
    {
        SaveEditorWindow(serializedObject);
        if (selectedItemBase.itemName == "")
        {
            Debug.LogError("You must name the item");
        }
        selectedItemBase.itemName = selectedItemBase.itemName.ToLower();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}/{selectedItemBase.itemName}.asset";

        if (File.Exists(fullPath))
        {
            Debug.LogError($"Item {selectedItemBase.itemName} already exists!");
            return;
        }
        SaveEditorWindow(serializedObject);
        AssetDatabase.CreateAsset(selectedItemBase, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        AddItemToRegistry(selectedItemBase);

        onCreationMenu = false;
        popUpInt = 0;
        dropDownIndex = 0;
    }

    private void AddItemToRegistry(ItemBase item)
    {
        if (inventoryManager == null)
        {
            return;
        }
        Type inventoryType = typeof(InventoryManager);
        MethodInfo registryField = inventoryType.GetMethod("AddItemToRegistry", BindingFlags.NonPublic | BindingFlags.Instance);

        if (registryField != null)
        {
            registryField.Invoke(inventoryManager, new object[] { item });
            EditorUtility.SetDirty(inventoryManager);
            AssetDatabase.SaveAssets();
        }
        else
        {
            Debug.LogError("Something went wrong with the save");
        }

    }

    [DidReloadScripts]
    public static void GetAllItemBaseTypes()
    {
        types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(type => (type.IsSubclassOf(typeof(ItemBase)) && !type.IsAbstract))
            .ToList();

        types.Add(typeof(ItemBase));
        typeNames = new string[types.Count + 1];
        typeNames[0] = "None";
        for (int i = 0; i < types.Count; i++)
        {
            typeNames[i + 1] = types[i].Name;
        }
    }

    [DidReloadScripts]
    public static void CleanRegistry()
    {
        InventoryManager inventoryManager = null;

        inventoryManager = AssetDatabase.LoadAssetAtPath<InventoryManager>("Assets/Prefabs/InventoryManager.prefab");
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager not in the folder!");
            return;
        }

        Type inventoryType = typeof(InventoryManager);
        MethodInfo registryField = inventoryType.GetMethod("CleanRegistry", BindingFlags.NonPublic | BindingFlags.Instance);

        if (registryField != null)
        {
            registryField.Invoke(inventoryManager, null);
            EditorUtility.SetDirty(inventoryManager);
            AssetDatabase.SaveAssets();
        }
        else
        {
            Debug.LogError("Something went wrong with the save");
        }

    }
}


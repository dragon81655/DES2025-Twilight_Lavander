using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CraftToolEditorWindow : EditorWindow
{
    private bool onCreationMenu = false;
    private CraftingManager craftManager;

    private CraftBase selectedCraftBase;
    private SerializedObject serializedObject;


    private const string path = "Assets/ScriptableObjects/Crafts";

    [MenuItem("Window/OurTools/CraftingManagerWindow")]
    public static void OpenWindow()
    {
        GetWindow<CraftToolEditorWindow>("Crafting Manager");
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
            SelectedCraftBase();
        else
            NewItemMenu();

    }

    private void NewItem()
    {
        if (craftManager == null)
        {
            craftManager = AssetDatabase.LoadAssetAtPath<CraftingManager>("Assets/Prefabs/CraftManager.prefab");
            if (craftManager == null)
            {
                Debug.LogError("Crafting Manager not in the folder!");
                return;
            }
        }
        onCreationMenu = !onCreationMenu;
        selectedCraftBase = new CraftBase();
        serializedObject = new SerializedObject(selectedCraftBase);
        SaveEditorWindow(serializedObject);
    }

    private void NewItemMenu()
    {
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

    private void SelectedCraftBase()
    {
        GUILayout.Label("Item to edit:", EditorStyles.boldLabel);
        selectedCraftBase = (CraftBase)EditorGUILayout.ObjectField(selectedCraftBase, typeof(CraftBase), false, GUILayout.Height(30));
        if (selectedCraftBase == null)
            return;

        if (serializedObject == null || serializedObject.targetObject != selectedCraftBase)
        {
            SaveEditorWindow(serializedObject);
            serializedObject = new SerializedObject(selectedCraftBase);
        }

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
        UpadteItems();
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

        if (selectedCraftBase.craftId == "")
        {
            Debug.LogError("You must give an id to the craft");
        }
        selectedCraftBase.craftId = selectedCraftBase.craftId.ToLower();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}/{selectedCraftBase.craftId}.asset";

        if (File.Exists(fullPath))
        {
            Debug.LogError($"Item {selectedCraftBase.craftId} already exists!");
            return;
        }
        SaveEditorWindow(serializedObject);
        AssetDatabase.CreateAsset(selectedCraftBase, fullPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        AddItemToRegistry(selectedCraftBase);

        onCreationMenu = false;
    }

    private void AddItemToRegistry(CraftBase item)
    {
        if (craftManager == null)
        {
            return;
        }
        Type inventoryType = typeof(CraftingManager);
        FieldInfo registryField = inventoryType.GetField("craftRegistry", BindingFlags.NonPublic | BindingFlags.Instance);

        List<CraftBase> itemRegistry = (List<CraftBase>)registryField.GetValue(craftManager);

        itemRegistry.Add(item);

        EditorUtility.SetDirty(craftManager);
        AssetDatabase.SaveAssets();
    }

    private void UpadteItems()
    {
        Debug.Log("Upadating Items");
        foreach(Item i in selectedCraftBase.outputs)
        {
            i.OnGUI();
        }
        serializedObject.Update();
    }
}

using System.Collections;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEditor;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using UnityEngine.SceneManagement;
using System.Web;

namespace LevelReconfigurator
{
    public class LevelMAnagerTool : EditorWindow
    {
        private static bool isAreasViewON = false;

        private LevelManagerConfiguration selectedLevelConfig = null;
        private SerializedObject serializedObject = null;
        private string configName;

        private string configPath = "Assets/Editor/LevelConfig";

        [MenuItem("Window/OurTools/´LevelManagerFixer")]
        public static void OpenWindow()
        {
            GetWindow<LevelMAnagerTool>("Level Fixer");
        }


        private void OnGUI()
        {
            GUILayout.Label("Create a new rule to change the level or apply current rules to fix your level.", EditorStyles.boldLabel);
            configName = EditorGUILayout.TextField("Name:", configName);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("New Level configuration", GUILayout.Height(30), GUILayout.Width(position.width / 2)))
            {
                if (configName == "")
                {
                    Debug.LogWarning("You need to set a name!");
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                    return;
                }
                CreateNewConfig();
            }
            if (GUILayout.Button("Delete config", GUILayout.Height(30), GUILayout.Width(position.width / 2)))
            {
                DeleteAsset();

            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);


            selectedLevelConfig = (LevelManagerConfiguration)EditorGUILayout.ObjectField(selectedLevelConfig, typeof(LevelManagerConfiguration), false, GUILayout.Height(30));
            if (selectedLevelConfig == null)
                return;

            LoadConfig();
            GUILayout.Space(30);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Apply changes!", GUILayout.Height(30), GUILayout.Width(position.width / 2)))
            {
                OnApplyChanges();
            }
            if (GUILayout.Button("Toggle Area Visualization", GUILayout.Height(30), GUILayout.Width(position.width / 2)))
            {
                isAreasViewON = !isAreasViewON;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            AreaView();
        }

        private void AreaView()
        {
            if(!isAreasViewON || selectedLevelConfig == null || selectedLevelConfig == null) return;
        }

        private void LoadConfig()
        {
            if (serializedObject == null || serializedObject.targetObject != selectedLevelConfig)
            {
                SaveEditorWindow(serializedObject);
                serializedObject = new SerializedObject(selectedLevelConfig);
            }

            SerializedProperty property = serializedObject.GetIterator();
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                EditorGUILayout.PropertyField(property, true);
            }
        }

        private void DeleteAsset()
        {
            if (selectedLevelConfig == null) return;
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(selectedLevelConfig));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            selectedLevelConfig = null;
            serializedObject = null;
        }
        private void CreateNewConfig()
        {
            selectedLevelConfig = (LevelManagerConfiguration)CreateInstance(typeof(LevelManagerConfiguration));
            serializedObject = new SerializedObject(selectedLevelConfig);
            selectedLevelConfig.name = configName;
            SaveAsset(selectedLevelConfig, configPath);
        }
        private void SaveEditorWindow(SerializedObject t)
        {
            if (t == null || selectedLevelConfig == null) return;
            t.ApplyModifiedProperties();
            t.Update();
        }

        private void SaveAsset(ScriptableObject obj, string path)
        {
            if(obj == null) return;
            SaveEditorWindow(serializedObject);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = $"{path}/{obj.name}.asset";

            if (File.Exists(fullPath))
            {
                EditorUtility.SetDirty(obj);
                AssetDatabase.SaveAssets();
                return;
            }
            SaveEditorWindow(serializedObject);
            AssetDatabase.CreateAsset(obj, fullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private void OnApplyChanges()
        {
            Scene scene = SceneManager.GetActiveScene();
            GameObject[] allObjects = scene.GetRootGameObjects();

            foreach(LevelConfigData data in selectedLevelConfig.levelConfigs)
            {
                Transform parent = CreateSubFolders(data);
                IEnumerable<GameObject> gos = GetObjectByName(GetObjectsInArea(allObjects, data.center, data.affectedArea), data.baseModelName);
                InstantiateNewPrefabs(gos, parent, data);
            }
        }

        private Transform CreateSubFolders(LevelConfigData data)
        {
            string currentfolder = "";
            Transform parent = null;
            foreach (char c in data.desiredFolderStructure)
            {
                if (c == '/' || c == '\\')
                {
                    GameObject t = GameObject.Find(currentfolder);
                    if(t == null) t = new GameObject(currentfolder);
                    if (parent)
                    t.transform.parent = parent;
                    parent = t.transform;
                    currentfolder = "";
                }
                else currentfolder += c;
            }
            GameObject t2 = new GameObject(currentfolder);
            if (parent)
                t2.transform.parent = parent;
            return t2.transform;
        }
        
        private IEnumerable<GameObject> GetObjectByName(IEnumerable<GameObject> gos,string objName)
        {
            foreach(GameObject gameObject in gos)
            {
                if (gameObject.name.Contains(objName))
                    yield return gameObject;
            }
        }

        private IEnumerable<GameObject> GetObjectsInArea(IEnumerable<GameObject> gos, Vector3 center, Vector3 area)
        {
            List<GameObject> toReturn = new List<GameObject>();
            if (area == Vector3.zero) return gos;
            foreach (GameObject go in gos)
            {
                Vector3 pos = go.transform.position;
                Vector3 area2 = area / 2;
                if (pos.x <= center.x + area2.x && pos.x >= center.x - area2.x && 
                    pos.y <= center.y + area2.y && pos.y >= center.y - area2.y && 
                    pos.z <= center.z + area2.z && pos.z >= center.z - area2.z)
                    toReturn.Add(go);
            }
            return toReturn;
        }

        private void InstantiateNewPrefabs(IEnumerable<GameObject> gos, Transform parent, LevelConfigData data)
        {
            bool destroyOriginal = data.destroyOriginal;
            Object prefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GetAssetPath(data.prefab));
            foreach (GameObject go in gos)
            {
                GameObject t = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
                t.transform.position = go.transform.position;
                if(destroyOriginal)
                    DestroyImmediate(go);
            }
        }

        private void OnLostFocus()
        {
            SaveEditorWindow(serializedObject);
        }
        private void OnFocus()
        {
            SaveEditorWindow(serializedObject);
        }
    }

    [System.Serializable]
    public struct ReplacementOptions
    {
        public string folderName;
        public string gameObjectPath;
        public string modelBaseName;
        public bool destroyOriginal;
    }
}
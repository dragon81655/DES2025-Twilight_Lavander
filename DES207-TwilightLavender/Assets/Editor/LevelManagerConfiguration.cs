using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelReconfigurator
{
    public class LevelManagerConfiguration : ScriptableObject
    {
        public List<LevelConfigData> levelConfigs = new List<LevelConfigData>();
    }
    [System.Serializable]
    public struct LevelConfigData
    {
        public GameObject prefab;
        public string desiredFolderStructure;
        public string baseModelName;
        public bool destroyOriginal;

        [Header("Affected Area Visualization")]
        public Vector3 affectedArea;
        public Vector3 center;
        public Color gizmoColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelReconfigurator
{
    public class LevelConfigDataObject : ScriptableObject
    {
        public string prefabPath;
        public string hierarchyPath;
        public string baseModelName;
        public bool destroyOriginal;

        [Tooltip("If the values are different from Vector3.zero, it will only apply this rule to the area of the level where u are configuring this.\nA box will appear in the area.")]
        public Vector3 affectedArea;
    }
}

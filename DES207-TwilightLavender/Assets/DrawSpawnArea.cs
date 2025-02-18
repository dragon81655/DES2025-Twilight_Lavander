using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawSpawnArea : MonoBehaviour
{
    public Color gizmoColor = new Color(0, 1, 0, 0.3f); // Semi-transparent green

    private LootTablesController lootTablesController;
    private void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        if (lootTablesController == null)
        {
            lootTablesController = GetComponent<LootTablesController>();
        }
        else
        {
            // Set color for the area
            Vector3 areaSize = lootTablesController.spawnArea;
            Gizmos.color = gizmoColor;

            // Draw a wireframe cube (outline)
            Gizmos.DrawWireCube(transform.position, areaSize);

            // Draw a semi-transparent solid cube
            Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.2f);
            Gizmos.DrawCube(transform.position, areaSize);
        }
#endif
    }
}

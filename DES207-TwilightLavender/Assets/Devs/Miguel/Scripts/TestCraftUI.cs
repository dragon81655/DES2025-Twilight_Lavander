using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestCraftUI : MonoBehaviour
{
    [SerializeField] private GameObject instancePoint;
    [SerializeField] private GameObject craftOptionPrefab;

    private CraftingController currentController;
    List<CraftBase> crafts;

    public void OpenMenu(CraftingController controller)
    {
        Cursor.lockState = CursorLockMode.None;
        currentController = controller;
        CreateButtons();
    }

    private void CreateButtons()
    {
        if (crafts == null)
        {
            crafts = (List<CraftBase>)currentController.GetAvailableRecipes();
        }
        for (int i = 0; i < crafts.Count; i++)
        {
            GameObject g = Instantiate(craftOptionPrefab, instancePoint.transform);
            RectTransform ui = g.GetComponent<RectTransform>();
            ui.localPosition = new Vector3(ui.localPosition.x , ui.localPosition.y + 50 * i, ui.localPosition.z);
            int i2 = i;
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(() => { OpenCraftMenu(i2); });
            TextMeshProUGUI t = g.GetComponentInChildren<TextMeshProUGUI>();
            t.text= crafts[i].displayName;
        }
    }

    public void OpenCraftMenu(int index)
    {
        Debug.Log(index);
        currentController.Craft(index);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiveMindFinalUIController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] names;
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField] private Image[] spriteSlots;
    [SerializeField] private Image nameSlot;

    public void UpdateSlots(int prev, int current, int next)
    {
        if (prev != -1)
        {
            spriteSlots[0].sprite = sprites[prev];
            spriteSlots[0].color = ChangeColor(spriteSlots[0].color, 1);
        }
        else
        {
            spriteSlots[0].sprite = null;
            spriteSlots[0].color = ChangeColor(spriteSlots[1].color, 0);
        }
        if (current != -1)
        {
            spriteSlots[1].sprite = sprites[current];
            nameSlot.sprite = names[current];
            spriteSlots[1].color = ChangeColor(spriteSlots[1].color, 1);
        }
        else
        {
            spriteSlots[1].sprite = null;
            nameSlot.sprite = null;
            spriteSlots[1].color = ChangeColor(spriteSlots[1].color, 0);
        }
        if (next != -1)
        {
            spriteSlots[2].sprite = sprites[next];
            spriteSlots[2].color = ChangeColor(spriteSlots[2].color, 1);
        }
        else
        {
            spriteSlots[2].sprite = null;
            spriteSlots[2].color = ChangeColor(spriteSlots[2].color, 0);
        }
    }

    private Color ChangeColor(Color color, int alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image normalImage;
    public Image blueImage;
    public bool isExitButton = false;

    void Start()
    {
        blueImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        normalImage.gameObject.SetActive(false);
        blueImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        normalImage.gameObject.SetActive(true);
        blueImage.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (isExitButton)
        {
            Debug.Log("Exiting Game...");
            Application.Quit(); // Quits the game
        }
    }

    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("ChoiceMenu");
    }

    public void OnCreditsButtonClicked()
    {
        SceneManager.LoadScene("CreditPage");
    }

    public void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("Settings");
    }
}

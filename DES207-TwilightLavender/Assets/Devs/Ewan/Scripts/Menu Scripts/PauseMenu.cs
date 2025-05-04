using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UIController UIController;
    public Image normalImage;
    public Image blueImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene("FinalLevel");
    }

    public void OnResumeButtonClicked()
    {
        UIController.Resume();
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

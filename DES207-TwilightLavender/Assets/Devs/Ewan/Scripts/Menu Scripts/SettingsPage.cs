using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPage : MonoBehaviour
{
    public GameObject Page1;
    public GameObject Page2;

    // Start is called before the first frame update
    void Start()
    {
        Page2.SetActive(false);
        Page1.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnForwardButtonClicked()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
    }

    public void OnBackwardButtonClicked()
    {
        Page2.SetActive(false);
        Page1.SetActive(true);
    }

    public void OnBack2MenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

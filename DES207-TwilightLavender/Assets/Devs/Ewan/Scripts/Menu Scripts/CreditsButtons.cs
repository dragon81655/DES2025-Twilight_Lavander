using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreditsButtons : MonoBehaviour
{
    public GameObject Page1;
    public GameObject Page2;

    void Start()
    {
        Page1.SetActive(true);
        Page2.SetActive(false);
    }
    public void OnForwardArrowClicked()
    {
        Page2.SetActive(true);
        Page1.SetActive(false);
    }

    public void OnBackArrowClicked()
    {
        Page2.SetActive(false);
        Page1.SetActive(true);
    }

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

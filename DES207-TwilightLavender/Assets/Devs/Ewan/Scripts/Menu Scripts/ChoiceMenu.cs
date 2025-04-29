using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnParasiteButtonPressed()
    {
        SceneManager.LoadScene("FinalLevel");
    }

    public void OnHumanButtonPressed()
    {
        SceneManager.LoadScene("FinalLevel");
    }
}

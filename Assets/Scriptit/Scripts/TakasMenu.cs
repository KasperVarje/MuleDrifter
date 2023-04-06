using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakasMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Menu");
    }
}

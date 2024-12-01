using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContenMenu : MonoBehaviour
{
    public void TiepTuc()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void Thoat()
    {
        Application.Quit();
    }
}

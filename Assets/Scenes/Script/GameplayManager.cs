using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public void Exit(){
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Retry(){
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public string level;
    public void RestartGame1()
    {
        SceneManager.LoadScene(level);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Selector : MonoBehaviour
{
    public bool Fade = false;
    public void Load_Levels(string Level)
    {
        StartCoroutine(Load_Level(Level));
    }
   public IEnumerator Load_Level(string Level)
    {
        if (Fade)
        {
            yield return new WaitForSeconds(1f);
        }
            SceneManager.LoadScene(Level);

    }

}

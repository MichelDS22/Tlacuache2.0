using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator Fade_animator;
    public GameObject Fade_OBJ;
    public bool fade_Out;

    private void Start()
    {
        Fade_OBJ.gameObject.SetActive(true);
        fade_Out = false;
}
    void Update()
    {
        if (fade_Out)
        {
            Fade_animator.SetBool("Fade_Out", true);
        }
        else
        {
            Fade_animator.SetBool("Fade_Out", false);
        }
    }

    public void Do_Fade_out()
    {
        fade_Out = true;
    }

}

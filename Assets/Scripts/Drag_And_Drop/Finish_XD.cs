using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_XD : MonoBehaviour
{
    public Drag[] P_Drag;
    public GameObject XD;
    public AudioSource A;
    public AudioClip C;

    private bool Audio_Lock;

    void Update()
    {
        bool todosSonVerdaderos = true;

        foreach (var elemento in P_Drag)
        {
            if (!elemento.locked)
            {
                todosSonVerdaderos = false;
                break;
            }
        }


        if (todosSonVerdaderos)
        {
            wea();
        }

    }



    void wea()
    {

        if (!Audio_Lock)
        {
            A.PlayOneShot(C);
            Audio_Lock = true;
        }
        XD.gameObject.SetActive(true);
    }
}

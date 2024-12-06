using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    [SerializeField] //usar los private

    private Transform imgPlace; //coordenadas el receptor
    private Vector2 PosIni; // pos iniicial del objeto a mover
    public bool locked; //Fijar cuando llega a la base

    private AudioSource AudioS;
    public AudioClip Clip;

    Vector3 mousePosOffset;

    private Vector3 GetMouseWoldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void Start()
    {
        PosIni = transform.position; //obtener coordenadas
        AudioS = GetComponent<AudioSource>();
    }

    private void OnMouseDown() //clic
    {


        if (!locked) // ! opuesto
        {
            mousePosOffset = gameObject.transform.position - GetMouseWoldPosition();
            print(mousePosOffset);
        }
    }

    private void OnMouseDrag()
    {
        if (!locked)
        {
            transform.position = GetMouseWoldPosition() + mousePosOffset;
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - imgPlace.position.x) <= 0.3f && Mathf.Abs(transform.position.y - imgPlace.position.y) <= 0.3f)
        {
            if (!locked)
            { 
                AudioS.PlayOneShot(Clip); 
            }
            transform.position = new Vector2(imgPlace.position.x, imgPlace.position.y);
            locked = true; //imagen fija
        }
        else
        {
            transform.position = new Vector2(PosIni.x, PosIni.y);
        }
    }

}


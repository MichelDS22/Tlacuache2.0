using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [SerializeField] private GameObject Dialogue_Mark;
    [SerializeField] private GameObject Dialogue_Panel;
    [SerializeField] private TMP_Text Dialogue_Text;
    [SerializeField, TextArea(4,6)] private string[] Dialogue_Lines;

    private float Typing_Time = 0.005f;

    private bool isPlayerInRange;
    private bool DidDialogueStart;
    private int Line_Index;

    void Update()
    {



        if ( isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!DidDialogueStart)
            {
                Start_Dialogue();
            }
            else if (Dialogue_Text.text == Dialogue_Lines[Line_Index]){
                Next_Dialogue_Line();
            }

        }        
    }

    private void Start_Dialogue()
    {
        DidDialogueStart = true;
        Dialogue_Panel.SetActive(true);
        Dialogue_Mark.SetActive(false);
        Line_Index = 0;
        StartCoroutine(Show_Line());
    }

    private void Next_Dialogue_Line()
    {
        Line_Index++;
        if (Line_Index < Dialogue_Lines.Length)
        {
            StartCoroutine(Show_Line());
        }
        else
        {
            DidDialogueStart = false;
            Dialogue_Panel.SetActive(false);
            Dialogue_Mark.SetActive(true);

        }
    }

    private IEnumerator Show_Line()
    {
        Dialogue_Text.text = string.Empty;
        foreach(char Ch in Dialogue_Lines[Line_Index])
        {
            Dialogue_Text.text += Ch;
            yield return new WaitForSeconds(Typing_Time);        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInRange = true;
            Dialogue_Mark.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerInRange = false;
            DidDialogueStart = false;
            Dialogue_Mark.SetActive(false);
            Dialogue_Panel.SetActive(false);
        }
    }
}

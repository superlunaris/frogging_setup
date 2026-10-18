using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string[] Lines;
    public TMP_Text textbox;
    public float Timer;
    public GameObject Panel;
    private int Index;
    private bool Done;
    private bool StartText;
    private bool Finished;

    public DialogueManager dialogueManager;

    private void Start()
    {
        Done = false;
        Finished = false;
        Panel.SetActive(false);
        StartText = false;
    }

    void Update()
    {
        if (!dialogueManager.isWaiting && StartText)
        {

            Panel.SetActive(true);
            textbox.text = Lines[Index];

            StartCoroutine(Waiting());
            dialogueManager.isWaiting = true;
        }

        if (!dialogueManager.isWaiting && Done)
        {
            Index = 1;
            textbox.text = Lines[Index];

            StartCoroutine(Finishing());
            dialogueManager.isWaiting = true;
        }

        if (Finished)
        {
            Index = 0;
            Finished = false;
            textbox.text = "";
            Panel.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartText = true;
        }
    }

    IEnumerator Waiting()
    {
        print("yolo");
        yield return new WaitForSeconds(Timer);
        Done = true;
        StartText = false;
        dialogueManager.isWaiting = false;
    }

    IEnumerator Finishing()
    {
        yield return new WaitForSeconds(Timer);
        dialogueManager.isWaiting = false;
        Done = false;
        Finished = true;
    }

}

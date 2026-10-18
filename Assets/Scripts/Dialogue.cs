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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {


        if (StartText)
        {

            Panel.SetActive(true);
            textbox.text = Lines[Index];

            StartCoroutine(Waiting());
        }

        if (Done) 
        {
            Index = 1;
            textbox.text = Lines[Index];

            StartCoroutine(Finishing());
        }

        if (Finished)
        {
            textbox.text = "";
            Panel.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Index = 0;
        StartText = true;

    }

    private void Start()
    {
        Done = false;
        Finished = false;
        Panel.SetActive(false);
        StartText = false;
        
        
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(Timer);
        Done = true;
    }

    IEnumerator Finishing()
    {
        yield return new WaitForSeconds(Timer);
        Finished = true;
    }

}

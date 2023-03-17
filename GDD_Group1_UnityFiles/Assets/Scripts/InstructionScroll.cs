using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScroll : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject instCanvas;
    public GameObject ins1;
    public GameObject ins2;
    public GameObject ins3;

    float state = 0;

    public void nextInstruction()
    {
        if(state == 0)
        {
            menuCanvas.SetActive(false);
            instCanvas.SetActive(true);
            ins1.SetActive(true);
            ins2.SetActive(false);
            ins3.SetActive(false);
        }

        else if(state == 1)
        {
            ins1.SetActive(false);
            ins2.SetActive(true);
            ins3.SetActive(false);
        }

        else if (state == 2)
        {
            ins1.SetActive(false);
            ins2.SetActive(false);
            ins3.SetActive(true);
        }

        else if(state == 3)
        {
            ins3.SetActive(false);
            instCanvas.SetActive(false);
            menuCanvas.SetActive(true);
        }

        state = (state + 1) % 4;
    }
}

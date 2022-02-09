using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressCounter : MonoBehaviour
{
    private int counter = 0;
    public Text text;

    public void Increment()
    {
        counter++;
        text.text = $"Hits: {counter}";
    }
}

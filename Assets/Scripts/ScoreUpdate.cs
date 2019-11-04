using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUpdate : MonoBehaviour
{
    Text text;

    // Use this for initialization
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = DataControl.dataControl.currentScore.ToString();
    }
}

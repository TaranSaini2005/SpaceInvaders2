using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreUpdate : MonoBehaviour 
{
	Text text;

	// Use this for initialization
	void Start () 
	{
		text = gameObject.GetComponent<Text> ();
        text.text = DataControl.dataControl.highScore.ToString();
    }
}

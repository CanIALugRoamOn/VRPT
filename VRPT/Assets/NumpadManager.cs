using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumpadManager : MonoBehaviour {


    public InputField inField;
    private Button[] numbers;

	// Use this for initialization
	void Start () {

        numbers = gameObject.GetComponentsInChildren<Button>();

        for(int i = 0; i < numbers.Length; i++)
        {
            Button btn = numbers[i];
            btn.onClick.AddListener(() => TaskOnClick(btn));
        }
	}

    public void TaskOnClick(Button button)
    {
        if (button.name.Contains("1")){
            inField.text += "1";
        }
        else if (button.name.Contains("2"))
        {
            inField.text += "2";
        }
        else if (button.name.Contains("3"))
        {
            inField.text += "3";
        }
        else if (button.name.Contains("4"))
        {
            inField.text += "4";
        }
        else if (button.name.Contains("5"))
        {
            inField.text += "5";
        }
        else if (button.name.Contains("6"))
        {
            inField.text += "6";
        }
        else if (button.name.Contains("7"))
        {
            inField.text += "7";
        }
        else if (button.name.Contains("8"))
        {
            inField.text += "8";
        }
        else if (button.name.Contains("9"))
        {
            inField.text += "9";
        }
        else if (button.name.Contains("Point"))
        {
            inField.text += ".";
        }
        else if (button.name.Contains("0"))
        {
            inField.text += "0";
        }
        else if (button.name.Contains("Delete"))
        {
            if (inField.text != "") {
                inField.text = inField.text.Remove(inField.text.Length - 1);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUIController : MonoBehaviour
{
	[SerializeField] List<TextMeshProUGUI> textBoxes;


	private void Start()
	{
		foreach (var textBox in textBoxes)
		{
			textBox.text = "";
		}
	}


	public void Print(string text, bool appendPrevious = false)
	{
		if (appendPrevious)
		{
			textBoxes[^1].text += " | " + text;
		}
		else
		{
			for (int i = 0; i < textBoxes.Count - 1; i++)
			{
				textBoxes[i].text = textBoxes[i + 1].text;
			}

			textBoxes[^1].text = text;
		}
	}
	
	public void Print(Vector3 v3, bool appendPrevious = false)
	{
		var text = " | " + v3.x + " | " + v3.y + " | " + v3.z + " | ";
		Print(text, appendPrevious);
	}
}

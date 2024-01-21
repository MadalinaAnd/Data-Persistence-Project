using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
	private Button backButton;

	[SerializeField]
	private TextMeshProUGUI namesListText;
	[SerializeField]
	private TextMeshProUGUI scoreListText;
	private int maxScorePositions = 10;

	private void Awake()
	{
		backButton = transform.Find("Back").GetComponent<Button>();
	}

	private void Start()
	{
		backButton.onClick.AddListener(BackButton);

		if (DataManager.Instance != null)
		{
			int listlength = DataManager.Instance.namesAndScore.Count;
			//shows the first "maxScorePositions" positions
			for (int i = 0; i < maxScorePositions; i++)
			{
				//if score list in the DataManager shorter then 10
				if (listlength != 0 && i < listlength && DataManager.Instance.namesAndScore[i].score != 0)
				{
					namesListText.text += $"{i + 1}.\t {DataManager.Instance.namesAndScore[i].names} \n";
					scoreListText.text += $"{DataManager.Instance.namesAndScore[i].score}\n";
				}
				else
				{
					namesListText.text += $"{i + 1}.\n";
				}
			}
		}
	}

	public void BackButton()
	{
		SceneManager.LoadScene(0);
	}
}

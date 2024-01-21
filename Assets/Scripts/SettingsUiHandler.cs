using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUiHandler : MonoBehaviour
{
	private Button backButton;
	private Button saveButton;

	private TMP_InputField inputNameField;

	private void Awake()
	{
		backButton = transform.Find("PlayerName/BackButton").GetComponent<Button>();
		saveButton = transform.Find("PlayerName/SaveButton").GetComponent<Button>();
		inputNameField = transform.Find("PlayerName/PlayerNameInput").GetComponent<TMP_InputField>();
		inputNameField = GetComponentInChildren<TMP_InputField>();
	}

	private void Start()
	{
		backButton.onClick.AddListener(BackButton);
		saveButton.onClick.AddListener(SaveButton);

		if (DataManager.Instance != null)
		{
			inputNameField.text = DataManager.Instance.playerName;
		}
		else
		{
			inputNameField.text = "Player";
		}
	}

	public void BackButton()
	{
		SceneManager.LoadScene(0);
	}

	public void SaveButton()
	{
		if (DataManager.Instance != null)
		{
			DataManager.Instance.playerName = inputNameField.text;
			DataManager.Instance.SaveDates();
		}
	}
}

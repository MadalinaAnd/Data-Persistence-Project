using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiHandler : MonoBehaviour
{
	private Button startButton;
	private Button endButton;
	private Button scoreButton;
	private Button settingsButton;


	private void Awake()
	{
		startButton = transform.Find("StartGame").GetComponent<Button>();
		endButton = transform.Find("EndGame").GetComponent<Button>();
		scoreButton = transform.Find("HighScore").GetComponent<Button>();
		settingsButton = transform.Find("PlayerSettings").GetComponent<Button>();
	}

	private void Start()
	{
		startButton.onClick.AddListener(StartButton);
		endButton.onClick.AddListener(ExitButton);
		scoreButton.onClick.AddListener(HighScoreButton);
		settingsButton.onClick.AddListener(PlayerSettingsButton);
	}

	public void StartButton()
	{
		SceneManager.LoadScene(1);
	}

	public void PlayerSettingsButton()
	{
		SceneManager.LoadScene(3);
	}

	public void HighScoreButton()
	{
		SceneManager.LoadScene(2);
	}

	public void ExitButton()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}
}

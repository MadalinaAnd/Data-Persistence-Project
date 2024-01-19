using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
	private Button backButton;

	private void Awake()
	{
		backButton = transform.Find("Back").GetComponent<Button>();
	}

	private void Start()
	{
		backButton.onClick.AddListener(BackButton);
	}

	public void BackButton()
	{
		SceneManager.LoadScene(0);
	}
}

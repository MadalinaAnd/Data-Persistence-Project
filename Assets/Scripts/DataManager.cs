using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public static DataManager Instance;
	public string playerName = "Player";
	public List<(string names, int score)> namesAndScore = new List<(string names, int score)>();

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		LoadData();
	}

	[Serializable]
	private class SaveData
	{
		public string playerName;
		public string[] scoreNamesList = new string[10];
		public int[] scoreList = new int[10];
	}

	public void SaveDates()
	{
		SaveData data = new SaveData();
		data.playerName = playerName;

		for (int i = 0; i < 10; i++)
		{
			if (i < namesAndScore.Count)
			{
				data.scoreNamesList[i] = namesAndScore[i].names;
				data.scoreList[i] = namesAndScore[i].score;
			}
		}

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(Application.persistentDataPath + "/saveFile.json", json);
	}

	public void LoadData()
	{
		string path = Application.persistentDataPath + "/saveFile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			playerName = data.playerName;
			for (int i = 0; i < data.playerName.Length; i++)
			{
				if (data.scoreNamesList[i] != null)
				{
					namesAndScore.Add((data.scoreNamesList[i], data.scoreList[i]));
				}
			}
		}
	}
}

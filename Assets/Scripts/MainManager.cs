using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
	public Button button;
    public GameObject GameOverText;

	private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

	private void Awake()
	{
		button = FindObjectOfType<Button>();
	}

	// Start is called before the first frame update
	void Start()
    {
		button.onClick.AddListener(StartButton);
		const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
		RefreshBestScore();
	}

	public void StartButton()
	{
		SceneManager.LoadScene(0);
	}

	private void RefreshBestScore()
	{
		if (DataManager.Instance != null && DataManager.Instance.namesAndScore.Count != 0)
		{
			bestScoreText.text = $"Best Score : {DataManager.Instance.namesAndScore[0].names} : {DataManager.Instance.namesAndScore[0].score}";
			Debug.Log("Best score refreshed");
		}
	}

	private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
		AddToScoreList();
		SaveNewScoreList();
		RefreshBestScore();

		m_GameOver = true;
        GameOverText.SetActive(true);
    }

	private void AddToScoreList()
	{
		int scoreListLenth = DataManager.Instance.namesAndScore.Count;

		if (scoreListLenth == 0 ||//if list empty
			DataManager.Instance.namesAndScore[scoreListLenth - 1].score >= m_Points)//if less than last element
		{
			DataManager.Instance.namesAndScore.Add((DataManager.Instance.playerName, m_Points));
			Debug.Log("Score added.");
		}
		else // finding position and insert score 
		{
			//prevents multiple additions
			bool isScoreAdded = false;
			for (int i = 0; i < scoreListLenth; i++)
			{
				while (!isScoreAdded)
				{
					//if new score more than score on i position
					if (DataManager.Instance.namesAndScore[i].score < m_Points)
					{
						DataManager.Instance.namesAndScore.Insert(i, (DataManager.Instance.playerName, m_Points));
						isScoreAdded = true;
					}
					break;
				}
			}
			Debug.Log("Score added.");
		}

	}

	private void SaveNewScoreList()
	{
		if (DataManager.Instance != null)
		{
			DataManager.Instance.SaveDates();
		}
	}

}

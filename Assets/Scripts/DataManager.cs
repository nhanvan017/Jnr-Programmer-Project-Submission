using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public string PlayerName { get; set; }

    public string HSPlayerName { get; set; }

    public int HighScore { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        LoadScore();
    }

    [Serializable]
    class HighScoreData
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        HighScoreData data = new HighScoreData();
        data.name = HSPlayerName;
        data.score = HighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
        Debug.Log("Highscore data was saved into file path: " + Application.persistentDataPath + "/highscore.json");
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/highscore.json"; ;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            HSPlayerName = data.name;
            HighScore = data.score;
            Debug.Log("Highscore data was read from file path: " + Application.persistentDataPath + "/highscore.json");
        }
    }
}

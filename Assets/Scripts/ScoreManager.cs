using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Thirdweb;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int currentScore;
    public int highScore;
    public int difficulty;
    public GameObject tokenGUI;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
    }


    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
        }
    }

    // Save the High score 
    public void SaveHighScore()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }
}

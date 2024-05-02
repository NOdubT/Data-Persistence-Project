using System;
using System.IO;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    public string userName;
    public string bestUserName;
    public int bestScore;

    public static UserManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        LoadHighScore();
        DontDestroyOnLoad(this);
    }

    public void InputUserName(string name)
    {
        userName = name;
    }

    public String BestScore()
    {
        return $"Best Score: {UserManager.instance.bestUserName} : {UserManager.instance.bestScore}";
    }

    [Serializable]
    class SaveData
    {
        public string userName;
        public int score;
    }

    public void SaveHighScore(int score)
    {
        SaveData data = new SaveData();
        bestUserName = userName;
        bestScore = score;
        data.userName = bestUserName;
        data.score = score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.score;
            bestUserName = data.userName;
        }
    }
}

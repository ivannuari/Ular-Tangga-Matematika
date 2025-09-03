using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameState currentState;

    //jumlah pemain dan jumlah ai
    [SerializeField] private int jumlahPemain;
    [SerializeField] private CharacterData[] playerDatas;

    private string savePath;
    [SerializeField] private SaveData saveDataFiles;

    public string currentName;
    public TipeSoal currentSoalType = TipeSoal.Campuran;

    public delegate void GameStateDelegate(GameState newState);
    public event GameStateDelegate OnStateChange;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath;
        LoadData();
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
        OnStateChange?.Invoke(state);
    }

    public void SaveData(UserRanking userRanking)
    {
        string path = Path.Combine(savePath, "saveData.json");

        // Bersihkan entry kosong
        saveDataFiles.rankingData.RemoveAll(item => string.IsNullOrEmpty(item.username));

        // Cari user yang sama
        var existing = saveDataFiles.rankingData.Find(item => item.username == userRanking.username);

        if (existing != null)
        {
            // Update data lama
            if(existing.score <= userRanking.score)
            {
                existing.score = userRanking.score;
            }
        }
        else
        {
            // Tambah data baru
            saveDataFiles.rankingData.Add(userRanking);
        }

        // Simpan ke file
        string json = JsonUtility.ToJson(saveDataFiles, true);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Path.Combine(savePath, "saveData.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            saveDataFiles = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Data loaded from: " + path);
        }
        else
        {
            Debug.LogWarning("Save file not found, creating new save data.");
            saveDataFiles = new SaveData(); // buat instance baru
            SaveData(null); // opsional, langsung simpan file baru
        }
    }

    public SaveData GetSaveData()
    {
        return saveDataFiles;
    }

    public void RestartGameplay()
    {
        foreach (var item in playerDatas)
        {
            item.characterPosition = 0;
        }
    }

    public void SetPlayersData(CharacterData[] data)
    {
        playerDatas = data;
    }

    public CharacterData[] GetPlayerData()
    {
        return playerDatas;
    }
}



public enum GameState
{
    Menu,
    Tutorial,
    Tentang,
    Level,
    SubLevel,
    Belajar,
    HighScore,
    Setting,
    Save,
    EditMode,
    Game,
    Soal,
    Result,
    Restart,
    BackToMenu,
    Exit
}


[System.Serializable]
public class SaveData
{
    public List<UserRanking> rankingData = new List<UserRanking>();
}


[System.Serializable]
public class UserRanking
{
    public string username;
    public int score;

    public UserRanking(string name, int poin)
    {
        username = name;
        score = poin;
    }
}

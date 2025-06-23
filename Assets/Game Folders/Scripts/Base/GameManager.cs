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
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
        OnStateChange?.Invoke(state);
    }

    public void SaveData(string saveName)
    {
        string path = savePath + "saveData.json";

        SaveFile newFile = new SaveFile(saveName);
        saveDataFiles.allSaveData.Add(newFile);

        string json = JsonUtility.ToJson(saveDataFiles);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = savePath + "saveData.json";

        string json = File.ReadAllText(path);
        saveDataFiles = JsonUtility.FromJson<SaveData>(json);
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
    public List<SaveFile> allSaveData = new List<SaveFile>();
}


[System.Serializable]
public class SaveFile
{
    public string nama;

    public SaveFile(string saveName)
    {
        nama = saveName;
    }
}

using System;
using System.Collections;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting Instance;
    public int score;

    [SerializeField] private Pemain[] pemains;
    [SerializeField] private GameObject[] skins;
    [SerializeField] private Transform[] spawner;

    [SerializeField] private PlayerType _currentPlayer = PlayerType.Pemain;
    
    [SerializeField] private int dadu;
    [SerializeField] private int currentPlayer;

    [SerializeField] private SoalManager soalManager;

    private TileType _currentType = TileType.NaikTangga;

    public event Action OnDaduRolled;
    public event Action<int> OnScoreUpdate;

    public delegate void DaduRollDelegate(int nilai);
    public event DaduRollDelegate OnDaduRoll;

    public delegate void CreateSoalDelegate(SoalPertanyaan newSoal);
    public event CreateSoalDelegate OnSoalCreated;

    public delegate void EndTurnDelegate();
    public event EndTurnDelegate OnEndTurn;

    public delegate void FinishGameDelegate(CharacterData dataPemenang);
    public event FinishGameDelegate OnGameEnded;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        soalManager = GetComponent<SoalManager>();

        currentPlayer = 0;
        for (int i = 0; i < pemains.Length; i++)
        {
            pemains[i].SetCharacterData(GameManager.Instance.GetPlayerData()[i] , skins[GameManager.Instance.GetPlayerData()[i].characterSkin]);
        }
    }

    public void MunculkanSoal(TileType tipe)
    {
        _currentType = tipe;
        GameManager.Instance.ChangeState(GameState.Soal);
        OnSoalCreated?.Invoke(soalManager.GetSoal());
    }
    public void CheckJawabanBenar(bool benar)
    {
        bool _benarCondition = benar && _currentType == TileType.NaikTangga || benar && _currentType == TileType.NaikPipa || !benar && _currentType == TileType.Turun; 

        if(_benarCondition)
        {
            pemains[currentPlayer].BerhasilMenjawab(_currentType);
            if(_currentPlayer == PlayerType.Pemain)
            {
                score += 20;
            }
        }
        else
        {
            pemains[currentPlayer].BerhasilMenjawab(_currentType , false);
            
        }
        OnScoreUpdate?.Invoke(score);
    }

    public void RollDadu()
    {
        OnDaduRolled?.Invoke();
        StartCoroutine(LoadingRoll());
    }

    IEnumerator LoadingRoll()
    {
        AudioManager.Instance.MainkanSuara("kocok");
        yield return new WaitForSeconds(1f);

        dadu = UnityEngine.Random.Range(1, 7);
        OnDaduRoll?.Invoke(dadu);
    }

    public int CheckCurrentPlayer()
    {
        return currentPlayer;
    }

    public Pemain[] GetAllPemain()
    {
        return pemains;
    }

    public void EndTurn()
    {
        currentPlayer++;
        if(currentPlayer > 3)
        {
            currentPlayer = 0;
        }

        _currentPlayer = pemains[currentPlayer].GetTipePemain();
        OnEndTurn?.Invoke();
    }

    public void GameEnd(CharacterData pemenang)
    {
        GameManager.Instance.ChangeState(GameState.Result);
        OnGameEnded?.Invoke(pemenang);
    }

    public Transform GetSpawnerPosition(int number)
    {
        return spawner[number];
    }

    public void JawabanBenar(bool benar)
    {
        if(benar)
        {
            pemains[currentPlayer].BerhasilMenjawab(_currentType);
        }
        else
        {
            EndTurn();
        }
    }

    public void Finish()
    {
        Debug.Log("Menang!!");
    }
}

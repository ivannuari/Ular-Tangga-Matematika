using GaweDeweStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class subLevelPage : Page
{
    [SerializeField] private Button b_home;
    [SerializeField] private Button b_mulai;

    [SerializeField] private CharacterSelector[] selectors;

    [SerializeField] private GameObject[] characterPrefabs;
    //[SerializeField] private CharacterSO[] allCharacterDataSO;
    [SerializeField] private CharacterData[] data;

    private MainMenuCharacterSelector mainMenuCharacterSelector;

    private void OnEnable()
    {
        mainMenuCharacterSelector = GetComponentInParent<MainMenuCharacterSelector>();
        mainMenuCharacterSelector.OnCharacterUpdated += MainMenuCharacterSelector_OnCharacterUpdated;
    }

    private void OnDisable()
    {
        mainMenuCharacterSelector.OnCharacterUpdated -= MainMenuCharacterSelector_OnCharacterUpdated;
    }

    private void MainMenuCharacterSelector_OnCharacterUpdated(int charaId, int index)
    {

    }

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Level));
        b_mulai.onClick.AddListener(() =>
        {
            GameManager.Instance.SetPlayersData(data);
            ChangeScene("Gameplay");
        });

        selectors[1].playerType.onValueChanged.AddListener((isPlayer) =>
        {
            SetPlayer(isPlayer, 1);
        });
        
        selectors[2].playerType.onValueChanged.AddListener((isPlayer) =>
        {
            SetPlayer(isPlayer, 2);
        });

        selectors[3].playerType.onValueChanged.AddListener((isPlayer) =>
        {
            SetPlayer(isPlayer, 3);
        });

        selectors[0].playerName.onClick.AddListener(() =>
        {
            SetCharacter(0);
        });

        selectors[1].playerName.onClick.AddListener(() =>
        {
            SetCharacter(1);
        });

        selectors[2].playerName.onClick.AddListener(() =>
        {
            SetCharacter(2);
        });

        selectors[3].playerName.onClick.AddListener(() =>
        {
            SetCharacter(3);
        });

        selectors[0].inputPlayerName.onValueChanged.AddListener((newName) =>
        {
            data[0].characterName = newName;
        });
        selectors[1].inputPlayerName.onValueChanged.AddListener((newName) =>
        {
            data[1].characterName = newName;
        });
        selectors[2].inputPlayerName.onValueChanged.AddListener((newName) =>
        {
            data[2].characterName = newName;
        });
        selectors[3].inputPlayerName.onValueChanged.AddListener((newName) =>
        {
            data[3].characterName = newName;
        });

        SetPlayer(false, 1);
        SetPlayer(false, 2);
        SetPlayer(false, 3);

        SetCharacter(0);
        SetCharacter(1);
        SetCharacter(2);
        SetCharacter(3);
    }

    private void SetCharacter(int n)
    {
        selectors[n].selectedCharacter++;

        if(selectors[n].selectedCharacter > characterPrefabs.Length - 1)
        {
            selectors[n].selectedCharacter = 0;
        }

        mainMenuCharacterSelector.ChangeCharacter(n, selectors[n].selectedCharacter);
        selectors[n].playerName.GetComponent<TMP_Text>().text = $"<  {characterPrefabs[selectors[n].selectedCharacter].name}  >";
        data[n].characterName = characterPrefabs[selectors[n].selectedCharacter].name;
        data[n].characterSkin = selectors[n].selectedCharacter;
    }

    private void SetPlayer(bool isPemain, int n)
    {
        selectors[n].isPlayer = isPemain;

        if (selectors[n].isPlayer)
        {
            selectors[n].playerTypeText.text = "Pemain";
            data[n].tipe = PlayerType.Pemain;
        }
        else
        {
            selectors[n].playerTypeText.text = "Komputer";
            data[n].tipe = PlayerType.Ai;
        }
    }
}


[System.Serializable]
public class CharacterSelector
{
    public RawImage potrait;

    public Toggle playerType;
    public TMP_Text playerTypeText;
    
    public Button playerName;
    public TMP_InputField inputPlayerName;

    public bool isPlayer = true;
    public int selectedCharacter = 0;
}

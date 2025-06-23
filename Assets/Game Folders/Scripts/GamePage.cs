using GaweDeweStudio;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePage : Page
{
    [SerializeField] private Button b_dadu;
    [SerializeField] private Sprite[] allDadu;
    [SerializeField] private GameObject layer_dadu;
    [SerializeField] private GameObject panel_playerTurn;

    [SerializeField] private Image image_dadu;

    [SerializeField] private TMP_Text label_giliran;
    [SerializeField] private TMP_Text label_nama;
    [SerializeField] private TMP_Text label_turnPlayer;

    [SerializeField] private TMP_Text label_score;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    protected override void Start()
    {
        base.Start();
    
        GameSetting.Instance.OnDaduRoll += Instance_OnDaduRoll;
        GameSetting.Instance.OnEndTurn += Instance_OnEndTurn;
        GameSetting.Instance.OnDaduRolled += Instance_OnDaduRolled;
        GameSetting.Instance.OnScoreUpdate += UpdateScore;

        b_dadu.onClick.AddListener(() =>
        {
            RollTheDice();
        });

        Instance_OnEndTurn();

        restartButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Restart));
        exitButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.BackToMenu));
        UpdateScore(0);
    }

    

    private void OnDestroy()
    {
        GameSetting.Instance.OnDaduRoll -= Instance_OnDaduRoll; 
        GameSetting.Instance.OnEndTurn -= Instance_OnEndTurn;
        GameSetting.Instance.OnDaduRolled -= Instance_OnDaduRolled;
        GameSetting.Instance.OnScoreUpdate -= UpdateScore;
    }

    private void RollTheDice()
    {
        //roll the dice
        image_dadu.gameObject.SetActive(false);
        layer_dadu.SetActive(false);

        GameSetting.Instance.RollDadu();
        
        b_dadu.GetComponent<Image>().enabled = true;
        b_dadu.GetComponent<Animator>().SetBool("isRoll", true);
    }

    private void Instance_OnDaduRolled()
    {
        b_dadu.interactable = false;
    }

    private void Instance_OnEndTurn()
    {
        panel_playerTurn.SetActive(true);
        Pemain currentPemain = GameSetting.Instance.GetAllPemain()[GameSetting.Instance.CheckCurrentPlayer()];
        string nm = currentPemain.GetCharacterName();
        label_turnPlayer.text = $"Giliran {nm}";
        label_nama.text = nm;

        switch (currentPemain.GetTipePemain())
        {
            case PlayerType.Pemain:
                label_giliran.text = $"Giliran : Pemain";
                b_dadu.interactable = true;
                break;
            case PlayerType.Ai:
                label_giliran.text = $"Giliran : AI";
                b_dadu.interactable = false;
                StartCoroutine(AiWalk());
                break;
        }
    }

    IEnumerator AiWalk()
    {
        yield return new WaitForSeconds(2f);
        RollTheDice();
    }

    private void Instance_OnDaduRoll(int nilai)
    {
        b_dadu.GetComponent<Image>().enabled = false;
        b_dadu.GetComponent<Animator>().SetBool("isRoll", false);

        panel_playerTurn.SetActive(false);
        layer_dadu.SetActive(true);

        image_dadu.gameObject.SetActive(true);
        image_dadu.sprite = allDadu[nilai - 1];
    }

    public void UpdateScore(int score)
    {
        label_score.text = $"Score: {score}";
    }
}
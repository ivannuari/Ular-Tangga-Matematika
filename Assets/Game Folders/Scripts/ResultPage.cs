using GaweDeweStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : Page
{
    [SerializeField] private Button b_restart;
    [SerializeField] private Button b_quit;

    [SerializeField] private TMP_Text label_nama;
    [SerializeField] private TMP_Text label_score;

    protected override void Start()
    {
        base.Start();
    
        b_restart.onClick.AddListener(() => ChangeScene("Gameplay"));
        b_quit.onClick.AddListener(() => ChangeScene("Main Menu"));
    }

    private void OnEnable()
    {

        GameSetting.Instance.OnGameEnded += Instance_OnGameEnded;
    }
    private void OnDisable()
    {
        GameSetting.Instance.OnGameEnded -= Instance_OnGameEnded;
    }

    private void Instance_OnGameEnded(CharacterData dataPemenang)
    {
        label_nama.text = $"{dataPemenang.tipe} {dataPemenang.characterName}";
        label_score.text = $"Total Score: {GameSetting.Instance.score}";
    }
}

using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class menuPage : Page
{
    [SerializeField] private Button b_exit;

    [SerializeField] private Button b_highscore;
    [SerializeField] private Button b_tutorial;
    [SerializeField] private Button b_tentang;
    [SerializeField] private Button b_play;

    [SerializeField] private Toggle t_sound;

    protected override void Start()
    {
        base.Start();
    
        b_exit.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Exit));

        b_highscore.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.HighScore));
        b_tutorial.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Tutorial));
        b_tentang.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Tentang));
        b_play.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Level));

        t_sound.onValueChanged.AddListener((isMute) =>
        {
            if(isMute)
            {
                AudioManager.Instance.HentikanSemuaSuara();
            }
            else
            {
                AudioManager.Instance.NyalakanSemuaSuara();
            }
        });
    }
}

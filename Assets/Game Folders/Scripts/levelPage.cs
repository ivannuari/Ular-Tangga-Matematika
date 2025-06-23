using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class levelPage : Page
{
    [SerializeField] private Button b_home;

    [SerializeField] private Button b_belajar;
    [SerializeField] private Button b_bermain;

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));

        b_belajar.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Belajar));
        b_bermain.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.SubLevel));
    }
}

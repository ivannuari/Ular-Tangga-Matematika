using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class tentangPage : Page
{
    [SerializeField] private Button b_home;

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
    }
}

using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class ExitPage : Page
{
    [SerializeField] private Button yaButton;
    [SerializeField] private Button tidakButton;

    protected override void Start()
    {
        base.Start();
    
        yaButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        tidakButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ChangeState(GameState.Menu);
        });
    }
}

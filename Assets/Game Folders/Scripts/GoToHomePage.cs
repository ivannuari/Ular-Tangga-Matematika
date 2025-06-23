using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class GoToHomePage : Page
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    protected override void Start()
    {
        base.Start();
    
        yesButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameManager.Instance.ChangeState(GameState.Menu);
            ChangeScene("Main Menu");
        });
        noButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameManager.Instance.ChangeState(GameState.Game);
        });
    }
}

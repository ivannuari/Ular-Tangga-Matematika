using GaweDeweStudio;
using UnityEngine;
using UnityEngine.UI;

public class RestartPage : Page
{
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    protected override void Start()
    {
        base.Start();
    
        yesButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameManager.Instance.RestartGameplay();
            ChangeScene("Gameplay");
        });
        noButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameManager.Instance.ChangeState(GameState.Game);
        });
    }
}

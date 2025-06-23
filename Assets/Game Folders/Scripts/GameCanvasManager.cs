using GaweDeweStudio;
using UnityEngine;

public class GameCanvasManager : CanvasManager
{
    private void OnEnable()
    {
        GameManager.Instance.OnStateChange += Instance_OnStateChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStateChange -= Instance_OnStateChange;
    }

    private void Instance_OnStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Game:
                Time.timeScale = 1f;
                SetPage(PageName.Game);
                break;
            case GameState.Soal:
                SetPage(PageName.Soal);
                break;
            case GameState.Result:
                SetPage(PageName.Result);
                break;
            case GameState.Restart:
                Time.timeScale = 0f;
                SetPage(PageName.Restart);
                break;
            case GameState.BackToMenu:
                Time.timeScale = 0f;
                SetPage(PageName.GoToHome);
                break;
        }
    }
}
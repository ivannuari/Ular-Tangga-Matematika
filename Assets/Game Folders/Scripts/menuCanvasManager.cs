using GaweDeweStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCanvasManager : CanvasManager
{
    private void Start()
    {
        GameManager.Instance.OnStateChange += Instance_OnStateChange;
        AudioManager.Instance.MainkanSuara("BGM");
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChange -= Instance_OnStateChange;    
    }

    private void Instance_OnStateChange(GameState newState)
    {
        switch (newState)
        {
            case GameState.Menu:
                SetPage(PageName.Menu);
                break;
            case GameState.Tutorial:
                SetPage(PageName.Tutorial);
                break;
            case GameState.Tentang:
                SetPage(PageName.Tentang);
                break;
            case GameState.Level:
                SetPage(PageName.Level);
                break;
            case GameState.SubLevel:
                SetPage(PageName.SubLevel);
                break;
            case GameState.Belajar:
                SetPage(PageName.Belajar);
                break;
            case GameState.HighScore:
                SetPage(PageName.Highscore);
                break;
            case GameState.Save:
                break;
            case GameState.EditMode:
                break;
            case GameState.Exit:
                SetPage(PageName.Exit);
                break;
        }
    }
}

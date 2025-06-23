using GaweDeweStudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscorePage : Page
{
    [SerializeField] private Button b_home;

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
    }
}

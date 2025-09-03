using GaweDeweStudio;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class highscorePage : Page
{
    [SerializeField] private Button b_home;

    [SerializeField] private RankingItem rankingItem;
    [SerializeField] private Transform content;

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));
        SetupLeaderboard();
    }

    private void SetupLeaderboard()
    {
        List<UserRanking> topTen = GameManager.Instance
     .GetSaveData()
     .rankingData
     .OrderByDescending(item => item.score)
     .Take(10)
     .ToList();

        for (int i = 0; i < topTen.Count; i++) 
        {
            var clone = Instantiate(rankingItem, content);
            clone.SetupItem(topTen[i].username, topTen[i].score);
        }
    }
}

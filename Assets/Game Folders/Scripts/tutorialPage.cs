using GaweDeweStudio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tutorialPage : Page
{
    [SerializeField] private Button b_home;
    [SerializeField] private Button b_kanan;
    [SerializeField] private Button b_kiri;

    [SerializeField] private TMP_Text label_nomor;
    [SerializeField] private TMP_Text label_isi;

    [SerializeField , TextArea(5,5)] private string[] tutorialInfos;
    private int n = 0;

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Menu));

        SetInfo();

        b_kanan.onClick.AddListener(() =>
        {
            if(n < tutorialInfos.Length - 1)
            {
                n++;
                SetInfo();
            }
        });

        b_kiri.onClick.AddListener(() =>
        {
            if(n > 0)
            {
                n--;
                SetInfo();
            }
        });
    }

    private void SetInfo()
    {
        label_nomor.text = $"{n + 1}";
        label_isi.text = tutorialInfos[n];
    }
}

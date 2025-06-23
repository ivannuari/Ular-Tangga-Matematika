using GaweDeweStudio;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class belajarPage : Page
{
    [SerializeField] private Button b_home;
    [SerializeField] private Button b_back;
    [SerializeField] private Button b_selectMateri;

    [SerializeField] private Button b_left;
    [SerializeField] private Button b_right;

    [SerializeField] private Toggle penjumlahanToggle;
    [SerializeField] private Toggle penguranganToggle;
    [SerializeField] private Toggle perkalianToggle;
    [SerializeField] private Toggle pembagianToggle;

    [SerializeField] private Image materiImage;
    [SerializeField] private MateriKalkulasi[] materiKalkulasi;

    [SerializeField] private GameObject[] panels;
    [SerializeField] private kalkulasi activeKalkulasi = kalkulasi.Penjumlahan;

    [SerializeField] private TMP_Text judulKalkulasiText;
    [SerializeField] private TMP_Text judulNomorText;
    [SerializeField] private TMP_Text[] isiMateri;
    [SerializeField] private int activeNumber = 1;

    public enum kalkulasi
    {
        Penjumlahan,
        Pengurangan,
        Perkalian,
        Pembagian
    }

    private void OnEnable()
    {
        SetFrame(0);
    }

    protected override void Start()
    {
        base.Start();
    
        b_home.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Level));
        b_back.onClick.AddListener(() => SetFrame(0));

        penjumlahanToggle.onValueChanged.AddListener((selected) => { if (selected) { activeKalkulasi = kalkulasi.Penjumlahan; }});
        penguranganToggle.onValueChanged.AddListener((selected) => { if (selected) { activeKalkulasi = kalkulasi.Pengurangan; }});
        perkalianToggle.onValueChanged.AddListener((selected) => { if (selected) { activeKalkulasi = kalkulasi.Perkalian; }});
        pembagianToggle.onValueChanged.AddListener((selected) => { if (selected) { activeKalkulasi = kalkulasi.Pembagian; }});

        b_selectMateri.onClick.AddListener(() =>
        {
            SetFrame(1);
            SetKalkulasi(activeKalkulasi);
        });

        b_right.onClick.AddListener(() =>
        {
            if (activeNumber > 9) { return; }

            activeNumber++;
            SetKalkulasi(activeKalkulasi);
        });

        b_left.onClick.AddListener(() =>
        {
            if(activeNumber <= 1) { return; }

            activeNumber--;
            SetKalkulasi(activeKalkulasi);
        });
    }

    private void SetKalkulasi(kalkulasi activeKalkulasi)
    {
        judulNomorText.text = $"{activeNumber}";
        judulKalkulasiText.text = activeKalkulasi.ToString();

        string kalkulasiText = "+";
        switch (activeKalkulasi)
        {
            case kalkulasi.Penjumlahan:
                kalkulasiText = "+";
                break;
            case kalkulasi.Pengurangan:
                kalkulasiText = "-";
                break;
            case kalkulasi.Perkalian:
                kalkulasiText = "x";
                break;
            case kalkulasi.Pembagian:
                kalkulasiText = ":";
                break;
        }
        for (int i = 0; i < isiMateri.Length; i++)
        {
            int hasil = 0;
            switch (activeKalkulasi)
            {
                case kalkulasi.Penjumlahan:
                    hasil = (i + 1) + activeNumber;
                    break;
                case kalkulasi.Pengurangan:
                    hasil = (i + 1) - activeNumber;
                    break;
                case kalkulasi.Perkalian:
                    hasil = (i + 1) * activeNumber;
                    break;
                case kalkulasi.Pembagian:
                    hasil = (i + 1) / activeNumber;
                    break;
            }
            isiMateri[i].text = $"{i + 1} {kalkulasiText} {activeNumber} = {hasil}";
        }
    }

    private void SetFrame(int index)
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

        panels[index].SetActive(true);
    }
}

[Serializable]
public class MateriKalkulasi
{
    public string idNama;
    public Sprite[] allSprite;
}

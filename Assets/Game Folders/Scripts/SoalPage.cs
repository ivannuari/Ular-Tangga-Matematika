using GaweDeweStudio;
using System.Collections;
using TMPro;
using UnityEngine;

public class SoalPage : Page
{
    [SerializeField] private TMP_Text label_soal;
    [SerializeField] private TMP_Text[] label_opsi;

    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text poinText;

    [SerializeField] private GameObject[] stars;

    [SerializeField] private GameObject layer_AI;
    [SerializeField] private GameObject resultPanel;

    private int tempJawaban;


    [SerializeField] private int maxCountdown;
    private int countdown;

    [SerializeField] private TMP_Text label_timer;

    private void OnEnable()
    {
        StartCoroutine(StartCountdown());
        GameSetting.Instance.OnSoalCreated += Instance_OnSoalCreated;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        GameSetting.Instance.OnSoalCreated -= Instance_OnSoalCreated;
    }

    private void Instance_OnSoalCreated(SoalPertanyaan newSoal)
    {
        label_soal.text = newSoal.soal;
        for (int i = 0; i < newSoal.opsi.Length; i++)
        {
            label_opsi[i].text = newSoal.opsi[i];
        }
        tempJawaban = newSoal.jawaban;

        switch (GameSetting.Instance.GetAllPemain()[GameSetting.Instance.CheckCurrentPlayer()].GetTipePemain())
        {
            case PlayerType.Pemain:
                layer_AI.SetActive(false);
                //pemain harus pilih
                break;
            case PlayerType.Ai:
                layer_AI.SetActive(true);
                StartCoroutine(AIJawab());
                break;
        }
    }

    IEnumerator AIJawab()
    {
        yield return new WaitForSeconds(2f);
        CheckJawaban(Random.Range(0, 3));
    }

    public void CheckJawaban(int n)
    {
        bool isBenar = true;
        if(n == tempJawaban)
        {
            //benar
            GameSetting.Instance.CheckJawabanBenar(true);
        }
        else
        {
            //salah
            GameSetting.Instance.CheckJawabanBenar(false);
            isBenar = false;
        }

        StopAllCoroutines();
        StartCoroutine(ShowResult(isBenar));
    }

    IEnumerator StartCountdown()
    {
        resultPanel.SetActive(false);
        countdown = maxCountdown;
        for (int i = 0; i < maxCountdown; i++)
        {
            label_timer.text = $"00 : {countdown}";
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        //GameManager.Instance.ChangeState(GameState.Game);
        GameSetting.Instance.CheckJawabanBenar(false);
        StartCoroutine(ShowResult(false));
    }

    IEnumerator ShowResult(bool isBenar)
    {
        resultPanel.SetActive(true);
        string jawaban = isBenar ? "Jawaban Benar" : "Jawaban Salah";
        resultText.text = $"{jawaban}";
        poinText.gameObject.SetActive(isBenar);

        foreach (var item in stars)
        {
            item.SetActive(isBenar);
        }

        yield return new WaitForSeconds(2f);

        GameManager.Instance.ChangeState(GameState.Game);
        if(!isBenar)
        {
            GameSetting.Instance.EndTurn();
        }
    }
}

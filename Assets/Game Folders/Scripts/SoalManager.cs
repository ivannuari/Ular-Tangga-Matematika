using System;
using UnityEngine;

public class SoalManager : MonoBehaviour
{
    [SerializeField] private SoalPertanyaan[] allSoals;
    private SoalPertanyaan[] allActiveSoals;

    public void Awake()
    {
        TipeSoal currentSoal = GameManager.Instance.currentSoalType;

        if(currentSoal == TipeSoal.Campuran)
        {
            allActiveSoals = allSoals;
            return;
        }

        allActiveSoals = Array.FindAll(allSoals,x => x.tipeSoal == currentSoal);
    }

    public SoalPertanyaan GetSoal()
    {
        return allActiveSoals[UnityEngine.Random.Range(0, allSoals.Length)];
    }
}












[System.Serializable]
public class SoalPertanyaan
{
    public TipeSoal tipeSoal = TipeSoal.Campuran;
    [TextArea(3,2)]public string soal;
    [TextArea(2,2)]public string[] opsi;
    public int jawaban;
}

public enum TipeSoal
{
    Campuran,
    Penjumlahan,
    Pengurangan,
    Perkalian,
    Pembagian
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoalManager : MonoBehaviour
{
    [SerializeField] private SoalPertanyaan[] allSoals;

    public SoalPertanyaan GetSoal()
    {
        return allSoals[Random.Range(0, allSoals.Length)];
    }
}












[System.Serializable]
public class SoalPertanyaan
{
    [TextArea(3,2)]public string soal;
    [TextArea(2,2)]public string[] opsi;
    public int jawaban;
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tile : MonoBehaviour
{
    [SerializeField] private TileType tipe = TileType.Normal;

    [SerializeField] private int tileTujuan = 0;

    public bool isHadapKanan = false;

    public TileType CheckType()
    {
        return tipe;
    }

    public int GetNomorTujuan()
    {
        return tileTujuan;
    }
}

public enum TileType
{
    NaikTangga,
    NaikPipa,
    Turun,
    Normal,
    Finish
}

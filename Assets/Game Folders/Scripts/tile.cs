using UnityEngine;

public class tile : MonoBehaviour
{
    [SerializeField] private TileType tipe = TileType.Normal;

    [SerializeField] private int tileTujuan = 0;
    [SerializeField] private GameObject[] specialFx;

    public bool isHadapKanan = false;

    private void OnEnable()
    {
        foreach (var item in specialFx)
        {
            item.SetActive(tipe != TileType.Normal);
        }
    }

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

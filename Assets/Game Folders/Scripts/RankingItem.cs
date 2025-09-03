using TMPro;
using UnityEngine;

public class RankingItem : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text rankText;

    public void SetupItem(string n, int rank)
    {
        nameText.text = n;
        rankText.text = rank.ToString();
    }
}

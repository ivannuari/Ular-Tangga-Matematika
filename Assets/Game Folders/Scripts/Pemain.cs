using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Pemain : MonoBehaviour
{
    [SerializeField] private CharacterData data;
    [SerializeField] private LayerMask layerCharacter;
    [SerializeField] private LayerMask layerTile;

    [SerializeField] private float speedModifier = 1.2f;

    private bool startMoving = false;

    [SerializeField] private int tileModifier;

    private SpriteRenderer rend;
    
    private void Start()
    {
        GameSetting.Instance.OnDaduRoll += Instance_OnDaduRoll;
    }

    private void OnDisable()
    {
        GameSetting.Instance.OnDaduRoll -= Instance_OnDaduRoll;
    }

    private void Instance_OnDaduRoll(int nilai)
    {
        if(GameSetting.Instance.CheckCurrentPlayer() == data.characterNumber)
        {
            StartCoroutine(Walk(nilai));
        }
    }

    IEnumerator Walk(int nilai)
    {
        yield return new WaitForSeconds(1f);

        if(data.characterPosition + nilai <= 100)
        {
            for (int i = 0; i < nilai; i++)
            {
                transform.position = Vector3.Lerp(transform.position, gridManager.Instance.transform.GetChild(i + data.characterPosition).position, 1f);
                CheckHadap();
                AudioManager.Instance.MainkanSuara("walk");
                yield return new WaitForSeconds(1f);
            }
            data.characterPosition += nilai;

            Collider2D[] isHitAnotherPlayer = Physics2D.OverlapCircleAll(transform.position, 0.25f, layerCharacter);
            foreach (var item in isHitAnotherPlayer)
            {
                if(item != null)
                {
                    if(item.gameObject.name != gameObject.name)
                    {
                        item.GetComponent<Pemain>().StartOver();
                    }   
                }
            }
        }


        Collider2D[] isHitSpesialTile = Physics2D.OverlapCircleAll(transform.position, 0.25f, layerTile);
        foreach (var item in isHitSpesialTile)
        {
            if(item != null)
            {
                switch (item.GetComponent<tile>().CheckType())
                {
                    case TileType.NaikTangga:
                        GameSetting.Instance.MunculkanSoal(TileType.NaikTangga);
                        tileModifier = item.GetComponent<tile>().GetNomorTujuan();
                        break;
                    case TileType.NaikPipa:
                        GameSetting.Instance.MunculkanSoal(TileType.NaikPipa);
                        tileModifier = item.GetComponent<tile>().GetNomorTujuan();
                        break;
                    case TileType.Turun:
                        GameSetting.Instance.MunculkanSoal(TileType.Turun);
                        tileModifier = item.GetComponent<tile>().GetNomorTujuan();
                        break;
                    case TileType.Normal:
                        GameSetting.Instance.EndTurn();
                        break;
                    case TileType.Finish:
                        GameSetting.Instance.GameEnd(data);
                        break;

                }
            }
        }
    }

    public void BerhasilMenjawab(TileType tipe , bool benar = true)
    {
        switch (tipe)
        {
            case TileType.NaikTangga:
                AudioManager.Instance.MainkanSuara("naik");
                startMoving = true;
                break;
            case TileType.NaikPipa:
                AudioManager.Instance.MainkanSuara("naik");
                StartCoroutine(NaikPipa());
                break;
            case TileType.Turun:
                AudioManager.Instance.MainkanSuara("turun");
                if(!benar)
                {
                }
                StartCoroutine(TurunUlar());
                break;
        }
    }

    public void SalahMenjawab(TileType currentType)
    {
        StartCoroutine(EndTurn());
    }

    IEnumerator EndTurn()
    {
        yield return new WaitForSeconds(2f);
        GameSetting.Instance.EndTurn();
    }

    IEnumerator TurunUlar()
    {
        GetComponentInChildren<SpriteRenderer>(true).enabled = false;
        yield return new WaitForSeconds(2f);

        transform.position = gridManager.Instance.transform.GetChild(tileModifier).position;
        GetComponentInChildren<SpriteRenderer>(true).enabled = true;

        data.characterPosition = tileModifier + 1;
        GameSetting.Instance.EndTurn();
    }

    IEnumerator NaikPipa()
    {
        transform.position += Vector3.up * 0.35f;
        transform.localScale = new Vector3(0.85f, 0.85f, 1f);

        for (int i = 0; i < 35; i++)
        {
            transform.position -= Vector3.up * 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        GetComponentInChildren<SpriteRenderer>(true).enabled = false;

        yield return new WaitForSeconds(2f);

        transform.position = gridManager.Instance.transform.GetChild(tileModifier).position;
        transform.position += Vector3.up * 0.35f;

        GetComponentInChildren<SpriteRenderer>(true).enabled = true;

        for (int i = 0; i < 35; i++)
        {
            transform.position -= Vector3.up * 0.01f;
            yield return new WaitForSeconds(0.1f);
        }

        transform.localScale = new Vector3(0.85f, 0.85f, 1f);

        yield return new WaitForSeconds(1f);

        data.characterPosition = tileModifier + 1;
        GameSetting.Instance.EndTurn();
    }

    private void Update()
    {
        if(startMoving)
        {
            Vector3 targetPos = gridManager.Instance.transform.GetChild(tileModifier).position;
            float jarak = Vector3.Distance(transform.position, targetPos);

            if(jarak >= 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speedModifier * Time.deltaTime);
            }
            else
            {
                startMoving = false;
                data.characterPosition = tileModifier + 1;
                GameSetting.Instance.EndTurn();
            }
        }
    }

    private void CheckHadap()
    {
        Collider2D isHitSpesialTile = Physics2D.OverlapCircle(transform.position, 0.25f , layerTile);
        if (isHitSpesialTile != null)
        {
            rend.flipX = !isHitSpesialTile.GetComponent<tile>().isHadapKanan;
        }
    }

    public string GetCharacterName()
    {
        return data.characterName;
    }

    public PlayerType GetTipePemain()
    {
        return data.tipe;
    }

    public void SetCharacterData(CharacterData newData , GameObject skin)
    {
        data = newData;
        GameObject clone = Instantiate(skin, transform);
        rend = clone.GetComponent<SpriteRenderer>();

        rend.flipX = true;
    }

    public void StartOver()
    {
        data.characterPosition = 0;
        transform.position = GameSetting.Instance.GetSpawnerPosition(data.characterNumber).position;
    }

    
}

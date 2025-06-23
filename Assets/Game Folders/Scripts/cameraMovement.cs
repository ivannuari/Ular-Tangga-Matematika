using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private Vector3 target;

    private void Start()
    {
        GameSetting.Instance.OnEndTurn += Instance_OnEndTurn;
        Instance_OnEndTurn();
    }

    private void OnDisable()
    {
        GameSetting.Instance.OnEndTurn -= Instance_OnEndTurn;
    }

    private void Instance_OnEndTurn()
    {
    }

    private void LateUpdate()
    {
        Pemain p = GameSetting.Instance.GetAllPemain()[GameSetting.Instance.CheckCurrentPlayer()];
        target = p.transform.position;
        target.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, target, 2f);
    }
}

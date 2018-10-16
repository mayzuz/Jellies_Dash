using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// メニュー拡張用
/// プレイヤーの不変なステータスをデータとして出力
/// </summary>
[CreateAssetMenu]
public class PlayerDataTable : ScriptableObject {

    [SerializeField]
    float speed;
    [SerializeField]
    float jump;
    [SerializeField]
    float stamina;

    [SerializeField]
    int mind;

    public float Speed
    {
        get
        {
            return speed;
        }
    }
    public float Jump
    {
        get
        {
            return jump;
        }
    }
    public float Stamina
    {
        get
        {
            return stamina;
        }
    }
    public int Mind
    {
        get
        {
            return mind;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回復（ヒール）
/// 使用時にスタミナを10点回復
/// </summary>
public class Heal : MonoBehaviour
{
    private Player player;
    private ParticleSystem particle;
    private float maxST;        //スタミナの最大値

    public float heal = 10;     //スタミナの回復量
    public int mind = 6;        //使用するMP

    // Use this for initialization
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }

    void Start()
    {
        particle = player.GetComponent<ParticleSystem>();
        maxST = player.Stamina;
        Debug.Log("スタミナの最大量:"+maxST);
    }


    // Update is called once per frame
    void Update()
    {
        //特技使用可能時(即時回復)
        if (Input.GetKeyDown(KeyCode.C) && player.jumpEnd && player.perk
             && (player.mind >= mind))
        {
            player.perk = false;

            if (player.stamina + heal > maxST)
                player.stamina = maxST;
            else
                player.stamina += heal;

            player.mind -= mind;
            particle.Play();
        }

    }
}

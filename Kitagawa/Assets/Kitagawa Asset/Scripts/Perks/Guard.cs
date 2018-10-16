using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御（ガード）
/// 妨害系特技の影響を一回無効化する
/// </summary>
public class Guard : MonoBehaviour
{
    private Player player;
    private ParticleSystem particle;
    public int mind = 8;              //使用するMP

    // Use this for initialization
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }
    
    void Start()
    {
        particle = player.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //特技使用可能時かつガードを発動していない時(即時展開)
        if (Input.GetKeyDown(KeyCode.V) && player.jumpEnd && player.perk
            && !player.guard && (player.mind >= mind) && (player.stamina > 0))
        {
            particle.Play();
            player.guard = true;
            player.perk = false;

            player.mind -= mind;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跳躍（スプリング）
/// 使用時に2秒間ジャンプの値を2点上昇
/// </summary>
public class Spring : MonoBehaviour
{
    private Player player;
    private ParticleSystem particle;
    private float defaultJP;    //ジャンプの初期値

    bool reaction;              //特技を発動しているか
    float timer = 0;

    public float spring = 2;           //上昇するジャンプ力
    public int mind = 5;               //使用するMP
    public float reactionTime = 2.0f;　//特技の効果時間

    // Use this for initialization
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }

    void Start()
    {
        particle = player.GetComponent<ParticleSystem>();
        defaultJP = player.Jump;
        Debug.Log("元のジャンプ力:" + defaultJP);
        reaction = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(defaultJP);

        //特技使用可能時かつ効果時間外の時
        if (Input.GetKeyDown(KeyCode.X) && !reaction && player.jumpEnd && player.perk
             && (player.mind >= mind) && (player.stamina > 0))
        {
            player.jump += spring;
            reaction = true;
            player.perk = false;

            particle.Play();
            player.mind -= mind;
        }

        //特技の効果時間
        if (reaction)
        {
            timer += Time.deltaTime;

            if (timer >= reactionTime)
            {
                reaction = false;
                player.jump = defaultJP;
                timer = 0;
            }
        }
    }
}

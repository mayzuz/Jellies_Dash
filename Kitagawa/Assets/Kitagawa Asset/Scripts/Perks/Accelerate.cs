using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加速(アクセル)
/// 使用時に1秒間スピードの値を2点上昇
/// </summary>
public class Accelerate : MonoBehaviour
{
    private Player player;
    private ParticleSystem particle;

    private float defaultSP;    //スピードの初期値

    bool reaction;              //特技を発動しているか
    float timer = 0;

    public float accelerate = 2;      //加速させるスピード
    public int mind = 5;              //使用するMP
    public float reactionTime = 1;　//特技の効果時間

    // Use this for initialization
    void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }

    void Start()
    {
        particle = player.GetComponent<ParticleSystem>();
        defaultSP = player.Speed;
        Debug.Log("元の速さ:" + defaultSP);
        reaction = false;
    }


    // Update is called once per frame
    void Update()
    {
        //特技使用可能時かつ効果時間外の時
        if (Input.GetKeyDown(KeyCode.Z) 
            && !reaction && player.jumpEnd && player.perk
             && (player.mind >= mind) && (player.stamina > 0))
        {
            player.speed += accelerate;
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
                timer = 0;
                reaction = false;
                player.speed = defaultSP;
                
            }

        }
    }
}

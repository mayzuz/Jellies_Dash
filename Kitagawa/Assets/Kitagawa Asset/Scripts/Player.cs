using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    private Vector3 force;

    ////キャラクターのステータス////
    [SerializeField]
    PlayerDataTable defaultStatus;

    public float speed;
    public float jump;
    public float stamina;
    public int mind;

    public float Speed
    {
        get { return speed; }
    }
    public float Jump
    {
        get { return jump; }
    }
    public float Stamina
    {
        get { return stamina; }
    }
    public int Mind
    {
        get { return mind; }
    }

    public bool guard;      //ガードの有無
    public bool jumpEnd;    //ジャンプしていないか
    public bool perk;       //特技発動可能か

    float timer;
    float waitTime;
    float coolTime;

    void Awake()
    {
        rb = transform.GetComponent<Rigidbody>();

        speed = defaultStatus.Speed;
        jump = defaultStatus.Jump;
        stamina = defaultStatus.Stamina;
        mind = defaultStatus.Mind;
    }

    void Start()
    {
        guard = false;
        jumpEnd = true;
        perk = true;
    }

    // Update is called once per frame
    void Update()
    {
        //MPは0以下にならない
        if (mind < 0)
            mind = 0;

        //速さを設定
        force = new Vector3(-speed, 0, 0);

        //動ける時は前に力を加える
        if (stamina > 0)
        {
            rb.AddForce(force);
        }

        //スタミナがない時はその場に留まる
        if (stamina <= 0)
        {
            stamina = 0;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            waitTime += Time.deltaTime;
            if (waitTime >= 3.0f)
            {
                stamina = defaultStatus.Stamina;
                waitTime = 0;
            }
        }

        //ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && jumpEnd
            && stamina > 0)
        {
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            perk = false;
            jumpEnd = false;

            stamina -= 2;
        }

        //クールタイム
        if (!perk)
        {
            coolTime += Time.deltaTime;
            if (coolTime >= 2.0f)
            {
                if (jumpEnd)
                {
                    perk = true;
                    coolTime = 0.0f;
                }
            }
        }

        //1秒毎にスタミナを減らす（確認用）
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            timer = 0.0f;

            stamina -= 1;
        }

        //特技の発動///////////////////////////////////

        if ((stamina >= 0) && jumpEnd && perk)
        {
            //     if (Input.GetKeyDown(KeyCode.X))
            //
            //       if (Input.GetKeyDown(KeyCode.X))
            //
            //           if (Input.GetKeyDown(KeyCode.C))
            //
            //              if (Input.GetKeyDown(KeyCode.V))
        }



    }

    void OnCollisionStay()
    {
        //慣性を消して移動する力を一定化
        rb.velocity = Vector3.zero;

        if (stamina > 0)
            rb.AddForce(new Vector3(-0.1f, 5.0f, 0), ForceMode.Impulse);

        jumpEnd = true;
    }

}

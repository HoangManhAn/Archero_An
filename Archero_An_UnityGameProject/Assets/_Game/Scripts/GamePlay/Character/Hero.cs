
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Hero : Character
{

    [Header("-------------Hero Move-------------")]

    [SerializeField] private Rigidbody rb;

    public DynamicJoystick joyStick;

    private Vector3 joyStickInput;


    //Weapon
    [Header("-------------Weapon-------------")]
    [SerializeField] WeaponData weaponData;
    [SerializeField] Transform rightHand;
    [SerializeField] Weapon currentWeapon;
    public WeaponType weaponType;


    //Hat
    [Header("-------------Hat-------------")]
    [SerializeField] HatData hatData;
    [SerializeField] Transform head;
    [SerializeField] Hat currentHat;
    public HatType hatType;

    //Pant
    [Header("-------------Pant-------------")]
    [SerializeField] PantData pantData;
    [SerializeField] Renderer currentPant;
    public PantType pantType;

    //Pant
    [Header("-------------Skin-------------")]
    [SerializeField] SkinData skinData;
    [SerializeField] Renderer currentSkin;
    public SkinType skinType;

    [Header("-------------Setting-------------")]
    public float moveSpeed = 500f;

    public List<Enemy> targets = new List<Enemy>();


    private void Update()
    {
        if (GameManager.Ins.IsState(GameState.GamePlay) && !IsDead)
        {
            //Move
            joyStickInput = new Vector3(joyStick.Horizontal, 0f, joyStick.Vertical);
            if (joyStickInput.magnitude > 0f)
            {
                //isMoving = true;
                ChangeAnim("run");
                HeroMove(joyStickInput);
            }
            else
            {
                //isMoving = false;
                rb.velocity = Vector3.zero;

                if (targets.Count <= 0)
                {
                    ChangeAnim("idle");

                }
                else
                {
                    if (!isAttack)
                    {
                        
                        OnAttack();
                    }
                }

            }
        }

    }

    public override void OnInit()
    {
        base.OnInit();

        hp = 100f;
        isAttack = false;
        GetHealthBarHero(hp);
        joyStick.gameObject.SetActive(true);


        ChangeWeapon(WeaponType.W_Knife_1);
        ChangeHat(HatType.Cowboy_1);
        ChangePant(PantType.Pant_Blue);
        ChangeSkin(SkinType.Red);
    }


    public override void OnDespawn()
    {
        base.OnDespawn();

        Destroy(healthBar.gameObject);
        joyStick.gameObject.SetActive(false);

        UIManager.Ins.CloseUI<GamePlay>();
        UIManager.Ins.OpenUI<Lose>();
        GameManager.Ins.ChangeState(GameState.Pause);
        LevelManager.Ins.OnFinish();


    }

    public override void OnDeath()
    {
        base.OnDeath();
        OnStopMove();
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1.5f);
    }


    public override void OnStopMove()
    {
        rb.velocity = Vector3.zero;
    }

    public void HeroMove(Vector3 direct)
    {
        //Set target position
        Vector3 targetPos = Camera.main.transform.TransformDirection(direct);
        targetPos.y = 0f;


        //Move by Navmesh
        //agent.Move(moveSpeed * Time.deltaTime * targetPos);

        //Move by rigidbody
        direct.Normalize();
        rb.velocity = direct * moveSpeed; // * Time.deltaTime;


        //Change direct follow joystick
        if (targetPos != Vector3.zero)
        {
            //Set target rotation
            Quaternion targetRos = Quaternion.LookRotation(targetPos);
            TF.rotation = Quaternion.Slerp(TF.rotation, targetRos, 10f);
        }
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weaponData.GetWeapon(weaponType), rightHand);
        currentWeapon.ChangeBullet(weaponType);

    }

    public void ChangeHat(HatType hatType)
    {
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }

        currentHat = Instantiate(hatData.GetHat(hatType), head);

    }

    public void ChangePant(PantType pantType)
    {
        this.pantType = pantType;
        currentPant.material = pantData.GetMat(pantType);
    }

    public void ChangeSkin(SkinType skinType)
    {
        this.skinType = skinType;
        currentSkin.material = skinData.GetMat(skinType);
    }

    public Vector3 GetDirect()
    {
        //Chuyen huong Hero toi Enemy
        Vector3 directForward = FindEnemyNearest().position - TF.position;
        TF.forward = directForward;

        //Lay huong tu diem ban den Enemy
        Vector3 direct = FindEnemyNearest().position - currentWeapon.bulletPoint.position;
        return direct.normalized;

    }

    public Transform FindEnemyNearest()
    {

        if (targets.Count > 0)
        {

            Transform enemyNearest = targets[0].TF;

            float dis;
            float disMin = Vector3.Distance(TF.position, enemyNearest.position);
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null) continue;

                dis = Vector3.Distance(TF.position, targets[i].TF.position);
                if (dis < disMin)
                {
                    disMin = dis;
                }
            }

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null) continue;

                dis = Vector3.Distance(TF.position, targets[i].TF.position);
                if (Mathf.Abs(dis - disMin) <= 0.1f)
                {
                    return targets[i].TF;
                }
            }
        }

        return targets[0].TF;
    }

    public void GetHealthBarHero(float hp)
    {
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }

        GetHealthBar(hp);

    }


    public override void OnAttack()
    {
        isAttack = true;
        ChangeAnim("attack");
        Invoke(nameof(ResetAttack), 0.4f);
        //StartCoroutine(IEdelayAction(ResetAttack, 0.4f));
        currentWeapon.Fire(GetDirect());
    }


    //private IEnumerator IEdelayAction(Action action, float delay)
    //{
        
    //        yield return new WaitForSeconds(delay);
    //        action?.Invoke();

    //}
}

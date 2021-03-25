using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskerSavas : MonoBehaviour
{
    private ParentTakip SoldierAccess;
    private SphereCollider EffectArea;
    private BirlikOzellik CommanderAccess;
    public GameObject Soldier;
    private GameObject Commander;
    public GameObject Enemy;
    private AskerSavas EnemyIsTrue;

    Collider[] hitCollider;

    public int Id;
    public string Name;
    public AskerTur TroopType;
    public float Attack;
    public float Defense;
    public float Shock;
    public float Speed;
    public int Count;
    public float SoldierHealth;
    public float Morale;
    public float Power;
    public string GeneralName;
    public int Side;
    public Dizilim ActiveFormation;
    void Start()
    {
        SoldierAccess = Soldier.GetComponent<ParentTakip>();
        EffectArea = Soldier.GetComponent<SphereCollider>();
        Commander = SoldierAccess.target.gameObject.transform.parent.gameObject;
        CommanderAccess = Commander.GetComponent<BirlikOzellik>();

        Id = CommanderAccess.Id;
        Name = CommanderAccess.Name;
        TroopType = CommanderAccess.TroopType;
        Attack = CommanderAccess.Attack;
        Defense = CommanderAccess.Defense;
        Shock = CommanderAccess.Shock;
        Speed = CommanderAccess.Speed;
        Count = CommanderAccess.Count;
        SoldierHealth = CommanderAccess.SoldierHealth;
        Morale = CommanderAccess.Morale;
        Power = CommanderAccess.Power;
        GeneralName = CommanderAccess.GeneralName;
        Side = CommanderAccess.Side;
        ActiveFormation = CommanderAccess.ActiveFormation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dustumu")
        {
            transform.position = new Vector3(transform.position.x, SoldierAccess.target.position.y, transform.position.z);
        }
    }
    void Update()
    {
        if (Time.frameCount % 10 == 0) // Her 10 frame'de bir çalışır
        {
            makeRaycast();
        }
    }
    private void makeRaycast()
    {
        float range = EffectArea.radius + 1.5f;
        hitCollider = Physics.OverlapSphere(transform.position, range);
        foreach (var item in hitCollider)
        {
            EnemyIsTrue = item.gameObject.GetComponent<AskerSavas>();
            if (EnemyIsTrue != null && EnemyIsTrue.Side != Side)
            {
                Enemy = item.gameObject;
                EnemyIsTrue.SoldierHealth -= Power;
                if (SoldierHealth <= 0)
                {
                    CommanderAccess.ReFormation = true;
                    CommanderAccess.Count -= 1;
                    Debug.Log("asker öldü, sayi:" + CommanderAccess.Count);
                    CommanderAccess.Morale-= 0.05f;
                    Destroy(Soldier);
                    Destroy(SoldierAccess.target.gameObject);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirlikOzellik : MonoBehaviour
{
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
    public bool ReFormation = false;
    public List<GameObject> Soldiers;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;

    public BirlikOzellik unit;
    void Start()
    {
        unit = transform.gameObject.GetComponent<BirlikOzellik>();
    }

    private float PowerCalculate(BirlikOzellik unit)
    {
        float Attack = unit.Attack;
        float Defense = unit.Defense;
        float Shock = unit.Shock;
        float Speed = unit.Speed;
        float Morale = unit.Morale;
        float Count = unit.Count;
        float Power = ((Attack * Morale * ((Count / 10) + 1)) + (Shock * Speed)) - (Defense * Morale * ((Count / 10) + 1));
        return Power;
    }
    void Update()
    {
        Debug.LogWarning(Count);
        if (Time.frameCount % 85 == 0) // Her 85 frame'de bir çalışır
        {
            PowerCalculate(unit);
        }
        if (Count <= 0)
        {
            Destroy(transform.gameObject);
        }
        /*if (Morale <= 0)
        {
            if (transform.position.x <= 0 && transform.position.z <= 0)
            {
                var gidilecek = new Vector3(-500,transform.position.y,-500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, unit.Speed * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
            else if (transform.position.x <= 0 && transform.position.z >= 0)
            {
                var gidilecek = new Vector3(-500, transform.position.y, +500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, unit.Speed * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
            else if (transform.position.x >= 0 && transform.position.z <= 0)
            {
                var gidilecek = new Vector3(+500, transform.position.y, -500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, unit.Speed * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
            else if (transform.position.x >= 0 && transform.position.z >= 0)
            {
                var gidilecek = new Vector3(+500, transform.position.y, +500);
                transform.position = Vector3.MoveTowards(transform.position, gidilecek, unit.Speed * Time.deltaTime);
                if (transform.position == gidilecek)
                {
                    Destroy(transform.gameObject);
                }
            }
        }*/
    }
}

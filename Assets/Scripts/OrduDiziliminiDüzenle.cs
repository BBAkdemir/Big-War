//using Assets.Scripts.Classlar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrduDiziliminiDüzenle : MonoBehaviour
{
    private HangisiHareketEtsin access;
    private ParentTakip Soldier;
    private AskerSavas SoldierWarScript;
    private HangisiHareketEtsin movementObject;
    private OrduHareket GetCommander;
    private BirlikOzellik UnitProperty;
    private Dizilim formation;

    private GameObject Commander;
    private GameObject GetMovementObject;
    private GameObject GetMovementObjectParent;
    private GameObject GetMovementObjectSoldier;
    private GameObject GetselectionArea;
    private GameObject selectionArea;
    private GameObject parentReserve;

    private List<GameObject> soldiers;
    public List<Dizilim> formationList;

    private bool ClickedF1;
    
    private Quaternion Direction;
    private Vector3 selectionAreaLocation;
    private Vector3 CommanderLocation;
    private Vector3 selectionAreaSize;

    public OrduDiziliminiDüzenle()
    {
        List<Dizilim> formationList = new List<Dizilim>();
        formationList.Add(formation1());
        formationList.Add(formation2());
        formationList.Add(formation3());
        this.formationList = formationList;
    }
    private void Start()
    {
        Commander = gameObject.transform.parent.gameObject;
        UnitProperty = Commander.GetComponent<BirlikOzellik>();
    }
    void Update()
    {
        movementObject = GameObject.Find("HangisiHareketEtsin").GetComponent<HangisiHareketEtsin>();
        soldiers = new List<GameObject>();
        
        if (UnitProperty.ReFormation == true)
        {
            GetMovementObjectParent = Commander;
            parentReserve = GetMovementObjectParent;
            TakeList(Commander);
            FormationChange(UnitProperty.ActiveFormation, Commander, parentReserve);
            UnitProperty.ReFormation = false;
        }

        if (ClickedF1 && (movementObject.movementObject.tag == "selectionArea" || movementObject.movementObject.tag == "Askerler1"))//hareket edecek varmı diye kontrol et
        {
            GetPivotAndselectionArea();
            parentReserve = GetMovementObjectParent;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                FormationChange(formation1(), GetMovementObjectParent, parentReserve);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                FormationChange(formation2(), GetMovementObjectParent, parentReserve);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FormationChange(formation3(), GetMovementObjectParent, parentReserve);
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            ClickedF1 = true;
        }
    }
    public void FormationChange(Dizilim formation, GameObject commander, GameObject commanderReserve)
    {
        UnitProperty.ActiveFormation = formation;
        Quaternion yon = commander.transform.rotation;
        selectionAreaSize = new Vector3(1.5f * formation.X + 2, 10, 1.5f * formation.Z + 2);
        selectionAreaLocation = new Vector3(commander.transform.position.x, commander.transform.position.y - 25, commander.transform.position.z);
        CommanderLocation = new Vector3(commander.transform.position.x, commander.transform.position.y, commander.transform.position.z);

        selectionArea.transform.localScale = selectionAreaSize;
        selectionArea.transform.position = selectionAreaLocation;

        Direction = new Quaternion(0, -commanderReserve.transform.rotation.y, 0, 0);

        ParentChange(false, selectionArea, soldiers);

        commanderReserve.transform.position = CommanderLocation;

        ParentChange(true, selectionArea, soldiers);

        selectionArea.transform.rotation = Direction;

        ParentChange(false, selectionArea, soldiers);

        Direction = new Quaternion(0, 0, 0, 0);
        commanderReserve.transform.rotation = Direction;

        ParentChange(true, selectionArea, soldiers);

        int b = 0;
        int a = 0;

        for (int i = formation.Z; i > 0; i--)
        {
            for (int j = formation.X; j > 0; j--)
            {
                if (formation.Details[a].Count == 1)
                {
                    Vector3 PivotPositionWillIncrease = new Vector3((j * 1.5f) + (commander.transform.position.x) - ((formation.X / 2) * 1.5f), commander.transform.position.y, (i * 1.5f) + (commander.transform.position.z) - ((1.5f * formation.Z + 2) / 2));
                    if (soldiers.Count <= b)
                    {
                        break;
                    }
                    soldiers[b].transform.rotation = Direction;
                    soldiers[b].transform.position = PivotPositionWillIncrease;
                    b++;
                }
                a++;
            }
            if (soldiers.Count <= b)
            {
                break;
            }
        }
        commander.transform.rotation = yon;
        ClickedF1 = false;
    }
    public void GetPivotAndselectionArea()
    {
        access = GameObject.Find("HangisiHareketEtsin").GetComponent<HangisiHareketEtsin>();
        soldiers = new List<GameObject>();
        GetCommander = GameObject.FindWithTag("Komutan").GetComponent<OrduHareket>();
        Soldier = GameObject.Find(access.movementObject.name).GetComponent<ParentTakip>();
        if (access.movementObject.tag == "Askerler1")
        {
            GetMovementObjectSoldier = Soldier.target.gameObject;
        }

        if (access.movementObject != null && (access.movementObject.tag == "selectionArea" || access.movementObject.tag == "Askerler1"))
        {
            var a = 0;
            if (access.movementObject.tag == "selectionArea")
            {
                GetMovementObjectParent = access.movementObject.transform.parent.gameObject;
            }
            else if (access.movementObject.tag == "Askerler1")
            {
                GetMovementObjectParent = GetMovementObjectSoldier.transform.parent.gameObject;
            }
            TakeList(GetMovementObjectParent);
        }
    }
    public void ParentChange(bool openClose, GameObject obj, List<GameObject> soldiers)
    {
        if (openClose == true)
        {
            obj.transform.parent = parentReserve.transform;
            foreach (var item in soldiers)
            {
                item.transform.parent = parentReserve.transform;
            }
        }
        else
        {
            obj.transform.parent = null;
            foreach (var item in soldiers)
            {
                item.transform.parent = null;
            }
        }
    }
    public void TakeList(GameObject obj)
    {
        var a = 0;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (obj.transform.GetChild(i).gameObject.tag == "selectionArea")
            {
                selectionArea = obj.transform.GetChild(i).gameObject; //Sçme alanını aldım.
                a = i;
                break;
            }

        }
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            if (i != a)
            {
                soldiers.Add(obj.transform.GetChild(i).gameObject); // soldiersi liseteye attık
            }
        }
    }
    public Dizilim formation1()
    {
        formation = new Dizilim();
        formation.X = 10;
        formation.Z = 10;
        formation.Name = "Klasik Dizilim";
        formation.Details = new List<Detay>();

        for (int i = 0; i < formation.X; i++)
        {
            for (int j = 0; j < formation.Z; j++)
            {
                var detay = new Detay();
                detay.X = i;
                detay.Z = j;
                detay.Count = 1;
                formation.Details.Add(detay);
            }
        }
        return formation;
    }
    public Dizilim formation2()
    {
        formation = new Dizilim();
        formation.X = 20;
        formation.Z = 5;
        formation.Name = "Geniş Dizilim";
        formation.Details = new List<Detay>();

        for (int i = 0; i < formation.X; i++)
        {
            for (int j = 0; j < formation.Z; j++)
            {
                var detay = new Detay();
                detay.X = i;
                detay.Z = j;
                detay.Count = 1;
                formation.Details.Add(detay);
            }
        }
        return formation;
    }
    public Dizilim formation3()
    {
        formation = new Dizilim();
        formation.X = 19;
        formation.Z = 10;
        formation.Name = "Üçgen Dizilim";
        formation.Details = new List<Detay>();
        var a = 1;

        for (int i = 0; i < formation.Z; i++)
        {

            var b = (formation.X - a) / 2;
            for (int j = 0; j < b; j++)
            {
                var detay = new Detay();
                detay.Count = 0;
                formation.Details.Add(detay);
            }
            for (int j = 0; j < a; j++)
            {
                var detay = new Detay();
                detay.Count = 1;
                formation.Details.Add(detay);
            }
            for (int j = 0; j < b; j++)
            {
                var detay = new Detay();
                detay.Count = 0;
                formation.Details.Add(detay);
            }
            a = a + 2;
        }
        return formation;
    }
}
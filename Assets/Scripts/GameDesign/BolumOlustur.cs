using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolumOlustur : MonoBehaviour
{
    public General general;
    public Birlik Unit;
    BirlikTuru UnitType;
    public Durum status;
    public BolumTasarimi levelDesign;
    private BirlikOzellik UnitProperty;
    public OrduDiziliminiDüzenle formations;

    public List<General> Generals;
    public List<Ordu> Armies;
    public List<BirlikTuru> UnitTypes;
    public List<Birlik> Units;
    public List<Durum> situations;

    public Terrain Terrain;
    private Vector3 TerrainLocation;

    public Transform CommanderUnitSpawnPoint1;
    public Transform CommanderUnitSpawnPoint2;
    public Transform SwordInfantrySpawnPoint1;
    public Transform SwordInfantrySpawnPoint2;
    public Transform SpearInfantrySpawnPoint1;
    public Transform SpearInfantrySpawnPoint2;
    public Transform SpearHorsemanSpawnPoint1;
    public Transform SpearHorsemanSpawnPoint2;
    public Transform HorseArcherSpawnPoint1;
    public Transform HorseArcherSpawnPoint2;
    public Transform ArcherInfantrySpawnPoint1;
    public Transform ArcherInfantrySpawnPoint2;

    public GameObject Commander;
    public GameObject Soldier;
    public GameObject Pivot;
    public GameObject selectionArea;
    private Transform IsFollowed;
    private ParentTakip IsFollowedObject;

    public int SwordInfantryCountSide1;
    public int SpearInfantryCountSide1;
    public int SpearHorsemanCountSide1;
    public int HorseArcherCountSide1;
    public int ArcherInfantryCountSide1;
    public string commanderSide1;
    public int SwordInfantryCountSide2;
    public int SpearInfantryCountSide2;
    public int SpearHorsemanCountSide2;
    public int HorseArcherCountSide2;
    public int ArcherInfantryCountSide2;
    public string commanderSide2;

    private int increase1 = 0;
    private int increase2 = 0;
    private int increase3 = 0;
    private int increase4 = 0;
    private int increase5 = 0;
    private int increase6 = 0;
    private int a = 0;

    GameObject newCommander;
    public List<GameObject> SoldiersList;
    public List<GameObject> SoldiersListReserve;

    public Material Side1;
    public Material Side2;
    void Start()
    {
        #region Generalleri Oluşturma
        General general;
        Generals = new List<General>();
        general = GeneralReturn(0001, "Berk", 10, 10, 10, 10);
        Generals.Add(general);
        general = GeneralReturn(0001, "Mert", 8, 8, 8, 8);
        Generals.Add(general);
        #endregion

        #region Unit Türü Oluşturma
        BirlikTuru UnitType;
        UnitTypes = new List<BirlikTuru>();
        UnitType = UnitTypeReturn(AskerTur.CommanderUnit, 200);
        UnitTypes.Add(UnitType);
        UnitType = UnitTypeReturn(AskerTur.SwordInfantry, 0);
        UnitTypes.Add(UnitType);
        UnitType = UnitTypeReturn(AskerTur.SpearInfantry, 0);
        UnitTypes.Add(UnitType);
        UnitType = UnitTypeReturn(AskerTur.SpearHorseman, 50);
        UnitTypes.Add(UnitType);
        UnitType = UnitTypeReturn(AskerTur.HorseArcher, 200);
        UnitTypes.Add(UnitType);
        UnitType = UnitTypeReturn(AskerTur.ArcherInfantry, 200);
        UnitTypes.Add(UnitType);
        #endregion

        #region Birlik Oluşturma
        UnitType = new BirlikTuru();
        Units = new List<Birlik>();
        Birlik Unit;
        formations = new OrduDiziliminiDüzenle();

        Unit = UnitReturn(0001, "CommanderUnit", SetType(AskerTur.CommanderUnit), 8, 5, 5, 4, 100, 100, 5, formations.formationList[0]);
        Units.Add(Unit);

        Unit = UnitReturn(0002, "SwordInfantryUnit", SetType(AskerTur.SwordInfantry), 5, 5, 2, 2, 100, 100, 3, formations.formationList[0]);
        Units.Add(Unit);

        Unit = UnitReturn(0003, "SpearInfantryUnit", SetType(AskerTur.SpearInfantry), 5, 5, 3, 2, 100, 100, 3, formations.formationList[0]);
        Units.Add(Unit);

        Unit = UnitReturn(0004, "SpearHorsemanUnit", SetType(AskerTur.SpearHorseman), 7, 4, 4, 4, 64, 100, 3, formations.formationList[0]);
        Units.Add(Unit);

        Unit = UnitReturn(0005, "HorseArcherUnit", SetType(AskerTur.HorseArcher), 3, 3, 1, 4, 64, 100, 3, formations.formationList[0]);
        Units.Add(Unit);

        Unit = UnitReturn(0006, "ArcherInfantryUnit", SetType(AskerTur.ArcherInfantry), 3, 3, 1, 2, 100, 100, 3, formations.formationList[0]);
        Units.Add(Unit);
        #endregion

        #region Ordu Oluşturma
        general = new General();
        Armies = new List<Ordu>();
        Ordu army;

        army = ArmyReturn(GeneralDetermine(commanderSide1), UnitFill(SwordInfantryCountSide1, SpearInfantryCountSide1, SpearHorsemanCountSide1, HorseArcherCountSide1, ArcherInfantryCountSide1, Units));
        Armies.Add(army); ;

        army = ArmyReturn(GeneralDetermine(commanderSide2), UnitFill(SwordInfantryCountSide2, SpearInfantryCountSide2, SpearHorsemanCountSide2, HorseArcherCountSide2, ArcherInfantryCountSide2, Units));
        Armies.Add(army);
        #endregion

        #region Ordu Taraflarını Oluşturma
        Durum status;
        situations = new List<Durum>();
        status = statusReturn(Armies[0], 1);
        situations.Add(status);
        status = statusReturn(Armies[1], 2);
        situations.Add(status);
        # endregion

        #region Bölüm Tasarımı Oluşturma
        BolumTasarimi levelDesign;
        levelDesign = LevelDesignReturn(Armies, situations, 150);
        #endregion

        #region Orduları Getir
        foreach (var Army in levelDesign.Army)
        {
            increase1 = 0;
            increase2 = 0;
            increase3 = 0;
            increase4 = 0;
            increase5 = 0;
            increase6 = 0;
            foreach (var unit in Army.Units)
            {
                if (unit.UnitType.Name == AskerTur.CommanderUnit && Army.General.Name == "Berk")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(CommanderUnitSpawnPoint1.transform.position, increase1, unit.Count, 1);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 1, SoldiersListReserve);
                    increase1 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.CommanderUnit && Army.General.Name == "Mert")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(CommanderUnitSpawnPoint2.transform.position, increase1, unit.Count, 2);

                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 2, SoldiersListReserve);
                    increase1 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.SwordInfantry && Army.General.Name == "Berk")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(SwordInfantrySpawnPoint1.transform.position, increase2, unit.Count, 1);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 1, SoldiersListReserve);
                    increase2 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.SwordInfantry && Army.General.Name == "Mert")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(SwordInfantrySpawnPoint2.transform.position, increase2, unit.Count, 2);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 2, SoldiersListReserve);
                    increase2 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.SpearInfantry && Army.General.Name == "Berk")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(SpearInfantrySpawnPoint1.transform.position, increase3, unit.Count, 1);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 1, SoldiersListReserve);
                    increase3 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.SpearInfantry && Army.General.Name == "Mert")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(SpearInfantrySpawnPoint2.transform.position, increase3, unit.Count, 2);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 2, SoldiersListReserve);
                    increase3 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.SpearHorseman && Army.General.Name == "Berk")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(SpearHorsemanSpawnPoint1.transform.position, increase4, unit.Count, 1);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 1, SoldiersListReserve);
                    increase4 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.SpearHorseman && Army.General.Name == "Mert")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(SpearHorsemanSpawnPoint2.transform.position, increase4, unit.Count, 2);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 2, SoldiersListReserve);
                    increase4 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.HorseArcher && Army.General.Name == "Berk")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(HorseArcherSpawnPoint1.transform.position, increase5, unit.Count, 1);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 1, SoldiersListReserve);
                    increase5 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.HorseArcher && Army.General.Name == "Mert")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(HorseArcherSpawnPoint2.transform.position, increase5, unit.Count, 2);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 2, SoldiersListReserve);
                    increase5 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.ArcherInfantry && Army.General.Name == "Berk")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(ArcherInfantrySpawnPoint1.transform.position, increase6, unit.Count, 1);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 1, SoldiersListReserve);
                    increase6 += 1;
                    a += 1;
                }
                if (unit.UnitType.Name == AskerTur.ArcherInfantry && Army.General.Name == "Mert")
                {
                    SoldiersListReserve = new List<GameObject>();
                    SoldiersListReserve = ArmyCreate(ArcherInfantrySpawnPoint2.transform.position, increase6, unit.Count, 2);
                    AssignUnitAttributes(unit.Id, unit.Name, unit.Attack, unit.Defense, unit.Shock, unit.Speed, unit.Count, unit.SoldierHealth, unit.Morale, PowerCalculate(unit), unit.UnitType.Name, Army.General.Name, unit.ActiveFormation, 2, SoldiersListReserve);
                    increase6 += 1;
                    a += 1;
                }
            }
        }
        #endregion
    }
    public void AssignUnitAttributes(int Id, string Name, float Attack, float Defense, float Shock, float Speed, int Count, float SoldierHealth, float Morale, float Power, AskerTur TroopType, string GeneralName, Dizilim ActiveFormation, int Side, List<GameObject> Soldiers)
    {
        UnitProperty = GameObject.Find(newCommander.name).GetComponent<BirlikOzellik>();
        UnitProperty.Id = Id;
        UnitProperty.Name = Name;
        UnitProperty.Attack = Attack;
        UnitProperty.Defense = Defense;
        UnitProperty.Shock = Shock;
        UnitProperty.Speed = Speed;
        UnitProperty.Count = Count;
        UnitProperty.SoldierHealth = SoldierHealth;
        UnitProperty.Morale = Morale;
        UnitProperty.Power = Power;
        UnitProperty.TroopType = TroopType;
        UnitProperty.GeneralName = GeneralName;
        UnitProperty.ActiveFormation = ActiveFormation;
        UnitProperty.Side = Side;
        UnitProperty.Soldiers = Soldiers;
    }
    public List<Birlik> UnitFill(int SwordInfantryCount, int SpearInfantryCount, int SpearHorsemanCount, int HorseArcherCount, int ArcherInfantryCount, List<Birlik> Units)
    {
        var UntiReserve = new List<Birlik>();
        foreach (var item in Units)
        {
            if (item.Name == "CommanderUnit")
            {
                UntiReserve.Add(item);
            }
            if (item.Name == "SwordInfantryUnit")
            {
                for (int i = 0; i < SwordInfantryCount; i++)
                {
                    UntiReserve.Add(item);
                }
            }
            if (item.Name == "SpearInfantryUnit")
            {
                for (int i = 0; i < SpearInfantryCount; i++)
                {
                    UntiReserve.Add(item);
                }
            }
            if (item.Name == "SpearHorsemanUnit")
            {
                for (int i = 0; i < SpearHorsemanCount; i++)
                {
                    UntiReserve.Add(item);
                }
            }
            if (item.Name == "HorseArcherUnit")
            {
                for (int i = 0; i < HorseArcherCount; i++)
                {
                    UntiReserve.Add(item);
                }
            }
            if (item.Name == "ArcherInfantryUnit")
            {
                for (int i = 0; i < ArcherInfantryCount; i++)
                {
                    UntiReserve.Add(item);
                }
            }
        }
        return UntiReserve;
    }
    public General GeneralReturn(int Id, string Name, int additionalAttack, int additionalDefense, int additionalShock, int additionalMorale)
    {
        var general = new General();
        general.Id = Id;
        general.Name = Name;
        general.additionalAttack = additionalAttack;
        general.additionalDefense = additionalDefense;
        general.additionalShock = additionalShock;
        general.additionalMorale = additionalMorale;

        return general;
    }
    public BirlikTuru UnitTypeReturn(AskerTur Name, int Value)
    {
        var UnitType = new BirlikTuru();
        UnitType.Name = Name;
        UnitType.Value = Value;
        return UnitType;
    }
    public Birlik UnitReturn(int Id, string Name, BirlikTuru UnitType, int Attack, int Defense, int Shock, int Speed, int Count, int SoldierHealth, int Morale, Dizilim ActiveFormation)
    {
        var unit = new Birlik();

        unit.Id = Id;
        unit.Name = Name;
        unit.UnitType = UnitType;
        unit.Attack = Attack;
        unit.Defense = Defense;
        unit.Shock = Shock;
        unit.Speed = Speed;
        unit.Count = Count;
        unit.SoldierHealth = SoldierHealth;
        unit.Morale = Morale;
        unit.ActiveFormation = ActiveFormation;

        return unit;
    }
    public Ordu ArmyReturn(General General, List<Birlik> Units)
    {
        var army = new Ordu();
        army.General = General;
        army.Units = Units;

        return army;
    }
    public Durum statusReturn(Ordu Army, int Side)
    {
        var status = new Durum();
        status.Army = Army;
        status.Side = Side;

        return status;
    }
    public BolumTasarimi LevelDesignReturn(List<Ordu> Army, List<Durum> Status, int LevelScore)
    {
        var levelDesign = new BolumTasarimi();
        levelDesign.Army = Army;
        levelDesign.Status = Status;
        levelDesign.LevelScore = LevelScore;
        return levelDesign;
    }
    public BirlikTuru SetType(AskerTur TypeName)
    {
        UnitType = new BirlikTuru();
        foreach (var item in UnitTypes)
        {
            if (item.Name == TypeName)
            {
                UnitType = item;
            }
        }
        return UnitType;
    }
    public General GeneralDetermine(string generalName)
    {
        foreach (var item in Generals)
        {
            if (item.Name == generalName)
            {
                general = item;
            }
        }
        return general;
    }
    private float PowerCalculate(Birlik Unit)
    {
        float Attack = Unit.Attack;
        float Defense = Unit.Defense;
        float Shock = Unit.Shock;
        float Speed = Unit.Speed;
        float Morale = Unit.Morale;
        float Count = Unit.Count;
        float Power = ((Attack * Morale * ((Count / 10) + 1)) + (Shock * Speed)) - (Defense * Morale * ((Count / 10) + 1));
        return Power;
    }
    public List<GameObject> ArmyCreate(Vector3 SpawnPoint, int increase, int Count, int side)
    {
        Vector3 CommanderLocation;
        Quaternion CommanderDirection;
        Vector3 selectionAreaLocation;
        Quaternion selectionAreaDirection;
        SoldiersList = new List<GameObject>();
        GameObject newselectionArea;
        double Karekok = Math.Sqrt(Count);
        int k = 7;
        int l = 1;
        int m = 5;
        int n = 1;
        int o = 6;
        int p = 1;

        Vector3 pos = new Vector3(SpawnPoint.x + increase * 25, 0, SpawnPoint.z);

        CommanderLocation = new Vector3(pos.x, Terrain.activeTerrain.SampleHeight(pos), pos.z);
        CommanderDirection = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        selectionAreaLocation = new Vector3(SpawnPoint.x + increase * 25, 10.0f, SpawnPoint.z + 6.5f);
        selectionAreaDirection = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        newCommander = Instantiate(Commander, CommanderLocation, CommanderDirection);
        newCommander.name = "Komutan" + a;

        newselectionArea = Instantiate(selectionArea, selectionAreaLocation, selectionAreaDirection);
        newselectionArea.name = "SecmeAlani" + a;
        newselectionArea.transform.parent = newCommander.transform;

        for (int i = 0; i < Karekok; i++)
        {
            for (int j = 1; j < (Karekok / 2); j++)
            {

                Vector3 PivotLocationWillIncrease = new Vector3(j * 1.5f, 0.0f, i * 1.5f);
                GameObject newPivot = Instantiate(Pivot, CommanderLocation + PivotLocationWillIncrease, CommanderDirection);
                newPivot.name = "Pivot" + a + k + l;
                GameObject newSoldier = Instantiate(Soldier, CommanderLocation + PivotLocationWillIncrease, CommanderDirection);
                newSoldier.name = "Asker" + a + k + l;

                if (side == 1)
                {
                    MeshRenderer color = GameObject.Find(newSoldier.name).GetComponent<MeshRenderer>();
                    color.material = Side1;
                }
                else
                {
                    MeshRenderer color = GameObject.Find(newSoldier.name).GetComponent<MeshRenderer>();
                    color.material = Side2;
                }

                newPivot.transform.parent = newCommander.transform;
                IsFollowedObject = newSoldier.GetComponent<ParentTakip>();
                IsFollowedObject.target = newPivot.transform;
                SoldiersList.Add(newSoldier);
                k += 1;

            }
            l += 1;
            k = 7;
        }
        for (int i = 1; i <= (Karekok / 2); i++)
        {
            for (int j = 0; j < Karekok; j++)
            {
                Vector3 PivotLocationWillIncrease = new Vector3(-i * 1.5f, 0.0f, j * 1.5f);
                GameObject newPivot = Instantiate(Pivot, CommanderLocation + PivotLocationWillIncrease, CommanderDirection);
                newPivot.name = "Pivot" + a + m + n;
                GameObject newSoldier = Instantiate(Soldier, CommanderLocation + PivotLocationWillIncrease, CommanderDirection);
                newSoldier.name = "Asker" + a + m + n;
                if (side == 1)
                {
                    MeshRenderer color = GameObject.Find(newSoldier.name).GetComponent<MeshRenderer>();
                    color.material = Side1;
                }
                else
                {
                    MeshRenderer color = GameObject.Find(newSoldier.name).GetComponent<MeshRenderer>();
                    color.material = Side2;
                }
                newPivot.transform.parent = newCommander.transform;
                IsFollowedObject = newSoldier.GetComponent<ParentTakip>();
                IsFollowedObject.target = newPivot.transform;
                SoldiersList.Add(newSoldier);
                n += 1;
            }
            m -= 1;
            n = 1;
        }
        for (int i = 0; i < Karekok; i++)
        {
            Vector3 PivotLocationWillIncrease = new Vector3(0, 0.0f, i * 1.5f);
            GameObject newPivot = Instantiate(Pivot, CommanderLocation + PivotLocationWillIncrease, CommanderDirection);
            newPivot.name = "Pivot" + a + o + p;

            GameObject newSoldier = Instantiate(Soldier, CommanderLocation + PivotLocationWillIncrease, CommanderDirection);
            newSoldier.name = "Asker" + a + o + p;

            if (side == 1)
            {
                MeshRenderer color = GameObject.Find(newSoldier.name).GetComponent<MeshRenderer>();
                color.material = Side1;
            }
            else
            {
                MeshRenderer color = GameObject.Find(newSoldier.name).GetComponent<MeshRenderer>();
                color.material = Side2;
            }
            newPivot.transform.parent = newCommander.transform;
            IsFollowedObject = newSoldier.GetComponent<ParentTakip>();
            IsFollowedObject.target = newPivot.transform;
            SoldiersList.Add(newSoldier);
            p += 1;
        }
        return SoldiersList;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Birlik
{
    public int Id { get; set; }
    public string Name { get; set; }
    public BirlikTuru UnitType { get; set; }
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float Shock { get; set; }
    public float Speed { get; set; }
    public int Count { get; set; }
    public float SoldierHealth { get; set; }
    public float Morale { get; set; }
    public float Power { get; set; }
    public List<GameObject> Soldiers { get; set; }
    public Dizilim ActiveFormation { get; set; }
}

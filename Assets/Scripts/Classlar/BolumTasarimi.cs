using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolumTasarimi
{
    public List<Ordu> Army { get; set; }
    public List<Durum> Status { get; set; }
    public Terrain Terrain { get; set; }
    public int LevelScore { get; set; }
}
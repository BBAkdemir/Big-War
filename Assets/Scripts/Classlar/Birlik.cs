using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Birlik
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public BirlikTuru BirlikTuru { get; set; }
    public float Saldiri { get; set; }
    public float Savunma { get; set; }
    public float Sok { get; set; }
    public float Hiz { get; set; }
    public int Sayi { get; set; }
    public float AskerCan { get; set; }
    public float Moral { get; set; }
    public float Guc { get; set; }
    public List<GameObject> Askerler { get; set; }
    public Dizilim AktifDizilim { get; set; }
}

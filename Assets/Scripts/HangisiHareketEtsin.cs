using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class HangisiHareketEtsin : MonoBehaviour
{
    public GameObject hareketEdecek;
    void Start()
    {
        hareketEdecek = null;
    }
    void SelectObject(GameObject komutan)
    {
        if(hareketEdecek != null)
        {
            if(komutan == hareketEdecek)
            {
                return;
            }
            ClearSelection();
        }
        hareketEdecek = komutan;
    }
    void ClearSelection()
    {
        if(hareketEdecek == null)
        {
            return;
        }
        hareketEdecek = null;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if(Input.GetMouseButtonDown(0))
            {
                SelectObject(hitObject);
            }
        }
        else
        {
            ClearSelection();
        }
    }
}

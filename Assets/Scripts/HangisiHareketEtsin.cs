using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class HangisiHareketEtsin : MonoBehaviour
{
    public GameObject movementObject;
    void Start()
    {
        movementObject = null;
    }
    void SelectObject(GameObject commander)
    {
        if(movementObject != null)
        {
            if(commander == movementObject)
            {
                return;
            }
            ClearSelection();
        }
        movementObject = commander;
    }
    void ClearSelection()
    {
        if(movementObject == null)
        {
            return;
        }
        movementObject = null;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofres : MonoBehaviour
{
    public bool isChestOpen = false;
    public GameObject _diamantes;
    public Transform _Spawner; 

    void Update()
    {
        if(isChestOpen == true)
        {
            return;
        }
    }
    public void OpenChest()
    {
        isChestOpen = true;
        Instantiate(_diamantes, _Spawner.position, _Spawner.rotation);
        Debug.Log("Cofre abierto");
    }
}

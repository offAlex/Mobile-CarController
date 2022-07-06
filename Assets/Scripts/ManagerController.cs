using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerController : MonoBehaviour
{   
    public GameObject _player;
    public Transform startPos;

    // Start is called before the first frame update
    void Awake()
    {   
        Instantiate(_player, new Vector3(startPos.position.x  ,startPos.position.y, startPos.position.z) ,Quaternion.Euler(0, 90, 0));
    }
}

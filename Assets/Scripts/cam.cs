using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{   
    public Camera _camera;
    public Canvas _canvas;
    [SerializeField] private Transform _player;
 
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {   
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _camera.transform.SetParent(_player.transform);
        _canvas.transform.SetParent(_player.transform);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstiatePrehab : MonoBehaviour
{
    private RaycastHit _hit;

    [SerializeField]
    private GameObject _gameObject;


    // Update is called once per frame
    void Update()
    {
        ///ƒ‚ƒfƒ‹‚Ì¶¬
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if(touch.phase == TouchPhase.Began )
            {
                var _ray = Camera.main.ScreenPointToRay( touch.position );
                if(Physics.Raycast( _ray, out _hit ))
                {
                    Instantiate(_gameObject,_hit.point,Quaternion.Euler(0,180,0));
                }
            }
        }
    }
}

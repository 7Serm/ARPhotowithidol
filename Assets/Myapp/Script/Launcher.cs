using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Launcher : MonoBehaviour
{
    [SerializeField] GameObject _modelprefab;
    [SerializeField] GameObject _emptyobjprehab;
    private RaycastHit _hit;



    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
                if (touch.phase == TouchPhase.Began)
                {
                    var _ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(_ray, out _hit))

                    {
                        Instantiate(_modelprefab, _hit.transform.position, _hit.transform.rotation);
                        GameObject emptyobj = Instantiate(_emptyobjprehab, _hit.transform.position, _hit.transform.rotation);
                        emptyobj.transform.SetParent(Camera.main.transform);
                        ModelScript modelscp= _modelprefab.GetComponent<ModelScript>();
                       // modelscp._emptyObject = emptyobj;
                    }
                } 
            

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Niantic.Lightship.AR.NavigationMesh;
using System;

public class NaviMesh : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LightshipNavMeshManager _navimeshManager;

    [SerializeField]
    private LightshipNavMeshAgent _agentPrehab;

    private LightshipNavMeshAgent _agentInstance;
   
    // Update is called once per frame
    void Update()
    {
        Handletouch();
    }

    private void SetVisualizetion(bool isVisualizetion)
    {
        _navimeshManager.GetComponent<LightshipNavMeshRenderer>().enabled = isVisualizetion;

        if (_agentInstance != null)
        {
            _agentInstance.GetComponent<LightshipNavMeshAgentPathRenderer>().enabled = isVisualizetion;
        }
    }
    private void Handletouch()
    {
        var touch = Input.GetTouch(0);

        if(Input.touchCount <= 0)return;

        if(touch.phase == TouchPhase.Began)
        {
            Ray ray = _camera.ScreenPointToRay(touch.position);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(_agentInstance == null)
                {
                    _agentInstance = Instantiate(_agentPrehab);
                    _agentInstance.transform.position = hit.point;
                }

                else
                {
                    _agentInstance.SetDestination(hit.point);
                }
            }
        }
    }
}

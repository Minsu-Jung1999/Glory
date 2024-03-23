using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterRayController : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    float rayDistance = 10f;
    [SerializeField]
    Camera camera;

    // Update is called once per frame
    void Update()
    {
        // ray 거리 보이게 하기 [디버그 용]
        Debug.DrawRay(camera.transform.position, camera.transform.forward *  rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("communicator"))
            {
                Debug.Log("맞음");
            }
        }
    }
}

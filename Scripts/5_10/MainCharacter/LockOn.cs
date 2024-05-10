using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // ray의 길이
    [SerializeField]
    private float _maxDistance = 20.0f;

    // ray의 색상
    [SerializeField]
    private Color _rayColor = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;
        float sphereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        Camera mainCamera = Camera.main; // 메인 카메라를 가져옴

        if (mainCamera != null)
        {
            // 카메라가 바라보는 방향을 구하기 위해 화면의 중심점을 기준으로 한 레이캐스트
            Ray cameraCenterRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            if (Physics.SphereCast(mainCamera.transform.position, sphereScale / 2.0f, cameraCenterRay.direction, out RaycastHit hit, _maxDistance))
            {
                // Hit된 지점까지 ray를 그려준다.
                Gizmos.DrawRay(mainCamera.transform.position, cameraCenterRay.direction * hit.distance);

                // Hit된 지점에 Sphere를 그려준다.
                Gizmos.DrawWireSphere(mainCamera.transform.position + cameraCenterRay.direction * hit.distance, sphereScale / 2.0f);
            }
            else
            {
                // Hit가 되지 않았으면 최대 검출 거리로 ray를 그려준다.
                Gizmos.DrawRay(transform.position, cameraCenterRay.direction * _maxDistance);
            }
        }
    }
}

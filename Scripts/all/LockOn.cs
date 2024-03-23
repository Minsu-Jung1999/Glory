using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    // ray�� ����
    [SerializeField]
    private float _maxDistance = 20.0f;

    // ray�� ����
    [SerializeField]
    private Color _rayColor = Color.red;

    void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;
        float sphereScale = Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);

        Camera mainCamera = Camera.main; // ���� ī�޶� ������

        if (mainCamera != null)
        {
            // ī�޶� �ٶ󺸴� ������ ���ϱ� ���� ȭ���� �߽����� �������� �� ����ĳ��Ʈ
            Ray cameraCenterRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

            if (Physics.SphereCast(mainCamera.transform.position, sphereScale / 2.0f, cameraCenterRay.direction, out RaycastHit hit, _maxDistance))
            {
                // Hit�� �������� ray�� �׷��ش�.
                Gizmos.DrawRay(mainCamera.transform.position, cameraCenterRay.direction * hit.distance);

                // Hit�� ������ Sphere�� �׷��ش�.
                Gizmos.DrawWireSphere(mainCamera.transform.position + cameraCenterRay.direction * hit.distance, sphereScale / 2.0f);
            }
            else
            {
                // Hit�� ���� �ʾ����� �ִ� ���� �Ÿ��� ray�� �׷��ش�.
                Gizmos.DrawRay(transform.position, cameraCenterRay.direction * _maxDistance);
            }
        }
    }
}

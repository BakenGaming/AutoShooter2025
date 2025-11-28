using UnityEngine;

public class OrbitFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public void Initialize()
    {
        playerTransform = GameManager.i.GetPlayerGO().transform;
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position;
    }
}

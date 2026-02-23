using UnityEngine;

public class LanesMovement : MonoBehaviour
{
    [SerializeField] Transform playerCarTransform;
    [SerializeField] float offSet = -5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offSet;
        transform.position = cameraPos;
    }
}

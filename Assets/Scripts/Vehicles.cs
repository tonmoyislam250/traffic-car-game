using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    void Start()
    {
    }

    void Update()
    {
        transform.Translate(0f, 0f, speed * Time.deltaTime);
    }
}
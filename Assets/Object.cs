using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.red;
        int randomValue = Random.Range(1, 101);
        Debug.Log("Случайное число: " + randomValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

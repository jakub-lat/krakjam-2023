using UnityEngine;

// https://www.youtube.com/watch?v=zit45k6CUMk
public class Parallax : MonoBehaviour
{
    private GameObject cameraObj;
    
    private float startPosition;
    public float length, parallaxFactor;

    public bool repeat = true;

    void Start()
    {
        startPosition = transform.position.x; 
        cameraObj = Camera.main.gameObject;
        if(length == 0) length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        var temp = (cameraObj.transform.position.x * (1 - parallaxFactor));
        var dist = (cameraObj.transform.position.x * parallaxFactor);
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (!repeat) return;
        if (temp > startPosition + length) startPosition += length;
        else if (temp < startPosition - length) startPosition -= length;
    }
}

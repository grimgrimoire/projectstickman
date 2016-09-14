using UnityEngine;
using System.Collections;

public class CustomCameraMan : MonoBehaviour
{

    public GameObject player;
    public float rightLimit;
    public float leftLimit;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
            transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, leftLimit, rightLimit), transform.position.y, -10);
    }

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
}

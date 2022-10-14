using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CerrarVideo : MonoBehaviour
{
    public VideoPlayer video;
    private void Awake()
    {
        video.loopPointReached += CheckOver;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CheckOver(VideoPlayer vp)
    {
        gameObject.SetActive(false);

    }
}

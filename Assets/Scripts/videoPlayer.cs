using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class videoPlayer : MonoBehaviour
{
    public VideoPlayer vid;


    void Start() { vid.loopPointReached += CheckOver; }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //비디오끝나면 엔딩씬 출력
        SceneManager.LoadScene("EndScene");
    }
}

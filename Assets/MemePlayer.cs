using UnityEngine;
using UnityEngine.Video;

public class MemePlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = System.IO.Path.Combine(
            Application.streamingAssetsPath, "mua-quat.webm"
        );
        videoPlayer.Play();
    }
}

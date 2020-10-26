using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace UI {
    public class TransferVideoTex : MonoBehaviour {
        private VideoPlayer videoPlayer;
        private RawImage rawImage;

        private void Awake() {
            videoPlayer = GetComponent<VideoPlayer>();
            rawImage = GetComponent<RawImage>();
        }

        private void Update() {
            rawImage.texture = videoPlayer.texture;
        }
    }
}

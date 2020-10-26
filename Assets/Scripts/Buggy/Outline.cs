using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Buggy {
    public class Outline : MonoBehaviour
    {

        private Image image;
        private Transform outlinetf;

        private Color tempColor;

        private bool fst;

        [Header("时间控制参数")]
        public float activeTime;
        public float activeStart;

        [Header("不透明度控制")]
        public float alpha;
        public float alphaSet;
        public float alphaMutipler;

        private void Awake()
        {
            image = GetComponent<Image>();


        
        }

        private void Start()
        {
            alpha = alphaSet;
            fst = true;
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                if (fst)
                {
                    activeStart = Time.time;
                    fst = false;
                }
                DissloveEffect();

            }
        }

        private void DissloveEffect()
        {
            alpha *= alphaMutipler;

            tempColor = new Color(1, 1, 1, alpha);
            image.color = tempColor;
            if (Time.time >= activeTime+activeStart)
            {
                //消失
                gameObject.SetActive(false);
            }
        }
    }
}

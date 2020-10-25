using UnityEngine;

namespace Map {
    public class MapBlockUI : MonoBehaviour {
        public SpriteRenderer spr;

        public Vector2Int Position { get; set; }

        public void SetSprite(Sprite sprite, Vector2 offset) {
            spr.sprite = sprite;
            spr.color = Color.white;
            spr.transform.localPosition = offset;
        }

        public void ClearSprite() {
            spr.sprite = null;
            spr.transform.localPosition = Vector3.zero;
        }
    }
}

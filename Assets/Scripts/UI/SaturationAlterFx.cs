using UnityEngine;

namespace UI {
    public class SaturationAlterFx : MonoBehaviour {
        public Shader shader;
        public float saturation = 1;

        private Material material;

        public static Material CreateMaterial(Shader shader) {
            if(shader == null || !shader.isSupported) {
                Debug.LogError($"Shader error: {shader}");
                return null;
            }
            return new Material(shader) {hideFlags = HideFlags.DontSave};
        }


        private void Awake() {
            material = CreateMaterial(shader);
            enabled = material != null;
        }


        private static readonly int SaturationID = Shader.PropertyToID("_Saturation");

        private void OnRenderImage(RenderTexture src, RenderTexture dest) {
            material.SetFloat(SaturationID, saturation);
            Graphics.Blit(src, dest, material);
        }
    }
}

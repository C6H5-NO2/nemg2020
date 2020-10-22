using UnityEngine;
using Util;

namespace Property {
    public class PropertyManager : ManualSingleton<PropertyManager> {
        public PropertyManager() {
            GameProp = new PropertyReprGroup();
        }

        private PropertyReprGroup gameProp;
        public PropertyReprGroup GameProp {
            get => gameProp;

            private set {
                gameProp = value;

                var ins = SceneObjRef.Instance;
                if(ins != null) {
                    var ui = ins.GamePropUI;
                    if(ui != null)
                        ui.UpdateText();
                }
            }
        }

        public void AddProperty(PropertyRepr prop) {
            GameProp += prop;
        }

        public void AddProperty(PropertyReprGroup group) {
            GameProp += group;
        }

        public void SubtractProperty(PropertyRepr prop) {
            GameProp -= prop;
        }

        public void SubtractProperty(PropertyReprGroup group) {
            GameProp -= group;
        }

        public bool CanSubtractProperty(PropertyRepr prop) {
            var ans = GameProp - prop;
            return ans[PropertyType.Population] >= 0 && ans[PropertyType.Finance] >= 0;
        }

        public bool CanSubtractProperty(PropertyReprGroup group) {
            //return !(GameProp - group).Any(p => p < 0);
            var ans = GameProp - group;
            return ans[PropertyType.Population] >= 0 && ans[PropertyType.Finance] >= 0;
        }

        public override void OnReset() {
            GameProp = new PropertyReprGroup();
        }
    }
}

namespace Property {
    public class PropertyManager {
        public PropertyManager() {
            GameProp = new PropertyReprGroup();
        }

        public PropertyReprGroup GameProp { get; private set; }

        public void Reset() {
            GameProp = new PropertyReprGroup();
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
            return !(GameProp - prop).Any(p => p < 0);
        }

        public bool CanSubtractProperty(PropertyReprGroup group) {
            return !(GameProp - group).Any(p => p < 0);
        }
    }
}

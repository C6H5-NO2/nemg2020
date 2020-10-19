using System;
using UnityEngine;

namespace Property {
    [Serializable]
    public struct PropertyRepr {
        public PropertyRepr(PropertyType type, int count) {
            this.type = type;
            this.count = count;
        }

        [SerializeField] private PropertyType type;
        [SerializeField] private int count;

        // Unity cannot show auto properties on inspector
        public PropertyType Type => type;
        public int Count => count;

        public override string ToString() => $"[{type.ToString()}, {count}]";

        public bool TypeMatched(PropertyRepr b) => type == b.type;
        public static bool TypeMatched(PropertyRepr a, PropertyRepr b) => a.type == b.type;

        public static PropertyRepr operator+(PropertyRepr a, PropertyRepr b) {
        #if GAME_DEBUG_MODE
            if(!TypeMatched(a, b))
                throw new ArgumentException();
        #endif
            return new PropertyRepr(a.type, a.count + b.count);
        }

        public static PropertyRepr operator-(PropertyRepr a, PropertyRepr b) {
        #if GAME_DEBUG_MODE
            if(!TypeMatched(a, b))
                throw new ArgumentException();
        #endif
            return new PropertyRepr(a.type, a.count - b.count);
        }

        public static PropertyRepr operator*(PropertyRepr a, float f) {
            return new PropertyRepr(a.type, (int)(a.count * f));
        }
    }
}

using System;
using UnityEngine;

namespace Game.Prop {
    [Serializable]
    public struct PropRepr {
        public PropRepr(PropType type, int count) {
            this.type = type;
            this.count = count;
        }

        [SerializeField] private PropType type;
        [SerializeField] private int count;

        // Unity cannot show auto properties on inspector
        public PropType Type => type;
        public int Count => count;

        public override string ToString() => $"[{type.ToString()}, {count}]";

        public bool TypeMatched(PropRepr b) => this.type == b.type;
        public static bool TypeMatched(PropRepr a, PropRepr b) => a.type == b.type;

        public static PropRepr operator+(PropRepr a, PropRepr b) {
        #if GAME_DEBUG_MODE
            if(!TypeMatched(a, b))
                throw new ArgumentException();
        #endif
            return new PropRepr(a.type, a.count + b.count);
        }

        public static PropRepr operator-(PropRepr a, PropRepr b) {
        #if GAME_DEBUG_MODE
            if(!TypeMatched(a, b))
                throw new ArgumentException();
        #endif
            return new PropRepr(a.type, a.count - b.count);
        }
    }
}

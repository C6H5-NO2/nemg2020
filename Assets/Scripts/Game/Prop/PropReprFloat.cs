using System;
using UnityEngine;

namespace Game.Prop {
    [Serializable]
    public struct PropReprFloat {
        public PropReprFloat(PropType type, float factor) {
            this.type = type;
            this.factor = factor;
        }

        [SerializeField] private PropType type;
        [SerializeField] private float factor;

        // Unity cannot show auto properties on inspector
        public PropType Type => type;
        public float Factor => factor;

        public override string ToString() => $"[{type.ToString()}, {factor}]";

        public bool TypeMatched(PropReprFloat b) => this.type == b.type;
        public static bool TypeMatched(PropReprFloat a, PropReprFloat b) => a.type == b.type;

        public static PropReprFloat operator*(PropReprFloat a, PropReprFloat b) {
        #if UNITY_EDITOR
            if(!TypeMatched(a, b))
                throw new ArgumentException();
        #endif
            return new PropReprFloat(a.type, a.factor * b.factor);
        }

        public static explicit operator PropReprFloat(PropRepr repr) => new PropReprFloat(repr.Type, repr.Count);
        public static explicit operator PropRepr(PropReprFloat repr) => new PropRepr(repr.Type, (int)repr.Factor);
    }
}

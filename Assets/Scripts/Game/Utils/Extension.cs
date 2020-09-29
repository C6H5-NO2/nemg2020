namespace Game.Utils {
    public static class Extension {
        /// <summary>True if obj passes the lifetime check of the underlying engine object. False otherwise.</summary>
        public static bool CheckLifetime(this UnityEngine.Object obj) => obj != null;
    }
}

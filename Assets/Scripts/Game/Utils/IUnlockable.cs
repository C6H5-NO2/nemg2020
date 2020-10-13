namespace Game.Utils {
    public interface IUnlockable {
        bool Locked { get; }
        bool CanUnlock();
        bool Unlock();
        void ForceUnlock();
    }
}

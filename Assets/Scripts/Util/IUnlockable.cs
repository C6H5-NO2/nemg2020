namespace Util {
    public interface IUnlockable {
        bool Unlocked { get; }

        bool CanUnlock();
        bool TryUnlock();
        void ForceUnlock();

        void Lock();
    }
}

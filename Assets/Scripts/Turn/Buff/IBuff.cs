namespace Turn.Buff {
    public interface IBuff {
        void OnApplied();
        void OnRemoved();
        void OnNewTurn(int turn);
        bool IsActive();
    }
}

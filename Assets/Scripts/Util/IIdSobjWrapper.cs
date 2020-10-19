namespace Util {
    public interface IIdSobjWrapper<out T> where T : IdSobj {
        T Sobj { get; }
    }
}

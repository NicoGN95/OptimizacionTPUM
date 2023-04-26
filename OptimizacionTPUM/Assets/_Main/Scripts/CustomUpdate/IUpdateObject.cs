namespace _Main.Scripts.CustomUpdate
{
    public interface IUpdateObject
    {
        void SubscribeUpdateManager();
        void UnSubscribeUpdateManager();
        void MyUpdate();
    }
}
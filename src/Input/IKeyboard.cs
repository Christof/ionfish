namespace Input
{
    public interface IKeyboard
    {
        void Update();
        bool IsPressed(Button button);
    }
}
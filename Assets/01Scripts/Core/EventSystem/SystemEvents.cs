namespace GGM.EventSystem
{
    public static class SystemEvents
    {
        public static readonly FadeEvent FadeEvent = new FadeEvent();
        public static readonly FadeEndEvent FadeEndEvent = new FadeEndEvent();
    }

    public class FadeEvent : GameEvent
    {
        public bool isFadeIn;
    }

    public class FadeEndEvent : GameEvent
    {
    }
}

namespace InitiativeTracker.Rendering
{
    public static class RenderFactory
    {
        public static IRenderer GetRenderer()
        {
            return ConsoleRenderer.Instance();
        }
    }
}

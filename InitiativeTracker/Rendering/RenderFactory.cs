namespace InitiativeTracker.Rendering
{
    public static class RenderFactory
    {
        public static IRenderable GetRenderer()
        {
            return ConsoleRenderer.Instance();
        }
    }
}

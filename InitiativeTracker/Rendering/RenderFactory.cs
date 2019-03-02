using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using UnityEngine;
using Shapes;

public class SimpleLines : ImmediateModeShapeDrawer
{
    public override void DrawShapes(Camera cam)
    {
        using (Draw.Command(cam))
        {
            Draw.ZTest = UnityEngine.Rendering.CompareFunction.Always;
            Draw.Line(Vector3.zero, Vector3.right, Color.red);
            Draw.Line(Vector3.zero, Vector3.up, Color.green);
            Draw.Line(Vector3.zero, Vector3.forward, Color.blue);
        }
    }
}
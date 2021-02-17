using UnityEngine;

public static class MathTools
{

    public static Vector3 V2ToV3(Vector2 input)
    {
        return new Vector3(input.x, input.y,0);
    }

    public static Vector2 V3ToV2(Vector3 input)
    {
        return new Vector2(input.x, input.y);
    }


}

using UnityEngine;

public class EntityUtil
{
    public static Bounds findBounds( GameObject go )
    {
        Bounds empty = new Bounds();

        if( !go )
            return empty;

        Renderer renderer = go.GetComponent<Renderer>();
        if( !renderer )
            return empty;

        return renderer.bounds;
    }

    public static Bounds findSpriteBounds( GameObject go )
    {
        Bounds empty = new Bounds();

        if( !go )
            return empty;

        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        if( !renderer )
            return empty;

        return renderer.sprite.bounds;
    }
}
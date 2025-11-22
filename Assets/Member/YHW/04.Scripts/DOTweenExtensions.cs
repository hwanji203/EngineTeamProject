using DG.Tweening;

public static class DOTweenExtensions
{
    public static Tweener UI(this Tweener tweener)
    {
        return tweener.SetUpdate(true);
    }
}

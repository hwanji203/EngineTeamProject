using UnityEngine.U2D.Animation;

public class Fish : Enemy
{
    private SpriteLibrary _spriteLibrary;

    protected override void Awake()
    {
        base.Awake();
        _spriteLibrary = GetComponent<SpriteLibrary>();
    }

    public void VisualSetting(SpriteLibraryAsset asset)
    {
        _spriteLibrary.spriteLibraryAsset = asset;
    }
}

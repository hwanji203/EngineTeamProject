using Member.Kimyongmin._02.Code.Enemy.SO;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    [RequireComponent(typeof(FishBrain))]
    public class Fish : global::Enemy
    {
        private SpriteLibrary _spriteLibrary;
        private FishDataSo _fishData;

        protected override void Awake()
        {
            base.Awake();
            _spriteLibrary = GetComponentInChildren<SpriteLibrary>();
        
            _fishData = EnemyDataSo as FishDataSo;
        }

        protected override void Start()
        {
            base.Start();
            VisualSetting(_fishData.fishSpriteLibrary);
        }

        public override void Attack()
        {
        
        }


        public void VisualSetting(SpriteLibraryAsset asset)
        {
            _spriteLibrary.spriteLibraryAsset = asset;
        }
    }
}

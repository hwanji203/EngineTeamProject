    using UnityEngine;

    /// 느린 상태 이상을 받을 수 있는 객체
    public interface ISlowable
    {
        void ApplySlow(float slowPercent, float duration);
    }
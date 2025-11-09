using System.Collections;

namespace Member.Kimyongmin._02.Code.Interface
{
    public interface IHitface
    {
        public float panjeongTime { get; set; }
        public float panjeongDuration { get; set; }

        public IEnumerator HitPanJeong();
    }
}

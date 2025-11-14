using System.Collections;

namespace Member.Kimyongmin._02.Code.Interface
{
    public interface IHitface
    {
        public float PanjeongTime { get; set; }
        public float PanjeongDuration { get; set; }

        public IEnumerator HitPanJeong();

        public void DealStamina(Player player, float damage);
    }
}

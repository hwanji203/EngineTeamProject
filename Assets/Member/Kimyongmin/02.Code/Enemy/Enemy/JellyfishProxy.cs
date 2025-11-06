using Member.Kimyongmin._02.Code.Enemy.Effect;
using UnityEngine;

namespace Member.Kimyongmin._02.Code.Enemy.Enemy
{
    public class JellyfishProxy : MonoBehaviour
    {
        [SerializeField] private GameObject laserPrefabs;
        [SerializeField] private float laserRange = 15f;
        
        public void DeathEvent()
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject laser = Instantiate(laserPrefabs, transform.position,  Quaternion.Euler(new Vector3(0,0,90 * i)));
                laser.GetComponent<JellyLaser>().Shoot(laserRange);
            }
        }

        public void DestroyAng()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}

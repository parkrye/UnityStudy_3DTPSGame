using UnityEngine;
using UnityEngine.Events;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] int hp, maxHP;
    [SerializeField] int bullets;

    [SerializeField] UnityEvent<int> hpEvent, bulletEvent;

    public int HP { get { return hp; } set { hp = value; if (hp > maxHP) hp = maxHP; hpEvent?.Invoke(hp); } }
    public int Bullets { get {  return bullets; } set { bullets = value; bulletEvent?.Invoke(bullets); } }

    void Start()
    {
        hpEvent?.Invoke(hp);
        bulletEvent?.Invoke(bullets);
    }

    public void Shot()
    {
        bullets--;
        bulletEvent?.Invoke(bullets);
    }
}

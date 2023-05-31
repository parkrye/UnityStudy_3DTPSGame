using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataViewer : MonoBehaviour
{
    [SerializeField] Slider hp;
    [SerializeField] TextMeshProUGUI bullets;

    public void HPChange(int _hp)
    {
        hp.value = _hp;
    }

    public void BulletsChange(int _bullets)
    {
        bullets.text = _bullets.ToString();
    }
}

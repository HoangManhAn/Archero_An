using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    [SerializeField] Text hpText;

    float hp;
    float maxHp;

    private Transform target;

    private void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHp, Time.deltaTime * 5f );
        transform.position = target.position + offset;
    }

    public void OnInit(float maxHp, Transform target)
    {
        this.target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
        hpText.text = maxHp.ToString();
    }

    public void SetNewHP(float hp)
    {
        this.hp = hp;
        hpText.text = hp.ToString();
        //imageFill.fillAmount = hp / maxHp;
    }
}

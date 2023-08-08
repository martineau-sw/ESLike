using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ESLike.Actor;

namespace ESLike.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        Image _healthBar;
        [SerializeField]
        Image _breathBar;
        [SerializeField]
        Image _focusBar;

        [SerializeField]
        ActorMono _player;

        void Awake() 
        {
            SetMeterLength();
            SetEvents();
        }

        // Update is called once per frame
        void LateUpdate()
        {
        }

        void SetEvents()
        {
            _player.Meters.Health.OnChange += (s, e) => StartCoroutine(Bar(_healthBar, _player.Meters.Health.Normal));
            _player.Meters.Focus.OnChange += (s, e) => StartCoroutine(Bar(_focusBar, _player.Meters.Focus.Normal));
            _player.Meters.Breath.OnChange += (s, e) => StartCoroutine(Bar(_breathBar, _player.Meters.Breath.Normal));
        }

        void SetMeterLength()
        {

            RectTransform healthRect = _healthBar.rectTransform;
            RectTransform breathRect = _breathBar.rectTransform;
            RectTransform focusRect = _focusBar.rectTransform;

            Vector2 sizeDelta = new Vector2 { y = healthRect.sizeDelta.y };

            sizeDelta.x = _player.Meters.Health.Max;
            healthRect.sizeDelta = sizeDelta;
            healthRect.transform.parent.GetChild(1).GetComponent<RectTransform>().sizeDelta = sizeDelta;

            sizeDelta.x = _player.Meters.Breath.Max;
            breathRect.sizeDelta = sizeDelta;
            breathRect.transform.parent.GetChild(1).GetComponent<RectTransform>().sizeDelta = sizeDelta;

            sizeDelta.x = _player.Meters.Focus.Max;
            focusRect.sizeDelta = sizeDelta;
            focusRect.transform.parent.GetChild(1).GetComponent<RectTransform>().sizeDelta = sizeDelta;
        }

        public IEnumerator Bar(Image bar, float target) 
        {
            while(bar.fillAmount != target) 
            {
                bar.fillAmount  = Mathf.Lerp(bar.fillAmount, target, 2 * Time.deltaTime);
            }

            yield return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ESLike.Actor;
using ESLike.Actor.Extensions;

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
        Actor.Actor _player;

        // Update is called once per frame
        void LateUpdate()
        {
            UpdateMeterFill();
            SetMeterLength();
        }

        void SetMeterLength()
        {

            RectTransform healthRect = _healthBar.rectTransform;
            RectTransform breathRect = _breathBar.rectTransform;
            RectTransform focusRect = _focusBar.rectTransform;

            healthRect.sizeDelta = new Vector2(_player.Meters.Health.Max, healthRect.sizeDelta.y);
            healthRect.transform.parent.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(_player.Meters.Health.Max, healthRect.sizeDelta.y);

            breathRect.sizeDelta = new Vector2(_player.Meters.Breath.Max, healthRect.sizeDelta.y);
            breathRect.transform.parent.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(_player.Meters.Breath.Max, healthRect.sizeDelta.y);

            focusRect.sizeDelta = new Vector2(_player.Meters.Focus.Max, healthRect.sizeDelta.y);
            focusRect.transform.parent.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(_player.Meters.Focus.Max, healthRect.sizeDelta.y);
        }

        void UpdateMeterFill() 
        {
            _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, _player.Meters.HealthNormalized(), 2 * Time.deltaTime);
            _breathBar.fillAmount = Mathf.Lerp(_breathBar.fillAmount, _player.Meters.BreathNormalized(), 2 * Time.deltaTime);
            _focusBar.fillAmount = Mathf.Lerp(_focusBar.fillAmount, _player.Meters.FocusNormalized(), 2 * Time.deltaTime);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Text m_Text;
    [SerializeField] private Health m_Health;

    private void Awake()
    {
        Prime(m_Health);
    }

    private void OnEnable()
    {
        m_Health.onHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnDisable()
    {
        m_Health.onHealthChanged.RemoveListener(OnHealthChanged);
    }

    public void Prime(Health health)
    {
        if (health == null) return;

        m_Health = health;
        m_Health.onHealthChanged.AddListener(OnHealthChanged);
    }

    private void OnHealthChanged()
    {
        if (m_Slider)
        {
            m_Slider.value = (float)m_Health.Value / m_Health.Max;
        }

        if (m_Text)
        {
            m_Text.text = $"{m_Health.Value}/{m_Health.Max}";
        }
    }
}

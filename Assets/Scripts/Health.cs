using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int m_Max = 100;

    [Space(10)]
    public UnityEvent onHealthChanged;
    public UnityEvent onDie;


    private int m_Health;


    public int Value
    {
        get => m_Health;
        set => SetValue(value);
    }

    public int Max
    {
        get => m_Max;
        private set => m_Max = value;
    }

    public bool IsDead
    {
        get => m_Health <= 0;
    }

    private void Awake()
    {
        RestoreMax();
    }

    public void SetValue(int value)
    {
        int oldValue = m_Health;
        m_Health = Mathf.Clamp(value, 0, m_Max);

        if (oldValue != m_Health)
        {
            onHealthChanged.Invoke();
        }

        if (m_Health == 0) Die();
    }

    public void Subtract(int value)
    {
        Value -= value;
    }

    public void RestoreMax()
    {
        Value = m_Max;
    }

    private void Die()
    {
        onDie?.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

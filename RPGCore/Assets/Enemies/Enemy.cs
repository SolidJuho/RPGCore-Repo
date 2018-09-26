using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] float maxHealth;
    float currentHealth;

    private void Start()
    {
        SetDefaultValues();
    }
    
    void SetDefaultValues()
    {
        currentHealth = maxHealth;
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealth / maxHealth;
        }
    }
}

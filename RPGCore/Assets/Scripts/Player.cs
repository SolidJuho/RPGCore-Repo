using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100f;

    [SerializeField]float currentHealthPoints;


    private void Start()
    {
        SetDefaultValues();
    }

    private void SetDefaultValues()
    {
        currentHealthPoints = maxHealthPoints; //Sets health default value
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

}

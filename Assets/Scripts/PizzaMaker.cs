using UnityEngine;

public class PizzaMaker : MonoBehaviour
{
    public GameObject doughBall;
    public GameObject doughFlat;
    public GameObject doughWithSauce;
    public GameObject doughWithSauceCheese;

    private enum PizzaState
    {
        DoughBall,
        DoughFlat,
        DoughWithSauce,
        DoughWithSauceCheese
    }

    private PizzaState currentState = PizzaState.DoughBall;

    void Start()
    {
        UpdatePizzaVisual();
    }

    public void FlattenDough()
    {
        if (currentState == PizzaState.DoughBall)
        {
            currentState = PizzaState.DoughFlat;
            UpdatePizzaVisual();
        }
    }

    public void AddSauce()
    {
        if (currentState == PizzaState.DoughFlat)
        {
            currentState = PizzaState.DoughWithSauce;
            UpdatePizzaVisual();
        }
    }

    public void AddCheese()
    {
        if (currentState == PizzaState.DoughWithSauce)
        {
            currentState = PizzaState.DoughWithSauceCheese;
            UpdatePizzaVisual();
        }
    }

    private void UpdatePizzaVisual()
    {
        GameObject previousPizza = null;

        if (doughBall.activeSelf) previousPizza = doughBall;
        if (doughFlat.activeSelf) previousPizza = doughFlat;
        if (doughWithSauce.activeSelf) previousPizza = doughWithSauce;
        if (doughWithSauceCheese.activeSelf) previousPizza = doughWithSauceCheese;

        // Désactive tout
        doughBall.SetActive(false);
        doughFlat.SetActive(false);
        doughWithSauce.SetActive(false);
        doughWithSauceCheese.SetActive(false);

        GameObject activePizza = null;

        switch (currentState)
        {
            case PizzaState.DoughBall:
                activePizza = doughBall;
                break;
            case PizzaState.DoughFlat:
                activePizza = doughFlat;
                break;
            case PizzaState.DoughWithSauce:
                activePizza = doughWithSauce;
                break;
            case PizzaState.DoughWithSauceCheese:
                activePizza = doughWithSauceCheese;
                break;
        }

        if (activePizza != null)
        {
            if (previousPizza != null)
            {
                // Copier position
                activePizza.transform.position = previousPizza.transform.position;
                // Forcer la rotation pour être bien à plat
                activePizza.transform.rotation = Quaternion.Euler(-90, 0, 0);
            }
            activePizza.SetActive(true);
        }
    }


}

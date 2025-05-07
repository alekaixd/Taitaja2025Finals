using UnityEngine;
using System.Collections.Generic;
using TMPro;

// This script manages the order system in the game
// It handles the creation, tracking, and completion of orders
// needs to do:
// - Create new orders when last one is completed
// - Track the current order
// - complete order button


public class OrderManager : MonoBehaviour
{
    public GameObject orderPrefab; // Prefab for the order UI
    public GameObject orderParent; // Parent object for the orders
    public OrderObject currentOrder; // The current order object
    public GameObject currentOrderObject; // The current order UI object

    public GameManager gameManager;

    public List<OrderObject> orderList = new(); // List of all orders

    public int currentRound = 0;
    public int ingredientsUsed = 0; // Number of ingredients used in the current round

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI ingredientsUsedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateOrder(orderList[Random.Range(0, orderList.Count)]);
        NextRound();
    }

    public void CreateOrder(OrderObject order)
    {
        // Create a new order UI element
        currentOrderObject = Instantiate(orderPrefab, orderParent.transform);
        currentOrder = order;

        currentOrderObject.GetComponent<OrderNoteScript>().SetInfoText(currentOrder.orderName, currentOrder.orderDescription);
    }

    public void CompleteOrder()
    {
        // check if the requirements of the current order are met
        foreach (OrderRequirement req in currentOrder.orderRequirement)
        {
            if(req.isReversedRequirement == true)
            {
                switch (req.flavor)
                {
                    case CardFlavor.Spicy:
                        if (gameManager.spicy > req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Too spicy");
                            return;
                        }
                        break;
                    case CardFlavor.Sweet:
                        if (gameManager.sweet > req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Too sweet");
                            return;
                        }
                        break;
                    case CardFlavor.Sour:
                        if (gameManager.sour > req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Too sour");
                            return;
                        }
                        break;
                    case CardFlavor.Savoury:
                        if (gameManager.savoury > req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Too savoury");
                            return;
                        }
                        break;
                    case CardFlavor.Salty:
                        if (gameManager.salty > req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Too salty");
                            return;
                        }
                        break;
                 }
            }
            else if(req.isReversedRequirement == false)
            {
                switch (req.flavor)
                {
                    case CardFlavor.Spicy:
                        if (gameManager.spicy < req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Not spicy enough");
                            return;
                        }
                        break;
                    case CardFlavor.Sweet:
                        if (gameManager.sweet < req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Not sweet enough");
                            return;
                        }
                        break;
                    case CardFlavor.Sour:
                        if (gameManager.sour < req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Not sour enough");
                            return;
                        }
                        break;
                    case CardFlavor.Savoury:
                        if (gameManager.savoury < req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Not savoury enough");
                            return;
                        }
                        break;
                    case CardFlavor.Salty:
                        if (gameManager.salty < req.flavourValue)
                        {
                            // requirement not met
                            GameOver("Not salty enough");
                            return;
                        }
                        break;
                }
            }

        }
        Debug.Log("Order completed successfully!"); // Log success message
        // if all requirements are met, complete the order
        Destroy(currentOrderObject);
        CreateOrder(orderList[Random.Range(0, orderList.Count)]);
        NextRound();
    }

    public void GameOver(string gameoverMessage)
    {
        Debug.Log("Game Over: " + gameoverMessage);
        // Game over logic
        // Show game over screen
        // Reset game state
    }

    public void NextRound()
    {
        currentRound++;
        roundText.text = "Round: " + currentRound;
    }

    public void IncrementIngridients()
    {
        ingredientsUsed++;
        ingredientsUsedText.text = "Ingredients used: " + ingredientsUsed;
    }
}

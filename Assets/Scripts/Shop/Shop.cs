using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private int _currentSelectedItem;
    [SerializeField] private int _currentItemCost;
    private Player _player; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _player = other.GetComponent<Player>();

            if(_player != null )
            {
                UIManager.Instance.OpenShop(_player.GemsCount);
            }

            _shopMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _shopMenu.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch(item)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(82);
                _currentSelectedItem = 0;
                _currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-28);
                _currentSelectedItem = 1;
                _currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-138);
                _currentSelectedItem = 2;
                _currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if(_player.GemsCount >= _currentItemCost)
        {
            if(_currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            _player.SubstrucDiamond(_currentItemCost);
            Debug.Log("Purchase " +  _currentSelectedItem);
            Debug.Log("Quedan " +  _player.GemsCount);
            _shopMenu.SetActive(false);
        }
        else
        {
            Debug.Log("You don't have enough gems. Closing shop.");
            _shopMenu.SetActive(false);
        }
    }
}
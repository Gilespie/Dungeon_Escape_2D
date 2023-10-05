using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    [SerializeField] private TextMeshProUGUI _playerGemCountText;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Text _gemCountText;
    [SerializeField] private Image[] _healthBars;

    /*private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UIManager is NULL!");
            }

            return _instance;
        }
    }*/

    /*private void Awake()
    {
        _instance = this;
    }
*/
    public void OpenShop(int gemCount)
    {
        _playerGemCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        _selectionImage.rectTransform.anchoredPosition = new Vector2(-54, yPos);
    }

    public void UpdateGemCount(int count)
    {
        _gemCountText.text = count.ToString();
    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                _healthBars[i].enabled = false;
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void OnNicknameChange(string nickname)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UpdateNickname(nickname);
    }

    public void OnColorChange(int optionValue)
    {
        Dropdown ddColor = GameObject.Find("Dropdown_Color").GetComponent<Dropdown>();
        string colorName = ddColor.options[optionValue].text;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UpdateColor(colorName);
    }
}

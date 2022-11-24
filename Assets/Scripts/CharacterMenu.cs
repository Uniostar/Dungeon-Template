using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //Public Fields
    public Text levelText, hitpointText, eurosText, xpText, upgradeCostText;

    //Logic
    private int characterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //When the game begins
    private void Start()
    {
        loadMenu();
    }
    //Character Selection
    public void onArrowSelection(bool right)
    {
        if (right)
        {
            characterSelection++;

            if (characterSelection == GameManager.instance.playerSprites.Count)
            {
                characterSelection = 0;
            }

            OnSelectionChanged();
        }
        else
        {
            characterSelection--;

            if (characterSelection < 0)
            {
                characterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        GameManager.instance.currentPlayerSprite = characterSelection;
        GameManager.instance.player.GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[characterSelection];
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[characterSelection];
        GameManager.instance.SaveState();
    }

    //Update the info
    public void onClickUpgrade()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            updateMenu();
    }

    //Update the character info
    public void updateMenu()
    {
        //Weapon
        if (GameManager.instance.TryUpgradeWeapon())
        {
            upgradeCostText.GetComponent<Button>().interactable = true;
            weaponSprite.sprite = GameManager.instance.weapons[GameManager.instance.weapon.weaponLevel];
            upgradeCostText.text = "€" + GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
            GameManager.instance.weapon.upgradeWeapon();
        }
        else
        {
            upgradeCostText.GetComponent<Button>().interactable = false;
        }

        //Meta
        hitpointText.text = GameManager.instance.player.hitPoint.ToString() + "/" + GameManager.instance.player.maxHitPoint;
        eurosText.text = GameManager.instance.euros.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //XP Bar 
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXP = GameManager.instance.GetXpToLevel(currentLevel-1);
            int currentLevelXP = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXP - prevLevelXP;
            int currentXPIntoLevel = GameManager.instance.experience - prevLevelXP;

            float completionRation = (float)currentXPIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRation, 1, 1);
            xpText.text = currentXPIntoLevel.ToString() + " / " + diff.ToString();
        }
        GameManager.instance.SaveState();
    }
    public void loadMenu()
    {
        upgradeCostText.text = "€" + GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        weaponSprite.sprite = GameManager.instance.weapons[GameManager.instance.weapon.weaponLevel];
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[GameManager.instance.playerSprites.IndexOf(GameManager.instance.player.GetComponent<SpriteRenderer>().sprite)];
        GameManager.instance.currentPlayerSprite = GameManager.instance.playerSprites.IndexOf(GameManager.instance.player.GetComponent<SpriteRenderer>().sprite);
    }
}

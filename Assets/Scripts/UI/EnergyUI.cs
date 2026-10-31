
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    public GhostAbilities abilities;
    public Image energyFill;
    public TMP_Text energyText;

    void Update()
    {
        if (abilities == null)
            return;

        float energy = abilities.currentEnergy;
        float maxEnergy = abilities.maxEnergy;

        energyFill.fillAmount = energy / maxEnergy;

        energyText.text =
            Mathf.CeilToInt(energy) + " / " +
            Mathf.CeilToInt(maxEnergy);
    }
}


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCooldownUI : MonoBehaviour
{
    [Header("References")]
    public GhostAbilities abilities;

    [Header("Poltergeist")]
    public Image poltergeistFill;
    public TMP_Text poltergeistText;

    [Header("Flicker")]
    public Image flickerFill;
    public TMP_Text flickerText;

    [Header("Apparition")]
    public Image apparitionFill;
    public TMP_Text apparitionText;

    [Header("Colors")]
    public Color readyColor = new Color(0.65f, 0.3f, 1f);
    public Color cooldownColor = new Color(0.25f, 0.2f, 0.3f);

    void Update()
    {
        if (abilities == null)
            return;

        UpdateAbility(
            poltergeistFill,
            poltergeistText,
            abilities.PoltergeistCooldown,
            abilities.timerPoltergeist,
            "SPACE",
            "POLTERGEIST"
        );

        UpdateAbility(
            flickerFill,
            flickerText,
            abilities.FlickerCooldown,
            abilities.timerFlicker,
            "SHIFT",
            "FLICKER"
        );

        UpdateAbility(
            apparitionFill,
            apparitionText,
            abilities.ApparitionCooldown,
            abilities.timerApparition,
            "E",
            "APPARITION"
        );
    }

    void UpdateAbility(
        Image fill,
        TMP_Text text,
        float cooldown,
        float maxCooldown,
        string key,
        string abilityName)
    {
        if (fill == null)
            return;

        float progress = 1f;

        if (maxCooldown > 0f)
        {
            progress = 1f - (cooldown / maxCooldown);
        }

        fill.fillAmount = Mathf.Clamp01(progress);

        bool ready = cooldown <= 0f;

        fill.color = ready ? readyColor : cooldownColor;

        if (text != null)
        {
            if (ready)
            {
                text.text = "[" + key + "] " + abilityName;
            }
            else
            {
                text.text = "[" + key + "] " + abilityName
                    + "\n" + cooldown.ToString("0.0") + " s";
            }
        }
    }
}

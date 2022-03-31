using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class HealthBar : MonoBehaviour
{
    PlayerModel.Settings _playerModelSettings = null;
    SignalBus _signalBus = null;

    [SerializeField] Image hpBar = null;
    [SerializeField] Image hpBarFollow = null;
    [SerializeField] TMP_Text hpTxt = null;

    float ratio = 1f;

    [Inject]
    public void Construct(
        PlayerModel.Settings playerModelSettings,
        SignalBus signalBus)
    {
        _playerModelSettings = playerModelSettings;
        _signalBus = signalBus;
    }

    private void Start()
    {
        OnHPChanged(_playerModelSettings.MaxHP, _playerModelSettings.MaxHP);

        _signalBus.Subscribe<PlayerHPChangedSignal>(HandlePlayerHPChangedSignal);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<PlayerHPChangedSignal>(HandlePlayerHPChangedSignal);
    }

    private void LateUpdate()
    {
        hpBarFollow.fillAmount = Mathf.Lerp(hpBarFollow.fillAmount, hpBar.fillAmount, Time.deltaTime * 1.5f);
    }

    void HandlePlayerHPChangedSignal(PlayerHPChangedSignal args)
    {
        OnHPChanged(args.HP, args.MaxHP);
    }

    void OnHPChanged(float hp, float maxHP)
    {
        ratio = hp / maxHP;

        hpTxt.text = $"{hp} / {maxHP}";

        hpBar.fillAmount = ratio;
    }
}

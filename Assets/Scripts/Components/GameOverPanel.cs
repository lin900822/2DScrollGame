using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameOverPanel : MonoBehaviour
{
    SignalBus _signalBus = null;
    [SerializeField] CanvasGroup canvasGroup = null;
    [SerializeField] Animator animator = null;

    [Inject]
    public void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;

        _signalBus.Subscribe<PlayerDiedSignal>(HandlePlayerDied);
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<PlayerDiedSignal>(HandlePlayerDied);
    }

    void HandlePlayerDied()
    {
        Invoke(nameof(ShowPanel), .5f);
    }

    void ShowPanel()
    {
        animator.SetTrigger("Enter");
    }
}

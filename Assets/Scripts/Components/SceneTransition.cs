using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{ 
    [SerializeField] Animator animator = null;
    [SerializeField] string sceneName = "GamePlay";
    [SerializeField] float timeToTransite = 1.2f;

    public void Enter()
    {
        animator.SetTrigger("Enter");
    }

    public void Exit(string sceneName)
    {
        this.sceneName = sceneName;
        animator.SetTrigger("Exit");
        Invoke(nameof(LoadScene), timeToTransite);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

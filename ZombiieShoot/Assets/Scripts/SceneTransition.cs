using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private Animator transitionAnim;
    private void Start()
    {
        transitionAnim = GetComponent<Animator>();
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }
    public void Exit()
    {
        Application.Quit();
    }
    IEnumerator Transition (string sceneName)
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}

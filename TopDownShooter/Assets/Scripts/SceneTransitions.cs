using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnimator;

    private void Start()
    {
        transitionAnimator = GetComponent<Animator>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName)
    {
        transitionAnimator.SetTrigger("end");
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(sceneName);
    }
}

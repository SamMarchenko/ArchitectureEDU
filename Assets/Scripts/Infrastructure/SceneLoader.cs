using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
        _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded = null) => _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

    private IEnumerator LoadScene(string nextScene, Action OnLoaded = null)
    {
        if (SceneManager.GetActiveScene().name == nextScene)
        {
            OnLoaded?.Invoke();
            yield break;
        }
        var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

        while (!waitNextScene.isDone)
        {
            yield return null;
        }

        OnLoaded?.Invoke();
    }
}
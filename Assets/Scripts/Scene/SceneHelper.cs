using MJW.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MJW.Scenes
{
    public class SceneHelper : Singleton<SceneHelper>
    {
        public void LoadAndUnload(SceneType sceneToLoad, SceneType sceneToUnload, Action onLoadedCallback = null)
        {
            var loadAsyncOp = SceneManager.LoadSceneAsync((int)sceneToLoad, LoadSceneMode.Additive);
            StartCoroutine(OnLoadingSceneCoroutine(loadAsyncOp, sceneToLoad, sceneToUnload, onLoadedCallback));
        }

        public void UnloadAndLoad(SceneType sceneToUnload, SceneType sceneToLoad)
        {
            var loadAsyncOp = SceneManager.UnloadSceneAsync((int)sceneToUnload);
            StartCoroutine(OnUnloadingSceneCoroutine(loadAsyncOp, sceneToLoad));
        }

        public IEnumerator OnLoadingSceneCoroutine(AsyncOperation loadOp, SceneType loadingScene, SceneType sceneToUnload, Action onLoadedCallback)
        {
            while (!loadOp.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)loadingScene));

            if (sceneToUnload != SceneType.Undefined)
            {
                SceneManager.UnloadSceneAsync((int)sceneToUnload);
            }

            onLoadedCallback?.Invoke();
        }

        public IEnumerator OnUnloadingSceneCoroutine(AsyncOperation unload, SceneType loadingScene)
        {
            while (!unload.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }

            var loadAsyncOp = SceneManager.LoadSceneAsync((int)loadingScene, LoadSceneMode.Additive);

            while (!loadAsyncOp.isDone)
            {
                yield return new WaitForSeconds(0.1f);
            }

            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)loadingScene));
        }
    }
}
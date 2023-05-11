using System;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using AsyncOperation = UnityEngine.AsyncOperation;

namespace Wonderland.Management
{
    public abstract class SceneHandler : Handler
    {
        public static async void LoadSceneWithTransition(int buildIndex)
        {
            if (buildIndex == SceneManager.GetActiveScene().buildIndex)return;
            CustomLog.GameState.Log("Start Load Scene");
            
            var loadOperation = SceneManager.LoadSceneAsync(buildIndex);
            loadOperation.allowSceneActivation = false;
            
            do {
                await Task.Delay(100);
                CustomLog.GameState.Log("Loading Progress: " + loadOperation.progress);
            } while (loadOperation.progress < 0.9f);
            
            UIHandler.ShowLoadingScreen();
            
            await Task.Delay(1500);
            loadOperation.allowSceneActivation = true;
            
            await Task.Delay(1500);
            GameManager.ChangeGameState(GameManager.State.IdleState);
        }
    }
}
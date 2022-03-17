﻿using USceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;
namespace Dumb
{
    public static class SceneUtil
    {
        public static List<Scene> GetallScene(bool AddDontdestroy=true)
        {
            List<Scene> allscenes = new();
            for(int i=0;i<USceneManager.sceneCount;i++)
            {
                allscenes.Add(USceneManager.GetSceneAt(i));
            }
            if(AddDontdestroy)
            {
                GameObject go = new();
                UObject.DontDestroyOnLoad(go);
                allscenes.Add(go.scene);
                UObject.Destroy(go);
            }
            return allscenes;
        }
    }
}

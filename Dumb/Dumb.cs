using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modding;
using UnityEngine;
using GlobalEnums;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMaker;

namespace Dumb
{
    public class Dumb:Mod,ITogglableMod
    {
        public override string GetVersion()
        {
            return "1.0";
        }
        public override void Initialize()
        {
            On.tk2dBaseSprite.Awake += SetnoColor;
            On.tk2dBaseSprite.SetColors += SetnoCl;
            ModHooks.ObjectPoolSpawnHook += ChangeMarkothNail;
        }

        private GameObject ChangeMarkothNail(GameObject arg)
        {
           if(arg.name.StartsWith("Shot Markoth Nail"))
           {
                arg.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
           }
            return arg;
        }

        private void SetnoCl(On.tk2dBaseSprite.orig_SetColors orig, tk2dBaseSprite self, Color32[] dest)
        {
            orig(self, dest);
            if (CheckSet(self.gameObject))
             self.color = new Color(0, 0, 0, 0);
        }

        private void SetnoColor(On.tk2dBaseSprite.orig_Awake orig, tk2dBaseSprite self)
        {
            orig(self);
            if(CheckSet(self.gameObject))
            self.color = new Color(0, 0, 0, 0);
        }
       public void Unload()
        {
            On.tk2dBaseSprite.Awake -= SetnoColor;
            On.tk2dBaseSprite.SetColors -= SetnoCl;
            ModHooks.ObjectPoolSpawnHook -= ChangeMarkothNail;

        }
        private bool CheckSet(GameObject gameObject)
        {
            if (gameObject.layer == 11 || gameObject.layer == 22|| gameObject.layer == 17)
            {
                if(gameObject.layer==17)
                {
                    if (gameObject.name.EndsWith("Slash") || gameObject.name.StartsWith("Fireball"))
                        return false;
                }
                return true;
            }
            if (gameObject.name.StartsWith("Flameball Grimmballoon")||gameObject.name=="Legs")
                return true;
            return false;
        }
    }
}

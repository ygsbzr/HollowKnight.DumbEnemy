using Modding;
using UnityEngine;

namespace Dumb
{
    public class Dumb:Mod,IGlobalSettings<Setting>,IMenuMod
    {
        public override string GetVersion()
        {
            return "1.3";
        }
        public Setting GS = new();
        public override void Initialize()
        {
            On.tk2dBaseSprite.Awake += SetnoColor;
            On.tk2dBaseSprite.SetColors += SetnoCl;
            ModHooks.ObjectPoolSpawnHook += ChangeShot;
        }
        public void OnLoadGlobal(Setting s) => GS = s;
        public bool ToggleButtonInsideMenu => true;
        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? menu)
        {
            List<IMenuMod.MenuEntry> menus = new();
            menus.Add(new()
            {
                Name="Enemy Invisible?",
                Description="",
                Values=new string[] {"True","False"},
                Loader=()=>GS.EnemyInvisible?0:1,
                Saver=opt=>GS.EnemyInvisible=(opt==0)
            });
            menus.Add(new()
            {
                Name = "Enemy Particle Invisible?",
                Description = "",
                Values = new string[] { "True", "False" },
                Loader = () => GS.ParticleInvisible ? 0 : 1,
                Saver = opt => GS.ParticleInvisible = (opt == 0)
            });
            return menus;
        }
        public Setting OnSaveGlobal() => GS;
        private GameObject ChangeShot(GameObject arg)
        {
           if(arg.name.StartsWith("Shot Markoth Nail")&&GS.EnemyInvisible)
           {
                arg.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                if(GS.ParticleInvisible)
                {
                   foreach(var par in arg.GetComponentsInChildren<ParticleSystemRenderer>())
                    {
                        par.enabled = false;
                    }
                }
           }
            if (arg.name.StartsWith("No Eyes Head") && GS.EnemyInvisible)
            {
                GameObject white = arg.FindGameObjectInChildren("white_light");
                if(white != null)
                {
                    white.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                }
                if (GS.ParticleInvisible)
                {
                    foreach (var par in arg.GetComponentsInChildren<ParticleSystemRenderer>())
                    {
                        par.enabled = false;
                    }
                }
            }
            return arg;
        }

        private void SetnoCl(On.tk2dBaseSprite.orig_SetColors orig, tk2dBaseSprite self, Color32[] dest)
        {
            orig(self, dest);
            if (CheckSet(self.gameObject)&&GS.EnemyInvisible)
            {
                self.color = new Color(0, 0, 0, 0);
                if(GS.ParticleInvisible)
                {
                    foreach (var par in self.gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
                    {
                        par.enabled = false;
                    }
                }
            }
        }

        private void SetnoColor(On.tk2dBaseSprite.orig_Awake orig, tk2dBaseSprite self)
        {
            orig(self);
            if (CheckSet(self.gameObject) && GS.EnemyInvisible)
            {
                if (GS.ParticleInvisible)
                {
                    foreach (var par in self.gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
                    {
                        par.enabled = false;
                    }
                }
            }
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
                if (gameObject.name.Contains("Shot Mantis Lord"))
                    return false;
                return true;
            }
            if (gameObject.name.StartsWith("Flameball Grimmballoon")||gameObject.name=="Legs"||gameObject.name.StartsWith("Black Knight")||gameObject.name.StartsWith("IK Projectile DS")||gameObject.name=="Rush")
                return true;
            return false;
        }
    }
}

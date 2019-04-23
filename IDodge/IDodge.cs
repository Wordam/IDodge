
using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using EntityStates.Commando;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using UnityEngine;

namespace IDodge
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("dev.wordam.ror2.idodge", "iDodge", "1.0.0")]
    public class IDodge : BaseUnityPlugin
    {
        private static double iFrameDurationPercent = 0.33f;
        private static double iFrameDuration = iFrameDurationPercent * EntityStates.Commando.DodgeState.duration;
        private void Awake()
        {
            // Add an iframe to the commando for the given duration
            IL.EntityStates.Commando.DodgeState.OnEnter += (il1) =>
            {
                var c2 = new ILCursor(il1);
                c2.Emit(OpCodes.Ldarg_0);

                c2.EmitDelegate<Action<EntityStates.Commando.DodgeState>>((d) =>
                {
                    if (d.outer)
                    {
                        if (d.outer.commonComponents.characterBody)
                        {
                            d.outer.commonComponents.characterBody.AddTimedBuff(BuffIndex.Invincibility, (float)iFrameDuration);
                        }
                    }
                });
            };
        }
    }
}
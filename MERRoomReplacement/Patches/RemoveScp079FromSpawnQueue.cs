using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Exiled.API.Features.Pools;
using HarmonyLib;
using PlayerRoles;
using PlayerRoles.RoleAssign;
using static HarmonyLib.AccessTools;

namespace MERRoomReplacement.Patches;

public static class RemoveScp079FromSpawnQueue
{
    public static void PatchSpawnQueue(Harmony harmony)
    {
        var originalMethod = PropertyGetter(typeof(ScpSpawner), nameof(ScpSpawner.SpawnableScps));
        var transpiler = new HarmonyMethod(typeof(RemoveScp079FromSpawnQueue), nameof(Transpiler));

        harmony.Patch(originalMethod, transpiler: transpiler);
    }
    
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        var newInstructions = ListPool<CodeInstruction>.Pool.Get(instructions);

        var index = newInstructions.FindLastIndex(instruction => instruction.opcode ==
            OpCodes.Isinst) + 2;

        var continueIndex = newInstructions.FindIndex(instruction =>
            instruction.Calls(Method(typeof(Dictionary<RoleTypeId, PlayerRoleBase>.Enumerator), 
                nameof(Dictionary<RoleTypeId, PlayerRoleBase>.Enumerator.MoveNext))))
            - 1;
        
        var continueLabel = newInstructions[continueIndex].labels.First();
        
        newInstructions.InsertRange(index, new[]
        {
            new CodeInstruction(OpCodes.Ldloca_S, 2),
            new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(KeyValuePair<RoleTypeId, PlayerRoleBase>), 
                nameof(KeyValuePair<RoleTypeId, PlayerRoleBase>.Key))),
            new CodeInstruction(OpCodes.Ldc_I4, (int)RoleTypeId.Scp079),
            new CodeInstruction(OpCodes.Brtrue, continueLabel)
        });
        
        for (var z = 0; z < newInstructions.Count; z++)
            yield return newInstructions[z];

        ListPool<CodeInstruction>.Pool.Return(newInstructions);
    }
}
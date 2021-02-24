using UnityEngine;
using Zenject;

namespace Asteroids
{
public class UFOIdleState : INPCState
{
    private UFO ufo;
    public UFO SetNPC 
    {
        set => ufo = value;
    }
    public INPCState DoState( UFO npc)
    {   
        return npc.idleState;
    }
}
}
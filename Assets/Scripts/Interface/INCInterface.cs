
namespace Asteroids
{

public interface INPCState
{
    UFO SetNPC{ set; }
    INPCState DoState( UFO npc);
}

}
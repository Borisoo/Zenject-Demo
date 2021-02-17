
namespace Asteroids
{

public interface INPCInterface
{

}

public interface INPCState
{
    UFO SetNPC{ set; }
    INPCState DoState( UFO npc);
}

}
using UnityEngine;

namespace Asteroids
{
    public class UFORoamState : INPCState
    {
        private UFO ufo;

        float _theta = 0;
        Vector3 _starPos;

        public UFO SetNPC
        {
            set => ufo = value;
        }

        public INPCState DoState(UFO npc)
        {
            if (npc.PlayerShip.IsDead)
            {
                return npc.roamState;
            }

            var lookDir = npc.PlayerShip.ShipPosition - npc.transform.position;
            npc.transform.position += lookDir * npc.UfoSettings.speed * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.forward);

            if (Vector3.Distance(npc.transform.position, npc.PlayerShip.ShipPosition) < npc.UfoSettings.attackRange)
            {
                return npc.attackState;
            }
            return npc.roamState;
        }
    }
}
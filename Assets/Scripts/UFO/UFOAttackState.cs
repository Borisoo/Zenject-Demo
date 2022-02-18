using UnityEngine;
using Zenject;

namespace Asteroids
{
    public class UFOAttackState : INPCState
    {
        private UFO m_ufo;
        private float m_timer;
        private float m_strafeTimer;
        private bool m_strafeRight;

        public UFO SetNPC
        {
            set => m_ufo = value;
        }

        public INPCState DoState(UFO npc)
        {
            if (npc.PlayerShip.IsDead)
            {
                return npc.idleState;
            }

            var lookDir = (npc.PlayerShip.ShipPosition - npc.transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookDir, Vector3.forward);
            npc.transform.rotation = targetRotation;

            Strafe();

            m_timer += Time.deltaTime;
            m_strafeTimer += Time.deltaTime;

            if (m_timer > npc.UfoSettings.attackInterval)
            {
                m_timer = 0;
                Fire(lookDir);
            }

            if (m_strafeTimer > m_ufo.UfoSettings.strafeInterval)
            {
                m_strafeTimer = 0;
                m_strafeRight = !m_strafeRight;

                if (Vector3.Distance(npc.transform.position, npc.PlayerShip.ShipPosition) > npc.UfoSettings.attackRange)
                {
                    return npc.roamState;
                }
            }
            return npc.attackState;
        }

        private void Strafe()
        {
            if (m_strafeRight)
            {
                m_ufo.transform.position += m_ufo.RightDir() * Time.deltaTime * m_ufo.UfoSettings.strafeSpeed;
            }
            else
            {
                m_ufo.transform.position += -m_ufo.RightDir() * Time.deltaTime * m_ufo.UfoSettings.strafeSpeed;
            }
        }

        private void Fire(Vector3 dir)
        {
            Bullet projectile = m_ufo._bulletFactory.Create(BulletType.FromEnemy);

            if (projectile != null)
            {
                projectile.transform.position = m_ufo.transform.position;
                projectile.transform.rotation = m_ufo.transform.rotation;
                projectile.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                projectile.GetComponent<Rigidbody>().AddForce(m_ufo.UfoSettings.attackSpeed * dir);
            }
        }
    }
}
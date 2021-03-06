﻿using UnityEngine;
using System.Collections;
using RTSPrototype;

namespace S3
{
    public class Player_Master : MonoBehaviour
    {
        public delegate void GeneralEventHandler();
        public event GeneralEventHandler EventInventoryChanged;
        public event GeneralEventHandler EventHandsEmpty;
        public event GeneralEventHandler EventAmmoChanged;

        public delegate void AmmoPickupEventHandler(string ammoName, int quantity);
        public event AmmoPickupEventHandler EventPickedUpAmmo;

        public delegate void PlayerHealthEventHandler(int healthChange);
        public event PlayerHealthEventHandler EventPlayerHealthDeduction;
        public event PlayerHealthEventHandler EventPlayerHealthIncrease;

        public delegate void AllyDeathEventHandler(AllyMember pendingDeath, AllyMember instigator);
        public event AllyDeathEventHandler EventAllyMemberDeath;

        public void CallEventInventoryChanged()
        {
            if(EventInventoryChanged != null)
            {
                EventInventoryChanged();
            }
        }

        public void CallEventHandsEmpty()
        {
            if (EventHandsEmpty != null)
            {
                EventHandsEmpty();
            }
        }

        public void CallEventAmmoChanged()
        {
            if (EventAmmoChanged != null)
            {
                EventAmmoChanged();
            }
        }

        public void CallEventPickedUpAmmo(string ammoName, int quantity)
        {
            if (EventPickedUpAmmo != null)
            {
                EventPickedUpAmmo(ammoName,quantity);
            }
        }

        //Call must have a instigator for damage to be received
        public void CallEventPlayerHealthDeduction(int dmg)
        {
            if (EventPlayerHealthDeduction != null)
            {
                EventPlayerHealthDeduction(dmg);
            }
        }

        public void CallEventPlayerHealthIncrease(int dmg)
        {
            if (EventPlayerHealthIncrease != null)
            {
                EventPlayerHealthIncrease(dmg);
            }
        }

        public void CallEventAllyMemberDeath(AllyMember pendingDeath, AllyMember instigator)
        {
            if(EventAllyMemberDeath != null)
            {
                EventAllyMemberDeath(pendingDeath, instigator);
            }
        }

    }
}
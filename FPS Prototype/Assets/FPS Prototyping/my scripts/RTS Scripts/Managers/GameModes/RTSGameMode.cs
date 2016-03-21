using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using S3;

namespace RTSPrototype
{
    public class RTSGameMode : GameManager_Master
    {
        [HideInInspector]
        public enum ECommanders
        {
            Commander_01,
            Commander_02,
            Commander_03,
            Commander_04,
            Commander_05,
            Commander_06,
        };
        [HideInInspector]
        public enum EFactions
        {
            Faction_Allies,
            Faction_Enemies,
            Faction_Default,
        };
        [HideInInspector]
        public enum ERTSGameState
        {
            EWaitingToStart,
            EPlaying,
            EGameIsPaused,
            EGameOver,
            EWon,
            EUnknown
        };
        [HideInInspector]
        public enum ERTSRewardTypes
        {
            Reward_Kill,
            
        };
        [HideInInspector]
        public enum ERTSPunishmentTypes
        {
            Punishment_KilledAnAlly,
            
        };
        //[SerializeField]
        public List<ECommanders> AllyFaction = new List<ECommanders>();
        public List<ECommanders> EnemyFaction = new List<ECommanders>();
        //TArray<TSubclassOf<class ARTSSpectator>> Spectators;
        public AllyMember FAllySpawnGObject;
        public AllyMember FEnemySpawnGObject;
        [HideInInspector]
        public List<PartyManager> GeneralMembers = new List<PartyManager>();
        [HideInInspector]
        public PartyManager GeneralInCommand;
        [HideInInspector]
        public int TargetKillCount = 0;
        [HideInInspector]
        public int CurrentEnemyCount = 0;
        [HideInInspector]
        public int TargetGoal = 2;
        [HideInInspector]
        public int RoundScaleMultiplier = 2;
        [HideInInspector]
        public int AmmoInLevel = 0;
        [HideInInspector]
        public bool LostGame = false;
        //TArray<class AShooterPickup*> LevelPickups;
        //GameTimer
        public int RemainingMinutes;
        public int RemainingSeconds;
        public float DefaultMatchTimeLimit;

        private int Ally_Kills;
        private int Enemy_Kills;
        private int Ally_Points;
        private int Enemy_Points;
        private int Ally_Deaths;
        private int Enemy_Deaths;
        //timers
        private float MatchRemainingTime;
        private float DefaultMatchStartingTime;

        //Point Rewards
        private int DefaultKillPoints;
        //Point Consequences
        private int DefaultFriendlyFirePoints;
        //Important GameState Instance
        private ERTSGameState MatchState;

        protected virtual void OnEnable()
        {
            ResetGameModeStats();
            InitializeGameModeValues();
        }

        protected virtual void OnDisable()
        {
            
        }

        // Use this for initialization
        protected virtual void Start()
        {
            if (AllyFaction.Count <= 0)
            {
                AllyFaction.Add(ECommanders.Commander_01);
            }
            if (EnemyFaction.Count <= 0)
            {
                EnemyFaction.Add(ECommanders.Commander_02);
            }

            PartyManager firstGeneralFound = FindGenerals(false, null);
            if(firstGeneralFound == null)
            {
                Debug.LogWarning("No General could be found!");
            }
            else
            {
                foreach(var General in GeneralMembers)
                {
                    if(General.GeneralCommander == ECommanders.Commander_01)
                    {
                        SetGeneralInCommand(General);
                    }
                }
            }

            if(GeneralInCommand == null)
            {
                Debug.LogWarning("There is no Commander 01 in the scene!");
            }

        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if(MatchState == ERTSGameState.EWaitingToStart)
            {
                waitingTillBeginMatch();
            }else if(MatchState == ERTSGameState.EPlaying)
            {
                playTheMatch();
            }

        }
        //GameModeSetupFunctions
        public PartyManager FindGenerals(bool pendingLeave, PartyManager generalLeaving){
            GeneralMembers.Clear();
            PartyManager[] PMembers = GameObject.FindObjectsOfType<PartyManager>();
            foreach(var PMember in PMembers)
            {
                if (pendingLeave)
                {
                    if(PMember != generalLeaving)
                    {
                        GeneralMembers.Add(PMember);
                    }
                }
                else
                {
                    GeneralMembers.Add(PMember);
                }

                UpdateGeneralStatuses();

                if(GeneralMembers.Count <= 0)
                {
                    Debug.LogWarning("No partyMembers in Scene!");
                    return null;
                }
                else
                {
                    PartyManager FirstGeneralFound = GeneralMembers[0];
                    return FirstGeneralFound;
                }
            }
            return null;
        }
        public void SetGeneralInCommand(PartyManager setToCommand)
        {
            if (GeneralMembers.Contains(setToCommand))
            {
                GeneralInCommand = setToCommand;
            }
            UpdateGeneralStatuses();
        }
        public void UpdateGeneralStatuses()
        {
            if(GeneralMembers.Count > 0)
            {
                foreach(var General in GeneralMembers)
                {
                    if (AllyFaction.Contains(General.GeneralCommander))
                    {
                        General.GeneralFaction = EFactions.Faction_Allies;
                    }else if (EnemyFaction.Contains(General.GeneralCommander))
                    {
                        General.GeneralFaction = EFactions.Faction_Enemies;
                    }
                }
            }
        }

        public int GetAllyFactionPlayerCount(AllyMember teamMember)
        {
            int playerCount = 0;
            EFactions Faction = GetAllyFaction(teamMember);
            AllyMember[] Allies = GameObject.FindObjectsOfType<AllyMember>();
            foreach(var Ally in Allies)
            {
                if(Ally.AllyFaction == Faction)
                {
                    playerCount++;
                }
            }
            return playerCount;
        }

        //Adding new versions of playercount and partymanager functions for flexibility
        public int GetFactionPlayerCount(EFactions Faction)
        {
            int playerCount = 0;
            AllyMember[] Allies = GameObject.FindObjectsOfType<AllyMember>();
            foreach (var Ally in Allies)
            {
                if (Ally.AllyFaction == Faction)
                {
                    playerCount++;
                }
            }
            return playerCount;
        }

        public int GetGeneralPlayerCount(ECommanders General)
        {
            int playerCount = 0;
            AllyMember[] Allies = GameObject.FindObjectsOfType<AllyMember>();
            foreach (var Ally in Allies)
            {
                if (Ally.GeneralCommander == General)
                {
                    playerCount++;
                }
            }
            return playerCount;
        }

        public PartyManager GetPartyManagerFromECommander(ECommanders General)
        {
            foreach(var Gen in GeneralMembers)
            {
                if(Gen.GeneralCommander == General)
                {
                    return Gen;
                }
            }
            return null;
        }

        public int GetAllyGeneralPlayerCount(AllyMember teamMember)
        {
            int playerCount = 0;
            ECommanders General = teamMember.GeneralCommander;
            AllyMember[] allies = GameObject.FindObjectsOfType<AllyMember>();
            foreach(var ally in allies)
            {
                if(ally.GeneralCommander == General)
                {
                    playerCount++;
                }
            }
            return playerCount;
        }

        public PartyManager GetPartyManager(AllyMember teamMember)
        {
            foreach(var Gen in GeneralMembers)
            {
                if(Gen.GeneralCommander == teamMember.GeneralCommander)
                {
                    return Gen;
                }
            }
            return null;
        }

        public List<PartyManager> GetPartyManagers(EFactions Faction)
        {
            List<PartyManager> partymanagers = new List<PartyManager>();
            foreach (var Gen in GeneralMembers)
            {
                if (Gen.GeneralFaction == Faction)
                {
                    partymanagers.Add(Gen);
                }
            }
            return partymanagers;
        }

        public EFactions GetAllyFaction(AllyMember teamMember)
        {
            if (AllyFaction.Contains(teamMember.GeneralCommander))
            {
                return EFactions.Faction_Allies;
            }else if (EnemyFaction.Contains(teamMember.GeneralCommander))
            {
                return EFactions.Faction_Enemies;
            }
            return EFactions.Faction_Default;
        }

        public virtual void CallGameOverEvent(ECommanders callingCommander)
        {
            
        }

        //Kills and Points Getters
        public int GetFactionKills(bool CalculateAccurateResults, EFactions Faction)
        {
            int FKills = (Faction == EFactions.Faction_Allies) ? Ally_Kills : Enemy_Kills;
            if(Faction != EFactions.Faction_Allies && Faction != EFactions.Faction_Enemies)
            {
                return 0;
            }
            if (CalculateAccurateResults)
            {
                foreach(var Gen in GeneralMembers)
                {
                    if(Gen.GeneralFaction == Faction)
                    {
                        FKills += Gen.GetPartyKillCount();
                    }
                }
                if (Faction == EFactions.Faction_Allies)
                    Ally_Kills = FKills;
                else if (Faction == EFactions.Faction_Enemies)
                    Enemy_Kills = FKills;
            }
            return FKills;
        }

        public int GetFactionPoints(bool CalculateAccurateResults, EFactions Faction)
        {
            int FPoints = (Faction == EFactions.Faction_Allies) ? Ally_Points : Enemy_Points;
            if (Faction != EFactions.Faction_Allies && Faction != EFactions.Faction_Enemies)
            {
                return 0;
            }
            if (CalculateAccurateResults)
            {
                foreach (var Gen in GeneralMembers)
                {
                    if (Gen.GeneralFaction == Faction)
                    {
                        FPoints += Gen.GetPartyPointsScored();
                    }
                }
                if (Faction == EFactions.Faction_Allies)
                    Ally_Points = FPoints;
                else if (Faction == EFactions.Faction_Enemies)
                    Enemy_Points = FPoints;
            }
            return FPoints;
        }

        public int GetFactionDeaths(bool CalculateAccurateResults, EFactions Faction)
        {
            int FactionDeaths = (Faction == EFactions.Faction_Allies) ? Ally_Deaths : Enemy_Deaths;
            if (Faction != EFactions.Faction_Allies && Faction != EFactions.Faction_Enemies)
                return 0;

            if (CalculateAccurateResults)
            {
                foreach (var Gen in GeneralMembers)
                {
                    if (Gen.GeneralFaction == Faction)
                    {
                        FactionDeaths += Gen.GetPartyDeathCount();
                    }
                }
                if (Faction == EFactions.Faction_Allies)
                    Ally_Deaths = FactionDeaths;
                else if (Faction == EFactions.Faction_Enemies)
                    Enemy_Deaths = FactionDeaths;
            }
            return FactionDeaths;
        }

        //Reset or Update Stats
        public void UpdateGameModeStats()
        {
            EFactions allyFac = EFactions.Faction_Allies;
            EFactions enemyFac = EFactions.Faction_Enemies;
            GetFactionKills(true, allyFac);
            GetFactionKills(true, enemyFac);
            GetFactionPoints(true, allyFac);
            GetFactionPoints(true, enemyFac);
            GetFactionDeaths(true, allyFac);
            GetFactionDeaths(true, enemyFac);
        }

        public void ResetGameModeStats()
        {
            Ally_Kills = 0;
            Enemy_Kills = 0;
            Ally_Points = 0;
            Enemy_Points = 0;
            Ally_Deaths = 0;
            Enemy_Deaths = 0;
        }

        public void InitializeGameModeValues()
        {
            DefaultKillPoints = 3;
            DefaultFriendlyFirePoints = -1;
            DefaultMatchTimeLimit = 60.0f * 5.0f;
            DefaultMatchStartingTime = Time.time + DefaultMatchTimeLimit;
            ERTSGameState waitingstate = ERTSGameState.EWaitingToStart;
            SetMatchState(waitingstate);
            
        }

        public int GetPendingReward(AllyMember receiver, ERTSRewardTypes rewardType) {
	        return DefaultKillPoints;
        }

        public int GetPendingPunishment(AllyMember receiver, ERTSPunishmentTypes punishType) {
            return DefaultFriendlyFirePoints;
        }

        //GameState Getter and Setter
        public ERTSGameState GetMatchState()
        {
            return MatchState;
        }

        public void SetMatchState(ERTSGameState setmatchstate)
        {
            MatchState = setmatchstate;
        }

        //virtual play state methods handling the match state inside of tick function
        public virtual void playTheMatch()
        {
            //if (MatchRemainingTime > 0) {
            //	MatchRemainingTime--;
            //}
            MatchRemainingTime = DefaultMatchStartingTime - Time.time;
            RemainingMinutes = (int)(MatchRemainingTime / 60.0f);
            RemainingSeconds = ((int)MatchRemainingTime) % 60;
            //if (GetWorld()->GetTimeSeconds() - DefaultMatchStartingTime > DefaultMatchTimeLimit)
            if (MatchRemainingTime <= 0)
            {
                ERTSGameState gameover = ERTSGameState.EGameOver;
                SetMatchState(gameover);
            }
        }

        public virtual void waitingTillBeginMatch()
        {
            if (GetMatchState() == (ERTSGameState.EWaitingToStart)) {
                ERTSGameState playgame = ERTSGameState.EPlaying;
                SetMatchState(playgame);
            }

        }

        /*
    //Specator Events
    TSubclassOf<class ARTSSpectator> ARTSProtoGameMode::getSpectatorSubClass(int32 index) {
    TSubclassOf<ARTSSpectator> retSpec = nullptr;
    //ARTSSpectator* retSpec = nullptr;
    int32 SpecIndex = Spectators.Find(Spectators[index]);
    if (Spectators.IsValidIndex(SpecIndex)) {
        retSpec = Spectators[index];
    }
    return retSpec;
    }

    class ARTSSpectator* ARTSProtoGameMode::getSpectatorClass(int32 index) {
    ARTSSpectator* retSpecClass = nullptr;
    TSubclassOf<ARTSSpectator> specSub = getSpectatorSubClass(index);
    if (specSub != nullptr) {
        retSpecClass = Cast<ARTSSpectator>(specSub);
    }

    return retSpecClass;
    }

    void ARTSProtoGameMode::setSpecFaction(int32 specIndex, TEnumAsByte<EFactions> faction) {
    ARTSSpectator* spectator = getSpectatorClass(specIndex);
    if (spectator != nullptr) {
        spectator->setSpectatorFaction(faction);
    }
    }

    void ARTSProtoGameMode::setSpecGeneral(int32 specIndex, TEnumAsByte<ECommanders> general) {
    ARTSSpectator* spectator = getSpectatorClass(specIndex);
    if (spectator != nullptr) {
        spectator->setSpectatorGeneral(general);
    }
    }

    void ARTSProtoGameMode::setSpecAlly(int32 specIndex, class AAllyMember* ally) {
    ARTSSpectator* spectator = getSpectatorClass(specIndex);
    if (spectator != nullptr) {
        spectator->setSpectatorAlly(ally);
    }
    }

    void ARTSProtoGameMode::UpdateSpecStats() {
    for (int32 i = 0; i < Spectators.Num(); i++) {
        if (getSpectatorClass(i) != nullptr && generalInCommand != nullptr) {
            setSpecFaction(i,getAllyFaction(generalInCommand->allyInCommand));
            setSpecGeneral(i, generalInCommand->generalCommander);
            setSpecAlly(i, generalInCommand->allyInCommand);
        }
    }
    }*/

    }
}
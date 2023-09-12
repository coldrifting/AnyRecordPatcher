using Mutagen.Bethesda.Skyrim;
// ReSharper disable StringLiteralTypo

namespace AnyRecordData.DataTypes.Conditions;

public static partial class DataConditionParser
{
    public static ConditionData ImportConditionData(string type, string? param1, string? param2)
    {
        return type switch
        {
	        // No Parameters
	        "CanFlyHere" => new CanFlyHereConditionData(),
			"DoesNotExist" => new DoesNotExistConditionData(),
			"EffectWasDualCast" => new EffectWasDualCastConditionData(),
			"EPAlchemyGetMakingPoison" => new EPAlchemyGetMakingPoisonConditionData(),
			"EPTemperingItemIsEnchanted" => new EPTemperingItemIsEnchantedConditionData(),
			"GetActivatorHeight" => new GetActivatorHeightConditionData(),
			"GetActorAggroRadiusViolated" => new GetActorAggroRadiusViolatedConditionData(),
			"GetActorCrimePlayerEnemy" => new GetActorCrimePlayerEnemyConditionData(),
			"GetActorsInHigh" => new GetActorsInHighConditionData(),
			"GetAlarmed" => new GetAlarmedConditionData(),
			"GetAllowWorldInteractions" => new GetAllowWorldInteractionsConditionData(),
			"GetAmountSoldStolen" => new GetAmountSoldStolenConditionData(),
			"GetArrestedState" => new GetArrestedStateConditionData(),
			"GetArrestingActor" => new GetArrestingActorConditionData(),
			"GetAttacked" => new GetAttackedConditionData(),
			"GetAttackState" => new GetAttackStateConditionData(),
			"GetBribeSuccess" => new GetBribeSuccessConditionData(),
			"GetCannibal" => new GetCannibalConditionData(),
			"GetClothingValue" => new GetClothingValueConditionData(),
			"GetCombatGroupMemberCount" => new GetCombatGroupMemberCountConditionData(),
			"GetCombatState" => new GetCombatStateConditionData(),
			"GetCurrentAIPackage" => new GetCurrentAIPackageConditionData(),
			"GetCurrentAIProcedure" => new GetCurrentAIProcedureConditionData(),
			"GetCurrentShoutVariation" => new GetCurrentShoutVariationConditionData(),
			"GetCurrentTime" => new GetCurrentTimeConditionData(),
			"GetCurrentWeatherPercent" => new GetCurrentWeatherPercentConditionData(),
			"GetDayOfWeek" => new GetDayOfWeekConditionData(),
			"GetDaysInJail" => new GetDaysInJailConditionData(),
			"GetDead" => new GetDeadConditionData(),
			"GetDefaultOpen" => new GetDefaultOpenConditionData(),
			"GetDestroyed" => new GetDestroyedConditionData(),
			"GetDestructionStage" => new GetDestructionStageConditionData(),
			"GetDialogueEmotion" => new GetDialogueEmotionConditionData(),
			"GetDialogueEmotionValue" => new GetDialogueEmotionValueConditionData(),
			"GetDisabled" => new GetDisabledConditionData(),
			"GetDisease" => new GetDiseaseConditionData(),
			"GetFlyingState" => new GetFlyingStateConditionData(),
			"GetFriendHit" => new GetFriendHitConditionData(),
			"GetGold" => new GetGoldConditionData(),
			"GetGroupMemberCount" => new GetGroupMemberCountConditionData(),
			"GetGroupTargetCount" => new GetGroupTargetCountConditionData(),
			"GetHasNote" => new GetHasNoteConditionData(),
			"GetHealthPercentage" => new GetHealthPercentageConditionData(),
			"GetHighestRelationshipRank" => new GetHighestRelationshipRankConditionData(),
			"GetIdleDoneOnce" => new GetIdleDoneOnceConditionData(),
			"GetIgnoreCrime" => new GetIgnoreCrimeConditionData(),
			"GetIgnoreFriendlyHits" => new GetIgnoreFriendlyHitsConditionData(),
			"GetIntimidateSuccess" => new GetIntimidateSuccessConditionData(),
			"GetIsAlerted" => new GetIsAlertedConditionData(),
			"GetIsCrashLandRequest" => new GetIsCrashLandRequestConditionData(),
			"GetIsFlying" => new GetIsFlyingConditionData(),
			"GetIsGhost" => new GetIsGhostConditionData(),
			"GetIsHastyLandRequest" => new GetIsHastyLandRequestConditionData(),
			"GetIsInjured" => new GetIsInjuredConditionData(),
			"GetIsPlayableRace" => new GetIsPlayableRaceConditionData(),
			"GetKnockedState" => new GetKnockedStateConditionData(),
			"GetKnockedStateEnum" => new GetKnockedStateEnumConditionData(),
			"GetLastBumpDirection" => new GetLastBumpDirectionConditionData(),
			"GetLastHitCritical" => new GetLastHitCriticalConditionData(),
			"GetLastPlayerAction" => new GetLastPlayerActionConditionData(),
			"GetLevel" => new GetLevelConditionData(),
			"GetLightLevel" => new GetLightLevelConditionData(),
			"GetLocked" => new GetLockedConditionData(),
			"GetLockLevel" => new GetLockLevelConditionData(),
			"GetLowestRelationshipRank" => new GetLowestRelationshipRankConditionData(),
			"GetMapMarkerVisible" => new GetMapMarkerVisibleConditionData(),
			"GetMovementDirection" => new GetMovementDirectionConditionData(),
			"GetMovementSpeed" => new GetMovementSpeedConditionData(),
			"GetNoBleedoutRecovery" => new GetNoBleedoutRecoveryConditionData(),
			"GetOffersServicesNow" => new GetOffersServicesNowConditionData(),
			"GetOpenState" => new GetOpenStateConditionData(),
			"GetPairedAnimation" => new GetPairedAnimationConditionData(),
			"GetPathingCurrentSpeed" => new GetPathingCurrentSpeedConditionData(),
			"GetPathingTargetSpeed" => new GetPathingTargetSpeedConditionData(),
			"GetPlayerAction" => new GetPlayerActionConditionData(),
			"GetPlayerTeammate" => new GetPlayerTeammateConditionData(),
			"GetPlayerTeammateCount" => new GetPlayerTeammateCountConditionData(),
			"GetRandomPercent" => new GetRandomPercentConditionData(),
			"GetRealHoursPassed" => new GetRealHoursPassedConditionData(),
			"GetRestrained" => new GetRestrainedConditionData(),
			"GetScale" => new GetScaleConditionData(),
			"GetSecondsPassed" => new GetSecondsPassedConditionData(),
			"GetSitting" => new GetSittingConditionData(),
			"GetSleeping" => new GetSleepingConditionData(),
			"GetStaminaPercentage" => new GetStaminaPercentageConditionData(),
			"GetTalkedToPC" => new GetTalkedToPCConditionData(),
			"GetTimeDead" => new GetTimeDeadConditionData(),
			"GetTrespassWarningLevel" => new GetTrespassWarningLevelConditionData(),
			"GetUnconscious" => new GetUnconsciousConditionData(),
			"GetVampireFeed" => new GetVampireFeedConditionData(),
			"GetVATSBackTargetVisible" => new GetVATSBackTargetVisibleConditionData(),
			"GetVATSLeftAreaFree" => new GetVATSLeftAreaFreeConditionData(),
			"GetVATSMode" => new GetVATSModeConditionData(),
			"GetVATSRightAreaFree" => new GetVATSRightAreaFreeConditionData(),
			"GetVatsTargetHeight" => new GetVatsTargetHeightConditionData(),
			"GetWalkSpeed" => new GetWalkSpeedConditionData(),
			"GetWantBlocking" => new GetWantBlockingConditionData(),
			"GetWeaponAnimType" => new GetWeaponAnimTypeConditionData(),
			"GetWindSpeed" => new GetWindSpeedConditionData(),
			"GetXPForNextLevel" => new GetXPForNextLevelConditionData(),
			"HasBeenEaten" => new HasBeenEatenConditionData(),
			"HasFamilyRelationshipAny" => new HasFamilyRelationshipAnyConditionData(),
			"HasLoaded3D" => new HasLoaded3DConditionData(),
			"HasTwoHandedWeaponEquipped" => new HasTwoHandedWeaponEquippedConditionData(),
			"IsActor" => new IsActorConditionData(),
			"IsActorAVictim" => new IsActorAVictimConditionData(),
			"IsActorUsingATorch" => new IsActorUsingATorchConditionData(),
			"IsAllowedToFly" => new IsAllowedToFlyConditionData(),
			"IsAttacking" => new IsAttackingConditionData(),
			"IsBeingRidden" => new IsBeingRiddenConditionData(),
			"IsBleedingOut" => new IsBleedingOutConditionData(),
			"IsBlocking" => new IsBlockingConditionData(),
			"IsBribedbyPlayer" => new IsBribedbyPlayerConditionData(),
			"IsCarryable" => new IsCarryableConditionData(),
			"IsCasting" => new IsCastingConditionData(),
			"IsChild" => new IsChildConditionData(),
			"IsCloudy" => new IsCloudyConditionData(),
			"IsCommandedActor" => new IsCommandedActorConditionData(),
			"IsContinuingPackagePCNear" => new IsContinuingPackagePCNearConditionData(),
			"IsDualCasting" => new IsDualCastingConditionData(),
			"IsEnteringInteractionQuick" => new IsEnteringInteractionQuickConditionData(),
			"IsEssential" => new IsEssentialConditionData(),
			"IsExitingInstant" => new IsExitingInstantConditionData(),
			"IsExitingInteractionQuick" => new IsExitingInteractionQuickConditionData(),
			"IsFacingUp" => new IsFacingUpConditionData(),
			"IsFleeing" => new IsFleeingConditionData(),
			"IsFlyingMountFastTravelling" => new IsFlyingMountFastTravellingConditionData(),
			"IsFlyingMountPatrolQueud" => new IsFlyingMountPatrolQueudConditionData(),
			"IsGoreDisabled" => new IsGoreDisabledConditionData(),
			"IsGreetingPlayer" => new IsGreetingPlayerConditionData(),
			"IsGuard" => new IsGuardConditionData(),
			"IsIgnoringCombat" => new IsIgnoringCombatConditionData(),
			"IsInCombat" => new IsInCombatConditionData(),
			"IsInCriticalStage" => new IsInCriticalStageConditionData(),
			"IsInDangerousWater" => new IsInDangerousWaterConditionData(),
			"IsInDialogueWithPlayer" => new IsInDialogueWithPlayerConditionData(),
			"IsInFavorState" => new IsInFavorStateConditionData(),
			"IsInFriendStateWithPlayer" => new IsInFriendStateWithPlayerConditionData(),
			"IsInInterior" => new IsInInteriorConditionData(),
			"IsInMyOwnedCell" => new IsInMyOwnedCellConditionData(),
			"IsInScene" => new IsInSceneConditionData(),
			"IsIntimidatedbyPlayer" => new IsIntimidatedbyPlayerConditionData(),
			"IsLastHostileActor" => new IsLastHostileActorConditionData(),
			"IsLeftUp" => new IsLeftUpConditionData(),
			"IsMoving" => new IsMovingConditionData(),
			"IsOnFlyingMount" => new IsOnFlyingMountConditionData(),
			"IsPathing" => new IsPathingConditionData(),
			"IsPC1stPerson" => new IsPC1stPersonConditionData(),
			"IsPCAMurderer" => new IsPCAMurdererConditionData(),
			"IsPCSleeping" => new IsPCSleepingConditionData(),
			"IsPlayerMovingIntoNewSpace" => new IsPlayerMovingIntoNewSpaceConditionData(),
			"IsPlayersLastRiddenMount" => new IsPlayersLastRiddenMountConditionData(),
			"IsPleasant" => new IsPleasantConditionData(),
			"IsPoison" => new IsPoisonConditionData(),
			"IsPowerAttacking" => new IsPowerAttackingConditionData(),
			"IsProtected" => new IsProtectedConditionData(),
			"IsRaining" => new IsRainingConditionData(),
			"IsRecoiling" => new IsRecoilingConditionData(),
			"IsRidingMount" => new IsRidingMountConditionData(),
			"IsRotating" => new IsRotatingConditionData(),
			"IsRunning" => new IsRunningConditionData(),
			"IsScenePackageRunning" => new IsScenePackageRunningConditionData(),
			"IsScenePlaying" => new IsScenePlayingConditionData(),
			"IsShieldOut" => new IsShieldOutConditionData(),
			"IsSmallBump" => new IsSmallBumpConditionData(),
			"IsSneaking" => new IsSneakingConditionData(),
			"IsSnowing" => new IsSnowingConditionData(),
			"IsSprinting" => new IsSprintingConditionData(),
			"IsStaggered" => new IsStaggeredConditionData(),
			"IsSwimming" => new IsSwimmingConditionData(),
			"IsTalking" => new IsTalkingConditionData(),
			"IsTimePassing" => new IsTimePassingConditionData(),
			"IsTorchOut" => new IsTorchOutConditionData(),
			"IsTrespassing" => new IsTrespassingConditionData(),
			"IsTurning" => new IsTurningConditionData(),
			"IsUndead" => new IsUndeadConditionData(),
			"IsUnique" => new IsUniqueConditionData(),
			"IsUnlockedDoor" => new IsUnlockedDoorConditionData(),
			"IsWaterObject" => new IsWaterObjectConditionData(),
			"IsWeaponMagicOut" => new IsWeaponMagicOutConditionData(),
			"IsWeaponOut" => new IsWeaponOutConditionData(),
			"SameFactionAsPC" => new SameFactionAsPCConditionData(),
			"SameRaceAsPC" => new SameRaceAsPCConditionData(),
			"SameSexAsPC" => new SameSexAsPCConditionData(),
	        
			// One Parameter
			"CanPayCrimeGold" => new CanPayCrimeGoldConditionData()
			{
				Faction = GetFaction<CanPayCrimeGoldConditionData>(param1)
			},
			"EPAlchemyEffectHasKeyword" => new EPAlchemyEffectHasKeywordConditionData()
			{
				Keyword = GetKeyword<EPAlchemyEffectHasKeywordConditionData>(param1)
			},
	        "EPMagic_IsAdvanceSkill" => new EPMagic_IsAdvanceSkillConditionData()
	        {
		        ActorValue = GetActorValue(param1)
	        },
			"EPMagic_SpellHasKeyword" => new EPMagic_SpellHasKeywordConditionData()
			{
				Keyword = GetKeyword<EPMagic_SpellHasKeywordConditionData>(param1)
			},
			"EPMagic_SpellHasSkill" => new EPMagic_SpellHasSkillConditionData()
			{
				ActorValue = GetActorValue(param1)
			},
			"EPModSkillUsage_AdvanceObjectHasKeyword" => new EPModSkillUsage_AdvanceObjectHasKeywordConditionData()
			{
				Keyword = GetKeyword<EPModSkillUsage_AdvanceObjectHasKeywordConditionData>(param1)
			},
			"EPModSkillUsage_IsAdvanceAction" => new EPModSkillUsage_IsAdvanceActionConditionData()
			{
				AdvanceAction = Enum.Parse<AdvanceAction>(param1 ?? "NormalUsage")
			},
			"EPTemperingItemHasKeyword" => new EPTemperingItemHasKeywordConditionData()
			{
				Keyword = GetKeyword<EPTemperingItemHasKeywordConditionData>(param1)
			},
			"Exists" => new ExistsConditionData()
			{
				Target = GetPlacedSimple<ExistsConditionData>(param1)
			},
	        "GetActorValue" => new GetActorValueConditionData()
	        {
		        ActorValue = GetActorValue(param1)
	        },
			"GetActorValuePercent" => new GetActorValuePercentConditionData()
			{
				ActorValue = GetActorValue(param1)
			},
			"GetAngle" => new GetAngleConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetBaseActorValue" => new GetBaseActorValueConditionData()
			{
				ActorValue = GetActorValue(param1)
			},
			"GetCombatTargetHasKeyword" => new GetCombatTargetHasKeywordConditionData()
			{
				Keyword = GetKeyword<GetCombatTargetHasKeywordConditionData>(param1)
			},
			"GetCrimeGold" => new GetCrimeGoldConditionData()
			{
				Faction = GetFaction<GetCrimeGoldConditionData>(param1)
			},
			"GetCrimeGoldNonviolent" => new GetCrimeGoldNonviolentConditionData()
			{
				Faction = GetFaction<GetCrimeGoldNonviolentConditionData>(param1)
			},
			"GetCrimeGoldViolent" => new GetCrimeGoldViolentConditionData()
			{
				Faction = GetFaction<GetCrimeGoldViolentConditionData>(param1)
			},
			"GetCurrentCastingType" => new GetCurrentCastingTypeConditionData()
			{
				SpellSource = GetCastSource(param1)
			},
			"GetCurrentDeliveryType" => new GetCurrentDeliveryTypeConditionData()
			{
				SpellSource = GetCastSource(param1)
			},
			"GetDeadCount" => new GetDeadCountConditionData()
			{
				Npc = GetNpc<GetDeadCountConditionData>(param1)
			},
			"GetDetected" => new GetDetectedConditionData()
			{
				TargetNpc = GetPlacedNpc<GetDetectedConditionData>(param1)
			},
			"GetDistance" => new GetDistanceConditionData()
			{
				Target = GetPlacedSimple<GetDistanceConditionData>(param1)
			},
			"GetEquipped" => new GetEquippedConditionData()
			{
				ItemOrList = GetItemOrList<GetEquippedConditionData>(param1)
			},
	        "GetEquippedItemType" => new GetEquippedItemTypeConditionData()
	        {
		        ItemSource = GetCastSource(param1)
	        },
			"GetEquippedShout" => new GetEquippedShoutConditionData()
			{
				Shout = GetShout<GetEquippedShoutConditionData>(param1)
			},
			"GetEventData" => new GetEventDataConditionData()
			{
				Function = Enum.Parse<GetEventDataConditionData.EventFunction>(param1 ?? "GetValue")
			},
			"GetFactionRank" => new GetFactionRankConditionData()
			{
				Faction = GetFaction<GetFactionRankConditionData>(param1)
			},
			"GetFactionRelation" => new GetFactionRelationConditionData()
			{
				TargetNpc = GetPlacedNpc<GetFactionRelationConditionData>(param1)
			},
	        "GetGlobalValue" => new GetGlobalValueConditionData()
	        {
		        Global = GetGlobal<GetGlobalValueConditionData>(param1)
			},
			"GetGraphVariableFloat" => new GetGraphVariableFloatConditionData()
			{
				GraphVariable = param1
			},
			"GetGraphVariableInt" => new GetGraphVariableIntConditionData()
			{
				GraphVariable = param1
			},
			"GetHeadingAngle" => new GetHeadingAngleConditionData()
			{
				Target = GetPlacedSimple<GetHeadingAngleConditionData>(param1),
			},
			"GetInCell" => new GetInCellConditionData()
			{
				Cell = GetCell<GetInCellConditionData>(param1)
			},
			"GetInContainer" => new GetInContainerConditionData()
			{
				Target = GetPlacedSimple<GetInContainerConditionData>(param1)
			},
			"GetInCurrentLoc" => new GetInCurrentLocConditionData()
			{
                Location = GetLocation<GetInCurrentLocConditionData>(param1)
			},
			"GetInCurrentLocAlias" => new GetInCurrentLocAliasConditionData()
			{
				LocationAliasIndex = GetIntValue(param1)
			},
			"GetInCurrentLocFormList" => new GetInCurrentLocFormListConditionData()
			{
				FormList = GetFormList<GetInCurrentLocFormListConditionData>(param1)
			},
			"GetInFaction" => new GetInFactionConditionData()
			{
				Faction = GetFaction<GetInFactionConditionData>(param1)
			},
			"GetInSameCell" => new GetInSameCellConditionData()
			{
				Target = GetPlacedSimple<GetInSameCellConditionData>(param1)
			},
			"GetInSharedCrimeFaction" => new GetInSharedCrimeFactionConditionData()
			{
				Target = GetPlacedSimple<GetInSharedCrimeFactionConditionData>(param1)
			},
	        "GetInWorldspace" => new GetInWorldspaceConditionData()
	        {
		        WorldspaceOrList = GetWorldspaceOrList<GetInWorldspaceConditionData>(param1)
	        },
			"GetInZone" => new GetInZoneConditionData()
			{
				EncounterZone = GetEncounterZone<GetInZoneConditionData>(param1)
			},
			"GetIsAliasRef" => new GetIsAliasRefConditionData()
			{
				ReferenceAliasIndex = GetIntValue(param1)
			},
			"GetIsClass" => new GetIsClassConditionData()
			{
				Class = GetClass<GetIsClassConditionData>(param1)
			},
			"GetIsClassDefault" => new GetIsClassDefaultConditionData()
			{
				Class = GetClass<GetIsClassDefaultConditionData>(param1)
			},
			"GetIsCrimeFaction" => new GetIsCrimeFactionConditionData()
			{
				Faction = GetFaction<GetIsCrimeFactionConditionData>(param1)
			},
			"GetIsCurrentPackage" => new GetIsCurrentPackageConditionData()
			{
				Package = GetPackage<GetIsCurrentPackageConditionData>(param1)
			},
			"GetIsCurrentWeather" => new GetIsCurrentWeatherConditionData()
			{
				Weather = GetWeather<GetIsCurrentWeatherConditionData>(param1)
			},
			"GetIsEditorLocAlias" => new GetIsEditorLocAliasConditionData()
			{
                LocationAliasIndex = GetIntValue(param1)
			},
			"GetIsEditorLocation" => new GetIsEditorLocationConditionData()
			{
                Location = GetLocation<GetIsEditorLocationConditionData>(param1)
			},
	        "GetIsID" => new GetIsIDConditionData()
	        {
		        Object = GetReferenceableObject<GetIsIDConditionData>(param1)
	        },
			"GetIsObjectType" => new GetIsObjectTypeConditionData()
			{
				FormType = Enum.Parse<FormType>(param1 ?? "Actor")
			},
			"GetIsRace" => new GetIsRaceConditionData()
			{
				Race = GetRace<GetIsRaceConditionData>(param1)
			},
			"GetIsReference" => new GetIsReferenceConditionData()
			{
				Target = GetPlacedSimple<GetIsReferenceConditionData>(param1)
			},
			"GetIsSex" => new GetIsSexConditionData()
			{
				MaleFemaleGender = GetGender(param1)
			},
			"GetIsUsedItemEquipType" => new GetIsUsedItemEquipTypeConditionData()
			{
				EquipType = GetEquipType<GetIsUsedItemEquipTypeConditionData>(param1)
			},
			"GetIsUsedItemType" => new GetIsUsedItemTypeConditionData()
			{
				FormType = Enum.Parse<FormType>(param1 ?? "Actor")
			},
			"GetIsVoiceType" => new GetIsVoiceTypeConditionData()
			{
				VoiceTypeOrList = GetVoiceTypeOrList<GetIsVoiceTypeConditionData>(param1)
			},
			"GetItemCount" => new GetItemCountConditionData()
			{
				ItemOrList = GetItemOrList<GetItemCountConditionData>(param1)
			},
			"GetKeywordDataForCurrentLocation" => new GetKeywordDataForCurrentLocationConditionData()
			{
				Keyword = GetKeyword<GetKeywordDataForCurrentLocationConditionData>(param1)
			},
			"GetKeywordItemCount" => new GetKeywordItemCountConditionData()
			{
				Keyword = GetKeyword<GetKeywordItemCountConditionData>(param1)
			},
			"GetLineOfSight" => new GetLineOfSightConditionData()
			{
				Target = GetPlacedSimple<GetLineOfSightConditionData>(param1)
			},
			"GetLocationAliasCleared" => new GetLocationAliasClearedConditionData()
			{
                LocationAliasIndex = GetIntValue(param1)
			},
			"GetLocationCleared" => new GetLocationClearedConditionData()
			{
                Location = GetLocation<GetLocationClearedConditionData>(param1)
			},
			"GetNumericPackageData" => new GetNumericPackageDataConditionData()
			{
				PackageDataIndex = GetIntValue(param1)
			},
			"GetPathingCurrentSpeedAngle" => new GetPathingCurrentSpeedAngleConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetPathingTargetAngleOffset" => new GetPathingTargetAngleOffsetConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetPathingTargetOffset" => new GetPathingTargetOffsetConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetPathingTargetSpeedAngle" => new GetPathingTargetSpeedAngleConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetPCEnemyofFaction" => new GetPCEnemyofFactionConditionData()
			{
				Faction = GetFaction<GetPCEnemyofFactionConditionData>(param1)
			},
			"GetPCExpelled" => new GetPCExpelledConditionData()
			{
				Faction = GetFaction<GetPCExpelledConditionData>(param1)
			},
			"GetPCFactionAttack" => new GetPCFactionAttackConditionData()
			{
				Faction = GetFaction<GetPCFactionAttackConditionData>(param1)
			},
			"GetPCFactionMurder" => new GetPCFactionMurderConditionData()
			{
				Faction = GetFaction<GetPCFactionMurderConditionData>(param1)
			},
			"GetPCInFaction" => new GetPCInFactionConditionData()
			{
				Faction = GetFaction<GetPCInFactionConditionData>(param1)
			},
			"GetPCIsClass" => new GetPCIsClassConditionData()
			{
				Class = GetClass<GetPCIsClassConditionData>(param1)
			},
			"GetPCIsRace" => new GetPCIsRaceConditionData()
			{
				Race = GetRace<GetPCIsRaceConditionData>(param1)
			},
			"GetPCIsSex" => new GetPCIsSexConditionData()
			{
				MaleFemaleGender = GetGender(param1)
			},
			"GetPCMiscStat" => new GetPCMiscStatConditionData()
			{
				MiscStat = Enum.Parse<MiscStatEnum>(param1 ?? "BooksRead")
			},
			"GetPermanentActorValue" => new GetPermanentActorValueConditionData()
			{
				ActorValue = GetActorValue(param1)
			},
			"GetPos" => new GetPosConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetQuestCompleted" => new GetQuestCompletedConditionData()
			{
				Quest = GetQuest<GetQuestCompletedConditionData>(param1)
			},
			"GetQuestRunning" => new GetQuestRunningConditionData()
			{
				Quest = GetQuest<GetQuestRunningConditionData>(param1)
			},
			"GetRelationshipRank" => new GetRelationshipRankConditionData()
			{
				TargetNpc = GetPlacedNpc<GetRelationshipRankConditionData>(param1)
			},
			"GetReplacedItemType" => new GetReplacedItemTypeConditionData()
			{
				ItemSource = GetCastSource(param1)
			},
			"GetShouldAttack" => new GetShouldAttackConditionData()
			{
				TargetNpc = GetPlacedNpc<GetShouldAttackConditionData>(param1)
			},
			"GetShouldHelp" => new GetShouldHelpConditionData()
			{
				TargetNpc = GetPlacedNpc<GetShouldHelpConditionData>(param1)
			},
			"GetSpellUsageNum" => new GetSpellUsageNumConditionData()
			{
				MagicItem = GetMagicItem<GetSpellUsageNumConditionData>(param1)
			},
			"GetStage" => new GetStageConditionData()
			{
				Quest = GetQuest<GetStageConditionData>(param1)
			},
			"GetStartingAngle" => new GetStartingAngleConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetStartingPos" => new GetStartingPosConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetStolenItemValue" => new GetStolenItemValueConditionData()
			{
				Faction = GetFaction<GetStolenItemValueConditionData>(param1)
			},
			"GetStolenItemValueNoCrime" => new GetStolenItemValueNoCrimeConditionData()
			{
				Faction = GetFaction<GetStolenItemValueNoCrimeConditionData>(param1)
			},
			"GetTalkedToPCParam" => new GetTalkedToPCParamConditionData()
			{
				TargetNpc = GetPlacedNpc<GetTalkedToPCParamConditionData>(param1)
			},
			"GetTargetHeight" => new GetTargetHeightConditionData()
			{
				Target = GetPlacedSimple<GetTargetHeightConditionData>(param1)
			},
			"GetThreatRatio" => new GetThreatRatioConditionData()
			{
				TargetNpc = GetPlacedNpc<GetThreatRatioConditionData>(param1)
			},
			"GetVATSBackAreaFree" => new GetVATSBackAreaFreeConditionData()
			{
				Target = GetPlacedSimple<GetVATSBackAreaFreeConditionData>(param1)
			},
			"GetVATSFrontAreaFree" => new GetVATSFrontAreaFreeConditionData()
			{
				Target = GetPlacedSimple<GetVATSFrontAreaFreeConditionData>(param1)
			},
			"GetVATSFrontTargetVisible" => new GetVATSFrontTargetVisibleConditionData()
			{
				Target = GetPlacedSimple<GetVATSFrontTargetVisibleConditionData>(param1)
			},
			"GetVATSLeftTargetVisible" => new GetVATSLeftTargetVisibleConditionData()
			{
				Target = GetPlacedSimple<GetVATSLeftTargetVisibleConditionData>(param1)
			},
			"GetVATSRightTargetVisible" => new GetVATSRightTargetVisibleConditionData()
			{
				Target = GetPlacedSimple<GetVATSRightTargetVisibleConditionData>(param1)
			},
			"GetVelocity" => new GetVelocityConditionData()
			{
				Axis = GetAxis(param1)
			},
			"GetWithinPackageLocation" => new GetWithinPackageLocationConditionData()
			{
				PackageDataIndex = GetIntValue(param1)
			},
			"HasAssociationTypeAny" => new HasAssociationTypeAnyConditionData()
			{
				AssociationType = GetAssociationType<HasAssociationTypeConditionData>(param1)
			},
			"HasBoundWeaponEquipped" => new HasBoundWeaponEquippedConditionData()
			{
				WeaponSource = GetCastSource(param1)
			},
			"HasEquippedSpell" => new HasEquippedSpellConditionData()
			{
				SpellSource = GetCastSource(param1)
			},
			"HasFamilyRelationship" => new HasFamilyRelationshipConditionData()
			{
				TargetNpc = GetPlacedNpc<HasFamilyRelationshipConditionData>(param1)
			},
	        "HasKeyword" => new HasKeywordConditionData()
	        {
		        Keyword = GetKeyword<HasKeywordConditionData>(param1)
	        },
			"HasLinkedRef" => new HasLinkedRefConditionData()
			{
				Keyword = GetKeyword<HasLinkedRefConditionData>(param1)
			},
	        "HasMagicEffect" => new HasMagicEffectConditionData()
	        {
		        MagicEffect = GetMagicEffect<HasMagicEffectConditionData>(param1)
	        },
			"HasMagicEffectKeyword" => new HasMagicEffectKeywordConditionData()
			{
				Keyword = GetKeyword<HasMagicEffectKeywordConditionData>(param1)
			},
			"HasParentRelationship" => new HasParentRelationshipConditionData()
			{
				TargetNpc = GetPlacedNpc<HasParentRelationshipConditionData>(param1)
			},
	        "HasPerk" => new HasPerkConditionData()
	        {
		        Perk = GetPerk<HasPerkConditionData>(param1)
	        },
			"HasRefType" => new HasRefTypeConditionData()
			{
				LocationReferenceType = GetLocationReferenceType<HasRefTypeConditionData>(param1)
			},
			"HasShout" => new HasShoutConditionData()
			{
				Shout = GetShout<HasShoutConditionData>(param1)
			},
			"HasSpell" => new HasSpellConditionData()
			{
				Spell = GetSpell<HasSpellConditionData>(param1)
			},
	        "IsAttackType" => new IsAttackTypeConditionData()
	        {
				Keyword = GetKeyword<IsAttackTypeConditionData>(param1)
	        },
			"IsCombatTarget" => new IsCombatTargetConditionData()
			{
				TargetNpc = GetPlacedNpc<IsCombatTargetConditionData>(param1)
			},
			"IsCurrentFurnitureObj" => new IsCurrentFurnitureObjConditionData()
			{
				Furniture = GetFurniture<IsCurrentFurnitureObjConditionData>(param1)
			},
			"IsCurrentFurnitureRef" => new IsCurrentFurnitureRefConditionData()
			{
				Target = GetPlacedSimple<IsCurrentFurnitureRefConditionData>(param1)
			},
			"IsFurnitureAnimType" => new IsFurnitureAnimTypeConditionData()
			{
				FurnitureAnimType = GetFurnitureAnimType(param1)
			},
			"IsFurnitureEntryType" => new IsFurnitureEntryTypeConditionData()
			{
				FurnitureEntryType = GetFurnitureEntryType(param1)
			},
	        "IsHostileToActor" => new IsHostileToActorConditionData()
	        {
		        TargetNpc  = GetPlacedNpc<IsHostileToActorConditionData>(param1)
	        },
			"IsInFurnitureState" => new IsInFurnitureStateConditionData()
			{
				FurnitureAnimType = GetFurnitureAnimType(param1)
			},
			"IsInList" => new IsInListConditionData()
			{
				FormList = GetFormList<IsInListConditionData>(param1)
			},
			"IsKiller" => new IsKillerConditionData()
			{
				TargetNpc = GetPlacedNpc<IsKillerConditionData>(param1)
			},
			"IsKillerObject" => new IsKillerObjectConditionData()
			{
				FormList = GetFormList<IsKillerObjectConditionData>(param1)
			},
			"IsLastIdlePlayed" => new IsLastIdlePlayedConditionData()
			{
				IdleAnimation = GetIdleAnimation<IsLastIdlePlayedConditionData>(param1)
			},
			"IsLimbGone" => new IsLimbGoneConditionData()
			{
				Limb = GetIntValue(param1)
			},
			"IsLocAliasLoaded" => new IsLocAliasLoadedConditionData()
			{
                LocationAliasIndex = GetIntValue(param1)
			},
			"IsLocationLoaded" => new IsLocationLoadedConditionData()
			{
                Location = GetLocation<IsLocationLoadedConditionData>(param1)
			},
			"IsNullPackageData" => new IsNullPackageDataConditionData()
			{
				PackageDataIndex = GetIntValue(param1)
			},
			"IsOwner" => new IsOwnerConditionData()
			{
				Owner = GetOwner<IsOwnerConditionData>(param1)
			},
			"IsPlayerActionActive" => new IsPlayerActionActiveConditionData()
			{
				PlayerAction = GetPlayerAction(param1)
			},
			"IsPlayerGrabbedRef" => new IsPlayerGrabbedRefConditionData()
			{
				Target = GetPlacedSimple<IsPlayerGrabbedRefConditionData>(param1)
			},
			"IsPlayerInRegion" => new IsPlayerInRegionConditionData()
			{
				Region = GetRegion<IsPlayerInRegionConditionData>(param1)
			},
			"IsSpellTarget" => new IsSpellTargetConditionData()
			{
				MagicItem = GetMagicItem<IsSpellTargetConditionData>(param1)
			},
			"IsTalkingActivatorActor" => new IsTalkingActivatorActorConditionData()
			{
				TargetNpc = GetPlacedNpc<IsTalkingActivatorActorConditionData>(param1)
			},
			"IsWardState" => new IsWardStateConditionData()
			{
				WardState = Enum.Parse<WardState>(param1 ?? "None")
			},
			"IsWarningAbout" => new IsWarningAboutConditionData()
			{
				FormList = GetFormList<IsWarningAboutConditionData>(param1)
			},
			"IsWeaponInList" => new IsWeaponInListConditionData()
			{
				FormList = GetFormList<IsWeaponInListConditionData>(param1)
			},
			"IsWeaponSkillType" => new IsWeaponSkillTypeConditionData()
			{
				ActorValue = GetActorValue(param1)
			},
			"LocAliasHasKeyword" => new LocAliasHasKeywordConditionData()
			{
				Keyword = GetKeyword<LocAliasHasKeywordConditionData>(param1)
			},
			"LocAliasIsLocation" => new LocAliasIsLocationConditionData()
			{
                LocationAliasIndex = GetIntValue(param1)
			},
			"LocationHasKeyword" => new LocationHasKeywordConditionData()
			{
				Keyword = GetKeyword<LocationHasKeywordConditionData>(param1)
			},
			"LocationHasRefType" => new LocationHasRefTypeConditionData()
			{
				LocationReferenceType = GetLocationReferenceType<LocationHasRefTypeConditionData>(param1)
			},
			"PlayerKnows" => new PlayerKnowsConditionData()
			{
				Knowable = GetKnowable<PlayerKnowsConditionData>(param1)
			},
			"SameFaction" => new SameFactionConditionData()
			{
				TargetNpc = GetPlacedNpc<SameFactionConditionData>(param1)
			},
			"SameRace" => new SameRaceConditionData()
			{
				TargetNpc = GetPlacedNpc<SameRaceConditionData>(param1)
			},
			"SameSex" => new SameSexConditionData()
			{
				TargetNpc = GetPlacedNpc<SameSexConditionData>(param1)
			},
			"ShouldAttackKill" => new ShouldAttackKillConditionData()
			{
				TargetNpc = GetPlacedNpc<ShouldAttackKillConditionData>(param1)
			},
			"SpellHasCastingPerk" => new SpellHasCastingPerkConditionData()
			{
				Perk = GetPerk<SpellHasCastingPerkConditionData>(param1)
			},
			"SpellHasKeyword" => new SpellHasKeywordConditionData()
			{
				Keyword = GetKeyword<SpellHasKeywordConditionData>(param1)
			},
	        "WornApparelHasKeywordCount" => new WornApparelHasKeywordCountConditionData()
	        {
		        Keyword = GetKeyword<WornApparelHasKeywordCountConditionData>(param1)
	        },
			"WornHasKeyword" => new WornHasKeywordConditionData()
			{
				Keyword = GetKeyword<WornHasKeywordConditionData>(param1)
			},
	        
	        // Two Parameters
            "GetCrime" => new GetCrimeConditionData()
            {
                Criminal = GetPlacedNpc<GetCrimeConditionData>(param1),
                CrimeType = Enum.Parse<CrimeType>(param2 ?? "Steal")
            },
            "GetFactionCombatReaction" => new GetFactionCombatReactionConditionData()
            {
                FactionA = GetFaction<GetFactionCombatReactionConditionData>(param1),
                FactionB = GetFaction<GetFactionCombatReactionConditionData>(param2)
            },
            "GetFactionRankDifference" => new GetFactionRankDifferenceConditionData()
            { 
				Faction = GetFaction<GetFactionRankDifferenceConditionData>(param1),
                TargetNpc = GetPlacedNpc<GetFactionRankDifferenceConditionData>(param2)
            },
            "GetInCellParam" => new GetInCellParamConditionData()
            {
                Cell = GetCell<GetInCellParamConditionData>(param1),
                Target = GetPlacedSimple<GetInCellParamConditionData>(param2)
            },
            "GetKeywordDataForAlias" => new GetKeywordDataForAliasConditionData()
            {
                LocationAliasIndex = GetIntValue(param1),
                Keyword = GetKeyword<GetKeywordDataForAliasConditionData>(param2)
            },
            "GetKeywordDataForLocation" => new GetKeywordDataForLocationConditionData()
            {
                Location = GetLocation<GetKeywordDataForLocationConditionData>(param1),
                Keyword = GetKeyword<GetKeywordDataForLocationConditionData>(param2)
            },
            "GetLocAliasRefTypeAliveCount" => new GetLocAliasRefTypeAliveCountConditionData()
            {
	            LocationAliasIndex = GetIntValue(param1),
	            LocationReferenceType = GetLocationReferenceType<GetLocAliasRefTypeAliveCountConditionData>(param2)
            },
            "GetLocAliasRefTypeDeadCount" => new GetLocAliasRefTypeDeadCountConditionData()
            {
                LocationAliasIndex = GetIntValue(param1),
                LocationReferenceType = GetLocationReferenceType<GetLocAliasRefTypeDeadCountConditionData>(param2)
            },
            "GetPlayerControlsDisabled" => new GetPlayerControlsDisabledConditionData()
            {
                PlayerControlsParameterOne = GetIntValue(param1),
                PlayerControlsParameterTwo = GetIntValue(param2)
            },
            "GetRefTypeAliveCount" => new GetRefTypeAliveCountConditionData()
            {
	            Location = GetLocation<GetRefTypeAliveCountConditionData>(param1),
	            LocationReferenceType = GetLocationReferenceType<GetRefTypeAliveCountConditionData>(param2)
            },
            "GetRefTypeDeadCount" => new GetRefTypeDeadCountConditionData()
            {
                Location = GetLocation<GetRefTypeDeadCountConditionData>(param1),
                LocationReferenceType = GetLocationReferenceType<GetRefTypeDeadCountConditionData>(param2)
            },
            "GetRelativeAngle" => new GetRelativeAngleConditionData()
            {
                Target = GetPlacedSimple<GetRelativeAngleConditionData>(param1),
                Axis = GetAxis(param2)
            },
            "GetStageDone" => new GetStageDoneConditionData()
            {
                Quest = GetQuest<GetStageDoneConditionData>(param1),
                Stage = GetIntValue(param2)
            },
            "GetVMQuestVariable" => new GetVMQuestVariableConditionData()
            {
                Quest = GetQuest<GetVMQuestVariableConditionData>(param1),
                VariableName = param2
            },
            "GetVMScriptVariable" => new GetVMScriptVariableConditionData()
            {
                Target = GetPlacedSimple<GetVMScriptVariableConditionData>(param1),
                VariableName = param2
            },
            "GetWithinDistance" => new GetWithinDistanceConditionData()
            {
                Target = GetPlacedSimple<GetWithinDistanceConditionData>(param1),
                Distance = GetFloatValue(param2)
            },
            "HasAssociationType" => new HasAssociationTypeConditionData()
            {
                TargetNpc = GetPlacedNpc<HasAssociationTypeConditionData>(param1),
                AssociationType = GetAssociationType<HasAssociationTypeConditionData>(param2)
            },
            "HasSameEditorLocAsRef" => new HasSameEditorLocAsRefConditionData()
            {
                Target = GetPlacedSimple<HasSameEditorLocAsRefConditionData>(param1),
                Keyword = GetKeyword<HasSameEditorLocAsRefConditionData>(param2)
            },
            "HasSameEditorLocAsRefAlias" => new HasSameEditorLocAsRefAliasConditionData()
            {
                ReferenceAliasIndex = GetIntValue(param1),
                Keyword = GetKeyword<HasSameEditorLocAsRefAliasConditionData>(param2)
            },
            "IsCellOwner" => new IsCellOwnerConditionData()
            {
                Cell = GetCell<IsCellOwnerConditionData>(param1),
                Owner = GetOwner<IsCellOwnerConditionData>(param2)
            },
            "IsCloserToAThanB" => new IsCloserToAThanBConditionData()
            {
                TargetA = GetPlacedSimple<IsCloserToAThanBConditionData>(param1),
                TargetB = GetPlacedSimple<IsCloserToAThanBConditionData>(param2)
            },
            "IsCurrentSpell" => new IsCurrentSpellConditionData()
            {
                Spell = GetSpell<IsCurrentSpellConditionData>(param1),
                SpellSource = GetCastSource(param2)
            },
            "IsInSameCurrentLocAsRef" => new IsInSameCurrentLocAsRefConditionData()
            {
                Target = GetPlacedSimple<IsInSameCurrentLocAsRefConditionData>(param1),
                Keyword = GetKeyword<IsInSameCurrentLocAsRefConditionData>(param2)
            },
            "IsInSameCurrentLocAsRefAlias" => new IsInSameCurrentLocAsRefAliasConditionData()
            {
                ReferenceAliasIndex = GetIntValue(param1),
                Keyword = GetKeyword<IsInSameCurrentLocAsRefAliasConditionData>(param2)
            },
            "IsLinkedTo" => new IsLinkedToConditionData()
            {
                Target = GetPlacedSimple<IsLinkedToConditionData>(param1),
                Keyword = GetKeyword<IsLinkedToConditionData>(param2)
            },
            "IsSceneActionComplete" => new IsSceneActionCompleteConditionData()
            {
                Scene = GetScene<IsSceneActionCompleteConditionData>(param1),
                SceneActionIndex = GetIntValue(param2)
            },
	        
            _ => new DoesNotExistConditionData()
        };
    }
}
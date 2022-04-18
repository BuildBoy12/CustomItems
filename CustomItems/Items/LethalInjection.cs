// -----------------------------------------------------------------------
// <copyright file="LethalInjection.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Items
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using CustomPlayerEffects;
    using Exiled.API.Features;
    using Exiled.API.Features.Attributes;
    using Exiled.API.Features.Roles;
    using Exiled.API.Features.Spawn;
    using Exiled.CustomItems.API;
    using Exiled.CustomItems.API.Features;
    using Exiled.Events.EventArgs;
    using MEC;
    using PlayableScps;
    using PlayerStatsSystem;

    /// <inheritdoc />
    [CustomItem(ItemType.Adrenaline)]
    public class LethalInjection : CustomItem
    {
        /// <inheritdoc/>
        public override uint Id { get; set; } = 3;

        /// <inheritdoc/>
        public override string Name { get; set; } = "LJ-119";

        /// <inheritdoc/>
        public override string Description { get; set; } = "This is a Lethal Injection that, when used, will cause SCP-096 to immediately leave his enrage, regardless of how many targets he currently has, if you are one of his current targets. You always die when using this, even if there's no enrage to break, or you are not a target.";

        /// <inheritdoc/>
        public override float Weight { get; set; } = 1f;

        /// <inheritdoc/>
        public override SpawnProperties SpawnProperties { get; set; } = new()
        {
            Limit = 1,
            DynamicSpawnPoints = new List<DynamicSpawnPoint>
            {
                new()
                {
                    Chance = 100,
                    Location = SpawnLocation.Inside096,
                },
            },
        };

        /// <summary>
        /// Gets or sets a value indicating whether the Lethal Injection should always kill the user, regardless of if they stop SCP-096's enrage.
        /// </summary>
        [Description("Whether the Lethal Injection should always kill the user, regardless of if they stop SCP-096's enrage.")]
        public bool KillOnFail { get; set; } = true;

        /// <inheritdoc/>
        protected override void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem += OnUsingItem;
            base.SubscribeEvents();
        }

        /// <inheritdoc/>
        protected override void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.UsingItem -= OnUsingItem;
            base.UnsubscribeEvents();
        }

        private void OnUsingItem(UsedItemEventArgs ev)
        {
            Log.Debug($"{ev.Player.Nickname} used a medical item: {ev.Item}", Plugin.Instance.Config.IsDebugEnabled);
            if (!Check(ev.Player.CurrentItem))
                return;

            Timing.CallDelayed(1.5f, () =>
            {
                Log.Debug($"{ev.Player.Nickname} used a {Name}", Plugin.Instance.Config.IsDebugEnabled);
                foreach (Player player in Player.List)
                {
                    if (!player.Role.Is(out Scp096Role scp096))
                        continue;

                    Log.Debug($"{ev.Player.Nickname} - {Name} found an 096: {player.Nickname}", Plugin.Instance.Config.IsDebugEnabled);
                    Log.Debug($"{player.Nickname} 096 component found.", Plugin.Instance.Config.IsDebugEnabled);
                    if ((!scp096.Script.HasTarget(ev.Player.ReferenceHub) ||
                         scp096.State != Scp096PlayerState.Enraged) &&
                        scp096.State != Scp096PlayerState.Enraging &&
                        scp096.State != Scp096PlayerState.Attacking)
                        continue;

                    Log.Debug($"{player.Nickname} 096 checks passed.", Plugin.Instance.Config.IsDebugEnabled);
                    scp096.Script.EndEnrage();
                    ev.Player.Hurt(new UniversalDamageHandler(-1f, DeathTranslations.Poisoned));
                    return;
                }

                if (!KillOnFail)
                {
                    if (ev.Player.ArtificialHealth > 30)
                        ev.Player.ArtificialHealth -= 30;
                    else
                        ev.Player.ArtificialHealth = 0;
                    ev.Player.DisableEffect<Invigorated>();
                    return;
                }

                Log.Debug($"{Name} kill on fail: {ev.Player.Nickname}", Plugin.Instance.Config.IsDebugEnabled);
                ev.Player.Hurt(new UniversalDamageHandler(-1f, DeathTranslations.Poisoned));
            });

            ev.Player.RemoveItem(ev.Player.CurrentItem);
        }
    }
}
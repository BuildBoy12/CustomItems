// -----------------------------------------------------------------------
// <copyright file="C4Charge.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.API.Features.Items;
    using UnityEngine;

    /// <inheritdoc/>
    [CommandHandler(typeof(ClientCommandHandler))]
    public class C4Charge : ICommand
    {
        /// <inheritdoc/>
        public string Command => "detonate";

        /// <inheritdoc/>
        public string[] Aliases { get; } = { "det" };

        /// <inheritdoc/>
        public string Description => "Detonate command for detonating C4 charges";

        /// <inheritdoc/>
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);

            if (!Items.C4Charge.PlacedCharges.ContainsValue(ply))
            {
                response = "\n<color=red>You've haven't placed any C4 charges!</color>";
                return false;
            }

            if (Items.C4Charge.Instance.RequireDetonator && ply.CurrentItem.Type != Items.C4Charge.Instance.DetonatorItem)
            {
                response = $"\n<color=red>You need to have a Remote Detonator ({Items.C4Charge.Instance.DetonatorItem}) in your hand to detonate C4!</color>";
                return false;
            }

            int i = 0;

            foreach (KeyValuePair<Pickup, Player> charge in Items.C4Charge.PlacedCharges.ToList())
            {
                if (charge.Value != ply)
                    continue;

                float distance = Vector3.Distance(charge.Key.Position, ply.Position);
                if (distance < Items.C4Charge.Instance.MaxDistance)
                {
                    Items.C4Charge.Instance.C4Handler(charge.Key);
                    i++;
                }
                else
                {
                    ply.SendConsoleMessage($"One of your charges is out of range. You need to get closer by {Mathf.Round(distance - Items.C4Charge.Instance.MaxDistance)} meters.", "yellow");
                }
            }

            response = $"\n<color=green>{i} C4 {(i == 1 ? "charge has" : "charges have")} been detonated!</color>";
            return true;
        }
    }
}

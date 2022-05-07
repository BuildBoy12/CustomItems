// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems
{
    using System;
    using CustomItems.Events;
    using Exiled.API.Features;
    using Exiled.CustomItems.API.Features;
    using HarmonyLib;
    using Server = Exiled.Events.Handlers.Server;

    /// <inheritdoc />
    public class Plugin : Plugin<Config>
    {
        private Harmony harmonyInstance;
        private ServerHandler serverHandler;

        /// <summary>
        /// Gets the Plugin instance.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <inheritdoc/>
        public override Version RequiredExiledVersion { get; } = new(5, 0, 0);

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;

            serverHandler = new ServerHandler(this);
            Server.ReloadedConfigs += serverHandler.OnReloadingConfigs;

            harmonyInstance = new Harmony($"com.{Name}.galaxy-{DateTime.UtcNow.Ticks}");
            harmonyInstance.PatchAll();

            Config.LoadItems();

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            Config.ItemConfigs.Unregister();

            harmonyInstance?.UnpatchAll(harmonyInstance.Id);
            harmonyInstance = null;

            Server.ReloadedConfigs -= serverHandler.OnReloadingConfigs;
            serverHandler = null;

            Instance = null;

            base.OnDisabled();
        }
    }
}

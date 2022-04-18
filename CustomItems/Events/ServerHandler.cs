// -----------------------------------------------------------------------
// <copyright file="ServerHandler.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Events
{
    /// <summary>
    /// Event Handlers.
    /// </summary>
    public class ServerHandler
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHandler"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public ServerHandler(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// OnReloadingConfigs handler.
        /// </summary>
        public void OnReloadingConfigs() => plugin.Config.LoadItems();
    }
}

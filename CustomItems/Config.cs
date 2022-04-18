// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems
{
    using System.ComponentModel;
    using System.IO;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using Exiled.Loader;
    using YamlDotNet.Serialization;

    /// <summary>
    /// The plugin's config class.
    /// </summary>
    public class Config : IConfig
    {
        /// <summary>
        /// Gets the Item Config settings.
        /// </summary>
        [YamlIgnore]
        public Configs.Items ItemConfigs { get; private set; }

        /// <inheritdoc/>
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether if debug mode is enabled.
        /// </summary>
        [Description("Whether or not debug messages should be displayed in the server console.")]
        public bool IsDebugEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating what folder item configs will be stored in.
        /// </summary>
        public string ItemConfigFolder { get; set; } = Path.Combine(Paths.Configs, "CustomItems");

        /// <summary>
        /// Gets or sets a value indicating what file will be used for item configs.
        /// </summary>
        public string ItemConfigFile { get; set; } = "global.yml";

        /// <summary>
        /// Loads the item configs.
        /// </summary>
        public void LoadItems()
        {
            if (!Directory.Exists(ItemConfigFolder))
                Directory.CreateDirectory(ItemConfigFolder);

            string filePath = Path.Combine(ItemConfigFolder, ItemConfigFile);
            ItemConfigs = !File.Exists(filePath)
                ? new Configs.Items()
                : Loader.Deserializer.Deserialize<Configs.Items>(File.ReadAllText(filePath));

            File.WriteAllText(filePath, Loader.Serializer.Serialize(ItemConfigs));
        }
    }
}

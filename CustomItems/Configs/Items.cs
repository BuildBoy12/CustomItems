// -----------------------------------------------------------------------
// <copyright file="Items.cs" company="Galaxy119 and iopietro">
// Copyright (c) Galaxy119 and iopietro. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace CustomItems.Configs
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using CustomItems.Items;
    using Exiled.CustomItems.API;
    using Exiled.CustomItems.API.Features;

    /// <summary>
    /// All item config settings.
    /// </summary>
    public class Items
    {
        private IEnumerable<CustomItem> registeredItems;

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.EmpGrenade"/>s.
        /// </summary>
        [Description("The list of EMP grenades.")]
        public List<EmpGrenade> EmpGrenade { get; set; } = new()
        {
            new EmpGrenade(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.GrenadeLauncher"/>s.
        /// </summary>
        [Description("The list of grenade launchers.")]
        public List<GrenadeLauncher> GrenadeLauncher { get; set; } = new()
        {
            new GrenadeLauncher(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.ImplosionGrenade"/>s.
        /// </summary>
        [Description("The list of implosion grenades.")]
        public List<ImplosionGrenade> ImplosionGrenade { get; set; } = new()
        {
            new ImplosionGrenade(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.LethalInjection"/>s.
        /// </summary>
        [Description("The list of lethal injections.")]
        public List<LethalInjection> LethalInjection { get; set; } = new()
        {
            new LethalInjection(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.LuckyCoin"/>s.
        /// </summary>
        [Description("The list of lucky coins.")]
        public List<LuckyCoin> LuckyCoin { get; set; } = new()
        {
            new LuckyCoin(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.MediGun"/>s.
        /// </summary>
        [Description("The list of medical guns.")]
        public List<MediGun> MediGun { get; set; } = new()
        {
            new MediGun(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.Scp127"/>s.
        /// </summary>
        [Description("The list of Scp127s.")]
        public List<Scp127> Scp127 { get; set; } = new()
        {
            new Scp127(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.Scp1499"/>s.
        /// </summary>
        [Description("The list of Scp1499s.")]
        public List<Scp1499> Scp1499 { get; set; } = new()
        {
            new Scp1499(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.SniperRifle"/>s.
        /// </summary>
        [Description("The list of sniper rifles.")]
        public List<SniperRifle> SniperRifle { get; set; } = new()
        {
            new SniperRifle(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.TranquilizerGun"/>s.
        /// </summary>
        [Description("The list of tranquilizer guns.")]
        public List<TranquilizerGun> TranquilizerGun { get; set; } = new()
        {
            new TranquilizerGun(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.Scp714"/>s.
        /// </summary>
        [Description("The list of Scp714s.")]
        public List<Scp714> Scp714 { get; set; } = new()
        {
            new Scp714(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.AntiMemeticPills"/>.
        /// </summary>
        [Description("The list of Anti-Memetic Pills.")]
        public List<AntiMemeticPills> AntiMemeticPills { get; set; } = new()
        {
            new AntiMemeticPills(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.DeflectorShield"/>s.
        /// </summary>
        [Description("The list of DeflectorShields.")]
        public List<DeflectorShield> DeflectorShield { get; set; } = new()
        {
            new DeflectorShield(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="Scp2818"/>s.
        /// </summary>
        [Description("The list of SCP-2818s.")]
        public List<Scp2818> Scp2818 { get; set; } = new()
        {
            new Scp2818(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.C4Charge"/>s.
        /// </summary>
        [Description("The list of C4Charges.")]
        public List<C4Charge> C4Charge { get; set; } = new()
        {
            new C4Charge(),
        };

        /// <summary>
        /// Gets or sets the list of <see cref="CustomItems.Items.AutoGun"/>s.
        /// </summary>
        [Description("The list of AutoGuns.")]
        public List<AutoGun> AutoGun { get; set; } = new()
        {
            new AutoGun(),
        };

        /// <summary>
        /// Registers all custom items.
        /// </summary>
        public void Register() => registeredItems = CustomItem.RegisterItems(overrideClass: this);

        /// <summary>
        /// Unregisters all custom items.
        /// </summary>
        public void Unregister()
        {
            if (registeredItems is null)
                return;

            foreach (CustomItem customItem in registeredItems)
                customItem.Unregister();
        }
    }
}

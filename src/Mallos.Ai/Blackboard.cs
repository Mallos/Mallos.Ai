﻿namespace Mallos.Ai
{
    using System;
    using System.Collections.Generic;
    using Mallos.Ai.Dialog;

    /// <summary>
    /// A blackboard is a central place to store and look up relevant data.
    /// </summary>
    public class Blackboard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Blackboard"/> class.
        /// </summary>
        /// <param name="guid">A unique id.</param
        /// <param name="dialogHistoryCollection">The dialog history provider.</param>
        public Blackboard(
            Guid? guid = null,
            DialogHistoryCollection dialogHistoryCollection = null)
        {
            this.Guid = guid ?? Guid.NewGuid();
            this.DialogHistoryCollection = dialogHistoryCollection ?? new DialogHistoryCollectionInMemory();
        }

        /// <summary>
        /// Gets a unique id for the Blackboard.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Gets or sets the elapsed time.
        /// This is necessary for some nodes to work.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;

        /// <summary>
        /// Gets a value indicating whether a <see cref="Behavior.BehaviorTree"/> is running this blackboard now.
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>
        /// Gets and set easily accessable properties.
        /// </summary>
        public Dictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the dialog history provider.
        /// </summary>
        public DialogHistoryCollection DialogHistoryCollection { get; }

        /// <summary>
        /// Occures when we start to execute a <see cref="Behavior.BehaviorTree"/>.
        /// </summary>
        /// <returns>Return true if it should execute.</returns>
        public virtual bool OnBeforeExecute()
        {
            if (!this.IsRunning)
            {
                this.IsRunning = true;
                return true;
            }
            else
            {
                // Already running
                return false;
            }
        }

        /// <summary>
        /// Occures after executing a <see cref="Behavior.BehaviorTree"/>.
        /// </summary>
        public virtual void OnAfterExecute()
        {
            this.IsRunning = false;
        }
    }
}

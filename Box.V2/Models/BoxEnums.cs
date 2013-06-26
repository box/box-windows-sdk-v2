using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    /// <summary>
    /// The available types of access for a shared link
    /// </summary>
    public enum BoxSharedLinkAccessType
    {
        open,
        company,
        collaborators
    }

    /// <summary>
    /// The available types for the sync states
    /// </summary>
    public enum BoxSyncStateType
    {
        synced,
        not_synced,
        partially_synced
    }
}

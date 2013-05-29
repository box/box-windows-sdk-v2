using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models
{
    public enum BoxSharedLinkAccessType
    {
        open,
        company,
        collaborators
    }

    public enum BoxSyncStateType
    {
        synced,
        not_synced,
        partially_synced
    }
}

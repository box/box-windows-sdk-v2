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

    /// <summary>
    /// The way an item collection is ordered by
    /// </summary>
    public enum BoxSortBy
    {
        Type,
        Name,
        file_version_id,
        Id,
        policy_name,
        retention_policy_id,
        retention_policy_object_id,
        retention_policy_set_id
    }

    /// <summary>
    /// The sort direction of an item collection
    /// </summary>
    public enum BoxSortDirection
    {
        ASC,
        DESC
    }

    /// <summary>
    /// The operation type for a metadata update
    /// </summary>
    public enum MetadataUpdateOp
    {
        add,
        replace,
        remove,
        test,
        move,
        copy
    }

    /// <summary>
    /// Resulution state of task assignment
    /// </summary>
    public enum ResolutionStateType
    {
        completed,
        incomplete,
        approved,
        rejected
    }

    /// <summary>
    /// The operation type for a metadata template update
    /// </summary>
    public enum MetadataTemplateUpdateOp
    {
        addEnumOption,
        addField,
        editField,
        editTemplate,
        reorderEnumOptions,
        reorderFields
    }

    public enum UserEventsStreamType
    {
        all,
        changes,
        sync
    }
}

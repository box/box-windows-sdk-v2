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
        retention_policy_set_id,
        interacted_at
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
        editEnumOption,
        reorderEnumOptions,
        reorderFields,
        removeField,
        removeEnumOption
    }

    public enum UserEventsStreamType
    {
        all,
        changes,
        sync
    }

	
	/// <summary>
	/// Enum of popular representations that might be requested. This is only a suggestion and the user can also
	/// manually pass in representation types as well to build their specific request.
	/// </summary>	
	public enum RepresentationType
	{
		PDF = "[pdf]", 
		MULTI_PAGE = "[png?dimensions=1024x1024]",
		EXTRACTED_TEXT = "[extracted_text]",
		THUMBNAIL_SMALL = "[jpg?dimensions=320x320]",
		THUMBNAIL_MEDIUM = "[jpg?dimensions=1024x1024][png?dimensions=1024x1024]",
		THUMBNAIL_LARGE = "[jpg?dimensions=2048x2048][png?dimensions=2048x2048]"
	}
}

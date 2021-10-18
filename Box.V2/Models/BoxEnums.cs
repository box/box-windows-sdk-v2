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

    public enum DispositionAction
    {
        permanently_delete,
        remove_retention
    }

    /// <summary>
    /// Defines which assignees need to complete the task before it is considered completed.
    /// </summary>
    public enum BoxCompletionRule
    {
        /// <summary>
        /// all_assignees requires all assignees to review or approve the the task in order for it to be considered completed.
        /// </summary>
        all_assignees,
        /// <summary>
        /// any_assignee accepts any one assignee to review or approve the the task in order for it to be considered completed.
        /// </summary>
        any_assignee
    }
}

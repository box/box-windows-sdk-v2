using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Box.V2.Models.Request
{
    public enum BoxRequestFields
    {
        /*** An item, which could be a file, a folder, a weblink... */
        ITEM,
        /** A plural format of {@link #ITEM}. */
        ITEMS,
        /** A box file. */
        FILE,
        /** A plural format of {@link #FILE}. */
        FILES,
        /** A box weblink. */
        WEB_LINK,
        /** A plural format of {@link #WEB_LINK}. */
        WEB_LINKS,
        /** Preview of a file. */
        PREVIEW,
        /** A box folder. */
        FOLDER,
        /** A box user. */
        USER,
        /** A plural format of {@link #USER}. */
        USERS,
        /** A comment. */
        COMMENT,
        /** A plural format of {@link #COMMENT}. */
        COMMENTS,
        /** A version of a file. */
        FILE_VERSION,
        /** A plural format of {@link #FILE_VERSION}. */
        FILE_VERSIONS,
        /** Box's equivalent of access control lists. They can be used to set and apply permissions for users to folders. */
        COLLABORATION,
        /** A plural format of {@link ResourceType.COLLABORATION}. */
        COLLABORATIONS,
        /** An email alias. */
        EMAIL_ALIAS,
        /** A plural format of {@link #EMAIL_ALIAS}. */
        EMAIL_ALIASES,
        /** OAuth data. */
        OAUTH_DATA,
        /** Error. */
        ERROR,
        /** Event. */
        Event,
        /** Realtime server. */
        Realtime_Server
    }
}

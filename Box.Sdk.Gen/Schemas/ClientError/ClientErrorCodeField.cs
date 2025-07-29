using System.ComponentModel;
using Box.Sdk.Gen.Internal;

namespace Box.Sdk.Gen.Schemas {
    public enum ClientErrorCodeField {
        [Description("created")]
        Created,
        [Description("accepted")]
        Accepted,
        [Description("no_content")]
        NoContent,
        [Description("redirect")]
        Redirect,
        [Description("not_modified")]
        NotModified,
        [Description("bad_request")]
        BadRequest,
        [Description("unauthorized")]
        Unauthorized,
        [Description("forbidden")]
        Forbidden,
        [Description("not_found")]
        NotFound,
        [Description("method_not_allowed")]
        MethodNotAllowed,
        [Description("conflict")]
        Conflict,
        [Description("precondition_failed")]
        PreconditionFailed,
        [Description("too_many_requests")]
        TooManyRequests,
        [Description("internal_server_error")]
        InternalServerError,
        [Description("unavailable")]
        Unavailable,
        [Description("item_name_invalid")]
        ItemNameInvalid,
        [Description("insufficient_scope")]
        InsufficientScope
    }
}
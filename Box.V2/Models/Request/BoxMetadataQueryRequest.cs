using System.Collections.Generic;

namespace Box.V2.Models.Request
{
    /// <summary>
    /// An object representing the request to execute Metadata query.
    /// </summary>
    public class BoxMetadataQueryRequest
    {
        /// <summary>
        /// The template used in the query. Must be in the form scope.templateKey
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// The folder_id to which to restrain the query
        /// </summary>
        public string AncestorFolderId { get; set; }

        /// <summary>
        /// The logical expression of the query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Attribute(s) to include in the response
        /// </summary>
        public IEnumerable<string> Fields { get; set; }

        /// <summary>
        /// Required if query present. The arguments for the query
        /// </summary>
        public Dictionary<string, object> QueryParameters { get; set; }

        /// <summary>
        /// A list of BoxMetadataQueryOrderBy objects that contain field_key(s) to order on and the corresponding direction(s)
        /// </summary>
        public List<BoxMetadataQueryOrderBy> OrderBy { get; set; }

        /// <summary>
        /// The maximum number of items to return in a page. The default is 100 and the max is 1000.
        /// </summary>
        public int Limit { get; set; } = 100;

        /// <summary>
        /// The marker to use for requesting the next page
        /// </summary>
        public string Marker { get; set; }

        /// <summary>
        /// Whether or not to auto-paginate to fetch all items; defaults to false.
        /// </summary>
        public bool AutoPaginate { get; set; }

    }
}

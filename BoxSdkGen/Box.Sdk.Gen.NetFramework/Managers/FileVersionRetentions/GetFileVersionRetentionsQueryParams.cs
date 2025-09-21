using Box.Sdk.Gen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public class GetFileVersionRetentionsQueryParams {
        /// <summary>
        /// Filters results by files with this ID.
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// Filters results by file versions with this ID.
        /// </summary>
        public string FileVersionId { get; set; }

        /// <summary>
        /// Filters results by the retention policy with this ID.
        /// </summary>
        public string PolicyId { get; set; }

        /// <summary>
        /// Filters results by the retention policy with this disposition
        /// action.
        /// </summary>
        public StringEnum<GetFileVersionRetentionsQueryParamsDispositionActionField> DispositionAction { get; set; }

        /// <summary>
        /// Filters results by files that will have their disposition
        /// come into effect before this date.
        /// </summary>
        public string DispositionBefore { get; set; }

        /// <summary>
        /// Filters results by files that will have their disposition
        /// come into effect after this date.
        /// </summary>
        public string DispositionAfter { get; set; }

        /// <summary>
        /// The maximum number of items to return per page.
        /// </summary>
        public long? Limit { get; set; }

        /// <summary>
        /// Defines the position marker at which to begin returning results. This is
        /// used when paginating using marker-based pagination.
        /// 
        /// This requires `usemarker` to be set to `true`.
        /// </summary>
        public string Marker { get; set; }

        public GetFileVersionRetentionsQueryParams() {
            
        }
    }
}
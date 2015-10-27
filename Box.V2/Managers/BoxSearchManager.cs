using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Extensions;
using Box.V2.Models;
using Box.V2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    /// <summary>
    /// The manager that represents all of the Search endpoints
    /// </summary>
    public class BoxSearchManager : BoxResourceManager
    {
        public BoxSearchManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth)
            : base(config, service, converter, auth) { }


        /// <summary>
        /// Returns a collection of search results that match the keyword, if there are are no matching search results
        /// an empty collection will be returned
        /// </summary>
        /// <param name="keyword">The string to search for</param>
        /// <param name="limit">Number of search results to return</param>
        /// <param name="offset">The search result at which to start the response. Note: Both limit and offset must be included for either to be used. Offset must be a multiple of limit.</param>
        /// <param name="fields"></param>
        /// <param name="file_extensions">Limit searches to specific file extensions like pdf,png,doc. Requires one or a set of comma delimited file extensions: file_extension_1,file_extension_2,...</param>
        /// <param name="ancestor_folder_ids">Limit searches to specific parent folders. Requires one or a set of comma delimited folder_ids: folder_id_1,folder_id_2,.... Parent folder results will also include items within subfolders.</param>
        /// <param name="content_types">Limit searches to specific Box designated content types. Can be name, description, file_content, comments, or tags. Requires one or a set of comma delimited content_types: content_type_1,content_type_2,....</param>
        /// <param name="type">The type you want to return in your search. Can be file, folder, or web_link.</param>
        /// <returns>A collection of search results is returned. If there are no matching search results, the entries array will be empty.</returns>
        public async Task<BoxCollection<BoxItem>> SearchAsync(string keyword, int limit, int offset = 0, List<string> fields = null, 
          string file_extensions = null, string ancestor_folder_ids = null, string content_types = null, string type = null)
        {
            keyword.ThrowIfNullOrWhiteSpace("keyword");

            BoxRequest request = new BoxRequest(_config.SearchEndpointUri)
                .Param("query", keyword)
                .Param("limit", limit.ToString())
                .Param("offset", offset.ToString())
                .Param("file_extensions", file_extensions)
                .Param("ancestor_folder_ids", ancestor_folder_ids)
                .Param("content_types", content_types)
                .Param("type", type)
                .Param(ParamFields, fields);

            IBoxResponse<BoxCollection<BoxItem>> response = await ToResponseAsync<BoxCollection<BoxItem>>(request).ConfigureAwait(false);
                    
            return response.ResponseObject;
        }

    }
}

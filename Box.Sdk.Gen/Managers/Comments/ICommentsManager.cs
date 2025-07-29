using Box.Sdk.Gen;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Box.Sdk.Gen.Internal;
using Box.Sdk.Gen.Schemas;

namespace Box.Sdk.Gen.Managers {
    public interface ICommentsManager {
        /// <summary>
    /// Retrieves a list of comments for a file.
    /// </summary>
    /// <param name="fileId">
    /// The unique identifier that represents a file.
    /// 
    /// The ID for any file can be determined
    /// by visiting a file in the web application
    /// and copying the ID from the URL. For example,
    /// for the URL `https://*.app.box.com/files/123`
    /// the `file_id` is `123`.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getFileComments method
    /// </param>
    /// <param name="headers">
    /// Headers of getFileComments method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<Comments> GetFileCommentsAsync(string fileId, GetFileCommentsQueryParams? queryParams = default, GetFileCommentsHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Retrieves the message and metadata for a specific comment, as well
    /// as information on the user who created the comment.
    /// </summary>
    /// <param name="commentId">
    /// The ID of the comment.
    /// Example: "12345"
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of getCommentById method
    /// </param>
    /// <param name="headers">
    /// Headers of getCommentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CommentFull> GetCommentByIdAsync(string commentId, GetCommentByIdQueryParams? queryParams = default, GetCommentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Update the message of a comment.
    /// </summary>
    /// <param name="commentId">
    /// The ID of the comment.
    /// Example: "12345"
    /// </param>
    /// <param name="requestBody">
    /// Request body of updateCommentById method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of updateCommentById method
    /// </param>
    /// <param name="headers">
    /// Headers of updateCommentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CommentFull> UpdateCommentByIdAsync(string commentId, UpdateCommentByIdRequestBody? requestBody = default, UpdateCommentByIdQueryParams? queryParams = default, UpdateCommentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Permanently deletes a comment.
    /// </summary>
    /// <param name="commentId">
    /// The ID of the comment.
    /// Example: "12345"
    /// </param>
    /// <param name="headers">
    /// Headers of deleteCommentById method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task DeleteCommentByIdAsync(string commentId, DeleteCommentByIdHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

        /// <summary>
    /// Adds a comment by the user to a specific file, or
    /// as a reply to an other comment.
    /// </summary>
    /// <param name="requestBody">
    /// Request body of createComment method
    /// </param>
    /// <param name="queryParams">
    /// Query parameters of createComment method
    /// </param>
    /// <param name="headers">
    /// Headers of createComment method
    /// </param>
    /// <param name="cancellationToken">
    /// Token used for request cancellation.
    /// </param>
    public System.Threading.Tasks.Task<CommentFull> CreateCommentAsync(CreateCommentRequestBody requestBody, CreateCommentQueryParams? queryParams = default, CreateCommentHeaders? headers = default, System.Threading.CancellationToken? cancellationToken = null) => throw new System.NotImplementedException("This method needs to be implemented by the derived class before calling it.");

    }
}
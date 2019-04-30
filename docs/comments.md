Comments
========

Comment objects represent a user-created comment on a file. They can be added
directly to a file.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Get a Comment's Information](#get-a-comments-information)
- [Get the Comments on a File](#get-the-comments-on-a-file)
- [Add a Comment to a File](#add-a-comment-to-a-file)
- [Change a Comment's Message](#change-a-comments-message)
- [Delete a Comment](#delete-a-comment)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get a Comment's Information
---------------------------

To get information about a specific comment, call
`CommentsManager.GetInformationAsync(string id, IEnumerable<string> fields = null)`
with the ID of the comment.

<!-- sample get_comments_id -->
```c#
BoxComment comment = await client.CommentsManager.GetInformationAsync(id: "11111");
```

Get the Comments on a File
--------------------------

You can get all of the comments on a file by calling
`FilesManager.GetCommentsAsync(string id, IEnumerable<string> fields = null)`
with the ID of the file.

<!-- sample get_comments -->
```c#
string fileId = "11111";
BoxCollection<BoxComment> comments = await client.FilesManager.GetCommentsAsync(fileId);
```

Add a Comment to a File
-----------------------

A comment can be added to a file by calling
`CommentsManager.AddCommentAsync(BoxCommentRequest commentRequest, IEnumerable<string> fields = null)`.

<!-- sample post_comments -->
```c#
var requestParams = new BoxCommentRequest()
{
    Item = new BoxRequestEntity()
    {
        Type = BoxType.File,
        Id = "12345"
    },
    Message = "Great work!"
};
BoxComment comment = await client.CommentsManager.AddCommentAsync(requestParams);
```

Change a Comment's Message
--------------------------

The message of a comment can be changed by calling
`CommentsManager.UpdateAsync(string id, BoxCommentRequest commentsRequest, IEnumerable<string> fields = null)`
with the ID of the comment and the fields to update.

<!-- sample put_comments_id -->
```c#
var requestParams = new BoxCommentRequest()
{
    Message = "New message"
};
BoxComment updatedComment = await client.CommentsManager.UpdateAsync(id: "11111", requestParams);
```

Delete a Comment
----------------

A comment can be deleted by calling `CommentsManager.DeleteAsync(string id)` with the ID of the comment.

<!-- sample delete_comments_id -->
```c#
await client.CommentsManager.DeleteAsync(id: "11111");
```
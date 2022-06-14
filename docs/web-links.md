Web Links
=========

Web links are objects that point to URLs. These objects are also known as bookmarks within the Box web application.
Web link objects are treated similarly to file objects, so they will also support shared links, copy, permanent delete,
and restore.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create a Web Link](#create-a-web-link)
- [Get a Web Link's information](#get-a-web-links-information)
- [Update a Web Link](#update-a-web-link)
- [Delete a Web Link](#delete-a-web-link)
- [Copy a Web Link](#copy-a-web-link)
- [Create or update a Shared Link](#create-or-update-a-shared-link)
- [Remove a Shared Link](#remove-a-shared-link)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create a Web Link
-----------------

To create a web link call `WebLinksManager.CreateWebLinkAsync(BoxWebLinkRequest createWebLinkRequest)`.

<!-- sample post_web_links -->
```c#
var weblinkParams = new BoxWebLinkRequest()
{
    Url = new Uri("http://www.example.com"),
    Parent = new BoxRequestEntity()
    {
        Id = "22222"
    }
};
BoxWebLink link = await client.WebLinksManager.CreateWebLinkAsync(weblinkParams);
```

Get a Web Link's information
----------------------------

You can request a web link object by ID by calling `WebLinksManager.GetWebLinkAsync(string webLinkId)`
with the ID of the web link object.

<!-- sample get_web_links_id -->
```c#
BoxWebLink link = await client.WebLinksManager.GetWebLinkAsync("11111");
```

Update a Web Link
-----------------

To update a web link call the
`WebLinksManager.UpdateWebLinkAsync(string webLinkId, BoxWebLinkRequest updateWebLinkRequest)`
method with the fields to update and their new values.

<!-- sample put_web_links_id -->
```c#
var updates = new BoxWebLinkRequest()
{
    Name = "New Name for Weblink"
};
BoxWebLink updatedLink = await client.WebLinksManager.UpdateWebLinkAsync("11111", updates);
```

Delete a Web Link
-----------------

To move a web link to the trash call `WebLinksManager.DeleteWebLinkAsync(string webLinkId)`
with the ID of the web link object to delete.

<!-- sample delete_web_links_id -->
```c#
await client.WebLinksManager.DeleteWebLinkAsync("11111");
```

Copy a Web Link
---------------

To make a copy of a web link, call `WebLinksManager.CopyAsync(string webLinkId, string destinationFolderId, IEnumerable<string> fields = null)` with the ID of the web link and the ID of the folder into which it should be copied.

<!-- sample post_web_links_id_copy -->
```c#
BoxWebLink copiedLink = await client.WebLinksManager.CopyAsync("11111", "22222");
```

Create or update a Shared Link
--------------------

A shared link for a web link can be created or updated by calling
`WebLinksManager.CreateSharedLinkAsync(string id, BoxSharedLinkRequest sharedLinkRequest, IEnumerable<string> fields = null)`.

<!-- sample put_web_links_id add_shared_link -->
```c#
string webLinkId = "11111";
var sharedLinkParams = new BoxSharedLinkRequest()
{
    Access = BoxSharedLinkAccessType.open
};
BoxWebLink link = client.WebLinksManager
    .CreateSharedLinkAsync(webLinkId, sharedLinkParams);
string sharedLinkUrl = link.SharedLink.Url;
```

Remove a Shared Link
--------------------

To remove a shared link from a web link, call
`WebLinksManager.DeleteSharedLinkAsync(string id, IEnumerable<string> fields = null)`
with the ID of the web link.

<!-- sample put_web_links_id remove_shared_link -->
```c#
BoxWebLink updatedLink = client.WebLinksManager.DeleteSharedLinkAsync("11111");
```

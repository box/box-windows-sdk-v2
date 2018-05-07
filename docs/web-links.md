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

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create a Web Link
-----------------

To create a web link call `WebLinksManager.CreateWebLinkAsync(BoxWebLinkRequest createWebLinkRequest)`.

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

```c#
BoxWebLink link = await client.WebLinksManager.GetWebLinkAsync("11111");
```

Update a Web Link
-----------------

To update a web link call the
`WebLinksManager.UpdateWebLinkAsync(string webLinkId, BoxWebLinkRequest updateWebLinkRequest)`
method with the fields to update and their new values.

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

```c#
await client.WebLinksManager.DeleteWebLinkAsync("11111");
```
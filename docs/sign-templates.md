Sign Templates
==================

Box Sign enables you to create templates so you can automatically add the same fields and formatting to requests for signature.  With templates, you don't need to repetitively add the same fields to each request every time you send a new document for signature.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->

- [Get All Sign Templates](#get-all-sign-templates)
- [Get Sign Template by ID](#get-sign-template-by-id)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Get All Sign Templates
------------------------

Calling the `SignTemplatesManager.GetSignTemplatesAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)` method will return an iterable that will page through all the Sign Templates.

<!-- sample get_sign_templates -->
```c#
BoxCollectionMarkerBased<BoxSignTemplate> signTemplates = await client.SignTemplatesManager.GetSignTemplatesAsync();
```

Get Sign Template by ID
------------------------

Calling the `SignTemplatesManager.GetSignTemplateByIdAsync(string signTemplateId)` method will return the Sign Template with the given ID.

<!-- sample get_sign_templates_id -->
```c#
BoxSignTemplate signTemplate = await client.SignTemplatesManager.GetSignTemplateByIdAsync("12345");
```

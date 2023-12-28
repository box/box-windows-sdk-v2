Sign Requests
==================

Sign Requests are used to request e-signatures on documents from signers.  
A Sign Request can refer to one or more Box Files and can be sent to one or more Box Sign Request Signers.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create Sign Request](#create-sign-request)
- [Get all Sign Requests](#get-all-sign-requests)
- [Get Sign Request by ID](#get-sign-request-by-id)
- [Cancel Sign Request](#cancel-sign-request)
- [Resend Sign Request](#resend-sign-request)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create Sign Request
------------------------

The `SignRequestsManager.CreateSignRequestAsync(BoxSignRequestCreateRequest signRequestCreateRequest)`
method will create a Sign Request. You need to provide at least one file and up to ten files (from which the signing document will be created) with at least one signer to receive the Sign Request.

<!-- sample post_sign_requests -->
```c#
var sourceFiles = new List<BoxSignRequestCreateSourceFile>
{
    new BoxSignRequestCreateSourceFile()
    {
        Id = "12345"
    }
};

var signers = new List<BoxSignRequestSignerCreate>
{
    new BoxSignRequestSignerCreate()
    {
        Email = "example@gmail.com"
    }
};

var parentFolder = new BoxRequestEntity()
{
    Id = "12345",
    Type = BoxType.folder
};

var request = new BoxSignRequestCreateRequest
{
    SourceFiles = sourceFiles,
    Signers = signers,
    ParentFolder = parentFolder
};

BoxSignRequest signRequest = await client.SignRequestsManager.CreateSignRequestAsync(request);
```

If you set ```isDocumentPreparationNeeded``` flag to true, you need to visit ```prepareUrl``` before the Sign Request will be sent. 
For more information on ```isDocumentPreparationNeeded``` and the other parameters available, please refer to the [developer documentation](https://developer.box.com/guides/sign-request/).

Get All Sign Requests
------------------------

Calling the `SignRequestsManager.GetSignRequestsAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)`
will return an iterable that will page through all the Sign Requests.

<!-- sample get_sign_requests -->
```c#
BoxCollectionMarkerBased<BoxSignRequest> signRequests = await client.SignRequestsManager.GetSignRequestsAsync();
```

Get Sign Request by ID
------------------------

Calling `SignRequestsManager.GetSignRequestByIdAsync(string signRequestId)` will return an object
containing information about the Sign Request.

<!-- sample get_sign_requests_id -->
```c#
BoxSignRequest signRequest = await client.SignRequestsManager.GetSignRequestByIdAsync("12345");
```

Cancel Sign Request
------------------------

Calling `SignRequestsManager.CancelSignRequestAsync(string signRequestId)` will cancel a created Sign Request.

<!-- sample post_sign_requests_id_cancel -->
```c#
BoxSignRequest cancelledSignRequest = await client.SignRequestsManager.CancelSignRequestAsync("12345");
```

Resend Sign Request
------------------------

Calling `SignRequestsManager.ResendSignRequestAsync(string signRequestId)` will resend a Sign Request to all signers that have not signed it yet.
There is an 10-minute cooling-off period between re-sending reminder emails.

<!-- sample post_sign_requests_id_resend -->
```c#
await client.SignRequestsManager.ResendSignRequestAsync("12345");
```

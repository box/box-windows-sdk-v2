Terms of Service
================

Terms of Service are custom objects that the admin of an enterprise can configure. This will prompt the
end user to accept/re-accept or decline the custom Terms of Service for custom applications built on
Box Platform.

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Create a Terms of Service for an Enterprise](#create-a-terms-of-service-for-an-enterprise)
- [Update a Terms of Service for an Enterprise](#update-a-terms-of-service-for-an-enterprise)
- [Get a Terms of Service By ID](#get-a-terms-of-service-by-id)
- [Get Terms of Service for an Enterprise](#get-terms-of-service-for-an-enterprise)
- [Create User Status on Terms of Service](#create-user-status-on-terms-of-service)
- [Update User Status on Terms of Service](#update-user-status-on-terms-of-service)
- [Get Terms of Service Status for User](#get-terms-of-service-status-for-user)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

Create a Terms of Service for an Enterprise
-------------------------------------------

To create a terms of service call the
`TermsOfServiceManager.CreateTermsOfServicesAsync(BoxTermsOfServicesRequest termsOfServicesRequest)`
method with the parameters for the new terms of service.

<!-- sample post_terms_of_services -->
```c#
var tosParams = new CreateTermsOfServicesAsync()
{
    Status = "enabled"
    TosType = "external",
    Text = "By using this service, you agree to ..."
};
BoxTermsOfService tos = await client.TermsOfServiceManager.CreateTermsOfServicesAsync(tosParams);
```

Update a Terms of Service for an Enterprise
-------------------------------------------

To update a terms of service call
`TermsOfServiceManager.UpdateTermsOfServicesAsync(string tosId, BoxTermsOfServicesRequest termsOfServicesRequest)`
method with the fields to update and their new values.

<!-- sample put_terms_of_services_id -->
```c#
var updates = new BoxTermsOfServicesRequest()
{
    Status = "disabled",
    Text = "Updated Text"
};
BoxTermsOfService updatedToS = client.TermsOfServiceManager
    .UpdateTermsOfServicesAsync("11111", updates);
```

Get a Terms of Service By ID
----------------------------

To get the terms of service with an ID call 
`TermsOfServiceManager.GetTermsOfServicesByIdAsync(string tosId)`
with the ID of the terms of service object.

<!-- sample get_terms_of_services_id -->
```c#
BoxTermsOfService tos = await client.TermsOfServiceManager.GetTermsOfServicesByIdAsync("11111");
```

Get Terms of Service for an Enterprise
--------------------------------------

To get the terms of service for an enterprise, call
`TermsOfServiceManager.GetTermsOfServicesAsync(string tosType = null)`.

<!-- sample get_terms_of_services -->
```c#
BoxTermsOfServiceCollection<BoxTermsOfService> termsOfService = await client.TermsOfServiceManager
    .GetTermsOfServicesAsync();
```

Create User Status on Terms of Service 
--------------------------------------

For create user status on a terms of service call the
`TermsOfServiceManager.CreateBoxTermsOfServiceUserStatusesAsync(BoxTermsOfServiceUserStatusCreateRequest termsOfServiceUserStatusCreateRequest)`

You can only create a user status on a terms of service if the user has never accepted/declined a terms of service.
If they have then you will need to make the update call..

<!-- sample post_terms_of_service_user_statuses -->
```c#
var createStatusRequest = new BoxTermsOfServiceUserStatusCreateRequest()
{
    TermsOfService = new BoxRequestEntity()
    {
        Id = "11111",
        Type = BoxType.terms_of_service
    },
    User = new BoxRequestEntity()
    {
        Id = "22222",
        Type = BoxType.user
    },
    IsAccepted = true
};
BoxTermsOfServiceUserStatuses termsOfServiceUserStatuses =
    await client.TermsOfServiceManager.CreateBoxTermsOfServiceUserStatusesAsync(createStatusRequest);
```

Update User Status on Terms of Service 
--------------------------------------

To update user status on a terms of service call the
`TermsOfServiceManager.UpdateTermsofServiceUserStatusesAsync(string tosId, bool isAccepted)`
method with the ID of the status object, and the new acceptance value for the user.

It is important to note that this will accept or decline a custom terms of service for a user. For a user that has
taken action in this terms of service, this will update their status. If the user has never taken action on this terms
of service then this will return a 404 Not Found error. 

<!-- sample put_terms_of_service_user_statuses_id -->
```c#
BoxTermsOfServiceUserStatuses updatedStatus = await client.TermsOfServiceManager
    .UpdateTermsofServiceUserStatusesAsync("12345", false);
```

Get Terms of Service Status for User
------------------------------------

To get user status on a terms of service call the
`TermsOfServiceManager.GetTermsOfServiceUserStatusesAsync(string tosId, String userId = null)`
method with the ID of the terms of service.  If no user ID is provided, the method defaults to the
current user.

<!-- sample get_terms_of_service_user_statuses_id -->
```c#
BoxTermsOfServiceUserStatusesCollection<BoxTermsOfServiceUserStatuses> tosStatuses = await client.TermsOfServiceManager
    .GetTermsOfServiceUserStatusesAsync(tosId: "11111", userId: "22222");
```
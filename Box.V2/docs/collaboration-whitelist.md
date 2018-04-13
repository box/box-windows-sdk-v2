Collaboration Whitelists
========================

Collaboration Whitelists are used to determine specific email domains that can collaborate 
with a Box Enterprise.   Certain users can be exempted from these restrictions, for example
if they are a trusted person who needs to collaborate outside of the normally-allowed set of
domains.

Add a Domain to Collaboration Whitelist
---------------------------------------

You can whitelist a certain domain to allow collaboration with that domain for your
enterprise by calling
`CollaborationWhitelistManager.AddCollaborationWhitelistEntryAsync(string domainToWhitelist, string directionForWhitelist)`.

```c#
BoxCollaborationWhitelistEntry entry = await client.CollaborationWhitelistManager.AddCollaborationWhitelistEntryAsync(
    "example.com",
    "both"
);
```

Get a Whitelisted Domain's Information
--------------------------------------

Information about a specific collaboration whitelist record, which shows
the domain that is whitelisted, can be retrieved by calling
`CollaborationWhitelistManager.GetCollaborationWhitelistEntryAsync(string id, IEnumerable<string> fields = null)`.

```c#
string entryID = "11111";
BoxCollaborationWhitelistEntry entry = await client.CollaborationWhitelistManager.GetCollaborationWhitelistEntryAsync(
    entryID
);
```

Get Whitelisted Domains for an Enterprise
-----------------------------------------

You can retrieve the collection of whitelisted domains for an enterprise by calling
`CollaborationWhitelistManager.GetAllCollaborationWhitelistEntriesAsync(int limit= 100, string nextMarker = null, bool autoPaginate = false)`.

```c#
BoxCollectionMarkerBased<BoxCollaborationWhitelistEntry> whitelistedDomains = await client.CollaborationWhitelistManager
    .GetAllCollaborationWhitelistEntriesAsync();
```

You can set the `limit` parameter to determine how many results will be fetched, and use the `nextMarker`
parameter to manually page through the collection.  Alternatively, setting the `autoPaginate` parameter to `true` will
make as many API calls as necessary to retrieve the full collection and return it. 

Remove a Domain from Collaboration Whitelist
--------------------------------------------

You can remove a domain from the collaboration whitelist by calling
`CollaborationWhitelistManager.DeleteCollaborationWhitelistEntryAsync(string id)`.

```c#
string entryID = "11111";
await client.CollaborationWhitelistManager.DeleteCollaborationWhitelistEntryAsync(entryID);
```

Exempt a User from the Collaboration Whitelist
----------------------------------------------

You can make a specific user exempt from the collaboration whitelist, which
allows them to collaborate with users from any domain, by calling
`CollaborationWhitelistManager.AddCollaborationWhitelistExemptUserAsync(string userId)`.

```c#
string userId = "22222";
BoxCollaborationWhitelistTargetEntry exemptUser = await client.CollaborationWhitelistManager
    .AddCollaborationWhitelistExemptUserAsync(userId);
```

Get an Exempt User's Information
--------------------------------

To retrieve information about a specific user exemption record, you can use
`CollaborationWhitelistManager.GetCollaborationWhitelistExemptUserAsync(string id, IEnumerable<string> fields = null)`.

```c#
string exemptionID = "33333";
BoxCollaborationWhitelistTargetEntry exemptUser = await client.CollaborationWhitelistManager
    .GetCollaborationWhitelistExemptUserAsync(exemptionID);
```

Get All Exempt Users for an Enterprise
--------------------------------------

To retrieve a collection of users who are exempt from the collaboration whitelist
for an enterprise, call
`CollaborationWhitelistManager.GetAllCollaborationWhitelistExemptUsersAsync(int limit = 100, string nextMarker = null, bool autoPaginate = false)`.

```c#
BoxCollectionMarkerBased<BoxCollaborationWhitelistTargetEntry> = await client.CollaborationWhitelistManager
    .GetAllCollaborationWhitelistExemptUsersAsync();
```
You can set the `limit` parameter to determine how many results will be fetched, and use the `nextMarker`
parameter to manually page through the collection.  Alternatively, setting the `autoPaginate` parameter to `true` will
make as many API calls as necessary to retrieve the full collection and return it. 

Remove a User Exemption from the Collaboration Whitelist
--------------------------------------------------------

To remove a user exemption from collaboration whitelist and make that user
subject to whitelist restrictions again, you can call
`CollaborationWhitelistManager.DeleteCollaborationWhitelistExemptUserAsync(string id)`.

```c#
string exemptionId = "55555";
await client.CollaborationWhitelistManager.DeleteCollaborationWhitelistExemptUserAsync(exemptionId);
```

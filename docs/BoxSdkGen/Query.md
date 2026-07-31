# IQueryManager


- [Query for Box items](#query-for-box-items)
- [Create insights for Box items](#create-insights-for-box-items)

## Query for Box items

Runs a query to discover Box items using a logical predicate that can filter
across item fields and metadata templates. Results can be sorted, paginated,
and shaped to include additional item or metadata fields.

This operation is performed by calling function `CreateQueryV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/post-query/).

<!-- sample post_query_v2026.0 -->
```
await client.Query.CreateQueryV2026R0Async(requestBody: new QueryRequestBodyV2026R0(query: new QueryRequestBodyV2026R0QueryField(predicate: predicate) { Parameters = new Dictionary<string, object>() { { "name", "John" }, { "age", 50 } }, Ancestors = Array.AsReadOnly(new [] {new QueryAncestorReferenceV2026R0(id: "0", type: "folder")}) }) { Limit = 10, Fields = Array.AsReadOnly(new [] {"box:item:name",searchFrom}) });
```

### Arguments

- requestBody `QueryRequestBodyV2026R0`
  - Request body of createQueryV2026R0 method
- headers `CreateQueryV2026R0Headers`
  - Headers of createQueryV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `QueryResultsV2026R0`.

Returns a paginated list of items matching the query.


## Create insights for Box items

Computes aggregated metrics over Box items matching a query predicate.
Filters are applied first, followed by optional grouping, after which the
requested metrics (such as `sum`, `avg`, `min`, `max`, and `count`) are
computed for each resulting group or over the entire filtered dataset.

This operation is performed by calling function `CreateQueryInsightV2026R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2026.0/post-query-insights/).

<!-- sample post_query_insights_v2026.0 -->
```
await client.Query.CreateQueryInsightV2026R0Async(requestBody: new QueryInsightsRequestBodyV2026R0(query: new QueryInsightsRequestBodyV2026R0QueryField(predicate: predicate) { Parameters = new Dictionary<string, object>() { { "minAmount", 0 } }, Ancestors = Array.AsReadOnly(new [] {new QueryAncestorReferenceV2026R0(id: "0", type: "folder")}), GroupBy = Array.AsReadOnly(new [] {new QueryInsightsGroupByV2026R0(field: string.Concat(mdPrefix, ".category")) { BucketLimit = 5 }}) }, metrics: metrics));
```

### Arguments

- requestBody `QueryInsightsRequestBodyV2026R0`
  - Request body of createQueryInsightV2026R0 method
- headers `CreateQueryInsightV2026R0Headers`
  - Headers of createQueryInsightV2026R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `QueryInsightsV2026R0`.

Returns the computed insight entries.



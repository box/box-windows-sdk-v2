# IDocgenManager


- [Get Box Doc Gen job by ID](#get-box-doc-gen-job-by-id)
- [List all Box Doc Gen jobs](#list-all-box-doc-gen-jobs)
- [Get Box Doc Gen jobs by batch ID](#get-box-doc-gen-jobs-by-batch-id)
- [Generate document using Box Doc Gen template](#generate-document-using-box-doc-gen-template)

## Get Box Doc Gen job by ID

Get details of the Box Doc Gen job.

This operation is performed by calling function `GetDocgenJobByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-jobs-id/).

<!-- sample get_docgen_jobs_id_v2025.0 -->
```
await client.Docgen.GetDocgenJobByIdV2025R0Async(jobId: docgenJobItemFromList.Id);
```

### Arguments

- jobId `string`
  - Box Doc Gen job ID. Example: 123
- headers `GetDocgenJobByIdV2025R0Headers`
  - Headers of getDocgenJobByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenJobV2025R0`.

Details of the Box Doc Gen job.


## List all Box Doc Gen jobs

Lists all Box Doc Gen jobs for a user.

This operation is performed by calling function `GetDocgenJobsV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-jobs/).

<!-- sample get_docgen_jobs_v2025.0 -->
```
await client.Docgen.GetDocgenJobsV2025R0Async(queryParams: new GetDocgenJobsV2025R0QueryParams() { Limit = 500 });
```

### Arguments

- queryParams `GetDocgenJobsV2025R0QueryParams`
  - Query parameters of getDocgenJobsV2025R0 method
- headers `GetDocgenJobsV2025R0Headers`
  - Headers of getDocgenJobsV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenJobsFullV2025R0`.

A list of Box Doc Gen jobs.


## Get Box Doc Gen jobs by batch ID

Lists Box Doc Gen jobs in a batch.

This operation is performed by calling function `GetDocgenBatchJobByIdV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/get-docgen-batch-jobs-id/).

<!-- sample get_docgen_batch_jobs_id_v2025.0 -->
```
await client.Docgen.GetDocgenBatchJobByIdV2025R0Async(batchId: docgenBatch.Id);
```

### Arguments

- batchId `string`
  - Box Doc Gen batch ID. Example: 123
- queryParams `GetDocgenBatchJobByIdV2025R0QueryParams`
  - Query parameters of getDocgenBatchJobByIdV2025R0 method
- headers `GetDocgenBatchJobByIdV2025R0Headers`
  - Headers of getDocgenBatchJobByIdV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenJobsV2025R0`.

Returns a list of Box Doc Gen jobs in a Box Doc Gen batch.


## Generate document using Box Doc Gen template

Generates a document using a Box Doc Gen template.

This operation is performed by calling function `CreateDocgenBatchV2025R0`.

See the endpoint docs at
[API Reference](https://developer.box.com/reference/v2025.0/post-docgen-batches/).

<!-- sample post_docgen_batches_v2025.0 -->
```
await client.Docgen.CreateDocgenBatchV2025R0Async(requestBody: new DocGenBatchCreateRequestV2025R0(file: new FileReferenceV2025R0(id: uploadedFile.Id), inputSource: "api", destinationFolder: new DocGenBatchCreateRequestV2025R0DestinationFolderField(id: folder.Id), outputType: "pdf", documentGenerationData: Array.AsReadOnly(new [] {new DocGenDocumentGenerationDataV2025R0(generatedFileName: "test", userInput: new Dictionary<string, object>() { { "abc", "xyz" } })})));
```

### Arguments

- requestBody `DocGenBatchCreateRequestV2025R0`
  - Request body of createDocgenBatchV2025R0 method
- headers `CreateDocgenBatchV2025R0Headers`
  - Headers of createDocgenBatchV2025R0 method
- cancellationToken `System.Threading.CancellationToken?`
  - Token used for request cancellation.


### Returns

This function returns a value of type `DocGenBatchBaseV2025R0`.

The created Batch ID.



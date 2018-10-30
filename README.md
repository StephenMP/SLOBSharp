# SLOBSharp
A .NET Standard Client library used to integrate with StreamLabs OBS

|Build|Release|Code Coverage|
|:---:|:-----:|:-----------:|
|[![Build Status](https://travis-ci.org/StephenMP/SLOBSharp.svg?branch=master)](https://travis-ci.org/StephenMP/SLOBSharp)|[![Build Status](https://travis-ci.org/StephenMP/SLOBSharp.svg?branch=release)](https://travis-ci.org/StephenMP/SLOBSharp) [![NuGet](https://img.shields.io/nuget/v/SLOBSharp.svg)](https://www.nuget.org/packages/SLOBSharp/) [![NuGet](https://img.shields.io/nuget/dt/SLOBSharp.svg)](https://www.nuget.org/packages/SLOBSharp/)|[![codecov](https://codecov.io/gh/StephenMP/SLOBSharp/branch/master/graph/badge.svg)](https://codecov.io/gh/StephenMP/SLOBSharp)|

## Current API Support
Currently, this project only supports using named pipes for [StreamLabs OBS](https://streamlabs.com/streamlabs-obs). WebSocket support may be added at a later date.

## Simple Example
```csharp
/* 
 * This example will get the currently active scene in SLOBS
 * Make sure you have SLOBS running and that you've enabled named pipes before trying
 * See https://github.com/stream-labs/streamlabs-obs-api-docs#how-to-connect-web-application-to-streamlabs-obs
 * on how to enable named pipes in SLOBS
 */

// Constructor takes the name of the pipe (default constructor uses the pipe name "slobs")
var client = new SlobsPipeClient("slobs");

// Build our request
var slobsRequest = SlobsRequestBuilder.NewRequest().SetMethod("activeScene").SetResource("ScenesService").BuildRequest();

// Issue the request
var slobsRpcResponse = await this.slobsClient.ExecuteRequestAsync(request).ConfigureAwait(false);

// Get the result
var activeScene = slobsRpcResponse.Result.FirstOrDefault();
```

## Description

Library to provide a basic set of calls to the Pixum API.

## Usage

### General

The Pixum API is divided into different services wich provide different actions to call.
Most calls are asynchrounous operations and return a `Task<T>`

### Auth

Before using the API you need to get a valid session id by authenticating yourself with an AuthUser and AuthPassword.
We also need a valid login token wich will be later used for logging in a Pixum user account.

##### Init API

```csharp
var sessionId = PixumApi.GetSessionId(authUser, authPassword);
var loginToken = PixumApi.GetLoginToken(sessionId);
var pixumApi = new PixumApi(sessionId, loginToken);
```

##### Logging in

```csharp
var loginInformation = pixumApi.Login(email, password);
```

### Album

All album specific methods live under `PixumApi.Album.*`

For example:

##### Get album list

```csharp
// Getting albums and inbox
var albums = await pixumApi.Album.GetAlbums(withInbox: true, withAlbum: true);

foreach(var album in albums.album.items)
{
	Console.WriteLine(albums.title);
}
```

##### Creating a album

```csharp
var albumInformation = await pixumApi.Album.CreateAlbum("Album title", "Album description");
Console.WriteLine(albumInformation.title);
```

### Image

All image specific methods live under `PixumAPI.Image.*`

For example:

##### Upload an image

```csharp
// Read image into memory
var imageData = System.IO.File.ReadAllBytes(@"Testfiles\Image.jpg");
var uploadResult = await pixumApi.Image.DataCreate(imageData, "Image title");
```

##### Change Image title

````csharp
var imageInformation = await pixumApi.Image.SetTitle(imageId, "New Image title");
Console.WriteLine(imageInformation.title);
```

## Tests

In order to run the tests fill the configuration file with a valid auth user and pixum user account.
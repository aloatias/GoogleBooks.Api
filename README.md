# GoogleBooks.Api
The APIs in the Google Books API Family let you bring Google Books features to your site or application. The new Google Books API lets you perform programmatically most of the operations that you can do interactively on the Google Books website. The Embedded Viewer API lets you embed the content directly into your site.  At Google, we're constantly striving to make information available to as many people as possible, and our APIs were designed with that goal in mind. However, we license much of the data that we use to power Google Books, so it's not ours to distribute however we choose.  The API is not intended to be used as a replacement for commercial services. These services are valuable and Google also relies upon them to build our own APIs for the general public.  The Google Terms of Service for use of the APIs is available at https://developer.google.com/books/terms.html. We will suspend a user's access to the APIs if a user violates the Terms of Service and does not take action to remedy the violation after notice of violation by Google.

IDEs
You can just launch the "GoogleBooks.Api" project on your Visual Studio/VS Code and a web browser will open to the Swashbuckle UI for you to start testing the APIs.

Docker (Windows using Linux containers) -> You can download docker here: https://docs.docker.com/docker-for-windows/install/
Using powershell go inside "GoogleBooks\GoogleBooks.Api\GoogleBooks.Api" and execute the following commands:
1) dotnet user-secrets set "Kestrel:Certificates:Development:Password" "GoogleBooksApi"
2) dotnet dev-certs https -ep $Env:USERPROFILE\.aspnet\https\GoogleBooks.Api.pfx -p GoogleBooksApi
3) dotnet dev-certs https --trust
4) dotnet user-secrets -p .\GoogleBooks.Api.csproj set "Kestrel:Certificates:Development:Password" "GoogleBooksApi"
5) cd..
6) docker build --pull -t googlebooksapi .
7) docker run --rm -it -p 5000:80 -p 5001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -v $Env:APPDATA\microsoft\UserSecrets\:/root/.microsoft/usersecrets -v $Env:USERPROFILE\.aspnet\https:/root/.aspnet/https/ googlebooksapi

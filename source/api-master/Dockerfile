FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
COPY dist /home
WORKDIR /home
ENTRYPOINT ["dotnet", "WebApi.dll"]
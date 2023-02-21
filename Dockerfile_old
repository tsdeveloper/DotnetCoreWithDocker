FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_URLS http://*:5008 #change IP dotnet run


#SET ENV DOTNET CORE
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 5008



FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Ecommerce.API/Ecommerce.API.csproj", "Ecommerce.API/"]
RUN dotnet restore "Ecommerce.API/Ecommerce.API.csproj"
COPY /src/. .
WORKDIR "/src/Ecommerce.API"
RUN dotnet build "Ecommerce.API.csproj" -c Release -o /app/build


FROM build as publish
RUN dotnet publish "Ecommerce.API.csproj" -c Release -o /app/publish


FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.API.dll"]

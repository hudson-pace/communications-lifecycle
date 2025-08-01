FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /App
EXPOSE 8080
EXPOSE 8081

# copy everything
COPY ./CommLifecycle.Web/CommLifecycle.Web.csproj ./CommLifecycle.Web/CommLifecycle.Web.csproj
COPY ./SharedModels ./SharedModels
# restore as distinct layers
RUN dotnet restore ./CommLifecycle.Web/CommLifecycle.Web.csproj



WORKDIR /App/CommLifecycle.Web

FROM dev AS build
# build and publish as a release

COPY ./CommLifecycle.Web ./CommLifecycle.Web
COPY ./SharedModels ./SharedModels

RUN dotnet clean


# already ran restore, so --no-restore
RUN dotnet publish -c Release -o /App/out --no-restore

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS prod
WORKDIR /App
COPY --from=build /App/out .
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /App
EXPOSE 5075

# copy everything
RUN ls
COPY ./CommLifecycle.Api/ ./CommLifecycle.Api/
COPY ./SharedModels ./SharedModels

# restore as distinct layers
RUN dotnet restore ./CommLifecycle.Api/CommLifecycle.Api.csproj

FROM dev AS build
# build and publish as a release
RUN dotnet clean ./CommLifecycle.Api/CommLifecycle.Api.csproj

RUN dotnet --info && ls -la && find . -name '*.csproj'
# already ran restore, so --no-restore
RUN dotnet publish ./CommLifecycle.Api/CommLifecycle.Api.csproj -c Release -o /App/out --no-restore

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS prod
WORKDIR /App
COPY --from=build /App/out .
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dev
WORKDIR /App
EXPOSE 8080
EXPOSE 8081

# copy everything
COPY ./BlazorApp1/BlazorApp1.csproj ./BlazorApp1/BlazorApp1.csproj
COPY ./SharedModels ./SharedModels
# restore as distinct layers
RUN dotnet restore ./BlazorApp1/BlazorApp1.csproj



WORKDIR /App/BlazorApp1

FROM dev AS build
# build and publish as a release

COPY ./BlazorApp1 ./BlazorApp1
COPY ./SharedModels ./SharedModels

RUN dotnet clean


# already ran restore, so --no-restore
RUN dotnet publish -c Release -o /App/out --no-restore

# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS prod
WORKDIR /App
COPY --from=build /App/out .
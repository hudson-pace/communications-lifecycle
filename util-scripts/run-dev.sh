#!/bin/bash
docker build --target dev -t counter-image -f ./BlazorApp1/Dockerfile .
docker build --target prod -t movie-api-image -f ./MoviesApi/Dockerfile .
docker compose -p blazor-app -f compose.yaml -f compose.override.yaml up
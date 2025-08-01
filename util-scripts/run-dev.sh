#!/bin/bash
docker build --target dev -t web-image -f ./CommLifecycle.Web/Dockerfile .
docker build --target prod -t api-image -f ./CommLifecycle.Api/Dockerfile .
docker compose -p comm-lifecycle -f compose.yaml -f compose.override.yaml up -d
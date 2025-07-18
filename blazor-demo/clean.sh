#!/bin/bash
echo "Tearing down containers and volumes."
docker compose down -v

echo "Removing dangling images and cache."
docker system prune -f

echo "Running dotnet restore."
dotnet restore
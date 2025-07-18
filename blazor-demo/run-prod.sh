#!bin/bash
docker build --target prod -t counter-image .
docker compose -p blazor-app -f compose.yaml up
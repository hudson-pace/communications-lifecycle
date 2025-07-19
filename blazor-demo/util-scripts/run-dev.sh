#!/bin/bash
docker build --target dev -t counter-image .
docker compose -p blazor-app -f compose.yaml -f compose.override.yaml up
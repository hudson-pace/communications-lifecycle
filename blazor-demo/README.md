To Start Blazor App locally in Docker

create .env file in base directory containing
OKTA_DOMAIN
OKTA_CLIENT_ID
OKTA_CLIENT_SECRET
OKTA_AUTHORIZATION_SERVER_ID (default)
DB_PASSWORD=Password1


okta config:
Sign-in redirect URI : http://localhost:8000/authorization-code/callback
Sign-out redirect URI : http://localhost:8000/signout/callback

Then run docker compose as usual.
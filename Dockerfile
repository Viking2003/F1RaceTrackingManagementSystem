# ── Stage 1: Build ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY F1RaceTracker.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish F1RaceTracker.csproj -c Release -o /app/out --no-restore

# ── Stage 2: Runtime ─────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

ENV ASPNETCORE_ENVIRONMENT=Production

# Use shell form so $PORT expands — works on both Railway ($PORT) and Fly.io (8080 default)
ENTRYPOINT ["sh", "-c", "dotnet F1RaceTracker.dll --urls=http://0.0.0.0:${PORT:-8080}"]

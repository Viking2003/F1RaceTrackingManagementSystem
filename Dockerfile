# ── Stage 1: Build ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies first (layer caching)
COPY F1RaceTracker.csproj ./
RUN dotnet restore

# Copy everything else and publish
COPY . .
RUN dotnet publish F1RaceTracker.csproj -c Release -o /app/out --no-restore

# ── Stage 2: Runtime ─────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/out .

# SQLite database will be written here at runtime
# Railway provides a writable /app directory
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:$PORT

# Railway injects $PORT at runtime — use shell form so the variable expands
ENTRYPOINT ["sh", "-c", "dotnet F1RaceTracker.dll --urls=http://0.0.0.0:$PORT"]

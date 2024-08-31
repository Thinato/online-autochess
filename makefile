
game:
	source ./client/venv/bin/activate && python3 ./client/src/main.py

server-build:
	find server -name "*.csproj" -exec dotnet build {} \;

server-run-api:
	dotnet run --project server/src/API/API.csproj

server-run-all:
	@find server -name "*.csproj" | while read csproj; do \
		dotnet run --project "$$csproj"; \
	done

# server-run:
# 	dotnet run --project server/server.csproj

# server-watch:
# 	dotnet watch --project server/server.csproj 

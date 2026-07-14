.PHONY: tests run sbom clean

# Default target
all: run

# Run the test suite
tests:
	dotnet test --logger "console;verbosity=detailed" DiceyStarClusterTests/DiceyStarClusterTests.csproj

# Run the application
run:
	dotnet run --project DiceyStarCluster.csproj

# Generate SBOM and scan for vulnerabilities
sbom:
	@echo "Generating SBOM with syft..."
	syft dir:. --source-name DiceyStarCluster --source-version 1.0.0 -o cyclonedx-json > sbom.json
	@echo "Scanning SBOM for vulnerabilities with grype..."
	grype sbom.json

# Clean build artifacts
clean:
	find . -type d -name "bin" -exec rm -rf {} +
	find . -type d -name "obj" -exec rm -rf {} +
	rm -f sbom.json

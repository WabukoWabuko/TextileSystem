# TextileSystem/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Install Avalonia templates
RUN dotnet new install Avalonia.Templates

# Set working directory
WORKDIR /src

# Keep the container running for interactive use
CMD ["bash"]
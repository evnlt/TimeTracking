FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /src

COPY . .
RUN dotnet restore

WORKDIR /src/TimeTracker.Tests

CMD ["dotnet", "test", "--logger:console;verbosity=normal"]
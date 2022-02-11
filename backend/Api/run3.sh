#!/bin/zsh

dotnet build .
./bin/Debug/net6.0/Api --urls="http://localhost:1113;https://localhost:1132"



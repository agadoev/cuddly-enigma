#!/bin/zsh

dotnet build .
./bin/Debug/net6.0/Api --urls="http://localhost:1111;https://localhost:1112"



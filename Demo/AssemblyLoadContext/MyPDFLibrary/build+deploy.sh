#!/bin/bash
rm -rf ../FavoritePlaces/bin/Debug/netcoreapp3.1/MyPDFLibrary/
dotnet publish -c Debug -r osx-x64 -o ../FavoritePlaces/bin/Debug/netcoreapp3.1/MyPDFLibrary --self-contained
# We don't want our LoadContext to take in account the existing MyPDFLibrary.deps.json and fully handle it ourselves. 
rm ../FavoritePlaces/bin/Debug/netcoreapp3.1/MyPDFLibrary/MyPDFLibrary.deps.json
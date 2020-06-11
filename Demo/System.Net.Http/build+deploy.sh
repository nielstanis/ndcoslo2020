#!/bin/bash
rm ../AssemblyLoadContext/FavoritePlaces/bin/debug/netcoreapp3.1/MyPDFLibrary/System.Net.Http.*
dotnet publish -c Debug  -o ../AssemblyLoadContext/FavoritePlaces/bin/debug/netcoreapp3.1/MyPDFLibrary

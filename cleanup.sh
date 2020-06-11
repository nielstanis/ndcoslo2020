#!/bin/bash

#remove published version of app
rm -rf ./Demo/AssemblyLoadContext/Published

#remove all databases
rm -rf ./Demo/FavoritePlaces/Data/places.sqlite
rm -rf ./Demo/AssemblyLoadContext/FavoritePlaces/Data/places.sqlite
rm -rf ./Demo/NamedPipes/FavoritePlaces/Data/places.sqlite

#remove all PDF output
rm -rf ./Demo/FavoritePlaces/PDF
rm -rf ./Demo/AssemblyLoadContext/FavoritePlaces/PDF
rm -rf ./Demo/NamedPipes/FavoritePlaces/PDF

rm -rf ./Demo/FavoritePlaces/*.pdf
rm -rf ./Demo/AssemblyLoadContext/FavoritePlaces/*.pdf
rm -rf ./Demo/NamedPipes/FavoritePlaces/*.pdf
rm -rf ./Demo/FavoritePlaces/wwwroot/*.pdf
rm -rf ./Demo/AssemblyLoadContext/FavoritePlaces/wwwroot/*.pdf
rm -rf ./Demo/NamedPipes/FavoritePlaces/wwwroot/*.pdf

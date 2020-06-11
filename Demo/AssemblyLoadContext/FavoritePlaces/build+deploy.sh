#!/bin/bash
dotnet publish -c Debug -r osx-x64 -o ../Published --self-contained

#remove old version and move output to right directory.
cp -R Images ../Published/Images/
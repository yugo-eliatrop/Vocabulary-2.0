#!/bin/bash

cd ./Frontend
npm install
npm run build
cd ../
dotnet publish --configuration Production
dotnet ./bin/Production/netcoreapp3.0/Vocabulary.dll

dotnet tool restore
dotnet husky install

cd client
npm install

Set-Content -Path ".env" -Value @"
VITE_APP_PATH_BASE=""
"@

cd ..

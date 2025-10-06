dotnet tool restore
dotnet husky install

cd client
npm install

cat <<EOF > .env
VITE_APP_PATH_BASE=""
EOF

cd ..

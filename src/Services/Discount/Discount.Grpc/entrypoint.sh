#!/bin/bash
# Ensure permissions at runtime
mkdir -p /app/datadb
chmod -R 777 /app/datadb

# Start the application
exec dotnet Discount.Grpc.dll
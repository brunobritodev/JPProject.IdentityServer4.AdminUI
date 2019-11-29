Set-Location ..
docker build -f .\build\continuous-delivery\admin-prod.dockerfile -t bhdebrito/jpproject-admin-ui:prd .
docker push bhdebrito/jpproject-admin-ui:prd


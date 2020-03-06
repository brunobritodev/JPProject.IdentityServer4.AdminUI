Set-Location ..
docker build -f .\build\continuous-delivery\admin-prod.dockerfile -t bhdebrito/jpproject-admin-ui:prd .
docker push bhdebrito/jpproject-admin-ui:prd

docker build -f admin-ui.dockerfile -t bhdebrito/jpproject-admin-ui:3.0.3 .
docker push bhdebrito/jpproject-admin-ui:3.0.3
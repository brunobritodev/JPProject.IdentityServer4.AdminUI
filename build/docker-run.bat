@echo off
cd /d %~dp0
ECHO This script will update your HOST file with an entry 127.0.0.1 jpproject (Pre req for docker-compose). Then will run compose
pause
start /shared update-host.bat

ECHO Running compose
docker-compose up
PAUSE

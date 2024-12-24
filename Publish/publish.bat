@echo off
echo.
echo Iniciando a publicacao...

echo.
echo Diretorio de trabalho atual:
cd /d "C:\Projetos\EasyRecruter\Publish"
echo %cd%

echo.
echo Limpando a pasta wwwroot...
rd /s /q "%~dp0\..\Backend\wwwroot\"

echo.
echo Limpando a pasta dist...
rd /s /q "%~dp0\..\Frontend\dist\"

echo.
echo Limpando a pasta out...
rd /s /q "%~dp0\..\Publish\deploy\"

echo.
echo Construindo o front-end...
pushd "%~dp0\..\Frontend"
echo Executando npm run build...
call npm run build
echo Codigo de saida do npm run build: %ERRORLEVEL%
popd

echo.
echo Conteudo da pasta dist:
dir "%~dp0\..\Frontend\dist"

echo.
echo Copiando arquivos do front-end para a pasta wwwroot...
xcopy /s /e /y /h /r /f "%~dp0\..\Frontend\dist\*.*" "%~dp0\..\Backend\wwwroot\"

echo.
echo Gerando o projeto de backend...
pushd "%~dp0\..\Backend"
dotnet publish -c Release -o "%~dp0\..\Publish\deploy"
popd

echo.
echo Publicacao concluida! Os arquivos estao disponiveis na pasta 'deploy'.
pause
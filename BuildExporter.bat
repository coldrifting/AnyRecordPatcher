cd AnyRecordExporter
dotnet publish -c Release -r win10-x64 --no-self-contained -o ..\Exporter -p:PublishSingleFile=true -p:PublishSingleFile=True

cd ..\Exporter

del *.pdb

@echo off
echo # The config file for AnyRecordExporter. You can either place this in the same directory as the exporter application,>settings.yaml
echo # or specify a path to a yaml file as the first command line argument.>>settings.yaml
echo # Once you have configs, you'll need to import AnyRecordPatcher into synthesis>>settings.yaml 
echo # and place their folders into the user data folder>>settings.yaml
echo.>>settings.yaml
echo #Path: Your Path Here. Defaults to User Desktop folder>>settings.yaml
echo.>>settings.yaml
echo # The plugin to patch changes overrides against. The next previous override/definition will be compared and the differences exported>>settings.yaml
echo Plugin: Unofficial Skyrim Special Edition Patch.esp>>settings.yaml 
echo.>>settings.yaml
echo # Book text can bloat a config file, and it's usually not needed>>settings.yaml
echo IgnoreBookText: true>>settings.yaml 

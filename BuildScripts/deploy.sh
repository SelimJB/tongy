echo Clearning Up Build Directory
rm -rf ../build/

echo Starting Build Process
'C:/Program Files/Unity/Hub/Editor/2019.4.9f1/Editor/Unity.exe' -quit -batchmode -projectPath ../ -executeMethod BuildScript.PerformBuild
echo Ended Build Process
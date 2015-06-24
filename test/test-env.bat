rem %ComputerName% should always be set.
..\build\bin\debug\echoer.exe -env ComputerName

rem %ABC123% shouldn't be set.
..\build\bin\debug\echoer.exe -env ABC123
@pause

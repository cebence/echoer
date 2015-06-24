rem Simulate a failed long running process.
..\build\bin\debug\echoer.exe -out "Working ..." -wait 10 -err "FAILED" -exit 5
@pause

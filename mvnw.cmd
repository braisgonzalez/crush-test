@ECHO OFF
set JAVA_EXE=%JAVA_HOME%\bin\java.exe
if exist "%JAVA_EXE%" goto found
set JAVA_EXE=java
:found
"%JAVA_EXE%" -Dmaven.multiModuleProjectDirectory=%CD% -cp .mvn/wrapper/maven-wrapper.jar org.apache.maven.wrapper.MavenWrapperMain %*

#!/usr/bin/env sh
JAVA_EXE="${JAVA_HOME}/bin/java"
[ -x "$JAVA_EXE" ] || JAVA_EXE="java"
exec "$JAVA_EXE" -Dmaven.multiModuleProjectDirectory="$PWD" -cp .mvn/wrapper/maven-wrapper.jar org.apache.maven.wrapper.MavenWrapperMain "$@"
